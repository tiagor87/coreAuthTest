using System;

namespace domain.Models
{
    public abstract class Disposable : IDisposable
    {
        protected bool disposed = false;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Disposable()
        {
            this.Dispose(false);
        }

        protected virtual void DisposeCreatedObjects()
        {
        }

        protected void Dispose(bool disposing)
        {
            if (this.disposed) return;

            if (disposing)
            {
                this.DisposeCreatedObjects();
            }

            this.disposed = true;
        }
    }
}