namespace DexcomChallenge.Services.Contracts
{
    using System.Collections.Generic;


    public interface IOperationResult
    {
        bool Success { get; }
    }

    public interface IOperationResult<TResultType> : IOperationResult
    {
    }

    public struct OperationSuccessResult<TResultType> : IOperationResult<TResultType>
    {
        public OperationSuccessResult(TResultType resultType)
        {
            OperationResult = resultType;
        }

        public TResultType OperationResult { get; private set; }

        public bool Success => true;
    }

    public struct OperationFailureResult<TResultType> : IOperationResult<TResultType>
    {
        private readonly List<string> _messages;

        public OperationFailureResult()
        {
            _messages = new List<string>();
        }

        public OperationFailureResult(string message) : this()
        {
            _messages.Add(message);
        }

        public bool Success => false;

        public IEnumerable<string> Messages { get { return _messages.ToArray(); } }

        public void AddErrorMessage(string errorMessage)
        {
            if (errorMessage != null)
            {
                _messages.Add(errorMessage);
            }
        }
    }
}
