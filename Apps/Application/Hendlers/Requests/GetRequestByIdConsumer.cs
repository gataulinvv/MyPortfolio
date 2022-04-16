using MassTransit;
using Microsoft.AspNetCore.Identity;
using Apps.MVCApp.Models;
using Apps.MVCApp.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.MVCApp.Application.Hendlers.Requests
{
    public class GetRequestByIdConsumer : IConsumer<GetRequestByIdCommand>
    {
        MVCAppContext _DBcontext;

        public GetRequestByIdConsumer(MVCAppContext DBcontext)
        {
            _DBcontext = DBcontext;
        }
        public async Task Consume(ConsumeContext<GetRequestByIdCommand> context)
        {
            int requestId = context.Message.RequestId;

            if (requestId != 0)
            {
                var result = _DBcontext.requests.Select(i => new RequestFormViewModel
                {
                    Id = i.id,
                    date = i.date,
                    userid = i.user.Id,
                    userslist = _DBcontext.Users.ToList(),
                    clientid = i.client.id,
                    clientslist = _DBcontext.clients.Select(i => new Client
                    {
                        id = i.id,
                        name = i.name
                    }).ToList()

                }).FirstOrDefault(x => x.Id == requestId);

                if (result == null)
                    await context.RespondAsync(new GetRequestByIdResult { Text = "Item is not found" });
                else
                    await context.RespondAsync(result);
            }
            else
            {
                var result = new RequestFormViewModel
                {
                    userslist = _DBcontext.Users.ToList(),
                    clientslist = _DBcontext.clients.Select(i => new Client
                    {
                        id = i.id,
                        name = i.name
                    }).ToList()
                };
                await context.RespondAsync(result);
            }
        }
    }
    public class GetRequestByIdCommand
    {
        public int RequestId { get; set; }
    }
    public class GetRequestByIdResult
    {
        public string Text { get; set; }
    }
}
