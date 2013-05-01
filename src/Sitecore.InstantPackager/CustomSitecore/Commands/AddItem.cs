using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Exceptions;
using Sitecore.SharedSource.InstantPackager.Utils.ItemKeys;
using Sitecore.SharedSource.InstantPackager.Utils.PackageManager;
using Sitecore.Shell.Framework.Commands;
using Sitecore.SharedSource.Commons.Extensions;

namespace Sitecore.SharedSource.InstantPackager.Utils.CustomSitecore.Commands
{
	[Serializable]
	internal class AddItem : Command
	{
		public override void Execute(CommandContext context)
		{

			Assert.ArgumentNotNull(context, "context");

			var foo = new CommandContext();

			InstantPackageManager manager = new InstantPackageManager(new PackageSourceDictionary());

			//get current item
			Item currentItem = context.Items[0];
			if (currentItem == null)
			{
				throw new InvalidItemException("No Item found in the CommandContext.");
			}

			manager.AddItem(new ItemKey(currentItem));

			string recurseVal = context.Parameters.Get("recurse");
			string relatedVal = context.Parameters.Get("related");
			bool recurse = (!string.IsNullOrEmpty(recurseVal) && true.ToString().ToLowerInvariant().CompareTo(recurseVal.ToLowerInvariant()) == 0);
			bool related = (!string.IsNullOrEmpty(relatedVal) && true.ToString().ToLowerInvariant().CompareTo(relatedVal.ToLowerInvariant()) == 0);
			if (recurse)
			{
				IEnumerable<IItemKey> subItems = currentItem.Axes.GetDescendants().Select(x => new ItemKey(x) as IItemKey);
				foreach (IItemKey subItem in subItems)
				{
					manager.AddItem(subItem);
				}
			}
			if (related)
			{
				IEnumerable<IItemKey> relatedItems = currentItem.GetRelatedItems().Select(x => new ItemKey(x) as IItemKey);
				foreach (IItemKey relatedItem in relatedItems)
				{
					manager.AddItem(relatedItem);
				}
			}

			if (InstantPackageManager.IsDebug)
			{
				manager.AlertCurrentInstantPackage();
			}
		}
	}
}
