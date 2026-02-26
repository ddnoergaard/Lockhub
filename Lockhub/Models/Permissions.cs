namespace Lockhub.Models
{
    public class Permissions
    {
        public int PermissionId { get; set; }
        public string Name { get; set; }

        public Permissions()
        {
            
        }

        public Permissions(int permissionId, string name)
        {
            PermissionId = permissionId;
            Name = name;
        }
    }
}
