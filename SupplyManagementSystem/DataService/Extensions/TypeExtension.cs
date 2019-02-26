using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace SCM.Core
{
    public static class TypeExtension
    {
        [CanBeNull]
        public static T GetValue<T>(this object @this, string name)
        {
            return (T)GetValue(@this, name);
        }
        
        [CanBeNull]
        public static object GetValue(this object @this, string name)
        {
            var descriptors = TypeDescriptor.GetProperties(@this);

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var item in descriptors.OfType<PropertyDescriptor>())
            {
                if (!item.Name.Equals(name))
                    continue;

                return item.GetValue(@this);
            }

            return null;
        }
    }
}