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
			_activeRules.Add(rule);
		}
		
		public void Deactivate(IJudgeSql rule)
		{
			if(_activeRules.Remove(rule))
			{
				_inactiveRules.Add(rule);
			}
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

		private List<IJudgeSql> _activeRules;
		private List<IJudgeSql> _inactiveRules;
	}
}
