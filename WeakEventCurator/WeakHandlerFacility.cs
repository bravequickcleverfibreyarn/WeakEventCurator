using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Software9119.WeakEvent;

/// <summary>
/// Helper model with auxiliary methods.
/// </summary>
static public class WeakHandlerFacility
{
  /// <summary>
  /// Use to get new <see cref="WeakEvent.WeakHandler"/>.
  /// </summary>
  /// <exception cref="ArgumentNullException">When <paramref name="handler"/> is <see langword="null"/>.</exception>  
  static public WeakHandler WeakHandlerSafe ( Delegate handler )
    => handler is null
      ? throw new ArgumentNullException ( nameof ( handler ) )
      : new WeakHandler ( handler );

  /// <summary>
  /// Use to get new <see cref="WeakEvent.WeakHandler"/>.
  /// </summary>  
  [MethodImpl ( MethodImplOptions.AggressiveInlining )]
  static public WeakHandler WeakHandler ( Delegate handler ) => new ( handler );

  /// <summary>Use to compare <see cref="WeakEvent.WeakHandler"/> against <see cref="Delegate"/>.</summary>
  /// <exception cref="ArgumentNullException">When any of <paramref name="wh"/>, <paramref name="del"/> is <see langword="null"/>.</exception>
  static public bool EqualsSafe ( this WeakHandler wh, Delegate del )
  {
    if ( wh is null ) throw new ArgumentNullException ( nameof ( wh ) );
    if ( del is null ) throw new ArgumentNullException ( nameof ( del ) );

    return wh.Equals ( del );
  }

  /// <summary>Use to compare <see cref="WeakEvent.WeakHandler"/> against <see cref="Delegate"/>.</summary>
  [MethodImpl ( MethodImplOptions.AggressiveInlining )]
  static public bool Equals ( this WeakHandler wh, Delegate del ) => wh.Equals ( del );

  /// <summary>
  /// Use to get <see cref="WeakHandlerCleanUp"/> construction function.
  /// </summary>  
  static public Func<Dictionary<int, List<WeakHandler>>, WeakHandlerCleanUp> WeakHandlerCleanUpConstruction ( TimeSpan cleanUpInterval )
    => handlers => new WeakHandlerCleanUp ( handlers, cleanUpInterval );
}
