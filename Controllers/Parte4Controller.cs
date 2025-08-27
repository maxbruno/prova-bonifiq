using Microsoft.AspNetCore.Mvc;
using ProvaPub.Application.Interfaces;

namespace ProvaPub.Controllers
{
    /// <summary>
    /// O C�digo abaixo faz uma chamada para a regra de neg�cio que valida se um consumidor pode fazer uma compra.
    /// Crie o teste unit�rio para esse Service. Se necess�rio, fa�a as altera��es no c�digo para que seja poss�vel realizar os testes.
    /// Tente criar a maior cobertura poss�vel nos testes.
    /// 
    /// Utilize o framework de testes que desejar. 
    /// Crie o teste na pasta "Tests" da solution
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Parte4Controller : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public Parte4Controller(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        [HttpGet("CanPurchase")]
        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            return await _customerService.CanPurchase(customerId, purchaseValue);
        }
    }
}
