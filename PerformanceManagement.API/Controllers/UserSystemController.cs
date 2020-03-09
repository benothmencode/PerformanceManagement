using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.DATA.Repositories;

namespace PerformanceManagement.API.Controllers
{
    [Route("api/user/{UserId}/[controller]/[action]")]
    [ApiController]
    public class UserSystemController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _mapper;
        public UserSystemController(IUserRepository userRepository, IMapper mapper, /*IOptions<AppSettings> appSettings,*/ IConfiguration configuration)
        {
            _UserRepository = userRepository ??
               throw new ArgumentNullException(nameof(userRepository));

            _mapper = mapper ??
               throw new ArgumentNullException(nameof(mapper));

        }
    }
}