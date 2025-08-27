using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{
	
	[ApiController]
	[Route("[controller]")]
	public class Parte2Controller :  ControllerBase
	{
		/// <summary>
		/// Correções implementadas:
		/// 1 - Corrigido o bug de paginação utilizando Skip e Take com cálculos corretos de página
		/// 2 - Implementada Injeção de Dependência para ProductService e CustomerService
		/// 3 - Criada estrutura genérica PagedList<T> para evitar repetição de código entre CustomerList e ProductList
		/// 4 - Criada classe base BaseService<T> para evitar repetição entre CustomerService e ProductService
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
