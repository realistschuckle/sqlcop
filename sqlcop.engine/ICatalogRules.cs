using System;
using System.Collections.Generic;

namespace sqlcop.engine
{
	public interface ICatalogRules
	{
		IEnumerable<IJudgeSql> ActiveRules { get; }
		IEnumerable<IJudgeSql> Rules { get; }
		IEnumerable<IJudgeSql> InactiveRules { get; }
	}
}

