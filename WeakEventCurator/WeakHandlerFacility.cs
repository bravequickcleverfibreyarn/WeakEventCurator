﻿using System;
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
  [MethodImpl ( MethodImplOptions.AggressiveInlining )]
  static public WeakHandler WeakHandlerSafe ( Delegate handler )
    => handler is null
      ? throw new ArgumentNullException ( nameof ( handler ) )
      : new WeakHandler ( handler );

  /// <summary>
  /// Use to get new <see cref="WeakEvent.WeakHandler"/>.
  /// </summary>  
  [MethodImpl ( MethodImplOptions.AggressiveInlining )]
  static public WeakHandler WeakHandler ( Delegate handler ) => new ( handler );
}
