using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Data;
using MyWebAPI.Model;

namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : Controller
    {
        //Crude Create, Read, Update, Delete record 

        private readonly DataContext _dbContext;

        public UserController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserEntity>> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<UserEntity> GetUsers(int id)
        {
            return _dbContext.Users.Find(id);
        }

        [HttpPost("adduser")]
        public async Task<ActionResult<string>> AddUser(UserEntity registerObject)
        {
            var User = new UserEntity
            {
                Username = registerObject.Username,
                Emailaddress = registerObject.Emailaddress,
                Mobilenumber = registerObject.Mobilenumber,
                Password = registerObject.Password,
            };

            _dbContext.Users.Add(User);
            await _dbContext.SaveChangesAsync();

            return "User added successfully";
        }

        [HttpPost("updateuserdetails")]
        public async Task<ActionResult<string>> UpdateUser(UserEntity updateObj)
        {
            var currentUser = _dbContext.Users.Where(s => s.ID == updateObj.ID).FirstOrDefault();
            if (currentUser != null)
            {
                currentUser.Username = updateObj.Username;
                currentUser.Emailaddress = updateObj.Emailaddress;
                currentUser.Mobilenumber = updateObj.Mobilenumber;
                currentUser.Password = updateObj.Password;

                _dbContext.SaveChanges();
            }
            else
            {
                return "User details NOT Found";
            };

            return "User deatils UPDATED Successfully";

        }

        [HttpDelete("deleteuser")]
        public async Task<ActionResult<string>> DeleteUser (int ID)
        {
            if (ID <= 0)
            {
                return BadRequest ("Not a valid User");
            }
            var currentUser =_dbContext.Users.Where (s => s.ID == ID).FirstOrDefault<UserEntity>();

            if(currentUser != null)
            {
                _dbContext.Entry(currentUser).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _dbContext.SaveChanges();
            }
            else
            {
                return "User Details NOT Found For Deletion";
            }

            return "User deatils DELETED Successfully";
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
