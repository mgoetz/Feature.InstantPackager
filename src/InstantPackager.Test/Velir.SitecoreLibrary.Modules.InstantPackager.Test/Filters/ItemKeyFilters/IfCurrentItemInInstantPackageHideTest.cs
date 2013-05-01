using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using Sitecore.Data;
using Velir.SitecoreLibrary.Extensions;
using Sitecore.Data.Items;
using Velir.SitecoreLibrary.Modules.InstantPackager.Filters.InstantPackageStatusFilters;
using Velir.SitecoreLibrary.Modules.InstantPackager.Filters.ItemKeyFilters;
using Velir.SitecoreLibrary.Modules.InstantPackager.ItemKeys;
using Velir.SitecoreLibrary.Modules.InstantPackager.PackageManager;
using Velir.SitecoreLibrary.Modules.InstantPackager.Test.Utility;
using Version = Sitecore.Data.Version;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.Test.Filters.ItemKeyFilters
{
	[Category("InstantPackageFilters")]
	[TestFixture]
	class IfCurrentItemInInstantPackageHideTest
	{
		private InstantPackageManager _testInstantPackageManager = null;
		private IItemKey _testItemKey1 = null;
		private IItemKey _testItemKey2 = null;
		private IItemKey _testItemKey3 = null;
		private IItemKey _testItemKey4 = null;

		#region constants
		private Sitecore.Data.Version _ver1 = new Sitecore.Data.Version(1);
		private Sitecore.Data.Version _ver2 = new Version(2);
		private static ID _guid1 = new ID("{00000000-0000-0000-0000-000000000001}");
		private ID _guid2 = new ID("{00000000-0000-0000-0000-000000000002}");
		private ID _guid3 = new ID("{00000000-0000-0000-0000-000000000003}");
		private ID _guid4 = new ID("{00000000-0000-0000-0000-000000000004}");
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

			_testItemKey1 = new ItemKey(_guid1, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testItemKey2 = new ItemKey(_guid2, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testItemKey3 = new ItemKey(_guid3, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testItemKey4 = new ItemKey(_guid4, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);

		}

		[SetUp]
		public void SetUp()
		{
			TestCache.Clear();
			_testInstantPackageManager = new InstantPackageManager(new PackageSourceDictionary(new TestCache()));
		}

		#region Hide(ItemKey itemKey)
		[Test]
		public void Hide_EmptyPackageReturnsFalse()
		{
			//Arrange
			IfCurrentItemInInstantPackageHide testObject = new IfCurrentItemInInstantPackageHide(new TestCache());

			//Act
			bool hidden1 = testObject.Hide(_testItemKey1);
			bool hidden2 = testObject.Hide(_testItemKey2);
			bool hidden3 = testObject.Hide(_testItemKey3);
			bool hidden4 = testObject.Hide(_testItemKey4);

			//Assert
			Assert.IsFalse(hidden1);
			Assert.IsFalse(hidden2);
			Assert.IsFalse(hidden3);
			Assert.IsFalse(hidden4);
		}

		[Test]
		public void Hide_PackageContainsOnlyItemReturnsTrue()
		{
			//Arrange
			IfCurrentItemInInstantPackageHide testObject = new IfCurrentItemInInstantPackageHide(new TestCache());
			_testInstantPackageManager.AddItem(_testItemKey1);

			//Act
			bool hidden = testObject.Hide(_testItemKey1);

			//Assert
			Assert.IsTrue(hidden);
		}

		[Test]
		public void Hide_PackageContainsSeveralItemsIncludingTargetReturnsTrue()
		{
			//Arrange
			IfCurrentItemInInstantPackageHide testObject = new IfCurrentItemInInstantPackageHide(new TestCache());
			_testInstantPackageManager.AddItem(_testItemKey1);
			_testInstantPackageManager.AddItem(_testItemKey2);
			_testInstantPackageManager.AddItem(_testItemKey3);
			_testInstantPackageManager.AddItem(_testItemKey4);

			//Act
			bool hidden = testObject.Hide(_testItemKey1);

			//Assert
			Assert.IsTrue(hidden);
		}

		[Test]
		public void Hide_PackageDoesNotContainItemReturnsFalse()
		{
			//Arrange
			IfCurrentItemInInstantPackageHide testObject = new IfCurrentItemInInstantPackageHide(new TestCache());
			_testInstantPackageManager.AddItem(_testItemKey1);
			_testInstantPackageManager.AddItem(_testItemKey2);
			_testInstantPackageManager.AddItem(_testItemKey3);

			//Act
			bool hidden = testObject.Hide(_testItemKey4);

			//Assert
			Assert.IsFalse(hidden);
		}

		[Test]
		public void Hide_NullItemKeyThrowsArgumentNullException()
		{
			//Arrange
			IfCurrentItemInInstantPackageHide testObject = new IfCurrentItemInInstantPackageHide(new TestCache());
			_testInstantPackageManager.AddItem(_testItemKey1);
			_testInstantPackageManager.AddItem(_testItemKey2);
			_testInstantPackageManager.AddItem(_testItemKey3);

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => testObject.Hide(null));
		}
		#endregion
	}
}
