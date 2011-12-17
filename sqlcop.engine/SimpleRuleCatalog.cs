using System;
using System.Collections.Generic;
using System.Linq;

namespace sqlcop.engine
{
	public class SimpleRuleCatalog : ICatalogRules
	{
		public SimpleRuleCatalog()
		{
			_activeRules = new List<IJudgeSql>();
			_inactiveRules = new List<IJudgeSql>();
		}
		
		public IEnumerable<IDescribeSqlProblem> ApplyActiveRules(IDescribeSql sql)
		{
			if(sql == null)
			{
				return Enumerable.Empty<IDescribeSqlProblem>();
			}
			List<IDescribeSqlProblem> results;
			results = new List<IDescribeSqlProblem>();
			foreach(IJudgeSql rule in ActiveRules)
			{
				IEnumerable<IDescribeSqlProblem> ruleResults;
				ruleResults = rule.Judge(sql);
				if(ruleResults == null)
				{
					continue;
				}
				results.AddRange(ruleResults);
			}	
			return results;
		}

		public void AddActive(IJudgeSql rule)
		{
			AddRule(rule, _activeRules);
		}
		
		public void AddInactive(IJudgeSql rule)
		{
			AddRule(rule, _inactiveRules);
		}
		
		public void Activate(string canonicalName)
		{
			MoveRule(canonicalName, _inactiveRules);
		}
		
		public void Deactivate(string canonicalName)
		{
			MoveRule(canonicalName, _activeRules);
		}

		public IEnumerable<IJudgeSql> ActiveRules
		{
			get { return _activeRules; }
		}

		public IEnumerable<IJudgeSql> Rules
		{
			get { return _activeRules.Concat(_inactiveRules); }
		}

		public IEnumerable<IJudgeSql> InactiveRules
		{
			get { return _inactiveRules; }
		}
		
		private void MoveRule(string canonicalName, List<IJudgeSql> source)
		{
			if(canonicalName == null)
			{
				throw new ArgumentNullException("canonicalName");
			}
			List<IJudgeSql> target = source == _activeRules? _inactiveRules : _activeRules;
			IJudgeSql rule;
			rule = source.Where(r => r.CanonicalName == canonicalName)
						 .FirstOrDefault();
			if(rule != null)
			{
				source.RemoveAll(m => m == rule);
				target.Add(rule);
			}
		}
		
		private void AddRule(IJudgeSql rule, List<IJudgeSql> target)
		{
			if(rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			List<IJudgeSql> other = target == _activeRules? _inactiveRules : _activeRules;
			other.RemoveAll(r => r.CanonicalName == rule.CanonicalName);
			IEnumerable<IJudgeSql> found;
			found = target.Where(r => r.CanonicalName == rule.CanonicalName);
			if(found.Count() > 0 && found.Any(r => !r.Equals(rule)))
			{
				throw new DuplicateCanonicalNameException(rule.CanonicalName);
			}
			else if(found.Count() == 0)
			{
				target.Add(rule);
			}
		}

		private List<IJudgeSql> _activeRules;
		private List<IJudgeSql> _inactiveRules;
	}
}
