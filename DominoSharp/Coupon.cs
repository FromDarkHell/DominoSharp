namespace DominoSharp
{
    /// <summary>
    /// This class is just generally loose class for coupons, no important logic
    /// </summary>
    public class Coupon
    {
        #region Constructors
        /// <summary>
        /// Creates a new loose Coupon object for payment based on the code and quantity param
        /// </summary>
        /// <param name="code">The code of the coupon</param>
        /// <param name="quantity">The quantity of the coupon(s)</param>
        public Coupon(string code, int quantity)
        {
            this.code = code;
            this.quantity = quantity;
            this.ID = "1";
            this.isNew = true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The code of the coupon
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// The quantity of the coupon
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// If the coupon is a new, unused coupon.
        /// </summary>
        public bool isNew { get; }
        /// <summary>
        /// The ID of our coupon.
        /// </summary>
        public string ID { get; }
        #endregion

        #region Overrides

        /// <summary>
        /// An override of ToString()
        /// </summary>
        /// <returns>A string representation of the coupon like: "Coupon 3412 x1"</returns>
        public override string ToString()
        {
            return string.Format("Coupon {0} x{1}", code, quantity);
        }

        #endregion
    }
}
