using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using DominoSharp;
using Newtonsoft.Json.Linq;

namespace ExampleDomino
{
    class Program
    {
        /// <summary>
        /// A general testing program for DominoSharp!
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Create an address based on the White House.
            Address address = new Address("1600 Pennsylvania Ave NW, Washington, DC 20500 U.S.");

            // Create a new customer based on the address of the white house
            Customer thePresident = new Customer("The", "President", address, "barack@whitehouse.gov", "2024561111");

            // Get the nearest & open store to the white house
            Store nearestStore = thePresident.getNearestStore();
            // Get the menu of the nearestStore
            Menu menu = nearestStore.getMenu();

            // Get all items in the menu containing "Pizza".
            List<Menu.MenuItem> menuItems = menu.searchInMenu("Pizza");

            // Print out every MenuItem that contains "Pizza" and write its name & price.
            foreach (Menu.MenuItem item in menuItems)
            {
                Console.WriteLine(item.code + " | " + item.name + " | " + item.price);
            }

            // Create a new Order.
            Order order = new Order(nearestStore, thePresident);

            // Add Parmesan Bread Twists & Brownies onto our order.
            order.addItem("B8PCPT", 1);

            // We want 5 brownies because we're very fat
            order.addItem("MARBRWNE", 5);

            // Remove our brownies, sadly...
            order.removeItem("MARBRWNE");

            // Add Diet Coke and Cinnamon Bread Twists
            order.addItems(new string[] { "B8PCCT", "20BDCOKE" }, new int[] { 1, 1 });

            // Remove our Parmesan Bread Twists and Cinnamon Bread Twists.
            order.removeItems(new string[] { "B8PCPT", "B8PCCT" });
            // Now we only have our Diet Coke and Brownie.


            // Now we're gonna add some coupons for the $$$.
            order.addCoupon(new Coupon("3351", 1));
            order.addCoupons(new Coupon[] { new Coupon("5152", 1), new Coupon("8696", 1) });

            // Now we're gonna remove some coupons to spend more $$$, who needs it anyways.
            order.removeCoupon(new Coupon("3351", 1));
            order.removeCoupons(new Coupon[] { new Coupon("5152", 1), new Coupon("8696", 1) });

            // Create a new CreditCard
            Payment.CreditCard card = new Payment.CreditCard("4100123422343234", "0115", "777", "90210");

            // We do this just so we don't end up buying about way too many pizzas in debugging.
            if (false) order.placeOrder(card);
            Console.WriteLine(order.payWith(card));

            // Wait.
            Console.ReadLine();
        }
    }
}
