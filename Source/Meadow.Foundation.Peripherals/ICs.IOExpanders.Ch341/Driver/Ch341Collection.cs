using System.Collections;
using System.Collections.Generic;

#nullable enable

namespace Meadow.Foundation.ICs.IOExpanders;

/// <summary>
/// Represents a collection of Ch341 devices and provides functionality for device enumeration.
/// </summary>
public class Ch341Collection : IEnumerable<Ch341>
{
    private static Ch341Collection? _instance;

    private List<Ch341> _list = new List<Ch341>();

    /// <summary>
    /// Gets the number of Ch341 devices connected to the host machine.
    /// </summary>
    public int Count => _list.Count;

    /// <summary>
    /// Gets the Ch341 device at the specified index in the collection.
    /// </summary>
    /// <param name="index">The index of the Ch341 device to retrieve.</param>
    public Ch341 this[int index] => _list[index];

    private Ch341Collection()
    {
    }

    /// <summary>
    /// Refreshes the collection by detecting and updating Ch341 devices.
    /// </summary>
    public void Refresh()
    {
        _list.Clear();

        // Currently no known way to know how many are connected, assume one
        _list.Add(new Ch341(0));
    }

    /// <inheritdoc/>
    public IEnumerator<Ch341> GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Gets the singleton instance of Ch341Collection, initializing it if necessary.
    /// </summary>
    public static Ch341Collection Devices
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Ch341Collection();
                _instance.Refresh();
            }
            return _instance;
        }
    }
}