using EMSApp.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EMSApp.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseController<AccountController>
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

       

    }
}