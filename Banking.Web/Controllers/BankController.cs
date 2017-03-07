using System.Threading.Tasks;
using System.Web.Http;
using Banking.Core;
using Banking.Service;
using Banking.Web.Filters;
using Banking.Web.Infra;
using Banking.Web.Models;

namespace Banking.Web.Controllers
{
    [JwtAuthentication]
    public class BankController : ApiController
    {
        public IBankingService BankingService { get; set; }

        public BankController(IBankingService bankingService)
        {
            BankingService = bankingService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAccountInfo(int id)
        {
            var acc = await BankingService.GetAccountInfo(id);
            if (acc == null) return NotFound();
            var vm = Mapper.Map(acc);
            return Ok(vm);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllAccountsInfo()
        {
            var accs = await BankingService.GetAllAccountsInfo();
            if (accs == null) return NotFound();
            var vm = Mapper.Map(accs);
            return Ok(vm);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Transfer(TransferViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var res = await BankingService.Transfer(model.SourceAccountId, model.TargetAccountId, model.Amount);
            return EndOperation(res);
        }

        [HttpPut]
        [Route("api/bank/deposit")]
        public async Task<IHttpActionResult> Deposit(IoTransactionVm model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var res = await BankingService.Deposit(model.Id, model.Amount);
            return EndOperation(res);
        }

        [HttpPut]
        [Route("api/bank/withdraw")]
        public async Task<IHttpActionResult> Withdraw(IoTransactionVm model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var res = await BankingService.Withdraw(model.Id, model.Amount);
            return EndOperation(res);
        }

        private IHttpActionResult EndOperation(OperationDetails details)
        {
            if (details.Successful) return Ok();
            if (details.ErrorOccurred) return BadRequest(details.Error.Message);
            return InternalServerError(details.Exception);
        }
    }
}
