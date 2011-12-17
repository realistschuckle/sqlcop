using System;
using System.Collections.Generic;

namespace sqlcop.engine
{
	public interface ICatalogRules
	{
		IEnumerable<IDescribeSqlProblem> ApplyActiveRules(IDescribeSql sql);
		IEnumerable<IJudgeSql> ActiveRules { get; }
		IEnumerable<IJudgeSql> Rules { get; }
		IEnumerable<IJudgeSql> InactiveRules { get; }
	}
}

