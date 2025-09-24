// 代码生成时间: 2025-09-24 11:38:21
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem
# 改进用户体验
{
    // Represents an item in the inventory.
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        // Constructor.
        public InventoryItem(int id, string name, int quantity)
# 扩展功能模块
        {
            Id = id;
            Name = name;
            Quantity = quantity;
        }
    }

    // Manages the inventory items.
    public class InventoryManager
# 扩展功能模块
    {
        private readonly Dictionary<int, InventoryItem> inventoryItems = new Dictionary<int, InventoryItem>();

        // Adds an item to the inventory.
# NOTE: 重要实现细节
        public void AddItem(int id, string name, int quantity)
        {
# 增强安全性
            if (inventoryItems.ContainsKey(id))
            {
                throw new InvalidOperationException($"An item with ID {id} already exists in the inventory.");
            }

            inventoryItems.Add(id, new InventoryItem(id, name, quantity));
        }

        // Removes an item from the inventory.
        public bool RemoveItem(int id)
        {
            if (!inventoryItems.ContainsKey(id))
# TODO: 优化性能
            {
# 增强安全性
                throw new InvalidOperationException($"No item with ID {id} found in the inventory.");
            }
# FIXME: 处理边界情况

            return inventoryItems.Remove(id);
        }

        // Updates the quantity of an existing item in the inventory.
        public void UpdateQuantity(int id, int quantity)
        {
            if (!inventoryItems.ContainsKey(id))
            {
                throw new InvalidOperationException($"No item with ID {id} found in the inventory.");
# 改进用户体验
            }

            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), $"Quantity cannot be negative.");
            }

            inventoryItems[id].Quantity = quantity;
        }
# 添加错误处理

        // Returns the current inventory as a list of items.
        public List<InventoryItem> GetInventoryList()
        {
            return new List<InventoryItem>(inventoryItems.Values);
        }
    }

    // The program class that runs the inventory management system.
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                InventoryManager manager = new InventoryManager();

                // Add items to the inventory.
                manager.AddItem(1, "Laptop", 5);
# 改进用户体验
                manager.AddItem(2, "Smartphone", 10);

                // Update the quantity of an item.
# NOTE: 重要实现细节
                manager.UpdateQuantity(1, 3);

                // Remove an item from the inventory.
                bool removed = manager.RemoveItem(2);

                // Display the current inventory.
                List<InventoryItem> inventoryList = manager.GetInventoryList();
                foreach (InventoryItem item in inventoryList)
                {
                    Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}