using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizco.Common.Base;

namespace Wiz.Template.Domain.Entities;

public class Contacts : IModelContext
{
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the razao social.
    /// </summary>
    /// <value>
    /// The razao social.
    /// </value>
    public string RazaoSocial { get; set; }

    /// <summary>
    /// Gets or sets the cargo.
    /// </summary>
    /// <value>
    /// The cargo.
    /// </value>
    public string Cargo { get; set; }

    /// <summary>
    /// Gets or sets the codigo fip.
    /// </summary>
    /// <value>
    /// The codigo fip.
    /// </value>
    public int CodigoFip { get; set; }
}
