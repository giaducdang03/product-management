using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.ViewModels.RequestModels;
using ProductManagement.API.ViewModels.ResponseModels;
using ProductManagement.Service.BussinessModels.AuthenModels;
using ProductManagement.Service.Interfaces;
using ProductManagement.Service.Services;

namespace ProductManagement.API.Controllers
{
    [Route("api/authen")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public AuthenController(IMemberService memberService) 
        {
            _memberService = memberService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(SignUpModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _memberService.RegisterAsync(model);
                    var resp = new ResponseModel
                    {
                        HttpCode = StatusCodes.Status200OK,
                        Message = "Create account successfuly"
                    };
                    return Ok(resp);
                }
                return ValidationProblem(ModelState);

            }
            catch (Exception ex)
            {
                var resp = new ResponseModel
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message.ToString()
                };
                return BadRequest(resp);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginWithEmail(LoginRequestModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _memberService.LoginWithEmailPassword(model.Email, model.Password);
                    if (result.HttpCode == StatusCodes.Status200OK)
                    {
                        return Ok(result);
                    }
                    return Unauthorized(result);
                }
                return ValidationProblem(ModelState);
            }
            catch (Exception ex)
            {
                var resp = new ResponseModel()
                {
                    HttpCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message.ToString()
                };
                return BadRequest(resp);
            }
        }
    }
}
