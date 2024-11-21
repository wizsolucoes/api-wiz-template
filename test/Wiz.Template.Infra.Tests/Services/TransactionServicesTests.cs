using FluentAssertions;
using NSubstitute;
using Wiz.Template.Application.Clients.OpenFinanceBb;
using Wiz.Template.Domain.Interfaces.Repository;
using Wiz.Template.Infra.Services;

namespace Wiz.Template.Infra.Tests.Services;

public class TransactionServicesTests
{
    private readonly IMerchantRepository merchantRepository;
    private readonly IPaymentMethodRepository paymentMethodRepository;
    private readonly ITransactionRepository transactionRepository;
    private readonly IOpenRatesService openRatesService;
    private readonly TransactionServices transactionServices;

    public TransactionServicesTests()
    {
        merchantRepository = Substitute.For<IMerchantRepository>();
        paymentMethodRepository = Substitute.For<IPaymentMethodRepository>();
        transactionRepository = Substitute.For<ITransactionRepository>();
        openRatesService = Substitute.For<IOpenRatesService>();
        transactionServices = new TransactionServices(merchantRepository, paymentMethodRepository, transactionRepository, openRatesService);
    }

    [Fact]
    public async Task ExistsMerchantAsync_ShouldReturnTrue_WhenMerchantExists()
    {
        // Arrange
        int merchantId = 1;
        merchantRepository.ExistsByIdAsync(merchantId).Returns(true);

        // Act
        var result = await transactionServices.ExistsMerchantAsync(merchantId);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task ExistsMerchantAsync_ShouldReturnFalse_WhenMerchantDoesNotExist()
    {
        // Arrange
        int merchantId = 1;
        merchantRepository.ExistsByIdAsync(merchantId).Returns(false);

        // Act
        var result = await transactionServices.ExistsMerchantAsync(merchantId);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task ExistsPaymentMethodAsync_ShouldReturnTrue_WhenPaymentMethodExists()
    {
        // Arrange
        string paymentMethodId = "PM01";
        paymentMethodRepository.ExistsByIdAsync(paymentMethodId).Returns(true);

        // Act
        var result = await transactionServices.ExistsPaymentMethodAsync(paymentMethodId);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task ExistsPaymentMethodAsync_ShouldReturnFalse_WhenPaymentMethodDoesNotExist()
    {
        // Arrange
        string paymentMethodId = "PM01";
        paymentMethodRepository.ExistsByIdAsync(paymentMethodId).Returns(false);

        // Act
        var result = await transactionServices.ExistsPaymentMethodAsync(paymentMethodId);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetPaymentsByMerchantAsync_ShouldReturnPayments_WhenTransactionsExist()
    {
        // Arrange
        var transactionId = Guid.NewGuid();
        int merchantId = 1;
        var transactions = new List<Wiz.Template.Domain.Models.Transaction>
        {
            new()
            {
                Id = transactionId,
                MerchantId = merchantId,
                Amount = 100,
                ExternalId = "EXT123",
                CriadoEm = DateTime.UtcNow,
                PaymentMethodId = "PM01",
                Merchant = new Wiz.Template.Domain.Models.Merchant { Name = "Merchant1" },
                PaymentMethod = new Wiz.Template.Domain.Models.PaymentMethod { Name = "Credit Card" }
            }
        };
        transactionRepository.GetPaymentsByMerchantAsync(merchantId).Returns(transactions);

        // Act
        var result = await transactionServices.GetPaymentsByMerchantAsync(merchantId);

        // Assert
        result.Should().HaveCount(1);
        result[0].MerchantName.Should().Be("Merchant1");
        result[0].PaymentMethodName.Should().Be("Credit Card");
    }

    [Fact]
    public async Task GetPaymentsByMerchantAsync_ShouldReturnEmptyList_WhenNoTransactionsExist()
    {
        // Arrange
        int merchantId = 1;
        transactionRepository.GetPaymentsByMerchantAsync(merchantId).Returns(new List<Wiz.Template.Domain.Models.Transaction>());

        // Act
        var result = await transactionServices.GetPaymentsByMerchantAsync(merchantId);

        // Assert
        result.Should().BeEmpty();
    }
}
