using System;
using System.Collections.Generic;
using Sitecore.Data.Items;
using Velir.SitecoreLibrary.Extensions;
using Velir.SitecoreLibrary.Modules.InstantPackager.Cache;
using Velir.SitecoreLibrary.Modules.InstantPackager.ItemKeys;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.PackageManager
{
	public class PackageSourceDictionary : IPackageManagerDataSource
	{
		private static readonly Object LockObject;

		private static ICache _cache = null;


		/// <summary>
		/// Our static constructor for initializing our static properties.
		/// </summary>
		static PackageSourceDictionary()
		{
			//datastore.AddItem, removeSubItems, etc.
			LockObject = new object();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PackageSourceDictionary"/> class.
		/// </summary>
		/// <param name="cache">The cache.</param>
		public PackageSourceDictionary(ICache cache)
		{
			_cache = cache;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PackageSourceDictionary"/> class.
		/// </summary>
		/// <param name="cache">The cache.</param>
		public PackageSourceDictionary()
		{
			_cache = new SessionCache();//Whao, cool, I got to abstract away how to get the session cache!
		}

		private SortedDictionary<string, IItemKey> _items;
		public SortedDictionary<string, IItemKey> Items
		{
			get
			{
				if (_items == null)
				{
					//System.Web.Caching.Cache cache = Sitecore.Context.ClientPage.Cache;
					string cacheKey = InstantPackageManager.InstantPackageSessionKey;// +Sitecore.Context.ClientPage.Session.SessionID;

					//if our cache entry is still null then add our results
					if (_cache.Get(cacheKey) == null)
					{
						//lock our object to prevent multiple threads from writing to it at the same time
						lock (LockObject)
						{
							//double check to make sure our cache entry is still null
							if (_cache.Get(cacheKey) == null)
							{
								_items = new SortedDictionary<string, IItemKey>();
								_cache.Add(cacheKey, _items);
							}
							else
							{
								_items = (SortedDictionary<string, IItemKey>)_cache.Get(cacheKey);
							}
						}
					}
					else
					{
						_items = (SortedDictionary<string, IItemKey>)_cache.Get(cacheKey);
					}

				}
				return _items;
			}

		}

		public void Add(IItemKey item)
		{
			string itemKeyString = item.GetKeyString();
			if (!Items.ContainsKey(itemKeyString))
			{
				Items.Add(itemKeyString, item);
			}
		}

		public void Remove(IItemKey item)
		{
			string itemKeyString = item.GetKeyString();
			if (Items.ContainsKey(itemKeyString))
			{
				Items.Remove(itemKeyString);
			}
		}

		public void Clear()
		{
			Items.Clear();
			Items.GetEnumerator();
		}

		public int Count
		{
			get { return Items.Count; }
		}

		public IEnumerator<IItemKey> GetEnumerator()
		{
			return Items.Values.GetEnumerator();
		}

		public bool Contains(IItemKey item)
		{
			return Items.ContainsKey(item.GetKeyString());
		}
	}
}
