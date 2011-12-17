using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace sqlcop.engine
{
	[Serializable]
	public class BadSqlException : Exception
	{
		public BadSqlException() {}
		public BadSqlException(string message)
			: base(message) {}
		public BadSqlException(string message, Exception innerException)
			: base(message, innerException) {}

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected BadSqlException(SerializationInfo info, StreamingContext context)
			: base(info, context) {} 
	}
}

