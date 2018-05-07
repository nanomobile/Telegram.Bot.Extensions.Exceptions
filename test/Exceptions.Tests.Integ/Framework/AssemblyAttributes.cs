﻿using Exceptions.Tests.Integ.Framework;
using Exceptions.Tests.Integ.Framework.XunitExtensions;
using Xunit;

[assembly: TestFramework(Constants.AssemblyName + ".Framework.XunitExtensions.XunitTestFrameworkWithAssemblyFixture", Constants.AssemblyName)]
[assembly: AssemblyFixture(typeof(TestsFixture))]
[assembly: TestCollectionOrderer(Constants.AssemblyName + ".Framework.TestCollectionOrderer", Constants.AssemblyName)]
[assembly: CollectionBehavior(DisableTestParallelization = true)]
