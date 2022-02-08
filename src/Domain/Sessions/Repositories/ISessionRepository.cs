using System.Collections.Generic;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Models;
using System.Net;

namespace Aisoftware.Tracker.Admin.Domain.Sessions.Repositories
{
    public interface ISessionRepository
    {
        /// <summary>
        /// Fetch Session information
        /// </summary>
        /// <returns>
        /// 200 - Ok
        /// </returns>
        Task<Session> Find();

        /// <summary>
        /// Create a new Session
        /// </summary>
        /// <returns>
        /// 200 - Ok
        /// </returns>
        Task<Session> Create(Dictionary<string, string> request);

        /// <summary>
        /// Close the Session
        /// </summary>
        /// <returns>
        /// 204 - No Content
        /// </returns>
        Task Delete();

        /// <summary>
        /// <para/>Fetch Cookies
        /// </summary>
        Cookie GetCookie();
    }
}
