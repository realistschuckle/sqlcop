using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace sqlcop.engine
{
	[Serializable]
	public class InvalidSqlException : Exception
	{
		public InvalidSqlException(string input, int row, int column)
			: this(input)
		{
			Input = input;
			Row = row;
			Column = column;
		}
		
		public InvalidSqlException() {}
		
		public InvalidSqlException(string message) : this(message, null) {}

		public InvalidSqlException(string message, Exception innerException)
			: base(message, innerException) {}

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected InvalidSqlException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			Input = info.GetString(InputNameKey);
			Row = info.GetInt32(RowNameKey);
			Column = info.GetInt32(ColumnNameKey);
		} 
		
		public void GetData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(InputNameKey, Input);
			info.AddValue(RowNameKey, Row);
			info.AddValue(ColumnNameKey, Column);
		}
		
		public string Input { get; private set; }
		public int Row { get; private set; }
		public int Column { get; private set; }
			
		private static string InputNameKey = "Input";
		private static string RowNameKey = "Row";
		private static string ColumnNameKey = "Column";
	}
}
