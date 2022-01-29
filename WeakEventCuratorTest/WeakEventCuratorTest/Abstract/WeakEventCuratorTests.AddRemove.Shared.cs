using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Diagnostics;

namespace WeakEventCuratorTest.WeakEventCuratorTest.Abstract;

abstract public class WeakEventCuratorTests_AddRemove_Shared : WeakEventCuratorTests_Shared
{

  protected delegate void AddRemove ( object? eventSource, string? eventName, params Delegate [] handlers );

  abstract protected AddRemove AddRemoveDelegate { get; }


  [TestMethod]
  public void Handlers_IsNull__ThrowsArgumentNullException ()
  {
    AddRemove del = AddRemoveDelegate;
    ArgumentNullException ane = Assert.ThrowsException<ArgumentNullException> ( () => del ( new object (), "", null! ) );
    Debug.Write ( ane.Message ); // output message

    ((IDisposable) del.Target!).Dispose ();
  }

  [TestMethod]
  public void Handlers_IsEmpty__ThrowsArgumentException ()
  {
    AddRemove del = AddRemoveDelegate;
    ArgumentException ae = Assert.ThrowsException<ArgumentException> ( () => del ( new object (), "", new Delegate [0] ) );
    Debug.Write ( ae.Message ); // output message

    ((IDisposable) del.Target!).Dispose ();
  }

  [TestMethod]
  public void Handlers_HasNullDelegate__ThrowsArgumentException ()
  {
    AddRemove del = AddRemoveDelegate;
    ArgumentException ae  = Assert.ThrowsException<ArgumentException> ( () => del ( new object (), "", string.IsNullOrWhiteSpace, null! ) );
    Debug.Write ( ae.Message ); // output message

    ((IDisposable) del.Target!).Dispose ();
  }
}
