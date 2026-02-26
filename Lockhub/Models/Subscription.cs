using System.Reflection.Metadata.Ecma335;

namespace Lockhub.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int CredentialCapacity { get; set; }

        public Subscription()
        {
            
        }

        public Subscription(int subscriptionId, string name, float price, int credentialCapacity)
        {
            SubscriptionId = subscriptionId;
            Name = name;
            Price = price;
            CredentialCapacity = credentialCapacity;
        }
    }
}
