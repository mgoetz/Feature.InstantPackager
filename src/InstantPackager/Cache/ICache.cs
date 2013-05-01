using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.Cache
{
	public interface ICache
	{
		object Get(string key);
		void Remove(string key);
		void Add(string key, Object obj);
	}
}
