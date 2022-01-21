using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;

namespace WeakEventCuratorTest.WeakEventCuratorTest;

[TestClass]
public class WeakEventCuratorTests_Invoke
{
  [TestMethod]
  public void EventSource_IsNull__ThrowsArgumentNullException ()
  {
    using WeakEventCurator weakEventCurator = new ( default );

    _ = Assert.ThrowsException<ArgumentNullException> ( () => weakEventCurator.Invoke ( null!, "", default ) );
  }

  [TestMethod]
  public void EventName_IsNull__ThrowsArgumentNullException ()
  {
    using WeakEventCurator weakEventCurator = new ( default );

    _ = Assert.ThrowsException<ArgumentNullException> ( () => weakEventCurator.Invoke ( new object (), null!, default ) );
  }

  [TestMethod]
  public void EventSource_IsUnknown__ThrowsArgumentException ()
  {
    using WeakEventCurator weakEventCurator = new ( default );

    const string eventName = "eventName";

    weakEventCurator.Add ( new object (), eventName, string.Intern );
    _ = Assert.ThrowsException<ArgumentException> ( () => weakEventCurator.Invoke ( new object (), eventName, "" ) );
  }

  [TestMethod]
  public void EventName_IsUnknown__ThrowsArgumentException ()
  {
    using WeakEventCurator weakEventCurator = new ( default );
    object eventSource = new();

    weakEventCurator.Add ( eventSource, "eventName", string.Intern );
    _ = Assert.ThrowsException<ArgumentException> ( () => weakEventCurator.Invoke ( eventSource, "unknownName", "" ) );
  }

  [TestMethod]
  public void EverythingAsExpected__InvokeWorks ()
  {
    using WeakEventCurator weakEventCurator = new ( default );
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
