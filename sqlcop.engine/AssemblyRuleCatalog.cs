using System;
using System.Collections.Generic;
using System.Reflection;

namespace sqlcop.engine
{
	public class AssemblyRuleCatalog : ICatalogRules
	{
		public AssemblyRuleCatalog()
		{
			_catalog = new SimpleRuleCatalog();
		}
		
		public void AddAssembly(string assemblyString)
		{
			Assembly a = Assembly.Load(assemblyString);
			foreach(Type type in a.GetExportedTypes())
			{
				if(type.IsClass && typeof(IJudgeSql).IsAssignableFrom(type))
				{
					object o = type.GetConstructor(Type.EmptyTypes)
								   .Invoke(null);
					IJudgeSql rule = (IJudgeSql) o;
					_catalog.AddActive(rule);
				}
			}
		}

		public IEnumerable<IDescribeSqlProblem> ApplyActiveRules(IDescribeSql sql)
		{
			return _catalog.ApplyActiveRules(sql);
		}

		public void Activate(string canonicalName)
		{
			_catalog.Activate(canonicalName);
		}
		
		public void Deactivate(string canonicalName)
		{
			_catalog.Deactivate(canonicalName);
		}

		public IEnumerable<IJudgeSql> ActiveRules
		{
			get
			{
				return _catalog.ActiveRules;
			}
		}

		public IEnumerable<IJudgeSql> Rules
		{
			get
			{
				return _catalog.Rules;
			}
		}

		public IEnumerable<IJudgeSql> InactiveRules
		{
			get
			{
				return _catalog.InactiveRules;
			}
		}
		
		private SimpleRuleCatalog _catalog;
	}
}

