﻿using Sitecore.Data;
using Sitecore.Data.Items;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.ItemKeys
{
	public interface IItemKey
	{
//		void Initialize(Item item);
//		void Initialize(ID guid, string fullPath, Version version, ILanguageWrapper language, string databaseName);
		ID Guid { get; }
		string Path { get; }
		Version Version { get; }
		ILanguageWrapper Language { get; }
		string DatabaseName { get; }
		string GetKeyString();
	}
}
