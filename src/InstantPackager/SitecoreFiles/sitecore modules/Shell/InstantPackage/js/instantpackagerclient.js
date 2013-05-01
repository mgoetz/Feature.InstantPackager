var VelirInstantPackager = VelirInstantPackager || {};


//the path to opening the listing for the Instant Package
//VelirInstantPackager.ListPath = '/sitecore%20modules/Shell/InstantPackage/ControlRetriever.aspx?targetcontrol=Installer.CustomDesigner';
VelirInstantPackager.ListPath = '/sitecore/shell/default.aspx?xmlcontrol=InstantPackage.InstantPackageList'

VelirInstantPackager.WarnAndExecute = function (guid, obj) {
	var command = obj.value;
	switch (command) {
		case 'removefromip':
			VelirInstantPackager.Remove(guid);
			break;
		case 'removesubfromip':
			VelirInstantPackager.RemoveSubItems(guid);
			break;
		case 'removefromiprecurse':
		case 'removewithsubfromip':
			VelirInstantPackager.RemoveWithSubItems(guid);
			break;
		case 'removerelatedfromip':
			VelirInstantPackager.RemoveRelatedItems(guid);
			break;
		case 'addsubtoip':
			VelirInstantPackager.AddsubItems(guid);
			break;
		case 'addrelatedtoip':
			VelirInstantPackager.AddRelatedItems(guid);
			break;
		default:
			return;
	}
	alert(guid + command);
	document.location.reload(true);
};

VelirInstantPackager.Remove = function (guid) {
	alert('velir:removefromip(id=' + guid + ')');
	scForm.invoke('velir:removefromip(id=' + guid + ')');
};

VelirInstantPackager.RemoveWithSubItems = function (guid) {
	scForm.invoke('velir:removefromip(id=' + guid + ', recurse=true)');
};

VelirInstantPackager.RemoveSubItems = function (guid) {
	scForm.invoke('velir:removesubfromip(id=' + guid + ')');
};

VelirInstantPackager.RemoveRelatedItems = function (guid) {
	scForm.invoke('velir:removerelatedfromip(id=' + guid + ')');
};

VelirInstantPackager.AddItems = function (guid) {
	scForm.invoke('velir:addtoip(id=' + guid + ')');
};

VelirInstantPackager.AddSubItems = function (guid) {
	scForm.invoke('velir:addsubtoip(id=' + guid + ')');
};

VelirInstantPackager.AddRelatedItems = function (guid) {
	scForm.invoke('velir:addrelatedtoip(id=' + guid + ')');
};

VelirInstantPackager.GoToItem = function (guid) {
};



