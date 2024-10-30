using System;
using System.ComponentModel.DataAnnotations;
using Wizco.Common.Base;

namespace Wiz.Template.Domain.Models;

public class PaymentMethod : IModelContext
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool? Active { get; set; }
}
