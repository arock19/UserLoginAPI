using System;
using Microsoft.AspNetCore.Mvc;

namespace UserLoginAPI.Controllers
{
    
    public class UserLoginController : AbstractController{
        
        [HttpPost("Add")]
        public string addUserLogin([FromBody] UserLogin user){//checked --works
            string queryString = "EXEC addUser '" + user.Username + 
            "' , '" + user.Password + "' ;";
            try{
                DBWrapper.sendCommand(queryString);
            } catch(Exception e) {
                System.Console.Error.Write(e);
            }
            
            return "ok";
        }

        [HttpPost("Check")]
        public IActionResult checkUserLogin([FromBody] UserLogin userInput){//checked --works
            // System.Console.WriteLine(userInput1);
            // return true;
            // User userInput = new User{
            //     Username = "avd",
            //     Password = "dsvv"
            // };
            UserLogin userActual = null;
            System.Console.Write(userInput);
            try{
                userActual = DBWrapper.getUserLoginData(userInput.Username);  
            } catch (Exception e){
                System.Console.Error.Write(e);
                return Ok(false);
            }
            
            if(userActual == null){
                System.Console.WriteLine("unavailable username");
                return Ok(false);//message -> unavailable username
            } 
            if(userActual.Password.Equals(userInput.Password)){
                System.Console.WriteLine("correct password");
                return Ok(true);
            }
            System.Console.WriteLine("incorrect password");
            return Ok(false); //message -> incorrect password
        }

        // [HttpGet]
        // public IActionResult getUserLogin(){//checked --works
        //     return Ok("Get Works");  
        // }

        [HttpPost("Update")]
        public bool updateUserLogin([FromBody] UserLogin user){//checked --works
            string queryString = "EXEC updateUser '" + user.Username + 
            "' , '" + user.Password + "' ;";
            try{
                DBWrapper.sendCommand(queryString);
            } catch(Exception e) {
                System.Console.Error.Write(e);
                return false;
            }
            
            return true;
        }
    } 
}