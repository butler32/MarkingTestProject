using MarkingTestProject.Core.DTOS;
using MarkingTestProject.Core.Interfaces;
using MarkingTestProject.Domain.Entities;

namespace MarkingTestProject.Core.Services
{
    public class DTOConversionService : IDTOConversionService
    {
        public Box Convert(BoxDTO entity)
        {
            return new Box
            {
                Id = entity.Id,
                Code = entity.Code,
                Pallet = Convert(entity.Pallet),
                PalletId = entity.PalletId,
                Bottles = entity.Bottles.Select(Convert).ToList()
            };
        }

        public BoxDTO Convert(Box entity)
        {
            return new BoxDTO
            {
                Id = entity.Id,
                Code = entity.Code,
                PalletId = entity.PalletId,
                Bottles = entity.Bottles?.Select(Convert).ToList()
            };
        }

        public Pallet Convert(PalletDTO entity)
        {
            return new Pallet
            {
                Id = entity.Id,
                Code = entity.Code,
                Boxes = entity.Boxes.Select(Convert).ToList()
            };
        }

        public PalletDTO Convert(Pallet entity)
        {
            return new PalletDTO
            {
                Id = entity.Id,
                Code = entity.Code,
                Boxes = entity.Boxes?.Select(Convert).ToList()
            };
        }

        public Bottle Convert(BottleDTO entity)
        {
            return new Bottle
            {
                Id = entity.Id,
                Code = entity.Code,
                Box = Convert(entity.Box),
                BoxId = entity.BoxId
            };
        }

        public BottleDTO Convert(Bottle entity)
        {
            return new BottleDTO
            {
                Id = entity.Id,
                Code = entity.Code,
                BoxId = entity.BoxId
            };
        }
    }
}
