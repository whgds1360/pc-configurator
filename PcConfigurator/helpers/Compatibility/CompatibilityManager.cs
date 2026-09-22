using Dapper;
using PcConfigurator.helpers.TableStructures;

namespace PcConfigurator.helpers.Compatibility;

enum Status
{
    OK,
    Error,
    Warning,
    Empty
};

/// <summary>
/// Класс с проверками совместимости: 
/// 
/// 1. По чипсету проверяется CPU <-> Motherboard
/// 2. По отводимому теплу кулера и TDP проца
/// 3. По сокету кулера и TDP проца
/// 4. По форм фактору матери и корпуса
/// 5. По количеству требующихся кабелей для GPU и тому сколько имеет БП
/// 6. По типу DDR модуля памяти и матери
/// </summary>
internal class CompatibilityManager
{
    public static Status status { get; set; } = Status.Empty;
    public static string? comment { get; set; } = null;

    public static bool CheckChipset()
    {}

    public static bool CheckCooling()
    {}

    public static bool CheckSoketCooling()
    {}

    public static bool CheckFormFactor()
    {}

    public static bool CheckGpuCables()
    {}

    public static bool CheckDdrType()
    {}




}