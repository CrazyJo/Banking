using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Banking.Core
{
    public class Operation
    {
        public static OperationDetails Create([CallerMemberName] string operationName = "")
        {
            return new OperationDetails(operationName);
        }

        public static OperationDetails Wrap(Action<OperationDetails> action,
            [CallerMemberName] string operationName = "")
        {
            var res = new OperationDetails(operationName);
            try
            {
                action.Invoke(res);
            }
            catch (Exception e)
            {
                res.SetException(e);
            }
            return res;
        }

        public static async Task<OperationDetails> Wrap(Func<OperationDetails, Task> func, [CallerMemberName] string operationName = "")
        {
            var res = new OperationDetails(operationName);
            try
            {
                await func.Invoke(res);
            }
            catch (Exception e)
            {
                res.SetException(e);
            }
            return res;
        }

        public static async Task<OperationDetails> Wrap(Func<OperationDetails, Task<OperationDetails>> func, [CallerMemberName] string operationName = "")
        {
            OperationDetails res = null;
            try
            {
                res = await func.Invoke(new OperationDetails(operationName));
            }
            catch (Exception e)
            {
                if (res == null)
                {
                    res = new OperationDetails(operationName);
                }
                res.SetException(e);
            }
            return res ?? new OperationDetails(operationName);
        }

        public static async Task<OperationResult<TResult>> Wrap<TResult>(
            Func<OperationResult<TResult>, Task<TResult>> func, [CallerMemberName] string operationName = "")
        {
            var res = new OperationResult<TResult>(operationName);
            try
            {
                res.SetResult(await func.Invoke(res));
            }
            catch (Exception e)
            {
                res.SetException(e);
            }
            return res;
        }
    }
}