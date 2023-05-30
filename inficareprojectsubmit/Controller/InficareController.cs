using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace inficareprojectsubmit.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InficareController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;   
        public InficareController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this._jwtAuthenticationManager = jwtAuthenticationManager;
        }
        public class LoginViewModel
        {
            public string Token { get; set; }
            public int recordStatus { get; set; }   
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserClassModel userModel)
        {
            int resultStatus=200;
            var tokern = _jwtAuthenticationManager.Authenticate(userModel.UserName, userModel.Password);
            if(tokern == null)
            {
                return Unauthorized();  
            }
            else if (tokern == "1")
            {
                tokern = "Authentication failed. Invalid username or password.";
                 resultStatus = 400;
            }

            var viewModel = new LoginViewModel
            {
                Token = tokern,
                recordStatus = resultStatus
            };

            return Ok(viewModel);
        }


        [HttpGet("GetCustomerList")]
        [Authorize]
        public IActionResult GetCustomerList()
        {
            // making custom data 
            var customerList = new List<CustomerModel>
            {
                new CustomerModel { CustomerId = 1, CustomerName = "Bhuwan joshi" },
                new CustomerModel { CustomerId = 2, CustomerName = "Rabina Joshi" },
                // Add more customers detail  as needed
            };

            return Ok(customerList);
        }


        [HttpGet("GetBankList")]
        [Authorize] 
        public IActionResult GetBankList()
        {
            // making custom data 
            var bankList = new List<BankModel>
            {
                new BankModel { BankId = 1, BankName = "Sanima Bank", BankAccount="0111222333444" },
                new BankModel { BankId = 2, BankName = "Global Bank", BankAccount="012340123401234" },
                // Add more banks  detail as needed
            };

            return Ok(bankList);
        }


    }
}
