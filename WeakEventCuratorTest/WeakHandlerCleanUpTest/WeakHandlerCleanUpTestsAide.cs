using Software9119.WeakEvent;

namespace WeakEventCuratorTest.WeakHandlerCleanUpTest;

class WeakHandlerCleanUpTestsAide
{
  readonly public TargetModel Target = new ();

  public WeakHandler MortalOne () => new (new TargetModel ().Handler);
  public WeakHandler ImmortalOne () => new (Target.Handler);

  public class TargetModel
  {
    int testcount;

    public int TestCount => testcount;
    public void Handler () => ++testcount;
  }
}
