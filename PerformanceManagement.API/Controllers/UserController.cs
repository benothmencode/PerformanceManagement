using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.DATA.Repositories;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using PerformanceManagement.API.Models;
using PerformanceManagement.ENTITIES;
using PerformanceManagement.API.Models.userEntityModel;

namespace PerformanceManagement.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("PerformanceManager/[Controller]/[action]")]
    public class UserController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper, /*IOptions<AppSettings> appSettings,*/ IConfiguration configuration)
        {
            _UserRepository = userRepository ??
               throw new ArgumentNullException(nameof(userRepository));

            _mapper = mapper ??
               throw new ArgumentNullException(nameof(mapper));

        }

        [HttpGet()]
        [ActionName("GetAll")]
        //[Authorize(Roles =Role.SuperAdmin)]
        public IActionResult GetAll()
        {
            var users = _UserRepository.GetAll();
            var model = _mapper.Map<IList<UserEntityDto>>(users);
            return Ok(model);
        }
        
        
       [HttpGet("{UserId:guid}")]
        [ActionName("GetById")]
        //[Authorize(Roles = Role.SuperAdmin)]
        public IActionResult GetById(Guid UserId)
        {
            var user = _UserRepository.GetById(UserId);
            var model = _mapper.Map<UserEntityDto>(user);
            return Ok(model);
        }

        [HttpPut("{UserId}")]
        [ActionName("Update")]
       // [Authorize(Roles =Role.SuperAdmin)]
        public IActionResult Update(Guid UserId, [FromBody]UserEntityDto model)
        {
            // map model to entity and set id
            var user = _mapper.Map<User>(model);
            user.Id = UserId;

            try
            {
                // update user 
                _UserRepository.Update(user);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [ActionName("Delete")]
        [HttpDelete("{UserId}")]
        public IActionResult Delete(Guid UserId)
        {
            _UserRepository.Delete(UserId);
            return Ok();
        }

        [ActionName("Create")]
        public IActionResult Create([FromBody]UserEntityDtoForCreate model ){

            var userr = _mapper.Map<User>(model);
            try
            {
                _UserRepository.Create(userr);
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }


            
        
        }







    }
}