using Stripe;
using Stripe.Checkout;

public class StripeService
{
    public StripeService(string secretKey)
    {
        StripeConfiguration.ApiKey = secretKey;
    }

    public Session CreateCheckoutSession(string successUrl, string cancelUrl, List<SessionLineItemOptions> lineItems)
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = successUrl,
            CancelUrl = cancelUrl,
        };

        var service = new SessionService();
        return service.Create(options);
    }
}
