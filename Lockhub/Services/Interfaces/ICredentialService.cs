using Lockhub.Models;

namespace Lockhub.Services.Interfaces
{
    public interface ICredentialService
    {
        IEnumerable<Credential> GetAllCredentialsWhereUserHasAuth(User user);
        Credential GetCredentialById(int id, User user);
        IEnumerable<Credential> GetCredentialByVaultId(int vaultId, User user);
        IEnumerable<Credential> GetCredentialByUrl(string url, User user);
        Dictionary<int, List<Credential>> GetAllCredentialsWhereUserHasAuthVaultSeparated(User user);
        void MoveCredentialToVault(Credential credential, Vault vault, User user);

    }
}
