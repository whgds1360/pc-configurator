using System.Collections.Generic;

namespace PcConfigurator.helpers.TableStructures;


/// <summary>
/// Тут распишу чтобы не забыть используется в админ панели,
/// нужен крч чтобы запросить от EF даннные о БД там список таблиц,
/// их атрибуты и т.д. это чисто шаблон то чего нужно.
/// </summary>
internal class MetaData
{
    public string? TableName { set; get; }
    public List<string>? Properties { set; get; } 
}