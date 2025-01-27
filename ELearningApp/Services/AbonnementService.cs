using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class AbonnementService : IAbonnementService
{
    private readonly ApplicationDbContext _context;
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigation;

    // Injection d'HttpClient via le constructeur

    public AbonnementService(HttpClient httpClient, ApplicationDbContext context, NavigationManager navigation)
    {
        _navigation = navigation;
        _context = context;
        _httpClient = httpClient;

        // Dynamically set the base address using NavigationManager
        var dynamicBaseUrl = _navigation.BaseUri.TrimEnd('/');
        _httpClient.BaseAddress = new Uri(dynamicBaseUrl); // Définir l'URL de base de votre API

    }
    public async Task<List<Abonnement>> GetAllAbonnementsAsync()
    {
        return await _context.Abonnements.ToListAsync();
    }

    public async Task<Abonnement> GetAbonnementByIdAsync(int id)
    {
        var abonnement = await _context.Abonnements.FindAsync(id);

        if (abonnement == null)
        {
            throw new Exception($"Abonnement with ID {id} not found.");
        }

        return abonnement;
    }

    public async Task CreateAbonnementAsync(Abonnement abonnement)
    {
        if (abonnement == null)
        {
            throw new ArgumentNullException(nameof(abonnement));
        }

        if (string.IsNullOrWhiteSpace(abonnement.Caracteristiques) || abonnement.Prix < 0)
        {
            throw new ArgumentException("Les données d'abonnement sont invalides.");
        }

        _context.Abonnements.Add(abonnement);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAbonnementAsync(Abonnement abonnement)
    {
        _context.Abonnements.Update(abonnement);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAbonnementAsync(int id)
    {
        var abonnement = await _context.Abonnements.FindAsync(id);
        if (abonnement != null)
        {
            _context.Abonnements.Remove(abonnement);
            await _context.SaveChangesAsync();
        }
    }

    // Méthode Checkout corrigée
    public async Task<string> CreateCheckoutSession(int id,string Plan, int prix)
    {
        // Créez un objet contenant les données à envoyer à l'API
        var paymentRequest = new
        {
            Id = id,
            plan=Plan,
            Prix = prix
        };

        // Sérialisez l'objet en JSON
        var content = new StringContent(
            JsonSerializer.Serialize(paymentRequest),
            Encoding.UTF8,
            "application/json"
        );

        // Effectuez la requête POST vers l'API PaymentController
        var response = await _httpClient.PostAsync("api/payment/checkout", content);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(responseBody);
            return result.GetProperty("url").GetString(); // Récupérez l'URL
        }
        else
        {
            throw new Exception("Failed to create checkout session.");
        }
    }


}
