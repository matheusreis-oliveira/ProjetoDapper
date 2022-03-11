using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace ProjetoDapper.Repositories
{
    //so aceito criação de repository<TModel>(TModel = tipo generico) where(aonde) TModel : class (é igual a uma classe) pois os models são classes
    public class Repository<TModel> where TModel : class
    {
        private readonly SqlConnection _connection;

        public Repository(SqlConnection connection)
        => _connection = connection;

        public IEnumerable<TModel> GetAll()
        => _connection.GetAll<TModel>();

        public TModel Get(int id)
        => _connection.Get<TModel>(id);

        public void Create(TModel model)
        => _connection.Insert<TModel>(model);

        public void Update(TModel model)
        => _connection.Update<TModel>(model);

        public void Delete(TModel model)
        => _connection.Delete<TModel>(model);

        public void Delete(int id)
        {
            var model = _connection.Get<TModel>(id);
            _connection.Delete<TModel>(model);
        }
    }
}