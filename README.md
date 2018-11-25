### DominoSharp

Description
-----------
This is a C# wrapper for the Dominos Pizza API.

It's a port of:<br/>
	- The [PizzaAPI nodeJS Module](https://github.com/RIAEvangelist/node-dominos-pizza-api) written by [RIAEvangelist](https://github.com/RIAEvangelist)<br/>
	- The [PizzaAPI Python wrapper](https://github.com/Magicjarvis/pizzapi/) written by [Magicjarvis](https://github.com/Magicjarvis)<br/>


Quick Start
-----------
First create a `Customer` object and set the customer's address:
```C#
Address address = new Address("1600 Pennsylvania Ave NW, Washington, DC 20500 U.S.");
Customer thePresident = new Customer("The", "President", address, "barack@whitehouse.gov", "2024561111");
```
Then, find a `Store` object that will deliver to the customer's address.
```C#
Store nearestStore = thePresident.getNearestStore();
```
For you to be able to order any items you'll need the item's product codes.<br/> 
To find the codes, get the menu from the store, and search for the items to add. Kindly ask the `Store` object for its `Menu`.
```C#
Menu menu = nearestStore.getMenu();
```
Then to search the `menu` for an item do:
```C#
// Get all items in the menu containing "Pizza".
List<Menu.MenuItem> menuItems = menu.searchInMenu("Pizza");
```
Once you've gathered all the `MenuItem`'s you want. Time to start ordering them bucko!<br/>  
The URLs.Country enum is to change the URL we gather data from based on the country (USA or Canada currently).
```C#
Order order = new Order(nearestStore, thePresident, URLs.Country.USA); // Create a new Order.
order.addItem("B8PCPT", 1); // Add 1 Parmesan Bread Twist onto our order.
order.addItem("MARBRWNE", 5); // We want 5 brownies because we're very fat
```
You can even remove items! Just perfect for when you mess up and realize you're too fat for those 5 brownies from earlier...
```C#
order.removeItem("MARBRWNE"); // Remove our brownies, sadly...
```
Next up is actually paying for your one parmesan bread twist or whatever.<br/> 
You need to create a new CreditCard object to pay with.
```C#
Payment.CreditCard card = new Payment.CreditCard("4100123422343234", "0115", "777", "90210");
```
Then you get to place an order and __**hopefully__** if my code is to be believed, pay for it:
```C#
order.placeOrder(card);
```