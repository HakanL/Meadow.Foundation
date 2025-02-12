// See https://aka.ms/new-console-template for more information
using Meadow;
using Meadow.Foundation.ICs.IOExpanders;
using Meadow.Hardware;
using System.Diagnostics;
using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;
using Meadow.Peripherals.Displays;

Console.WriteLine("HELLO FROM THE WILDERNESS CH341A DRIVER!");

var count = Ch341Collection.Devices.Count();
var expander = Ch341Collection.Devices[0];

//await TestGpio(Ch341Collection.Devices);
//await TestI2C(Ch341Collection.Devices[0]);
await TestSPI(Ch341Collection.Devices[0]);

async Task TestSPI(Ch341 expander)
{
    var display = new St7789
        (
            spiBus: expander.CreateSpiBus(),
            chipSelectPin: expander.Pins.D0,
            dcPin: expander.Pins.D1,
            resetPin: null,
            135, 240
        );

    var microGraphics = new MicroGraphics(display)
    {
        CurrentFont = new Font12x16(),
        Rotation = RotationType._270Degrees
    };

    microGraphics.Clear();
    microGraphics.DrawText(0, 0, "Loading Menu");
    microGraphics.Show();

    while (true)
    {
        Debug.WriteLine("Sleeping...");

        await Task.Delay(1000);
    }
}

async Task TestGpio(IEnumerable<Ch341> expanders)
{
    var outputs = new List<IDigitalOutputPort>();

    foreach (var expander in expanders)
    {
        outputs.Add(expander.CreateDigitalOutputPort(expander.Pins.D0));
        outputs.Add(expander.CreateDigitalOutputPort(expander.Pins.D1));
        outputs.Add(expander.CreateDigitalOutputPort(expander.Pins.D2));
        outputs.Add(expander.CreateDigitalOutputPort(expander.Pins.D3));
        outputs.Add(expander.CreateDigitalOutputPort(expander.Pins.D4));
        outputs.Add(expander.CreateDigitalOutputPort(expander.Pins.D5));
        outputs.Add(expander.CreateDigitalOutputPort(expander.Pins.D6));
        outputs.Add(expander.CreateDigitalOutputPort(expander.Pins.D7));
    }

    var s = false;

    while (true)
    {

        for (var i = 0; i < outputs.Count; i++)
        {
            var setTo = (i % 2 == 0) ? s : !s;
            outputs[i].State = setTo;
        }

        await Task.Delay(1000);
        s = !s;
    }
}
