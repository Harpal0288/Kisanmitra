using Models.Entities;
namespace Kisanmitra.API.Repository.Interface
{
    public interface IQuery
    {
        Task<List<TbQuery>> GetAllQueries(int page, int pageSize);
        Task InsertQuery(TbQuery query);
        Task UpdateQuery(TbQuery query);
        Task DeleteQuery(string queryId);
        Task<List<TbQuery>> GetQueriesByFarmerId(string farmerId);
    }
}
