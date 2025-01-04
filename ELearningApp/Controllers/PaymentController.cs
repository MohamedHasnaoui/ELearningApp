using ELearningApp.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ELearningApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayment _paymentService;

        public PaymentController(IPayment paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("checkout")]
        public ActionResult<string> CreateCheckoutSession([FromBody] PaymentRequest paymentRequest)
        {
            if (paymentRequest.Id <= 0 || paymentRequest.Prix <= 0)
            {
                return BadRequest(new { Message = "Invalid id or price." });
            }

            // Appel de la méthode pour créer la session de paiement
            var url = _paymentService.CreateCheckoutSession(paymentRequest.Id,paymentRequest.plan ,paymentRequest.Prix);

            if (string.IsNullOrEmpty(url))
            {
                return StatusCode(500, new { Message = "Failed to create the checkout session." });
            }

            // Retourne l'URL pour rediriger l'utilisateur vers Stripe
            return Ok(new { url });
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook([FromBody] PaymentSuccessRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.IdEtudiant) || request.IdAbonnement <= 0)
                {
                    return BadRequest(new { Message = "Invalid data." });
                }

                // Simuler le traitement d'un paiement réussi
                var success = await _paymentService.HandlePaymentSuccessAsync(request.IdEtudiant, request.IdAbonnement);

                if (success)
                {
                    return Ok(new { Message = "Payment processed successfully." });
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                // Capturer l'exception et renvoyer le message d'erreur complet
                return StatusCode(500, new { Message = "An error occurred while processing the payment.", Error = ex.Message, StackTrace = ex.StackTrace });
            }
        }

    }

    public class PaymentRequest
    {
        public int Id { get; set; }
        public int Prix { get; set; }
        public string plan { get; set; }
       
    }

    public class PaymentSuccessRequest
    {
        public string IdEtudiant { get; set; }
        public int IdAbonnement { get; set; }
    }
}
