using System;
using System.ComponentModel.DataAnnotations;
using Wizco.Common.Base;

namespace Wiz.Template.Domain.Entities;

public class PaymentMethod : IModelContext
{
    [Key]
    public string Id { get; set; }

    public string Name { get; set; }

    public bool? Active { get; set; }
}
