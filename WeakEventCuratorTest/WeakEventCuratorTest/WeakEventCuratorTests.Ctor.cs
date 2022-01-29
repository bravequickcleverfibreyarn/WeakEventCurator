using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;
using System.Diagnostics;

namespace WeakEventCuratorTest.WeakEventCuratorTest;

[TestClass]
sealed public class WeakEventCuratorTests
{
  [TestMethod]
  public void CleanUpConstruction_IsNull__ThrowsArgumentNullException ()
  {
    ArgumentNullException ane = Assert.ThrowsException<ArgumentNullException> ( () => new WeakEventCurator ( null! ) );
    Debug.Write ( ane.Message ); // output message
  }

  [TestMethod]
  public void CleanUpConstruction_ReturnsNull__ThrowsArgumentException ()
  {
    ArgumentException ae = Assert.ThrowsException<ArgumentException> ( () => new WeakEventCurator ( x => null! ) );
    Debug.Write ( ae.Message ); // output message
  }
}
