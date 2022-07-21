using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using Sender;

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
      float maxSOC= GetMaximumSOC(SensorParameters);
      float socAverage = CalculateSOCAverage(SensorParameters);
      float temperatureAverage = CalculateTemperatureAverage(SensorParameters);
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

    public static float CalculateTemperatureAverage(List<SensorParameter> sensorParameterList)
    {

      return CalculateMovingAverage(sensorParameterList.Select(SensorParameter => SensorParameter.Temperature).ToList());
    }

    public static float CalculateSOCAverage(List<SensorParameter> sensorParameterList)
    {
      return CalculateMovingAverage(sensorParameterList.Select(SensorParameter => SensorParameter.StateOfCharge).ToList());
    }
    private static float CalculateMovingAverage(List<float> sensorParameterList)
    {
      float movingaverage = 0;
       for (int i = sensorParameterList.Count - 1; i > sensorParameterList.Count - 6 && i > 0; i--)
      {
        movingaverage += sensorParameterList[i];
      }

      return movingaverage/5;
    }

    public static List<SensorParameter> ReadSensorData()
    {
      string newLine = Console.ReadLine();
      Console.WriteLine(newLine);
      List<SensorParameter> sensorParameters = JsonSerializer.Deserialize<List<SensorParameter>>(newLine);
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
