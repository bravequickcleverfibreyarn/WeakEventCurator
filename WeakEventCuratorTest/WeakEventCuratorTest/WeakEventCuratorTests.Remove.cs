using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;
using System.Diagnostics;

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

    ArgumentException ae = Assert.ThrowsException<ArgumentException> ( () => Remove () );
    Debug.Write ( ae.Message ); // output message

    void Remove () => wec.Remove ( eventSource, eventName, handler );
  }

  [TestMethod]
  public void DoubleHandlerAddition__ThrowsArgumentException ()
  {
    object eventSource      = new();
    const string eventName  = "eventName";
    Delegate handler        = string.Intern;

    using WeakEventCurator wec = WeakEventCurator();

    wec.Add ( eventSource, eventName, handler, handler );
    ArgumentException ae = Assert.ThrowsException<ArgumentException> ( () => wec.Remove ( eventSource, eventName, handler ) );
    Debug.Write ( ae.Message ); // output message
  }

  [TestMethod]
  public void EventSourceNotKnown__ThrowsArgumentException ()
  {
    const string eventName  = "eventName";
    Delegate handler        = string.Intern;

    using WeakEventCurator wec = WeakEventCurator();
    wec.Add ( new object (), eventName, handler );

    ArgumentException ae = Assert.ThrowsException<ArgumentException> ( () => wec.Remove ( new object (), eventName, handler ) );
    Debug.Write ( ae.Message ); // output message
  }

  [TestMethod]
  public void EventNameNotKnown__ThrowsArgumentException ()
  {
    object eventSource  = new();
    Delegate handler    = string.Intern;

    using WeakEventCurator wec = WeakEventCurator();
    wec.Add ( eventSource, "eventName", handler );

    ArgumentException ae = Assert.ThrowsException<ArgumentException> ( () => wec.Remove ( eventSource, "unknownName", handler ) );
    Debug.Write ( ae.Message ); // output message
  }

  [TestMethod]
  public void HandlerNotKnown__ThrowsArgumentException ()
  {
    object eventSource      = new();
    const string eventName  = "eventName";

    using WeakEventCurator wec = WeakEventCurator();
    wec.Add ( eventSource, eventName, string.Intern );
    ArgumentException ae = Assert.ThrowsException<ArgumentException> ( () => wec.Remove ( eventSource, eventName, string.IsInterned ) );
    Debug.Write ( ae.Message ); // output message
  }
}
