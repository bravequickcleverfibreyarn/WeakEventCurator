using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace WeakEventCuratorTest.WeakEventCuratorTest;

[TestClass]
sealed public class FrameworkBehavorialTests
{
  [TestMethod]
  public void SystemHashCodeComputesWithNulls ()
  {
    int hash1 = Hash();
    int hash2 = Hash();

    Assert.AreEqual ( hash1, hash2 );

    static int Hash () => HashCode.Combine<object, string> ( null!, null! ); // Must not throw.
  }
}
