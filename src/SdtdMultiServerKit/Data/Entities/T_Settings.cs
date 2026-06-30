using IceCoffee.SimpleCRUD.OptionalAttributes;

namespace SdtdMultiServerKit.Data.Entities
{
    public class T_Settings
    {
        [PrimaryKey]
        public string Name { get; set; }

        public string SerializedValue { get; set; }
    }
}