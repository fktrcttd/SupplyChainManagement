using System;

namespace TicketStore.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CascadeDeleteAttribute : Attribute
    {

    }
}