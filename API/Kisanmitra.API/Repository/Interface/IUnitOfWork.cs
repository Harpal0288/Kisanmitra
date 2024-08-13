namespace Kisanmitra.API.Repository.Interface
{
    public interface IUnitOfWork
    {
        IQuery Query { get; }

        IConsultantCertification ConsultantCertification { get; }
        void Save();
    }
}
