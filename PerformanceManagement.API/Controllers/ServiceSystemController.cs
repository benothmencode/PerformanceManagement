using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.DATA.Repositories.SystemRepository;

namespace PerformanceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceSystemController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISystemRepository systemRepository;

    }
}