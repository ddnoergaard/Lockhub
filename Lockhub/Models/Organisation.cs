namespace Lockhub.Models
{
    public class Organisation
    {
        public int OrganisationId { get; set; }
        public string Name { get; set; }
        public string VatNumber { get; set; }
        public string HashPassword { get; set; }
        public string Phone { get; set; }
        public int SubscriptionId { get; set; } //FK to SubscriptionId
        public DateTime CreatedAt { get; set; }

        public Organisation()
        {
            
        }

        public Organisation(int orgID, string name, string vatNumber, 
            string hashPassword, string phone, int subscriptionId, DateTime createdAt)
        {
            OrganisationId = orgID;
            Name = name;
            VatNumber = vatNumber;
            HashPassword = hashPassword;
            Phone = phone;
            SubscriptionId = subscriptionId;
            CreatedAt = createdAt;
        }
    }
}
