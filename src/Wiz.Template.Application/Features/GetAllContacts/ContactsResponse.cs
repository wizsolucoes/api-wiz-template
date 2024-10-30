using Wizco.Common.Application;

namespace Wiz.Template.Application.Features.GetAllContacts;

public class ContactsResponse : Response
{
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
