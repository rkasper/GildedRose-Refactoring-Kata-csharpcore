using System.Collections.Generic;

namespace GildedRose_Refactoring_Kata_csharpcore
{
    public class GildedRose
    {
        private const string AgedBrie = "Aged Brie";
        private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        private const int MinQuality = 0;
        private const int MaxQuality = 50;
        private const int ConcertDateIsClose = 11;
        private const int ConcertDateIsReallyClose = 6;

        readonly IList<Item> _items;

        public GildedRose(IList<Item> items)
        {
            this._items = items;
        }

        public void UpdateQuality()
        {
            foreach (var item in _items)
            {
                HandleIfAgedBrie(item); 
                HandleIfBackstagePasses(item);
                HandleIfSulfuras(item);
                HandleIfNormalItem(item);
            }
        }

        private static void HandleIfNormalItem(Item item)
        {
            if (IsNormalItem(item))
            {

                if (item.SellIn <= 0)
                {
                    item.Quality -= 2;
                }
                else
                {
                    item.Quality--;
                }

                item.SellIn--;
            }
        }

        private static bool IsNormalItem(Item item)
        {
            return AgedBrie != item.Name && BackstagePasses != item.Name && Sulfuras != item.Name;
        }

        private static void HandleIfSulfuras(Item item)
        {
            if (Sulfuras == item.Name)
            {
                // Do nothing
            }
        }

        private static void HandleIfBackstagePasses(Item item)
        {
            if (BackstagePasses == item.Name)
            {
                if (item.SellIn <= 0)
                {
                    item.Quality = MinQuality;
                }
                else if (item.SellIn < ConcertDateIsReallyClose)
                {
                    item.Quality += 3;
                }
                else if (item.SellIn < ConcertDateIsClose)
                {
                    item.Quality += 2;
                }
                else
                {
                    item.Quality++;
                }

                EnsureMaxQuality(item);

                item.SellIn--;
            }
        }

        private static void HandleIfAgedBrie(Item item)
        {
            if (AgedBrie == item.Name)
            {
                if (item.SellIn < 0)
                {
                    item.Quality += 2;
                }
                else
                {
                    item.Quality++;
                }

                EnsureMaxQuality(item);

                item.SellIn--;
            }
        }

        private static void EnsureMaxQuality(Item item)
        {
            if (item.Quality > MaxQuality)
            {
                item.Quality = MaxQuality;
            }
        }
    }
}
