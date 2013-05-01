using System.Collections.Generic;
using Sitecore.SharedSource.InstantPackager.Utils.Cache;

namespace Sitecore.SharedSource.InstantPackager.Utils.Test.Utility
{
	public class TestCache : ICache
	{
		private static Dictionary<string, object> _cache;

		static TestCache()
		{
			_cache = new Dictionary<string, object>(4); //Why 4?  Why not!
		}

		public object Get(string key)
		{
			object value = null;
			_cache.TryGetValue(key, out value);
			return value;
		}

		public void Remove(string key)
		{
			_cache.Remove(key);
		}

		public void Add(string key, object obj)
		{
			_cache.Add(key, obj);
		}

		public static void Clear()
		{
			_cache.Clear();
		}
	}

}
