using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, 
        ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(LeaveManagementDbContext leaveManagementDbContext)
            :base(leaveManagementDbContext)
        {
            
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            return await _leaveManagementDbContext.LeaveAllocations
                .Include(a=> a.LeaveType)
                .FirstAsync(e=>e.Id.Equals(id));
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            return await _leaveManagementDbContext.LeaveAllocations
                .Include(a => a.LeaveType)
                .ToListAsync();
        }
    }
}
