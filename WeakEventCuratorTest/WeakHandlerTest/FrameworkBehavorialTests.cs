using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace WeakEventCuratorTest.WeakHandlerTest;

[TestClass]
sealed public class FrameworkBehavorialTests
{
  [TestMethod]
  public void DifferentTargetsSameHandlers__EqualHandlers_NonEqualDelegates ()
  {
    WeakHandlerTestsAide aide = new ();

    Delegate t1d1 = aide.ExistingTarget_Handler;
    Delegate t2d1 = aide.NewTarget_Handler();

    Assert.AreNotEqual ( t1d1, t2d1 );
    Assert.AreEqual ( t1d1.Method, t2d1.Method );
  }

  [TestMethod]
  public void SameTargetsSameHandlers__EqualDelegates ()
  {
    WeakHandlerTestsAide aide = new ();

    Delegate h1_1 = aide.ExistingTarget_Handler;
    Delegate h1_2 = aide.ExistingTarget_Handler;

    Assert.AreEqual ( h1_1, h1_2 );
    Assert.IsFalse ( ReferenceEquals ( h1_1, h1_2 ) );
  }
}