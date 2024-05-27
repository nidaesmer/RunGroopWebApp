using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Controllers
{
                  //home
    public class ClubController : Controller
    {
        private readonly ApplicationDbContext _context;

        //private kısmının cıkması ıcın contextte cıkan kutuyo  2.create ile ilerlemeliyiz
        public ClubController(ApplicationDbContext context)
        {
            _context = context;
        }
        //buraların amacı veritabanından verileri çekmek için geçerli 
        public IActionResult Index()  // controller
        {
            List<Club> clubs = _context.Clubs.ToList(); //model
            return View(clubs); //view
        }
        //detail sayfası olusturmak ıcın :
                             //index    //id
        public IActionResult Detail(int id)
        {
            Club club = _context.Clubs.Include(a=>a.Address).FirstOrDefault(c => c.Id == id);
            return View(club);
        }
    }
}
