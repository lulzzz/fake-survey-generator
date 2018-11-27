using System.Threading.Tasks;
using FakeSurveyGenerator.Domain.SeedWork;

namespace FakeSurveyGenerator.Domain.AggregatesModel.AuditAggregate
{
    public interface IAuditRepository : IRepository<Audit>
    {
        Audit Add(Audit audit);

        Task<Audit> GetAsync(int auditId);
    }
}
