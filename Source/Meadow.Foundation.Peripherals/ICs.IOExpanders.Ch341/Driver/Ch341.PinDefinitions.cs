using Meadow.Hardware;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Meadow.Foundation.ICs.IOExpanders;

public partial class Ch341
{
    /// <summary>
    /// Provides definitions for the pins of the Ch341 device.
    /// </summary>
    public class PinDefinitions : IPinDefinitions
    {
        /// <inheritdoc/>
        public IEnumerator<IPin> GetEnumerator() => AllPins.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Collection of pins
        /// </summary>
        public IList<IPin> AllPins { get; } = new List<IPin>();

        /// <inheritdoc/>
        public IPinController? Controller { get; set; }

        /// <summary>
        /// Create a new PinDefinitions object
        /// </summary>
        internal PinDefinitions(Ch341 controller)
        {
            Controller = controller;
            InitAllPins();
        }

        /// <summary>
        /// Gets the pin representing IO0 on the Ch341 device.
        /// </summary>
        public IPin D0 => new Pin(
            Controller,
            "D0",
            (byte)(1 << 0),
            new List<IChannelInfo> {
                new DigitalChannelInfo("D0", interruptCapable: false, pullUpCapable: false, pullDownCapable: false)
            });

        /// <summary>
        /// Gets the pin representing IO1 on the Ch341 device.
        /// </summary>
        public IPin D1 => new Pin(
            Controller,
            "D1",
            (byte)(1 << 1),
            new List<IChannelInfo> {
                new DigitalChannelInfo("D1", interruptCapable: false, pullUpCapable: false, pullDownCapable: false)
            });

        /// <summary>
        /// Gets the pin representing IO2 on the Ch341 device.
        /// </summary>
        public IPin D2 => new Pin(
            Controller,
            "D2",
            (byte)(1 << 2),
            new List<IChannelInfo> {
                new DigitalChannelInfo("D2", interruptCapable: false, pullUpCapable: false, pullDownCapable: false)
            });

        /// <summary>
        /// Gets the pin representing IO3 on the Ch341 device.
        /// </summary>
        public IPin D3 => new Pin(
            Controller,
            "D3",
            (byte)(1 << 3),
            new List<IChannelInfo> {
                new DigitalChannelInfo("D3", interruptCapable: false, pullUpCapable: false, pullDownCapable: false)
            });

        /// <summary>
        /// Gets the pin representing IO4 on the Ch341 device.
        /// </summary>
        public IPin D4 => new Pin(
            Controller,
            "D4",
            (byte)(1 << 4),
            new List<IChannelInfo> {
                new DigitalChannelInfo("D4", interruptCapable: false, pullUpCapable: false, pullDownCapable: false)
            });

        /// <summary>
        /// Gets the pin representing IO5 on the Ch341 device.
        /// </summary>
        public IPin D5 => new Pin(
            Controller,
            "D5",
            (byte)(1 << 5),
            new List<IChannelInfo> {
                new DigitalChannelInfo("D5", interruptCapable: false, pullUpCapable: false, pullDownCapable: false)
            });

        /// <summary>
        /// Gets the pin representing IO6 on the Ch341 device.
        /// </summary>
        public IPin D6 => new Pin(
            Controller,
            "D6",
            (byte)(1 << 6),
            new List<IChannelInfo> {
                new DigitalChannelInfo("D6", interruptCapable: false, pullUpCapable: false, pullDownCapable: false)
            });

        /// <summary>
        /// Gets the pin representing IO7 on the Ch341 device.
        /// </summary>
        public IPin D7 => new Pin(
            Controller,
            "D7",
            (byte)(1 << 7),
            new List<IChannelInfo> {
                new DigitalChannelInfo("D7", interruptCapable: false, pullUpCapable: false, pullDownCapable: false)
            });

        /// <summary>
        /// Initialized all pins of the Ch341
        /// </summary>
        protected void InitAllPins()
        {
            // add all our pins to the collection
            AllPins.Add(D0);
            AllPins.Add(D1);
            AllPins.Add(D2);
            AllPins.Add(D3);
            AllPins.Add(D4);
            AllPins.Add(D5);
            AllPins.Add(D6);
            AllPins.Add(D7);
        }
    }
}