using CloudinaryDotNet;
using ELearningApp.Components;
using ELearningApp.Components.Account;
using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Model;
using ELearningApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

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

    var enseignantUser = await userManager.FindByEmailAsync("enseignantmain@gmail.com");
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = "Enseignant",
            FormalUserName = "Enseignant",
            Email = "enseignantmain@gmail.com",
            Bio = "I'm the enseignant",
            PhoneNumber = "0606060606",
            PhoneNumberCode = "212",
            Adress = "Morroco, Meknes",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(adminUser, "Enseignant1/");
        await userManager.AddToRoleAsync(adminUser, "Enseignant");
    }

    var etudiantUser = await userManager.FindByEmailAsync("etudiantmain@gmail.com");
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = "Etudiant",
            FormalUserName = "Etudiant",
            Email = "etudiantmain@gmail.com",
            Bio = "I'm the etudiant",
            PhoneNumber = "0606060606",
            PhoneNumberCode = "212",
            Adress = "Morroco, Meknes",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(adminUser, "Etudiant1/");
        await userManager.AddToRoleAsync(adminUser, "Etudiant");
    }



    // Seed 15 enseignants
    for (int i = 1; i <= 15; i++)
    {
        var enseignant = new Enseignant
        {
            Email = $"enseignant{i}@example.com",
            UserName = $"enseignant{i}",
            FormalUserName = $"enseignant{i}",
            EmailConfirmed = true,
            PhoneNumber = $"060000000{i}",
            PhoneNumberCode = "212",
            Bio = "I'm a Product Designer based in Melbourne, Australia. I specialise in UX/UI design, brand strategy, and Webflow development. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
            speciality = $"Speciality {i}",
            Adress = "Morroco, Meknes"
        };

        if (await userManager.FindByEmailAsync(enseignant.Email) == null)
        {
            await userManager.CreateAsync(enseignant, "Password@123");
            await userManager.AddToRoleAsync(enseignant, "Enseignant");
        }
    }
    

    // Seed 15 etudiants
    for (int i = 1; i <= 15; i++)
    {
        var etudiant = new Etudiant
        {
            Email = $"etudiant{i}@example.com",
            UserName = $"etudiant{i}",
            FormalUserName = $"etudiant{i}",
            EmailConfirmed = true,
            PhoneNumber = $"060000000{i}",
            Adress = "Morroco, Meknes",
            PhoneNumberCode = "212",
            Bio = "Hi! I’m Alex, a 17-year-old student at Green Valley High. I love coding, basketball, and sci-fi books. My dream is to become a software engineer and create tech that makes a difference!",

        };

        if (await userManager.FindByEmailAsync(etudiant.Email) == null)
        {
            await userManager.CreateAsync(etudiant, "Password@123");
            await userManager.AddToRoleAsync(etudiant, "Etudiant");
        }

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

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString),
    ServiceLifetime.Transient);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
//           .EnableSensitiveDataLogging()
//           .LogTo(Console.WriteLine));




builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

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
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<ICategoryCoursService, CategoryCoursService>();
builder.Services.AddScoped<ICertificatService, CertificatService>();
builder.Services.AddScoped<IChoixService, ChoixService>();
builder.Services.AddScoped<ICommentaireVideoService, CommentaireVideoService>();
builder.Services.AddScoped<ICoursCommenceService, CoursCommenceService>();
builder.Services.AddScoped<ICoursService, CoursService>();
builder.Services.AddScoped<IExamenService, ExamenService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IReponseCommentaireService, ReponseCommentaireService>();
builder.Services.AddScoped<ISectionService, SectionService>();
builder.Services.AddScoped<ISoumissionService, SoumissionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IEtudiantService, EtudiantService>();
builder.Services.AddScoped<IEnseignantService, EnseignantService>();


//AbonnementService
builder.Services.AddHttpClient();
builder.Services.AddScoped<IAbonnementService, AbonnementService>();
builder.Services.AddScoped<IPayment, PaymentS>();
builder.Services.AddScoped<IAbonnementAchete, AbonnementAcheteService>();
builder.Services.AddScoped<IAbonnementTempService, AbonnementTempService>();

/*builder.Services.AddHttpClient<IPayment, PaymentS>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7134/"); // Remplacez par l'URL correcte de votre backend
});
*/

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
app.UseAuthorization();
app.UseAntiforgery();


app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var services = scope.ServiceProvider;
    await SeedDataAsync(services);
}


app.Run();

