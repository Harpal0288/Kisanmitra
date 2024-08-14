using Models.Entities;


namespace Kisanmitra.API.Repository.Interface
{
    public interface IConsultantLanguage
    {
        Task<IEnumerable<TbConsultantLanguage>> GetAllConsultantLanguage(int page,int pageSize);
        Task<IEnumerable<TbConsultantLanguage>> GetConsultantLanguageById(string id);
       
        Task<TbConsultantLanguage> InsertConsultantLanguage(TbConsultantLanguage consultantLanguage);

        Task<TbConsultantLanguage> UpdateConsultantLanguage(string id, string language, TbConsultantLanguage consultantLanguage);
        Task<int> DeleteConsultantLanguage(string id,string language);
    }
}   
