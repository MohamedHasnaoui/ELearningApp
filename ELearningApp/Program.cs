using CloudinaryDotNet;
using ELearningApp.Components;
using ELearningApp.Components.Account;
using ELearningApp.Data;
using ELearningApp.IServices;
using ELearningApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

async Task InitializeRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // D�finissez les r�les requis
    string[] roleNames = { "Etudiant", "Enseignant" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//StripeService
builder.Services.AddSingleton(new StripeService("sk_test_51Qc2SdKXk8GKN0SWJzl1muovelWngKG3y8YEnoBtOXyE71skkpIvXRzyszipUKBRuOxEHD2MvZA1g0vVKReD5Mik00DJLjMFcS"));


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

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine));




builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// cloudinary Account
var cloudinaryAccount = new Account(
    builder.Configuration.GetValue<string>("Cloudinary:CloudName"),
    builder.Configuration.GetValue<string>("Cloudinary:ApiKey"),
    builder.Configuration.GetValue<string>("Cloudinary:ApiSecret")
);
builder.Services.AddSingleton<Cloudinary>(serviceProvider =>
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


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .DisableAntiforgery()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await InitializeRoles(services);
}


app.Run();
