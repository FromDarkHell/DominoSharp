using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DominoSharp
{
    /// <summary>
    /// A general class to handle our payement information
    /// </summary>
    public class Payment
    {
        #region Classes
        /// <summary>
        /// Our class for CreditCards as the name gives.
        /// </summary>
        public class CreditCard
        {
            #region Enums
            /// <summary>
            /// An enum for all of our possible credit card types.
            /// </summary>
            public enum CreditCardType
            {
                /// <summary>
                /// Visa Card
                /// </summary>
                Visa = 0,
                /// <summary>
                /// Master Card
                /// </summary>
                Mastercard = 1,
                /// <summary>
                /// Amex Card
                /// </summary>
                Amex = 2,
                /// <summary>
                ///  Diners Card
                /// </summary>
                Diners = 3,
                /// <summary>
                /// Discover Card
                /// </summary>
                Discover = 4,
                /// <summary>
                /// JCB Card
                /// </summary>
                JCB = 5,
                /// <summary>
                /// Enroute Card
                /// </summary>
                Enroute = 6,
                /// <summary>
                /// A 'MAX' value to act as a null-like enum.
                /// </summary>
                MAX = 7
            }
            #endregion

            #region Constructors

            /// <summary>
            /// Creates a new CreditCard object based on the information
            /// </summary>
            /// <param name="number">The number of our card</param>
            /// <param name="expiration">The expiration date of our card</param>
            /// <param name="cvv">The CVV</param>
            /// <param name="zip">The ZIP</param>
            public CreditCard(string number, string expiration, string cvv, string zip)
            {
                this.number = number;
                this.cardType = findCreditCardType();
                this.expirationDate = expiration;
                this.cvv = cvv;
                this.zip = zip;

                if (cardType == CreditCardType.MAX || !isValidCard())
                {
                    this.number = null;
                    this.expirationDate = null;
                    this.cvv = null;
                    this.zip = null;
                    throw new Exception("Incorrect Credit Card Type!");
                }
            }
            #endregion

            #region Functions

            /// <summary>
            /// Does fun logic to check if our credit card given is valid or not.
            /// </summary>
            /// <returns></returns>
            public bool isValidCard()
            {
                bool isValid = true;

                isValid = (Regex.IsMatch(cvv, "^[0-9]{3,4}$")) && Regex.IsMatch(zip, "^[0-9]{5}(?:-[0-9]{4})?$");

                return isValid;
            }

            /// <summary>
            /// Uses a series of regexes to figure out what type of CreditCardType our current credit card could be
            /// </summary>
            /// <returns></returns>
            private CreditCardType findCreditCardType()
            {
                Dictionary<CreditCardType, string> regexes = new Dictionary<CreditCardType, string>();
                // Visa Card
                regexes.Add(CreditCardType.Visa, "^4[0-9]{12}(?:[0-9]{3})?$");
                // Master Card
                regexes.Add(CreditCardType.Mastercard, "^5[1-5][0-9]{14}$");
                // Amex Card
                regexes.Add(CreditCardType.Amex, "^3[47][0-9]{13}$");
                // Diners Card
                regexes.Add(CreditCardType.Diners, "^3(?:0[0-5]|[68][0-9])[0-9]{11}$");
                // Discover Card
                regexes.Add(CreditCardType.Discover, "^6(?:011|5[0-9]{2})[0-9]{12}$");
                // JCB Card
                regexes.Add(CreditCardType.JCB, @"^(?:2131|1800|35\d{3})\d{11}$");
                // Enroute Card
                regexes.Add(CreditCardType.Enroute, @"^(?:2014|2149)\d{11}$");


                foreach (string regex in regexes.Values)
                {
                    if (Regex.IsMatch(number, regex))
                    {
                        return regexes.FirstOrDefault(x => x.Value == regex).Key;
                    }
                }

                return CreditCardType.MAX;
            }

            #endregion

            #region Properties
            /// <summary>
            /// The number of our card
            /// </summary>
            public string number { get; set; }

            /// <summary>
            /// The CreditCardType of our current credit card type (MasterCard, etc)
            /// </summary>
            public CreditCardType cardType { get; set; }
            /// <summary>
            /// The expiration date of our credit card
            /// </summary>
            public string expirationDate { get; set; }
            /// <summary>
            /// The CVV of our credit card
            /// </summary>
            public string cvv { get; set; }
            /// <summary>
            /// The ZIP of our credit card.
            /// </summary>
            public string zip { get; set; }
            #endregion
        }
        #endregion
    }
}
