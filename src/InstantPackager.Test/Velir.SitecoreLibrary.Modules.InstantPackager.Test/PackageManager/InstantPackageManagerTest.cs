using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using Sitecore.Data;
using Sitecore.Globalization;
using Velir.SitecoreLibrary.Extensions;
using Velir.SitecoreLibrary.Modules.InstantPackager.ItemKeys;
using Velir.SitecoreLibrary.Modules.InstantPackager.PackageManager;
using Velir.SitecoreLibrary.Modules.InstantPackager.Test.Utility;
using Version = Sitecore.Data.Version;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.Test.PackageManager
{
	[Category("InstantPackageManager")]
	[TestFixture]
	class InstantPackageManagerTest
	{
		private InstantPackageManager _testInstantPackageManager = null;
		private IItemKey _testItemKey1 = null;
		private IItemKey _testItemKey1Duplicate1 = null;
		private IItemKey _testItemKey1Duplicate2 = null;
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
			ILanguageWrapper mockLanguageWrapperSpanish = MockRepository.GenerateMock<ILanguageWrapper>();
			mockLanguageWrapperEnglish.Stub(x => x.ToString()).Return("en");
			mockLanguageWrapperSpanish.Stub(x => x.ToString()).Return("en");

			Language foo = null;
			_testItemKey1 = new ItemKey(_guid1, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testItemKey1Duplicate1 = new ItemKey(_guid1, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testItemKey1Duplicate2 = new ItemKey(_guid1, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testItemKey2 = new ItemKey(_guid2, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testItemKey3 = new ItemKey(_guid3, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testItemKey4 = new ItemKey(_guid4, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);

			//_testInstantPackageManager = new PackageSourceDictionary();// This is in the test setup instead of fixture setup so each test gets a clean start.
		}

		[SetUp]
		public void SetUp()
		{
			TestCache.Clear();
			_testInstantPackageManager = new InstantPackageManager(new PackageSourceDictionary(new TestCache()));
			//_testInstantPackageManager.Clear();// This is part of the API we're testing, so it shouldn't be in the setup!
		}

		[TearDown]
		public void TearDown()
		{
		}



		#region public void AddItem(ItemKey item)
		[Test]
		public void AddItem_AddNewToEmpty()
		{
			//Arrange

			//Action
			_testInstantPackageManager.AddItem(_testItemKey1);

			//Assert
			Assert.AreEqual(1, _testInstantPackageManager.Count);
			Assert.IsTrue(_testInstantPackageManager.Contains(_testItemKey1));
		}

		[Test]
		public void Add_AddNewToPopulated()
		{
			//Arrange

			//Action
			_testInstantPackageManager.AddItem(_testItemKey1);
			_testInstantPackageManager.AddItem(_testItemKey2);
			_testInstantPackageManager.AddItem(_testItemKey3);

			//Assert
			Assert.AreEqual(3, _testInstantPackageManager.Count);
			Assert.IsTrue(_testInstantPackageManager.Contains(_testItemKey1));
			Assert.IsTrue(_testInstantPackageManager.Contains(_testItemKey2));
			Assert.IsTrue(_testInstantPackageManager.Contains(_testItemKey3));
		}

		[Test]
		public void Add_AddDuplicateToPopulatedDoesNotDuplicateInSource()
		{
			//Arrange

			//Action
			_testInstantPackageManager.AddItem(_testItemKey1);
			_testInstantPackageManager.AddItem(_testItemKey2);
			_testInstantPackageManager.AddItem(_testItemKey1Duplicate1);

			//Assert
			Assert.AreEqual(2, _testInstantPackageManager.Count);
			Assert.IsTrue(_testInstantPackageManager.Contains(_testItemKey1));
			Assert.IsTrue(_testInstantPackageManager.Contains(_testItemKey2));
			Assert.IsTrue(_testInstantPackageManager.Contains(_testItemKey1Duplicate1));
		}

		[Test]
		public void Add_AddSeveralDuplicatesDoesNotDuplicateInSource()
		{
			//Arrange

			//Action
			_testInstantPackageManager.AddItem(_testItemKey1);
			_testInstantPackageManager.AddItem(_testItemKey1Duplicate1);
			_testInstantPackageManager.AddItem(_testItemKey1Duplicate2);

			//Assert
			Assert.AreEqual(1, _testInstantPackageManager.Count);
			Assert.IsTrue(_testInstantPackageManager.Contains(_testItemKey1));
			Assert.IsTrue(_testInstantPackageManager.Contains(_testItemKey1Duplicate1));
			Assert.IsTrue(_testInstantPackageManager.Contains(_testItemKey1Duplicate2));
		}

		#endregion

		#region void Remove(ItemKey item);
		[Test]
		public void RemoveItem_RemoveFromEmptyList()
		{
			//Arrange

			//Act
			int beforeCount = _testInstantPackageManager.Count;
			_testInstantPackageManager.RemoveItem(_testItemKey1);
			int afterCount = _testInstantPackageManager.Count;

			//Assert
			Assert.IsFalse(_testInstantPackageManager.Contains(_testItemKey1));
			Assert.That(afterCount, Is.EqualTo(beforeCount));
			Assert.That(afterCount, Is.EqualTo(0));
		}

		[Test]
		public void RemoveItem_RemoveItemWhereNotIncludedMakesNoChange()
		{
			//Arrange
			_testInstantPackageManager.AddItem(_testItemKey1);

			//Act
			int beforeCount = _testInstantPackageManager.Count;
			_testInstantPackageManager.RemoveItem(_testItemKey2);
			int afterCount = _testInstantPackageManager.Count;

			//Assert
			Assert.IsFalse(_testInstantPackageManager.Contains(_testItemKey2));
			Assert.That(afterCount, Is.EqualTo(beforeCount));
		}

		[Test]
		public void RemoveItem_RemoveItemWhereIncludedRemoves()
		{
			//Arrange
			_testInstantPackageManager.AddItem(_testItemKey1);
			_testInstantPackageManager.AddItem(_testItemKey2);

			//Act
			int beforeCount = _testInstantPackageManager.Count;
			_testInstantPackageManager.RemoveItem(_testItemKey1);
			int afterCount = _testInstantPackageManager.Count;

			//Assert
			Assert.IsFalse(_testInstantPackageManager.Contains(_testItemKey1));
			Assert.That(afterCount, Is.EqualTo(beforeCount - 1));
		}

		[Test]
		public void RemoveItem_MultipleRemovesWhereIncluddeRemovesAndDoesntThrowException()
		{
			//Arrange
			_testInstantPackageManager.AddItem(_testItemKey1);
			_testInstantPackageManager.AddItem(_testItemKey2);

			//Act
			int beforeCount = _testInstantPackageManager.Count;
			_testInstantPackageManager.RemoveItem(_testItemKey1);
			_testInstantPackageManager.RemoveItem(_testItemKey1);
			_testInstantPackageManager.RemoveItem(_testItemKey1);
			int afterCount = _testInstantPackageManager.Count;

			//Assert
			Assert.IsFalse(_testInstantPackageManager.Contains(_testItemKey1));
			Assert.That(afterCount, Is.EqualTo(beforeCount - 1));
		}

		[Test]
		public void RemoveItem_RemoveEquivalentButNotSameKey()
		{
			//Arrange
			_testInstantPackageManager.AddItem(_testItemKey1);

			//Act
			int beforeCount = _testInstantPackageManager.Count;
			_testInstantPackageManager.RemoveItem(_testItemKey1Duplicate1);
			int afterCount = _testInstantPackageManager.Count;

			//Assert
			Assert.That(afterCount, Is.EqualTo(beforeCount - 1));
			Assert.IsFalse(_testInstantPackageManager.Contains(_testItemKey1));
		}

		[Test]
		public void RemoveItem_RemoveDoesNotAffectOtherElements()
		{
			//Arrange
			_testInstantPackageManager.AddItem(_testItemKey1);
			_testInstantPackageManager.AddItem(_testItemKey2);
			_testInstantPackageManager.AddItem(_testItemKey3);
			_testInstantPackageManager.AddItem(_testItemKey4);

			//Act
			int beforeCount = _testInstantPackageManager.Count;
			_testInstantPackageManager.RemoveItem(_testItemKey1);
			int afterCount = _testInstantPackageManager.Count;

			//Assert
			Assert.That(afterCount, Is.EqualTo(beforeCount - 1));
			Assert.IsFalse(_testInstantPackageManager.Contains(_testItemKey1));
			Assert.IsTrue(_testInstantPackageManager.Contains(_testItemKey2));
			Assert.IsTrue(_testInstantPackageManager.Contains(_testItemKey3));
			Assert.IsTrue(_testInstantPackageManager.Contains(_testItemKey4));
		}
		#endregion

		#region void Clear();
		[Test]
		public void Clear_OnEmpty()
		{
			//Arrange

			//Act
			_testInstantPackageManager.Clear();
			int afterCount = _testInstantPackageManager.Count;

			//Assert
			Assert.That(afterCount, Is.EqualTo(0));
		}

		[Test]
		public void Clear_OnPopulated()
		{
			//Arrange
			_testInstantPackageManager.AddItem(_testItemKey1);
			_testInstantPackageManager.AddItem(_testItemKey2);
			_testInstantPackageManager.AddItem(_testItemKey3);

			//Act
			int beforeCount = _testInstantPackageManager.Count;
			_testInstantPackageManager.Clear();
			int afterCount = _testInstantPackageManager.Count;

			//Assert
			Assert.That(afterCount, Is.EqualTo(0));
			Assert.That(afterCount, Is.LessThan(beforeCount));
		}

		[Test]
		public void Clear_MultipleCalls()
		{
			//Arrange
			_testInstantPackageManager.AddItem(_testItemKey1);
			_testInstantPackageManager.AddItem(_testItemKey2);
			_testInstantPackageManager.AddItem(_testItemKey3);

			//Act
			int beforeCount = _testInstantPackageManager.Count;
			_testInstantPackageManager.Clear();
			_testInstantPackageManager.Clear();
			_testInstantPackageManager.Clear();
			int afterCount = _testInstantPackageManager.Count;

			//Assert
			Assert.That(afterCount, Is.EqualTo(0));
			Assert.That(afterCount, Is.LessThan(beforeCount));
		}
		#endregion

		#region int Count { get; }
		[Test]
		public void Count_EmptyList()
		{
			//Arrange

			//Act
			int afterCount = _testInstantPackageManager.Count;

			//Assert
			Assert.That(afterCount, Is.EqualTo(0));
		}

		[Test]
		public void Count_NonEmptyList()
		{
			//Arrange
			_testInstantPackageManager.AddItem(_testItemKey1);
			_testInstantPackageManager.AddItem(_testItemKey2);
			_testInstantPackageManager.AddItem(_testItemKey3);

			//Act
			int afterCount = _testInstantPackageManager.Count;

			//Assert
			Assert.That(afterCount, Is.EqualTo(3));
		}

		[Test]
		public void Count_DuplicatesAddedCountOnce()
		{
			//Arrange
			_testInstantPackageManager.AddItem(_testItemKey1);
			_testInstantPackageManager.AddItem(_testItemKey1Duplicate1);
			_testInstantPackageManager.AddItem(_testItemKey1Duplicate2);
			_testInstantPackageManager.AddItem(_testItemKey2);
			_testInstantPackageManager.AddItem(_testItemKey3);

			//Act
			int afterCount = _testInstantPackageManager.Count;

			//Assert
			Assert.That(afterCount, Is.EqualTo(3));
		}

		#endregion

		#region IEnumerator<IItemKey> GetEnumerator();
		[Test]
		public void GetEnumerator_EmptyEnumerator()
		{
			//Arrange

			//Act
			var enumerator = _testInstantPackageManager.GetEnumerator();

			//Assert
			int itemCount = 0;
			while (enumerator.MoveNext())
			{
				itemCount++;
			};
			Assert.That(itemCount, Is.EqualTo(_testInstantPackageManager.Count));
			Assert.That(itemCount, Is.EqualTo(0));
		}

		[Test]
		public void GetEnumerator_PopulatedSourceEnumeratorContainsAllSourceItems()
		{
			//Arrange
			_testInstantPackageManager.AddItem(_testItemKey1);
			_testInstantPackageManager.AddItem(_testItemKey2);
			_testInstantPackageManager.AddItem(_testItemKey3);
			_testInstantPackageManager.AddItem(_testItemKey4);
			bool[] foundItems = new bool[4] { false, false, false, false };

			//Act
			IEnumerator<IItemKey> enumerator = _testInstantPackageManager.GetEnumerator();

			//Assert
			int itemCount = 0;
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == _testItemKey1)
				{
					foundItems[0] = true;
				}
				else if (enumerator.Current == _testItemKey2)
				{
					foundItems[1] = true;
				}
				else if (enumerator.Current == _testItemKey3)
				{
					foundItems[2] = true;
				}
				else if (enumerator.Current == _testItemKey4)
				{
					foundItems[3] = true;
				}

				itemCount++;
			}
			Assert.That(itemCount, Is.EqualTo(_testInstantPackageManager.Count));
			Assert.That(itemCount, Is.EqualTo(4));
			Assert.IsTrue(foundItems[0]);
			Assert.IsTrue(foundItems[1]);
			Assert.IsTrue(foundItems[2]);
			Assert.IsTrue(foundItems[3]);
		}

		#endregion

		#region bool Contains(ItemKey item);
		[Test]
		public void Contains_EmptyListContains()
		{
			//Arrange

			//Act
			bool itemIncluded = _testInstantPackageManager.Contains(_testItemKey1);

			//Assert
			Assert.IsFalse(itemIncluded);
		}

		[Test]
		public void Contains_PopulatedListDoesNotContain()
		{
			//Arrange
			_testInstantPackageManager.AddItem(_testItemKey1);

			//Act
			bool itemIncluded = _testInstantPackageManager.Contains(_testItemKey2);

			//Assert
			Assert.IsFalse(itemIncluded);
		}

		[Test]
		public void Contains_PopulatedListContainsAllItemsAdded()
		{
			//Arrange
			_testInstantPackageManager.AddItem(_testItemKey1);
			_testInstantPackageManager.AddItem(_testItemKey2);

			//Act
			bool item1Included = _testInstantPackageManager.Contains(_testItemKey1);
			bool item2Included = _testInstantPackageManager.Contains(_testItemKey2);

			//Assert
			Assert.IsTrue(item1Included);
			Assert.IsTrue(item2Included);
		}

		[Test]
		public void Contains_ContainsEquivalentButNotSameKey()
		{
			//Arrange
			_testInstantPackageManager.AddItem(_testItemKey1);
			_testInstantPackageManager.AddItem(_testItemKey2);

			//Act
			bool itemIncluded = _testInstantPackageManager.Contains(_testItemKey1Duplicate1);

			//Assert
			Assert.IsTrue(itemIncluded);
		}
		#endregion
	}
}
