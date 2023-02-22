using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Inventory
{
    internal interface IInventoryModel
    {
        IReadOnlyList<string> EquippedItems { get; }
        void EquipItem(string itemId);
        void UnequipItem(string itemId);
        bool IsEquipped(string itemId);
    }

    internal class InventoryModel : IInventoryModel //HashSet на заметку
    {
        private readonly List<string> _equippedItems = new();

        public IReadOnlyList<string> EquippedItems => _equippedItems;

        public void EquipItem(string itemId)
        {
            if (!IsEquipped(itemId))
                _equippedItems.Add(itemId);
        }

        public void UnequipItem(string itemId)
        {
            if (IsEquipped(itemId))
                _equippedItems.Remove(itemId);
        }

        public bool IsEquipped(string itemId) =>
            _equippedItems.Contains(itemId);
    }
}