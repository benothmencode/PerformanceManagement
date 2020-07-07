using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.DATA.Repositories.SystemeRepository;
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using PerformanceManagement.ENTITIES;
using ProjectF.BadgeJobs;
using ProjectF.Components;
using ProjectF.ModelsDTOS;
using ProjectF.ViewModels;

namespace ProjectF.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Administrator")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly PerformanceManagementDBContext _context;
        private readonly ISystemeRepository _systemeRepository;
        private readonly IBadgeRepository _badgeRepository;
        private readonly IUserBadgeRepository _UserbadgeRepository;
        private readonly IMapper _mapper;
        private readonly IJobService _jobService;
        public Systeme systemG;


        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            PerformanceManagementDBContext context,
            ISystemeRepository systemeRepository,
            IMapper mapper,
            IBadgeRepository badgeRepository, 
            IUserBadgeRepository userBadgeRepository,
            IJobService jobService
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _systemeRepository = systemeRepository;
            _mapper = mapper;
            _badgeRepository = badgeRepository;
            _UserbadgeRepository = userBadgeRepository;
            _jobService = jobService;
            systemG = _systemeRepository.GetGitlab("Gitlab");
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

         

        public class InputModel
        {

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
             
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "UserName")]
            public string UserName { get; set; }
            
           
            [DataType(DataType.Text)]
            [Display(Name = "UrlAccount")]
            public string UrlAccount { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Identifier")]
            public int? Identifier { get; set; }


        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new User { UserName = Input.UserName, FirstName = Input.FirstName , LastName =Input.LastName , Userimage= "avatar04.png", Active=true };
                var result = await _userManager.CreateAsync(user, Input.Password);
                
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Employee").Wait();
                    _logger.LogInformation("User created a new account with password.");

                    if(systemG != null)
                    {
                        var UserSystem = new SystemeUser()
                        {
                            User = user,
                            Systeme = systemG,
                            Identifier = Input.Identifier,
                            UrlUserSystemAccount = Input.UrlAccount,

                        };
                        if (UserSystem != null)
                        {
                            _context.Add(UserSystem);
                            _context.SaveChanges();
                        }
                    }

                    IEnumerable<Badge> badges = _badgeRepository.GetAll();
                    if (badges.Count() != 0)
                    {
                        foreach (var badge in badges)
                        {
                            _UserbadgeRepository.CreateUserBadge(user.Id, badge.Id);
                            if (badge.jobId != null)
                            {
                                _jobService.startJob(badge.jobId);
                            }
                            if (badge.TypeVote != null)
                            {
                                var voteRights = new VoteRights()
                                {
                                    TypeVoteId = (int)badge.TypeVoteId,
                                    Quantity = badge.BadgeCriteria,
                                    Update = DateTime.Now,
                                    UserId = user.Id,
                                };
                            }
                        }
                    }
                    
                    return LocalRedirect(returnUrl);
                    
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

           


            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
