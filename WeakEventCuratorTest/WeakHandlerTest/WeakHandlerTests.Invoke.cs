using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;

namespace WeakEventCuratorTest.WeakHandlerTest;

[TestClass]
public class WeakHandlerTests_Invoke
{
  [TestMethod]
  public void TargetIsDead__NoException ()
  {
    WeakHandlerTestsAide aide = new ();
    WeakHandler wh = aide.WeakHandler_NewTarget_Handler ();

    GC.Collect ();

    wh.Invoke ();
    Assert.IsTrue (true);
  }

  [TestMethod]
  public void TargetIsAlive__HandlerIvoked ()
  {
    WeakHandlerTestsAide aide = new ();
    WeakHandler wh = aide.WeakHandler_ExistingTarget_Handler();

    WeakHandlerTestsAide.TargetModel target = aide.Target;

    Assert.AreEqual (0, target.TestCount);
    wh.Invoke ();
    Assert.AreEqual (1, target.TestCount);
  }

  [TestMethod]
  public void StaticHandler__HandlerIvoked ()
  {
    WeakHandlerTestsAide.StaticModel staticHandler = new ();
    WeakHandler weakHandler = staticHandler.WeakHandler_Handler();

    Assert.AreEqual (0, staticHandler.TestCount);
    weakHandler.Invoke (staticHandler);
    Assert.AreEqual (1, staticHandler.TestCount);
  }
}
