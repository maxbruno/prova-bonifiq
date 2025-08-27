using Microsoft.AspNetCore.Mvc;
using ProvaPub.Domain.Entities;
using ProvaPub.Application.Services;

namespace ProvaPub.Controllers
{

    /// <summary>
    /// Esse teste simula um pagamento de uma compra.
    /// O m�todo PayOrder aceita diversas formas de pagamento. Dentro desse m�todo � feita uma estrutura de diversos "if" para cada um deles.
    /// Sabemos, no entanto, que esse formato n�o � adequado, em especial para futuras inclus�es de formas de pagamento.
    /// Como voc� reestruturaria o m�todo PayOrder para que ele ficasse mais aderente com as boas pr�ticas de arquitetura de sistemas?
    /// 
    /// Outra parte importante � em rela��o � data (OrderDate) do objeto Order. Ela deve ser salva no banco como UTC mas deve retornar para o cliente no fuso hor�rio do Brasil. 
    /// Demonstre como voc� faria isso.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Parte3Controller : ControllerBase
    {
        private readonly OrderService _orderService;

        public Parte3Controller(OrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpGet("orders")]
        public async Task<Order> PlaceOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            return await _orderService.PayOrder(paymentMethod, paymentValue, customerId);
        }
    }
}
