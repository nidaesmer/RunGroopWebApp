using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModel;

namespace RunGroopWebApp.Controllers
{
    public class AccountController : Controller
    {
        //Dependency Injection (bağımlılık enjeksiyonu)
        private readonly ApplicationDbContext _context; //Kullanıcı oluşturma, bulma, güncelleme gibi kullanıcı yönetim işlemlerini sağlar.
        private readonly SignInManager<AppUser> _signInManager; //Oturum açma, kapatma ve oturum durumunu kontrol etme işlemlerini sağlar.
        private readonly UserManager<AppUser> _userManager;  //Veritabanı bağlamı, Entity Framework Core kullanarak veritabanı işlemlerini yönetir.

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user=await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if(user != null)
            {
                //user is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    //password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                }
                //password is incorrect
                TempData["Error"] = "Wrong credentials. Please, try again";
                return View(loginViewModel);
            }
                //user not found
                TempData["Error"] = "Wrong credentials. Please, try again";
                return View(loginViewModel);
        }
    }
}
