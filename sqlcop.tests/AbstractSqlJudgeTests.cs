using System;
using NUnit.Framework;
using Rhino.Mocks;
using sqlcop.engine;

namespace sqlcop.tests
{
	[TestFixture]
	public class AbstractSqlJudgeTests
	{
		[Test]
		public void Canoncial_Name_Returns_Class_Full_Name()
		{
			AbstractSqlJudge stub;
			stub = MockRepository.GenerateStub<AbstractSqlJudge>();
			string stubFullName = stub.GetType().FullName;
			
			Assert.That(stub.CanonicalName, Is.EqualTo(stubFullName));
		}
	}
}

