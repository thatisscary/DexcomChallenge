using DexcomChallenge.Models.External;
using DexcomChallenge.Services;
using DexcomChallenge.Services.Clients;
using DexcomChallenge.Services.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DexcomChallenge.Pages
{
    public class cb_backModel : PageModel
    {
        private readonly EstimatedGlucoseClient _egClient;
        private readonly GmiCalculator _gmiCalculator;

        public cb_backModel(EstimatedGlucoseClient egClient, GmiCalculator gmiCalculator)
        {
            _egClient = egClient;
            _gmiCalculator = gmiCalculator;
        }

        public async Task OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                IOperationResult<CollectedEstimatedGlucoseValues> clientResult = await _egClient.GetCollectedGlucoseValuesAsync();

                switch (clientResult)
                {
                    case OperationSuccessResult<CollectedEstimatedGlucoseValues> successResult:
                        decimal gmi = _gmiCalculator.CalculateGmi(successResult.OperationResult);
                        ViewData["gmi"] = gmi;
                        ViewData["success"] = true;
                        break;

                    case OperationFailureResult<CollectedEstimatedGlucoseValues> failureResult:
                        ViewData["success"] = false;
                        ViewData["errorMessage"] = string.Join("<br/>",failureResult.Messages);
                        break;
                }
            }
            else
            {
                ViewData["success"]=false;
                ViewData["errorMessage"] = "User not Authenticated";
            }
        }
    }
}