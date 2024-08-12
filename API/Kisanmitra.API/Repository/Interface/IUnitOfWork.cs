namespace Kisanmitra.API.Repository.Interface
{
    public interface IUnitOfWork
    {
        IConsultantLanguage ConsultantLanguage { get; }
        void save();
    }
}
