using Meadow.Hardware;
using Meadow.Units;
using System;

namespace Meadow.Foundation.ICs.IOExpanders;

/// <summary>
/// Represents an SPI bus using the Ch341
/// </summary>
public class Ch341SpiBus : ISpiBus, IDisposable
{
    private bool _isDisposed;
    private Ch341 _device;
    private SpiClockConfiguration _configuration;

    /// <inheritdoc/>
    public Frequency[] SupportedSpeeds =>
        new Frequency[]
        {
                1000000.Hertz()
        };

    /// <inheritdoc/>
    public SpiClockConfiguration Configuration => _configuration;

    internal Ch341SpiBus(Ch341 device, SpiClockConfiguration configuration)
    {
        _configuration = configuration;
        _device = device;
    }

    private void Dispose(bool _)
    {
        if (!_isDisposed)
        {
            _isDisposed = true;
        }
    }

    /// <summary>
    /// Finalizer for the Ch341I2cBus class, used to release unmanaged resources.
    /// </summary>
    ~Ch341SpiBus()
    {
        Dispose(false);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    internal void Configure()
    {
        // Setup the clock and other elements
        _device.SetStream(0x88);

        //Span<byte> toSend = stackalloc byte[5];
        //int idx = 0;
        // Disable clock divide by 5 for 60Mhz master clock
/*        toSend[idx++] = (byte)Native.FT_OPCODE.DisableClockDivideBy5;
        // Turn off adaptive clocking
        toSend[idx++] = (byte)Native.FT_OPCODE.TurnOffAdaptiveClocking;
        // set SPI clock rate
        toSend[idx++] = (byte)Native.FT_OPCODE.SetClockDivisor;
        uint clockDivisor = (uint)(12000 / (_configuration.Speed.Kilohertz * 2)) - 1;
        toSend[idx++] = (byte)(clockDivisor & 0x00FF);
        toSend[idx++] = (byte)((clockDivisor >> 8) & 0x00FF);

        _device.Write(toSend);

        // make the SCK and SDO lines outputs
        _device.SetGpioDirectionAndState(true, _device.GpioDirectionLow |= 0x03, _device.GpioStateLow);*/
    }

    /// <inheritdoc/>
    public void Exchange(IDigitalOutputPort? chipSelect, Span<byte> writeBuffer, Span<byte> readBuffer, ChipSelectMode csMode = ChipSelectMode.ActiveLow)
    {
        byte clock;
        switch (_configuration.SpiMode)
        {
            default:
            case SpiClockConfiguration.Mode.Mode3:
            case SpiClockConfiguration.Mode.Mode0:
                //clock = (byte)Native.FT_OPCODE.ClockDataBytesOutOnMinusVeClockMSBFirst;
                break;
            case SpiClockConfiguration.Mode.Mode2:
            case SpiClockConfiguration.Mode.Mode1:
                //clock = (byte)Native.FT_OPCODE.ClockDataBytesOutOnPlusVeClockMSBFirst;
                break;
        }

        if (chipSelect != null)
        {
            chipSelect.State = csMode == ChipSelectMode.ActiveLow ? false : true;
        }

        int idx = 0;
        Span<byte> toSend = stackalloc byte[3 + writeBuffer.Length];
        //toSend[idx++] = clock;
        toSend[idx++] = (byte)((writeBuffer.Length - 1) & 0xff); // LSB of length to write 
        toSend[idx++] = (byte)((writeBuffer.Length - 1) >> 8);
        ; // MSB of length to write
        writeBuffer.CopyTo(toSend[3..]);
        //_device.Write(toSend);
        //_device.ReadInto(readBuffer);

        if (chipSelect != null)
        {
            chipSelect.State = csMode == ChipSelectMode.ActiveLow ? true : false;
        }
    }

    /// <inheritdoc/>
    public void Read(IDigitalOutputPort? chipSelect, Span<byte> readBuffer, ChipSelectMode csMode = ChipSelectMode.ActiveLow)
    {
        byte clock;
        switch (_configuration.SpiMode)
        {
            default:
            case SpiClockConfiguration.Mode.Mode3:
            case SpiClockConfiguration.Mode.Mode0:
                //clock = (byte)Native.FT_OPCODE.ClockDataBytesInOnPlusVeClockMSBFirst;
                break;
            case SpiClockConfiguration.Mode.Mode2:
            case SpiClockConfiguration.Mode.Mode1:
                //clock = (byte)Native.FT_OPCODE.ClockDataBytesInOnMinusVeClockMSBFirst;
                break;
        }

        if (chipSelect != null)
        {
            chipSelect.State = csMode == ChipSelectMode.ActiveLow ? false : true;
        }

        Span<byte> toSend = stackalloc byte[4];
        var idx = 0;
        //toSend[idx++] = clock;
        toSend[idx++] = (byte)((readBuffer.Length - 1) & 0xff); // LSB of length to read 
        toSend[idx++] = (byte)((readBuffer.Length - 1) >> 8);
        ; // MSB of length to read
        //toSend[idx++] = (byte)Native.FT_OPCODE.SendImmediate; // read now
        //_device.Write(toSend);
        //var readCount = _device.ReadInto(readBuffer);

        if (chipSelect != null)
        {
            chipSelect.State = csMode == ChipSelectMode.ActiveLow ? true : false;
        }
    }

    /// <inheritdoc/>
    public void Write(IDigitalOutputPort? chipSelect, Span<byte> writeBuffer, ChipSelectMode csMode = ChipSelectMode.ActiveLow)
    {
        if (writeBuffer.Length > 65535)
        {
            throw new ArgumentException("Buffer too large, maximum size if 65535");
        }

        byte clock;
        switch (_configuration.SpiMode)
        {
            default:
            case SpiClockConfiguration.Mode.Mode3:
            case SpiClockConfiguration.Mode.Mode0:
                //clock = (byte)Native.FT_OPCODE.ClockDataBytesOutOnMinusVeClockMSBFirst;
                break;
            case SpiClockConfiguration.Mode.Mode2:
            case SpiClockConfiguration.Mode.Mode1:
                //clock = (byte)Native.FT_OPCODE.ClockDataBytesOutOnPlusVeClockMSBFirst;
                break;
        }

        if (chipSelect != null)
        {
            chipSelect.State = csMode == ChipSelectMode.ActiveLow ? false : true;
        }

        //int idx = 0;
        //Span<byte> toSend = stackalloc byte[writeBuffer.Length];
        //toSend[idx++] = clock;
        //toSend[idx++] = (byte)((writeBuffer.Length - 1) & 0xff); // LSB of length to write 
        //toSend[idx++] = (byte)((writeBuffer.Length - 1) >> 8);
        ; // MSB of length to write
        //writeBuffer.CopyTo(toSend);
        //_device.Write(toSend);

        _device.StreamSPI4(0x00, writeBuffer.ToArray());

        if (chipSelect != null)
        {
            chipSelect.State = csMode == ChipSelectMode.ActiveLow ? true : false;
        }
    }
}
