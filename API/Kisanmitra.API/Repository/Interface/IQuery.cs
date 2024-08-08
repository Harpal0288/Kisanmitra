using Models.Entities;
namespace Kisanmitra.API.Repository.Interface
{
    public interface IQuery
    {
        List<TbQuery> GetAllQueries();

    }
}
