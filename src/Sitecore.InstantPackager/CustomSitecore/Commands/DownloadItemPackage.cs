using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.InstantPackager.Utils.ItemKeys;
using Sitecore.SharedSource.InstantPackager.Utils.PackageManager;
using Sitecore.Shell.Framework.Commands;

using Sitecore.Install;
using Sitecore.Install.Framework;
using Sitecore.Install.Zip;

namespace Sitecore.SharedSource.InstantPackager.CustomSitecore.Commands
{
	[Serializable]
	internal class DownloadItemPackage : Command
	{
		public override void Execute(CommandContext context)
		{
			Assert.ArgumentNotNull(context, "context");

			//get current item
			Item currentItem = context.Items[0];
			string currentItemId = currentItem.ID.ToString();
			string currentItemName = currentItem.Name;
			string currentItemDB = currentItem.Database.Name;
			string currentDateTime = DateTime.UtcNow.ToString("yyyyMMddTHHmmssfff");

			IEnumerable<IItemKey> itemKeys = context.Items.Select(x => new ItemKey(x) as IItemKey);
			string recurseVal = context.Parameters.Get("recurse");
			bool recurse = (!string.IsNullOrEmpty(recurseVal) && true.ToString().ToLowerInvariant().CompareTo(recurseVal.ToLowerInvariant()) == 0);
			if (recurse)
			{
				itemKeys = itemKeys.Union(currentItem.Axes.GetDescendants().Select(x => new ItemKey(x) as IItemKey));
			}
			PackageProject project = InstantPackageManager.GetPackageFromItems(itemKeys);

			string path = Sitecore.Configuration.Settings.PackagePath;
			string fileName = string.Format("{0}{1}_{2}_{3}_{4}.zip", currentItemName, (recurse ? "-WithSubItems" : ""),
											currentItemId.Trim(("{}").ToCharArray()), currentItemDB, currentDateTime);
			string fullPath = path + '\\' + fileName;

			//refactor this into a different object
			project = InstantPackageManager.SetMetaData(project);

			PackageWriter writer = new PackageWriter(fullPath);
			IProcessingContext processingContext = new SimpleProcessingContext();
			writer.Initialize(processingContext);
			PackageGenerator.GeneratePackage(project, writer);

			Context.ClientPage.ClientResponse.Download(fullPath);
		}
	}
}
