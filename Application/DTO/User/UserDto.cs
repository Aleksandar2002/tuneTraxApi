using Domain.Entities;

namespace Application.DTO.User;

public class UserDto
{
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public string Country { get; set; }
    public Gender Gender { get; set; }
}
