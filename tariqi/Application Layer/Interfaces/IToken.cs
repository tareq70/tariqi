using tariqi.Domain_Layer.Entities;

namespace tariqi.Application_Layer.Interfaces
{
    public interface IToken
    {
       public string GenerateJwtToken(ApplicationUser user, string role);
    }
}
