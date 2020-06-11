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
using PerformanceManagement.ENTITIES;
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
        private readonly IMapper _mapper;


        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            PerformanceManagementDBContext context,
            ISystemeRepository systemeRepository,
            IMapper mapper,
            IBadgeRepository badgeRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _systemeRepository = systemeRepository;
            _mapper = mapper;
            _badgeRepository = badgeRepository;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [BindProperty(SupportsGet = true)]
        public UserSystemsVm UserSystemsvm { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

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
            [Display(Name = "Identifier")]
            public int? Identifier { get; set; }
          
            [DataType(DataType.Text)]
            [Display(Name = "UrlAccount")]
            public string UrlAccount { get; set; }

        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var Systemes = _systemeRepository.GetSystemes();
            var SystemeModel = _mapper.Map<IList<SystemeEntityDto>>(Systemes);
            var systemeList = new SystemesList(SystemeModel.ToList());
            UserSystemsvm = new UserSystemsVm
            {
                systemes = systemeList.GetSystemesList()
            };
           
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new User { UserName = Input.UserName, Email = Input.Email , FirstName = Input.FirstName , LastName =Input.LastName , Userimage= "avatar04.png" };
                var result = await _userManager.CreateAsync(user, Input.Password);
                
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Employee").Wait();
                    UserSystemsvm.UserId = user.Id;
                    UserSystemsvm.SelectedSystemesID = UserSystemsvm.SelectedSystemesID;
                    if(UserSystemsvm != null)
                    {
                        User userselected = await _userManager.FindByIdAsync(UserSystemsvm.UserId.ToString());
                        var Systemselected = _systemeRepository.GetSystemeById(UserSystemsvm.SelectedSystemesID);
                        var UserSystem = new SystemeUser()
                        {
                            User = userselected,
                            UserId = UserSystemsvm.UserId,
                            Systeme = Systemselected,
                            SystemeId = UserSystemsvm.SelectedSystemesID,
                            Identifier = Input.Identifier,
                            UrlUserSystemAccount = Input.UrlAccount,

                        };
                        if (UserSystem != null)
                        {
                            _context.Add(UserSystem);
                            _context.SaveChanges();
                        }
                    }
                    _logger.LogInformation("User created a new account with password.");

                    IEnumerable<Badge> badges = _badgeRepository.GetAll();
                    foreach(var badge in badges)
                    {
                        var ub = new UserBadge()
                        {
                            Badge = badge,
                            User = user,


                        };
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
