namespace Kisanmitra.API.Repository.Interface
{
    public interface IUnitOfWork
    {
        IConsultant Consultant { get; }

        void Save();
    }
}
