using System;
using System.Linq;
using MediatR;
using MediatR.Pipeline;
using SimpleInjector;

namespace Jopalesha.Common.Application.Mediator
{
    public static class ContainerExtensions
    {
        public static void AddHandler<THandler, TRequest>(this Container container)
            where THandler : IRequestHandler<TRequest>
            where TRequest : IRequest<Unit> =>
            container.AddHandler<THandler, TRequest, Unit>();

        public static void AddHandler<T, TRequest, TResponse>(this Container container)
            where T : IRequestHandler<TRequest, TResponse>
            where TRequest : IRequest<TResponse> =>
            container.Register(typeof(IRequestHandler<TRequest, TResponse>), typeof(T), Lifestyle.Scoped);

        public static void AddMediator(this Container container)
        {
            container.RegisterSingleton<IMediator, MediatR.Mediator>();
            container.Collection.Register(typeof(IPipelineBehavior<,>), Enumerable.Empty<Type>());
            container.Collection.Register(typeof(IRequestPreProcessor<>), Enumerable.Empty<Type>());
            container.Collection.Register(typeof(IRequestPostProcessor<,>), Enumerable.Empty<Type>());
            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);

            container.RegisterDecorator(typeof(IRequestHandler<,>),
                typeof(ThreadScopedMemberServiceDecorator<,>));
        }
    }
}
