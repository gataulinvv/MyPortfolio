using System.Threading.Tasks;
using Apps.MVCApp.Models;
using MassTransit;

namespace Apps.MVCApp.Application.Hendlers.Clients
{
    public class CreateClientConsumer : IConsumer<CreateClientCommand>
    {
        MVCAppContext _DBcontext;
        public CreateClientConsumer(MVCAppContext DBcontext)
        {
            _DBcontext = DBcontext;
        }
        public async Task Consume(ConsumeContext<CreateClientCommand> context)
        {
            await _DBcontext.clients.AddAsync(context.Message.Client);

            if (_DBcontext.SaveChanges() == 1)
                await context.RespondAsync(new CreateClientResult { Succeeded = true, Text = "Item was created" });
            else
                await context.RespondAsync(new CreateClientResult { Succeeded = false, Text = "Item not created" });
        }
    }
    public class CreateClientCommand
    {
        public Client Client { get; set; }
    }
    public class CreateClientResult
    {
        public bool Succeeded { get; set; }
        public string Text { get; set; }
    }
}
