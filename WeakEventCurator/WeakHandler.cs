
using System;
using System.Reflection;

namespace Software9119.WeakEvent;

class WeakHandler
{
  readonly WeakReference weakTarget;
  readonly MethodInfo handlerInfo;

  public WeakHandler ( Delegate handler )
  {
    weakTarget = new WeakReference (handler.Target);
    handlerInfo = handler.Method;
  }

  public bool IsAlive => weakTarget.IsAlive || handlerInfo.IsStatic;

  public bool Equals ( Delegate del ) => weakTarget.Target == del.Target && handlerInfo.Name == del.Method.Name;

  public void Invoke ( params object [] parameters )
  {
    object? target = weakTarget.Target;
    if (target is null && !handlerInfo.IsStatic)
      return;

    _ = handlerInfo.Invoke (target, parameters);
  }
}
