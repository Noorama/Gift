using HedyoCom_AspNet.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HedyoCom_AspNet.DAL.Repositories
{
    public class EventRepository : GenericRepository<Event>
    {
        public EventRepository(ApplicationDbContext db) : base(db) {}

        public Event GetEventForManageById(int id, params Expression<Func<Event, object>>[] includeProperties)
        {
            return this.Get(
                x => x.Id == id,
                x => x.Gifts.Select(y => y.Image),
                x => x.Characters,
                x => x.EventType).FirstOrDefault();
        }

        public override void Update(Event entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;

            foreach (var character in entityToUpdate.Characters)
            {
                if (character == null)
                {
                    continue;
                }
                context.Characters.Attach(character);
                context.Entry(character).State = EntityState.Modified;
            }
        }
    }
}