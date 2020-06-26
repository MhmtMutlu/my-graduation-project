using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using bitirme.webui.EmailService;
using bitirme.webui.Extensions;
using bitirme.webui.Identity;
using bitirme.webui.Models;

namespace bitirme.webui.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController: Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Manage()
        {
            return View();
        }

        public IActionResult Login(string ReturnUrl = null)
        {

            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            // var user = await _userManager.FindByNameAsync(model.UserName);
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                // ModelState.AddModelError("", "Bu kullanıcı adı geçerli değil!");
                ModelState.AddModelError("", "Bu email adresi geçerli değil!");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Hesabınızı onaylamanız gerekmektedir!");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false); // kullanıcı, şifresi, tarayıcı kapanınca log out olması, yanlış şifre ile hesap kilitlenmez.

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl??"~/");
            }

            ModelState.AddModelError("","Girilen kullanıcı adı veya şifre yanlış!");

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Generate Token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail","Account", new {
                    userId = user.Id,
                    token = code 
                });
                // Email
                await _emailSender.SendEmailAsync(model.Email,"Hesabınızı onaylayınız.",$"Lütfen email hesabınızı onaylamak için linke <a href='https://localhost:5001{url}'>tıklayınız.</a>");
                return RedirectToAction("Login","Account");
            }

            // if (!model.Email.Contains("@nny.edu.tr"))
            // {
            //     ModelState.AddModelError("Email", "Okul email adresini kullanmalısınız!");
            // }
            ModelState.AddModelError("", "Bilinmeyen bir hata oluştu!");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData.Put("message", new AlertMessage()
            {
                Title = "Oturum Kapatıldı.",
                Message = "Hesabınızdan güvenli bir şekilde çıkış yapıldı.",
                AlertType = "warning"
            });
            return Redirect("~/");
        }
    
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Geçersiz Token!",
                    Message = "Geçersiz bir tokena tıkladınız veya kullandınız!",
                    AlertType = "danger"
                });
                return View();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "Hesabınız Onaylandı!",
                        Message = "Hesabınız başarıyla aktif ettiniz, kullanıma başlayabilirsiniz.",
                        AlertType = "success"
                    });
                    return View();
                }
            }
            TempData.Put("message", new AlertMessage()
            {
                Title = "Hesabınız Onaylanmadı!",
                Message = "Hesabınızı onaylamak için lütfen epostanızı kontrol ediniz.",
                AlertType = "danger"
            });
            return View();
        }
    
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)
            {
                return View();
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = Url.Action("ResetPassword","Account", new {
                userId = user.Id,
                token = code 
            });

            await _emailSender.SendEmailAsync(Email,"Şifre değiştirme.",$"Şifrenizi değiştirmek için linke <a href='https://localhost:5001{url}'>tıklayınız.</a>");

            return View();
        }

        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Home","Index");
            }
            var model = new ResetPasswordModel { Token = token };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("Home", "Index");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}