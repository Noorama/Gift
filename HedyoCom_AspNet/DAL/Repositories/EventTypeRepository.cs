using HedyoCom_AspNet.Models;
using System.Linq;

namespace HedyoCom_AspNet.DAL.Repositories
{
    public class EventTypeRepository: GenericRepository<EventType>
    {
        public EventTypeRepository(ApplicationDbContext context) : base(context) { }

        public EventType GetByName(string name)
        {
            return this.Get(x => x.Name == name).FirstOrDefault();
        }
    }
}