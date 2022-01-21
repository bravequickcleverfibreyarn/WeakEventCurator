using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

using WeakEventCuratorTest.WeakEventCuratorTest.Disctinction;

namespace WeakEventCuratorTest.WeakEventCuratorTest;

[TestClass]
sealed public class WeakEventCurator__InheritedTests_Remove : WeakEventCuratorTests_Remove
{
  override protected Type WeakEventCuratorType => typeof ( WeakEventCurator__Inherited );
}
