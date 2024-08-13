
namespace Kisanmitra.API.Repository.Interface
{
    /// <summary>
    /// Represents a unit of work pattern for managing repository transactions.
    /// Provides access to various repositories and a method to commit changes.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the query repository for executing raw SQL queries.
        /// </summary>
        IQuery Query { get; }

        /// <summary>
        /// Gets the farmer equipment repository for accessing farmer equipment data.
        /// </summary>
        IFarmerEquipment FarmerEquipment { get; }

        /// <summary>
        /// Gets the farmer library resource repository for managing farmer library resources.
        /// </summary>
        IFarmerLibraryResourceRepository FarmerLibraryResourceRepository { get; }

        /// <summary>
        /// Commits all changes made within the unit of work.
        /// This method saves all pending changes to the database.
        /// </summary>
        void Save();
    }
}
