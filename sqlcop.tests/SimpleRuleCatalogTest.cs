using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using sqlcop.engine;
using Rhino.Mocks;

namespace sqlcop.tests
{
	[TestFixture]
	public class SimpleRuleCatalogTest
	{
		[Test]
		public void New_Catalog_Has_No_Active_Rules()
		{
			Assert.That(_catalog.ActiveRules.Count(), Is.EqualTo(0));
		}
		
		[Test]
		public void New_Catalog_Has_No_InActive_Rules()
		{
			Assert.That(_catalog.InactiveRules.Count(), Is.EqualTo(0));
		}
		
		[Test]
		public void New_Catalog_Has_No_Rules()
		{
			Assert.That(_catalog.Rules.Count(), Is.EqualTo(0));
		}
		
		[Test]
		public void AddActive_Expects_Non_Null_Parameter()
		{
			TestDelegate td = () => _catalog.AddActive(null);
			ArgumentNullException ex;
			ex = Assert.Throws<ArgumentNullException>(td);
			Assert.That(ex.ParamName, Is.EqualTo("rule"));
		}
		
		[Test]
		public void Deactivate_Accepts_null_Parameter()
		{
			_catalog.Deactivate(null);
		}
		
		[Test]
		public void Rule_Added_Through_AddActive_Appears_In_ActiveRules()
		{
			_catalog.AddActive(_rule1);
			IEnumerable<IJudgeSql> activeRules;
			activeRules = _catalog.ActiveRules;
			Assert.That(activeRules.Count(), Is.EqualTo(1));
			Assert.That(activeRules, Has.Member(_rule1));
		}
		
		[Test]
		public void Rule_Added_Through_AddActive_Appears_In_Rules()
		{
			_catalog.AddActive(_rule1);
			IEnumerable<IJudgeSql> rules;
			rules = _catalog.Rules;
			Assert.That(rules.Count(), Is.EqualTo(1));
			Assert.That(rules, Has.Member(_rule1));
		}
		
		[Test]
		public void Rule_Added_Through_AddActive_Does_Not_Appear_In_InactiveRules()
		{
			_catalog.AddActive(_rule1);
			IEnumerable<IJudgeSql> inactiveRules;
			inactiveRules = _catalog.InactiveRules;
			Assert.That(inactiveRules.Count(), Is.EqualTo(0));
			Assert.That(inactiveRules, Has.No.Member(_rule1));
		}
		
		[Test]
		public void Deactivated_Rule_Removes_Rule_From_ActiveRules()
		{
			_catalog.AddActive(_rule1);
			_catalog.Deactivate(_rule1);
			IEnumerable<IJudgeSql> activeRules;
			activeRules = _catalog.ActiveRules;
			Assert.That(activeRules.Count(), Is.EqualTo(0));
		}
		
		[Test]
		public void Deactivated_Rule_Appears_In_InactiveRules()
		{
			_catalog.AddActive(_rule1);
			_catalog.Deactivate(_rule1);
			IEnumerable<IJudgeSql> inactiveRules;
			inactiveRules = _catalog.InactiveRules;
			Assert.That(inactiveRules.Count(), Is.EqualTo(1));
			Assert.That(inactiveRules, Has.Member(_rule1));
		}
		
		[Test]
		public void Deactivated_Rule_Still_Appears_In_Rules()
		{
			_catalog.AddActive(_rule1);
			_catalog.Deactivate(_rule1);
			IEnumerable<IJudgeSql> rules;
			rules = _catalog.Rules;
			Assert.That(rules.Count(), Is.EqualTo(1));
			Assert.That(rules, Has.Member(_rule1));
		}
		
		[SetUp]
		public void RunBeforeEachTest()
		{
			_rule1 = MockRepository.GenerateMock<IJudgeSql>();
			_catalog = new SimpleRuleCatalog();
		}
		
		private SimpleRuleCatalog _catalog;
		private IJudgeSql _rule1;
	}
}

