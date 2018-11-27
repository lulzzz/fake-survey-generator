using System;
using System.Threading.Tasks;
using FakeSurveyGenerator.Domain.AggregatesModel.AuditAggregate;
using FakeSurveyGenerator.Domain.SeedWork;

namespace FakeSurveyGenerator.Infrastructure.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly SurveyContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public AuditRepository(SurveyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Audit Add(Audit audit)
        {
            return _context.Audits.Add(audit).Entity;
        }

        public async Task<Audit> GetAsync(int auditId)
        {
            return await _context.Audits.FindAsync(auditId);
        }
    }
}
