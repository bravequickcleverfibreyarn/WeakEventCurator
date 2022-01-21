using Microsoft.VisualStudio.TestTools.UnitTesting;

using Software9119.WeakEvent;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeakEventCuratorTest.WeakHandlerCleanUpTest;

[TestClass]
public class WeakHandlerCleanUpTests
{
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

    const int cleanUpIntervalMillisec = 125;
    WeakHandlerCleanUp whcu = new (test, TimeSpan.FromMilliseconds(cleanUpIntervalMillisec));
    await WaitWhile ();

    Assert.AreEqual ( mortalsCount, mortalHandlers.Count );
    Assert.AreEqual ( 1, test.Count );

    GC.Collect (); // Have mortals die.
    await WaitWhile ();

    Assert.AreEqual ( 0, mortalHandlers.Count );
    Assert.AreEqual ( 0, test.Count );

    await whcu.DisposeAsync ();

    static Task WaitWhile () => Task.Delay ( cleanUpIntervalMillisec * 2 );
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

    const int cleanUpIntervalMillisec = 125;
    WeakHandlerCleanUp whcu = new (test, TimeSpan.FromMilliseconds(cleanUpIntervalMillisec));
    await WaitWhile ();

    Assert.AreEqual ( 4, mixedHandlers.Count );
    Assert.AreEqual ( 1, test.Count );

    GC.Collect (); // Have mortals die.
    await WaitWhile ();

    Assert.AreEqual ( 2, mixedHandlers.Count );
    Assert.AreEqual ( 1, test.Count );

    Assert.AreEqual ( 0, target.TestCount );
    mixedHandlers.ForEach ( x => x.Invoke ( null ) );
    Assert.AreEqual ( 2, target.TestCount );

    await whcu.DisposeAsync ();

    static Task WaitWhile () => Task.Delay ( cleanUpIntervalMillisec * 2 );
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

    const int cleanUpIntervalMillisec = 125;
    WeakHandlerCleanUp whcu = new (test, TimeSpan.FromMilliseconds(cleanUpIntervalMillisec));
    await WaitWhile ();

    Assert.AreEqual ( 2, immortalHandlers.Count );
    Assert.AreEqual ( 1, test.Count );

    GC.Collect (); // Have mortals die.
    await WaitWhile ();

    Assert.AreEqual ( 2, immortalHandlers.Count );
    Assert.AreEqual ( 1, test.Count );

    await whcu.DisposeAsync ();

    static Task WaitWhile () => Task.Delay ( cleanUpIntervalMillisec * 2 );
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

    const int cleanUpIntervalMillisec = 125;
    WeakHandlerCleanUp whcu = new (test, TimeSpan.FromMilliseconds(cleanUpIntervalMillisec));
    await WaitWhile ();

    Assert.AreEqual ( mortalsCount, mortalHandlers.Count );
    Assert.AreEqual ( 4, mixedHandlers.Count );
    Assert.AreEqual ( 2, immortalHandlers.Count );

    Assert.AreEqual ( 3, test.Count );

    GC.Collect (); // Have mortals die.
    await WaitWhile ();

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

    static Task WaitWhile () => Task.Delay ( cleanUpIntervalMillisec * 2 );
  }
}
