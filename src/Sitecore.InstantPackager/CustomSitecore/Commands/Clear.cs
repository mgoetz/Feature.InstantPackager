using System;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.InstantPackager.Utils.PackageManager;
using Sitecore.Shell.Framework.Commands;

namespace Sitecore.SharedSource.InstantPackager.Utils.CustomSitecore.Commands
{
	[Serializable]
	internal class Clear : Command
	{
		/// <summary>
		/// Executes the specified context.
		/// Removes all items from the HotPackage
		/// </summary>
		/// <param name="context">The context.</param>
		public override void Execute(CommandContext context)
		{
			Assert.ArgumentNotNull(context, "context");

			InstantPackageManager manager = new InstantPackageManager(new PackageSourceDictionary());
			manager.Clear();

			if (InstantPackageManager.IsDebug)
			{
				manager.AlertCurrentInstantPackage();
			}
		}
	}
}
