using Refit;

namespace Wiz.Template.Application.Clients.OpenFinanceBb;

public interface IOpenRatesService
{
    /// <summary>
    /// Gets the online rates.
    /// </summary>
    /// <returns>Rate List</returns>
    [Get("/open-banking/opendata-exchange/v1/online-rates")]
    public Task<RateResponse> GetOnlineRates();
}
