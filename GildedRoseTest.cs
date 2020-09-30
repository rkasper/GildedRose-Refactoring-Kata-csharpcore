using NUnit.Framework;
using System.Collections.Generic;

namespace GildedRose_Refactoring_Kata_csharpcore
{
    [TestFixture]
    public class GildedRoseTest
    {
        private static void UpdateQualityAndCheckItem(GildedRose app, Item item, int expectedSellIn, int expectedQuality)
        {
            app.UpdateQuality();
            Assert.AreEqual(expectedSellIn, item.SellIn);
            Assert.AreEqual(expectedQuality, item.Quality);
        }

        [Test]
        public void TestFrameworkWorksProperly()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual("foo", Items[0].Name);
        }

        /**
         * 	- All items have a SellIn value which denotes the number of days we have to sell the item
	     *  - All items have a Quality value which denotes how valuable the item is
	     *  - At the end of each day our system lowers both values for every item
         */
        [Test]
        public void NormalItemSellInAndQualityDecreaseByOne()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Normal Item", SellIn = 25, Quality = 15 } };
            GildedRose app = new GildedRose(Items);
            UpdateQualityAndCheckItem(app, Items[0], 24, 14);
            UpdateQualityAndCheckItem(app, Items[0], 23, 13);
        }

        /**
         * 	- Once the sell by date has passed, Quality degrades twice as fast
         */
        [Test]
        public void NormalItemQualityDegradesTwiceAsFast()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Normal Item", SellIn = 1, Quality = 15 } };
            GildedRose app = new GildedRose(Items);
            UpdateQualityAndCheckItem(app, Items[0], 0, 14);
            UpdateQualityAndCheckItem(app, Items[0], -1, 12);
            UpdateQualityAndCheckItem(app, Items[0], -2, 10);
        }

        /**
         * - "Aged Brie" actually increases in Quality the older it gets
         */
        [Test]
        public void AgedBrieIncreasesInQuality()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 25, Quality = 15 } };
            GildedRose app = new GildedRose(Items);
            UpdateQualityAndCheckItem(app, Items[0], 24, 16);
            UpdateQualityAndCheckItem(app, Items[0], 23, 17);
        }

        /**
         * - The Quality of an item is never more than 50
         */
        [Test]
        public void QualityNeverMoreThan50()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 25, Quality = 50 } };
            GildedRose app = new GildedRose(Items);
            UpdateQualityAndCheckItem(app, Items[0], 24, 50);

            Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = -1, Quality = 48 } };
            app = new GildedRose(Items);
            UpdateQualityAndCheckItem(app, Items[0], -2, 50);
        }

        /**
         * 	- "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
         */
        [Test]
        public void SulfurasIsSpecial()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 25, Quality = 15 } };
            GildedRose app = new GildedRose(Items);
            UpdateQualityAndCheckItem(app, Items[0], 25, 15);
        }

        /**
         * - "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
	     * Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
	     * Quality drops to 0 after the concert
         */
        [Test]
        public void BacstagePassesAreSpecial()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 15 } };
            GildedRose app = new GildedRose(Items);
            UpdateQualityAndCheckItem(app, Items[0], 10, 16);
            UpdateQualityAndCheckItem(app, Items[0], 9, 18);
            UpdateQualityAndCheckItem(app, Items[0], 8, 20);
            UpdateQualityAndCheckItem(app, Items[0], 7, 22);
            UpdateQualityAndCheckItem(app, Items[0], 6, 24);
            UpdateQualityAndCheckItem(app, Items[0], 5, 26);
            UpdateQualityAndCheckItem(app, Items[0], 4, 29);
            UpdateQualityAndCheckItem(app, Items[0], 3, 32);
            UpdateQualityAndCheckItem(app, Items[0], 2, 35);
            UpdateQualityAndCheckItem(app, Items[0], 1, 38);
            UpdateQualityAndCheckItem(app, Items[0], 0, 41);
            UpdateQualityAndCheckItem(app, Items[0], -1, 0);
        }
    }
}