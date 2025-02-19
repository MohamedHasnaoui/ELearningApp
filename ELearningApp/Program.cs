using CloudinaryDotNet;
using ELearningApp.Components;
using ELearningApp.Components.Account;
using ELearningApp.Data;
using ELearningApp.Hubs;
using ELearningApp.IServices;
using ELearningApp.Model;
using ELearningApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

async Task SeedDataAsync(IServiceProvider serviceProvider)
{
    // Get the RoleManager and UserManager services
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // Définissez les rôles requis
    string[] roleNames = { "Etudiant", "Enseignant" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Check if the admin role exists
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Check if the admin user exists
    var adminUser = await userManager.FindByEmailAsync("admin@gmail.com");
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = "Admin",
            FormalUserName = "Admin",
            Email = "admin@gmail.com",
            Bio = "I'm the Admin",
            PhoneNumber = "0606060606",
            PhoneNumberCode = "212",
            Adress = "Morroco, Meknes",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(adminUser, "Admin1/");
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

    //var enseignantUser = await userManager.FindByEmailAsync("enseignantmain@gmail.com");
    //if (enseignantUser == null)
    //{
    //    enseignantUser = new Enseignant
    //    {
    //        UserName = "Enseignant",
    //        FormalUserName = "Enseignant",
    //        Email = "enseignantmain@gmail.com",
    //        Bio = "I'm the enseignant",
    //        PhoneNumber = "0606060606",
    //        PhoneNumberCode = "212",
    //        Adress = "Morroco, Meknes",
    //        EmailConfirmed = true
    //    };
    //    await userManager.CreateAsync(enseignantUser, "Enseignant1/");
    //    await userManager.AddToRoleAsync(enseignantUser, "Enseignant");
    //}

    //var etudiantUser = await userManager.FindByEmailAsync("etudiantmain@gmail.com");
    //if (etudiantUser == null)
    //{
    //    etudiantUser = new Etudiant
    //    {
    //        UserName = "Etudiant",
    //        FormalUserName = "Etudiant",
    //        Email = "etudiantmain@gmail.com",
    //        Bio = "I'm the etudiant",
    //        PhoneNumber = "0606060606",
    //        PhoneNumberCode = "212",
    //        Adress = "Morroco, Meknes",
    //        EmailConfirmed = true
    //    };
    //    await userManager.CreateAsync(etudiantUser, "Etudiant1/");
    //    await userManager.AddToRoleAsync(etudiantUser, "Etudiant");
    //}



    
        // Arrays of common Moroccan first names and last names
        string[] firstNames = { "Youssef", "Ilyas", "Mehdi", "Amine", "Karim", "Saad", "Hassan", "Nourddine", "Omar", "Younnes" };
        string[] lastNames = { "El Amrani", "Benjelloun", "Cherkaoui", "Bouazzaoui", "Lahlou", "Zahir", "Idrissi", "Mourad", "Tazi", "Saidi" };

        Random random = new Random();

        // Seed enseignants
        //for (int i = 1; i <= 8; i++)
        //{
        //    string firstName = firstNames[random.Next(firstNames.Length)];
        //    string lastName = lastNames[random.Next(lastNames.Length)];
        //    string email = $"{firstName.ToLower()}.{lastName.ToLower()}@gmail.com";

        //    var enseignant = new Enseignant
        //    {
        //        Email = email,
        //        UserName = email,
        //        FormalUserName = $"{firstName} {lastName}",
        //        EmailConfirmed = true,
        //        PhoneNumber = $"06{random.Next(10000000, 99999999)}", // Random Moroccan phone number
        //        PhoneNumberCode = "212",
        //        Bio = $"I am {firstName} {lastName}, a passionate teacher dedicated to education and learning. I love sharing my knowledge with students and helping them succeed.",
        //        speciality = GetRandomSpeciality(random),
        //        Adress = $"Morocco, {GetRandomMoroccanCity(random)}"
        //    };

        //    if (await userManager.FindByEmailAsync(enseignant.Email) == null)
        //    {
        //        await userManager.CreateAsync(enseignant, "Enseignant1/");
        //        await userManager.AddToRoleAsync(enseignant, "Enseignant");
        //    }
        //}

        // Seed etudiants
        //for (int i = 1; i <= 8; i++)
        //{
        //    string firstName = firstNames[random.Next(firstNames.Length)];
        //    string lastName = lastNames[random.Next(lastNames.Length)];
        //    string email = $"{firstName.ToLower()}.{lastName.ToLower()}@gmail.com";

        //    var etudiant = new Etudiant
        //    {
        //        Email = email,
        //        UserName = email,
        //        FormalUserName = $"{firstName} {lastName}",
        //        EmailConfirmed = true,
        //        PhoneNumber = $"06{random.Next(10000000, 99999999)}",
        //        PhoneNumberCode = "212",
        //        Bio = $"I am {firstName} {lastName}, a student passionate about learning and discovery. My goal is to succeed in my studies and contribute to the development of my country.",
        //        Adress = $"Morocco, {GetRandomMoroccanCity(random)}"
        //    };

        //    if (await userManager.FindByEmailAsync(etudiant.Email) == null)
        //    {
        //        await userManager.CreateAsync(etudiant, "Etudiant1/");
        //        await userManager.AddToRoleAsync(etudiant, "Etudiant");
        //    }
        //}

        // Helper function to get a random Moroccan city
        string GetRandomMoroccanCity(Random random)
        {
            string[] cities = { "Casablanca", "Rabat", "Fes", "Marrakech", "Tangier", "Agadir", "Meknes", "Oujda", "Kenitra", "Tetouan" };
            return cities[random.Next(cities.Length)];
        }

        // Helper function to get a random speciality
        string GetRandomSpeciality(Random random)
        {
            string[] specialities = { "Computer Science", "Mathematics", "Physics", "Biology", "Economics", "English", "History", "Chemistry" };
            return specialities[random.Next(specialities.Length)];
        }

    }
async Task SeedAbonnementsAsync(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Vérifier si les abonnements existent déjà
    if (!dbContext.Abonnements.Any())
    {
        // Ajouter les abonnements sans spécifier l'ID
        dbContext.Abonnements.AddRange(
            new Abonnement
            {
                Type = TypeAbonnement.Basique,
                Duree = DureeAbonnement.an,
                Prix = 199,
                IsRecommanded = false,
                Description = "Plan parfait pour les étudiants",
                Caracteristiques = "Vidéo d'introduction au cours, Quiz interactifs, Programme du cours, Assistance communautaire, Certificat de fin de formation, Leçon d'exemple"
            },
            new Abonnement
            {
                Type = TypeAbonnement.Standard,
                Duree = DureeAbonnement.an,
                Prix = 299,
                IsRecommanded = true,
                Description = "Pour les utilisateurs qui souhaitent en faire plus",
                Caracteristiques = "Vidéo d'introduction au cours, Quiz interactifs, Programme du cours, Assistance communautaire, Certificat de fin de formation, Leçon d'exemple, Accès à la communauté du cours"
            },
            new Abonnement
            {
                Type = TypeAbonnement.Premium,
                Duree = DureeAbonnement.an,
                Prix = 499,
                IsRecommanded = false,
                Description = "Tous vos amis au même endroit",
                Caracteristiques = "Vidéo d'introduction au cours, Quiz interactifs, Programme du cours, Assistance communautaire, Certificat de fin de formation, Leçon d'exemple, Accès à la communauté du cours"
            }
        );

        await dbContext.SaveChangesAsync();
    }
}


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/courses/etudiant/decouvrir";
    options.AccessDeniedPath = "/courses/etudiant/decouvrir";
});
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//StripeService
builder.Services.AddSingleton(new StripeService("sk_test_51Qc2SdKXk8GKN0SWJzl1muovelWngKG3y8YEnoBtOXyE71skkpIvXRzyszipUKBRuOxEHD2MvZA1g0vVKReD5Mik00DJLjMFcS"));


builder.Services.AddCascadingAuthenticationState();
builder.Services.AddTransient<IdentityUserAccessor>();
builder.Services.AddTransient<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

builder.Services.AddResponseCompression(options =>
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] {"application/octet-stream"})
    );
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString),
    ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString),
    ServiceLifetime.Transient);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();


builder.Services.AddTransient<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// cloudinary Account
var cloudinaryAccount = new Account(
    builder.Configuration.GetValue<string>("Cloudinary:CloudName"),
    builder.Configuration.GetValue<string>("Cloudinary:ApiKey"),
    builder.Configuration.GetValue<string>("Cloudinary:ApiSecret")
);
builder.Services.AddTransient<Cloudinary>(serviceProvider =>
{
    return new Cloudinary(cloudinaryAccount);
});
// cloudinary service
builder.Services.AddTransient<ICloudinaryService, CloudinaryService>();
//other services
builder.Services.AddTransient<IVideoService, VideoService>();
builder.Services.AddTransient<ICategoryCoursService, CategoryCoursService>();
builder.Services.AddTransient<ICertificatService, CertificatService>();
builder.Services.AddTransient<IChoixService, ChoixService>();
builder.Services.AddTransient<ICommentaireVideoService, CommentaireVideoService>();
builder.Services.AddTransient<ICoursCommenceService, CoursCommenceService>();
builder.Services.AddTransient<ICoursService, CoursService>();
builder.Services.AddTransient<IExamenService, ExamenService>();
builder.Services.AddTransient<IQuestionService, QuestionService>();
builder.Services.AddTransient<IReponseCommentaireService, ReponseCommentaireService>();
builder.Services.AddTransient<ISectionService, SectionService>();
builder.Services.AddTransient<ISoumissionService, SoumissionService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<IEnseignantService, EnseignantService>();
builder.Services.AddScoped<IEtudiantService, EtudiantService>();
builder.Services.AddScoped<IMentorFollowService, MentorFollowService>();
builder.Services.AddScoped<IMentorRatingService, MentorRatingService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddTransient<IRatingService, RatingService>();
builder.Services.AddSingleton<IMessageHub, MessageHub>();
builder.Services.AddScoped<IEventService, EventService>();


//AbonnementService
builder.Services.AddHttpClient();
builder.Services.AddTransient<IAbonnementService, AbonnementService>();
builder.Services.AddTransient<IPayment, PaymentS>();
builder.Services.AddTransient<IAbonnementAchete, AbonnementAcheteService>();
builder.Services.AddTransient<IAbonnementTempService, AbonnementTempService>();

/*builder.Services.AddHttpClient<IPayment, PaymentS>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7134/"); // Remplacez par l'URL correcte de votre backend
});
*/
builder.Services.AddSignalR().AddHubOptions<MessageHub>(options =>
{
    options.EnableDetailedErrors = true;
});
builder.Services.AddSignalR(options =>
{
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
}).AddMessagePackProtocol();

var app = builder.Build();
app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();
//app.MapHub<MessageHub>("/messageHub");


// Map SignalR hub
app.MapHub<MessageHub>("/messageHub");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedDataAsync(services);
    await SeedAbonnementsAsync(services);
}

app.Run();
