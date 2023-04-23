namespace KasperConsoleLogParser; 


// Класс для ассинхронной работы над отчётами (предполагается, что пользователь будет взаимодействовать именно с этим классом)
public class LogReporter {  
    private Dictionary<int, Task<Dictionary<string, ReportResponse> > > tasks = new ();
    private LogParser _logParser = new ();
    private Random _idGenerator = new (DateTime.Now.Millisecond);

    public int RunReportTask(string serviceNamePattern, string directoryPath) { // Создаёт задачу для формирования отчёта и возвращает идентификатор
        var reportRequest = new ReportRequest(serviceNamePattern, directoryPath);
        var id = _idGenerator.Next();  // тут можно было бы использовать хэш, что может вызвать колизию, но будет быстрее
        while (!tasks.TryAdd(id, MakeReport(reportRequest))) {
            id = _idGenerator.Next();
        }
        return id;
    }
    
    private async Task<Dictionary<string, ReportResponse>> MakeReport(ReportRequest request) {  // Задача формирования отчета
        return await Task.Run(() => _logParser.MakeReport(request));
    }
    
    public bool IsTaskCompleted(int id) { // для проверки пользователем, создан ли этот отчет
        if (!tasks.ContainsKey(id)) return false;
        return tasks[id].IsCompleted;
    }
    
    public Dictionary<string, ReportResponse>? GetReport(int id) {  // возвращает отчёт и удаляет из коллекции или возвращает null если такого нет
        if (!tasks.ContainsKey(id)) return null;
        var res = tasks[id].Result; 
        tasks.Remove(id);
        return res;
    }
}