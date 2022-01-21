using Software9119.WeakEvent;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace WeakEventCuratorTest.WeakEventCuratorTest.Abstract;

abstract public class WeakEventCuratorTests_Shared
{
  abstract protected Type WeakEventCuratorType { get; }

  protected WeakEventCurator WeakEventCurator ()
  => (WeakEventCurator) Activator.CreateInstance
    (
      type: WeakEventCuratorType,
      bindingAttr: BindingFlags.Instance | BindingFlags.Public,
      null,
      args: new object [] { (Func<Dictionary<int, List<WeakHandler>>, WeakHandlerCleanUp>) (x => new WeakHandlerCleanUp ( x, TimeSpan.FromDays ( 1 ) )) },
      CultureInfo.CurrentCulture
    )!;
}
