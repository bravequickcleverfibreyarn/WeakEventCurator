
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Software9119.WeakEvent;

/// <summary>
/// <para>
/// ◾ Weak event pattern allows to register for event without need to explicit deregistration.
/// Thus it denies event listener memory leaks.
/// </para>
/// <para>
/// ◾ See also <see href="https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/weak-event-patterns?view=netframeworkdesktop-4.8"/>.
/// </para>
/// </summary>
/// <remarks>
/// <para>
/// ◾ All methods except disposal-pattern ones are thread-safe.
/// </para>
/// <para>
/// ◾ <see langword="override"/> <see cref="AddActual(object, string, Delegate[])"/>, <see cref="RemoveActual(object, string, Delegate[])"/>,
/// <see cref="InvokeActual(object, string, object[])"/> and <see cref="WeakHandlerCleanUp.ClearHandlersActual(Dictionary{int, List{WeakHandler}})"/>
/// implementations in derived classes when seeking optimal perfomance profile for specific scenario.
/// </para>
/// <para>
/// ◾ Actual synchronization implementation is realized through <see langword="lock"/>ing on private collections of handlers.
/// </para>
/// </remarks>
public partial class WeakEventCurator : IDisposable, IAsyncDisposable
{
  /// <summary>
  /// Maps handlers:
  /// <para>
  /// • <see cref="KeyValuePair{TKey, TValue}.Key"/> represents event and its source. Computed by <see cref="HasCode(object?, string?)"/>.
  /// </para>
  /// <para>
  /// • <see cref="KeyValuePair{TKey, TValue}.Value"/> holds relevant handlers.
  /// </para>
  /// </summary>
  readonly protected Dictionary<int, List<WeakHandler>> weakHandlersDict;

  // Dispose
  bool disposed;
  readonly WeakHandlerCleanUp cleaner;

  ///<remarks>
  ///<para>
  /// ◾ See also <see cref="WeakHandlerCleanUp"/>.
  ///</para>
  ///<para>
  /// ◾ See also <see cref="WeakHandlerFacility.WeakHandlerCleanUpConstruction(TimeSpan)"/>.
  ///</para>
  /// </remarks>
  /// <exception cref="ArgumentNullException">When <paramref name="cleanUpConstruction"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentException">When <paramref name="cleanUpConstruction"/> returns <see langword="null"/>.</exception>
  public WeakEventCurator ( Func<Dictionary<int, List<WeakHandler>>, WeakHandlerCleanUp> cleanUpConstruction )
  {
    if ( cleanUpConstruction is null )
      throw new ArgumentNullException ( paramName: nameof ( cleanUpConstruction ) );

    Dictionary<int, List<WeakHandler>> weakHandlersDict = new ();
    WeakHandlerCleanUp? cleaner = cleanUpConstruction ( weakHandlersDict );

    if ( cleaner is null )
      throw new ArgumentException ( "Construction must return instance!", paramName: nameof ( cleanUpConstruction ) );

    this.weakHandlersDict = weakHandlersDict;
    this.cleaner = cleaner;
  }

  /// <summary>
  /// Computes composed key made of event parameters.
  /// </summary>  
  virtual protected int HasCode ( object? eventSource, string? eventName ) => HashCode.Combine ( eventSource, eventName );

#nullable disable
  static void ThrowNoHandlerListHeldException ( object eventSource, string eventName )
  {
    const string _null = "null";

    string eSStr = eventSource is null ? _null : eventSource.ToString();

    string eNStr = eventName is null
      ? _null
      : eventName is ""
        ? "\"\""
        : eventName;

    throw new ArgumentException ( $"No handler list held for denoted event source {eSStr} and event name {eNStr}!" );
  }


  static void ValidateHandlers ( Delegate [] handlers )
  {
    if ( handlers is null )
      throw new ArgumentNullException ( paramName: nameof ( handlers ) );

    if ( handlers.Length is 0 )
      throw new ArgumentException ( "Empty handler array!", paramName: nameof ( handlers ) );

    int h_length = handlers.Length;
    for ( int h_i = 0 ; h_i < h_length ; ++h_i )
      if ( handlers [h_i] is null )
        throw new ArgumentException
        (
          $"All handlers must be valid instances, not null! See index {h_i}.",
          paramName: nameof ( handlers )
        );
  }
#nullable restore

  /*
   * *  * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *  * * 
   */

  #region disposal patterns

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
  public void Dispose ()
  {
    if ( disposed )
      return;

    cleaner.Dispose ();

    Dispose ( true );
    GC.SuppressFinalize ( this );

    disposed = true;
  }

  virtual protected void Dispose ( bool disposing ) { }

  async public ValueTask DisposeAsync ()
  {
    if ( disposed )
      return;

    await cleaner.DisposeAsync ();

    await DisposeAsyncCore ();
    GC.SuppressFinalize ( this );

    disposed = true;
  }

  virtual protected ValueTask DisposeAsyncCore () => ValueTask.CompletedTask;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

  #endregion
}
