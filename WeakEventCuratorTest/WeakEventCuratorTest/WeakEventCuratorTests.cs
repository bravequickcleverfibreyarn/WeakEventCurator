using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;

namespace WeakEventCuratorTest.WeakEventCuratorTest;

[TestClass]
sealed public class WeakEventCuratorTests
{
  [TestMethod]
  public void CleanUpConstruction_IsNull__ThrowsArgumentNullException ()
  {
    _ = Assert.ThrowsException<ArgumentNullException>
    (
      () => new WeakEventCurator ( null! )
    );
  }

  [TestMethod]
  public void CleanUpConstruction_ReturnsNull__ThrowsArgumentException ()
  {
    _ = Assert.ThrowsException<ArgumentException>
    (
      () => new WeakEventCurator ( x => null! )
    );
  }
}
