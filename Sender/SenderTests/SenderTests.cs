using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sender;

namespace SenderTests
{
  [TestClass]
  public class SenderTests
  {
    [TestMethod("Test method to read the input sensor readings")]
    public void CountInputReadings()
    {
      List<SensorParameter> inputReadings = DataProcessor.ReadInputData();

      Assert.AreEqual(inputReadings.Count, 50);
    }

    [TestMethod("Test to check if the csv file is generated")]
    public void CreateNewCsvFileTest()
    {
      string workingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      File.Create(Path.Combine(workingDirectory, "TestSensorReading.csv"));

      Assert.IsTrue(File.Exists(workingDirectory+ "/TestSensorReading.csv"));
    }

    [TestMethod("Test to convert the reading into the json format")]
    public void ConvertInputToJsonTest()
    {
      List<SensorParameter> parameterList = new List<SensorParameter>();
      SensorParameter sensorData1 = new SensorParameter { Temperature = 9, StateOfCharge = 10 };
      SensorParameter sensorData2 = new SensorParameter { Temperature = 15, StateOfCharge = 16 };
      parameterList.Add(sensorData1);
      parameterList.Add(sensorData2);

      string jsonString = DataProcessor.ConvertInputToJsonFormat(parameterList);

      string expectedString = "[{\"Temperature\":9,\"StateOfCharge\":10},{\"Temperature\":15,\"StateOfCharge\":16}]";

      Assert.AreEqual(jsonString, expectedString);
    }
  }
}



