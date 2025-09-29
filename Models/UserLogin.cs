namespace CRUD_Employee_standAlone.Models
{
    public class UserLogin
    {
        public int Id { get; set; }
        public DateTime LoginTime { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }
}