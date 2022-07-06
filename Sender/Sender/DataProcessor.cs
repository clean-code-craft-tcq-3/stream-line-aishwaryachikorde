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

    public static List<SensorParameter> ReadInputData()
    {
      List<SensorParameter> inputReadingList = new List<SensorParameter>();

      string fileDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "SensorReading.csv");

      using StreamReader streamReader = new StreamReader(fileDirectory);
      while (!streamReader.EndOfStream)
      {
        string[] parameterColumn = streamReader.ReadLine()?.Split(',');
        inputReadingList.Add(new SensorParameter { Temperature = float.Parse(parameterColumn[0]), StateOfCharge = float.Parse(parameterColumn[1]) });
      }

      return inputReadingList;
    }

    public static string ConvertInputToJsonFormat(SensorParameter sensorReading)
    {
      string jsonFormatReading = JsonSerializer.Serialize(sensorReading);

      return jsonFormatReading;
    }

    public static void LogSensorReadingsOnConsole(List<SensorParameter> sensorDataReadings)
    {
      foreach (SensorParameter sensorReading in sensorDataReadings)
      {
        string readingInJson = ConvertInputToJsonFormat(sensorReading);

        Console.WriteLine(readingInJson);
      }
    }
  }
}
