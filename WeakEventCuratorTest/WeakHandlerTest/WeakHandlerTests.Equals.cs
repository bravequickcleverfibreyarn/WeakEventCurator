using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;

namespace WeakEventCuratorTest.WeakHandlerTest;

[TestClass]
sealed public class WeakHandlerTests_Equals
{
  [TestMethod]
  public void TargetIsDead__NotEqual () // Can be merged with different target tests but why not.
  {
    WeakHandlerTestsAide aide = new ();

    WeakHandler wh = aide.WeakHandler_NewTarget_Handler();
    GC.Collect ();

    Assert.IsFalse ( wh.Equals ( aide.ExistingTarget_Handler ) );
  }

  [TestMethod]
  public void DifferentHandlers__NotEqual ()
  {
    WeakHandlerTestsAide aide = new ();
    WeakHandler wh = aide.WeakHandler_ExistingTarget_Handler ();

    Assert.IsFalse ( wh.Equals ( aide.ExistingTarget_Handler__Other ) );
  }

  [TestMethod]
  public void DifferentTargets__NotEqual ()
  {
    WeakHandlerTestsAide aide = new ();
    WeakHandler wh = aide.WeakHandler_ExistingTarget_Handler ();

    Assert.IsFalse ( wh.Equals ( aide.NewTarget_Handler ) );
  }

  [TestMethod]
  public void SameHandlers__Equals ()
  {
    WeakHandlerTestsAide aide = new ();
    WeakHandler wh = aide.WeakHandler_ExistingTarget_Handler();

    Assert.IsTrue ( wh.Equals ( aide.ExistingTarget_Handler ) );
  }

  [TestMethod]
  public void StaticHandler_SameHandlers__Equals ()
  {
    WeakHandlerTestsAide.StaticModel staticHandler = new ();
    WeakHandler wh = staticHandler.WeakHandler_Handler();

    Assert.IsTrue ( wh.Equals ( staticHandler.Delegate_Handler ) );
  }

  [TestMethod]
  public void StaticHandler_DifferentHandlers__NotEqual () // Can be removed since verifies same path as DifferentHandlers_NotEqual.
  {
    WeakHandlerTestsAide.StaticModel staticHandler1 = new ();
    WeakHandler wh = staticHandler1.WeakHandler_Handler();

    Assert.IsFalse ( wh.Equals ( staticHandler1.Delegate_Handler__Other ) );
  }
}
