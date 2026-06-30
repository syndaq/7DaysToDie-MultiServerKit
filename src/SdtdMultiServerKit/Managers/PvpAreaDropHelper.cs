namespace SdtdMultiServerKit.Managers
{
    internal static class PvpAreaDropHelper
    {
        public static bool ShouldRunVanillaDrop(string dropMode)
        {
            return dropMode switch
            {
                "none" => false,
                "delete_all" => false,
                _ => true,
            };
        }

        public static void ApplyDropMode(EntityPlayer player, string dropMode)
        {
            switch (dropMode)
            {
                case "delete_all":
                    ClearAllItems(player);
                    break;
                case "toolbelt":
                    ClearBagAndEquipment(player);
                    break;
                case "backpack":
                    ClearToolbelt(player);
                    break;
            }
        }

        public static void ClearAllItems(EntityPlayer player)
        {
            ClearToolbelt(player);
            ClearBag(player);
            ClearEquipment(player);
        }

        private static void ClearToolbelt(EntityPlayer player)
        {
            var inventory = player.inventory;
            if (inventory == null)
            {
                return;
            }

            for (int i = 0; i < inventory.PUBLIC_SLOTS; i++)
            {
                inventory[i] = ItemValue.None.Clone();
            }
        }

        private static void ClearBag(EntityPlayer player)
        {
            player.bag?.Clear();
        }

        private static void ClearBagAndEquipment(EntityPlayer player)
        {
            ClearBag(player);
            ClearEquipment(player);
        }

        private static void ClearEquipment(EntityPlayer player)
        {
            var equipment = player.equipment;
            if (equipment == null)
            {
                return;
            }

            var items = equipment.GetItems();
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = ItemValue.None.Clone();
            }
        }
    }
}
