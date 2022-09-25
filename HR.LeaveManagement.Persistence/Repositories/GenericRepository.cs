using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : BaseDomainEntity
    {
        protected readonly LeaveManagementDbContext _leaveManagementDbContext;

        public GenericRepository(LeaveManagementDbContext leaveManagementDbContext)
        {
            _leaveManagementDbContext = leaveManagementDbContext;
        }

        public async Task<T?> Get(int id)
        {
            return await _leaveManagementDbContext
                .Set<T>()
                .FirstOrDefaultAsync(e=> e.Id.Equals(id));
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _leaveManagementDbContext
                .Set<T>()
                .ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _leaveManagementDbContext.FindAsync<T>(id) is not null;
        }

        public async Task<T> Add(T entity)
        {
            await _leaveManagementDbContext.AddAsync(entity);
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _leaveManagementDbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<T> Delete(T entity)
        {
            _leaveManagementDbContext.Set<T>().Remove(entity);
            return entity;
        }
    }
}
