using System;
using System.Collections.Generic;

namespace Software9119.WeakEvent;
public partial class WeakEventCurator
{
  /// <remarks>
  /// <para>
  /// Use proper values for <paramref name="eventSource"/> and <paramref name="eventName"/> when there is more event sources or events.
  /// </para>
  /// </remarks>
  /// <exception cref="ArgumentNullException">If <paramref name="handlers"/>> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentException">
  /// <para>
  /// If <paramref name="handlers"/> is empty or any of <see cref="Delegate"/>s is <see langword="null"/>.
  /// </para>
  /// <para>
  /// If no handler list is held for <paramref name="eventSource"/> and <paramref name="eventName"/>.
  /// </para>
  /// <para>
  /// If <see cref="Delegate"/> cannot be found in handler collection, i.e. removed delegates count is less than <paramref name="handlers"/> count.
  /// </para>
  /// </exception>  
  /// <exception cref="InvalidOperationException">  
  /// If removed delagates count is greater than <paramref name="handlers"/> count. See <see cref="Add(object?, string?, Delegate[])"/> and
  /// rules of delegates equality for details.
  /// </exception>  
  /// <seealso href="https://docs.microsoft.com/en-us/dotnet/api/system.delegate.equals?view=net-6.0#remarks"/>
  public void Remove ( object? eventSource, string? eventName, params Delegate [] handlers ) => RemoveActual ( eventSource, eventName, handlers );

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
  virtual protected void RemoveActual ( object? eventSource, string? eventName, params Delegate [] handlers )
  {
    ValidateHandlers ( handlers );

    int key = HasCode (eventSource, eventName);

    List<WeakHandler>? whs;
    bool exists;

    // release lock on weakHandlersDict soon in order to allow other logic to take lock on it
    lock ( weakHandlersDict )
    {
      exists = weakHandlersDict.TryGetValue ( key, out whs );
    }

    if ( exists )
    {
      int h_length = handlers.Length;
      Predicate<WeakHandler> match = wh =>
      {
        for (int h_i = 0; h_i < h_length; ++h_i)
        {
          if (wh.Equals (handlers[h_i]))
            return true;
        }

        return false;
      };

      lock ( whs! )
      {
        int removed = whs.RemoveAll (match);
        if ( h_length > removed )
          throw new ArgumentException ( $"Removed only {removed} handlers! Expected {h_length}.", nameof ( handlers ) );
        else if ( h_length < removed )
          throw new InvalidOperationException ( $"Removed more handlers than expected! Expected {h_length}, removed {removed}!" );
      };

      return;
    }

    ThrowNoHandlerListHeldException ( eventSource, eventName );
  }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
