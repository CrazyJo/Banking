using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Banking.Service;
using Banking.Web.Infra.Jwt;
using Banking.Web.Models;

namespace Banking.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : ApiController
    {
        public ISignInManager SignInManager { get; set; }

        public AccountController(ISignInManager signInManager)
        {
            SignInManager = signInManager;
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IHttpActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authRes = await SignInManager.Authentication(model.Login, model.Password);
            if (authRes.Successful) return Ok(JwtManager.GenerateToken(authRes.Result.Login));
            if (authRes.ErrorOccurred) return BadRequest(authRes.Error.Message);

            return InternalServerError(authRes.Exception);
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            if (!model.Confirmed)
                ModelState.AddModelError("model.Confirmed", "The password was not confirmed.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await SignInManager.Register(model.Login, model.Password);
            if (res.Successful) return Ok();
            if (res.ErrorOccurred) return BadRequest(res.Error.Message);

            return InternalServerError(res.Exception);
        }
    }
}
