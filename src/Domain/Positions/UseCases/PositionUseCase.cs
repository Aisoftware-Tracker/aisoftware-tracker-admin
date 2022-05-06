using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Positions.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using System.Net.Http;
using Aisoftware.Tracker.Admin.Domain.Devices.UseCases;

namespace Aisoftware.Tracker.Admin.Domain.Positions.UseCases
{
    public class PositionUseCase : BaseUseCase<Position>, IPositionUseCase
    {
        private readonly IBaseRepository<Position> _repository;

        public PositionUseCase(IPositionRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task<Position> Update(Position content)
        {
            Position response = await _repository.FindById(content.Id, Endpoints.POSITIONS);

            Position request = new Position
            {
                Id = content.Id,
                Attributes = content.Attributes ?? response.Attributes,
                DeviceId = content.DeviceId == 0 ? response.DeviceId : content.DeviceId,
                Type = content.Type ?? response.Type,
                Protocol = content.Protocol ?? response.Protocol,
                ServerTime = content.ServerTime ?? response.ServerTime,
                DeviceTime = content.DeviceTime ?? response.DeviceTime,
                FixTime = content.FixTime ?? response.FixTime,
                OutDated = content.OutDated,
                Valid = content.Valid,
                Latitude = content.Latitude == 0 ? response.Latitude : content.Latitude,
                Longitude = content.Longitude == 0 ? response.Longitude : content.Longitude,
                Altitude = content.Altitude == 0 ? response.Altitude : content.Altitude,
                Speed = content.Speed == 0 ? response.Speed : content.Speed,
                Course = content.Course == 0 ? response.Course : content.Course,
                Address = content.Address ?? response.Address,
                Accuracy = content.Accuracy == 0 ? response.Accuracy : content.Accuracy,
                Network = content.Network ?? response.Network
            };

            return await _repository.Update(request, Endpoints.POSITIONS);

        }
    }
}
