using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Receiver
{
  [TestClass]
  public class ReceiverTest
  {
    private const string consoleJsonData =
      "[{\"Temperature\":20,\"StateOfCharge\":20}," +
      "{\"Temperature\":12,\"StateOfCharge\":79}," +
      "{\"Temperature\":1,\"StateOfCharge\":31}," +
      "{\"Temperature\":12,\"StateOfCharge\":39}," +
      "{\"Temperature\":5,\"StateOfCharge\":31}]";

    private List<SensorParameter> sensorData;

 [TestInitialize]
    public void init()
    {

      sensorData = new List<SensorParameter>();
      sensorData.Add(new SensorParameter() { Temperature = 20, StateOfCharge = 20 });
      sensorData.Add(new SensorParameter() { Temperature = 12, StateOfCharge = 79 });
      sensorData.Add(new SensorParameter() { Temperature = 1, StateOfCharge = 31 });
      sensorData.Add(new SensorParameter() { Temperature = 12, StateOfCharge = 39 });
      sensorData.Add(new SensorParameter() { Temperature = 5, StateOfCharge = 31 });

    }

    [TestMethod("should parse the json content and return list of type sensorParameter")]
    public void JsonParserTest()
    {
      List<SensorParameter> sensorDataList = Receiver.JsonParser(consoleJsonData);
      Assert.AreEqual(sensorDataList.Count, sensorData.Count);
      Assert.AreEqual(sensorDataList[0].Temperature,20);
      Assert.AreEqual(sensorDataList[1].Temperature, 12);
      Assert.AreEqual(sensorDataList[2].Temperature, 1);
      Assert.AreEqual(sensorDataList[0].StateOfCharge, 20);
      Assert.AreEqual(sensorDataList[1].StateOfCharge, 79);
      Assert.AreEqual(sensorDataList[2].StateOfCharge, 31);
    }

    [TestMethod("should calculate The moving average of  soc")]
    public void SocMovingAverageTest()
    {
      float movingAverageOfSoc = Receiver.GetMovingAverageForSOC(sensorData);
      Assert.AreEqual(movingAverageOfSoc,40);
    }

    [TestMethod("should calculate The moving average of  temperature")]
    public void TemperatureMovingAverageTest()
    {
      float movingAverageOfTemp = Receiver.GetMovingAverageForTemperature(sensorData);
      Assert.AreEqual(movingAverageOfTemp, 10);
    }

    [TestMethod("should fetch the maximum SOC")]
    public void GetMaximumSocTest()
    {
      float maximumSoc= Receiver.GetMaximumSOC(sensorData);
      Assert.AreEqual(maximumSoc, 79);
    }

    [TestMethod("should fetch the maximum temperature")]
    public void GetMaximumTemperatureTest()
    {
      float maximumTemperature = Receiver.GetMaximumTemperature(sensorData);
      Assert.AreEqual(maximumTemperature, 20);
    }

    [TestMethod("should fetch the minimum temperature")]
    public void GetMinimumTemperatureTest()
    {
      float minimumTemperature = Receiver.GetMinimumTemperature(sensorData);
      Assert.AreEqual(minimumTemperature, 1);
    }
    [TestMethod("should fetch the minimum SOC")]
    public void GetMinimumSOCTest()
    {
      float minimumSoc = Receiver.GetMinimumSOC(sensorData);
      Assert.AreEqual(minimumSoc, 20);
    }

  }
}
