using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.SessionState;
using Sitecore.SharedSource.Commons.Extensions;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.InstantPackager.Utils.Cache
{
	public class SessionCache : ICache
	{
		private HttpSessionState _state;

		public SessionCache()
		{
			if (Sitecore.Context.ClientPage == null || Sitecore.Context.ClientPage.Session == null)
			{
				throw new NullReferenceException("No client session exists, session cache not accessible.");
			}
			_state = Sitecore.Context.ClientPage.Session;
		}

			public SessionCache(HttpSessionState state)
		{
			_state = state;
		}

		public object Get(string key)
		{
			return _state[key];
		}

		public void Remove(string key)
		{
			_state.Remove(key);
		}

		public void Add(string key, object obj)
		{
			_state[key] = obj;
		}
	}
}
