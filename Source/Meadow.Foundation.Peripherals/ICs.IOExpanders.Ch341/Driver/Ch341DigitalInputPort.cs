using Meadow.Hardware;
using System;

namespace Meadow.Foundation.ICs.IOExpanders
{
    /// <summary>
    /// Represents a digital input port implementation for the Ch341 bus.
    /// </summary>
    public sealed class Ch341DigitalInputPort : DigitalInputPortBase
    {
        private readonly Ch341 _device;

        /// <summary>
        /// Instantiates a <see cref="Ch341DigitalInputPort"/>.
        /// </summary>
        /// <param name="pin">The pin connected to the input port.</param>
        /// <param name="info">The digital channel info associated with the pin.</param>
        /// <param name="device">The Ch341 device instance.</param>
        internal Ch341DigitalInputPort(IPin pin, IDigitalChannelInfo info, Ch341 device)
            : base(pin, info)
        {
            _device = device;
        }

        /// <summary>
        /// Gets the current state of the input port.
        /// </summary>
        /// <returns>The current state of the input port.</returns>
        public override bool State
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets or sets the resistor mode of the input port. 
        /// </summary>
        /// <exception cref="NotSupportedException">The Ch341 does not support internal resistors.</exception>
        public override ResistorMode Resistor
        {
            get => ResistorMode.Disabled;
            set => throw new NotSupportedException("The Ch341 does not support internal resistors");
        }
    }
}