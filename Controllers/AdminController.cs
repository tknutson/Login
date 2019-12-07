using Login.Models;
using Login.Processors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Login.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly DatabaseManager _databaseManager;
        private readonly AppDb _db;

        public AdminController(ILogger<AdminController> logger, DatabaseManager databaseManager, AppDb db)
        {
            _logger = logger;
            _databaseManager = databaseManager;
            _db = db;
        }

        public IActionResult Index()
        {
            //Get existing users
            return View();
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task AddUser(User user)
        {
            //save user
            user.Password = SHA1.Encode(user.Password);            
            var dbMgr = new DatabaseManager(_db);
            await dbMgr.AddUserAsync(user);
            
            //redirect to index
            RedirectToAction("Index", "Admin");
        }

    }
}