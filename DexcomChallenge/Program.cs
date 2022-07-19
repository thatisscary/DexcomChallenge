using IdentityModel;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oauth";
})
    .AddCookie("Cookies")
.AddOAuth("oauth", options =>
    {


        options.TokenEndpoint = "https://sandbox-api.dexcom.com/v2/oauth2/token";
        options.AuthorizationEndpoint = "https://sandbox-api.dexcom.com/v2/oauth2/login";

//        options.Authority = "https://sandbox-api.dexcom.com/";
        options.CallbackPath = "/cb_path";
        options.ClientId = "9E0pJ842Z0CROO5FVviU7hS6qce7ZDSS";
        options.ClientSecret = "aGxDtKYlsYum6RUK";
        //options.ResponseType = "code";
        options.SaveTokens = true;
        options.SignInScheme = "Cookies";
        options.SaveTokens = true;
        
 
        options.Scope.Clear();
        options.Scope.Add("offline_access");

 
        
    });

builder.Services.AddRazorPages(options =>
{
    //OpenIdConnectConfiguration
    options.Conventions.AuthorizePage("/Contact");
    options.Conventions.AuthorizeFolder("/Private");
    options.Conventions.AllowAnonymousToPage("/Private/PublicPage");
    options.Conventions.AllowAnonymousToFolder("/Private/PublicPages");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();