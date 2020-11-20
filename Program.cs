using System;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Threading;
using Iot.Device.DHTxx;

namespace IoTDotNetDemo
{
    class Program
    {
        public static void ReadSensor()
        {
            var blinky = false;
            GpioDriver driver = new RaspberryPi3Driver();
            var controller = new GpioController(PinNumberingScheme.Logical, driver);
            controller.OpenPin(16, PinMode.Output);

            using (var dht = new Dht11(4, gpioController: controller))
            {

                while (true)
                {

                    var temperature = dht.Temperature;
                    var humidity = dht.Humidity;
                    if (dht.IsLastReadSuccessful)
                        Console.WriteLine($"Temperature: {temperature.DegreesCelsius} \u00B0C, Humidity: {humidity.Percent} %");

                      if (!blinky)
                        controller.Write(16, PinValue.High);
                        else
                        controller.Write(16, PinValue.Low);

                      blinky = !blinky;

                    Thread.Sleep(1000);
                }
            }
        }

        static void Main(string[] args)
        {
            ReadSensor();
        }
    }
}
