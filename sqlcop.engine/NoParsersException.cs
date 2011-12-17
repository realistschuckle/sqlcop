using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace sqlcop.engine
{
	[Serializable]
	public class NoParsersException : Exception
	{
		public NoParsersException() {}
		public NoParsersException(string message)
			: base(message) {}
		public NoParsersException(string message, Exception innerException)
			: base(message, innerException) {}

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected NoParsersException(SerializationInfo info, StreamingContext context)
			: base(info, context) {} 
	}
}

