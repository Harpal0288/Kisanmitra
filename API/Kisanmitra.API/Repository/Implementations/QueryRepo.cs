using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Entities;


namespace Kisanmitra.API.Repository.Implementations
{
    public class QueryRepo : IQuery
    {
        private readonly ApplicationDbContext _context;

        public QueryRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<TbQuery>> GetAllQueries(int page = 1, int pageSize = 10)
        {
            var pageNumPara = new SqlParameter("@PageNumber", page);
            var pageSizePara = new SqlParameter("@PageSize", pageSize);

            return await _context.TbQueries
                .FromSqlRaw("EXEC sp_GetAllQueries @PageNumber, @PageSize", pageNumPara, pageSizePara)
                .ToListAsync();
        }

        public async Task InsertQuery(TbQuery query)
        {
            var parameters = new[]
            {
                new SqlParameter("@query_id", query.QueryId),
                new SqlParameter("@category_id", query.CategoryId),
                new SqlParameter("@farmer_id", query.FarmerId),
                new SqlParameter("@query_title", query.QueryTitle),
                new SqlParameter("@query_description", query.QueryDescription),
                new SqlParameter("@inserted_by", query.InsertedBy),
                new SqlParameter("@updated_by", query.UpdatedBy)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_SingleInsertQuery @query_id, @category_id, @farmer_id, @query_title, @query_description, @inserted_by, @updated_by",
                parameters
            );
        }

        public async Task UpdateQuery(TbQuery query)
        {
            var parameters = new[]
            {
                new SqlParameter("@query_id", query.QueryId),
                new SqlParameter("@category_id", query.CategoryId ?? (object)DBNull.Value),
                new SqlParameter("@farmer_id", query.FarmerId ?? (object)DBNull.Value),
                new SqlParameter("@query_title", query.QueryTitle ?? (object)DBNull.Value),
                new SqlParameter("@query_description", query.QueryDescription ?? (object)DBNull.Value),
                new SqlParameter("@updated_by", query.UpdatedBy)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_SingleUpdateQuery @query_id, @category_id, @farmer_id, @query_title, @query_description, @updated_by",
                parameters
            );
        }

        public async Task DeleteQuery(string queryId)
        {
            var parameter = new SqlParameter("@query_id", queryId);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_SingleDeleteQuery @query_id", parameter);
        }

        public async Task<List<TbQuery>> GetQueriesByFarmerId(string farmerId)
        {
            var parameter = new SqlParameter("@farmerid", farmerId);
            return await _context.TbQueries.FromSqlRaw("EXEC sp_QueryByFarmer @farmerid", parameter).ToListAsync();
        }
    }
}