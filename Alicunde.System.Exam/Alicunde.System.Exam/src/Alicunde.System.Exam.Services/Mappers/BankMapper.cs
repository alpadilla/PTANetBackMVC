using Alicunde.System.Exam.Contracts.Bank;
using Alicunde.System.Exam.Contracts.OpenData;
using Riok.Mapperly.Abstractions;

namespace Alicunde.System.Exam.Services.Mappers;

[Mapper]
public static partial class BankMapper
{
    public static partial BankCreateDto ToBankCreateDto(this BankOpenDataDto openDataDto);
    public static partial IEnumerable<BankCreateDto> ToIEnumerableBankCreateDto(this IEnumerable<BankOpenDataDto> openDataDtos);
    public static partial Domain.Bank ToEntity(this BankCreateDto bankCreateDto);
    public static partial BankDto ToGeneralDto(this Domain.Bank bank);
    public static partial IEnumerable<BankDto> ToGeneralDtos(this IEnumerable<Domain.Bank> bank);
    public static partial Domain.Bank ToEntity(this BankUpdateDto bankUpdateDto);
    public static partial BankUpdateDto ToUpdateDto(this BankCreateDto bankCreateDto);
}