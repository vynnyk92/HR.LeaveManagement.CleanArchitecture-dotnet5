using System.Threading.Tasks;
using HR.LeaveManagement.Application.Models;

namespace HR.LeaveManagement.Application.Infrastructure.Contracts
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Email email);
    }
}
