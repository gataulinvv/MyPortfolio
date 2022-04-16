using MassTransit;
using Microsoft.AspNetCore.Identity;

using Apps.MVCApp.Models;
using Apps.MVCApp.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Force.Ddd.Pagination;
using Force.Extensions;
using AutoMapper;
using Force.AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Nest;

namespace Apps.MVCApp.Application.Hendlers.Requests
{
   
    public class GetRequestsListConsumer : IConsumer<GetRequestsListCommand>
    {

        MVCAppContext _DBcontext;

        ElasticClient _ElasticClient;
        public GetRequestsListConsumer(MVCAppContext dbContext)
        {

            var settings = new ConnectionSettings(new Uri("http://localhost:9200/")).DefaultIndex("portfolio");
            _ElasticClient = new ElasticClient(settings);
            _DBcontext = dbContext;
        }
        public async Task Consume(ConsumeContext<GetRequestsListCommand> context)
        {
            try
            {
                //_ElasticClient.SearchAsync()
                //var indexResponse = _ElasticClient.IndexDocument(_DBcontext.clients);

                //var requestsList1 = _DBcontext.requests.Include(x => x.user)
                //.ThenInclude(x => x.requests).ToList();

            }
            catch (Exception ex)
            {

                throw;
            }


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
        public IPaging spec { get; set; }
    }

    public class GetReuestsListResult
    {
        public IEnumerable<RequestGridViewModel> RequstsList { get; set; }
    }
}
