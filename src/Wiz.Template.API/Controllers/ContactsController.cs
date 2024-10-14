﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wiz.Template.API.ViewModels.Customer;
using Wizco.Common.Base;
using Wizco.Common.Web;

namespace Wiz.Template.API.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/v1/contacts")]
public class ContactsController : WizcoControllerBase<ContactsController>
{
    public ContactsController(IServiceContext serviceContext, ILogger<ContactsController> logger, IMediator mediator) : base(serviceContext, logger, mediator)
    {
    }

    /// <summary>
    /// Lista de clientes.
    /// </summary>
    /// <remarks>
    /// Retorna uma lista com todos os clientes
    /// </remarks>
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
    /// <remarks>
    /// Retorna cliente pelo Id
    /// </remarks>
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
    /// <remarks>
    /// Retorna cliente pelo nome
    /// </remarks>
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
    /// <remarks>
    /// Cria um novo cliente
    /// </remarks>
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
    public async Task<ActionResult<CustomerViewModel>> PostCustomerAsync([FromBody] CustomerViewModel customer)
    {
        if (customer == null || string.IsNullOrWhiteSpace(customer.Name))
        {
            //teste
            return NoContent();
        }

        return Created(nameof(GetByName), await _customerService.AddAsync(customer).ConfigureAwait(false));
    }

    /// <summary>
    /// Atualização de cliente.
    /// </summary>
    /// <remarks>
    /// Atualiza um cliente
    /// </remarks>
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

        await _customerService.UpdateAsync(customer).ConfigureAwait(false);

        return Accepted();
    }

    /// <summary>
    /// Exclusão de cliente.
    /// </summary>
    /// <remarks>
    /// Exclui um cliente
    /// </remarks>
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

        await _customerService.RemoveAsync(customerVM).ConfigureAwait(false);

        return Accepted();
    }
}