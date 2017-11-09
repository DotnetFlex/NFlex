using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Core.Events
{
    public interface IEventHandler
    {
    }

    public interface IEventHandler<in TEvent>:IEventHandler where TEvent:IEvent
    {
        void Handle(TEvent @event);
    }
}
