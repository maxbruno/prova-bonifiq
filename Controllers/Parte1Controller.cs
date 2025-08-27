using Microsoft.AspNetCore.Mvc;
using ProvaPub.Application.Interfaces;

namespace ProvaPub.Controllers
{
    /// <summary>
    /// Ao rodar o c�digo abaixo o servi�o deveria sempre retornar um n�mero diferente, mas ele fica retornando sempre o mesmo n�mero.
    /// 1 - Fa�a as altera��es para que o retorno seja sempre diferente
    /// 2 - Tome cuidado 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Parte1Controller : ControllerBase
    {
        private readonly IRandomNumberService _randomService;

        public Parte1Controller(IRandomNumberService randomService)
        {
            _randomService = randomService;
        }
        [HttpGet]
        public async Task<int> Index()
        {
            var result = await _randomService.GetUniqueRandomNumberAsync();
            
            return result;
        }
    }
}
