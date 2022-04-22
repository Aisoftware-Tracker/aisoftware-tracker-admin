using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Drivers.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using System.Net.Http;

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

            foreach (var item in response)
            {
                if (!string.IsNullOrEmpty(item.DocumentValidAt))
                {
                    item.DocumentValidAt = Convert.ToDateTime(item.DocumentValidAt).ToString(FormatString.FORMAT_DATE_BR);
                }
            }

            return response.OrderBy(driver => driver.Id);
        }

        public async Task<Driver> FindById(int id)
        {
            var driver = await _repository.FindById(id, Endpoints.DRIVERS);
            driver.DocumentValidAt = $"{driver.DocumentValidAt}{FormatString.FORMAT_TIME_00}";
            
            return driver;
        }

        public async Task<Driver> Save(Driver request)
        {
            request.Attributes = request.Attributes ?? new object();
            request.Photo = request.Photo ?? string.Empty;
            request.UniqueId = Guid.NewGuid().ToString();
            request.DocumentValidAt = request.DocumentValidAt.Remove(10, 6);
            
            return await _repository.Save(request, Endpoints.DRIVERS);
        }

        public async Task<Driver> Update(Driver driver)
        {
            Driver response = await _repository.FindById(driver.Id, Endpoints.DRIVERS);

            driver.DocumentValidAt = driver.DocumentValidAt.Substring(0, 10);

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

        public async Task<HttpResponseMessage> Delete(int id)
        {
            return await _repository.Delete(id, Endpoints.DRIVERS);
        }

    }
}
