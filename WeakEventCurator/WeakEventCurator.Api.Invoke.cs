using System;
using System.Collections.Generic;

namespace Software9119.WeakEvent;
public partial class WeakEventCurator
{
  /// <remarks>
  /// Use proper values for <paramref name="eventSource"/> and <paramref name="eventName"/> when there is more event sources or events.
  /// </remarks>
  /// <exception cref="ArgumentException">
  /// If no handler list is held for <paramref name="eventSource"/> and <paramref name="eventName"/>.
  /// </exception>
  public void Invoke ( object? eventSource, string? eventName, params object? []? parameters ) => InvokeActual ( eventSource, eventName, parameters );

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
  virtual protected void InvokeActual ( object? eventSource, string? eventName, params object? []? parameters )
  {
    int key = HasCode (eventSource, eventName);

    List<WeakHandler>? whs;
    bool exists;

    // immediate lock release allows other logic to take lock on weakHandlersDict
    lock ( weakHandlersDict )
    {
      exists = weakHandlersDict.TryGetValue ( key, out whs );
    }

    if ( exists )
    {
      lock ( whs! )
      {
        int whs_length = whs.Count;
        for ( int whs_i = 0 ; whs_i < whs_length ; ++whs_i )
          whs [whs_i].Invoke ( parameters );
      }

      return;
    }

    ThrowNoHandlerListHeldException ( eventSource, eventName );
  }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
