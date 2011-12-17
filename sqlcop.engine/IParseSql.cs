using System;

namespace sqlcop.engine
{
	public interface IParseSql
	{
		IDescribeSql Parse(string input);
	}
}

