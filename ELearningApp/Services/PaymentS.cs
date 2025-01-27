using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;

namespace ELearningApp.Services
{
    public class PaymentS : IPayment
    {
        private readonly ApplicationDbContext _context;
        private readonly NavigationManager _navigation;
        public PaymentS(ApplicationDbContext context, NavigationManager navigation)
        {
            _navigation = navigation;
            _context = context;
            StripeConfiguration.ApiKey = "sk_test_51Qc2SdKXk8GKN0SWJzl1muovelWngKG3y8YEnoBtOXyE71skkpIvXRzyszipUKBRuOxEHD2MvZA1g0vVKReD5Mik00DJLjMFcS";
        }
        public string CreateCheckoutSession(int id, string plan, int prix, string successUrl, string cancelUrl)
        {
            if (id == 0) return null!;

            var lineItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = prix * 100,
                    Currency = "mad",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Abonnement #" + id.ToString(),
                        Description = $"Abonnement pour Plan {plan}"
                    }
                },
                Quantity = 1
            };

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions> { lineItem },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
            };

            var service = new SessionService();
            var session = service.Create(options);
            return session.Url;
        }





        public async Task<bool> HandlePaymentSuccessAsync(string idEtudiant, int idAbonnement)
        {
            // Vérification des paramètres
            if (string.IsNullOrEmpty(idEtudiant) || idAbonnement <= 0)
                return false;

            try
            {
                // Vérifier si l'abonnement existe dans la table Abonnements
                var abonnement = await _context.Abonnements
                    .FirstOrDefaultAsync(a => a.Id == idAbonnement);

                if (abonnement == null)
                    return false; // L'abonnement n'existe pas

                // Vérifier si l'étudiant existe dans la table Etudiants
                var etudiant = await _context.Etudiants
                    .FirstOrDefaultAsync(e => e.Id == idEtudiant);

                if (etudiant == null)
                    return false; // L'étudiant n'existe pas

                // Vérifier si un abonnement existant satisfait les critères
                var existingAbonnement = await _context.AbonnementsAchetes
                    .FirstOrDefaultAsync(a =>
                        a.IdEtudiant == idEtudiant &&
                        a.IdAbonnement == idAbonnement &&
                        a.DateExpiration > DateTime.Now);

                if (existingAbonnement != null)
                {
                    // L'abonnement existe déjà et est encore valide
                    return false;
                }

                // Calculer la date d'expiration en fonction de la durée de l'abonnement
                DateTime dateExpiration;
                if (abonnement.Duree == DureeAbonnement.moi)
                {
                    dateExpiration = DateTime.Now.AddMonths(1); // Ajouter 1 mois si la durée est "moi"
                }
                else if (abonnement.Duree == DureeAbonnement.an)
                {
                    dateExpiration = DateTime.Now.AddYears(1); // Ajouter 1 an si la durée est "an"
                }
                else
                {
                    // En cas d'erreur (cas non défini pour la durée)
                    return false;
                }

                // Créer un nouvel enregistrement AbonnementAchete
                var abonnementAchete = new AbonnementAchete
                {
                    IdEtudiant = idEtudiant,
                    IdAbonnement = idAbonnement,
                    DateDebutAchat = DateTime.Now,
                    DateExpiration = dateExpiration
                };

                // Ajouter à la base de données
                _context.AbonnementsAchetes.Add(abonnementAchete);
                await _context.SaveChangesAsync(); // Sauvegarde dans la base

                return true;
            }
            catch (Exception ex)
            {
                // Log l'exception si nécessaire
                Console.WriteLine($"Erreur lors du traitement du paiement : {ex.Message}");
                return false;
            }

        }
        public async Task<(decimal totalAmount, decimal lastAmount, DateTime lastPaymentDate)> GetPaymentSummaryAsync()
        {
            try
            {
                // Initialiser le service Stripe
                var paymentIntentService = new PaymentIntentService();

                // Récupérer les paiements (limité ici à 100 pour des raisons de performance)
                var paymentIntents = await paymentIntentService.ListAsync(new PaymentIntentListOptions
                {
                    Limit = 100 // Ajustez selon vos besoins
                });

                if (paymentIntents == null || paymentIntents.Data.Count == 0)
                {
                    // Aucun paiement trouvé
                    return (0, 0, DateTime.MinValue);
                }

                // Calculer la somme totale et trouver le dernier paiement
                decimal totalAmount = 0;
                decimal lastAmount = 0;
                DateTime lastPaymentDate = DateTime.MinValue;

                foreach (var paymentIntent in paymentIntents.Data)
                {
                    if (paymentIntent.Status == "succeeded") // Vérifier si le paiement a réussi
                    {
                        totalAmount += paymentIntent.Amount / 100m; // Montant en unités de base (MAD)
                        if (paymentIntent.Created > lastPaymentDate)
                        {
                            lastPaymentDate = paymentIntent.Created;
                            lastAmount = paymentIntent.Amount / 100m;
                        }
                    }
                }

                return (totalAmount, lastAmount, lastPaymentDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des paiements : {ex.Message}");
                return (0, 0, DateTime.MinValue);
            }
        }



    }
}