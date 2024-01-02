﻿using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase {
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
        {
			this._authService = authService;
		}

		[HttpPost("login")]
		public async Task<ActionResult<AuthResponse>> Login(AuthRequest request) {
			return Ok(await _authService.Login(request));
		}

		[HttpPost("register")]
		public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request) {
			return Ok(await _authService.Register(request));
		}
    }
}
