using Lockhub.Models;

namespace Lockhub.Repositories.Interfaces
{
    public interface ICredentialRepo
    {
        void AddCredential(Credential credential);
        void RemoveCredential(int id);
        void UpdateCredential(Credential credential);
        void UpdateVaultIdOnCredential(Credential credential, Vault vault);
        IEnumerable<Credential> GetCredentials();
    }
}
