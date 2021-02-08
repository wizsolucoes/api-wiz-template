using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Wiz.Template.API.ViewModels.Customer
{
    public class CustomerIdViewModel
    {
        public CustomerIdViewModel(int id)
        {
            Id = id;
        }

        [FromRoute(Name = "id")]
        [Required(ErrorMessage = "Id é obrigatório")]
        public int Id { get; set; }
    }
}
