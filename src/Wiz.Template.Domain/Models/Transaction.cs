using System.ComponentModel.DataAnnotations;
using System;
using Wizco.Common.Base;

namespace Wiz.Template.Domain.Models;

public class Transaction : IModelContext
{
    [Key]
    public Guid Id { get; set; }

    public int MerchantId { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; }

    public string ExternalId { get; set; }

    public string PaymentMethodId { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public DateTime CriadoEm { get; set; }

    public DateTime? ChangedAt { get; set; }

    public string TenantId { get; set; }

    public Merchant Merchant { get; set; }
}