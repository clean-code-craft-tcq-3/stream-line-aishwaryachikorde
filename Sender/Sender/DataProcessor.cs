using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Sender
{
  public class DataProcessor
  {
    [ExcludeFromCodeCoverage]
    static void Main(string[] args)
    {
      List<SensorParameter> sensorDataReadings = ReadInputData();

      LogSensorReadingsOnConsole(sensorDataReadings);
    }

    private static void CreateNewCsvFile(string fileDirectory)
    {
      var newFile = File.Create(Path.Combine(fileDirectory, "SensorReading.csv"));
      newFile.Close();
    }

    private static void GenerateSensorData(string fileDirectory)
    {
      int numberOfSamples = 50;
      int maximumTemperature = 45;
      int minimumTemperature = 0;
      int maximumSoc = 80;
      int minimumSoc = 20;
      Random temperatureData = new Random();
      Random stateOfChargeData = new Random();
      using StreamWriter writer = File.AppendText(Path.Combine(fileDirectory, "SensorReading.csv"));
      for (int i = 1; i <= numberOfSamples; i++)
      {
        int temperatureDataValue = temperatureData.Next(minimumTemperature, maximumTemperature);
        int chargeDataValue = stateOfChargeData.Next(minimumSoc, maximumSoc);
        writer.Write(temperatureDataValue + "," + chargeDataValue + "\n");
      }
    }

    public static List<SensorParameter> ReadInputData()
    {
      string workingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
  
        CreateNewCsvFile(workingDirectory);
        GenerateSensorData(workingDirectory);
      

      List<SensorParameter> inputReadingList = new List<SensorParameter>();

      string fileDirectory = Path.Combine(workingDirectory, "SensorReading.csv");

      using StreamReader streamReader = new StreamReader(fileDirectory);
      while (!streamReader.EndOfStream)
      {
        string[] parameterColumn = streamReader.ReadLine()?.Split(',');
        inputReadingList.Add(new SensorParameter { Temperature = float.Parse(parameterColumn[0]), StateOfCharge = float.Parse(parameterColumn[1]) });
      }

      return inputReadingList;
    }

    public static string ConvertInputToJsonFormat(List<SensorParameter> sensorReading)
    {
      string jsonFormatReading = JsonSerializer.Serialize(sensorReading);

      return jsonFormatReading;
    }

    private static void LogSensorReadingsOnConsole(List<SensorParameter> sensorDataReadings)
    {
       string jsonFormatReading = ConvertInputToJsonFormat(sensorDataReadings); 
       Console.WriteLine(jsonFormatReading);
    }
  }
}

