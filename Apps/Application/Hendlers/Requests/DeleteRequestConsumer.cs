using System.Linq;
using System.Threading.Tasks;
using Apps.MVCApp.Models;
using MassTransit;

namespace Apps.MVCApp.Application.Hendlers.Requests
{
    public class DeleteRequestConsumer : IConsumer<DeleteRequestCommand>
    {
        MVCAppContext _DBcontext;
        public DeleteRequestConsumer(MVCAppContext DBcontext)
        {

            _DBcontext = DBcontext;
        }

        public async Task Consume(ConsumeContext<DeleteRequestCommand> context)
        {
            var request = _DBcontext.requests.FirstOrDefault(x => x.id == context.Message.RequestId);

            if (request == null)
                await context.RespondAsync(new DeleteReuestResult { Succeeded = false, Text = "Item not found!" });

            _DBcontext.requests.Remove(request);

            if (_DBcontext.SaveChanges() == 1)
                await context.RespondAsync(new DeleteReuestResult { Succeeded = true, Text = "Item was deleted" });
            else
                await context.RespondAsync(new DeleteReuestResult { Succeeded = false, Text = "Item not deleted" });

        }
    }
    public class DeleteRequestCommand
    {
        public int RequestId { get; set; }

    }
    public class DeleteReuestResult
    {
        public bool Succeeded { get; set; }
        public string Text { get; set; }
    }
}
