
namespace TravelAgency.Manager.Abstractions
{
    public abstract class ManagerBase
    {
        public IUserManager UserManager { get; }
        public ManagerBase(IUserManager userManager = null)
        {
            UserManager = userManager;
        }
    }
}
