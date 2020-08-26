using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WEBAPI_DAPPER.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        private readonly ILogger logger;
        public BaseController(ILogger logger)
        {
            this.logger = logger;
        }
    }
}
