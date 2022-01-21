
using System;
using System.Reflection;

namespace Software9119.WeakEvent;

class WeakHandler
{
  readonly WeakReference? weakTarget;
  readonly MethodInfo handlerInfo;

  public WeakHandler ( Delegate handler )
  {
    MethodInfo handlerInfo = handler.Method;
    this.handlerInfo = handlerInfo;
    if (handlerInfo.IsStatic)
      return;

    weakTarget = new WeakReference (handler.Target);
  }

  public bool IsAlive => weakTarget?.IsAlive == true || handlerInfo.IsStatic;
    
  public bool Equals ( Delegate del )
  {
    bool sameHandlers = handlerInfo == del.Method;

    if (sameHandlers)
    {
      if (handlerInfo.IsStatic)
        return true;

      return weakTarget!.Target == del.Target;
    }

    return false;
  }

  public void Invoke ( params object [] parameters )
  {
    object? target;
    if (handlerInfo.IsStatic)
      target = null;
    else
    {
      target = weakTarget!.Target;

      if (target is null)
        return;
    }

    _ = handlerInfo.Invoke (target, parameters);
  }
}
