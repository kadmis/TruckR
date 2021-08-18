using System.Reflection;

namespace BuildingBlocks.EventBus
{
    public class EventBusAssembly
    {
        public static Assembly Assembly => Assembly.GetExecutingAssembly();
    }
}
