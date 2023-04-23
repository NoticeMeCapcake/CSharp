using System.Text;

namespace KasperConsoleLogParser; 

// Класс, содержащий все данные для отчёта (в качестве ответа на запрос)
public class ReportResponse {
    public string ServiceName { get; }
    public DateTime EarliestDate { get; set; }
    public DateTime LatestDate { get; set; }
    public Dictionary<string, int> CategoriesCount { get; }
    public int RotationCount { get; set; }
    
    public ReportResponse(string serviceName, DateTime earliestDate, DateTime latestDate, Dictionary<string, int> categoriesCount, int rotationCount) {
        ServiceName = serviceName;
        EarliestDate = earliestDate;
        LatestDate = latestDate;
        CategoriesCount = categoriesCount;
        RotationCount = rotationCount;
    }
    public override string ToString() {  // Чтобы можно было удобно выводить в консоль. Можно было обойтись типом JSON, но, кажется, в ТЗ просят что-то подобное
        return $"Имя сервиса: {ServiceName}{Environment.NewLine}Дата и время самой ранней записи: {EarliestDate:dd.MM.yyyy HH:mm:ss.fff}{Environment.NewLine}Дата и время самой последней записи: {LatestDate:dd.MM.yyyy HH:mm:ss.fff}{Environment.NewLine}Количество записей в каждой категории: {CategoriesCount.Aggregate(new StringBuilder(), (text, category) => text.Append($"{Environment.NewLine}{category.Key}: {category.Value.ToString()}"), text => text.ToString() )}{Environment.NewLine}Количество ротаций: {RotationCount}{Environment.NewLine}";
    }
}