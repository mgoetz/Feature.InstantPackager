using System;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;

using Sitecore.Install;
using Sitecore.Install.Framework;
using Sitecore.Install.Zip;
using Velir.SitecoreLibrary.Modules.InstantPackager.PackageManager;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.CustomSitecore.Commands
{

	[Serializable]
	internal class GetPackage : Command
	{

		public override void Execute(CommandContext context)
		{

			Assert.ArgumentNotNull(context, "context");

			string currentDateTime = DateTime.UtcNow.ToString("yyyyMMddTHHmmssfff");

			string path = Sitecore.Configuration.Settings.PackagePath;
			string fileName = string.Format("InstantPackage_{0}.zip", currentDateTime);
			string fullPath = path + '\\' + fileName;


			InstantPackageManager packageManager = new InstantPackageManager(new PackageSourceDictionary());
			PackageProject package = packageManager.GetPackage();

			PackageWriter writer = new PackageWriter(fullPath);
			IProcessingContext processingContext = new SimpleProcessingContext();
			writer.Initialize(processingContext);
			PackageGenerator.GeneratePackage(package, writer);

			Context.ClientPage.ClientResponse.Download(fullPath);
		}
	}
}
