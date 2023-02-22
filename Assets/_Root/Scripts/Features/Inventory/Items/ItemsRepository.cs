using System;
using System.Collections.Generic;

namespace Features.Inventory.Items
{
    internal interface IKItemRepository
    {
        IReadOnlyDictionary<String, IItem> Items {get;}
    }
    internal class ItemsRepository : BaseRepository<string, IItem, ItemConfig>
    {
        public ItemsRepository(IEnumerable<ItemConfig> configs) : base(configs)
        { }
        protected override string GetKey(ItemConfig config) => config.Id;
        protected override IItem CreateItem(ItemConfig config) =>
            new Item
            (
                config.Id,
                new ItemInfo
                (
                    config.Title,
                    config.Icon
                )
            );


    }
}