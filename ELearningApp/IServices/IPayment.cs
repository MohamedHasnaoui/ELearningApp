using Stripe.Checkout;
namespace ELearningApp.IServices
{
    public interface IPayment
    {
        string CreateCheckoutSession(int id,string plan,int prix);
        Task<bool> HandlePaymentSuccessAsync(string idEtudiant, int idAbonnement);
    }
}
