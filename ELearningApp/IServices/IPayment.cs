using Stripe;
using Stripe.Checkout;
namespace ELearningApp.IServices
{
    public interface IPayment
    {
        string CreateCheckoutSession(int id, string plan, int prix, string successUrl, string cancelUrl);
        Task<bool> HandlePaymentSuccessAsync(string idEtudiant, int idAbonnement);
        Task<(decimal totalAmount, decimal lastAmount, DateTime lastPaymentDate)> GetPaymentSummaryAsync();

    }
}
