using MassTransit;
using Apps.MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.MVCApp.Application.Hendlers.Clients
{
    public class GetClientByIdConsumer : IConsumer<GetClientByIdCommand>
    {
        MVCAppContext _DBcontext;

        public GetClientByIdConsumer(MVCAppContext DBcontext)
        {

            _DBcontext = DBcontext;
        }
        public async Task Consume(ConsumeContext<GetClientByIdCommand> context)
        {

            var client = _DBcontext.clients.FirstOrDefault(i => i.id == context.Message.ClientId);

           
            if (client == null)
                client = new Client { id = 0, name = "", email = "" };


            await context.RespondAsync(client);
        }
    }

    public class GetClientByIdCommand
    {
        public int ClientId { get; set; }
    }

    public class GetClientByIdResult
    {
        public string Text { get; set; }

    }

}
