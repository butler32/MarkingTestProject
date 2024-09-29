using MarkingTestProject.Core.DTOS;
using MarkingTestProject.Domain.Entities;

namespace MarkingTestProject.Core.Interfaces
{
    public interface IDTOConversionService
    {
        Box Convert(BoxDTO entity);
        BoxDTO Convert(Box entity);
        Pallet Convert(PalletDTO entity);
        PalletDTO Convert(Pallet entity);
        Bottle Convert(BottleDTO entity);
        BottleDTO Convert(Bottle entity);
    }
}
