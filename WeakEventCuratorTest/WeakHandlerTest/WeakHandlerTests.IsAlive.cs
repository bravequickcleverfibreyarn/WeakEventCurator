using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;

namespace WeakEventCuratorTest.WeakHandlerTest;

[TestClass]
public class WeakHandlerTests_IsAlive
{
  [TestMethod]
  public void NoReferenceToTarget__IsCollected ()
  {
    WeakHandlerTestsAide aide = new ();
    WeakHandler wh = aide.WeakHandler_NewTarget_Handler();

    GC.Collect ();

    Assert.IsFalse (wh.IsAlive);
  }

  [TestMethod]
  public void ReferenceToTargetExists__IsNotCollected ()
  {
    WeakHandlerTestsAide aide = new ();
    WeakHandler weakHandler = aide.WeakHandler_ExistingTarget_Handler();

    GC.Collect ();

    Assert.IsTrue (weakHandler.IsAlive);
  }

  [TestMethod]
  public void StaticHandler__ConsideredAsAlive ()
  {
    WeakHandlerTestsAide.StaticModel staticHandler = new ();
    WeakHandler weakHandler = staticHandler.WeakHandler_Handler();

    Assert.IsTrue (weakHandler.IsAlive);
  }
}
