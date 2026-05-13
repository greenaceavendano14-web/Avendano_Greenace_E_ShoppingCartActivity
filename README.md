Avendano, Greenace E.
BSIT 1-2

Parts Where I Used AI:
I used AI mainly to help me understand how to build the shopping cart system step by step. 
It helped me with the flowchart, organizing my program logic, and making sure my validation (like checking inputs and stock) works correctly. 
I also used it to understand how to update items in the cart instead of adding duplicates.

Why I Used AI:
I used AI because some parts of the program were a bit confusing at first, especially the logic behind validating inputs and handling the cart. 
AI helped me see the process more clearly and made it easier for me to follow the requirements. 
It also helped me make my flowchart simple and easy to understand.

Prompts / Questions I Asked:
How do I create a shopping cart system in C# using arrays?
How do I update an existing item in the cart instead of adding a duplicate?
How do I deduct stock after adding an item to the cart?

Changes / Improvements I Made:
After using AI, I didn’t just copy everything. 
I made sure to understand the code and adjusted it to match my own style. 
I simplified the flowchart, fixed some logic parts, and made sure everything follows the project requirements. 
I also reviewed the code so I can explain how it works if needed.



Part 2 Features
1.) Cart Management Menu
Users can manage their cart before checkout through a dedicated menu:
View Cart — display all items currently in the cart
Remove an Item — remove a specific item and restore its stock
Update Item Quantity — change the quantity with stock validation
Clear Cart — remove all items and restore all stock
Checkout — proceed to payment
2.) Product Search
Users can search for products by partial name match. The search is case-insensitive so typing 'key' will find 'Keyboard'.
3.) Product Categories
A Category field was added to the Product class. Products are now grouped into Electronics, School, Clothing, and Hardware. Users can filter the product list by choosing a category from the menu.
4.) Checkout Payment Validation
User is prompted to enter a payment amount
Must be numeric and greater than or equal to the final total
Re-prompted on invalid or insufficient input
Change is computed and displayed on the receipt
5.0 Receipt with Number and Date/Time
Each checkout generates a receipt showing:
Receipt number — auto-incremented, zero-padded (e.g., 0001, 0002)
Checkout date and time in Philippine Time (UTC+8)
All purchased items with quantities and subtotals
•Grand total, discount if applicable, final total, payment, and change
6.0 Low Stock Alert
After every checkout, any product with RemainingStock of 5 or less is flagged with a warning message so the user knows what needs restocking.
7.0 Order History
Completed transactions are stored in the OrderHistory class during the program run. Users can view all past orders from the main menu, with each entry showing the receipt number, date/time, and final total.
8.) Strict Y/N Validation
All Yes/No prompts use a YesNo() helper method that keeps re-asking until the user types exactly Y or N. Any other input shows an error message and prompts again.

AI Usage
Parts where I used AI:
I used AI mainly to help restructure my program from a single loop into a cleaner design using classes and methods. I first asked how to build a cart management menu with options like view, remove, update quantity, and clear the cart. AI explained how each option should be its own method and be called using a switch inside a loop. I followed that idea but still wrote the methods myself to match my existing logic. I also asked how to remove static keywords, and learned to create a Shop class to hold data like products, cart, and history as instance fields, with all methods inside it. Then the Program class just creates one Shop object and runs it, which made the structure more organized and easier to manage.

The trickiest part was handling stock when removing or updating items. AI explained that when removing an item, the quantity should be returned to RemainingStock before deleting it from the cart. For updates, it suggested restoring the stock first, then checking if the new quantity is valid before applying it. That explanation helped me write the RemoveItem() and UpdateQty() methods properly. I also used AI to format the receipt, like generating a 4-digit receipt number with leading zeros and displaying the current date and time in a readable format. Lastly, I fixed a compile error where I accidentally named a method the same as the class (Shop), which I learned isn’t allowed in C#, so I renamed it to AddToCart().

Why I Used AI:
I used AI because the program needed a much more structured approach than what I was used to, especially when managing the cart across different methods, correctly restoring stock during remove and update actions, and reorganizing everything without using static. These were new concepts for me, so I needed guidance not just on what to do, but why it works that way. AI helped break down the logic behind each step, which made it easier for me to understand and apply it on my own instead of just copying code. I mainly used it to clarify ideas, fix errors, and make sure my solution was clean, organized, and working properly.

Prompts / Questions I Asked:
How do I implement a cart history feature in C# to track all purchased and removed items?
How do I properly remove an item from the cart while restoring its quantity back to stock in C#?
How do I track item quantity changes when items are bought and returned in a shopping cart system?
How do I record every transaction (add, remove, update) in a cart history list in C#?
How do I format a receipt number as 4 digits with leading zeros and display the current date and time in C#?

Changes / Improvements I Made:
Instead of copying AI outputs, I used them as a guide and made sure I understood each suggestion before rewriting it in my own way. I organized the CartItem and Cart classes so that all cart-related operations like AddItem, RemoveItem, UpdateItem, and GetGrandTotal are handled inside the Cart class, which kept the Shop class cleaner and easier to manage. I also chose to store order history using three parallel arrays in the OrderHistory class, since it matched the array-based approach used in the rest of the program.

I also made several adjustments on my own. I encountered a compile error with void Shop() and realized it conflicted with the class name, so I renamed it to AddToCart() after understanding the issue. I carefully reviewed the stock restoration logic in UpdateQty and mentally tested different scenarios to ensure restoring stock before validation wouldn’t cause incorrect values. Overall, I used AI to support my understanding, but the final structure and decisions were based on my own reasoning.
