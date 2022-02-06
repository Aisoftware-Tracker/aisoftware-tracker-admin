using System.Collections.Generic;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Models;

namespace Aisoftware.Tracker.Admin.Domain.Doctor.UseCases
{
    public interface IDoctorsUseCase
    {
        List<Doctors> Index();
        Doctors Find(int id);
        Task<Doctors> Create(Doctors doctor);
        Task<Doctors> Update(Doctors doctor);

        /// <summary>
        /// Close the Doctors
        /// </summary>
        /// <returns>
        /// 204 - No Content
        /// </returns>
        Task Delete(int id);
    }
}
