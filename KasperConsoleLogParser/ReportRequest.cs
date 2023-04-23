namespace KasperConsoleLogParser; 

// Класс для отправки запроса на создание отчёта
public class ReportRequest {
    private string ServiceName { get; }  // регулярное выражение для поиска соответствующих файлов логов
    private string DirectoryPath { get; }  // путь к директории, в которой находятся файлы

    public ReportRequest(string serviceName, string directoryPath) {
        ServiceName = serviceName;
        DirectoryPath = directoryPath;
    }
    
    public string GetDirectoryPath() {
        return DirectoryPath;
    }
    
    public string GetServiceName() {
        return ServiceName;
    }
}