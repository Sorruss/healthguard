namespace healthguard.Models
{
    public class Administrator : BaseUser
    {
        public Administrator()
        {
            Role = "Administrator";
        }

        public int AdministratorId { get; set; }
    }
}
