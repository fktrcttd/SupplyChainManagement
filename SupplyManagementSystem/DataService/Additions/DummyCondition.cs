using System.ComponentModel;

namespace SCM.Core
{
    public class DummyCondition : IPropertyCondition
    {
        public bool IsSatisfiedBy(PropertyDescriptor property)
        {
            return true;
        }
    }
}