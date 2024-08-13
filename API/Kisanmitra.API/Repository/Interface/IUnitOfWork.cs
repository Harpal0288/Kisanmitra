namespace Kisanmitra.API.Repository.Interface
{
    public interface IUnitOfWork
    {
        IQuery Query { get; }
        IConsultantLanguage ConsultantLanguage { get; }
        void save();
    }
}
