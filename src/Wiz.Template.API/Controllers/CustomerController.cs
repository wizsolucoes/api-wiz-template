using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.API.Handler;
using Wiz.Template.API.Services.Interfaces;
using Wiz.Template.API.ViewModels.Customer;

namespace Wiz.Template.API.Controllers
{
    [Authorize(AuthenticationSchemes = TokenAuthenticationOptions.Bearer)]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiController]
    [Route("api/v{version:apiVersion}/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Lista de clientes.
        /// </summary>
        /// <returns>Clientes.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerAddressViewModel>>> List()
        {
            return Ok(await _customerService.GetAllAsync());
        }

        /// <summary>
        /// Cliente por Id.
        /// </summary>
        /// <param name="customer">Parâmetro "id" do cliente.</param>
        /// <returns>Cliente.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerAddressViewModel>> Get([FromQuery]CustomerIdViewModel customer)
        {
            var customerVM = await _customerService.GetAddressByIdAsync(customer);

            if (customerVM == null)
            {
                return NotFound();
            }

            return Ok(customerVM);
        }

        /// <summary>
        /// Cliente por nome.
        /// </summary>
        /// <param name="customer">Parâmetro "nome" do cliente.</param>
        /// <returns>Cliente.</returns>
        [HttpGet("name/{name}")]
        public async Task<ActionResult<CustomerAddressViewModel>> Get([FromQuery]CustomerNameViewModel customer)
        {
            var customerVM = await _customerService.GetAddressByNameAsync(customer);

            if (customerVM == null)
            {
                return NotFound();
            }

            return Ok(customerVM);
        }

        /// <summary>
        /// Criação de cliente.
        /// </summary>
        /// <param name="customer">Parâmetro "cliente".</param>
        /// <returns>Cliente criado.</returns>
        [HttpPost]
        public ActionResult<CustomerViewModel> Post([FromBody]CustomerViewModel customer)
        {
            if (customer == null)
            {
                return NotFound();
            }

            return Created(nameof(Get), _customerService.Add(customer));
        }

        /// <summary>
        /// Atualização de cliente.
        /// </summary>
        /// <param name="id">Parâmetro "id" do cliente.</param>
        /// <param name="customer">Parâmetro "cliente".</param>
        /// <returns>Cliente atualizado.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]CustomerViewModel customer)
        {
            if (customer == null || customer.Id != id)
            {
                return BadRequest();
            }

            var customerVM = await _customerService.GetByIdAsync(new CustomerIdViewModel(id));

            if (customerVM == null)
            {
                return NotFound();
            }

            _customerService.Update(customer);

            return NoContent();
        }

        /// <summary>
        /// Exclusão de cliente.
        /// </summary>
        /// <param name="id">Parâmetro "id" do cliente.</param>
        /// <returns>Cliente excluido.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromQuery]CustomerIdViewModel customer)
        {
            var customerVM = await _customerService.GetByIdAsync(customer);

            if (customerVM == null)
            {
                return NotFound();
            }

            _customerService.Remove(customerVM);

            return NoContent();
        }
    }
}