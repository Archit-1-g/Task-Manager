using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OritsoTaskManager.Data;
using OritsoTaskManager.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace OritsoTaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // Register a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            // Check if the email already exists
            var existing = await _context.Users.FirstOrDefaultAsync(x => x.Email == registerRequest.Email);
            if (existing != null)
                return BadRequest("User already exists");

            // Hash the password before saving to DB
            var passwordHash = HashPassword(registerRequest.Password);

            // Create a new User object and save to DB
            var user = new User
            {
                Name = registerRequest.Name,
                Email = registerRequest.Email,
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generate JWT Token for the newly registered user
            //var token = GenerateJwtToken(user);

            return Ok(new
            {
                //token,
                user.Id,
                user.Name,
                user.Email
            });
        }

        // Login an existing user
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == login.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
                return Unauthorized("Invalid email or password");

            //var token = GenerateJwtToken(user);

            return Ok(new
            {
                //token,
                user.Id,
                user.Name,
                user.Email
            });
        }

        // Generate JWT token
        //    private string GenerateJwtToken(User user)
        //    {
        //        var jwtKey = _config["Jwt:Key"];
        //        var jwtIssuer = _config["Jwt:Issuer"];
        //        var jwtAudience = _config["Jwt:Audience"];

        //        if (string.IsNullOrEmpty(jwtKey))
        //            throw new Exception("JWT Key is missing in configuration.");

        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        //        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //        var claims = new[]
        //        {
        //    new Claim("UserId", user.Id.ToString()),
        //    new Claim("Name", user.Name),
        //    new Claim("Email", user.Email)
        //};

        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(claims),
        //            Expires = DateTime.UtcNow.AddHours(8),
        //            SigningCredentials = credentials,
        //            Issuer = jwtIssuer,
        //            Audience = jwtAudience
        //        };

        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var token = tokenHandler.CreateToken(tokenDescriptor);

        //        return tokenHandler.WriteToken(token);
        //    }


        // Password Hashing logic
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Models
        public class RegisterRequest
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
