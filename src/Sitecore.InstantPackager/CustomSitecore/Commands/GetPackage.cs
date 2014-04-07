using System;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.InstantPackager.Utils.PackageManager;
using Sitecore.Shell.Framework.Commands;

using Sitecore.Install;
using Sitecore.Install.Framework;
using Sitecore.Install.Zip;

namespace Sitecore.SharedSource.InstantPackager.CustomSitecore.Commands
{

	[Serializable]
	internal class GetPackage : Command
	{

		public override void Execute(CommandContext context)
		{

			Assert.ArgumentNotNull(context, "context");

			InstantPackageManager packageManager = new InstantPackageManager(new PackageSourceDictionary());
			PackageProject package = packageManager.GetPackage();

			string currentDateTime = DateTime.UtcNow.ToString(InstantPackageManager.SortableTimeFormat);//possibly obsolete with use of version number.

			string path = Sitecore.Configuration.Settings.PackagePath;
			string fileName = string.Format("InstantPackage_{0}-{1}.zip", Sitecore.Context.Site.HostName, currentDateTime);
			string fullPath = path + '\\' + fileName;

			//refactor this into a different object
			package = InstantPackageManager.SetMetaData(package);

			PackageWriter writer = new PackageWriter(fullPath);
			IProcessingContext processingContext = new SimpleProcessingContext();
			writer.Initialize(processingContext);
			PackageGenerator.GeneratePackage(package, writer);

			Context.ClientPage.ClientResponse.Download(fullPath);
		}
	}
}

