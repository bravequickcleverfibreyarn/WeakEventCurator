using System;
using System.Collections.Generic;

namespace Software9119.WeakEvent;
public partial class WeakEventCurator
{
  /// <remarks>
  /// <para>
  /// Handlers are not validated for multiple inserts thus upon manual removal there can be removed more handlers than were passed
  /// for removal, i.e. all which are equal. This results in exception thrown.
  /// </para>
  /// <para>
  /// Use proper values for <paramref name="eventSource"/> and <paramref name="eventName"/> when there is more event sources or events.
  /// </para>
  /// </remarks>
  /// <exception cref="ArgumentNullException">If <paramref name="handlers"/>> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentException">If <paramref name="handlers"/> is empty or any of <see cref="Delegate"/>s is <see langword="null"/>.</exception>
  /// <seealso href="https://docs.microsoft.com/en-us/dotnet/api/system.delegate.equals?view=net-6.0#remarks"/>
  public void Add ( object? eventSource, string? eventName, params Delegate [] handlers ) => AddActual ( eventSource, eventName, handlers );

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
  virtual protected void AddActual ( object? eventSource, string? eventName, params Delegate [] handlers )
  {
    ValidateHandlers ( handlers );

    // try to shorten lock times, make WeakHandlers first
    int h_length = handlers.Length;
    List<WeakHandler> addition = new (h_length);
    for ( int h_i = 0 ; h_i < h_length ; ++h_i )
      addition.Add ( new WeakHandler ( handlers [h_i] ) );

    int key = HasCode (eventSource, eventName);
    List<WeakHandler>? whs;

    // release lock on weakHandlersDict soon in order to allow other logic to take lock on it
    lock ( weakHandlersDict )
    {
      if ( !weakHandlersDict.TryGetValue ( key, out whs ) )
      {
        weakHandlersDict [key] = addition;
        return;
      }
    }

    // this can render yeasty if cleanup logic removes whs from weakHandlersDict
    // otherwise it spares time needed to hold lock on weakHandlersDict
    // let be optimistic and consider this possibility as rare case
    lock ( whs )
    {
      _ = whs.EnsureCapacity ( whs.Count + h_length );
      whs.AddRange ( addition );
    }

    // cleanup procedure can remove whs from weakHandlersDict before lock is taken on it
    lock ( weakHandlersDict )
    {
      if ( weakHandlersDict.TryGetValue ( key, out List<WeakHandler>? validation ) )
      {
        // when do not, then okay
        if ( ReferenceEquals ( validation, whs ) )
          return;

        // when do and another thread inserted new list for this key, extend it with addition
        // worst case: whs are populated vainly and validation has to be populated
        // while locked on weakHandlersDict
        lock ( validation )
        {
          _ = validation.EnsureCapacity ( validation.Count + h_length );
          validation.AddRange ( addition );
        }
        return;
      }

      // worse case: whs are populated vainly
      weakHandlersDict [key] = addition;
    }
  }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
