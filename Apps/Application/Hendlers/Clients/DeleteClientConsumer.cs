using System.Linq;
using System.Threading.Tasks;
using Apps.MVCApp.Models;
using MassTransit;

namespace Apps.MVCApp.Application.Hendlers.Clients
{
    public class DeleteClientConsumer : IConsumer<DeleteClientCommand>
    {
        MVCAppContext _DBcontext;
        public DeleteClientConsumer(MVCAppContext DBcontext)
        {
            _DBcontext = DBcontext;
        }
        public async Task Consume(ConsumeContext<DeleteClientCommand> context)
        {
            var client = _DBcontext.clients.FirstOrDefault(x => x.id == context.Message.ClientId);

            if (client == null)
                await context.RespondAsync(new DeleteClientResult { Succeeded = false, Text = "Item not found!" });

            _DBcontext.clients.Remove(client);

            if (_DBcontext.SaveChanges() == 1)
                await context.RespondAsync(new DeleteClientResult { Succeeded = true, Text = "Item was deleted" });
            else
                await context.RespondAsync(new DeleteClientResult { Succeeded = false, Text = "Item not deleted" });
        }
    }
    public class DeleteClientCommand
    {
        public int ClientId { get; set; }
    }
    public class DeleteClientResult
    {
        public bool Succeeded { get; set; }
        public string Text { get; set; }
    }
}
