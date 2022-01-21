using Microsoft.VisualStudio.TestTools.UnitTesting;

using WeakEventCuratorTest.WeakEventCuratorTest.Abstract;

namespace WeakEventCuratorTest.WeakEventCuratorTest;

[TestClass]
sealed public class WeakEventCuratorTests_Add : WeakEventCuratorTests_Shared
{
  [ClassInitialize]
  static new public void Init ( TestContext tc ) => WeakEventCuratorTests_Shared.Init ( tc );
  [ClassCleanup]
  static new public void Cleansing () => WeakEventCuratorTests_Shared.Cleansing ();

  override protected AddRemoveMethod OneOfAddRemoveMethods => WeakEventCurator.Add;

  [TestMethod]
  public void AddSomeHandler__Added () => WeakEventCurator.Add ( new object (), "", string.Intern );
}
