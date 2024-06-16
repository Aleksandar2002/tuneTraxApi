namespace Domain.Entities;

public class InvalidToken
{
    public Guid TokenId { get; set; }
    // public string Token { get; set; }
    public DateTime AddedAt { get; set; }
    public int UserId { get; set; }
}
