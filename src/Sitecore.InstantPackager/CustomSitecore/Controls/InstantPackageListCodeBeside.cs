using System;
using System.Collections.Generic;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.SharedSource.InstantPackager.Utils.ItemKeys;
using Sitecore.SharedSource.InstantPackager.Utils.PackageManager;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;

//TODO: Move this calls out of the SitecoreModules folder
namespace Sitecore.SharedSource.InstantPackager.Utils.CustomSitecore.Controls
{
	class InstantPackageListCodeBeside : BaseForm
	{
		#region Fields

		protected Button Cancel;
		protected GridPanel Grid;
		protected Literal listingLiteral;
		protected Placeholder instantPackageListingPlaceholder;
		#endregion

		private InstantPackageManager _packageManager = new InstantPackageManager(new PackageSourceDictionary());
		#region Page Events

		protected override void OnLoad(EventArgs eventArgs)
		{
			Assert.ArgumentNotNull(eventArgs, "eventArgs");
			base.OnLoad(eventArgs);

			//If not a Client Event then attempt to load item id and language from QueryString
			if (!Context.ClientPage.IsEvent)
			{
				Context.ClientPage.ServerProperties["id"] = WebUtil.GetQueryString("id");
				Context.ClientPage.ServerProperties["language"] = WebUtil.GetQueryString("la", Language.Current.Name);
			}

			//Set the Custom Page Events
			//this.Version1.OnChange += new EventHandler(this.OnUpdate);
			//this.PublishingTargets.OnChange += new EventHandler(this.OnUpdate);
			//this.OK.OnClick += new EventHandler(ItemComparerViewerCodeBeside.OnOK);
			//this.Cancel.OnClick += new EventHandler(ItemComparerViewerCodeBeside.OnCancel);
			//this.publishButton.OnClick += new EventHandler(publishButton_OnClick);
			//this.publishButton.OnClick += new EventHandler(publishButton_OnClick);
			//this.viewTargetItemButton.OnClick += new EventHandler(viewTargetItemButton_OnClick);
			//this.viewTargetItemButton.Width = 150;
		}

		protected override void OnPreRender(EventArgs eventArgs)
		{
			instantPackageListingPlaceholder.Visible = false;
			Assert.ArgumentNotNull(eventArgs, "eventArgs");
			if (eventArgs == null)
			{
				SheerResponse.Alert("eventArgs is null");
				return;
			}

			//If this is a client page event then exit
			if (Context.ClientPage.IsEvent)
			{
				return;
			}


			// TODO: Do I load the rows here?
			string rendering = GetInstantPackageControl();

			listingLiteral.Text = rendering;

			instantPackageListingPlaceholder.Visible = true;
		}

		#endregion

		private string GetInstantPackageControl()
		{
			string instantPackageTable = "<table>";
			IEnumerator<IItemKey> enumerator = _packageManager.GetEnumerator();
			while (enumerator.MoveNext())
			{
				IItemKey item = (IItemKey)enumerator.Current;
				instantPackageTable += GenerateItemHtml(item);
			}
			instantPackageTable += "</table>";
			return instantPackageTable;
		}

		private string GenerateItemHtml(IItemKey item)
		{
			string itemRow = "<tr>";
			itemRow += "<td>" + item.Path + ", " + item.DatabaseName + ", " + item.Language.ToString() + ", " + item.Version + "</td>";
			itemRow += "<td>" + GenerateItemActionHtml(item) + "</td>";
			itemRow += "</tr>";
			return itemRow;
		}

		private string GenerateItemActionHtml(IItemKey item)
		{

			string itemRowActions = "<select onchange=\"VelirInstantPackager.WarnAndExecute(\'"+item.Guid+"\', this)\">";
			itemRowActions += "<option value=\"\" selected = \"selected\" >- Actions -</option>";
			itemRowActions += "<option value=\"removefromip\" >Remove</option>";
			itemRowActions += "<option value=\"removesubfromip\" >Remove SubItems</option>";
			itemRowActions += "<option value=\"removewithsubfromip\" >Remove With SubItems</option>";
			itemRowActions += "<option value=\"removerelatedfromip\" >Remove related Items</option>";
			itemRowActions += "<option value=\"removefromiprecurse\" >Remove With SubItems</option>";
			itemRowActions += "<option value=\"addsubtoip\" >Add SubItems</option>";
			itemRowActions += "<option value=\"addrelatedtoip\" >Add [Directly] Related Items</option>";
			itemRowActions += "</select>";
			return itemRowActions;
		}
	}
}
