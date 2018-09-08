namespace TicketStore.Core
{
    public interface IEntityEvent
    {
        void Added(DataContext context);

        void Adding(DataContext context);


        void Deleting(DataContext context);

        void Deleted(DataContext context);


        void Updating(DataContext context);

        void Updated(DataContext context);
    }
}