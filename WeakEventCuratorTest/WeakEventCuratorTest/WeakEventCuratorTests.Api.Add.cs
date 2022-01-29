using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;

using WeakEventCuratorTest.WeakEventCuratorTest.Abstract;

namespace WeakEventCuratorTest.WeakEventCuratorTest;

[TestClass]
public class WeakEventCuratorTests_Add : WeakEventCuratorTests_AddRemove_Shared
{
  sealed override protected AddRemove AddRemoveDelegate => WeakEventCurator ().Add;

  override protected Type WeakEventCuratorType { get; } = typeof ( WeakEventCurator );

  [TestMethod]
  public void AddSomeHandler__Added ()
  {
    using WeakEventCurator wec = WeakEventCurator ();
    wec.Add ( new object (), "", string.Intern );
  }
}
