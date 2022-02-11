using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Drivers.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Base.UseCases;

namespace Aisoftware.Tracker.Admin.Domain.Drivers.UseCases
{
    public class DriverUseCase : IDriverUseCase
    {
        private readonly IBaseRepository<Driver> _repository;

        public DriverUseCase(IDriverRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Driver>> FindAll()
        {
            IEnumerable<Driver> response = await _repository.FindAll(Endpoints.DRIVERS);

            return response.OrderBy(driver => driver.Id);
        }

        public async Task<Driver> FindById(int id)
        {
            return await _repository.FindById(id, Endpoints.DRIVERS);
        }

        public async Task<Driver> Save(Driver request)
        {
            return await _repository.Save(request, Endpoints.DRIVERS);
        }

        public async Task<Driver> Update(Driver driver)
        {
            Driver response = await _repository.FindById(driver.Id, Endpoints.DRIVERS);

            Driver request = new Driver
            {
                Id = driver.Id,
                Attributes = driver.Attributes ?? response.Attributes,
                Name = driver.Name ?? response.Name,
                UniqueId = driver.UniqueId ?? response.UniqueId,
                Photo = driver.Photo ?? response.Photo,
                Document = driver.Document ?? response.Document,
                DocumentValidAt = driver.DocumentValidAt ?? response.DocumentValidAt
            };

            return await _repository.Update(request, Endpoints.DRIVERS);

        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id, Endpoints.DRIVERS);
        }

    }
}
