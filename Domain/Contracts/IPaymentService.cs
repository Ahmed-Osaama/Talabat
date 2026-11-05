using Shared.DTOs.BasketModule;

namespace Domain.Contracts
{
    public interface IPaymentService
    {
        Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId);
        Task UpdatePaymentStatusAsync(string json, string signatureHeader);
    }
}
