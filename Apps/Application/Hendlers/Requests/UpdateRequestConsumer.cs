using System.Linq;
using System.Threading.Tasks;
using Apps.MVCApp.Models;
using MassTransit;

namespace Apps.MVCApp.Application.Hendlers.Requests
{
    public class UpdateRequestConsumer : IConsumer<UpdateRequestCommand>
    {
        MVCAppContext _DBcontext;
        public UpdateRequestConsumer(MVCAppContext dbContext)
        {
            _DBcontext = dbContext;
        }
        public async Task Consume(ConsumeContext<UpdateRequestCommand> context)
        {
            if (!_DBcontext.requests.Any(x => x.id == context.Message.Request.id))
                await context.RespondAsync(new UpdateRequestResult { Succeeded = false, Text = "Item is not found!" });

            _DBcontext.Update(context.Message.Request);

            if (await _DBcontext.SaveChangesAsync() == 1)
                await context.RespondAsync(new UpdateRequestResult { Succeeded = true, Text = "Item is updated" });
            else
                await context.RespondAsync(new UpdateRequestResult { Succeeded = false, Text = "Item is not updated" });
        }
    }
    public class UpdateRequestCommand
    {
        public Request Request { get; set; }

    }
    public class UpdateRequestResult
    {
        public bool Succeeded { get; set; }
        public string Text { get; set; }
    }
}
