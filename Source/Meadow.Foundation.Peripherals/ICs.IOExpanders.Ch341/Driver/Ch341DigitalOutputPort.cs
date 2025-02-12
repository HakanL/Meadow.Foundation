using Meadow.Hardware;

namespace Meadow.Foundation.ICs.IOExpanders
{
    /// <summary>
    /// Digital output port for Ch341 devices.
    /// </summary>
    public sealed class Ch341DigitalOutputPort : DigitalOutputPortBase
    {
        private readonly Ch341 _device;
        private bool _state;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ch341DigitalOutputPort"/> class.
        /// </summary>
        /// <param name="pin">The pin to use.</param>
        /// <param name="info">The digital channel info.</param>
        /// <param name="initialState">The initial state of the output port.</param>
        /// <param name="initialOutputType">The initial output type.</param>
        /// <param name="device">The Ch341 device.</param>
        internal Ch341DigitalOutputPort(IPin pin, IDigitalChannelInfo info, bool initialState, OutputType initialOutputType, Ch341 device)
            : base(pin, info, initialState, initialOutputType)
        {
            _device = device;
            State = initialState;
        }

        /// <summary>
        /// Gets or sets the state of the digital output port.
        /// </summary>
        /// <value>
        /// The state of the digital output port.
        /// </value>
        public override bool State
        {
            get => _state;
            set
            {
                if (value)
                {
                    _device.SetState((byte)this.Pin.Key);
                    _state = value;

                }
                else
                {
                    _device.ClearState((byte)this.Pin.Key);
                    _state = value;
                }
            }
        }
    }
}