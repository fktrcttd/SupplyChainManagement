using System.ComponentModel;

namespace TicketStore.Core
{
    public interface IPropertyCondition
    {
        bool IsSatisfiedBy(PropertyDescriptor property);
    }
}