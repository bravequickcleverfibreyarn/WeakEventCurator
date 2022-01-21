using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

using WeakEventCuratorTest.WeakEventCuratorTest.Abstract;
using WeakEventCuratorTest.WeakEventCuratorTest.Disctinction;

namespace WeakEventCuratorTest.WeakEventCuratorTest;

[TestClass]
sealed public class WeakEventCurator__InheritedTests_Invoke : WeakEventCuratorTests_Invoke_Shared
{
  override protected Type WeakEventCuratorType => typeof ( WeakEventCurator__Inherited );
}
