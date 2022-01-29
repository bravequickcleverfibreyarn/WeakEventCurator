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

  [TestMethod]
  public void EqualsSafe_WeakHandlerIsNull__ThrowsArgumentNullException ()
  {
    ArgumentNullException ane = Assert.ThrowsException<ArgumentNullException> ( () => WeakHandlerFacility.EqualsSafe ( null!, string.Intern ) );
    Debug.WriteLine ( ane.Message );
  }

  [TestMethod]
  public void EqualsSafe_DelegateIsNull__ThrowsArgumentNullException ()
  {
    ArgumentNullException ane = Assert.ThrowsException<ArgumentNullException> ( () => WeakHandlerFacility.EqualsSafe ( new WeakHandler(string.Intern), null! ) );
    Debug.WriteLine ( ane.Message );
  }

  [TestMethod]
  public void EqualsSafe_SameHandlers__ReturnsTrue ()
  {
    Delegate del = string.Intern;
    Assert.IsTrue ( WeakHandlerFacility.EqualsSafe ( new WeakHandler ( del ), del ) );
  }

  [TestMethod]
  public void EqualsSafe_DifferentHandlers__ReturnsFalse ()
  {
    Assert.IsFalse ( WeakHandlerFacility.EqualsSafe ( new WeakHandler ( string.Intern ), string.IsInterned ) );
  }

  [TestMethod]
  public void Equals_WeakHandlerIsNull__ThrowsNullReferenceException ()
  {
    _ = Assert.ThrowsException<NullReferenceException> ( () => WeakHandlerFacility.Equals ( null!, string.Intern ) );
  }

  [TestMethod]
  public void Equals_DelegateIsNull__ThrowsNullReferenceException ()
  {
    _ = Assert.ThrowsException<NullReferenceException> ( () => WeakHandlerFacility.Equals ( new WeakHandler ( string.Intern ), null! ) );
  }

  [TestMethod]
  public void Equals_SameHandlers__ReturnsTrue ()
  {
    Delegate del = string.Intern;
    Assert.IsTrue ( WeakHandlerFacility.Equals ( new WeakHandler ( del ), del ) );
  }

  [TestMethod]
  public void Equals_DifferentHandlers__ReturnsFalse ()
  {
    Assert.IsFalse ( WeakHandlerFacility.Equals ( new WeakHandler ( string.Intern ), string.IsInterned ) );
  }
}
