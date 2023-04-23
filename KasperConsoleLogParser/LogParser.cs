using System.Text.RegularExpressions;

namespace KasperConsoleLogParser;

public partial class LogParser {
    private readonly Regex _squareBracketsRegex = MyRegex();
    
    public Dictionary<string, ReportResponse> MakeReport(ReportRequest request) {
        var directoryPath = request.GetDirectoryPath();
        var serviceNamePattern = request.GetServiceName();
        var matchedFilesList = GetFileNames(directoryPath, serviceNamePattern);

        var reportDict = 
            matchedFilesList
                .Distinct()
                .Select(filePath => filePath.Split(".").First().Split("\\").Last())
                .ToDictionary(
                    serviceName => serviceName, 
                    serviceName => new ReportResponse(
                        serviceName, DateTime.MinValue, DateTime.MinValue, 
                        new Dictionary<string, int>(), 0)
                );
        var highestRotationNumber = reportDict.Keys
            .ToDictionary(serviceName => serviceName, serviceName => 0);


        foreach (var file in matchedFilesList) {
            var fileNameParts = file.Split(".");
            var serviceName =  fileNameParts.First().Split("\\").Last();  // по-хорошему, тут нужна регулярка, чтобы поддерживались оба формата пути к файлу (\ или /)
            var currentReport = reportDict[serviceName];
            var currentCountAsString = fileNameParts[1];
            using var reader = new StreamReader(file);
            MatchCollection? dataInBrackets = null;
            GroupCollection? matchedGroups = null;
            if ((currentCountAsString != "log" && int.Parse(currentCountAsString) > highestRotationNumber[serviceName]) ||
                (currentCountAsString == "log" && highestRotationNumber[serviceName] == 0)) {
                var line = reader.ReadLine();
                if (line is null) continue;
                dataInBrackets = _squareBracketsRegex.Matches(line);
                matchedGroups = dataInBrackets.First().Groups;
                if (matchedGroups.Count <= 2) continue;
                currentReport.EarliestDate = DateTime.Parse(matchedGroups[1].Value);
                var category = matchedGroups[2].Value;
                if (!currentReport.CategoriesCount.TryAdd(category, 1)) {
                    currentReport.CategoriesCount[category]++;
                }
            }
            while (reader.ReadLine() is { } line) {
                dataInBrackets = _squareBracketsRegex.Matches(line);
                matchedGroups = dataInBrackets.First().Groups;
                if (matchedGroups.Count <= 2) continue;
                var category = matchedGroups[2].Value;
                if (!currentReport.CategoriesCount.TryAdd(category, 1)) {
                    currentReport.CategoriesCount[category]++;
                }
            }

            if (currentCountAsString == "log") {
                currentReport.LatestDate = dataInBrackets == null ? DateTime.MinValue : DateTime.Parse(matchedGroups[1].Value);;
            }
            currentReport.RotationCount++;
        }
        
        return reportDict;
    }
    private static LinkedList<string> GetFileNames(string directoryPath, string namePattern) {
        if (!Directory.Exists(directoryPath)) {
            throw new DirectoryNotFoundException(directoryPath);
        }
        var serviceNameRegex = new Regex(namePattern);
        var matchedFilesList = new LinkedList<string>();
        var files = Directory.GetFiles(directoryPath);
        foreach (var file in files) {
            var fileName = Path.GetFileName(file);
            if (serviceNameRegex.IsMatch(fileName)) {
                matchedFilesList.AddFirst(directoryPath + "\\" + fileName);
            }
        }

        return matchedFilesList;
    }

    [GeneratedRegex(@"\[(.*)\]\[(.*)\]")]
    private static partial Regex MyRegex();
}