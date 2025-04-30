using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using beMahou.Models;
using Microsoft.AspNetCore.Hosting;

namespace beMahou.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<UsuarioMahou> _signInManager;
        private readonly UserManager<UsuarioMahou> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IWebHostEnvironment _environment;

        public RegisterModel(
            UserManager<UsuarioMahou> userManager,
            SignInManager<UsuarioMahou> signInManager,
            ILogger<RegisterModel> logger,
            IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _environment = environment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "El nombre es obligatorio")]
            [Display(Name = "Nombre completo")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "El email es obligatorio")]
            [EmailAddress(ErrorMessage = "Debe ser un email válido")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "La contraseña es obligatoria")]
            [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar contraseña")]
            [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Avatar (opcional)")]
            public IFormFile AvatarFile { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            
            if (ModelState.IsValid)
            {
                var user = new UsuarioMahou 
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Nombre = Input.Nombre,
                    EstrellasAcumuladas = 0 // Inicializar estrellas en 0
                };

                if (Input.AvatarFile != null && Input.AvatarFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "avatars");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Input.AvatarFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Input.AvatarFile.CopyToAsync(stream);
                    }

                    user.AvatarPath = "/avatars/" + fileName;
                }

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Usuario creado exitosamente.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}