using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace WeakEventCuratorTest.WeakEventCuratorTest.Abstract;

abstract public class WeakEventCuratorTests_AddRemove_Shared : WeakEventCuratorTests_Shared
{

  protected delegate void AddRemoveMethod ( object eventSource, string eventName, params Delegate [] handlers );

  abstract protected AddRemoveMethod OneOfAddRemoveMethods { get; }

  // Execution

  [TestMethod]
  public void Handlers_IsNull__ThrowsArgumentNullException ()
  {
    AddRemoveMethod method = OneOfAddRemoveMethods;
    _ = Assert.ThrowsException<ArgumentNullException> ( () => method ( new object (), "", null! ) );
    ((IDisposable) method.Target!).Dispose ();
  }

  [TestMethod]
  public void Handlers_IsEmpty__ThrowsArgumentException ()
  {
    AddRemoveMethod method = OneOfAddRemoveMethods;
    _ = Assert.ThrowsException<ArgumentException> ( () => method ( new object (), "", new Delegate [0] ) );
    ((IDisposable) method.Target!).Dispose ();
  }

  [TestMethod]
  public void Handlers_HasNullDelegate__ThrowsArgumentException ()
  {
    AddRemoveMethod method = OneOfAddRemoveMethods;
    _ = Assert.ThrowsException<ArgumentException> ( () => method ( new object (), "", string.IsNullOrWhiteSpace, null! ) );
    ((IDisposable) method.Target!).Dispose ();
  }
}
