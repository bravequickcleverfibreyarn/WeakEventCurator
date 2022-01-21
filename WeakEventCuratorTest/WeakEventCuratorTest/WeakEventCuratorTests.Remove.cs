using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;

using WeakEventCuratorTest.WeakEventCuratorTest.Abstract;

namespace WeakEventCuratorTest.WeakEventCuratorTest;

[TestClass]
public class WeakEventCuratorTests_Remove : WeakEventCuratorTests_AddRemove_Shared
{
  sealed override protected AddRemove AddRemoveDelegate => WeakEventCurator ().Remove;

  override protected Type WeakEventCuratorType { get; } = typeof ( WeakEventCurator );

  [TestMethod]
  public void EverythingAsExpected__HandlerRemoved ()
  {
    object eventSource      = new();
    const string eventName  = "eventName";
    Delegate handler        = string.Intern;

    using WeakEventCurator wec = WeakEventCurator();

    wec.Add ( eventSource, eventName, handler );
    Remove ();

    _ = Assert.ThrowsException<ArgumentException> ( () => Remove () );

    void Remove () => wec.Remove ( eventSource, eventName, handler );
  }

  [TestMethod]
  public void EventSourceNotKnown__ThrowsArgumentException ()
  {
    const string eventName  = "eventName";
    Delegate handler        = string.Intern;

    using WeakEventCurator wec = WeakEventCurator();
    wec.Add ( new object (), eventName, handler );

    _ = Assert.ThrowsException<ArgumentException> ( () => wec.Remove ( new object (), eventName, handler ) );
  }

  [TestMethod]
  public void EventNameNotKnown__ThrowsArgumentException ()
  {
    object eventSource  = new();
    Delegate handler    = string.Intern;

    using WeakEventCurator wec = WeakEventCurator();
    wec.Add ( eventSource, "eventName", handler );

    _ = Assert.ThrowsException<ArgumentException> ( () => wec.Remove ( eventSource, "unknownName", handler ) );
  }

  [TestMethod]
  public void HandlerNotKnown__ThrowsArgumentException ()
  {
    object eventSource      = new();
    const string eventName  = "eventName";

    using WeakEventCurator wec = WeakEventCurator();
    wec.Add ( eventSource, eventName, string.Intern );
    _ = Assert.ThrowsException<ArgumentException> ( () => wec.Remove ( eventSource, eventName, string.IsInterned ) );
  }
}
