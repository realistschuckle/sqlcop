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
	public class AssemblyRuleCatalogTests
	{
		[Test]
		public void AssemblyRuleCatalog_Implements_ICatalogRules()
		{
			Type type = typeof(AssemblyRuleCatalog);
			Type iface = typeof(ICatalogRules);
			Assert.That(iface.IsAssignableFrom(type));
		}
		
		[Test]
		public void AddAssembly_Raises_ArgumentNullException_For_Null_Parameter()
		{
			TestDelegate td = () => _catalog.AddAssembly(null);
			ArgumentNullException ex;
			ex = Assert.Throws<ArgumentNullException>(td);
			Assert.That(ex.ParamName, Is.EqualTo("assemblyString"));
		}
		
		[Test]
		public void AddAssembly_Raises_FileNotFoundException_For_Unfindable_Assembly_Name()
		{
			string fileName = "i.do.not.exist";
			TestDelegate td = () => _catalog.AddAssembly(fileName);
			FileNotFoundException ex;
			ex = Assert.Throws<FileNotFoundException>(td);
			Assert.That(ex.FileName, Is.EqualTo(fileName));
		}
		
		[Test]
		public void AddAssembly_Adds_Contained_Rules_To_ActiveRules()
		{
			_catalog.AddAssembly(GetType().Assembly.GetName().Name);
			Assert.That(_catalog.Rules.Count(), Is.EqualTo(1));
			Assert.That(_catalog.ActiveRules.Count(), Is.EqualTo(1));
			Assert.That(_catalog.InactiveRules.Count(), Is.EqualTo(0));
			
			IJudgeSql rule = _catalog.ActiveRules.First();
			Assert.That(rule, Is.InstanceOf(typeof(StubbedRule)));
		}
		
		[Test]
		public void AddAssemblyFrom_Raises_ArgumentNullException_For_Null_Parameter()
		{
			TestDelegate td = () => _catalog.AddAssemblyFrom(null);
			ArgumentNullException ex;
			ex = Assert.Throws<ArgumentNullException>(td);
			Assert.That(ex.ParamName, Is.EqualTo("assemblyFile"));
		}
		
		[Test]
		public void AddAssemblyFrom_Raises_FileNotFoundException_For_Unfindable_Assembly_Name()
		{
			string fileName = "/tmp/i.do.not.exist";
			TestDelegate td = () => _catalog.AddAssemblyFrom(fileName);
			FileNotFoundException ex;
			ex = Assert.Throws<FileNotFoundException>(td);
			Assert.That(ex.FileName, Is.EqualTo(fileName));
		}
		
		[Test]
		public void AddAssemblyFrom_Adds_Contained_Rules_To_ActiveRules()
		{
			_catalog.AddAssemblyFrom(GetType().Assembly.Location);
			Assert.That(_catalog.Rules.Count(), Is.EqualTo(1));
			Assert.That(_catalog.ActiveRules.Count(), Is.EqualTo(1));
			Assert.That(_catalog.InactiveRules.Count(), Is.EqualTo(0));
			
			IJudgeSql rule = _catalog.ActiveRules.First();
			Assert.That(rule, Is.InstanceOf(typeof(StubbedRule)));
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
		public void Deactivate_Moves_Rule_From_Active_To_Inactive()
		{
			_catalog.AddAssembly(GetType().Assembly.GetName().Name);
			_catalog.Deactivate(typeof(StubbedRule).FullName);
			Assert.That(_catalog.Rules.Count(), Is.EqualTo(1));
			Assert.That(_catalog.ActiveRules.Count(), Is.EqualTo(0));
			Assert.That(_catalog.InactiveRules.Count(), Is.EqualTo(1));
		}
		
		[Test]
		public void Activate_Moves_Rule_From_Inactive_To_Active()
		{
			_catalog.AddAssembly(GetType().Assembly.GetName().Name);
			_catalog.Deactivate(typeof(StubbedRule).FullName);
			_catalog.Activate(typeof(StubbedRule).FullName);
			Assert.That(_catalog.Rules.Count(), Is.EqualTo(1));
			Assert.That(_catalog.ActiveRules.Count(), Is.EqualTo(1));
			Assert.That(_catalog.InactiveRules.Count(), Is.EqualTo(0));
		}
		
		[Test]
		public void ApplyActiveRules_Returns_Expected_Problem()
		{
			IDescribeSql sql = MockRepository.GenerateMock<IDescribeSql>();
			_catalog.AddAssembly(GetType().Assembly.GetName().Name);
			StubbedRule rule = _catalog.Rules.First() as StubbedRule;
			IEnumerable<IDescribeSqlProblem> problems;
			problems = _catalog.ApplyActiveRules(sql);
			Assert.That(problems.Count(), Is.EqualTo(1));
			Assert.That(problems.First(), Is.EqualTo(rule.Problem));
		}
		
		[SetUp]
		public void RunBeforeEachTest()
		{
			_catalog = new AssemblyRuleCatalog();
		}
		
		private AssemblyRuleCatalog _catalog;
	}
	
	public interface BetterJudge : IJudgeSql
	{
	}
	
	public class StubbedRule : AbstractSqlJudge
	{
		public StubbedRule()
		{
			_problem = MockRepository.GenerateMock<IDescribeSqlProblem>();
		}
		
		public IDescribeSqlProblem Problem
		{
			get { return _problem; }
		}
		
		public override IEnumerable<IDescribeSqlProblem> Judge(IDescribeSql description)
		{
			return new List<IDescribeSqlProblem> { _problem };
		}
		
		public override string Name { get { return "Just a stub"; } }
		public override string Description { get { return "Just a stub."; } }
		public override Uri Documentation { get { return new Uri("http://bit.ly"); } }
		
		private IDescribeSqlProblem _problem;
	}
}

