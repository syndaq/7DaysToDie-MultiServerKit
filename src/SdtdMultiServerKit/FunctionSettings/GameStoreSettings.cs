namespace SdtdMultiServerKit.FunctionSettings
{
    public class GameStoreSettings : SettingsBase
    {
        /// <summary>
        /// Goodslistcommand
        /// </summary>
        public string QueryListCmd { get; set; }

        /// <summary>
        /// Commandbefore
        /// </summary>
        public string BuyCmdPrefix { get; set; }

        /// <summary>
        /// Goodsitem
        /// </summary>
        public string GoodsItemTip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BuySuccessTip { get; set; }

        /// <summary>
        /// Pointsnot
        /// </summary>
        public string PointsNotEnoughTip { get; set; }

        /// <summary>
        /// HasGoods
        /// </summary>
        public string NoGoods { get; set; }
    }
}