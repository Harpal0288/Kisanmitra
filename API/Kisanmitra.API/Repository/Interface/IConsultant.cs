using Models.Entities;

namespace Kisanmitra.API.Repository.Interface
{
    public interface IConsultant
    {
        List<TbConsultant> GetAllConsultants();
        void InsertConsultant(TbConsultant consultant);
        void UpdateConsultant(TbConsultant consultant);
        void DeleteConsultant(string ConsultantId);
        List<TbConsultant> GetConsultantById(string ConsultantId);
    }
}
