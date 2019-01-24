using System.Threading.Tasks;
using BATDemoSalesForce.Models;

namespace BATDemoSalesForce.Repos
{
    public interface IProfileRepository : IRepository<Profile>
    {
        Task<string> CreateAsync(Profile entity);

        Task<Profile> GetByEmailAsync(string email);

        Task<Profile> GetBySecondaryEmailAsync(string email);

        Task<bool> IsEmailExisting(string email, string secondaryEmail = null);
    }
}