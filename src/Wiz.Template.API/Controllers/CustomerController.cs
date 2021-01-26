using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.API.Services.Interfaces;
using Wiz.Template.API.ViewModels.Customer;

namespace Wiz.Template.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/v1/customers")]
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
        /// <response code="200">Retorna lista de endereços.</response>
        /// <response code="400">Erro de requisição.</response>
        /// <response code="401">Acesso negado.</response>
        /// <response code="500">Erro interno da API.</response>
        [ProducesResponseType(typeof(IEnumerable<CustomerAddressViewModel>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerAddressViewModel>>> GetAll()
        {
            return Ok(await _customerService.GetAllAsync());
        }

        /// <summary>
        /// Cliente por Id.
        /// </summary>
        /// <param name="customer">Parâmetro "id" do cliente.</param>
        /// <response code="200">Retorna endereço.</response>
        /// <response code="204">Cliente não encontrado.</response>
        /// <response code="400">Erro de requisição.</response>
        /// <response code="401">Acesso negado.</response>
        /// <response code="500">Erro interno da API.</response>
        [ProducesResponseType(typeof(CustomerAddressViewModel), 200)]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerAddressViewModel>> GetById([FromQuery] CustomerIdViewModel customer)
        {
            var customerVM = await _customerService.GetAddressByIdAsync(customer);

            if (customerVM == null)
            {
                return NoContent();
            }

            return Ok(customerVM);
        }


        /// <summary>
        /// Cliente por nome.
        /// </summary>
        /// <param name="customer">Parâmetro "nome" do cliente.</param>
        /// <response code="200">Retorna lista de endereços.</response>
        /// <response code="204">Não encontrado.</response>
        /// <response code="400">Erro de requisição.</response>
        /// <response code="401">Acesso negado.</response>
        /// <response code="500">Erro interno da API.</response>
        [ProducesResponseType(typeof(IEnumerable<CustomerAddressViewModel>), 200)]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        [HttpGet("name/{name}")]
        public async Task<ActionResult<CustomerAddressViewModel>> GetByName([FromQuery] CustomerNameViewModel customer)
        {
            var customerVM = await _customerService.GetAddressByNameAsync(customer);

            if (customerVM == null)
            {
                return NoContent();
            }

            return Ok(customerVM);
        }

        /// <summary>
        /// Criação de cliente.
        /// </summary>
        /// <param name="customer">Parâmetro "cliente".</param>
        /// <response code="201">Registro criado.</response>
        /// <response code="204">Cliente não encontrado.</response>
        /// <response code="400">Erro de requisição.</response>
        /// <response code="401">Acesso negado.</response>
        /// <response code="500">Erro interno da API.</response>
        [ProducesResponseType(typeof(CustomerViewModel), 201)]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        [HttpPost]
        public ActionResult<CustomerViewModel> PostCustomer([FromBody] CustomerViewModel customer)
        {
            if (customer == null)
            {
                return NoContent();
            }

            return Created(nameof(GetByName), _customerService.Add(customer));
        }

        /// <summary>
        /// Atualização de cliente.
        /// </summary>
        /// <param name="id">Parâmetro "id" do cliente.</param>
        /// <param name="customer">Parâmetro "cliente".</param>
        /// <response code="202">Registro criado.</response>
        /// <response code="204">Cliente não encontrado.</response>
        /// <response code="400">Erro de requisição.</response>
        /// <response code="401">Acesso negado.</response>
        /// <response code="500">Erro interno da API.</response>
        [ProducesResponseType(typeof(void), 202)]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCustomer(int id, [FromBody] CustomerViewModel customer)
        {
            if (customer == null || customer.Id != id)
            {
                return BadRequest();
            }

            var customerVM = await _customerService.GetByIdAsync(new CustomerIdViewModel(id));

            if (customerVM == null)
            {
                return NoContent();
            }

            _customerService.Update(customer);

            return Accepted();
        }

        /// <summary>
        /// Exclusão de cliente.
        /// </summary>
        /// <param name="customer">Parâmetro "id" do cliente.</param>
        /// <response code="202">Registro criado.</response>
        /// <response code="204">Cliente não encontrado.</response>
        /// <response code="400">Erro de requisição.</response>
        /// <response code="401">Acesso negado.</response>
        /// <response code="500">Erro interno da API.</response>
        [ProducesResponseType(typeof(void), 202)]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer([FromQuery] CustomerIdViewModel customer)
        {
            var customerVM = await _customerService.GetByIdAsync(customer);

            if (customerVM == null)
            {
                return NoContent();
            }

            _customerService.Remove(customerVM);

            return Accepted();
        }
    }
}