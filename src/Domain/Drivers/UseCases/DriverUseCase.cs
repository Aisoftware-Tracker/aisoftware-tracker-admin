using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Drivers.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;

namespace Aisoftware.Tracker.Admin.Domain.Drivers.UseCases
{
    public class DriverUseCase : IDriverUseCase
    {
        private readonly IBaseRepository<Driver> _repository;
        private const string FORMAT_DATE_BR = "dd/MM/yyyy";
        private const string FORMAT_TIME_00 = "T00:00";

        public DriverUseCase(IDriverRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Driver>> FindAll()
        {
            IEnumerable<Driver> response = await _repository.FindAll(Endpoints.DRIVERS);

            foreach (var item in response)
            {
                item.DocumentValidAt = Convert.ToDateTime(item.DocumentValidAt).ToString(FORMAT_DATE_BR);
            }

            return response.OrderBy(driver => driver.Id);
        }

        public async Task<Driver> FindById(int id)
        {
            var driver = await _repository.FindById(id, Endpoints.DRIVERS);
            driver.DocumentValidAt = $"{driver.DocumentValidAt}{FORMAT_TIME_00}";
            return driver;
        }

        public async Task<Driver> Save(Driver request)
        {
            return await _repository.Save(request, Endpoints.DRIVERS);
        }

        public async Task<Driver> Update(Driver driver)
        {
            Driver response = await _repository.FindById(driver.Id, Endpoints.DRIVERS);

            driver.DocumentValidAt = driver.DocumentValidAt.Replace(FORMAT_TIME_00, string.Empty);

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

            return await _repository.Update(request, $"{Endpoints.DRIVERS}/{driver.Id}");

        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id, Endpoints.DRIVERS);
        }

    }
}
