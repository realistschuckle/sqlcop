using System;
using System.Collections.Generic;

namespace sqlcop.engine
{
	public abstract class AbstractSqlJudge : IJudgeSql
	{
		public AbstractSqlJudge()
		{
			CanonicalName = GetType().FullName;
		}
		
		public string CanonicalName { get; private set; }
		
		public abstract IEnumerable<IDescribeSqlProblem> Judge(IDescribeSql description);
		public abstract string Description { get; }
		public abstract Uri Documentation { get; }
		public abstract string Name { get; }
	}
}

