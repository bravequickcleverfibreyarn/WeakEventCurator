using Software9119.WeakEvent;

using System;
using System.Collections.Generic;

namespace WeakEventCuratorTest.WeakHandlerCleanUpTest.Distinction;
sealed class WeakHandlerCleanUp__Inherited : WeakHandlerCleanUp
{
  public WeakHandlerCleanUp__Inherited ( Dictionary<int, List<WeakHandler>> handlerLists, TimeSpan interval )
  : base ( handlerLists, interval )
  { }

  override protected void ClearHandlersActual ( Dictionary<int, List<WeakHandler>> handlerLists ) => base.ClearHandlersActual ( handlerLists );
}
