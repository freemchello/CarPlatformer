using System;
using System.Collections.Generic;

internal interface IRepository : IDisposable
{

}
internal abstract class BaseRepository<TKey, TValue, TConfig> : IRepository
{
    private readonly Dictionary<TKey, TValue> _item;

    public IReadOnlyDictionary<TKey, TValue> Items => _item;

    protected BaseRepository(IEnumerable<TConfig> configs) =>
        _item = CreateItems(configs);

    public void Dispose() => _item.Clear();

    private Dictionary<TKey, TValue> CreateItems(IEnumerable<TConfig> configs)
    {
        var items = new Dictionary<TKey, TValue>();

        foreach (TConfig config in configs)
            items[GetKey(config)] = CreateItem(config);

            return items;
        
    }

    protected abstract TValue CreateItem(TConfig config);
    protected abstract TKey GetKey(TConfig config);
}
