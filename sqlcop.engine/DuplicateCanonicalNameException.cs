using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace sqlcop.engine
{
	[Serializable]
	public class DuplicateCanonicalNameException : Exception
	{
		public DuplicateCanonicalNameException() {}
		public DuplicateCanonicalNameException(string canonicalName)
			: this(canonicalName, null) {}

		public DuplicateCanonicalNameException(string canonicalName, Exception innerException)
			: base(canonicalName, innerException)
		{
			DuplicateName = canonicalName;
		}

		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected DuplicateCanonicalNameException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			DuplicateName = info.GetString(DuplicateNameKey);
		} 
		
		public void GetData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(DuplicateNameKey, DuplicateName);
		}
		
		public string DuplicateName { get; private set; }
			
		private static string DuplicateNameKey = "DuplicateName";
	}
}
