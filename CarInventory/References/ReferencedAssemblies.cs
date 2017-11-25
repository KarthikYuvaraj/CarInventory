using System.Reflection;

namespace CarInventory.References
{
    public static class ReferencedAssemblies
    {
        public static Assembly Services
        {
            get { return Assembly.Load("CarInventory.Services"); }
        }

        public static Assembly Repositories
        {
            get { return Assembly.Load("CarInventory.Data"); }
        }

        public static Assembly Dto
        {
            get
            {
                return Assembly.Load("CarInventory.Dto");
            }
        }

        public static Assembly Domain
        {
            get
            {
                return Assembly.Load("CarInventory.Core");
            }
        }
    }
}
