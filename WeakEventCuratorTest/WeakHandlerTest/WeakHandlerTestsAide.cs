using System;
using System.Diagnostics.CodeAnalysis;

using WeakEventCurator;

namespace WeakEventCuratorTest.WeakHandlerTest
{
  [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Can result in doubled dot notation invocation manner.")]
  internal class WeakHandlerTestsAide
  {
    public WeakHandlerTestsAide() => target = new Target();

    // Existing target

    Target target;
    public WeakHandler WeakHandler_ExistingTarget_Handler1() => new((Action)target.Handler1);

    public WeakHandler WeakHandler_ExistingTarget_Exception() => new((Action)target.Exception);

    public Delegate Delegate_ExistingTarget_Handler1 => (Action)target.Handler1;

    public Delegate Delegate_ExistingTarget_Handler2 => (Action)target.Handler2;


    // New target    

    public WeakHandler WeakHandler_NewTarget_Handler1() => new(Delegate_NewTarget_Handler1());

    public WeakHandler WeakHandler_NewTarget_Exception() => new((Action)new Target().Exception);

    public Delegate Delegate_NewTarget_Handler1() => (Action)new Target().Handler1;


    public class Target
    {
      public const string ExceptionMessage = "This is rare and unique exception message – XY9sWn!";

      public void Handler1() { }

      public void Handler2() { }

      public void Exception() => throw new Exception(ExceptionMessage);
    }

  }
}
