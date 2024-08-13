using Kisanmitra.API.Repository.Interface;
using DataAccessLayer.DAL;
using Microsoft.EntityFrameworkCore;

namespace Kisanmitra.API.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Gets the query repository for executing raw SQL queries.
        /// </summary>
        public IQuery Query { get; }

        /// <summary>
        /// Gets the farmer equipment repository for accessing farmer equipment data.
        /// </summary>
        public IFarmerEquipment FarmerEquipment { get; }

        /// <summary>
        /// Gets the farmer library resource repository for managing farmer library resources.
        /// </summary>
        public IFarmerLibraryResourceRepository FarmerLibraryResourceRepository { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class with the specified application context.
        /// Instantiates repositories to be used within the unit of work.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <exception cref="ArgumentNullException">Thrown when the provided context is null.</exception>
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(_context));
            FarmerLibraryResourceRepository = new FarmerLibraryResourceRepository(_context);
            Query = new QueryRepo(_context);
            FarmerEquipment = new FarmerEquipmentRepo(_context);
        }

        /// <summary>
        /// Commits all changes made within the unit of work to the database.
        /// Ensures that all repository operations are saved as a single transaction.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
