using MassTransit;
using Apps.MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Apps.MVCApp.Application.Hendlers.Clients
{
	public class UpdateClientConsumer : IConsumer<UpdateClientCommand>
	{
		MVCAppContext _DBcontext;
		public UpdateClientConsumer(MVCAppContext dbContext)
		{
			_DBcontext = dbContext;
		}

		public async Task Consume(ConsumeContext<UpdateClientCommand> context)
		{

            try
            {

				if (!_DBcontext.clients.Any(x => x.id == context.Message.Client.id))
					await context.RespondAsync(new UpdateClientResult { Succeeded = false, Text = "Item is not found!" });


				_DBcontext.Update(context.Message.Client);

				if (_DBcontext.SaveChanges() == 1)
					await context.RespondAsync(new UpdateClientResult { Succeeded = true, Text = "Item is updated" });
				else
					await context.RespondAsync(new UpdateClientResult { Succeeded = false, Text = "Item is not updated" });
			}
            catch (Exception ex)
            {

                throw;
            }

		}
	}

	public class UpdateClientCommand
	{
		public Client Client { get; set; }

	}


	public class UpdateClientResult
	{
		public bool Succeeded { get; set; }
		public string Text { get; set; }
	}
}
