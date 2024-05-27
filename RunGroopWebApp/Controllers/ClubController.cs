using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Controllers
{
                  //home
    public class ClubController : Controller
    {
 
        private readonly IClubRepository _clubRepository;

        //private kısmının cıkması ıcın contextte cıkan kutuyo  2.create ile ilerlemeliyiz
        public ClubController(ApplicationDbContext context, IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }
        //buraların amacı veritabanından verileri çekmek için geçerli 
        public async Task<IActionResult> Index()  // controller
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll(); //model
            return View(clubs); //view
        }
        //detail sayfası olusturmak ıcın :
                             //index    //id
        public async Task<IActionResult> Detail(int id)
        {
            Club club =await _clubRepository.GetByIdAsync(id);
            return View(club);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
