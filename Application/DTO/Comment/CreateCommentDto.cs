namespace Application.DTO.Comment;

public class CreateCommentDto
{
    public string Text { get; set; }
    public int? TrackId { get; set; }
    public int? ParentId { get; set; }
}
