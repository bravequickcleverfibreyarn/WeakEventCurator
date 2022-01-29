// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage ( "Performance", "CA1822:Mark members as static", Justification = "Results in undesired doubled member invocation style.", Scope = "type", Target = "~T:WeakEventCuratorTest.WeakHandlerCleanUpTest.WeakHandlerCleanUpTestsAide" )]
[assembly: SuppressMessage ( "Performance", "CA1822:Mark members as static", Justification = "Results in undesired doubled member invocation style.", Scope = "type", Target = "~T:WeakEventCuratorTest.WeakHandlerTest.WeakHandlerTestsAide" )]

[assembly: SuppressMessage ( "Performance", "CA1825:Avoid zero-length array allocations", Justification = "Useless.", Scope = "member", Target = "~M:WeakEventCuratorTest.WeakEventCuratorTest.Abstract.WeakEventCuratorTests_AddRemove_Shared.Handlers_IsEmpty__ThrowsArgumentException" )]

[assembly: SuppressMessage ( "Style", "IDE0022:Use expression body for methods", Justification = "No room.", Scope = "type", Target = "~T:WeakEventCuratorTest.WeakHandlerFacilityTest.WeakHandlerFacilityTests" )]
