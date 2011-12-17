using System;

namespace sqlcop.engine
{
	public interface IDescribeSqlProblem
	{
		string RuleCanonicalName { get; }
	}
}

