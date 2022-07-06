using System.Collections.Generic;
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

    [TestMethod("Test to convert the reading into the json format")]
    public void ConvertInputToJsonTest()
    {
      SensorParameter sensorData = new SensorParameter { Temperature = 9, StateOfCharge = 10 };

      string jsonString = DataProcessor.ConvertInputToJsonFormat(sensorData);

      string expectedString = "{\"Temperature\":9,\"StateOfCharge\":10}";

      Assert.AreEqual(jsonString, expectedString);
    }
  }
}