using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UserLoginAPI.Controllers
{    
    public class UserSubscriptionController : AbstractController{

        [HttpPost("Get")]
        public IActionResult getUserSubscriptions([FromBody] string Username){
            UserSubscription userSubscriptionNone = new UserSubscription {
                Username = Username,
                Alert1 = false,
                Alert2 = false,
                Alert3 = false,
                Alert4 = false,
                Alert5 = false,
                CustomAlert = null
            };
            UserSubscription userSubscription;
            System.Console.WriteLine(Username);
            try{
                userSubscription = DBWrapper.getUserSubscriptionData(Username);  
            } catch (Exception e){
                System.Console.Error.WriteLine(e);
                userSubscriptionNone.Username = e.Message;
                return Ok(userSubscriptionNone);
            }
            
            if(userSubscription == null){
                System.Console.WriteLine("unavailable username");
                return Ok(userSubscriptionNone);//message -> unavailable username
            }
            return Ok(userSubscription);
        }

        [HttpPost("Update")]
        public IActionResult updateUserSubscriptions([FromBody] UserSubscription alerts){
            string queryString = "EXEC updateUserSubs '" + 
            alerts.Username + "', " +
            (alerts.Alert1 ? 1 : 0) + ", " +
            (alerts.Alert2 ? 1 : 0) + ", " +
            (alerts.Alert3 ? 1 : 0) + ", " +
            (alerts.Alert4 ? 1 : 0) + ", " +
            (alerts.Alert5 ? 1 : 0) + ", '" +
            JsonConvert.SerializeObject(alerts.CustomAlert) + "' ;";
            try{
                DBWrapper.sendCommand(queryString);  
            } catch (Exception e){
                System.Console.Error.WriteLine(e);
                return Ok(false);
            }
            return Ok(true);
        }
    }
}