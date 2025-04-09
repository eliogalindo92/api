namespace api.Dtos.Auth
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
       public List<string> Roles { get; set; } = new List<string>();
       public List<string> Permissions { get; set; } = new List<string>(); 
    }
}