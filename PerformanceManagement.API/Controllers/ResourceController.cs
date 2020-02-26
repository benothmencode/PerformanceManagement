using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.API.Models.ResourceEntityModel;
using PerformanceManagement.DATA.Repositories.ResourceRepository;
using PerformanceManagement.ENTITIES;

namespace PerformanceManagement.API.Controllers
{
    [Route("api/{SystemId}/[controller]/[action]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IResourceRepository _resourceRepository;

        public ResourceController(IMapper mapper, IResourceRepository resourceRepository)
        {
            _mapper = mapper;
            _resourceRepository = resourceRepository;
        }

        [HttpPost]
        [ActionName("AddResource")]
        public async Task<ActionResult<Resource>> CreateResource(Guid SystemId, [FromBody] ResourceEntityForCreation model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resource = _mapper.Map<Resource>(model);

            try
            {
                // Add resource 
                _resourceRepository.AddResource(SystemId, resource);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

    }
}