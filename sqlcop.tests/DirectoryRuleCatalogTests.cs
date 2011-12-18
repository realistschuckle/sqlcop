using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using sqlcop.engine;
using Rhino.Mocks;
using System.IO;

namespace sqlcop.tests
{
	[TestFixture]
	public class DirectoryRuleCatalogTests
	{
		[Test]
		public void Implements_ICatalogRules()
		{
			Type type = typeof(DirectoryRuleCatalog);
			Type iface = typeof(ICatalogRules);
			Assert.That(iface.IsAssignableFrom(type));
		}
		
		[Test]
		public void New_Catalog_Has_No_Rules()
		{
			Assert.That(_catalog.Rules.Count(), Is.EqualTo(0));
		}
		
		[Test]
		public void New_Catalog_Has_No_ActiveRules()
		{
			Assert.That(_catalog.ActiveRules.Count(), Is.EqualTo(0));
		}
		
		[Test]
		public void New_Catalog_Has_No_InactiveRules()
		{
			Assert.That(_catalog.InactiveRules.Count(), Is.EqualTo(0));
		}
		
		[Test]
		public void AddDirectory_Raises_ArgumentNullException_For_Null_Parameter()
		{
			TestDelegate td = () => _catalog.AddDirectory(null);
			var ex = Assert.Throws<ArgumentNullException>(td);
			Assert.That(ex.ParamName, Is.EqualTo("path"));
		}
		
		[Test]
		public void AddDirectory_Raises_DirectoryNotFoundException_For_Unknown_Path()
		{
			string unkonwnPath = @"Q:\Annabel Lee\The Raven\El Dorado";
			TestDelegate td = () => _catalog.AddDirectory(unkonwnPath);
			Assert.Throws<DirectoryNotFoundException>(td);
		}
		
		[Test]
		public void AddDirectory_Adds_Rules_From_Directory_To_ActiveRules()
		{
			string path = Path.GetDirectoryName(GetType().Assembly.Location);
			_catalog.AddDirectory(path);
			Assert.That(_catalog.Rules.Count(), Is.GreaterThan(0));
			Assert.That(_catalog.ActiveRules.Count(), Is.GreaterThan(0));
			Assert.That(_catalog.InactiveRules.Count(), Is.EqualTo(0));
		}
		
		[Test]
		public void Deactivate_Moves_Rule_From_Active_To_Inactive()
		{
			string path = Path.GetDirectoryName(GetType().Assembly.Location);
			_catalog.AddDirectory(path);
			int activeRuleCount = _catalog.ActiveRules.Count();
			int inactiveRuleCount = _catalog.InactiveRules.Count();
			int ruleCount = _catalog.Rules.Count();
			_catalog.Deactivate(typeof(StubbedRule).FullName);
			Assert.That(_catalog.Rules.Count(), Is.EqualTo(ruleCount));
			Assert.That(_catalog.ActiveRules.Count(), Is.EqualTo(activeRuleCount - 1));
			Assert.That(_catalog.InactiveRules.Count(), Is.EqualTo(inactiveRuleCount + 1));
		}
		
		[Test]
		public void Activate_Moves_Rule_From_Inactive_To_Active()
		{
			string path = Path.GetDirectoryName(GetType().Assembly.Location);
			_catalog.AddDirectory(path);
			int activeRuleCount = _catalog.ActiveRules.Count();
			int inactiveRuleCount = _catalog.InactiveRules.Count();
			int ruleCount = _catalog.Rules.Count();
			_catalog.Deactivate(typeof(StubbedRule).FullName);
			_catalog.Activate(typeof(StubbedRule).FullName);
			Assert.That(_catalog.Rules.Count(), Is.EqualTo(ruleCount));
			Assert.That(_catalog.ActiveRules.Count(), Is.EqualTo(activeRuleCount));
			Assert.That(_catalog.InactiveRules.Count(), Is.EqualTo(inactiveRuleCount));
		}
		
		[Test]
		public void ApplyActiveRules_Returns_Expected_Problem()
		{
			IDescribeSql sql = MockRepository.GenerateMock<IDescribeSql>();
			string path = Path.GetDirectoryName(GetType().Assembly.Location);
			_catalog.AddDirectory(path);
			StubbedRule rule = _catalog.Rules.First() as StubbedRule;
			IEnumerable<IDescribeSqlProblem> problems;
			problems = _catalog.ApplyActiveRules(sql);
			Assert.That(problems.Count(), Is.EqualTo(1));
			Assert.That(problems.First(), Is.EqualTo(rule.Problem));
		}
		
		[SetUp]
		public void RunBeforeEachTest()
		{
			_catalog = new DirectoryRuleCatalog();
		}
		
		private DirectoryRuleCatalog _catalog;
	}
}

