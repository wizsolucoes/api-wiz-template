using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Wizco.Common.Base;

namespace Wiz.Template.Domain.Models;

public class Merchant : IModelContext
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string IdentificationNumber { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [MaxLength(100)]
    public string ChangedAt { get; set; }

    [MaxLength(100)]
    public string PaymentMethodId { get; set; }

    // Relacionamento com Transaction
    public ICollection<Transaction> Transactions { get; set; }
}