namespace Lockhub.Models
{
    public class Vault
    {
        public int VaultId { get; set; }
        public string Name { get; set; }
        public int OrganisationId { get; set; } //FK to OrgID
        public int CreatedBy { get; set; } //FK to user
        
    }
}
