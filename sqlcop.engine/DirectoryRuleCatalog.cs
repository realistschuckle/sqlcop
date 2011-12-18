using System;
using System.Collections.Generic;
using System.IO;

namespace sqlcop.engine
{
	public class DirectoryRuleCatalog : BaseProxyCatalog
	{
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
				AddActiveRule(activeRule);
			}
		}
	}
}

