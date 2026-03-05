using Lockhub.Models;

namespace Lockhub.Repositories.Interfaces
{
    public interface IVaultRepo
    {
        void AddVault(Vault vault);
        void RemoveVault(Vault vault);
        void UpdateVaultName(Vault vault);
        IEnumerable<Vault> GetAllVaults();
        IEnumerable<Vault> GetVaultsWithSpecificOrgId(int orgId);
    }
}
