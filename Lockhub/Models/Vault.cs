namespace Lockhub.Models
{
    public class Vault
    {
        public int VaultId { get; set; }
        public string Name { get; set; }
        public int OrganisationId { get; set; } //FK to OrgID
        public int CreatedBy { get; set; } //FK to user

        public Vault()
        {
            
        }
        public Vault(string name, int organisationId, int createdBy)
        {
            Name = name;
            OrganisationId = organisationId;
            CreatedBy = createdBy;
        }

        public Vault(int vaultId, string name, int organisationId, int createdBy)
        {
            VaultId = vaultId;
            Name = name;
            OrganisationId = organisationId;
            CreatedBy = createdBy;
        }
    }
}
