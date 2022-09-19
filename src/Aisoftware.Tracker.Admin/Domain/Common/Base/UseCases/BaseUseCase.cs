using Aisoftware.Tracker.Repositories.Base;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Devices.UseCases
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

        public virtual async Task<T> Update(T content)
        {
            T response = await _repository.FindById(GetIdValue(content), _endpoint);

            var request = this.SetSourceValueIfContentValueNull(content, response);

            return await _repository.Update(request, _endpoint);

        }

        ///TODO Remover metodo duplicado
        private int GetIdValue(T content)
        {
            var id = 0;
            var type = content.GetType();
            var property = type.GetProperty("Id");
            if (!(property is null))
            {
                id = (int)property.GetValue(content);
            }
            return id;
        }

        public virtual T SetSourceValueIfContentValueNull(T content, T source)
        {
            var typeContent = content.GetType();
            var typeSourse = source.GetType();
            var response = content;

            foreach (var property in typeContent.GetProperties())
            {
                var valueContent = property.GetValue(content);
                var valueSource = property.GetValue(source);

                if (valueContent is null)
                {
                    property.SetValue(response, valueSource);
                }
            }

            return response;
        }
    }
}
