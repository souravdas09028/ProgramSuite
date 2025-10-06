namespace Application.Contracts.DTOs
{
    public record RegisterRequest(string Email, string Password);
    public record LoginRequest(string Email, string Password);
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
