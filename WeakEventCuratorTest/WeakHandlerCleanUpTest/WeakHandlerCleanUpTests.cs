using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;

using WeakEventCuratorTest.WeakHandlerCleanUpTest.Abstract;

namespace WeakEventCuratorTest.WeakHandlerCleanUpTest;

[TestClass]
sealed public class WeakHandlerCleanUpTests : WeakHandlerCleanUpTests_Shared
{
  override protected Type WeakHandlerCleanUpType => typeof ( WeakHandlerCleanUp );
}
