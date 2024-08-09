using Models.Entities;
namespace Kisanmitra.API.Repository.Interface
{
    public interface IQuery
    {
        List<TbQuery> GetAllQueries();
        void InsertQuery(TbQuery query);
        void UpdateQuery(TbQuery query);
        void DeleteQuery(string queryId);
        List<TbQuery> GetQueriesByFarmerId(string farmerId);
    }
}
