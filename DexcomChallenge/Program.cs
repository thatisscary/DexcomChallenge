using System.Net.Http.Headers;
using System.Text.Json;
using DexcomChallenge.Services;
using DexcomChallenge.Services.Clients;
using Microsoft.AspNetCore.Authentication.OAuth;

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
        options.ClientId = "C6SOI1Ts7NoYUXoUYoZEu3TNiIU2qYdh";
        options.ClientSecret = "UvXbpwtsC9xpubZB";
        options.CallbackPath = new PathString("/cb_back");
        options.TokenEndpoint = "https://sandbox-api.dexcom.com/v2/oauth2/token";
        options.AuthorizationEndpoint = "https://sandbox-api.dexcom.com/v2/oauth2/login";
        options.SaveTokens = true;
        options.Scope.Clear();
        options.Scope.Add("offline_access");
        options.SignInScheme = "Cookies";
        options.Events = new OAuthEvents
        {
            OnCreatingTicket = async context =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                response.EnsureSuccessStatusCode();
                var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                context.RunClaimActions(json.RootElement);
            }
        };
    });

builder.Services.AddRazorPages(options =>
{
    //OpenIdConnectConfiguration
    options.Conventions.AuthorizePage("/Contact");
});

builder.Services.AddHttpClient<EstimatedGlucoseClient>();
builder.Services.AddTransient<GmiCalculator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();