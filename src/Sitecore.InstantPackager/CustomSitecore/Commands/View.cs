using System;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.InstantPackager.Utils.PackageManager;
using Sitecore.Shell.Framework.Commands;

namespace Sitecore.SharedSource.InstantPackager.CustomSitecore.Commands
{
	[Serializable]
	internal class View : Command
	{
		/// <summary>
		/// Executes the specified context.
		/// Removes all items from the HotPackage
		/// </summary>
		/// <param name="context">The context.</param>
		public override void Execute(CommandContext context)
		{
			Assert.ArgumentNotNull(context, "context");

			//AlertCurrentInstantPackage();

			ViewInstantPackageManager();
		}

		/// <summary>
		/// Alerts the current instant package contents to the content editor.
		/// </summary>
		private void AlertCurrentInstantPackage()
		{
			InstantPackageManager manager = new InstantPackageManager(new PackageSourceDictionary());
			Sitecore.Context.ClientPage.ClientResponse.Alert("Current Package Includes:\n" + manager.GetItemsListText());
		}

		private void ViewInstantPackageManager()
		{
			string InstantPackageManagerControlPath ="/sitecore/shell/default.aspx?xmlcontrol=InstantPackage.InstantPackageList";

			string instantPackageManagerPath = "/sitecore%20modules/shell/InstantPackage/InstantPackageList.xml";

			InstantPackageManager manager = new InstantPackageManager(new PackageSourceDictionary());
			Sitecore.Context.ClientPage.ClientResponse.ShowModalDialog(InstantPackageManagerControlPath);//gets url
			//Sitecore.Context.ClientPage.ClientResponse.SetDialogValue(manager.GetItemsListText());
		}
	}
}
