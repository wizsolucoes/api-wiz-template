using Microsoft.Extensions.Logging;
using Wiz.Template.Application.Services;
using Wizco.Common.Application;

namespace Wiz.Template.Application.Features.PostPayment
{
    public class MakePaymentHandler : HandlerBase<MakePaymentRequest, MakePaymentResponse>
    {
        private readonly ITransactionServices transactionServices;

        public MakePaymentHandler(ILogger<HandlerBase<MakePaymentRequest, MakePaymentResponse>> logger, ITransactionServices transactionServices) : base(logger)
        {
            this.transactionServices = transactionServices;
        }

        protected override async Task HandleAsync()
        {
            this.Response.Data = await this.transactionServices.ToPayAsync(this.Input);
        }

        protected override async Task ValidateAsync()
        {
            bool existeLojista = await this.transactionServices.ExistsMerchantAsync(this.Input.MerchantId);
            
            if (!existeLojista)
            {
                this.Response.AddError("Logista Inexistente");
            }

            bool existeMetodoPagamento = await this.transactionServices.ExistsPaymentMethodAsync(this.Input.PaymentMethodId);

            if (!existeMetodoPagamento)
            {
                this.Response.AddError("Não há suporte para este metodo de pagmento");
            }

        }
    }
}
