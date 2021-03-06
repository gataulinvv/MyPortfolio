using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apps.MVCApp.Models;
using Apps.MVCApp.ViewModels;
using MassTransit;

namespace Apps.MVCApp.Application.Hendlers.Requests
{
    public class GetRequestsListConsumer : IConsumer<GetRequestsListCommand>
    {
        MVCAppContext _DBcontext;
        public GetRequestsListConsumer(MVCAppContext dbContext)
        {
            _DBcontext = dbContext;
        }
        public async Task Consume(ConsumeContext<GetRequestsListCommand> context)
        {
            var requestsList = _DBcontext.requests.Select(i => new RequestGridViewModel
            {
                Id = i.id,
                Date = i.date,
                UserName = i.user.UserName,
                UserEmail = i.user.Email,
                ClientName = i.client.name,
                RoleNamesList = _DBcontext.Users
                        .Join(_DBcontext.UserRoles, user => user.Id, userRole => userRole.UserId, (user, userRole) => new
                        {
                            UserName = user.UserName,
                            RoleId = userRole.RoleId
                        })
                        .Join(_DBcontext.Roles, firstJoin => firstJoin.RoleId, role => role.Id, (firstJoin, role) => new
                        {
                            UserName = firstJoin.UserName,
                            RoleName = role.Name
                        })
                        .Where(x => x.UserName == i.user.UserName).Select(i => i.RoleName).ToList()

            }).OrderBy(i => i.Id).ToList();

            await context.RespondAsync(new GetReuestsListResult { RequstsList = requestsList });
        }
    }
    public class GetRequestsListCommand
    {
    }
    public class GetReuestsListResult
    {
        public IEnumerable<RequestGridViewModel> RequstsList { get; set; }
    }
}
