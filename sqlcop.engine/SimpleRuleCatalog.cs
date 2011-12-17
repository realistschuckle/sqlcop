using System;
using System.Collections.Generic;

namespace sqlcop.engine
{
	public class SimpleRuleCatalog : ICatalogRules
	{
		public SimpleRuleCatalog()
		{
		}

		public void Add(IJudgeSql rule)
		{
			if(rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			throw new NotImplementedException();
		}

		public IEnumerable<IJudgeSql> ActiveRules
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
