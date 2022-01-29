using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WeakEventCuratorTest.WeakHandlerFacilityTest;

[TestClass]
sealed public class WeakHandlerFacilityTests
{
  [TestMethod]
  public void WeakHandlerSafe__DelegateIsNull__ThrowsArgumentNullException ()
  {
    ArgumentNullException ane = Assert.ThrowsException<ArgumentNullException> ( () => WeakHandlerFacility.WeakHandlerSafe ( null! ) );
    Debug.Write ( ane.Message ); // output message
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

  [TestMethod]
  public void WeakHandlerCleanUpConstruction__GetsProperConstructionFuction ()
  {
    Func<Dictionary<int, List<WeakHandler>>, WeakHandlerCleanUp> construction
      = WeakHandlerFacility.WeakHandlerCleanUpConstruction ( TimeSpan.FromDays ( 1 ) );

    WeakHandlerCleanUp whcu = construction(default!);
    Assert.AreEqual ( typeof ( WeakHandlerCleanUp ), whcu.GetType () );
  }
}
