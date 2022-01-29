using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;
using System.Diagnostics;
using System.Reflection;

using WeakEventCuratorTest.WeakHandlerCleanUpTest.Abstract;

namespace WeakEventCuratorTest.WeakHandlerCleanUpTest;

[TestClass]
sealed public class WeakHandlerCleanUpTests : WeakHandlerCleanUpTests_Shared
{
  override protected Type WeakHandlerCleanUpType => typeof ( WeakHandlerCleanUp );

  [TestMethod]
  public void Interval_IsNotGreaterThanZero__ThrowsArgumentOutOfRangeException ()
  {
    TargetInvocationException tie = Assert.ThrowsException<TargetInvocationException>
    (
      () => WeakHandlerCleanUp ( new (), TimeSpan.Zero )
    );

    Exception innerException = tie.InnerException!;
    Assert.AreEqual ( typeof ( ArgumentOutOfRangeException ), innerException.GetType () );
    Debug.Write ( innerException.Message ); // output message
  }
}
