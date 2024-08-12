namespace Kisanmitra.API.Repository.Interface
{
    public interface IUnitOfWork
    {
        IConsultantCertification ConsultantCertification { get; }
        void Save();
    }
}
