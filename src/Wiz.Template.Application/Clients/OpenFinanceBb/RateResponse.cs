namespace Wiz.Template.Application.Clients.OpenFinanceBb;

public class RateResponse
{
    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public Datum[] Data { get; set; }

    /// <summary>
    /// Gets or sets the links.
    /// </summary>
    /// <value>
    /// The links.
    /// </value>
    public Links Links { get; set; }

    /// <summary>
    /// Gets or sets the meta.
    /// </summary>
    /// <value>
    /// The meta.
    /// </value>
    public Meta Meta { get; set; }
}

public class Links
{
    /// <summary>
    /// Gets the self.
    /// </summary>
    /// <value>
    /// The self.
    /// </value>
    public string Self { get; set; }

    public string First { get; set; }
    
    public string Next { get; set; }
    
    public string Last { get; set; }
}

public class Meta
{
    /// <summary>
    /// Gets or sets the total records.
    /// </summary>
    /// <value>
    /// The total records.
    /// </value>
    public int TotalRecords { get; set; }

    /// <summary>
    /// Gets or sets the total pages.
    /// </summary>
    /// <value>
    /// The total pages.
    /// </value>
    public int TotalPages { get; set; }
}

public class Datum
{
    public Participant Participant { get; set; }
    
    public string ForeignCurrency { get; set; }
    
    public string DeliveryForeignCurrency { get; set; }
    
    public string TransactionType { get; set; }
    
    public string TransactionCategory { get; set; }
    
    public string TargetAudience { get; set; }
    
    public string Value { get; set; }
    
    public DateTime ValueUpdateDateTime { get; set; }
    
    public string Disclaimer { get; set; }
}

public class Participant
{
    public string Brand { get; set; }
    
    public string Name { get; set; }
    
    public string CnpjNumber { get; set; }
}
