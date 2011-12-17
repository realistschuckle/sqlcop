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
				if(sqlDescription == null)
				{
					continue;
				}
				foreach(IJudgeSql rule in _rules.ActiveRules)
				{
					IEnumerable<IDescribeSqlProblem> ruleResults;
					ruleResults = rule.Judge(sqlDescription);
					if(ruleResults == null)
					{
						continue;
					}
					results.AddRange(ruleResults);
				}
			}
			return results;
		}
		
		private IEnumerable<IParseSql> _parsers;
		private ICatalogRules _rules;
	}
}

