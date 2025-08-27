using Microsoft.AspNetCore.Mvc;
using ProvaPub.Domain.Entities;
using ProvaPub.Repository;
using ProvaPub.Application.Interfaces;

namespace ProvaPub.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class Parte2Controller : ControllerBase
    {
        /// <summary>
        /// Corre��es implementadas:
        /// 1 - Corrigido o bug de pagina��o utilizando Skip e Take com c�lculos corretos de p�gina
        /// 2 - Implementada Inje��o de Depend�ncia para ProductService e CustomerService
        /// 3 - Criada estrutura gen�rica PagedList<T> para evitar repeti��o de c�digo entre CustomerList e ProductList
        /// 4 - Criada classe base BaseService<T> para evitar repeti��o entre CustomerService e ProductService
        /// 
        /// </summary>
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;

        public Parte2Controller(IProductService productService, ICustomerService customerService)
        {
            _productService = productService;
            _customerService = customerService;
        }

        [HttpGet("products")]
        public PagedList<Product> ListProducts(int page = 1)
        {
            return _productService.ListProducts(page);
        }

        [HttpGet("customers")]
        public PagedList<Customer> ListCustomers(int page = 1)
        {
            return _customerService.ListCustomers(page);
        }
    }
}
