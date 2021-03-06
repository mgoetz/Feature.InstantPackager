﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Exceptions;
using Sitecore.SharedSource.InstantPackager.Utils.ItemKeys;
using Sitecore.SharedSource.InstantPackager.Utils.PackageManager;
using Sitecore.Shell.Framework.Commands;

namespace Sitecore.SharedSource.InstantPackager.CustomSitecore.Commands
{
	[Serializable]
	internal class AddSubItems : Command
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

			IEnumerable<IItemKey> subItems = currentItem.Axes.GetDescendants().Select(x => new ItemKey(x) as IItemKey);
			foreach (IItemKey subItem in subItems)
			{
				manager.AddItem(subItem);
			}

			//Used to keep the gutter current:
			String refresh = String.Format("item:refreshchildren(id={0})", currentItem.Parent.ID);
			Sitecore.Context.ClientPage.ClientResponse.Timer(refresh, 2);

			if (InstantPackageManager.IsDebug)
			{
				manager.AlertCurrentInstantPackage();
			}
		}
	}
}
