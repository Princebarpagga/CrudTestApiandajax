using Angulartestapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;


namespace Angulartestapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AngularTestDbContext _context;
        private readonly IConfiguration _configuration;

        public UserController(AngularTestDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
      
        [HttpPost("login")] 
        public async Task<IActionResult> Login(LoginVm model) 
        {
           
           
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            
            if (user.Password != model.Password)
            {
                return Unauthorized("Invalid username or password.");
            }

            
            var token = GenerateJwtToken(user);

            return Ok(new { Token = token }); 
        }
        //private string GenerateJwtToken(User user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    // var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]);
        //    // var key = Encoding.ASCII.GetBytes("Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx"); 
        //    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        //    var issuer = _configuration["Jwt:Issuer"];
        //    var audience = _configuration["Jwt:Audience"];
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[] 
        //        {
        //            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        //            new Claim(ClaimTypes.Name, user.Username)
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
        //            SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key  = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var tocken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                 _configuration["Jwt:Audience"],
                
                 expires: DateTime.UtcNow.AddMinutes(10),
                 signingCredentials: signIn);
            var jwttocken = new  JwtSecurityTokenHandler().WriteToken(tocken);

            // Retrieve JWT configuration values from appsettings.json
            //var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            //var issuer = _configuration["Jwt:Issuer"];
            //var audience = _configuration["Jwt:Audience"];

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            //new Claim(ClaimTypes.Name, user.Username)
            //    }),
            //   // Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            //        SecurityAlgorithms.HmacSha256Signature)
            //};

            //var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(tocken);
        }


        
      //  [Authorize] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()  
        {
            return await _context.Users.ToListAsync();
        }

       
        [HttpGet("{id}")] 
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

 
        [HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    if (_context.Users.Any(u => u.Username == user.Username))
        //    {
        //        return Conflict("A user with the same username already exists.");
        //    }

        //    if (!IsValidMobileNumber(user.MobileNumber))
        //    {
        //        return BadRequest("Invalid mobile number format.");
        //    }
        //    if (_context.Users.Any(u => u.Username == user.Username))
        //    {
        //        return Conflict("A user with the same username already exists.");
        //    }
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        //}
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                if (_context.Users.Any(u => u.UserId == user.UserId))
                {
                    return Conflict("A user with the same user ID already exists.");
                }

                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    return Conflict("A user with the same username already exists.");
                }

                if (!IsValidMobileNumber(user.MobileNumber))
                {
                    return BadRequest("Invalid mobile number format.");
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        private bool IsValidMobileNumber(string mobileNumber)
        {
            
            string pattern = @"^\d{10}$"; 

           
            return Regex.IsMatch(mobileNumber, pattern);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}

