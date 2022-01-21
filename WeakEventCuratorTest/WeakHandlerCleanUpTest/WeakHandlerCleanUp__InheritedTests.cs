using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

using WeakEventCuratorTest.WeakHandlerCleanUpTest.Abstract;
using WeakEventCuratorTest.WeakHandlerCleanUpTest.Distinction;

namespace WeakEventCuratorTest.WeakHandlerCleanUpTest;

[TestClass]
sealed public class WeakHandlerCleanUp__InheritedTests : WeakHandlerCleanUpTests_Shared
{
  override protected Type WeakHandlerCleanUpType => typeof ( WeakHandlerCleanUp__Inherited );
}
