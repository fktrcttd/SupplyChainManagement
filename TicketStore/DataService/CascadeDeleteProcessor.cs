using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using TicketStore.DataService;

namespace TicketStore.Core
{
    public class CascadeDeleteProcessor
    {
        public void Process(object instance)
        {
            var context = AppDataContext.JoinOrOpen();
            var type = ObjectContext.GetObjectType(instance.GetType());
            var properties = type.GetProperties<DummyCondition>()
                .Where(x => x.HasAttribute<CascadeDeleteAttribute>());

            foreach (var propertyDescriptor in properties)
            {
                if (typeof(IEnumerable).IsAssignableFrom(propertyDescriptor.PropertyType))
                {
                    var itemsToDelete = new List<dynamic>();
                    foreach (var item in instance.GetValue<IEnumerable>(propertyDescriptor.Name))
                    {
                        itemsToDelete.Add(item);
                    }

                    foreach (var o in itemsToDelete)
                    {
                        context.Delete(o);
                    }
                }
                else if (!propertyDescriptor.PropertyType.IsValueType &&
                         propertyDescriptor.PropertyType != typeof(string))
                {
                    context.Delete(propertyDescriptor.GetValue(instance));
                }
            }
        }
    }
}