using Kisanmitra.API.Repository.Interface;
using DataAccessLayer.DAL;
using Microsoft.EntityFrameworkCore;

namespace Kisanmitra.API.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ApplicationDbContext _context;


        {
        }

        public void Save()
        {
        }
    }
}

