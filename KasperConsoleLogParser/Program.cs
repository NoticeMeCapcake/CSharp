using KasperConsoleLogParser;

var logReporter = new LogReporter();


Console.WriteLine("Программа для формирования отчётов по файлам логов");
while (true) {
    Console.WriteLine("Меню");
    Console.WriteLine("1. Создать отчёт.");
    Console.WriteLine("2. Получить отчёт.");
    Console.WriteLine("3. Узнать состояние отчёта.");
    Console.WriteLine("4. Завершить работу.");
    Console.Write("Выберите пункт меню: ");
    var input = Console.ReadLine();
    string? id;
    switch (input) {
        case "1":
            Console.WriteLine("Введите имя сервиса: ");
            var serviceName = Console.ReadLine();
            Console.WriteLine("Введите путь к директории (в формате C:\\directory): ");
            var directoryPath = Console.ReadLine();
            Console.WriteLine("Идентификатор вашего запроса: " + logReporter.RunReportTask(serviceName, directoryPath));
            break;
        case "2":
            Console.WriteLine("Введите идентификатор запроса: ");
            id = Console.ReadLine();
            try {
                var reportDict = logReporter.GetReport(int.Parse(id));
                if (reportDict != null) {
                    foreach (var report in reportDict) {
                        Console.WriteLine(report.Value);
                    }
                }
                else {
                    Console.WriteLine("Отчет не найден");
                }
            }
            catch (FormatException e) {
                Console.WriteLine("Неверный идентификатор");
            }
            catch (OverflowException e) {
                Console.WriteLine("Неверный идентификатор");
            }
            
            break;
        case "3":
            Console.WriteLine("Введите идентификатор запроса: ");
            id = Console.ReadLine();
            try {
                Console.WriteLine(logReporter.IsTaskCompleted(int.Parse(id))
                    ? "Отчет готов"
                    : "Отчет не готов или не найдён");
            }
            catch (FormatException e) {
                Console.WriteLine("Неверный идентификатор");
            }
            catch (OverflowException e) {
                Console.WriteLine("Неверный идентификатор");
            }
            break;
        case "4":
            Console.WriteLine("Завершение работы.");
            return;
        default:
            Console.WriteLine("Неизвестный пункт меню");
            break;
    }
}




// var logReporter = new LogReporter();  // Тест на асинхронной версии
//
// var id = logReporter.RunReportTask("test", "D:\\Danon\\Documents\\CSharp\\KasperConsoleLogParser");
// var reportDict = logReporter.GetReport(id);
// foreach (var report in reportDict) {
//     Console.WriteLine(report.Value);
// }
