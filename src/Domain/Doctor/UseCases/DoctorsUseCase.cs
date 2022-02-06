using System.Collections.Generic;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Doctor.Repositories;

namespace Aisoftware.Tracker.Admin.Domain.Doctor.UseCases
{
    public class DoctorsUseCase : IDoctorsUseCase
    {
        public List<Doctors> Index()
        {
            Doctors dr = new Doctors
            {
                Id = 1,
                Name = "Dr. Nome",
                Email = "dr@email.com",
                Specialist = "O Cara",
                Gender = 2
            };

            var doctors = new List<Doctors>();
            doctors.Add(dr);

            return doctors;

        }

        public Doctors Find(int id)
        {
            return new Doctors
            {
                Id = id,
                Name = "Dr. Nome",
                Email = "dr@email.com",
                Specialist = "O Cara",
                Gender = 2
            };
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
