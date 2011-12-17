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
		public void Rule_Added_Through_AddInactive_Does_Not_Appear_In_ActiveRules()
		{
			_catalog.AddInactive(_rule1);
			IEnumerable<IJudgeSql> activeRules;
			activeRules = _catalog.ActiveRules;
			Assert.That(activeRules.Count(), Is.EqualTo(0));
			Assert.That(activeRules, Has.No.Member(_rule1));
		}
		
		[Test]
		public void Rule_Added_Through_AddInactive_Appears_In_Rules()
		{
			_catalog.AddInactive(_rule1);
			IEnumerable<IJudgeSql> rules;
			rules = _catalog.Rules;
			Assert.That(rules.Count(), Is.EqualTo(1));
			Assert.That(rules, Has.Member(_rule1));
		}
		
		[Test]
		public void Rule_Added_Through_AddInactive_Appears_In_InactiveRules()
		{
			_catalog.AddInactive(_rule1);
			IEnumerable<IJudgeSql> inactiveRules;
			inactiveRules = _catalog.InactiveRules;
			Assert.That(inactiveRules.Count(), Is.EqualTo(1));
			Assert.That(inactiveRules, Has.Member(_rule1));
		}
		
		[Test]
		public void AddInactive_Expects_Non_Null_Parameter()
		{
			TestDelegate td = () => _catalog.AddInactive(null);
			ArgumentNullException ex;
			ex = Assert.Throws<ArgumentNullException>(td);
			Assert.That(ex.ParamName, Is.EqualTo("rule"));
		}
		
		[Test]
		public void Activate_Expects_Non_Null_Name_Parameter()
		{
			TestDelegate td = () => _catalog.Activate(null);
			ArgumentNullException ex;
			ex = Assert.Throws<ArgumentNullException>(td);
			Assert.That(ex.ParamName, Is.EqualTo("canonicalName"));
		}
		
		[Test]
		public void AddActive_Then_AddInactive_Moves_Rule_To_InactiveRules()
		{
			_catalog.AddActive(_rule1);
			_catalog.AddInactive(_rule1);
			IEnumerable<IJudgeSql> inactiveRules, activeRules;
			inactiveRules = _catalog.InactiveRules;
			activeRules = _catalog.ActiveRules;
			Assert.That(activeRules.Count(), Is.EqualTo(0));
			Assert.That(inactiveRules.Count(), Is.EqualTo(1));
			Assert.That(inactiveRules, Has.Member(_rule1));
		}
		
		[Test]
		public void AddInactive_Then_AddActive_Moves_Rule_To_InactiveRules()
		{
			_catalog.AddInactive(_rule1);
			_catalog.AddActive(_rule1);
			IEnumerable<IJudgeSql> inactiveRules, activeRules;
			inactiveRules = _catalog.InactiveRules;
			activeRules = _catalog.ActiveRules;
			Assert.That(inactiveRules.Count(), Is.EqualTo(0));
			Assert.That(activeRules.Count(), Is.EqualTo(1));
			Assert.That(activeRules, Has.Member(_rule1));
		}
		
		[Test]
		public void AddInactive_Then_Activate_Moves_Rule_To_InactiveRules()
		{
			_catalog.AddInactive(_rule1);
			_catalog.Activate(_rule1.CanonicalName);
			IEnumerable<IJudgeSql> inactiveRules, activeRules;
			inactiveRules = _catalog.InactiveRules;
			activeRules = _catalog.ActiveRules;
			Assert.That(inactiveRules.Count(), Is.EqualTo(0));
			Assert.That(activeRules.Count(), Is.EqualTo(1));
			Assert.That(activeRules, Has.Member(_rule1));
		}
		
		[Test]
		public void Deactivate_Expects_Non_Null_Name_Parameter()
		{
			TestDelegate td = () => _catalog.Deactivate(null);
			ArgumentNullException ex;
			ex = Assert.Throws<ArgumentNullException>(td);
			Assert.That(ex.ParamName, Is.EqualTo("canonicalName"));
		}
		
		[Test]
		public void AddActive_Then_Deactivate_Moves_Rule_To_InactiveRules()
		{
			_catalog.AddActive(_rule1);
			_catalog.Deactivate(_rule1.CanonicalName);
			IEnumerable<IJudgeSql> inactiveRules, activeRules;
			inactiveRules = _catalog.InactiveRules;
			activeRules = _catalog.ActiveRules;
			Assert.That(activeRules.Count(), Is.EqualTo(0));
			Assert.That(inactiveRules.Count(), Is.EqualTo(1));
			Assert.That(inactiveRules, Has.Member(_rule1));
		}
		
		[Test]
		public void AddActive_With_The_Same_Rule_Does_Not_Duplicate_The_Rule()
		{
			_catalog.AddActive(_rule1);
			_catalog.AddActive(_rule1);
			IEnumerable<IJudgeSql> activeRules = _catalog.ActiveRules;
			Assert.That(activeRules.Count(), Is.EqualTo(1));
		}
		
		[Test]
		public void AddInactive_With_The_Same_Rule_Does_Not_Duplicate_The_Rule()
		{
			_catalog.AddInactive(_rule1);
			_catalog.AddInactive(_rule1);
			IEnumerable<IJudgeSql> inactiveRules = _catalog.InactiveRules;
			Assert.That(inactiveRules.Count(), Is.EqualTo(1));
		}
		
		[Test]
		public void AddActive_With_Two_Discrete_Rules_With_The_Same_CanonicalName_Raises_DuplicateCanonicalNameException()
		{
			_catalog.AddActive(_rule1);
			TestDelegate td = () => _catalog.AddActive(_rule2);
			DuplicateCanonicalNameException ex;
			ex = Assert.Throws<DuplicateCanonicalNameException>(td);
			Assert.That(ex.DuplicateName, Is.EqualTo(_ruleName));
		}
		
		[Test]
		public void AddInactive_With_Two_Discrete_Rules_With_The_Same_CanonicalName_Raises_DuplicateCanonicalNameException()
		{
			_catalog.AddInactive(_rule1);
			TestDelegate td = () => _catalog.AddInactive(_rule2);
			DuplicateCanonicalNameException ex;
			ex = Assert.Throws<DuplicateCanonicalNameException>(td);
			Assert.That(ex.DuplicateName, Is.EqualTo(_ruleName));
		}
		
		[SetUp]
		public void RunBeforeEachTest()
		{
			_ruleName = "My Awesome Rule!";
			_rule1 = MockRepository.GenerateMock<IJudgeSql>();
			_rule2 = MockRepository.GenerateMock<IJudgeSql>();
			_rule1.Stub(r => r.CanonicalName)
				  .Return(_ruleName)
				  .Repeat.Any();
			_rule2.Stub(r => r.CanonicalName)
				  .Return(_ruleName)
				  .Repeat.Any();
			_catalog = new SimpleRuleCatalog();
		}
		
		private string _ruleName;
		private SimpleRuleCatalog _catalog;
		private IJudgeSql _rule1;
		private IJudgeSql _rule2;
	}
}

