using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;
using Sitecore.SharedSource.InstantPackager.Utils.ItemKeys;
using Sitecore.SharedSource.InstantPackager.Utils.PackageManager;
using Sitecore.Shell.Applications.ContentEditor.Gutters;

namespace Sitecore.SharedSource.InstantPackager.CustomSitecore.Gutters
{
	class InstantPackageGutter : GutterRenderer
	{
		protected override GutterIconDescriptor GetIconDescriptor(Item item)
		{
			GutterIconDescriptor gutterIcon = null;
			InstantPackageManager manager = new InstantPackageManager(new PackageSourceDictionary());
			IItemKey itemKey = new ItemKey(item);
			if (manager.Contains(itemKey))
			{
				gutterIcon = new GutterIconDescriptor();
				gutterIcon.Icon = "Core2/32x32/attach.png";
				gutterIcon.Tooltip = "This item will be downloaded with the next InstantPackage.";
			}
			return gutterIcon;
		}
	}
}
