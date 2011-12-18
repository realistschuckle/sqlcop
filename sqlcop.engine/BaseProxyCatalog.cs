using System;
using System.Collections.Generic;

namespace sqlcop.engine
{
	public class BaseProxyCatalog : ICatalogRules
	{
		protected BaseProxyCatalog()
		{
			_catalog = new SimpleRuleCatalog();
		}

		public IEnumerable<IDescribeSqlProblem> ApplyActiveRules(IDescribeSql sql)
		{
			return _catalog.ApplyActiveRules(sql);
		}

		public void Activate(string canonicalName)
		{
			_catalog.Activate(canonicalName);
		}
		
		public void Deactivate(string canonicalName)
		{
			_catalog.Deactivate(canonicalName);
		}

		public IEnumerable<IJudgeSql> ActiveRules
		{
			get { return _catalog.ActiveRules; }
		}

		public IEnumerable<IJudgeSql> Rules
		{
			get { return _catalog.Rules; }
		}

		public IEnumerable<IJudgeSql> InactiveRules
		{
			get { return _catalog.InactiveRules; }
		}
		
		protected void AddActiveRule(IJudgeSql rule)
		{
			_catalog.AddActive(rule);
		}
		
		private SimpleRuleCatalog _catalog;
	}
}

