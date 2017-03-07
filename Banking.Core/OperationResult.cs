namespace Banking.Core
{
    public class OperationResult<TResult> : OperationDetails
    {
        public TResult Result { get; protected set; }

        public OperationResult(string operationName = "") : base(operationName)
        {
        }

        public virtual void SetResult(TResult result)
        {
            Result = result;
        }
    }
}
