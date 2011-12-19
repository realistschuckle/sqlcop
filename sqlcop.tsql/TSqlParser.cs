using System;
using sqlcop.engine;

namespace sqlcop.tsql
{
	public class TSqlParser : IParseSql
	{
		public TSqlParser()
		{
		}

		public IDescribeSql Parse(string input)
		{
			if(input == null)
			{
				throw new ArgumentNullException("input");
			}
			if(input.Length == 0)
			{
				throw new InvalidSqlException(input, 0, 0);
			}
			throw new NotImplementedException();
		}
	}
}

