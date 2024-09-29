using MarkingTestProject.Domain.Entities;
using MarkingTestProject.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarkingTestProject.Infrastructure.Repositories
{
    public class PalletRepository<T> : BaseRepository<T>, IPalletRepository<T>
        where T : Pallet
    {
        public PalletRepository(TestProjDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Pallet>> GetAllByGtinAsync(string gtin, CancellationToken cancellationToken)
        {
            var pallets = await _context.Pallets
                .Where(p => p.Code.Substring(2, 14) == gtin)
                .ToListAsync(cancellationToken);

            return pallets;
        }

        public async Task<IEnumerable<Pallet>> GetAllWithReferencesByGtinAsync(string gtin, CancellationToken cancellationToken)
        {
            var pallets = await _context.Pallets
                .Include(b => b.Boxes)
                .ThenInclude(b => b.Bottles)
                .Where(p => p.Code.Substring(2, 14) == gtin)
                .ToListAsync(cancellationToken);

            return pallets;
        }
    }
}
