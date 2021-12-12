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
