namespace Application.Contracts.DTOs
{
    public record RegisterRequest(string Email, string Password);
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public record AuthResponse(string Token, DateTime ExpiresAt);
    public record ProgramDto(
       int Id,
       string Name,
       string Description,
       DateTime StartDate,
       DateTime EndDate,
       bool IsActive,
       int ParticipantCount
   );
}
