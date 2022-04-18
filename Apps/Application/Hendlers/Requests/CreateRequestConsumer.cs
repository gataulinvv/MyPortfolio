using System.Threading.Tasks;
using Apps.MVCApp.Models;
using MassTransit;

namespace Apps.MVCApp.Application.Hendlers.Requests
{
    public class CreateRequestConsumer : IConsumer<CreateRequestCommand>
    {
        MVCAppContext _DBcontext;
        public CreateRequestConsumer(MVCAppContext DBcontext)
        {
            _DBcontext = DBcontext;
        }
        public async Task Consume(ConsumeContext<CreateRequestCommand> context)
        {
            await _DBcontext.requests.AddAsync(context.Message.Request);

            if (_DBcontext.SaveChanges() == 1)
                await context.RespondAsync(new CreateRequestResult { Succeeded = true, Text = "Item was created" });
            else
                await context.RespondAsync(new CreateRequestResult { Succeeded = false, Text = "Item not created" });
        }
    }
    public class CreateRequestCommand
    {
        public Request Request { get; set; }
    }
    public class CreateRequestResult
    {
        public bool Succeeded { get; set; }
        public string Text { get; set; }
    }
}
