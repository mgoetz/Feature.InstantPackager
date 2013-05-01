using System;
using Sitecore.Data;
using Sitecore.Data.Items;
using Version = Sitecore.Data.Version;

namespace Sitecore.SharedSource.InstantPackager.Utils.ItemKeys
{
	public class ItemKey : IItemKey
	{
		public ItemKey(Item item)
		{
			this.Initialize(item.ID, item.Paths.FullPath, item.Version, (LanguageWrapper)item.Language, item.Database.Name);
		}

		public ItemKey(ID guid, string path, Version version, ILanguageWrapper language, string databaseName)
		{
			this.Initialize(guid, path, version, language, databaseName);
		}

//		public void Initialize(Item item)
//		{
//			Initialize(item.ID, item.Paths.FullPath, item.Version, (LanguageWrapper)item.Language, item.Database.Name);
//		}

		private void Initialize(ID guid, string path, Version version, ILanguageWrapper language, string databaseName)
		{
			if (guid == (ID)null || path == null || version == null || language == null || databaseName == null)
			{
				throw new ArgumentNullException();
			}
			if (path == string.Empty || databaseName == string.Empty)
			{
				throw new ArgumentException();
			}
			_guid = guid;
			_path = path;
			_version = version;
			_language = language;
			_databaseName = databaseName;
		}

		private ID _guid;
		public ID Guid
		{
			get { return _guid; }
		}

		private string _path;
		public string Path
		{
			get { return _path; }
		}

		private Version _version;
		public Version Version
		{
			get { return _version; }
		}

		private ILanguageWrapper _language;
		public ILanguageWrapper Language
		{
			get { return _language; }
		}

		private string _databaseName;
		public string DatabaseName
		{
			get { return _databaseName; }
		}

		public string GetKeyString()
		{
			return ToString();
		}

		public override string ToString()
		{
			string itemKeyText = string.Format("{0}|{1}|{2}|{3}|{4}", Path, Guid, DatabaseName, Language.ToString(), Version);
			return itemKeyText;
		}
	}
}
