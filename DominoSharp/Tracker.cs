using System;
using Newtonsoft.Json.Linq;

namespace DominoSharp
{
    /// <summary>
    /// A general class that keeps / gets the tracking data.
    /// </summary>
    public class Tracker
    {

        #region Classes
        /// <summary>
        /// A helper class that can be used to store all of your tracking data.
        /// </summary>
        public class OrderTrackingInfo
        {
            #region Constructors
            /// <summary>
            /// A helper class that can be used to store all of your tracking data.
            /// </summary>
            /// <param name="dataToBase">A JObject representation of our tracking data</param>
            public OrderTrackingInfo(JObject dataToBase)
            {
                JToken token = dataToBase.GetValue("GetTrackerDataResponse");

                // Register our version for whatever reason.
                version = token["Version"].ToString();

                // Register our asOf Date
                asOf = token["AsOf"].ToString();


                JToken orderStatus = token["OrderStatuses"]["OrderStatus"];

                // Setup our as of time
                asOfTime = orderStatus["AsOfTime"].ToString();

                // Setup our storeID
                storeId = orderStatus["StoreID"].ToString();

                // Our Order ID next.
                orderID = orderStatus["OrderID"].ToString();

                // Our phone number next!
                phoneNumber = orderStatus["Phone"].ToString();

                // Our method of service, Carry-Out or Delivery (its Digiorno)
                serviceMethod = orderStatus["ServiceMethod"].ToString() == "Carry-Out" ? Order.deliveryMethod.Carryout : Order.deliveryMethod.Delivery;

                AdvancedOrderTime = orderStatus["AdvancedOrderTime"].ToString();

                OrderDescription = orderStatus["OrderDescription"].ToString();

                OrderTakeCompleteTime = orderStatus["OrderTakeCompleteTime"].ToString();

                OrderStatus = orderStatus["OrderStatus"].ToString();

                makeTimeSecs = Int64.Parse(orderStatus["MakeTimeSecs"].ToString());

                ovenTimeSecs = Int64.Parse(orderStatus["OvenTimeSecs"].ToString());

                rackTimeSecs = Int64.Parse(orderStatus["RackTimeSecs"].ToString());

                // Driving Information
                routeTime = orderStatus["RouteTime"].ToString();
                driverID = orderStatus["DriverID"].ToString();
                driverName = orderStatus["DriverName"].ToString();
                orderDeliveryTime = orderStatus["DeliveryTime"].ToString();
                managerID = orderStatus["ManagerID"].ToString();
                managerName = orderStatus["ManagerName"].ToString();
            }
            #endregion

            #region Properties
            #region Query Information
            /// <summary>
            /// The date the baseData was obtained / queried for.
            /// </summary>
            public string asOf { get; set; }

            #endregion
            #region Order Information

            #region General Ordering Information
            /// <summary>
            ///  I'm not too sure what this is used for.
            /// </summary>
            public string version { get; set; }

            /// <summary>
            /// This is from the most recent update of the API I think.
            /// </summary>
            public string asOfTime { get; set; }

            /// <summary>
            /// Just our Store ID
            /// </summary>
            public string storeId { get; set; }

            /// <summary>
            /// Our Order ID (not too sure how useful this is to begin with)
            /// </summary>
            public string orderID { get; set; }

            /// <summary>
            /// The phone number associated
            /// </summary>
            public string phoneNumber { get; set; }

            /// <summary>
            /// The service method associated
            /// </summary>
            public Order.deliveryMethod serviceMethod { get; set; }

            /// <summary>
            /// Advanced Order Time (not too sure why this exists, its null in my examples)
            /// </summary>
            public string AdvancedOrderTime { get; set; }

            /// <summary>
            /// The description of our order like: 1 Large (14") Hand Tossed Pizza w/ Pepperoni, Salami, Italian Sausage
            /// </summary>
            public string OrderDescription { get; set; }

            /// <summary>
            /// Complete Time to take the order
            /// </summary>
            public string OrderTakeCompleteTime { get; set; }

            /// <summary>
            /// Order Status
            /// </summary>
            public string OrderStatus { get; set; }

            /// <summary>
            ///  The time to make the pizza dough maybe?
            /// </summary>
            public long makeTimeSecs { get; set; }

            /// <summary>
            /// The time it sits in the oven
            /// </summary>
            public long ovenTimeSecs { get; set; }

            /// <summary>
            /// Not too sure why this one exists.
            /// </summary>
            public long rackTimeSecs { get; set; }


            #endregion

            #region Driving Information 
            /// <summary>
            /// Presumably the time it takes for the entire route
            /// </summary>
            public string routeTime { get; set; }

            /// <summary>
            /// The ID of the Driver
            /// </summary>

            public string driverID { get; set; }

            /// <summary>
            /// The name of the driver
            /// </summary>
            public string driverName { get; set; }

            /// <summary>
            /// The delivery time of the order in secs
            /// </summary>
            public string orderDeliveryTime { get; set; }

            /// <summary>
            /// A general date for the time it was delivered
            /// </summary>
            public string deliveryTime { get; set; }


            /// <summary>
            /// The order key for this specific order
            /// </summary>
            public string orderKey { get; set; }


            /// <summary>
            /// The ID of the manager
            /// </summary>
            public string managerID { get; set; }

            /// <summary>
            /// The name of the manager
            /// </summary>
            public string managerName { get; set; }
            #endregion

            #endregion
            #endregion
        }

        #endregion

        #region Functions
        /// <summary>
        /// A function that returns a JObject based on the storeID, and orderKey
        /// </summary>
        /// <param name="storeID">The store ID of our Store</param>
        /// <param name="orderKey">The order key of our Order</param>
        /// <param name="c">The country our store is in for the API</param>
        /// <returns>A JObject representation of the XML returned by the tracker.</returns>
        public static JObject trackByOrder(string storeID, string orderKey, URLs.Country c)
        {
            return Utils.request_XML(
                URLs.trackOrder(c)
                    .Replace("{store_id}", storeID)
                    .Replace("{order_key}", orderKey));
        }

        /// <summary>
        /// A function that returns a JObject based on the phone number returned by the API as XML.
        /// </summary>
        /// <param name="phoneNumber">The phone number linked to the order</param>
        /// <param name="c">The country associated with the order</param>
        /// <returns>A JObject representation of the XML returned by the tracker.</returns>
        public static JObject trackByPhone(string phoneNumber, URLs.Country c)
        {
            return Utils.request_XML(URLs.trackPhone(c).Replace("{phone}", phoneNumber));
        }
        #endregion
    }
}
