public class UserSessionService
{
    public string? UserName { get; private set; }
    public string? Email { get; private set; }
    public bool IsLoggedIn => !string.IsNullOrEmpty(UserName);

    public void StartSession(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email)) return;
        UserName = name;
        Email = email;
    }

    public void EndSession()
    {
        UserName = null;
        Email = null;
    }
}