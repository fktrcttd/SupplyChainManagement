using System.ComponentModel;

namespace SCM.Core
{
    public interface IPropertyCondition
    {
        bool IsSatisfiedBy(PropertyDescriptor property);
    }
}