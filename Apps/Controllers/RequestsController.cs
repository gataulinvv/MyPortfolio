using MassTransit.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Apps.MVCApp.Application.Hendlers.Requests;
using Apps.MVCApp.Models;
using Apps.MVCApp.ViewModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Apps.MVCApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestFormViewModel>> Get(int id)
        {
            var client = _mediator.CreateRequestClient<GetRequestByIdCommand>();

            var response = await client.GetResponse<RequestFormViewModel>(new GetRequestByIdCommand { RequestId = id });

            return new ObjectResult(response.Message);
        }

        [HttpPut]
        public async Task<ActionResult<Request>> Put(Request model, CancellationToken cancellationToken)
        {
            var client = _mediator.CreateRequestClient<UpdateRequestCommand>();

            var response = await client.GetResponse<UpdateRequestResult>(new UpdateRequestCommand { Request = model });

            if (response.Message.Succeeded)
                return Ok(model);
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestGridViewModel>>> Get()
        {
            var client = _mediator.CreateRequestClient<GetRequestsListCommand>();

            var response = await client.GetResponse<GetReuestsListResult>(new GetRequestsListCommand());

            var result = new JsonResultModel()
            {
                IsOk = true,
                Data = response.Message.RequstsList,
                ErrMessage = "Данные успешно получены!"
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Request>> Post(Request model)
        {
            var client = _mediator.CreateRequestClient<CreateRequestCommand>();

            var response = await client.GetResponse<CreateRequestResult>(new CreateRequestCommand { Request = model });

            if (response.Message.Succeeded)
                return Ok(model);
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var client = _mediator.CreateRequestClient<DeleteRequestCommand>();

            var response = await client.GetResponse<DeleteReuestResult>(new DeleteRequestCommand { RequestId = id });

            if (response.Message.Succeeded)
                return Ok(response.Message.Text);
            else
                return BadRequest();
        }
    }
}
