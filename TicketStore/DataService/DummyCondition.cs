using System.ComponentModel;

namespace TicketStore.Core
{
    public class DummyCondition : IPropertyCondition
    {
        public bool IsSatisfiedBy(PropertyDescriptor property)
        {
            return true;
        }
    }
}