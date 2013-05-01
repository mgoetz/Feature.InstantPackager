using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Exceptions;
using Sitecore.Shell.Framework.Commands;
using Velir.SitecoreLibrary.Extensions;
using Velir.SitecoreLibrary.Modules.InstantPackager.ItemKeys;
using Velir.SitecoreLibrary.Modules.InstantPackager.PackageManager;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.CustomSitecore.Commands
{
	[Serializable]
	internal class AddRelatedItems : Command
	{
		/// <summary>
		/// Executes the specified context.
		/// Finds and merges in any descendants of the item passed in
		/// </summary>
		/// <param name="context">The context.</param>
		public override void Execute(CommandContext context)
		{
			Assert.ArgumentNotNull(context, "context");

			InstantPackageManager manager = new InstantPackageManager(new PackageSourceDictionary());

			//get current item
			Item currentItem = context.Items[0];
			if (currentItem == null)
			{
				throw new InvalidItemException("No Item found in the CommandContext.");
			}

			IEnumerable<IItemKey> relatedItems = currentItem.GetRelatedItems().Select(x => new ItemKey(x) as IItemKey);
			foreach (IItemKey relatedItem in relatedItems)
			{
				manager.AddItem(relatedItem);
			}

			if (InstantPackageManager.IsDebug)
			{
				manager.AlertCurrentInstantPackage();
			}
		}
	}
}
