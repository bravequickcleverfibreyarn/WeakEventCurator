using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

using WeakEventCuratorTest.WeakEventCuratorTest.Disctinction;

namespace WeakEventCuratorTest.WeakEventCuratorTest;

[TestClass]
sealed public class WeakEventCurator__InheritedTests_Invoke : WeakEventCuratorTests_Invoke
{
  override protected Type WeakEventCuratorType => typeof ( WeakEventCurator__Inherited );
}
