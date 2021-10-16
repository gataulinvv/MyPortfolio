using MassTransit;
using Microsoft.AspNetCore.Identity;
using Apps.MVCApp.Models;
using Apps.MVCApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Apps.MVCApp.Application.Hendlers.Requests
{
	public class GetRequestByIdConsumer : IConsumer<GetRequestByIdCommand>
	{
		MVCAppContext _DBcontext;

		UserManager<AppUser> _userManager;

		public GetRequestByIdConsumer(MVCAppContext DBcontext, UserManager<AppUser> userManager)
		{

			_DBcontext = DBcontext;
			_userManager = userManager;

		}

		public async Task Consume(ConsumeContext<GetRequestByIdCommand> context)
		{

			try
			{

				int requestId = context.Message.RequestId;

				if (requestId != 0)
				{
				
					var result = _DBcontext.requests.Select(i => new RequestFormViewModel
					{
						Id = i.id,
						date = i.date,
						user_id = i.user.Id,						
						users_list = _DBcontext.Users.ToList(),
						client_id = i.client.id,
						clients_list = _DBcontext.clients.Select(i => new Client
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
						users_list = _DBcontext.Users.ToList(),
						clients_list = _DBcontext.clients.Select(i => new Client
						{
							id = i.id,
							name = i.name
						}).ToList()
					};
					await context.RespondAsync(result);
				}

			}
			catch (Exception ex)
			{

				throw;
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
