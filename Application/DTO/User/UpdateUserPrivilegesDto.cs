namespace Application.DTO.User;

public class UpdateUserPrivilegesDto
{
    public int UserId { get; set; }
    public IEnumerable<int> UseCaseIds { get; set; }
}
