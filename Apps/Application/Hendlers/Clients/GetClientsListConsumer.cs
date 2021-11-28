using MassTransit;
using Apps.MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apps.MVCApp.Application.Hendlers.Clients
{
   
    public class GetClientsListConsumer : IConsumer<GetClientsListCommand>
	{
		MVCAppContext _DBcontext;
		/// <summary>
		/// context was changed in branch dev
		/// </summary>
		/// <param name="dbContext"></param>
		public GetClientsListConsumer(MVCAppContext dbContext)
		{
			_DBcontext = dbContext;

		}

		public async Task Consume(ConsumeContext<GetClientsListCommand> context)
		{  
			var clientsList = _DBcontext.clients.OrderBy(i => i.id).ToList();
			await context.RespondAsync(new GetClientsListResult { ClientsList = clientsList });
		}
	}

	public class GetClientsListCommand
	{
	}

	public class GetClientsListResult
	{
		public IEnumerable<Client> ClientsList { get; set; }
	}
}
