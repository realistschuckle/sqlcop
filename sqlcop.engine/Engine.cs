using System;
using System.Collections.Generic;
using System.Linq;

namespace sqlcop.engine
{
	public class Engine
	{
		public Engine(IEnumerable<IParseSql> parsers, ICatalogRules rules)
		{
			if(parsers == null)
			{
				throw new ArgumentNullException("parsers");
			}
			if(rules == null)
			{
				throw new ArgumentNullException("rules");
			}
			if(parsers.Count() == 0)
			{
				throw new NoParsersException();
			}
			_parsers = parsers;
			_rules = rules;
		}
		
		public IEnumerable<IDescribeSqlProblem> Run(string sql)
		{
			if(sql == null)
			{
				throw new ArgumentNullException("sql");
			}
			List<IDescribeSqlProblem> results;
			results = new List<IDescribeSqlProblem>();
			foreach(IParseSql parser in _parsers)
			{
				IDescribeSql sqlDescription = parser.Parse(sql);
				IEnumerable<IDescribeSqlProblem> ruleResults;
				ruleResults = _rules.ApplyActiveRules(sqlDescription);
				if(ruleResults != null)
				{
					results.AddRange(ruleResults);
				}
			}
			return results;
		}
		
		private IEnumerable<IParseSql> _parsers;
		private ICatalogRules _rules;
	}
}

