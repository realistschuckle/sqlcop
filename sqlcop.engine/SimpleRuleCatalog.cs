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

		public void AddActive(IJudgeSql rule)
		{
			if(rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			_inactiveRules.RemoveAll(r => r.CanonicalName == rule.CanonicalName);
			IEnumerable<IJudgeSql> found;
			found = _activeRules.Where(r => r.CanonicalName == rule.CanonicalName);
			if(found.Count() > 0 && found.Any(r => !r.Equals(rule)))
			{
				throw new DuplicateCanonicalNameException(rule.CanonicalName);
			}
			else if(found.Count() == 0)
			{
				_activeRules.Add(rule);
			}
		}
		
		public void AddInactive(IJudgeSql rule)
		{
			if(rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			_activeRules.RemoveAll(r => r.CanonicalName == rule.CanonicalName);
			IEnumerable<IJudgeSql> found;
			found = _inactiveRules.Where(r => r.CanonicalName == rule.CanonicalName);
			if(found.Count() > 0 && found.Any(r => !r.Equals(rule)))
			{
				throw new DuplicateCanonicalNameException(rule.CanonicalName);
			}
			else if(found.Count() == 0)
			{
				_inactiveRules.Add(rule);
			}
		}
		
		public void Activate(string canonicalName)
		{
			MoveRule(canonicalName, _inactiveRules, _activeRules);
		}
		
		public void Deactivate(string canonicalName)
		{
			MoveRule(canonicalName, _activeRules, _inactiveRules);
		}

		public IEnumerable<IJudgeSql> ActiveRules
		{
			get
			{
				return _activeRules;
			}
		}

		public IEnumerable<IJudgeSql> Rules
		{
			get
			{
				return _activeRules.Concat(_inactiveRules);
			}
		}

		public IEnumerable<IJudgeSql> InactiveRules
		{
			get
			{
				return _inactiveRules;
			}
		}
		
		private static void MoveRule(string canonicalName, List<IJudgeSql> source, List<IJudgeSql> target)
		{
			if(canonicalName == null)
			{
				throw new ArgumentNullException("canonicalName");
			}
			IJudgeSql rule;
			rule = source.Where(r => r.CanonicalName == canonicalName)
						 .FirstOrDefault();
			if(rule != null)
			{
				source.RemoveAll(m => m == rule);
				target.Add(rule);
			}
		}

		private List<IJudgeSql> _activeRules;
		private List<IJudgeSql> _inactiveRules;
	}
}
