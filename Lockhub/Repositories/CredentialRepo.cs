using Lockhub.Models;
using Lockhub.Repositories.Interfaces;
using Lockhub.Services;

namespace Lockhub.Repositories
{
    public class CredentialRepo : ICredentialRepo
    {
        private JsonFileService<Credential> _jsonFileServiceCredential;
        private IVaultRepo _vaultRepo;
        private List<Credential> _credentials;
        private List<Vault> _vaults;

        public CredentialRepo(JsonFileService<Credential> jsonFileServiceCredential, IVaultRepo vaultRepo)
        {
            _jsonFileServiceCredential = jsonFileServiceCredential;
            _vaultRepo = vaultRepo;
            _credentials = _jsonFileServiceCredential.GetJsonObjects().ToList();
            //_credentials = new List<Credential>();
            //_credentials.Add(new Credential(1,"test","testname","test123","github.com/login", 0));
            //_jsonFileServiceCredential.SaveJsonObjects(_credentials);
        }

        public IEnumerable<Credential> GetCredentials()
        {
            return _credentials;
        }

        public void AddCredential(Credential credential)
        {
            _credentials.Add(credential);
            _jsonFileServiceCredential.SaveJsonObjects(_credentials);
        }

        public void RemoveCredential(int id)
        {
            foreach (Credential c in _credentials)
            {
                if (c.CredentialId == id)
                {
                    _credentials.Remove(c);
                    break;
                }
            }
            _jsonFileServiceCredential.SaveJsonObjects(_credentials);
        }

        public void UpdateCredential(Credential credential)
        {
            foreach (Credential c in _credentials)
            {
                if (c.CredentialId == credential.CredentialId)
                {
                    c.Name = credential.Name;
                    c.Username = credential.Username;
                    c.Password = credential.Password;
                    c.Url = credential.Url;
                    break;
                }
            }
            _jsonFileServiceCredential.SaveJsonObjects(_credentials);
        }

        public void UpdateVaultIdOnCredential(Credential credential, Vault vault)
        {
            foreach (Credential c in _credentials)
            {
                if (c.CredentialId == credential.CredentialId)
                {
                    c.VaultId = vault.VaultId;
                    break;
                }
            }
            _jsonFileServiceCredential.SaveJsonObjects(_credentials);
        }
    }
}
