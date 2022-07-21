namespace DexcomChallenge.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DexcomChallenge.Services.Contracts;
    using Microsoft.Extensions.Logging;
    using static DexcomChallenge.Services.Clients.EstimatedGlucoseClient;

    internal static class Extensions
    {
        internal static IOperationResult<TResultType> HandleAndLogException<TResultType>(this Exception exception , string message, ILogger logger )
        {
            logger.LogError(exception, message);
            return new OperationFailureResult<TResultType>(message);
        }
    }
}
