using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using PerformanceManagement.ENTITIES;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.API.Models.badgeEntityModel;

namespace PerformanceManagement.API.Controllers
{
    [ApiController]
    [Route("PerformanceManager/[Controller]/[action]")]
    public class BadgeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBadgeRepository _BadgeRepository;

        public BadgeController(IBadgeRepository badgeRepository, IMapper mapper, /*IOptions<AppSettings> appSettings,*/ IConfiguration configuration)
        {
            _BadgeRepository = badgeRepository ??
               throw new ArgumentNullException(nameof(badgeRepository));

            _mapper = mapper ??
               throw new ArgumentNullException(nameof(mapper));

        }


        [ActionName("Create")]
        public IActionResult Create([FromBody]BadgeEntityDto model)
        {
            var badgee = _mapper.Map<Badge>(model);
            try
            {
                _BadgeRepository.Create(badgee);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { msg = e.Message });
            }
        }


        [HttpPut("{BadgeId}")]
        [ActionName("Update")]
        public IActionResult Update(Guid BadgeId, [FromBody]BadgeEntityDto model)
        {
             // map model to entity and set id
            var badge = _mapper.Map<Badge>(model);
            badge.Id = BadgeId;

            try
            {
                // update user 
                _BadgeRepository.Update(badge);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


        [ActionName("Delete")]
        [HttpDelete("{BadgeId}")]
        public IActionResult Delete(Guid BadgeId)
        {
            _BadgeRepository.Delete(BadgeId);
            return Ok();
        }



    }
        


        }
