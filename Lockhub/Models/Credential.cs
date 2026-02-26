namespace Lockhub.Models
{
    public class Credential
    {
        public int CredentialId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public int VaultId { get; set; } //FK to VaultId

        public Credential()
        {
            
        }

        public Credential(int credentialId, string name, string username, 
            string password, string url, int vaultId)
        {
            CredentialId = credentialId;
            Name = name;
            Username = username;
            Password = password;
            Url = url;
            VaultId = vaultId;
        }
    }
}
