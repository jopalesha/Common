using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Hosting.Test.Sample.Components;
using MediatR;

namespace Jopalesha.Common.Hosting.Test.Sample.Handlers
{
    public class DeleteRequestHandler : AsyncRequestHandler<DeleteRequest>
    {
        private readonly IValueStorage _valueStorage;

        public DeleteRequestHandler(IValueStorage valueStorage)
        {
            _valueStorage = valueStorage;
        }

        protected override Task Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            _valueStorage.Remove();
            return Task.CompletedTask;
        }
    }
}
