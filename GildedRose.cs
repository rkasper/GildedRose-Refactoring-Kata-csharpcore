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

        IList<Item> Items;

        public GildedRose(IList<Item> items)
        {
            this.Items = items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                switch (item.Name)
                {
                    case AgedBrie:
                        HandleAgedBrie(item);
                        break;
                    case BackstagePasses:
                        HandleBackstagePasses(item);
                        break;
                    case Sulfuras:
                        HandleSulfuras();
                        break;
                    default:
                        HandleNormalItem(item);
                        break;
                }
            }
        }

        private static void HandleNormalItem(Item item)
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

        private static void HandleSulfuras()
        {
            // Do nothing
        }

        private static void HandleBackstagePasses(Item item)
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

        private static void HandleAgedBrie(Item item)
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

        private static void EnsureMaxQuality(Item item)
        {
            if (item.Quality > MaxQuality)
            {
                item.Quality = MaxQuality;
            }
        }
    }
}
