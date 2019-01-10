using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DominoSharp
{
    /// <summary>
    /// The core interface to our payment API.
    /// </summary>
    public class Order
    {
        #region Enums
        /// <summary>
        /// The two delivery methods for an Order: Carryout and Delivery.
        /// </summary>
        public enum deliveryMethod
        {
            /// <summary>
            /// The Enum for Deliver
            /// </summary>
            Delivery = 0,
            /// <summary>
            /// The enum for carryout
            /// </summary>
            Carryout = 1
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new Order object based on the Store, Customer, and Country
        /// </summary>
        /// <param name="store">The store this order takes place at</param>
        /// <param name="customer">The customer placing the order</param>
        /// <param name="country">The country this order is taking place in</param>
        public Order(Store store, Customer customer)
        {
            this.store = store;
            this.menu = store.getMenu();
            this.customer = customer;
            this.address = customer.address;


            data = new JObject {
                { "Address", new JObject {
                    { "Street", address.Street},{"City", address.City},
                    { "Region", address.Region }, { "PostalCode", address.ZIP },
                    { "Type", "House" }
                } },

                { "Coupons", new JArray{} }, {"CustomerID", "" }, {"Extension", "" },
                {"OrderChannel","OLO" }, {"OrderID", ""}, { "NoCombine", true },
                {"OrderMethod", "Web"}, {"OrderTaker", JValue.CreateNull() }, {"Payments", new JArray{ } },
                { "Products", new JArray{ } }, {"Market", ""},{"Currency", "" },
                {"ServiceMethod", "Delivery"}, {"Tags", new JObject{ }  }, {"Version", "1.0"},
                {"SourceOrganizationURI", "order.dominos.com"}, {"LanguageCode", "en"},
                {"Partners", new JObject{ } }, {"NewUser", true}, {"metaData",  new JObject{ } },
                {"Amounts",  new JObject{ } }, {"BusinessDate", "" }, {"EstimatedWaitMinutes","" },
                {"PriceOrderTime", ""}, {"AmountsBreakdown",  new JObject{ } },
            };
        }
        #endregion

        #region Functions
        #region Item / Coupon Handling

        #region Add Item
        /// <summary>
        /// Adds an item to the products to order given an Item Code and Quantity
        /// </summary>
        /// <param name="itemCode">The ItemCode to order </param>
        /// <param name="quantity">The quantity of the Item we're ordering</param>
        public void addItem(string itemCode, int quantity)
        {
            JToken item = menu.variants[itemCode];
            item["ID"] = 1;
            item["isNew"] = true;
            item["qty"] = quantity;
            item["AutoRemove"] = false;
            JArray products = JArray.Parse(data.GetValue("Products").ToString());
            products.AddFirst(item);
            data["Products"] = products;
        }

        /// <summary>
        /// Does the same function as addItem() but takes arrays
        /// The 'quantity' property can be null if you want to avoid having a lengthy array of integers, the default value will be one.
        /// </summary>
        /// <param name="itemCodes">An array of the item codes we're adding</param>
        /// <param name="quantity">An array of the quantity of the items we're ordering</param>
        public void addItems(string[] itemCodes, int[] quantity)
        {
            JArray products = JArray.Parse(data.GetValue("Products").ToString());
            for (int i = 0; i < itemCodes.Length; i++)
            {
                JToken item = menu.variants[itemCodes[i]];
                item["ID"] = 1;
                item["isNew"] = true;
                item["qty"] = quantity[i];
                item["AutoRemove"] = false;
                products.AddFirst(item);
            }
            data["Products"] = products;
        }
        #endregion

        #region Remove Item
        /// <summary>
        /// Removes an item from the products to order given an item code.
        /// </summary>
        /// <param name="itemCode">The item code to remove.</param>
        public void removeItem(string itemCode)
        {
            JArray products = JArray.Parse(data.GetValue("Products").ToString());
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i]["Code"].ToString() == itemCode)
                {
                    products.RemoveAt(i);
                }
            }
            data["Products"] = products;
        }

        /// <summary>
        /// The same function as removeItem() but given a string[].
        /// </summary>
        /// <param name="itemCodes"></param>
        public void removeItems(string[] itemCodes)
        {
            JArray products = JArray.Parse(data.GetValue("Products").ToString());
            foreach (string itemCode in itemCodes)
            {
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i]["Code"].ToString() == itemCode)
                    {
                        products.RemoveAt(i);
                    }
                }
            }
            data["Products"] = products;
        }
        #endregion

        #region Add Coupon
        /// <summary>
        /// Adds a coupon to the current order
        /// </summary>
        /// <param name="coupon">The coupon to add</param>
        public void addCoupon(Coupon coupon)
        {
            JToken token = menu.coupons[coupon.code];
            token["ID"] = 1;
            token["isNew"] = true;
            token["qty"] = coupon.quantity;
            token["AutoRemove"] = false;

            JArray coupons = JArray.Parse(data.GetValue("Coupons").ToString());
            coupons.AddFirst(token);
            data["Coupons"] = coupons;
        }

        /// <summary>
        /// Adds a List of Coupons onto the order
        /// </summary>
        /// <param name="coupons">The list of coupons to add</param>
        public void addCoupons(Coupon[] coupons)
        {
            JArray originalCoupons = JArray.Parse(data.GetValue("Coupons").ToString());
            for (int i = 0; i < coupons.Length; i++)
            {
                JToken coupon = menu.coupons[coupons[i].code];
                coupon["ID"] = 1;
                coupon["isNew"] = true;
                coupon["qty"] = coupons[i].quantity;
                coupon["AutoRemove"] = false;
                originalCoupons.AddFirst(coupon);
            }
            data["Coupons"] = originalCoupons;
        }
        #endregion

        #region Remove Coupon

        /// <summary>
        /// Removes the coupon from the Order.
        /// </summary>
        /// <param name="coupon">The coupon to remove</param>
        public void removeCoupon(Coupon coupon)
        {
            JArray coupons = JArray.Parse(data.GetValue("Coupons").ToString());
            for (int i = 0; i < coupons.Count; i++)
            {
                if (coupons[i]["Code"].ToString() == coupon.code)
                {
                    coupons.RemoveAt(i);
                }
            }
            data["Coupons"] = coupons;
        }

        /// <summary>
        /// Removes all coupons from the coupon[] from the Order
        /// </summary>
        /// <param name="coupons">The coupons to remove</param>
        public void removeCoupons(Coupon[] coupons)
        {
            JArray originalCoupons = JArray.Parse(data.GetValue("Coupons").ToString());
            foreach (Coupon c in coupons)
            {
                for (int i = 0; i < originalCoupons.Count; i++)
                {
                    if (originalCoupons[i]["Code"].ToString() == c.code)
                    {
                        originalCoupons.RemoveAt(i);
                    }
                }
            }
            data["Coupons"] = originalCoupons;
        }

        #endregion

        /// <summary>
        /// Returns the items currently ordered in this Order
        /// </summary>
        /// <returns>The items currently ordered in this Order</returns>
        public JArray itemsCurrentlyOrdered()
        {
            return JArray.Parse(data.GetValue("Products").ToString());
        }

        /// <summary>
        /// Returns the coupons currently applied to this order
        /// </summary>
        /// <returns>The coupons currently applied to this order</returns>
        public JArray couponsCurrentlyApplied()
        {
            return JArray.Parse(data.GetValue("Coupons").ToString());
        }

        #endregion

        /// <summary>
        /// POST data onto a given URL
        /// </summary>
        /// <param name="url">The URL to POST to</param>
        /// <param name="merge">If we want to merge data</param>
        private JObject send(string url, bool merge)
        {
            // Update all of our data.
            data["StoreID"] = store.ID;
            data["Email"] = customer.email;
            data["FirstName"] = customer.firstName;
            data["LastName"] = customer.lastName;
            data["Phone"] = customer.phoneNumber;

            JObject jsonToReturn = null;

            #region HTTP Handling
            HttpResponseMessage response;

            using (HttpClient client = new HttpClient())
            {
                JObject jObject = new JObject
                {
                    {"Order", new JObject
                        {

                        }
                    }
                };
                jObject["Order"] = data;

                string replacedData = Regex.Replace(jObject.ToString(), "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");
                var httpContent = new StringContent(replacedData, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add("Referer", "https://order.dominos.com/en/pages/order/");
                response = client.PostAsync(url, httpContent).Result;

                if (!response.IsSuccessStatusCode) return new JObject { { "Status", "-1" } };
            }
            #endregion

            jsonToReturn = JObject.FromObject(JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result));

            return jsonToReturn;

        }

        /// <summary>
        /// Checks the validation of an order
        /// </summary>
        /// <returns>If the order is valid</returns>
        public bool validateOrder()
        {
            JObject s = send(URLs.placeURL(store.country), false);
            return s["Status"].ToString() != "-1";
        }

        /// <summary>
        /// This *hopefully* places an Order to Dominos.
        /// Not really sure if this works, not really going to pay. 
        /// This requires testing.
        /// </summary>
        /// <param name="creditCard">The credit card one is paying with. null if paying in cash.</param>
        public void placeOrder(Payment.CreditCard creditCard)
        {
            if (creditCard.cardType == Payment.CreditCard.CreditCardType.MAX)
            {
                throw new Exception("Credit Card is not a valid type!");
            }
            if (creditCard == null) payWith();
            else payWith(creditCard);
            send(URLs.placeURL(store.country), false);
        }

        #region Pay With
        /// <summary>
        /// Returns the price of the current Order combined. 
        /// Use this instead of place() when testing
        /// </summary>
        public JObject payWith()
        {

            // Get the price to check that everything worked okay
            JObject response = send(URLs.priceURL(store.country), true);
            // Throw an exception if we messed up.
            if (response["Status"].ToString() == "-1")
                throw new Exception(string.Format("Get Price Failed (Dominos Returned -1 Response): {0}", response));

            data["Payments"] = new JArray {
                    new JObject
                    {
                        {"Type","Cash"}
                    }
            };
            return response;
        }

        /// <summary>
        /// Returns the price of the current Order combined. 
        /// Use this instead of place() when testing.
        /// </summary>
        /// <param name="card">The Payment.CreditCard object to pay with.</param>
        public JObject payWith(Payment.CreditCard card)
        {
            // Get the price to check that everything worked okay
            JObject response = send(URLs.priceURL(store.country), true);

            // Throw an exception if we messed up.
            if (response["Status"].ToString() == "-1")
                throw new Exception(string.Format("Get Price Failed (Dominos Returned -1 Response): {0}", response));

            data["Payments"] = new JArray
            {
                new JObject
                {
                    {"Type", "CreditCard"},
                    {"Expiration",  card.expirationDate},
                    {"Amount", 0 },
                    {"CardType", card.cardType.ToString().ToUpper() },
                    {"Number", long.Parse(card.number) },
                    {"SecurityCode", long.Parse(card.cvv) },
                    {"PostalCode", long.Parse(card.zip) }
                }
            };

            return response;
        }

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// The store of our order
        /// </summary>
        public Store store { get; }
        /// <summary>
        /// The customer buying the order
        /// </summary>
        public Customer customer { get; }
        /// <summary>
        /// The address of the customer.
        /// </summary>
        public Address address { get; }
        /// <summary>
        /// The data of our order.
        /// </summary>
        public JObject data { get; }
        /// <summary>
        /// The Menu associated with the store / their order
        /// </summary>
        public Menu menu { get; }
        #endregion

        #region Overrides
        /// <summary>
        /// A string representation of the Order
        /// </summary>
        /// <returns>A string representation of the Order</returns>
        public override string ToString()
        {
            return string.Format("An order for {0} {1} with {2} items in it",
                customer.firstName,
                customer.lastName,
                data["Products"].ToString().Count(f => f == '\n'));
        }
        #endregion
    }
}
