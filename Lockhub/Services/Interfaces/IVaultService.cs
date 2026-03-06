using Lockhub.Models;

namespace Lockhub.Services.Interfaces
{
    public interface IVaultService
    {
        IEnumerable<Vault> GetVaultsUserAuth(int id);
        int GetVaultCountUserAuth(int id);
    }
}
