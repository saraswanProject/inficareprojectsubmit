namespace inficareprojectsubmit.Controller
{
    public class UserClassModel
    {
        public string UserName { get; set; }    
        public string Password { get; set; }    
    }

    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }  
        
    }
    public class BankModel
    {
        public int BankId { get; set; } 
        public string BankName { get; set; }    
        public string BankAccount { get; set; }
    }
}
