using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace WeakEventCuratorTest.WeakEventCuratorTest.Abstract;

abstract public class WeakEventCuratorTests_Shared
{

  protected delegate void AddRemoveMethod ( object eventSource, string eventName, params Delegate [] handlers );

  abstract protected Type WeakEventCuratorType { get; }
  abstract protected AddRemoveMethod OneOfAddRemoveMethods { get; }


  protected WeakEventCurator WeakEventCurator ()
  => (WeakEventCurator) Activator.CreateInstance
    (
      type: WeakEventCuratorType,
      bindingAttr: BindingFlags.Instance | BindingFlags.Public,
      null,
      args: new object [] { (Func<Dictionary<int, List<WeakHandler>>, WeakHandlerCleanUp>) (x => new WeakHandlerCleanUp ( x, TimeSpan.FromDays ( 1 ) )) },
      CultureInfo.CurrentCulture
    )!;


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
