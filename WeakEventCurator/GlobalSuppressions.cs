// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage ("Style", "IDE0046:Convert to conditional expression", Justification = "", Scope = "member", Target = "~M:Software9119.WeakEvent.WeakHandler.Equals(System.Delegate)~System.Boolean")]

[assembly: SuppressMessage ("Design", "CA1051:Do not declare visible instance fields", Justification = "It is readonly.", Scope = "member", Target = "~F:Software9119.WeakEvent.WeakEventCurator.weakHandlersDict")]


[assembly: SuppressMessage ("Design", "CA1062:Validate arguments of public methods", Justification = "No use for it.", Scope = "member", Target = "~M:Software9119.WeakEvent.WeakHandlerCleanUp.ClearHandlersActual(System.Collections.Generic.Dictionary{System.Int32,System.Collections.Generic.List{Software9119.WeakEvent.WeakHandler}})")]
[assembly: SuppressMessage ( "Design", "CA1062:Validate arguments of public methods", Justification = "Intentional.", Scope = "member", Target = "~M:Software9119.WeakEvent.WeakHandlerFacility.WeakHandler(System.Delegate)~Software9119.WeakEvent.WeakHandler" )]

[assembly: SuppressMessage ("Design", "CA1063:Implement IDisposable Correctly", Justification = "False positive.", Scope = "member", Target = "~M:Software9119.WeakEvent.WeakEventCurator.Dispose")]
[assembly: SuppressMessage ("Design", "CA1063:Implement IDisposable Correctly", Justification = "False positive.", Scope = "member", Target = "~M:Software9119.WeakEvent.WeakHandlerCleanUp.Dispose")]


