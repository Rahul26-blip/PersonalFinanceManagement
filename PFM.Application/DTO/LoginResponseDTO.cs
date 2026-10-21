namespace PFM.Application.DTO
{
    public class LoginResponseDTO
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string UserCode { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;
    }
}
