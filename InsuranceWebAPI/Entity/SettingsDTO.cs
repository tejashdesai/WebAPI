
namespace InsuranceWebAPI.Entity
{
    public class SettingsDTO
    {
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }
        public string SenderName { get; set; }
        public string CredentialEmailID { get; set; }
        public string CredentialPassword { get; set; }
        public string Mobile { get; set; }
        public string SMSUserName { get; set; }
        public string SMSPassword { get; set; }
        public string SMSSender { get; set; }
        public string SMSRoute { get; set; }
        public string SMSType { get; set; }
    }
}