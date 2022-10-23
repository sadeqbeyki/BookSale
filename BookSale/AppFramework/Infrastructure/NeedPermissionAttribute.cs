namespace AppFramework.Infrastructure
{
    //[AttributeUsage(AttributeTargets.All)]
    public class NeedsPermissionAttribute : Attribute
    {
        public int Permission { get; set; }

        public NeedsPermissionAttribute(int permission)
        {
            Permission = permission;
        }
    }
}
