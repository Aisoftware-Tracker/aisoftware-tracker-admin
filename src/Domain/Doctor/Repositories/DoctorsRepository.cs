using System.Collections.Generic;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Doctor.Repositories;

namespace Aisoftware.Tracker.Admin.Domain.Doctor.Repositories
{
    public class DoctorsRepository : IDoctorsRepository
    {
        public DoctorsRepository()
        {

        }

        public async Task<List<Doctors>> Index()
        {
            throw new System.NotImplementedException();
        }
        public async Task<Doctors> Create(Doctors doctor)
        {
            throw new System.NotImplementedException();
        }
        public async Task<Doctors> Update(Doctors doctor)
        {
            throw new System.NotImplementedException();
        }
        public async Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
