using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;

namespace WeakEventCuratorTest.WeakEventCuratorTest.Abstract;

abstract public class WeakEventCuratorTests_Shared
{

  // Class  set up

  [ClassInitialize]
  static public void Init ( TestContext _ ) => WeakEventCurator = new WeakEventCurator ( default );
  [ClassCleanup]
  static public void Cleansing () => WeakEventCurator.Dispose ();

  // Protected requirement

  protected delegate void AddRemoveMethod ( object eventSource, string eventName, params Delegate [] handlers );
  abstract protected AddRemoveMethod OneOfAddRemoveMethods { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  static protected WeakEventCurator WeakEventCurator { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

  // Execution

  [TestMethod]
  public void EventSource_IsNull__ThrowsArgumentNullException ()
  {
    _ = Assert.ThrowsException<ArgumentNullException> ( () => OneOfAddRemoveMethods ( null!, "", string.IsNullOrWhiteSpace ) );
  }

  [TestMethod]
  public void EventName_IsNull__ThrowsArgumentNullException ()
  {
    _ = Assert.ThrowsException<ArgumentNullException> ( () => OneOfAddRemoveMethods ( new object (), null!, string.IsNullOrWhiteSpace ) );
  }

  [TestMethod]
  public void Handlers_IsNull__ThrowsArgumentNullException ()
  {
    _ = Assert.ThrowsException<ArgumentNullException> ( () => OneOfAddRemoveMethods ( new object (), "", null! ) );
  }

  [TestMethod]
  public void Handlers_IsEmpty__ThrowsArgumentException ()
  {
    _ = Assert.ThrowsException<ArgumentException> ( () => OneOfAddRemoveMethods ( new object (), "", new Delegate [0] ) );
  }

  [TestMethod]
  public void Handlers_HasNullDelegate__ThrowsArgumentException ()
  {
    _ = Assert.ThrowsException<ArgumentException> ( () => OneOfAddRemoveMethods ( new object (), "", string.IsNullOrWhiteSpace, null! ) );
  }
}
