
using System;
using System.Reflection;

namespace Software9119.WeakEvent;

/// <summary>
/// Employs <see cref="WeakReference"/> for instance handlers. Static handlers are supported also.
/// </summary>
sealed public class WeakHandler
{
  readonly WeakReference? weakTarget;
  readonly MethodInfo handlerInfo;

  internal WeakHandler ( Delegate handler )
  {
    MethodInfo handlerInfo = handler.Method;
    this.handlerInfo = handlerInfo;
    if ( handlerInfo.IsStatic )
      return;

    weakTarget = new WeakReference ( handler.Target );
  }

  internal bool IsAlive => weakTarget?.IsAlive == true || handlerInfo.IsStatic;

  internal bool Equals ( Delegate del )
  {
    bool sameHandlers = handlerInfo == del.Method;

    if ( sameHandlers )
    {
      if ( handlerInfo.IsStatic )
        return true;

      return weakTarget!.Target == del.Target;
    }

    return false;
  }

  internal void Invoke ( params object? []? parameters )
  {
    object? target;
    if ( handlerInfo.IsStatic )
      target = null;
    else
    {
      target = weakTarget!.Target;

      if ( target is null )
        return;
    }

    _ = handlerInfo.Invoke ( target, parameters );
  }
}
