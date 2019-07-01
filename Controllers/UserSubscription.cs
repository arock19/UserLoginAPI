namespace UserLoginAPI.Controllers
{
    public class UserSubscription
    {
        public string Username { get; set; }
        public bool Alert1 { get; set; }
        public bool Alert2 { get; set; }
        public bool Alert3 { get; set; }
        public bool Alert4 { get; set; }
        public bool Alert5 { get; set; }
        public Alert[] CustomAlert { get; set; }
    }

    public class Alert 
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}