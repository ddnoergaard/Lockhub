using Lockhub.Models;
using Lockhub.Repositories.Interfaces;
using Lockhub.Services;

namespace Lockhub.Repositories
{
    public class VaultRepo : IVaultRepo
    {
        private JsonFileService<Vault> _fileService;
        private List<Vault> _vaults;

        public VaultRepo(JsonFileService<Vault> jsonFileService)
        {
            _fileService = jsonFileService;
            //_vaults = new List<Vault>();
            //_vaults.Add(new Vault(1, "testVault", 1, 1));
            //_fileService.SaveJsonObjects(_vaults);
            _vaults = _fileService.GetJsonObjects().ToList();
        }
        public void AddVault(Vault vault)
        {
            _vaults.Add(vault);
            _fileService.SaveJsonObjects(_vaults);
        }

        public void RemoveVault(Vault vault)
        {
            _vaults.Remove(vault);
            _fileService.SaveJsonObjects(_vaults);
        }

        public void UpdateVaultName(Vault vault)
        {
            foreach (Vault v in _vaults)
            {
                if (v.VaultId == vault.VaultId)
                {
                    v.Name = vault.Name;
                }
            }
        }

        public IEnumerable<Vault> GetAllVaults()
        {
            return _vaults;
        }

        public IEnumerable<Vault> GetVaultsWithSpecificOrgId(int orgId)
        {
            List<Vault> returnList = new List<Vault>();

            foreach (Vault v in _vaults)
            {
                if (v.OrganisationId == orgId)
                {
                    returnList.Add(v);
                }
            }
            return returnList;
        }


    }
}
