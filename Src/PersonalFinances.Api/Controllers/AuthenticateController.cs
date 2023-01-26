using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalFinances.Application.Interfaces;
using PersonalFinances.Application.ViewModel.Request.User;
using PersonalFinances.Domain.Entities;
using PersonalFinances.Domain.Exceptions;
using PersonalFinances.Domain.Enums;

namespace PersonalFinances.Api.Controllers
{
    [ApiController]
    [Route("api/Account")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        
        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<Token>> Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new ArgumentNullException(nameof(login));
            
            if (!ModelState.IsValid)
                return BadRequest(new BusinessException("Invalid Request", nameof(login), ErroEnum.ResourceBadRequest));

            Token token = await _authenticateService.Login(login);

            return Ok(token);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<Token>> Register(CreateUserRequest createUser)
        {
            if (createUser == null)
                throw new ArgumentNullException(nameof(createUser));
            
            if (!ModelState.IsValid)
                return BadRequest(new BusinessException("Invalid Request", nameof(createUser), ErroEnum.ResourceBadRequest));

            await _authenticateService.Register(createUser);

            return Ok();
        }
    }
}