using System;
using System.Collections.Generic;

namespace sqlcop.engine
{
	public interface IJudgeSql
	{
		IEnumerable<IDescribeSqlProblem> Judge(IDescribeSql description);
		
		string CanonicalName { get; }
		string Name { get; }
		string Description { get; }
		Uri Documentation { get; }
	}
}

