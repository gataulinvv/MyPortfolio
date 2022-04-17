using MassTransit.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Apps.MVCApp.Application.Hendlers.Clients;
using Apps.MVCApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Apps.MVCApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "manager, accountant")]
    public class ClientsController : Controller
    {
        IMediator _mediator;
        public ClientsController(MVCAppContext context, IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> Get()
        {
            var client = _mediator.CreateRequestClient<GetClientsListCommand>();

            var response = await client.GetResponse<GetClientsListResult>(new GetClientsListCommand());

            var result = new JsonResultModel()
            {
                IsOk = true,
                Data = response.Message.ClientsList,
                ErrMessage = "Данные успешно получены!"
            };

            return new ObjectResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> Get(int id)
        {
            var client = _mediator.CreateRequestClient<GetClientByIdCommand>();

            var response = await client.GetResponse<Client>(new GetClientByIdCommand { ClientId = id });

            return new ObjectResult(response.Message);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Post(Client model)
        {
            var client = _mediator.CreateRequestClient<CreateClientCommand>();
            var response = await client.GetResponse<CreateClientResult>(new CreateClientCommand { Client = model });

            if (response.Message.Succeeded)
                return Ok(model);
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<Client>> Put(Client model)
        {
            var client = _mediator.CreateRequestClient<UpdateClientCommand>();

            var response = await client.GetResponse<UpdateClientResult>(new UpdateClientCommand { Client = model });

            if (response.Message.Succeeded)
                return Ok(model);
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var client = _mediator.CreateRequestClient<DeleteClientCommand>();

            var response = await client.GetResponse<DeleteClientResult>(new DeleteClientCommand { ClientId = id });

            if (response.Message.Succeeded)
                return Ok(response.Message.Text);
            else
                return BadRequest();
        }
    }
}
