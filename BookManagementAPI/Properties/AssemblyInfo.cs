using Xunit;

[assembly: TestCollectionOrderer("Xunit.Sdk.TestCollectionOrderer", "xunit.execution.{Platform}")]
[assembly: TestCaseOrderer("Xunit.Sdk.TestCaseOrderer", "xunit.execution.{Platform}")]
[assembly: CollectionBehavior(DisableTestParallelization = true, MaxParallelThreads = 1)]
