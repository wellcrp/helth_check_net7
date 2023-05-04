using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HelthCheck.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{clienteId}")]
        public IActionResult GetByClientId(int clientId)
        {
            return Ok(HealthCheckResult.Healthy("Resultado para gerar no dashboard"));            
        }

        [HttpGet("Produtos")]
        public IActionResult GetProducts()
        {
            return Ok(HealthCheckResult.Degraded("Resultado para gerar no dashboard"));
        }

        [HttpGet("cpf/{clientId}")]
        public ActionResult GetForCpfByClientId(int clienteId)
        {
            return Ok(HealthCheckResult.Unhealthy("Resultado para gerar no dashboard"));
        }
    }
}