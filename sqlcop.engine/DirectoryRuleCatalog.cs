using System;
using System.Collections.Generic;
using System.IO;

namespace sqlcop.engine
{
	public class DirectoryRuleCatalog : ICatalogRules
	{
		public DirectoryRuleCatalog()
		{
			_catalog = new SimpleRuleCatalog();
		}
		
		public void AddDirectory(string path)
		{
			AssemblyRuleCatalog catalog = new AssemblyRuleCatalog();
			DirectoryInfo directory = new DirectoryInfo(path);
			foreach(FileInfo file in directory.GetFiles())
			{
				if(file.Extension.ToLower() == ".dll" ||
				   file.Extension.ToLower() == ".exe")
				{
					try
					{
						catalog.AddAssemblyFrom(file.FullName);
					}
					catch(Exception) {}
				}
			}
			foreach(IJudgeSql activeRule in catalog.ActiveRules)
			{
				_catalog.AddActive(activeRule);
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
			get { return _catalog.ActiveRules; }
		}

		public IEnumerable<IJudgeSql> Rules
		{
			get { return _catalog.Rules; }
		}

		public IEnumerable<IJudgeSql> InactiveRules
		{
			get { return _catalog.InactiveRules; }
		}
		
		private SimpleRuleCatalog _catalog;
	}
}

