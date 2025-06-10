using Refit;

namespace Wiz.Template.Application.Clients.OpenFinanceBb;

/// <summary>
/// Defines a service for retrieving online exchange rates from the Open Banking API.
/// </summary>
public interface IOpenRatesService
{
    /// <summary>
    /// Retrieves the current online rates from the Open Banking API.
    /// </summary>
    /// <remarks>This method sends a GET request to the Open Banking API endpoint to fetch the latest online
    /// rates. The returned data includes rate information relevant to the specified API contract.</remarks>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The result contains a <see
    /// cref="RateResponse"/> object with the online rate details.</returns>
    [Get("/open-banking/opendata-exchange/v1/online-rates")]
    public Task<RateResponse> GetOnlineRates();
}
