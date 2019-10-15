using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Jopalesha.Common.Application.Mediator
{
    public class ThreadScopedMemberServiceDecorator<T, TU> : IRequestHandler<T, TU> 
        where T : IRequest<TU>
    {
        private readonly Func<IRequestHandler<T, TU>> _decorateFactory;
        private readonly Container _container;

        public ThreadScopedMemberServiceDecorator(
            Func<IRequestHandler<T, TU>> decorateFactory,
            Container container)
        {
            _decorateFactory = decorateFactory;
            _container = container;
        }

        public async Task<TU> Handle(T request, CancellationToken cancellationToken)
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                return await _decorateFactory().Handle(request, cancellationToken);
            }
        }
    }
}
