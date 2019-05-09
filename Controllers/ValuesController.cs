using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ForbidProblemDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IOptions<ApiBehaviorOptions> _options;

        public ValuesController(IOptions<ApiBehaviorOptions> options)
        {
            _options = options;
        }

        [HttpGet]
        public IDictionary<int, ClientErrorData> Get()
        {
            return _options.Value.ClientErrorMapping;
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (id < 0)
            {
                // {"type":"https://tools.ietf.org/html/rfc7231#section-6.5.4","title":"Not Found","status":404,"traceId":"8000095c-0000-fb00-b63f-84710c7967bb"}
                return NotFound();
            }

            if (id > 100)
            {
                // empty response without ProblemDetails
                return Forbid();
            }

            return "value";
        }
    }
}
