using DataAccessLayer.DAL;
using Kisanmitra.API.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Serilog;
using System.Linq.Expressions;


namespace Kisanmitra.API.Repository.Implementations
{
    public class ConsultantLanguageRepo : IConsultantLanguage
    {
        private readonly ApplicationDbContext _dbContext;

        public ConsultantLanguageRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<TbConsultantLanguage>> GetAllConsultantLanguage(int page, int pageSize)
        {
            try
            {
                var parameters = new List<SqlParameter>
                { new SqlParameter("@PageNumber", page),
                    new SqlParameter("@pageSize", pageSize)
                };


                return await _dbContext.TbConsultantLanguages.FromSqlRaw("EXEC sp_GetAllConsultantLanguages @PageNumber, @PageSize", parameters.ToArray())
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured while fetching consultant language");
                throw;
            }
        }

            public async Task<IEnumerable<TbConsultantLanguage>> GetConsultantLanguageById(string consultantId)
            {
                try
                {
                    var parameters = new List<SqlParameter>
                {
                    new SqlParameter("consultantId", consultantId)
                };

                    return await _dbContext.TbConsultantLanguages
                                           .FromSqlRaw("EXEC sp_GetConsultantLanguageById @ConsultantId", parameters.ToArray())
                                           .ToListAsync();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occured while fetching consultant Language");
                    throw;
                }
            }
        
        public async Task<TbConsultantLanguage> InsertConsultantLanguage(TbConsultantLanguage consultantLanguage)
        {
            try
            {
                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@consultant_id",consultantLanguage.ConsultantId),
            new SqlParameter("@consultant_language",consultantLanguage.ConsultantLanguage),
            new SqlParameter("@inserted_by",consultantLanguage.InsertedBy),
            new SqlParameter("@inserted_date",consultantLanguage.InsertedDate),
            new SqlParameter("@updated_by",consultantLanguage.UpdatedBy),
            new SqlParameter("@updated_date",consultantLanguage.UpdatedDate ) 
        };

                await _dbContext.Database
                                 .ExecuteSqlRawAsync("EXEC sp_SingleInsertConsultantLanguage @consultant_id, @consultant_language, @inserted_by, @inserted_date, @updated_by, @updated_date", parameters.ToArray());

                return consultantLanguage; 
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while inserting consultant Language");
                throw;
            }
        }



        public async Task<TbConsultantLanguage> UpdateConsultantLanguage(string id, string language, TbConsultantLanguage consultantLanguage)
        {
            try
            {
                var parameters = new List<SqlParameter> {
            new SqlParameter("@consultant_id", consultantLanguage.ConsultantId),
            new SqlParameter("@consultant_language", consultantLanguage.ConsultantLanguage),
            new SqlParameter("@updated_by", consultantLanguage.UpdatedBy),
            new SqlParameter("@updated_date", consultantLanguage.UpdatedDate) 
        };

                await _dbContext.Database
                                 .ExecuteSqlRawAsync("EXEC sp_SingleUpdateConsultantLanguage @consultant_id, @consultant_language, @updated_by, @updated_date", parameters.ToArray());

                return consultantLanguage;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating consultant language");
                throw;
            }
        }


     public async Task<int> DeleteConsultantLanguage(string id, string language)
    {
    try
    {
        var parameters = new List<SqlParameter>
        {
            new SqlParameter("@consultantId", id),
            new SqlParameter("@consultantLanguage", language)
        };

        int rowsAffected = await _dbContext.Database
                                          .ExecuteSqlRawAsync("EXEC sp_SingleDeleteConsultantLanguage @consultantId, @consultantLanguage", parameters.ToArray());

        return rowsAffected; 
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred while deleting consultant Language");
        throw;
    }
    }

       

       
    }
}
