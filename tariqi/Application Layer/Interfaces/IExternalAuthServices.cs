namespace tariqi.Application_Layer.Interfaces
{
    public interface IExternalAuthServices
    {
        Task<string> GoogleLoginAsync(string email);

    }
}
