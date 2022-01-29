using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;

using WeakEventCuratorTest.WeakEventCuratorTest.Abstract;

namespace WeakEventCuratorTest.WeakEventCuratorTest;

[TestClass]
public class WeakEventCuratorTests_Invoke : WeakEventCuratorTests_Invoke_Shared
{
  override protected Type WeakEventCuratorType => typeof ( WeakEventCurator );
}
