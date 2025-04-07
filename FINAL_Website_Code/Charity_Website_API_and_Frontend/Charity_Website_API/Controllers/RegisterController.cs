using Microsoft.AspNetCore.Mvc;
using Charity_Website_API.Models;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly DBCNhom1 _dbContext;

        public RegisterController(DBCNhom1 dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] UserRegistrationModel user)
        {
            try
            {
                Console.WriteLine($"Received registration: {user.UName}, {user.UEmail}");

                if (string.IsNullOrWhiteSpace(user.UName) || string.IsNullOrWhiteSpace(user.UEmail) ||
                    string.IsNullOrWhiteSpace(user.UPassword))
                {
                    Console.WriteLine("Missing required fields");
                    return BadRequest(new { success = false, message = "Missing required fields" });
                }

                if (!IsValidEmail(user.UEmail))
                {
                    Console.WriteLine("Invalid email format");
                    return BadRequest(new { success = false, message = "Invalid email format" });
                }

                Console.WriteLine("User validation passed");

                var newUser = new TblUser
                {
                    UUserId = Guid.NewGuid().ToString(), // Tạo ID duy nhất
                    UName = user.UName,
                    UEmail = user.UEmail,
                    UPassword = user.UPassword,
                    URole = "customer", // Mặc định là "customer"
                    UCreatedAt = DateTime.UtcNow, // Ghi lại thời gian tạo
                    UPhone = user.UPhone,
                    UAddress = user.UAddress
                };

                _dbContext.TblUsers.Add(newUser);
                _dbContext.SaveChanges();

                Console.WriteLine("User registered successfully");

                return Ok(new { success = true, message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { success = false, message = "An error occurred while registering the user", error = ex.Message });
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        public class UserRegistrationModel
        {
            [Required(ErrorMessage = "Name is required")]
            [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters")]
            public string UName { get; set; }

            [Required(ErrorMessage = "Email is required")]
            [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
            public string UEmail { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [StringLength(255, ErrorMessage = "Password cannot exceed 255 characters")]
            public string UPassword { get; set; }

            [StringLength(255, ErrorMessage = "Phone number cannot exceed 255 characters")]
            public string? UPhone { get; set; }

            [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters")]
            public string? UAddress { get; set; }
        }
    }
}