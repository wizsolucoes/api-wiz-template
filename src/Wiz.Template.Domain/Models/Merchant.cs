using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Wizco.Common.Base;

namespace Wiz.Template.Domain.Models;

public class Merchant : IModelContext
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public string IdentificationNumber { get; set; }

    public DateTime CreatedAt { get; set; }

    public string ChangedAt { get; set; }

    public ICollection<Transaction> Transactions { get; set; }
}