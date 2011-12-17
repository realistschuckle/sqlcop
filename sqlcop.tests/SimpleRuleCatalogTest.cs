using System;
using NUnit.Framework;
using sqlcop.engine;

namespace sqlcop.tests
{
	[TestFixture]
	public class SimpleRuleCatalogTest
	{
		[Test]
		public void Add_Expects_Non_Null_Rule()
		{
			TestDelegate td = () => _catalog.Add(null);
			ArgumentNullException ex;
			ex = Assert.Throws<ArgumentNullException>(td);
			Assert.That(ex.ParamName, Is.EqualTo("rule"));
		}
		
		[SetUp]
		public void RunBeforeEachTest()
		{
			_catalog = new SimpleRuleCatalog();
		}
		
		private SimpleRuleCatalog _catalog;
	}
}

