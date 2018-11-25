using Newtonsoft.Json.Linq;

namespace DominoSharp
{
    /// <summary>
    /// A store based on the StoreID and JObject containg all of the data about the store
    /// </summary>
    public class Store
    {

        #region Constructors
        /// <summary>
        /// Creates a new Store() object
        /// </summary>
        /// <param name="ID">The ID by Dominos for the store</param>
        /// <param name="array">The JObject of data about the store</param>
        /// <param name="c">The country of the story</param>
        public Store(string ID, JObject array, URLs.Country c)
        {
            this.ID = ID;
            this.data = array;
            this.country = c;
        }

        /// <summary>
        /// Creates a new store object, generally assumed to be in USA
        /// </summary>
        /// <param name="ID">The ID of the Store by dominos</param>
        /// <param name="array">The JObject of data about the store</param>
        public Store(string ID, JObject array)
        {
            this.ID = ID;
            this.data = array;
            this.country = URLs.Country.USA;
        }

        /// <summary>
        /// Creates a new store object, generally assumed to be in USA
        /// </summary>
        /// <param name="ID">The ID of the Store by dominos</param>
        /// <param name="array">The JObject of data about the store</param>
        public Store(JValue ID, JObject array)
        {
            this.ID = ID.Value.ToString();
            this.data = array;
            this.country = URLs.Country.USA;
        }

        #endregion

        #region Functions
        /// <summary>
        /// Hopefully places an order from our Store using the Order and CreditCard
        /// </summary>
        /// <param name="order">The order we place</param>
        /// <param name="card">The card to pay with (null if using cash)</param>
        public void placeOrder(Order order, Payment.CreditCard card)
        {
            order.placeOrder();
        }

        /// <summary>
        /// Returns a Menu object for the menu of our current store based on the API.
        /// </summary>
        /// <returns>A Menu object for the menu of our current store based on the API.</returns>
        public Menu getMenu()
        {
            string properURL = URLs.menuURL(country).Replace("{store_id}", ID).Replace("{lang}", "en");
            return new Menu(Utils.request_JSON(properURL), country);
        }
        #endregion

        #region Properties
        /// <summary>
        /// The ID of the store
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// The country of the store
        /// </summary>
        public URLs.Country country { get; set; }
        /// <summary>
        /// The general data about the store
        /// </summary>
        public JObject data { get; set; }
        #endregion

        #region Overrides

        /// <summary>
        /// Returns a custom string implementation of the Store.
        /// </summary>
        /// <returns>A custom string implementation of the Store.</returns>
        public override string ToString()
        {
            return string.Format("Store {0} in {1}", ID, country.ToString());
        }

        #endregion
    }
}
