using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizco.Common.Base;

namespace Wiz.Template.Domain.Models;

[Table(name: "ses_contatos")]
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
    [Column(name: "pesrazaosocial")]
    public string RazaoSocial { get; set; }

    [Column(name: "dircargo")]
    public string Cargo { get; set; }

    [Column(name: "entcodigofip")]
    public int CodigoFip { get; set; }
}
