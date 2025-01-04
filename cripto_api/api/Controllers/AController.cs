using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public AController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
