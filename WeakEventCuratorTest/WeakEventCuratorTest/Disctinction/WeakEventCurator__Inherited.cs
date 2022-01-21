using Software9119.WeakEvent;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeakEventCuratorTest.WeakEventCuratorTest.Disctinction;
sealed class WeakEventCurator__Inherited : WeakEventCurator
{
  public WeakEventCurator__Inherited ( Func<Dictionary<int, List<WeakHandler>>, WeakHandlerCleanUp> cleanUpConstruction )
  : base ( cleanUpConstruction )
  {
  }

  override protected void AddActual ( object? eventSource, string? eventName, params Delegate [] handlers ) => base.AddActual ( eventSource, eventName, handlers );
  override protected void RemoveActual ( object? eventSource, string? eventName, params Delegate [] handlers ) => base.RemoveActual ( eventSource, eventName, handlers );
  override protected void InvokeActual ( object? eventSource, string? eventName, params object? []? parameters ) => base.InvokeActual ( eventSource, eventName, parameters );
  override protected int HasCode ( object? eventSource, string? eventName ) => base.HasCode ( eventSource, eventName );

  override protected void Dispose ( bool disposing ) => base.Dispose ( disposing );
  override protected ValueTask DisposeAsyncCore () => base.DisposeAsyncCore ();
}
