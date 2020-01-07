using App.Interfaces;
using System;
using System.Threading;

namespace App.Managers
{
    public class ServiceContext : MarshalByRefObject
    {
        private static AsyncLocal<ServiceContext> CurrentServiceContext = new AsyncLocal<ServiceContext>();
        private readonly ServiceContext previousContext;
        public CorrelationContext _correlationContext { get; }

        public static ServiceContext CurrentContext
        {
            get { return CurrentServiceContext.Value; }
        }

        private ServiceContext(ServiceContext previous)
        {
            previousContext = previous;
            _correlationContext = previous?._correlationContext;
        }

        private ServiceContext(ServiceContext previous, CorrelationContext correlationContext)
            : this(previous)
        {
            this._correlationContext = correlationContext;
        }

        internal static IDisposable Push(CorrelationContext correlationContext)
        {
            var currentContext = CurrentContext;
            var newContext = new ServiceContext(currentContext, correlationContext);
            return Push(newContext);
        }

        private static IDisposable Push(ServiceContext context)
        {
            var currentContext = CurrentContext;
            if (object.ReferenceEquals(context, currentContext))
                return VoidDisposable.Instance;

            CurrentServiceContext.Value = context;

            return new ActionDisposable(() =>
            {
                CurrentServiceContext.Value = currentContext;
            });
        }
    }

    public sealed class ActionDisposable : IDisposable
    {
        public static ActionDisposable Instance { get; }
        public ActionDisposable(Action action) { }
        
        public void Dispose() { }
    }

    public sealed class VoidDisposable : IDisposable
    {
        public static VoidDisposable Instance { get; }

        public void Dispose() { }
    }
}
