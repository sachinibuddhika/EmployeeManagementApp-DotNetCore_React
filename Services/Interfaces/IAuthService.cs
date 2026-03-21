namespace EmployeeManagementAPI.Services.Interfaces
{
    public interface IAuthService
    {
        string Authenticate(string email, string password);
    }
}
