using Lockhub.Models;
using Lockhub.Repositories.Interfaces;
using Lockhub.Services.Interfaces;
using System.Net;

namespace Lockhub.Services
{
    public class CredentialService : ICredentialService
    {
        private IVaultRepo _vaultRepo;
        private ICredentialRepo _credentialRepo;
        private List<Vault> _vaults;
        private List<Credential> _credentials;

        public CredentialService(IVaultRepo vaultRepo, ICredentialRepo credentialRepo)
        {
            _vaultRepo = vaultRepo;
            _credentialRepo = credentialRepo;
            _credentials = _credentialRepo.GetCredentials().ToList();
        }

        public IEnumerable<Credential> GetAllCredentialsWhereUserHasAuth(User user)
        {
            List<Credential> returnList = new List<Credential>();
            _vaults = _vaultRepo.GetVaultsWithSpecificOrgId(user.OrganisationId).ToList();

            List<int> vaultIds = new List<int>();
            foreach (Vault v in _vaults)
            {
                vaultIds.Add(v.VaultId);
            }

            foreach (Credential c in _credentials)
            {
                if (vaultIds.Contains(c.VaultId))
                {
                    returnList.Add(c);
                }
            }
            return returnList;
        }

        public bool IdentifyWeakPassword(string inputPassword)
        {
            List<string> commonWords = new List<string> { "password", "pass", "pwd", "welcome", "hello", "login", "admin", "user", "guest", "root", "iloveyou", "letmein", "monkey", "dragon", "master", "football", "soccer", "baseball", "liverpool", "chelsea", "arsenal", "batman", "superman", "qwerty", "qwert", "12345678" };
            if (commonWords.Contains(inputPassword) || commonWords.Contains(inputPassword.ToLower()) || !inputPassword.Any(c => char.IsUpper(c)) || !inputPassword.Any(c => char.IsDigit(c)) || inputPassword.Length < 8)
            {
                return true;
            }
            return false;
        }

        public int GetCountOfWeakPasswords(User user)
        {
            _credentials = GetAllCredentialsWhereUserHasAuth(user).ToList();

            int returnInt = 0;

            foreach (Credential c in _credentials)
            {
                if (IdentifyWeakPassword(c.Password))
                {
                    returnInt++;
                } 
            }
            return returnInt;
        }

        public Credential GetCredentialById(int id, User user)
        {
            List<Credential> tempList = GetAllCredentialsWhereUserHasAuth(user).ToList();

            foreach (Credential c in tempList)
            {
                if (c.CredentialId == id)
                {
                    return c;
                }
            }
            throw new Exception("No credential with that id found");

        }

        public IEnumerable<Credential> GetCredentialByVaultId(int vaultId, User user)
        {
            List<Credential> returnList = new List<Credential>();
            _vaults = _vaultRepo.GetVaultsWithSpecificOrgId(user.OrganisationId).ToList();
            List<int> vaultIds = new List<int>();

            foreach (Vault v in _vaults)
            {
                vaultIds.Add(v.VaultId);
            }
            if (!vaultIds.Contains(vaultId))
            {
                throw new AccessViolationException("Access not granted for given vault");
            }

            foreach (Credential c in _credentials)
            {
                if (c.VaultId == vaultId)
                {
                    returnList.Add(c);
                }
            }
            if (returnList.Count == 0)
            {
                throw new Exception("No credential found with that id");
            }
            return returnList;
        }

        public IEnumerable<Credential> GetCredentialByUrl(string url, User user)
        {
            List<Credential> tempList = GetAllCredentialsWhereUserHasAuth(user).ToList();
            List<Credential> returnList = new List<Credential>();

            foreach (Credential c in tempList)
            {
                if (c.Url.Contains(url))
                {
                    returnList.Add(c);
                }
            }
            if (returnList.Count == 0)
            {
                throw new Exception("No credential(s) found with that url");
            }

            return returnList;
        }

        public Dictionary<int, List<Credential>> GetAllCredentialsWhereUserHasAuthVaultSeparated(User user)
        {
            Dictionary<int, List<Credential>> returnDict = new Dictionary<int, List<Credential>>();
            _vaults = _vaultRepo.GetVaultsWithSpecificOrgId(user.OrganisationId).ToList();
            List<int> vaultIds = new List<int>();

            foreach (Vault v in _vaults)
            {
                vaultIds.Add(v.VaultId);
            }

            foreach (Credential c in _credentials)
            {
                if (vaultIds.Contains(c.VaultId))
                {
                    if (!returnDict.ContainsKey(c.VaultId)) //checks if key does not exist
                    {
                        returnDict.Add(c.VaultId, new List<Credential>());
                        returnDict[c.VaultId].Add(c);
                    }
                    else
                    {
                        returnDict[c.VaultId].Add(c);
                    }
                }
            }
            return returnDict;
        }
        public void MoveCredentialToVault(Credential credential, Vault vault, User user)
        {
            if (vault.OrganisationId == user.OrganisationId)
            {
                _credentialRepo.UpdateVaultIdOnCredential(credential, vault);
            }
        }
    }
}
