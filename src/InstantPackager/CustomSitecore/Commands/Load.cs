using System;
using System.Linq;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;

using Sitecore.Install;
using Sitecore.Install.Files;
using Sitecore.Install.Framework;
using Sitecore.Install.Items;
using Sitecore.Install.Zip;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.CustomSitecore.Commands
{
	[Serializable]
	internal class Load : Command
	{
		/// <summary>
		/// Executes the specified context.
		/// Loads a known XML file into the HotPackage item (likely changes everything in the package to an explicit item on initial load)
		/// </summary>
		/// <param name="context">The context.</param>
		public override void Execute(CommandContext context)
		{
			
			throw new NotImplementedException();
		}
	}
}
