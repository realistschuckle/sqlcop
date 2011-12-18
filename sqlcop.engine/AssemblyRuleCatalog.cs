using System;
using System.Collections.Generic;
using System.Reflection;

namespace sqlcop.engine
{
	public class AssemblyRuleCatalog : BaseProxyCatalog
	{
		public void AddAssembly(string assemblyString)
		{
			Assembly a = Assembly.Load(assemblyString);
			LoadTypesFromAssembly(a);
		}
		
		public void AddAssemblyFrom(string assemblyFile)
		{
			Assembly a = Assembly.LoadFrom(assemblyFile);
			LoadTypesFromAssembly(a);
		}
		
		private void LoadTypesFromAssembly(Assembly a)
		{
			foreach(Type type in a.GetExportedTypes())
			{
				if(type.IsClass && typeof(IJudgeSql).IsAssignableFrom(type))
				{
					object o = type.GetConstructor(Type.EmptyTypes)
								   .Invoke(null);
					IJudgeSql rule = (IJudgeSql) o;
					AddActiveRule(rule);
				}
			}
		}
	}
}

