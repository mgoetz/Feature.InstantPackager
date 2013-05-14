using System;
using System.Linq;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SharedSource.InstantPackager.Utils.PackageManager;
using Sitecore.Shell.Framework.Commands;

using Sitecore.Install;
using Sitecore.Install.Files;
using Sitecore.Install.Framework;
using Sitecore.Install.Items;
using Sitecore.Install.Zip;

namespace Sitecore.SharedSource.InstantPackager.CustomSitecore.Commands
{
	[Serializable]
	internal class Save : Command
	{
		/// <summary>
		/// Executes the specified context.
		/// Saves a HotPackage project to XML using a naming scheme similar to the one used in the downloader
		/// </summary>
		/// <param name="context">The context.</param>
		public override void Execute(CommandContext context)
		{
			Assert.ArgumentNotNull(context, "context");

			throw new NotImplementedException();

			string currentDateTime = DateTime.UtcNow.ToString("yyyyMMddTHHmmssfff");

			string path = Sitecore.Configuration.Settings.PackagePath;
			string fileName = string.Format("InstantPackage_{0}.xml", currentDateTime);
			string fullPath = path + '\\' + fileName;


			InstantPackageManager packageManager = new InstantPackageManager(new PackageSourceDictionary());
			PackageProject package = packageManager.GetPackage();

			PackageWriter writer = new PackageWriter(fullPath);
			IProcessingContext processingContext = new SimpleProcessingContext();
			writer.Initialize(processingContext);
			PackageGenerator.GeneratePackage(package, writer);

//			package.SaveProject(fullPath);

	
		}
	}
}
