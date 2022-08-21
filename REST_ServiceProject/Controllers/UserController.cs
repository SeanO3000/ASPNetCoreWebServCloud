using Microsoft.AspNetCore.Mvc;
using REST_ServiceProject.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace REST_ServiceProject.Controllers
{
    /// <summary>
    /// REST API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository; //user repository
        private readonly ILogger<UserController> logger;
        public UserController(ILogger<UserController> logger,
           IUserRepository userRepository)
        {
            this.logger = logger;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Get all the users in the list/database
        /// </summary>
        /// <returns></returns>
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult GetAll()
        {
            logger.LogInformation("Information");
            logger.LogWarning("Warning");
            logger.LogError("Error");

            return Ok(userRepository.Users);
        }

        /// <summary>
        /// Get specific user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetSpecific(int id)
        {
            var user = userRepository.Users.FirstOrDefault(t => t.Id == id);

            if (user == null)
            {
                return NotFound(null);
            }

            return Ok(user);
        }

        /// <summary>
        /// Post new user to list/database
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {
            if (string.IsNullOrEmpty(value.Email) || string.IsNullOrEmpty(value.Password))
            {
                return BadRequest(new ErrorResponse
                    {
                        Message = "Null or Empty Field",
                        Field = "Email or Password"
                    });
            }

            userRepository.Add(value);

            return CreatedAtAction(nameof(GetSpecific),new { id = value.Id },value);
        }

        /// <summary>
        /// Put new data into existing user by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User value)
        {
            if (string.IsNullOrEmpty(value.Email) || string.IsNullOrEmpty(value.Password))
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Null or Empty Field",
                    Field = "Email or Password"
                });
            }

            if (userRepository.Update(id, value))
            {
                return Ok(value);
            }

            return NotFound();
        }

        /// <summary>
        /// Delete user for list/database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!userRepository.Delete(id))
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
