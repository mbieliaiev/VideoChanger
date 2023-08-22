using Microsoft.Extensions.Configuration;

public class Program
{
    static void Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        // Accessing configuration settings
        string inputFileAddress = configuration["AppSettings:InputFileAddress"];
        string outputFileName = configuration["AppSettings:OutputFileName"];
        string outputDir = configuration["AppSettings:OutputDir"];
        int numberFilesGenerated = int.Parse(configuration["AppSettings:NumberFilesGenerated"]);
        int brightnessMinPercent = int.Parse(configuration["AppSettings:BrightnessMinPercent"]);
        int brightnessMaxPercent = int.Parse(configuration["AppSettings:BrightnessMaxPercent"]);

        double brightnessChangeMin = 1 + brightnessMinPercent / 100.0;
        double brightnessChangeMax = 1 - brightnessMaxPercent / 100.0;

        var random = new Random();        

        for (int i = 0; i < numberFilesGenerated; i++)
        {
            var videoChanger = new VideoChanger.VideoChanger(inputFileAddress);
            double randomChangeNumber = random.NextDouble() * (brightnessChangeMax - brightnessChangeMin) + brightnessChangeMin;
            videoChanger.AddBrightness($"{outputDir}\\{outputFileName}-{i + 1}.mp4", randomChangeNumber);
        }
    }
}