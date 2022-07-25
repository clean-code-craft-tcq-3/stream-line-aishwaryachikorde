using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;

namespace Receiver
{
  public class Receiver
  {
    [ExcludeFromCodeCoverage]
    static void Main(string[] args)
    {
      List<SensorParameter> SensorParameters = ReadSensorData();
      float maxTemperature = GetMaximumTemperature(SensorParameters);
      float minTemperature = GetMinimumTemperature(SensorParameters);
      float minSOC = GetMinimumSOC(SensorParameters);
      float maxSOC = GetMaximumSOC(SensorParameters);
      float socAverage = GetMovingAverageForSOC(SensorParameters);
      float temperatureAverage = GetMovingAverageForTemperature(SensorParameters);
      printOnConsole(maxTemperature, minTemperature, minSOC, maxSOC, socAverage, temperatureAverage);

    }

    private static void printOnConsole(float maxTemperature, float minTemperature, float minSOC, float maxSOC, float socAverage, float temperatureAverage)
    {
      Console.WriteLine($"Maximum temperature:{maxTemperature}");
      Console.WriteLine($"Minimum temperature:{minTemperature}");
      Console.WriteLine($"Maximum SOC:{maxSOC}");
      Console.WriteLine($"Minimum SOC:{minSOC}");
      Console.WriteLine($"Average temperature:{temperatureAverage}");
      Console.WriteLine($"Average SOC:{socAverage}");

    }

    public static float GetMovingAverageForTemperature(List<SensorParameter> sensorParameterList)
    {

      return CalculateMovingAverage(sensorParameterList.Select(SensorParameter => SensorParameter.Temperature).ToList());
    }

    public static float GetMovingAverageForSOC(List<SensorParameter> sensorParameterList)
    {
      return CalculateMovingAverage(sensorParameterList.Select(SensorParameter => SensorParameter.StateOfCharge).ToList());
    }

    private static float CalculateMovingAverage(List<float> sensorParameterList)
    {
      float movingaverage = 0;
      for (int i = 0 ;i < sensorParameterList.Count; i++)
      {
        movingaverage += sensorParameterList[i];
      }

      return movingaverage / sensorParameterList.Count;
    }

    public static List<SensorParameter> ReadSensorData()
    {
      string consoleData = Console.ReadLine();
      List<SensorParameter> sensorParameters = JsonParser(consoleData);
      return sensorParameters;
    }

    public static List<SensorParameter> JsonParser(string data)
    {
      List<SensorParameter> sensorParameters = JsonSerializer.Deserialize<List<SensorParameter>>(data);
      return sensorParameters;
    }



    public static float GetMaximumTemperature(List<SensorParameter> sensorParameter)
    {
      return GetMaximumValue(sensorParameter.Select(param => param.Temperature).ToList());
    }

    public static float GetMaximumSOC(List<SensorParameter> sensorParameter)
    {
      return GetMaximumValue(sensorParameter.Select(param => param.StateOfCharge).ToList());
    }

    public static float GetMinimumTemperature(List<SensorParameter> sensorParameter)
    {
      return GetMinimumValue(sensorParameter.Select(param => param.Temperature).ToList());
    }

    public static float GetMinimumSOC(List<SensorParameter> sensorParameter)
    {
      return GetMinimumValue(sensorParameter.Select(param => param.StateOfCharge).ToList());
    }

    private static float GetMinimumValue(List<float> parameterList)
    {
      return parameterList.Min();
    }

    private static float GetMaximumValue(List<float> parameterList)
    {
      return parameterList.Max();
    }
  }
}
