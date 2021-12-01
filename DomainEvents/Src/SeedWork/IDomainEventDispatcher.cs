using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainEvents.Src.SeedWork
{
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// IDomainEvent tipindeki tüm eventleri sevk et
        /// </summary>
        /// <param name="devent"></param>
        void Dispatch(IDomainEvent devent);
    }
}
