using System;
using HelpLine.BuildingBlocks.Bus.Queue;
using HelpLine.BuildingBlocks.Domain;
using HelpLine.BuildingBlocks.Infrastructure.Serialization;
using Newtonsoft.Json;

namespace HelpLine.BuildingBlocks.Infrastructure.InternalCommands
{
    public class InternalCommandsQueue<TInternalCommand> : IInternalCommandsQueue
        where TInternalCommand : InternalCommandTaskBase, new()
    {
        private readonly IQueue _queue;
        private readonly IUnitOfWork _unitOfWork;

        public InternalCommandsQueue(IQueue queue, IUnitOfWork unitOfWork)
        {
            _queue = queue;
            _unitOfWork = unitOfWork;
        }

        public void Add<T>(Guid id, T command)
        {
            var cmd = new TInternalCommand
            {
                Id = id,
                EnqueueDate = DateTime.UtcNow,
                Type = command.GetType().FullName,
                Data = JsonConvert.SerializeObject(command, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver(),
                    TypeNameHandling = TypeNameHandling.All
                }),
            };
            if (_unitOfWork.Transaction)
                _unitOfWork.OnCommit += async () => _queue.Add(cmd);
            else
                _queue.Add(cmd);
        }

        public void StartConsuming<T>(InternalCommandTaskHandlerBase<T> handler) where T : InternalCommandTaskBase
        {
            _queue.StartConsuming();
            _queue.AddHandler(handler);
        }
    }
}
