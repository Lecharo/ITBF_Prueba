using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APPLogin.Data;
using APPLogin.Models;
using APPLogin.Repository;
using QualaAPI.Models.Dto;
using APPLogin.Models.Dto;

namespace APPLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _userRepository;
        protected ResponseDto _response;

        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _response = new ResponseDto();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserDto user)
        {
            _logger.LogWarning("Method Register invoked");
            var respuesta = await _userRepository.Register(
                new User
                {
                    UserName = user.UserName,
                }, user.Password
                );

            if (respuesta == -1)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario Ya Existe";
                return BadRequest(_response);
            }

            if (respuesta == -500)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al Crear el usuario";
                return BadRequest(_response);
            }
            _response.DisplayMessage = "Usuario Creado con Exito!";
            _response.Result = respuesta;

            return Ok(_response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserDto user)
        {
            _logger.LogWarning("Method Login invoked");
            var respuesta = await _userRepository.Login(user.UserName, user.Password);
            if (respuesta == "nouser")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "User Don´t Exist";
                return BadRequest(_response);
            }
            if (respuesta == "wrongpassword")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Password Wrong";
                return BadRequest(_response);
            }
            _response.Result = respuesta;
            _response.DisplayMessage = "User Conected";
            return Ok(_response);
        }

    }
}
