using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;

namespace WeakEventCuratorTest.WeakEventCuratorTest;

[TestClass]
sealed public class WeakEventCuratorTests_Invoke
{
  [TestMethod]
  public void EventSource_IsUnknown__ThrowsArgumentException ()
  {
    using WeakEventCurator weakEventCurator = new ( x => new WeakHandlerCleanUp ( x, TimeSpan.FromDays(1)) );

    const string eventName = "eventName";

    weakEventCurator.Add ( new object (), eventName, string.Intern );
    _ = Assert.ThrowsException<ArgumentException> ( () => weakEventCurator.Invoke ( new object (), eventName, "" ) );
  }

  [TestMethod]
  public void EventName_IsUnknown__ThrowsArgumentException ()
  {
    using WeakEventCurator weakEventCurator = new ( x => new WeakHandlerCleanUp ( x, TimeSpan.FromDays(1) ));
    object eventSource = new();

    weakEventCurator.Add ( eventSource, "eventName", string.Intern );
    _ = Assert.ThrowsException<ArgumentException> ( () => weakEventCurator.Invoke ( eventSource, "unknownName", "" ) );
  }

  [TestMethod]
  public void EverythingAsExpected__InvokeWorks ()
  {
    using WeakEventCurator weakEventCurator = new ( x => new WeakHandlerCleanUp ( x, TimeSpan.FromDays(1) ));
    WeakEventCuratorTests_InvokeAide aide  = new ();

    object eventSource      = new();
    const string eventName  = "eventName";

    weakEventCurator.Add ( eventSource, eventName, aide.Increment );
    Assert.AreEqual ( 0, aide.Count );

    const int incrementBy = 5;
    weakEventCurator.Invoke ( eventSource, eventName, incrementBy );
    Assert.AreEqual ( incrementBy, aide.Count );
  }
}
