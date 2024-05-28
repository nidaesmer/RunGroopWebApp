using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModel;

namespace RunGroopWebApp.Controllers
{
                  //home
    public class ClubController : Controller
    {
 
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;


        //private kısmının cıkması ıcın contextte cıkan kutuyo  2.create ile ilerlemeliyiz
        public ClubController(ApplicationDbContext context, IClubRepository clubRepository, IPhotoService photoService)
        {
            _clubRepository = clubRepository;
            _photoService = photoService;
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

        //create sayfası olusturmak
        public IActionResult Create()
        {
            return View();
        }

        /*
        // create sayfasında formu sumbıtlemek ıcın

        [HttpPost] //özellikle form verilerini sunucuya yollamak ıcın
        public async Task<IActionResult> Create(Club club)
        {
            if(!ModelState.IsValid) //hatalıysa hataları gosterır
            {
                return View(club);
            }
            _clubRepository.Add(club); //gecerliyse dbye eklenır form
            return RedirectToAction("Index"); //eklendıkten sonra ındexe yonlendırır
        }
        */
        [HttpPost] 
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);
                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City, 
                        State = clubVM.Address.State,
                    }
                };
                _clubRepository.Add(club); 
                 return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(clubVM);
           
        }

    }
}
