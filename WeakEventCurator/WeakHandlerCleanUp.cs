
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Software9119.WeakEvent;

/// <summary>
/// Logic reponsible for removing dead handlers from weak event registration lists.
/// </summary>
public class WeakHandlerCleanUp : IDisposable, IAsyncDisposable
{
  // Dispose
  readonly Timer cleanTimer;
  bool disposed;

  internal WeakHandlerCleanUp
  (
    Dictionary<int, List<WeakHandler>> handlerLists,
    TimeSpan interval
  )
  {
    int cleanInterval = (int) interval.TotalMilliseconds;

    cleanTimer = new Timer
     (
       _ => ClearHandlers (handlerLists),
       null,
       cleanInterval,
       cleanInterval
     );
  }

  void ClearHandlers ( Dictionary<int, List<WeakHandler>> handlerLists ) => ClearHandlersActual (handlerLists);

  /// <remarks>  
  /// <para>
  /// <see langword="override"/> <see cref="WeakEventCurator.AddHandlerActual(Delegate, object, string)"/>, <seealso cref="WeakEventCurator.RemoveHandlerActual(Delegate, object, string)"/>,
  /// <see cref="WeakEventCurator.InvokeActual(object, string, object[])"/> and <see cref="ClearHandlersActual(Dictionary{int, List{WeakHandler}})"/>
  /// implementations in derived classes when seeking optimal perfomance profile for specific scenario.
  /// </para>
  /// <para>
  /// Actual implementation synchronization is realized through <see langword="lock"/>ing on discrete dictionary of handlers.
  /// </para>
  /// <para>
  /// If method cannot take <see langword="lock"/> on handler dictionary, skips iteration.
  /// </para>
  /// </remarks>
  virtual protected void ClearHandlersActual ( Dictionary<int, List<WeakHandler>> handlerLists )
  {
    // If previous execution is still running or client code is interacting with handlers,
    // skip this clearance iteration.
    if (Monitor.TryEnter (handlerLists))
    {
      foreach (KeyValuePair<int, List<WeakHandler>> kv in handlerLists)
      {
        List<WeakHandler> list = kv.Value;

        for (int index = list.Count - 1; index > -1; --index)
        {
          WeakHandler wh = list[index];
          if (wh.IsAlive)
            continue;

          _ = list.Remove (wh);
        }

        if (list.Count == 0)
          _ = handlerLists.Remove (kv.Key);
      }

      Monitor.Exit (handlerLists);
    }
  }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member  
  public void Dispose ()
  {
    if (disposed)
      return;

    cleanTimer.Dispose ();

    Dispose (true);
    GC.SuppressFinalize (this);

    disposed = true;
  }

  virtual protected void Dispose ( bool disposing ) { }

  async public ValueTask DisposeAsync ()
  {
    if (disposed)
      return;

    await cleanTimer.DisposeAsync ();

    await DisposeAsyncCore ();
    GC.SuppressFinalize (this);

    disposed = true;
  }

  virtual protected ValueTask DisposeAsyncCore () => ValueTask.CompletedTask;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
