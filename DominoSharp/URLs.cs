namespace DominoSharp
{
    /// <summary>
    /// A class for handling our URLs / HTTP stuff and associated country
    /// </summary>
    public class URLs
    {
        #region Enums
        /// <summary>
        /// An enum for all of our countries, generally for handling URL data, like ".com" vs ".ca"
        /// </summary>
        public enum Country
        {
            /// <summary>
            /// USA
            /// </summary>
            USA = 0,
            /// <summary>
            /// Canada
            /// </summary>
            CANADA = 1
        }
        #endregion

        #region Functions
        #region General URLs

        /// <summary>
        /// Returns the URL to locate nearby stores
        /// </summary>
        /// <param name="c">The country to search for</param>
        /// <returns>The URL to locate nearby stores</returns>
        public static string findURL(Country c)
        {
            return basePrefix(c) + "store-locator?s={street}&c={city}&type={type}";
        }

        /// <summary>
        /// Returns the URL for the info of a store
        /// </summary>
        /// <param name="c">The country to search in</param>
        /// <returns>the URL for the info of a store</returns>
        public static string infoURL(Country c)
        {
            return basePrefix(c) + "store/{store_id}/profile";
        }

        /// <summary>
        /// Returns the URL for the menu of a store.
        /// </summary>
        /// <param name="c">The country to search in</param>
        /// <returns>the URL for the menu of a store.</returns>
        public static string menuURL(Country c)
        {
            return basePrefix(c) + "store/{store_id}/menu?lang={lang}&structured=true";
        }
        /// <summary>
        /// Returns the URL to place an order
        /// </summary>
        /// <param name="c">The country to search in</param>
        /// <returns>the URL to place an order</returns>
        public static string placeURL(Country c)
        {
            return basePrefix(c) + "/place-order";
        }

        /// <summary>
        /// Returns the URL for pricing an order
        /// </summary>
        /// <param name="c">The country to search in</param>
        /// <returns>the URL for pricing an order</returns>
        public static string priceURL(Country c)
        {
            return basePrefix(c) + "price-order";
        }

        /// <summary>
        /// Returns the URL for validating an order
        /// </summary>
        /// <param name="c">The country to search in</param>
        /// <returns>the URL for validating an order</returns>
        public static string validateURL(Country c)
        {
            return basePrefix(c) + "validate-order";
        }

        /// <summary>
        /// Returns the URL for the coupons
        /// </summary>
        /// <param name="c">The country to search in</param>
        /// <returns>the URL for the coupons</returns>
        public static string couponURL(Country c)
        {
            return basePrefix(c) + "store/{store_id}/coupon/{couponid}?lang={lang}";
        }

        #endregion

        #region Tracking URLs
        /// <summary>
        /// The URL for tracking an Order
        /// </summary>
        /// <param name="c">The country to search in</param>
        /// <returns></returns>
        public static string trackOrder(Country c)
        {
            return trackPrefix(c) + "StoreID={store_id}&OrderKey={order_key}";
        }

        /// <summary>
        /// The URL for tracking an Order based on a phone number.
        /// </summary>
        /// <param name="c">The country to search in</param>
        /// <returns></returns>
        public static string trackPhone(Country c)
        {
            return trackPrefix(c) + "Phone={phone}";
        }

        #endregion

        #region URL Prefixes

        private static string basePrefix(Country c)
        {
            return "https://order.dominos." + (c == Country.USA ? "com" : "ca") + "/power/";
        }

        private static string trackPrefix(Country c)
        {
            return "https://trkweb.dominos." + (c == Country.USA ? "com" : "ca") + "/orderstorage/GetTrackerData?";
        }
        #endregion
        #endregion
    }
}
