using MassTransit.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Apps.MVCApp.Application.Hendlers.Users;
using Apps.MVCApp.Models;
using Apps.MVCApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apps.MVCApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "admin")]
	public class UsersController : Controller
	{
		IMediator _mediator;
		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserGridViewModel>>> Get()
		{
			var client = _mediator.CreateRequestClient<GetUsersListCommand>();

			var response = await client.GetResponse<GetUsersListResult>(new GetUsersListCommand());

			var result = new JsonResultModel()
			{
				IsOk = true,
				Data = response.Message.UsersList,
				ErrMessage = "Данные успешно получены!"
			};

			return new ObjectResult(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<AppUser>> Get(string id)
		{
			var client = _mediator.CreateRequestClient<GetUserByIdCommand>();

			var response = await client.GetResponse<AppUser>(new GetUserByIdCommand { UserId = id });

			return new ObjectResult(response.Message);
		}

		[HttpPost]
		public async Task<ActionResult<AppUser>> Post(AppUser model)
		{
			var client = _mediator.CreateRequestClient<CreateUserCommand>();

			var response = await client.GetResponse<CreateUserResult>(new CreateUserCommand { User = model, HttpContext = HttpContext });

			if (response.Message.Succeeded)
				return Ok(response.Message.User);
			else
				return BadRequest();

		}

		[HttpPut]
		public async Task<ActionResult> Put(AppUser model)
		{
			var client = _mediator.CreateRequestClient<UpdateUserCommand>();

			var response = await client.GetResponse<UpdateUserResult>(new UpdateUserCommand { User = model, HttpContext = HttpContext });

			if (response.Message.Succeeded)
				return Ok(model);
			else
				return BadRequest();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(string id)
		{
			var client = _mediator.CreateRequestClient<DeleteUserCommand>();

			var response = await client.GetResponse<DeleteUserResult>(new DeleteUserCommand { UserId = id });

			if (response.Message.Succeeded)
				return Ok(response);
			else
				return BadRequest();
		}
	}
}
