using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WeakEventCuratorTest.WeakHandlerCleanUpTest.Abstract;

abstract public class WeakHandlerCleanUpTests_Shared
{
  const int cleanUpIntervalMillisecs = 125;

  abstract protected Type WeakHandlerCleanUpType { get; }

  WeakHandlerCleanUp WeakHandlerCleanUp ( Dictionary<int, List<WeakHandler>> test, int intervalMillisecs )
    => WeakHandlerCleanUp ( test, TimeSpan.FromMilliseconds ( intervalMillisecs ) );

  WeakHandlerCleanUp WeakHandlerCleanUp ( Dictionary<int, List<WeakHandler>> test, TimeSpan interval )
  => (WeakHandlerCleanUp) Activator.CreateInstance
    (
      type: WeakHandlerCleanUpType,
      bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
      null,
      args: new object [] { test, interval },
      CultureInfo.CurrentCulture
    )!;

  static Task WaitWhile ( int intervalMillisecs ) => Task.Delay ( intervalMillisecs * 2 );

  [TestMethod]
  async public Task DeadHandlers__ListAndHandlersAreRemoved ()
  {
    WeakHandlerCleanUpTestsAide aide = new ();

    const int mortalsCount = 5;

#pragma warning disable IDE0007 // Use implicit type
    List<WeakHandler> mortalHandlers = Enumerable
      .Repeat(aide.MortalOne (), mortalsCount)
      .ToList();
#pragma warning restore IDE0007 // Use implicit type

    Dictionary<int, List<WeakHandler>> test = new ()
    {
      [default(int)] = mortalHandlers
    };

    WeakHandlerCleanUp whcu = WeakHandlerCleanUp(test, cleanUpIntervalMillisecs);
    await WaitWhile ( cleanUpIntervalMillisecs );

    Assert.AreEqual ( mortalsCount, mortalHandlers.Count );
    Assert.AreEqual ( 1, test.Count );

    GC.Collect (); // Have mortals die.
    await WaitWhile ( cleanUpIntervalMillisecs );

    Assert.AreEqual ( 0, mortalHandlers.Count );
    Assert.AreEqual ( 0, test.Count );

    await whcu.DisposeAsync ();
  }

  [TestMethod]
  async public Task MixedHandlers__DeadRemoved ()
  {
    WeakHandlerCleanUpTestsAide aide                = new ();
    WeakHandlerCleanUpTestsAide.TargetModel target  = aide.Target;

    List<WeakHandler> mixedHandlers = new ()
    {
      aide.MortalOne (),
      aide.MortalOne (),
      aide.ImmortalOne (),
      aide.ImmortalOne ()
    };

    Dictionary<int, List<WeakHandler>> test = new ()
    {
      [default(int)] = mixedHandlers
    };

    WeakHandlerCleanUp whcu = WeakHandlerCleanUp(test, cleanUpIntervalMillisecs);
    await WaitWhile ( cleanUpIntervalMillisecs );

    Assert.AreEqual ( 4, mixedHandlers.Count );
    Assert.AreEqual ( 1, test.Count );

    GC.Collect (); // Have mortals die.
    await WaitWhile ( cleanUpIntervalMillisecs );

    Assert.AreEqual ( 2, mixedHandlers.Count );
    Assert.AreEqual ( 1, test.Count );

    Assert.AreEqual ( 0, target.TestCount );
    mixedHandlers.ForEach ( x => x.Invoke ( null ) );
    Assert.AreEqual ( 2, target.TestCount );

    await whcu.DisposeAsync ();
  }

  [TestMethod]
  async public Task LiveHandlers__AreKept ()
  {
    WeakHandlerCleanUpTestsAide aide = new ();

    List<WeakHandler> immortalHandlers = new ()
    {
      aide.ImmortalOne (),
      aide.ImmortalOne ()
    };

    Dictionary<int, List<WeakHandler>> test = new ()
    {
      [default(int)] = immortalHandlers,
    };

    WeakHandlerCleanUp whcu = WeakHandlerCleanUp(test, cleanUpIntervalMillisecs);
    await WaitWhile ( cleanUpIntervalMillisecs );

    Assert.AreEqual ( 2, immortalHandlers.Count );
    Assert.AreEqual ( 1, test.Count );

    GC.Collect (); // Have mortals die.
    await WaitWhile ( cleanUpIntervalMillisecs );

    Assert.AreEqual ( 2, immortalHandlers.Count );
    Assert.AreEqual ( 1, test.Count );

    await whcu.DisposeAsync ();
  }

  [TestMethod]
  async public Task AllHandlerCollectionTypes__ExhibitSameAsWhenSeparate ()
  {
    WeakHandlerCleanUpTestsAide aide                = new ();
    WeakHandlerCleanUpTestsAide.TargetModel target  = aide.Target;

    const int mortalsCount = 5;

#pragma warning disable IDE0007 // Use implicit type
    List<WeakHandler> mortalHandlers = Enumerable
      .Repeat(aide.MortalOne (), mortalsCount)
      .ToList();
#pragma warning restore IDE0007 // Use implicit type

    List<WeakHandler> mixedHandlers = new ()
    {
      aide.MortalOne (),
      aide.MortalOne (),
      aide.ImmortalOne (),
      aide.ImmortalOne ()
    };

    List<WeakHandler> immortalHandlers = new ()
    {
      aide.ImmortalOne (),
      aide.ImmortalOne ()
    };

    Dictionary<int, List<WeakHandler>> test = new ()
    {
      [1] = mortalHandlers,
      [2] = mixedHandlers,
      [3] = immortalHandlers,
    };

    WeakHandlerCleanUp whcu = WeakHandlerCleanUp(test, cleanUpIntervalMillisecs);
    await WaitWhile ( cleanUpIntervalMillisecs );

    Assert.AreEqual ( mortalsCount, mortalHandlers.Count );
    Assert.AreEqual ( 4, mixedHandlers.Count );
    Assert.AreEqual ( 2, immortalHandlers.Count );

    Assert.AreEqual ( 3, test.Count );

    GC.Collect (); // Have mortals die.
    await WaitWhile ( cleanUpIntervalMillisecs );

    Assert.AreEqual ( 2, test.Count );

    Assert.AreEqual ( 0, mortalHandlers.Count );
    mortalHandlers = null!;
    Assert.AreEqual ( 2, immortalHandlers.Count );
    immortalHandlers = null!;
    Assert.AreEqual ( 2, mixedHandlers.Count );

    Assert.AreEqual ( 0, target.TestCount );
    mixedHandlers.ForEach ( x => x.Invoke ( null ) );
    Assert.AreEqual ( 2, target.TestCount );

    await whcu.DisposeAsync ();
  }

  [TestMethod]
  public void Interval_IsNotGreaterThanZero__ThrowsArgumentOutOfRangeException ()
  {
    TargetInvocationException tie = Assert.ThrowsException<TargetInvocationException>
    (
      () => WeakHandlerCleanUp ( new (), TimeSpan.Zero )
    );

    Assert.AreEqual ( typeof ( ArgumentOutOfRangeException ), tie.InnerException!.GetType () );
  }
}
