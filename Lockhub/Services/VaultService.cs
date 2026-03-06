using Lockhub.Models;
using Lockhub.Repositories.Interfaces;
using Lockhub.Services.Interfaces;
namespace Lockhub.Services
{
    public class VaultService : IVaultService
    {
        private IVaultRepo _vaultRepo;
        private List<Vault> _vaults;

        public VaultService(IVaultRepo vaultRepo)
        {
            _vaultRepo = vaultRepo;
        }

        public IEnumerable<Vault> GetVaultsUserAuth(int id)
        {
            return _vaults = _vaultRepo.GetVaultsWithSpecificOrgId(id).ToList();
        }

        public int GetVaultCountUserAuth(int id)
        {
            _vaults = _vaultRepo.GetVaultsWithSpecificOrgId(id).ToList();
            return _vaults.Count();
        }
    }
}
