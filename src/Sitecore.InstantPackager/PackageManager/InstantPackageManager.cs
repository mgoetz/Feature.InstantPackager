using System;
using System.Collections.Generic;
using System.Web.Caching;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.Install;
using Sitecore.Data.Items;
using Sitecore.Install.Framework;
using Sitecore.Install.Items;
using Sitecore.SharedSource.InstantPackager.Utils.ItemKeys;

namespace Sitecore.SharedSource.InstantPackager.Utils.PackageManager
{
	public class InstantPackageManager 
	{

		public static string InstantPackageSessionKey = "InstantPackage.Items";
		/// <summary>
		/// The lock object used when writing to the cache.
		/// </summary>
		private static readonly Object LockObject;

		public static readonly bool IsDebug = false;

		/// <summary>
		/// Our static constructor for initializing our static properties.
		/// </summary>
		static InstantPackageManager()
		{
			LockObject = new object();
		}
		
		public InstantPackageManager(IPackageManagerDataSource store)
		{
			_items = store;
		}


		private volatile IPackageManagerDataSource _items = null;
		/// <summary>
		/// Gets the items.
		/// </summary>
		private IPackageManagerDataSource Items
		{
			get
			{
				return _items;
			}

		}

		/// <summary>
		/// Takes in a list of items and generates a PackageProject object for those items.
		/// </summary>
		/// <param name="itemKeys">The items.</param>
		/// <returns></returns>
		public static PackageProject GetPackageFromItems(IEnumerable<IItemKey> itemKeys)
		{
			PackageProject project = new PackageProject();
			ExplicitItemSource.Builder explicitSourceBuilder = new ExplicitItemSource.Builder();
			foreach (IItemKey item in itemKeys)
			{
				//Re-fetch the item at the time of packaging to make sure it's current
				Item reFetchedItem = Sitecore.Data.Database.GetDatabase(item.DatabaseName).GetItem(item.Guid, LanguageManager.DefaultLanguage, item.Version);
			
				IEntryData ied = new ItemEntryData(reFetchedItem);
				PackageEntry iedEntry = new PackageEntry(ied);
				explicitSourceBuilder.Put(iedEntry);
			}
			ExplicitItemSource explicitSource = explicitSourceBuilder.Source;
			project.Sources.Add(explicitSource);
			return project;
		}

		/// <summary>
		/// Gets a PackageProject file for the items currently in the users/sessions InstantPackage.
		/// </summary>
		/// <returns></returns>
		public PackageProject GetPackage()
		{
			List<IItemKey> itemList = new List<IItemKey>();
			IEnumerator<IItemKey> enumerator = Items.GetEnumerator();
			while (enumerator.MoveNext())
			{
				IItemKey item = (IItemKey)enumerator.Current;
				itemList.Add(item);
			}
			return GetPackageFromItems(itemList);
		}

		/// <summary>
		/// Alerts the current instant package contents to the content editor.
		/// </summary>
		[Obsolete()]
		public void AlertCurrentInstantPackage()
		{
			Sitecore.Context.ClientPage.ClientResponse.Alert("Current Package Includes:\n" + GetItemsListText());
			//Sitecore.Context.ClientPage.ClientResponse.ShowModalDialog(GetItemsListText());//gets url
			//Sitecore.Context.ClientPage.ClientResponse.SetDialogValue(GetItemsListText());
		}

		/// <summary>
		/// Gets a string representation of the items in the Instant Package list
		/// </summary>
		/// <returns></returns>
		/// <Remarks> testable, to a degree (mostly only with detailed implementation knowledge)</Remarks>
		public string GetItemsListText()
		{
			string retVal = string.Empty;
			if (Items.Count == 0)
			{
				retVal = "No Items in Instant Package.";
			}
			else
			{
				IEnumerator<IItemKey> enumerator = Items.GetEnumerator();
				while (enumerator.MoveNext())
				{
					IItemKey itemKey = (IItemKey)enumerator.Current;
					retVal += itemKey.Path + ", " + itemKey.Guid.ToString() + ", " + itemKey.DatabaseName + ", " + itemKey.Language.ToString() + ", " + itemKey.Version.ToString() + "\n";
				}
			}
			return retVal;
		}

		/// <summary>
		/// Adds an item to the instant package.
		/// </summary>
		/// <param name="item">The item.</param>
		// TODO: Testable
		public void AddItem(IItemKey itemKey)
		{
			Items.Add(itemKey);
		}

		/// <summary>
		/// Removes an item from the instant package.
		/// (Items must match on DB, version, guid)
		/// </summary>
		/// <param name="item">The item.</param>
		// TODO: Testable
		public void RemoveItem(IItemKey itemKey)
		{
			Items.Remove(itemKey);
		}

		/// <summary>
		/// Clears the InstantPackage.
		/// </summary>
		// TODO: Testable
		public void Clear()
		{
			Items.Clear();
		}


		/// <summary>
		/// Saves the XML for the current InstantPackage.
		/// </summary>
		/// <param name="fileName">Name of the file. [Optional]</param>
		public void Save(string fileName)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Loads a package file into the InstantPackage.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		public void Load(string fileName)
		{
			throw new NotImplementedException();
		}

		// TODO: Testable
		public bool Contains(IItemKey item)
		{
			return Items.Contains(item);
		}

		// TODO: Testable
		public int Count { get { return Items.Count; } }

		// TODO: Testable
		public IEnumerator<IItemKey> GetEnumerator()
		{
			return Items.GetEnumerator();
		}
	}
}
