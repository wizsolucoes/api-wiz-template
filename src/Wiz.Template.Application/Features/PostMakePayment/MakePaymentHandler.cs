using Microsoft.Extensions.Logging;
using Wiz.Template.Application.Services;
using Wizco.Common.Application;

namespace Wiz.Template.Application.Features.PostMakePayment
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
            Response.Data = await transactionServices.ToPayAsync(Input);
        }

        protected override async Task ValidateAsync()
        {
            bool existeLojista = await transactionServices.ExistsMerchantAsync(Input.MerchantId);

            if (!existeLojista)
            {
                Response.AddError("Logista Inexistente");
            }

            bool existeMetodoPagamento = await transactionServices.ExistsPaymentMethodAsync(Input.PaymentMethodId);

            if (!existeMetodoPagamento)
            {
                Response.AddError("Não há suporte para este metodo de pagmento");
            }

        }
    }
}
