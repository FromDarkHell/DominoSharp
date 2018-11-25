namespace DominoSharp
{
    /// <summary>
    /// A class for all of our customers who buy things.
    /// </summary>
    public class Customer
    {
        #region Constructors
        /// <summary>
        /// Creates a new customer based on the params given
        /// </summary>
        /// <param name="first">The first name of the customer</param>
        /// <param name="last">The last name of the customer</param>
        /// <param name="address">An Address object of the address of the customer</param>
        /// <param name="email">The email of the customer</param>
        /// <param name="phone">The phone number of the customer</param>
        public Customer(string first, string last, Address address, string email, string phone)
        {
            this.firstName = first;
            this.lastName = last;
            this.address = address;
            this.email = email;
            this.phoneNumber = phone;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Gets the nearest store to the customer
        /// </summary>
        /// <returns>The nearest store to the customer</returns>
        public Store getNearestStore()
        {
            return address.getNearestStore();
        }
        #endregion

        #region Properties
        /// <summary>
        /// The first name of our customer
        /// </summary>
        public string firstName { get; set; }
        /// <summary>
        /// The last name of our customer
        /// </summary>
        public string lastName { get; set; }
        /// <summary>
        /// The Address of our customer
        /// </summary>
        public Address address { get; set; }
        /// <summary>
        /// The email of our customer
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// The phone number of our customer
        /// </summary>
        public string phoneNumber { get; set; }
        #endregion

        #region Overrides
        /// <summary>
        /// Returns a string representation of our customer
        /// </summary>
        /// <returns>A string representation of our customer</returns>
        public override string ToString()
        {
            return string.Format("Name: {0} {1} | Email: {2} | Address: {3}",
                firstName,
                lastName,
                email,
                phoneNumber,
                address);
        }
        #endregion
    }
}
