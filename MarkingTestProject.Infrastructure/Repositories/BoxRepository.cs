using MarkingTestProject.Domain.Entities;
using MarkingTestProject.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarkingTestProject.Infrastructure.Repositories
{
    public class BoxRepository<T> : BaseRepository<T>, IBoxRepository<T>
        where T : Box
    {
        public BoxRepository(TestProjDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Box>> GetAllByGtinAsync(string gtin, CancellationToken cancellationToken)
        {
            var boxes = await _context.Boxes
                .Where(p => p.Code.Substring(2, 14) == gtin)
                .ToListAsync(cancellationToken);

            return boxes;
        }
    }
}
