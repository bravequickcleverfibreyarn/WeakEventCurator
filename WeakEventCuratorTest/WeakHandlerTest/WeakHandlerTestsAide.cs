
using Software9119.WeakEvent;

using System;

namespace WeakEventCuratorTest.WeakHandlerTest;

sealed class WeakHandlerTestsAide
{
  public WeakHandlerTestsAide () => Target = new TargetModel ();

  // Existing target

  readonly public TargetModel Target;
  public WeakHandler WeakHandler_ExistingTarget_Handler () => new ( ExistingTarget_Handler );
  public Delegate ExistingTarget_Handler => (Action) Target.Handler;

  public Delegate ExistingTarget_Handler__Other => (Action<int>) Target.Handler;

  // New target

  public WeakHandler WeakHandler_NewTarget_Handler () => new ( NewTarget_Handler () );
  public Delegate NewTarget_Handler () => (Action) new TargetModel ().Handler;

  public class TargetModel
  {
    int testCount;
    public int TestCount => testCount;

    public void Handler () => ++testCount;
    public void Handler ( int _ ) { }
  }

  public class StaticModel
  {
    int testCount;
    public int TestCount => testCount;
    public WeakHandler WeakHandler_Handler () => new ( Delegate_Handler );
    public Delegate Delegate_Handler => (Action<StaticModel>) Handler;
    static public void Handler ( StaticModel thatThis ) => ++thatThis.testCount;

    public Delegate Delegate_Handler__Other => (Action) Handler;
    static public void Handler () { }
  }
}
