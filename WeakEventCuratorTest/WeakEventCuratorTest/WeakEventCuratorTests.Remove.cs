using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

using WeakEventCuratorTest.WeakEventCuratorTest.Abstract;

namespace WeakEventCuratorTest.WeakEventCuratorTest;

[TestClass]
sealed public class WeakEventCuratorTests_Remove : WeakEventCuratorTests_Shared
{
  [ClassInitialize]
  static new public void Init ( TestContext tc ) => WeakEventCuratorTests_Shared.Init ( tc );
  [ClassCleanup]
  static new public void Cleansing () => WeakEventCuratorTests_Shared.Cleansing ();

  override protected AddRemoveMethod OneOfAddRemoveMethods => WeakEventCurator.Remove;

  [TestMethod]
  public void EverythingAsExpected__HandlerRemoved ()
  {
    object eventSource      = new();
    const string eventName  = "eventName";
    Delegate handler        = string.Intern;

    WeakEventCurator.Add ( eventSource, eventName, handler );
    Remove ();

    _ = Assert.ThrowsException<ArgumentException> ( () => Remove () );

    void Remove () => WeakEventCurator.Remove ( eventSource, eventName, handler );
  }

  [TestMethod]
  public void EventSourceNotKnown__ThrowsArgumentException ()
  {
    const string eventName  = "eventName";
    Delegate handler        = string.Intern;

    WeakEventCurator.Add ( new object (), eventName, handler );

    _ = Assert.ThrowsException<ArgumentException> ( () => WeakEventCurator.Remove ( new object (), eventName, handler ) );
  }

  [TestMethod]
  public void EventNameNotKnown__ThrowsArgumentException ()
  {
    object eventSource  = new();
    Delegate handler    = string.Intern;

    WeakEventCurator.Add ( eventSource, "eventName", handler );

    _ = Assert.ThrowsException<ArgumentException> ( () => WeakEventCurator.Remove ( eventSource, "unknownName", handler ) );
  }

  [TestMethod]
  public void HandlerNotKnown__ThrowsArgumentException ()
  {
    object eventSource      = new();
    const string eventName  = "eventName";

    WeakEventCurator.Add ( eventSource, eventName, string.Intern );
    _ = Assert.ThrowsException<ArgumentException> ( () => WeakEventCurator.Remove ( eventSource, eventName, string.IsInterned ) );
  }
}
