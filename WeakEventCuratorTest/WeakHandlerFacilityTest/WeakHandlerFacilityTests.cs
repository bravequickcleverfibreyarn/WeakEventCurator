using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;

namespace WeakEventCuratorTest.WeakHandlerFacilityTest;

[TestClass]
sealed public class WeakHandlerFacilityTests
{
  [TestMethod]
  public void WeakHandlerSafe__DelegateIsNull__ThrowsArgumentNullException ()
  {
    _ = Assert.ThrowsException<ArgumentNullException> ( () => WeakHandlerFacility.WeakHandlerSafe ( null! ) );
  }

  [TestMethod]
  public void WeakHandlerSafe__DelegateIsOk__GetsWeakHandler ()
  {
    WeakHandler wh = WeakHandlerFacility.WeakHandlerSafe ( string.Intern );

    Assert.AreEqual ( typeof ( WeakHandler ), wh.GetType () );
  }

  [TestMethod]
  public void WeakHandler__DelegateIsNull__ThrowsNullReferenceException ()
  {
    _ = Assert.ThrowsException<NullReferenceException> ( () => WeakHandlerFacility.WeakHandler ( null! ) );
  }

  [TestMethod]
  public void WeakHandler__DelegateIsOk__GetsWeakHandler ()
  {
    WeakHandler wh = WeakHandlerFacility.WeakHandler ( string.Intern );

    Assert.AreEqual ( typeof ( WeakHandler ), wh.GetType () );
  }
}
