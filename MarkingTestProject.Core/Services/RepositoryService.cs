using MarkingTestProject.Core.DTOS;
using MarkingTestProject.Core.Interfaces;
using MarkingTestProject.Domain.Entities;
using MarkingTestProject.Infrastructure.Interfaces;

namespace MarkingTestProject.Core.Services
{
    public class RepositoryService : IRepositoryService
    {
        IBottleRepository<Bottle> _bottleRepository;
        IBoxRepository<Box> _boxRepository;
        IPalletRepository<Pallet> _palletRepository;
        IDTOConversionService _conversionService;

        //public async Task Import(IEnumerable<PalletDTO> pallets, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        foreach (var palletDTO in pallets)
        //        {
        //            var pallet = new Pallet
        //            {
        //                Code = palletDTO.Code,
        //            };

        //            pallet = await _palletRepository.AddAsync(pallet, cancellationToken);

        //            foreach (var boxDTO in palletDTO.Boxes)
        //            {
        //                var box = new Box
        //                {
        //                    Code = boxDTO.Code,
        //                    PalletId = pallet.Id
        //                };

        //                box = await _boxRepository.AddAsync(box, cancellationToken);


        //                foreach (var bottleDTO in boxDTO.Bottles)
        //                {
        //                    var bottle = new Bottle
        //                    {
        //                        Code = bottleDTO.Code,
        //                        BoxId = box.Id
        //                    };

        //                    await _bottleRepository.AddAsync(bottle, cancellationToken);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}
        public async Task Import(IEnumerable<PalletDTO> pallets, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                var palletsList = pallets.Select(x => new Pallet
                {
                    Code = x.Code,
                    Boxes = x.Boxes.Select(b => new Box
                    {
                        Code = b.Code,
                        Bottles = b.Bottles.Select(bb => new Bottle
                        {
                            Code = bb.Code
                        })
                        .ToList()
                    }).ToList()
                }).ToList();

                foreach (var pallet in palletsList)
                {
                    _palletRepository.AddAsync(pallet, cancellationToken).Wait();
                }
            }, cancellationToken);
        }

        public async Task DeleteAll(CancellationToken cancellationToken)
        {
            await _bottleRepository.DeleteAllAsync(cancellationToken);
            await _boxRepository.DeleteAllAsync(cancellationToken);
            await _palletRepository.DeleteAllAsync(cancellationToken);
        }

        public async Task<IEnumerable<PalletDTO>> GetAllPalletsByGtin(string gtin, CancellationToken cancellationToken)
        {
            var result = await _palletRepository.GetAllByGtinAsync(gtin, cancellationToken);

            return result.Select(_conversionService.Convert);
        }

        public async Task<IEnumerable<BoxDTO>> GetAllBoxesByGtin(string gtin, CancellationToken cancellationToken)
        {
            var result = await _boxRepository.GetAllByGtinAsync(gtin, cancellationToken);

            return result.Select(_conversionService.Convert);
        }

        public async Task<IEnumerable<BottleDTO>> GetAllBottlesByGtin(string gtin, CancellationToken cancellationToken)
        {
            var result = await _bottleRepository.GetAllByGtinAsync(gtin, cancellationToken);

            return result.Select(_conversionService.Convert);
        }

        public async Task<IEnumerable<PalletDTO>> GetAllDataByGtinAsync(string gtin, CancellationToken cancellationToken)
        {
            return (await _palletRepository.GetAllWithReferencesByGtinAsync(gtin, cancellationToken)).Select(_conversionService.Convert);
        }


        public RepositoryService(IDTOConversionService dTOConversionService, IBottleRepository<Bottle> bottleRepository, IBoxRepository<Box> boxRepository, IPalletRepository<Pallet> palletRepository)
        {
            _conversionService = dTOConversionService;
            _bottleRepository = bottleRepository;
            _boxRepository = boxRepository;
            _palletRepository = palletRepository;
        }
    }
}
