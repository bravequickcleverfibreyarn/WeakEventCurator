using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;

namespace WeakEventCuratorTest.WeakEventCuratorTest.Abstract;

abstract public class WeakEventCuratorTests_Invoke_Shared : WeakEventCuratorTests_Shared
{
  [TestMethod]
  public void EventSource_IsUnknown__ThrowsArgumentException ()
  {
    using WeakEventCurator wec = WeakEventCurator();

    const string eventName = "eventName";

    wec.Add ( new object (), eventName, string.Intern );
    _ = Assert.ThrowsException<ArgumentException> ( () => wec.Invoke ( new object (), eventName, "" ) );
  }

  [TestMethod]
  public void EventName_IsUnknown__ThrowsArgumentException ()
  {
    using WeakEventCurator wec = WeakEventCurator();
    object eventSource = new();

    wec.Add ( eventSource, "eventName", string.Intern );
    _ = Assert.ThrowsException<ArgumentException> ( () => wec.Invoke ( eventSource, "unknownName", "" ) );
  }

  [TestMethod]
  public void EverythingAsExpected__InvokeWorks ()
  {
    using WeakEventCurator wec = WeakEventCurator();
    WeakEventCuratorTests_InvokeAide aide  = new ();

    object eventSource      = new();
    const string eventName  = "eventName";

    wec.Add ( eventSource, eventName, aide.Increment );
    Assert.AreEqual ( 0, aide.Count );

    const int incrementBy = 5;
    wec.Invoke ( eventSource, eventName, incrementBy );
    Assert.AreEqual ( incrementBy, aide.Count );
  }
}
