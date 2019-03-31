namespace TicketStore.Core
{
    public interface IEntityEvent
    {
        void Added(ApplicationDataContext context);

        void Adding(ApplicationDataContext context);


        void Deleting(ApplicationDataContext context);

        void Deleted(ApplicationDataContext context);


        void Updating(ApplicationDataContext context);

        void Updated(ApplicationDataContext context);
    }
}