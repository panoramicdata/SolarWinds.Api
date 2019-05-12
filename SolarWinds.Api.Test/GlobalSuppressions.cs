
// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
	"Design",
	"RCS1075:Avoid empty catch clause that catches System.Exception.",
	Justification = "We could fail because we're on a background thread and our captured ITestOutputHelper is busted (if the test 'completed' before the background thread fired). So, ignore this. There isn't really anything we can do but hope the caller has additional loggers registered",
	Scope = "member",
	Target = "~M:SolarWinds.Api.Test.Logging.XunitLogger.Log``1(Microsoft.Extensions.Logging.LogLevel,Microsoft.Extensions.Logging.EventId,``0,System.Exception,System.Func{``0,System.Exception,System.String})")]

