using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Persistence.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ILeaveRequestRepository LeaveRequestRepository { get; }

        ILeaveAllocationRepository LeaveAllocationRepository { get; }

        ILeaveTypeRepository LeaveTypeRepository { get; }

        Task Save();
    }
}
