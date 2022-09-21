using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, 
        ILeaveRequestRepository
    {
        public LeaveRequestRepository(LeaveManagementDbContext leaveManagementDbContext) 
            : base(leaveManagementDbContext)
        {
        }

        public async Task<LeaveRequest?> GetLeaveRequestWithDetails(int id)
        {
            return await _leaveManagementDbContext.LeaveRequests
                .Include(r=> r.LeaveType)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            return await _leaveManagementDbContext.LeaveRequests
                .Include(r => r.LeaveType)
                .ToListAsync();
        }

        public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? ApprovalStatus)
        {
            leaveRequest.Approved = ApprovalStatus;
            _leaveManagementDbContext.Entry(leaveRequest).State = EntityState.Modified;
            await _leaveManagementDbContext.SaveChangesAsync();
        }
    }
}
