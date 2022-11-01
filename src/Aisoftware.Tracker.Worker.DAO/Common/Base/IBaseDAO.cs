using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aisoftware.Tracker.Borders;
using System.Net.Http.Headers;

namespace Aisoftware.Tracker.Worker.DAO.Common.Base;

public interface IBaseDAO<T> where T : class
{
    Task<IEnumerable<T>> FindAll();
    Task<T> FindById(int id);
    Task Save(T request);
    Task<T> Update(T request);
    // Task<HttpResponseMessage> Delete(int id, string endpoint);
}
