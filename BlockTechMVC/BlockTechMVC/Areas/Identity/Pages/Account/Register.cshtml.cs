using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BlockTechMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace BlockTechMVC.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use apenas caracteres alfabéticos.")]
            [Required(ErrorMessage = "O campo 'Nome/Razão Social' está vazio.")]
            [Display(Name = "Nome/Razão Social")]
            public string Nome { get; set; }

            [StringLength(14, ErrorMessage = "CNPJ Inexistente.")]
            [Display(Name = "CPF/CNPJ")]
            [Required(ErrorMessage = "O campo 'Documento' está vazio.")]
            public string Documento { get; set; }

            [Required(ErrorMessage = "O campo 'E-mail' está vazio.")]
            [EmailAddress(ErrorMessage = "E-mail inválido.")]
            [Display(Name = "E-mail")]
            [DisplayFormat(DataFormatString = "{0:#####-###}")]
            public string Email { get; set; }

            [StringLength(9, ErrorMessage = "CEP Inválido.")]
            [Required(ErrorMessage = "O campo 'CEP' está vazio.")]
            [Display(Name = "CEP")]
            public string Cep { get; set; }

            [StringLength(2, ErrorMessage = "UF Inválida.")]
            [Required(ErrorMessage = "O campo 'UF' está vazio.")]
            [Display(Name = "UF")]
            public string Uf { get; set; }

            [StringLength(58, ErrorMessage = "Essa cidade não existe.")] //O maior nome de cidade possui 58 caracteres
            [Required(ErrorMessage = "O campo 'Cidade' está vazio.")]
            [Display(Name = "Cidade")]
            public string Cidade { get; set; }

            [Required(ErrorMessage = "O campo 'Rua' está vazio.")]
            [Display(Name = "Rua")]
            public string Rua { get; set; }

            [StringLength(10)]
            [Required(ErrorMessage = "O campo 'Número' está vazio.")]
            [Display(Name = "Número")]
            public string Numero { get; set; }

            [Required(ErrorMessage = "O campo 'Telefone' está vazio.")]
            [StringLength(15, ErrorMessage = "Telefone inválido.")]
            [Display(Name = "Telefone")]
            public string Telefone { get; set; }

            [Required(ErrorMessage = "O campo 'Nome de Usuário' está vazio.")]
            [Display(Name = "Nome de Usuário")]
            public string UserName { get; set; }

            [Required(ErrorMessage="O campo 'Senha' está vazio")]
            [StringLength(12, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Required(ErrorMessage = "O campo 'Confirmar senha' está vazio")]
            [Display(Name = "Confirmar senha")]
            [Compare("Password", ErrorMessage = "A senha e a confirmação não correspondem.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                
                var user = new ApplicationUser { UserName = Input.UserName, Email = Input.Email, Nome = Input.Nome, Documento = Input.Documento, Cep = Input.Cep, Uf = Input.Uf, Cidade = Input.Cidade, Rua = Input.Rua, Numero = Input.Numero, Telefone = Input.Telefone };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("O usuário criou uma nova conta com senha.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirme seu email",
                        $"Por favor, confirme sua conta até <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
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
