using MarkingTestProject.Domain.Entities;
using MarkingTestProject.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarkingTestProject.Infrastructure.Repositories
{
    public class BottleRepository<T> : BaseRepository<T>, IBottleRepository<T>
        where T : Bottle
    {
        public BottleRepository(TestProjDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Bottle>> GetAllByGtinAsync(string gtin, CancellationToken cancellationToken)
        {
            var bottles = await _context.Bottles
                .Where(p => p.Code.Substring(2, 14) == gtin)
                .ToListAsync(cancellationToken);

            return bottles;
        }
    }
}
