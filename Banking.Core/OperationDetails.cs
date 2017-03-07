using System;

namespace Banking.Core
{
    public class OperationDetails
    {
        public CompletionStatus Status { get; private set; }
        public bool Successful => Status == CompletionStatus.Successfully;

        /// <summary>
        /// Did logical errors occur during the operation?
        /// </summary>
        public bool ErrorOccurred => Status == CompletionStatus.HasLogicError;
        public string OperationName { get; }

        /// <summary>
        /// Business logic error
        /// </summary>
        public Error Error { get; set; }
        public Exception Exception { get; protected set; }

        public OperationDetails(string operationName)
        {
            OperationName = operationName;
            Status = CompletionStatus.Successfully;
        }

        public override string ToString()
        {
            return Status.ToString();
        }

        public virtual void SetError(string message)
        {
            if (string.IsNullOrEmpty(message)) return;
            SetError(string.Empty, message);
        }

        public virtual void SetError(string property, string message)
        {
            SetError(new Error(property, message));
        }

        public virtual void SetError(Error error)
        {
            if (error == null) return;
            Status = CompletionStatus.HasLogicError;
            Error = error;
        }

        public virtual void SetException(Exception exception)
        {
            if (exception == null) return;
            Status = CompletionStatus.Failed;
            Exception = exception;
        }
    }
}
