using HR.LeaveManagement.Application.Persistence.Contracts;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LeaveManagementDbContext _context;
        private ILeaveTypeRepository? _leaveTypeRepository;
        private ILeaveRequestRepository? _leaveRequestRepository;
        private ILeaveAllocationRepository? _leaveAllocationRepository;

        public UnitOfWork(LeaveManagementDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }


        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }


        public ILeaveTypeRepository LeaveTypeRepository => _leaveTypeRepository ??= new LeaveTypeRepository(_context);
        public ILeaveRequestRepository LeaveRequestRepository => _leaveRequestRepository ??= new LeaveRequestRepository(_context);
        public ILeaveAllocationRepository LeaveAllocationRepository => _leaveAllocationRepository ??= new LeaveAllocationRepository(_context);
    }
}
