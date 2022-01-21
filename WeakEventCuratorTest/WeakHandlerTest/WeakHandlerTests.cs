using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;
using System.Reflection;

namespace WeakEventCuratorTest.WeakHandlerTest
{
  [TestClass]
  public class WeakHandlerTests
  {
    #region IsAlive

    [TestMethod]
    public void IsAlive_NoReferenceToTarget_IsCollected()
    {
      var aide = new WeakHandlerTestsAide();
      WeakHandler weakHandler = aide.WeakHandler_NewTarget_Handler1();

      GC.Collect();

      Assert.IsFalse(weakHandler.IsAlive);

      // Compiler verification

      weakHandler = aide.WeakHandler_ExistingTarget_Handler1();
      GC.Collect();

      Assert.IsTrue(weakHandler.IsAlive);
    }

    [TestMethod]
    public void IsAlive_ReferenceToTargetExists_IsNotCollected()
    {
      var aide = new WeakHandlerTestsAide();
      var weakHandler = aide.WeakHandler_ExistingTarget_Handler1();

      GC.Collect();

      Assert.IsTrue(weakHandler.IsAlive);
    }

    [TestMethod]
    public void IsAlive_StaticHandler_ConsideredAsAlive ()
    {
      var aide = new WeakHandlerTestsAide();
      WeakHandler weakHandler = aide.WeakHandler_StaticHandler_StaticException();

      Assert.IsTrue (weakHandler.IsAlive);
    }

    #endregion
    #region Equals

    [TestMethod]
    public void Equals_TargetIsDead_NotEqual()
    {
      var aide = new WeakHandlerTestsAide();

      WeakHandler gauge = aide.WeakHandler_NewTarget_Handler1();
      GC.Collect();

      Delegate comparand = aide.Delegate_ExistingTarget_Handler1;

      Assert.IsFalse(gauge.Equals(comparand));
    }

    [TestMethod]
    public void Equals_DifferentHandler_NotEqual()
    {
      var aide = new WeakHandlerTestsAide();
      WeakHandler gauge = aide.WeakHandler_ExistingTarget_Handler1();
      Delegate comparand = aide.Delegate_ExistingTarget_Handler2;

      Assert.IsFalse(gauge.Equals(comparand));
    }

    [TestMethod]
    public void Equals_DifferentTarget_NotEqual()
    {
      WeakHandler gauge;
      {
        var aide1 = new WeakHandlerTestsAide();
        gauge = aide1.WeakHandler_ExistingTarget_Handler1();
      }

      Delegate comparand;
      {
        var aide2 = new WeakHandlerTestsAide();
        comparand = aide2.Delegate_ExistingTarget_Handler1;
      }      

      Assert.IsFalse(gauge.Equals(comparand));
    }

    [TestMethod]
    public void Equals_ComparandIsTheSame_Equal()
    {
      var aide = new WeakHandlerTestsAide();
      WeakHandler gauge = aide.WeakHandler_ExistingTarget_Handler1();
      Delegate comparand = aide.Delegate_ExistingTarget_Handler1;

      Assert.IsTrue(gauge.Equals(comparand));
    }

    #endregion
    #region Invoke

    [TestMethod]
    public void Invoke_TargetIsDead_NothingHappens()
    {
      var aide = new WeakHandlerTestsAide();
      WeakHandler wh = aide.WeakHandler_NewTarget_Exception();

      GC.Collect();

      try
      {
        wh.Invoke();
      }
      catch
      {
        Assert.IsTrue(false);
      }

      Assert.IsTrue(true);
    }

    [TestMethod]
    public void Invoke_TargetIsAlive_HandlerIvoked()
    {
      var aide = new WeakHandlerTestsAide();
      WeakHandler wh = aide.WeakHandler_ExistingTarget_Exception();

      Assert.ThrowsException<TargetInvocationException>(() => wh.Invoke());
      try
      {
        wh.Invoke();
      }
      catch (TargetInvocationException tie)
      {
        Assert.AreEqual(WeakHandlerTestsAide.Target.ExceptionMessage, tie.InnerException.Message);
      }
    }

    [TestMethod]
    public void Invoke_StaticHandler_HandlerIvoked ()
    {
      var aide = new WeakHandlerTestsAide();
      WeakHandler wh = aide.WeakHandler_StaticHandler_StaticException();

      Assert.ThrowsException<TargetInvocationException> (() => wh.Invoke ());
      
      try
      {
        wh.Invoke ();
      }
      catch (TargetInvocationException tie)
      {
        Assert.AreEqual (WeakHandlerTestsAide.Target.ExceptionMessage, tie.InnerException.Message);
      }
    }

    #endregion
  }
}
