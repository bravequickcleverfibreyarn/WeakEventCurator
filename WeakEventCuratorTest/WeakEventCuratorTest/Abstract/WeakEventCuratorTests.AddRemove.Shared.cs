using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace WeakEventCuratorTest.WeakEventCuratorTest.Abstract;

abstract public class WeakEventCuratorTests_AddRemove_Shared : WeakEventCuratorTests_Shared
{

  protected delegate void AddRemove ( object eventSource, string eventName, params Delegate [] handlers );

  abstract protected AddRemove AddRemoveDelegate { get; }

  // Execution

  [TestMethod]
  public void Handlers_IsNull__ThrowsArgumentNullException ()
  {
    AddRemove del = AddRemoveDelegate;
    _ = Assert.ThrowsException<ArgumentNullException> ( () => del ( new object (), "", null! ) );
    ((IDisposable) del.Target!).Dispose ();
  }

  [TestMethod]
  public void Handlers_IsEmpty__ThrowsArgumentException ()
  {
    AddRemove del = AddRemoveDelegate;
    _ = Assert.ThrowsException<ArgumentException> ( () => del ( new object (), "", new Delegate [0] ) );
    ((IDisposable) del.Target!).Dispose ();
  }

  [TestMethod]
  public void Handlers_HasNullDelegate__ThrowsArgumentException ()
  {
    AddRemove del = AddRemoveDelegate;
    _ = Assert.ThrowsException<ArgumentException> ( () => del ( new object (), "", string.IsNullOrWhiteSpace, null! ) );
    ((IDisposable) del.Target!).Dispose ();
  }
}
