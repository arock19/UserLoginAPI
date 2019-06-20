using Microsoft.AspNetCore.Mvc;

namespace UserLoginAPI.Controllers
{
    [ApiController]
    class UserLoginController : AbstractController{
        
        [HttpPost("Users")]
        public string addUserLogin([FromBody] User user){
            string queryString = "EXEC addUser '" + user.Username + 
            "' , '" + user.Password + "' ;";
            DBWrapper.sendCommand(queryString);
            return "ok";
        }

        [HttpPost("UserCheck")]
        public bool checkUserLogin([FromBody] User userInput){
            User userActual = DBWrapper.getUserLoginData(userInput.Username);
            if(userActual == null){
                return false;//message -> unavailable username
            } 
            if(userActual.Password.Equals(userInput.Password)){
                return true;
            }
            return false; //message -> incorrect password
        }
    } 
}