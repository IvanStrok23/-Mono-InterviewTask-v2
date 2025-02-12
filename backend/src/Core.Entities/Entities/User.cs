namespace MonoTask.Core.Entities.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
    public bool IsClient { get; set; }
    public bool IsSuperAdmin { get; set; }
}
