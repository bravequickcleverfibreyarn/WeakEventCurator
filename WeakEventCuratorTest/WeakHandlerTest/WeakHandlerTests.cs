using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;

namespace WeakEventCuratorTest.WeakHandlerTest;

[TestClass]
public class WeakHandlerTests
{
  #region IsAlive

  [TestMethod]
  public void IsAlive_NoReferenceToTarget_IsCollected ()
  {
    WeakHandlerTestsAide aide = new ();
    WeakHandler wh = aide.WeakHandler_NewTarget_Handler();

    GC.Collect ();

    Assert.IsFalse (wh.IsAlive);
  }

  [TestMethod]
  public void IsAlive_ReferenceToTargetExists_IsNotCollected ()
  {
    WeakHandlerTestsAide aide = new ();
    WeakHandler weakHandler = aide.WeakHandler_ExistingTarget_Handler();

    GC.Collect ();

    Assert.IsTrue (weakHandler.IsAlive);
  }

  [TestMethod]
  public void IsAlive_StaticHandler_ConsideredAsAlive ()
  {
    WeakHandlerTestsAide.StaticModel staticHandler = new ();
    WeakHandler weakHandler = staticHandler.WeakHandler_Handler();

    Assert.IsTrue (weakHandler.IsAlive);
  }

  #endregion
  #region Equals

  [TestMethod]
  public void Equals_TargetIsDead_NotEqual () // Can be merged with different target tests but why not.
  {
    WeakHandlerTestsAide aide = new ();

    WeakHandler wh = aide.WeakHandler_NewTarget_Handler();
    GC.Collect ();

    Assert.IsFalse (wh.Equals (aide.ExistingTarget_Handler));
  }

  [TestMethod]
  public void Equals_DifferentHandlers_NotEqual ()
  {
    WeakHandlerTestsAide aide = new ();
    WeakHandler wh = aide.WeakHandler_ExistingTarget_Handler ();
    
    Assert.IsFalse (wh.Equals (aide.ExistingTarget_Handler__Other));
  }

  [TestMethod]
  public void Equals_DifferentTargets_NotEqual ()
  {
    WeakHandlerTestsAide aide = new ();
    WeakHandler wh = aide.WeakHandler_ExistingTarget_Handler ();

    Assert.IsFalse (wh.Equals (aide.NewTarget_Handler));
  }

  [TestMethod]
  public void Equals_SameHandlers_Equals ()
  {
    WeakHandlerTestsAide aide = new ();
    WeakHandler wh = aide.WeakHandler_ExistingTarget_Handler();
    
    Assert.IsTrue (wh.Equals (aide.ExistingTarget_Handler));
  }

  [TestMethod]
  public void Equals__StaticHandler_SameHandlers__Equals ()
  {
    WeakHandlerTestsAide.StaticModel staticHandler = new ();
    WeakHandler wh = staticHandler.WeakHandler_Handler();
    
    Assert.IsTrue (wh.Equals (staticHandler.Delegate_Handler));
  }

  [TestMethod]
  public void Equals__StaticHandler_DifferentHandlers__NotEqual () // Can be removed since verifies same path as Equals_DifferentHandlers_NotEqual.
  {
    WeakHandlerTestsAide.StaticModel staticHandler1 = new ();
    WeakHandler wh = staticHandler1.WeakHandler_Handler();

    Assert.IsFalse (wh.Equals (staticHandler1.Delegate_Handler__Other));
  }

  #endregion
  #region Invoke

  [TestMethod]
  public void Invoke_TargetIsDead_NoException ()
  {
    WeakHandlerTestsAide aide = new ();
    WeakHandler wh = aide.WeakHandler_NewTarget_Handler ();

    GC.Collect ();
        
    wh.Invoke ();
    Assert.IsTrue (true);
  }

  [TestMethod]
  public void Invoke_TargetIsAlive_HandlerIvoked ()
  {
    WeakHandlerTestsAide aide = new ();
    WeakHandler wh = aide.WeakHandler_ExistingTarget_Handler();

    WeakHandlerTestsAide.TargetModel target = aide.Target;

    Assert.AreEqual (0, target.TestCount);
    wh.Invoke ();
    Assert.AreEqual (1, target.TestCount);
  }

  [TestMethod]
  public void Invoke_StaticHandler_HandlerIvoked ()
  {
    WeakHandlerTestsAide.StaticModel staticHandler = new ();
    WeakHandler weakHandler = staticHandler.WeakHandler_Handler();

    Assert.AreEqual (0, staticHandler.TestCount);
    weakHandler.Invoke (staticHandler);
    Assert.AreEqual (1, staticHandler.TestCount);
  }

  #endregion
}
