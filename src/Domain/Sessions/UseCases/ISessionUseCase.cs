using System.Collections.Generic;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Models;

namespace Aisoftware.Tracker.Admin.Domain.Sessions.UseCases
{
    public interface ISessionUseCase
    {
        /// <summary>
        /// Fetch Session information
        /// </summary>
        /// <returns>
        /// Session
        /// </returns>
        Task<Session> Find();

        /// <summary>
        /// Create a new Session
        /// </summary>
        /// <returns>
        /// Session
        /// </returns>
        Task<Session> Create(Login login);
    
        /// <summary>
        /// Close the Session
        /// </summary>
        /// <returns>
        /// boolean
        /// </returns>
        Task Delete();
        
        /// <summary>
        /// <para/>Fetch Cookies
        /// </summary>
        string GetCookieValue();
    }
}
