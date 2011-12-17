using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using sqlcop.engine;

namespace sqlcop.tests
{
	[TestFixture]
	public class EngineTests
	{
		[Test]
		public void Initialization_Expects_Non_Null_Parser_Collection()
		{
			_parsers = null;
			TestDelegate td = CreateEngine;
			ArgumentNullException ex;
			ex = Assert.Throws<ArgumentNullException>(td);
			Assert.That(ex.ParamName, Is.EqualTo("parsers"));
		}
		
		[Test]
		public void Initialization_Expects_Non_Null_Rule_Catalog()
		{
			_catalog = null;
			TestDelegate td = CreateEngine;
			ArgumentNullException ex;
			ex = Assert.Throws<ArgumentNullException>(td);
			Assert.That(ex.ParamName, Is.EqualTo("rules"));
		}
		
		[Test]
		public void Initialization_Expects_More_Than_Zero_Parsers()
		{
			_parsers = new List<IParseSql>();
			TestDelegate td = CreateEngine;
			Assert.Throws<NoParsersException>(td);
		}
		
		[Test]
		public void Run_Expects_Non_Null_Input()
		{
			CreateEngine();
			TestDelegate td = () => _engine.Run(null);
			ArgumentNullException ex;
			ex = Assert.Throws<ArgumentNullException>(td);
			Assert.That(ex.ParamName, Is.EqualTo("sql"));
		}
		
		[Test]
		public void Run_Calls_Parse_On_All_Parsers()
		{
			string sql = "SELECT me FROM there";
			CreateEngine();
			_engine.Run(sql);
			foreach(IParseSql parser in _parsers)
			{
				parser.AssertWasCalled(p => p.Parse(sql));
			}
		}
		
		[Test]
		public void Run_Ignores_Null_Result_From_Parser()
		{
			CreateEngine();
			_engine.Run("SELECT me FROM there");
		}
		
		[Test]
		public void Run_Does_Not_Catch_BadSqlException()
		{
			BadSqlException ex = new BadSqlException();
			_parser1.Stub(p => p.Parse(null))
				    .IgnoreArguments()
					.Throw(ex);
			CreateEngine();
			TestDelegate td = () => _engine.Run("SELECT me FROM there");
			BadSqlException thrown = Assert.Throws<BadSqlException>(td);
			Assert.That(thrown, Is.SameAs(ex));
		}
		
		[Test]
		public void Run_Does_Not_Call_RuleCatalog_ActiveRules_When_Parsing_Fails()
		{
			CreateEngine();
			_engine.Run("SELECT me FROM there");
			_catalog.AssertWasNotCalled(c => c.ActiveRules);
		}
		
		[Test]
		public void Run_Calls_RuleCatalog_ActiveRules_When_Parsing_Succeeds()
		{
			CreateEngineAndSuccessfulParsers();
			_engine.Run("SELECT me FROM there");
			_catalog.AssertWasCalled(c => c.ActiveRules);
		}
		
		[Test]
		public void Run_Calls_Judge_On_Each_Active_Rule_With_All_Results()
		{
			CreateEngineAndSuccessfulParsers();
			_engine.Run("SELECT me FROM there");
			_rule1.AssertWasCalled(r => r.Judge(_parseResult1));
			_rule1.AssertWasCalled(r => r.Judge(_parseResult2));
			_rule2.AssertWasCalled(r => r.Judge(_parseResult1));
			_rule2.AssertWasCalled(r => r.Judge(_parseResult2));
		}
		
		[Test]
		public void Run_Ignores_Null_Result_From_Rule_Judge()
		{
			CreateEngineAndSuccessfulParsers();
			_rule1.Stub(r => r.Judge(null))
				  .IgnoreArguments()
				  .Return(null);
			_engine.Run("SELECT me FROM there");
		}
		
		[Test]
		public void Run_Returns_Combined_Results_From_Rule_Judge()
		{
			CreateEngineAndSuccessfulParsers();

			IDescribeSqlProblem problem1, problem2;
			problem1 = MockRepository.GenerateMock<IDescribeSqlProblem>();
			problem2 = MockRepository.GenerateMock<IDescribeSqlProblem>();
			
			List<IDescribeSqlProblem> rule1Problems;
			rule1Problems = new List<IDescribeSqlProblem> { problem1 };
			List<IDescribeSqlProblem> rule2Problems;
			rule2Problems = new List<IDescribeSqlProblem> { problem2 };
			
			_rule1.Stub(r => r.Judge(_parseResult1))
				  .Return(rule1Problems)
				  .Repeat.Any();
			_rule2.Stub(r => r.Judge(_parseResult2))
				  .Return(rule2Problems)
				  .Repeat.Any();
			
			IEnumerable<IDescribeSqlProblem> problems;
			problems = _engine.Run("SELECT me FROM there");
			Assert.That(problems.Count(), Is.EqualTo(2));
			Assert.That(problems, Has.Member(problem1));
			Assert.That(problems, Has.Member(problem2));
		}
		
		[SetUp]
		public void RunBeforeEachTest()
		{
			_engine = null;
			
			_parser1 = MockRepository.GenerateMock<IParseSql>();
			_parser2 = MockRepository.GenerateMock<IParseSql>();
			_parsers = new List<IParseSql> { _parser1, _parser2 };
			
			_rule1 = MockRepository.GenerateMock<IJudgeSql>();
			_rule2 = MockRepository.GenerateMock<IJudgeSql>();
			_activeRules = new List<IJudgeSql> { _rule1, _rule2 };
			
			_catalog = MockRepository.GenerateMock<ICatalogRules>();
			_catalog.Stub(c => c.ActiveRules).Return(_activeRules);
		}
		
		private void CreateEngine()
		{
			_engine = new Engine(_parsers, _catalog);
		}
		
		private void CreateEngineAndSuccessfulParsers()
		{
			CreateEngine();
			_parseResult1 = MockRepository.GenerateMock<IDescribeSql>();
			_parseResult2 = MockRepository.GenerateMock<IDescribeSql>();
			_parser1.Stub(p => p.Parse(null))
				    .IgnoreArguments()
					.Return(_parseResult1);
			_parser2.Stub(p => p.Parse(null))
				    .IgnoreArguments()
					.Return(_parseResult2);
		}
		
		private IJudgeSql _rule1;
		private IJudgeSql _rule2;
		private IDescribeSql _parseResult1;
		private IDescribeSql _parseResult2;
		private IParseSql _parser1;
		private IParseSql _parser2;
		private Engine _engine;
		private IEnumerable<IParseSql> _parsers;
		private ICatalogRules _catalog;
		private List<IJudgeSql> _activeRules;
	}
}

