using System;
using Microsoft.AspNetCore.Mvc;

namespace UserLoginAPI.Controllers
{
    [ApiController]
    class UserLoginController : AbstractController{
        
        [HttpPost("UserAdd")]
        public static string addUserLogin([FromBody] User user){//checked --works
            string queryString = "EXEC addUser '" + user.Username + 
            "' , '" + user.Password + "' ;";
            try{
                DBWrapper.sendCommand(queryString);
            } catch(Exception e) {
                System.Console.Error.Write(e);
            }
            
            return "ok";
        }

        [HttpPost("UserCheck")]
        public static bool checkUserLogin([FromBody] User userInput){//checked --works
            User userActual = null;
            try{
                userActual = DBWrapper.getUserLoginData(userInput.Username);  
            } catch (Exception e){
                System.Console.Error.Write(e);
                return false;
            }
            
            if(userActual == null){
                System.Console.WriteLine("unavailable username");
                return false;//message -> unavailable username
            } 
            if(userActual.Password.Equals(userInput.Password)){
                System.Console.WriteLine("correct password");
                return true;
            }
            System.Console.WriteLine("incorrect password");
            return false; //message -> incorrect password
        }

        [HttpPost("UserUpdate")]
        public static bool updateUserLogin([FromBody] User user){//checked --works
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