using Jopalesha.Common.Hosting.Test.Sample.Components;
using MediatR;

namespace Jopalesha.Common.Hosting.Test.Sample.Handlers
{
    public class PutRequestHandler : RequestHandler<PutRequest>
    {
        private readonly IValueStorage _storage;
        private readonly IHistoryStorage _historyStorage;

        public PutRequestHandler(
            IValueStorage storage,
            IHistoryStorage historyStorage)
        {
            _storage = storage;
            _historyStorage = historyStorage;
        }

        protected override void Handle(PutRequest request)
        {
            _storage.Set(request.Value);
            _historyStorage.Add(request.Value);
        }
    }
}