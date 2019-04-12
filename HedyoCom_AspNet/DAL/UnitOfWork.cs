using HedyoCom_AspNet.DAL.Repositories;
using HedyoCom_AspNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HedyoCom_AspNet.DAL
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private EventTypeRepository eventTypeRepository;

        public EventTypeRepository EventTypeRepository
        {
            get
            {
                if (this.eventTypeRepository == null)
                {
                    this.eventTypeRepository = new EventTypeRepository(context);
                }
                return this.eventTypeRepository;
            }
        }

        private EventRepository eventRepository;

        public EventRepository EventRepository
        {
            get
            {
                if (this.eventRepository == null)
                {
                    this.eventRepository = new EventRepository(context);
                }
                return this.eventRepository;
            }
        }


        public void Save()
        {
            context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}