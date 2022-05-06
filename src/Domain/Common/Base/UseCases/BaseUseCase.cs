using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;

namespace Aisoftware.Tracker.Admin.Domain.Devices.UseCases
{
    abstract public class BaseUseCase<T> where T : class 
    {
        private readonly IBaseRepository<T> _repository;
        private string _endpoint;
        public BaseUseCase(IBaseRepository<T> repository)
        {
            _repository = repository;
            _endpoint = $"{typeof(T).Name}s".ToLower();
        }

        public virtual async Task<HttpResponseMessage> Delete(int id)
        {
            return await _repository.Delete(id, _endpoint);
        }

        public virtual async Task<IEnumerable<T>> FindAll()
        {
            IEnumerable<T> response = await _repository.FindAll(_endpoint);

            return response;

        }

        public virtual async Task<T> FindById(int id)
        {
            var response = await _repository.FindById(id, _endpoint);

            return response;
        }

        public virtual async Task<T> Save(T request)
        {
            return await _repository.Save(request, _endpoint);
        }

        public abstract Task<T> Update(T content);
        
        ///TODO - Implementar futuramente, analizando as verificacoes de regras de negocio
        // {
        //     var response = await _repository.FindById(GetIdValue(content), _endpoint);

        //     var request = GetPropValue(content, response);

        //     return await _repository.Update(request, _endpoint);        
        
        // }

        // private T GetPropValue(object content, object response) 
        // {
        //     foreach(var propContent in content.GetType().GetProperties()) 
        //     {
        //         foreach(var propResponse in response.GetType().GetProperties())
        //         {
        //             if(propContent.Name == propResponse.Name)
        //             {
        //                 propResponse.SetValue(response, propContent.GetValue(content));
        //             }
        //         }
        //     }

        //     return response as T;

        // }

        // private object GeName(object obj)
        // {
        //     foreach(var prop in obj.GetType().GetProperties()) 
        //     {
        //         if (prop.ToString() == "Id")
        //         {
        //             return prop.Name;
        //         }
        //     }
            
        //     return null;
        // }

        // private int GetIdValue(object obj)
        // {
        //     foreach(var prop in obj.GetType().GetProperties()) 
        //     {
        //         if (prop.Name.ToString() == "Id")
        //         {
        //             return Convert.ToInt32(prop.GetValue(obj, null));
        //         }
        //     }

        //     return 0;
        // }
    }
}
