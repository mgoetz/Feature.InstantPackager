using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using Sitecore.Data;
using Velir.SitecoreLibrary.Extensions;
using Sitecore.Data.Items;
using Velir.SitecoreLibrary.Modules.InstantPackager.Filters.RelatedItemFilters;
using Velir.SitecoreLibrary.Modules.InstantPackager.ItemKeys;
using Velir.SitecoreLibrary.Modules.InstantPackager.PackageManager;
using Velir.SitecoreLibrary.Modules.InstantPackager.Test.Utility;
using Version = Sitecore.Data.Version;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.Test.Filters.RelatedItemFilters
{
	[Category("InstantPackageFilters")]
	[TestFixture]
	public class IfAllSubItemsInInstantPackageHideTest
	{
		private InstantPackageManager _testInstantPackageManager = null;
		private IItemKey _testSubItemKey1 = null;
		private IItemKey _testSubItemKey2 = null;
		private IItemKey _testSubItemKey3 = null;
		private IItemKey _testSubItemKey4 = null;
		private IItemKey _testItemKey5 = null;

		#region constants

		private Sitecore.Data.Version _ver1 = new Sitecore.Data.Version(1);
		private Sitecore.Data.Version _ver2 = new Version(2);
		private static ID _guid1 = new ID("{00000000-0000-0000-0000-000000000001}");
		private ID _guid2 = new ID("{00000000-0000-0000-0000-000000000002}");
		private ID _guid3 = new ID("{00000000-0000-0000-0000-000000000003}");
		private ID _guid4 = new ID("{00000000-0000-0000-0000-000000000004}");
		private ID _guid5 = new ID("{00000000-0000-0000-0000-000000000005}");
		private string mockLanguageWrapperEnglish_langEnglish = "en";
		private string _langSpanish = "es";
		private string _webDbName = "web";
		private string _masterDbName = "master";
		private string _coreDbName = "core";
		private string _path1 = "sitecore/home/Item1";

		#endregion

		[TestFixtureSetUp]
		public void SetUpFixture()
		{
			ILanguageWrapper mockLanguageWrapperEnglish = MockRepository.GenerateMock<ILanguageWrapper>();
			mockLanguageWrapperEnglish.Stub(x => x.ToString()).Return("en");

			_testSubItemKey1 = new ItemKey(_guid1, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testSubItemKey2 = new ItemKey(_guid2, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testSubItemKey3 = new ItemKey(_guid3, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testSubItemKey4 = new ItemKey(_guid4, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testItemKey5 = new ItemKey(_guid5, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);

		}

		[SetUp]
		public void SetUp()
		{
			TestCache.Clear();
			_testInstantPackageManager = new InstantPackageManager(new PackageSourceDictionary(new TestCache()));
		}

		#region Hide(IEnumerable<IItemKey> itemList)

		[Test]
		public void Hide_ContainsAllItemsReturnsTrue()
		{
			//Arrange
			IfAllSubItemsInInstantPackageHide testObject = new IfAllSubItemsInInstantPackageHide(new TestCache());
			List<IItemKey> subItems = new List<IItemKey>() {_testSubItemKey1, _testSubItemKey2, _testSubItemKey3, _testSubItemKey4};
			_testInstantPackageManager.AddItem(_testSubItemKey1);
			_testInstantPackageManager.AddItem(_testSubItemKey2);
			_testInstantPackageManager.AddItem(_testSubItemKey3);
			_testInstantPackageManager.AddItem(_testSubItemKey4);

			//Act
			bool hidden = testObject.Hide(subItems);

			//Assert
			Assert.IsTrue(hidden);
		}

		[Test]
		public void Hide_ContainsSomeItemsReturnsFalse()
		{
			//Arrange
			IfAllSubItemsInInstantPackageHide testObject = new IfAllSubItemsInInstantPackageHide(new TestCache());
			List<IItemKey> subItems = new List<IItemKey>() { _testSubItemKey1, _testSubItemKey2, _testSubItemKey3, _testSubItemKey4 };
			_testInstantPackageManager.AddItem(_testSubItemKey1);
			_testInstantPackageManager.AddItem(_testSubItemKey2);
			_testInstantPackageManager.AddItem(_testItemKey5);

			//Act
			bool hidden = testObject.Hide(subItems);

			//Assert
			Assert.IsFalse(hidden);
		}

		[Test]
		public void Hide_ContainsNoItemsReturnsFalse()
		{
			//Arrange
			IfAllSubItemsInInstantPackageHide testObject = new IfAllSubItemsInInstantPackageHide(new TestCache());
			List<IItemKey> subItems = new List<IItemKey>() { _testSubItemKey1, _testSubItemKey2, _testSubItemKey3, _testSubItemKey4 };
			_testInstantPackageManager.AddItem(_testItemKey5);

			//Act
			bool hidden = testObject.Hide(subItems);

			//Assert
			Assert.IsFalse(hidden);
		}

		[Test]
		public void Hide_EmptyPackageReturnsFalse()
		{
			//Arrange
			IfAllSubItemsInInstantPackageHide testObject = new IfAllSubItemsInInstantPackageHide(new TestCache());
			List<IItemKey> subItems = new List<IItemKey>() { _testSubItemKey1, _testSubItemKey2, _testSubItemKey3, _testSubItemKey4 };

			//Act
			bool hidden = testObject.Hide(subItems);

			//Assert
			Assert.IsFalse(hidden);
		}

		[Test]
		public void Hide_NoSubItemsEmptyPackageReturnsTrue()
		{
			//Arrange
			IfAllSubItemsInInstantPackageHide testObject = new IfAllSubItemsInInstantPackageHide(new TestCache());
			List<IItemKey> subItems = new List<IItemKey>() { };

			//Act
			bool hidden = testObject.Hide(subItems);

			//Assert
			Assert.IsTrue(hidden);
		}

		[Test]
		public void Hide_NoSubItemsPopulatePackageReturnsTrue()
		{
			//Arrange
			IfAllSubItemsInInstantPackageHide testObject = new IfAllSubItemsInInstantPackageHide(new TestCache());
			_testInstantPackageManager.AddItem(_testItemKey5);
			List<IItemKey> subItems = new List<IItemKey>() { };

			//Act
			bool hidden = testObject.Hide(subItems);

			//Assert
			Assert.IsTrue(hidden);
		}

		[Test]
		public void Hide_NullListThrowsException()
		{
			//Arrange
			IfAllSubItemsInInstantPackageHide testObject = new IfAllSubItemsInInstantPackageHide(new TestCache());
			List<IItemKey> nullSubItems = null;

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => testObject.Hide(nullSubItems));
		}
		#endregion
	}
}
