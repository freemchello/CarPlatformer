using Features.Inventory.Items;
using JetBrains.Annotations;
using System;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Inventory
{
    internal class InventoryController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("");

        private readonly InventoryView _view;
        private readonly IInventoryModel _model;
        private readonly ItemsRepository _repository;

        public InventoryController/*(Transform placeForUi, IInventoryModel inventoryModel)*/
            ([NotNull] Transform placeForUi,
            [NotNull] IInventoryModel inventoryModel)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _model = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));

            _repository = CreateRepository();
            _view = LoadView(placeForUi);

            _view.Display(_repository.Items.Values, OnItemClicked);

            foreach (string itemId in _model.EquippedItems)
                _view.Select(itemId);
        }

        private ItemsRepository CreateRepository()
        {
            ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(_dataSourcePath);
            var repository = new ItemsRepository(itemConfigs);
            AddRepository(repository);

            return repository;
        }

        private InventoryView LoadView( Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }

        private void OnItemClicked(string itemId)
        {
            bool IsEquipped = _model.IsEquipped(itemId);

            if (IsEquipped)
                UnequipItem(itemId);
            else
                EquipItem(itemId);
        }

        private void UnequipItem(string itemId)
        {
            _view.Unselect(itemId);
            _model.UnequipItem(itemId);
        }

        private void EquipItem(string itemId)
        {
            _view.Select(itemId);
            _model.EquipItem(itemId);
        }
    }
}