using SCM.DataService.DataContext;
using SCM.DataService;

namespace SCM.Core
{
    public interface IEntityEvent
    {
        void Added(AppDataContext context);

        void Adding(AppDataContext context);


        void Deleting(AppDataContext context);

        void Deleted(AppDataContext context);


        void Updating(AppDataContext context);

        void Updated(AppDataContext context);
    }
}