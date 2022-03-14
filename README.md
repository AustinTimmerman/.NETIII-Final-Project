# .NETII-FinalProject
A Magic the Gathering project using a MSSQL database and .NET framework

**11/8/21**
Finished the user login and signup. On login, the user's email is passed through checking for their roles and updating the Mainwindow accordingly. On signup, a new user
is placed in the DB and then logged in using those credentials. 

**11/14/21**
Finished the fake card accessor and manager along with the tests associated with them. Currently, the user does not have a way to view the cards 
on separate "pages" unless they change the code in the MainWindow. Will be the next update along with the actual CardAccessor being finished.

**11/21/21**
Finished the retrieve_cards_by_page. Able to retrieve cards from the database (currently not enough cards to select by page and the window does not currently support several pages yet). The next update will be to be able to change which cards are retrieved by which page is currently selected.

**11/26/21**
Cleaned up the retrieve_cards_by_page and am able to separate the search results by page. The application is now able to limit the cards shown by page number and will show up to 20 cards. The next update will show all the decks on the Decks tab.

**11/27/21**
Completed the deck and deck cards retrieval. The user is now able to retrieve all decks (that are *public*) and the cards related to those decks. The next update will show all matches in the matches tab.

**12/1/21**
Completed the match, match decks, and match deck cards retrieval. The user is now able to retrieve all matches (that are *public*) and the decks and cards related. The next step is to begin the user specific retrievals so the user is able to view their specific cards, decks, and matches.

**12/2/21**
Began working on the user retrieval (cards, decks, matches), but ran into problems with the UI schema. Will need to change from tabs into buttons and reorganize the retrieval process. The next update will be changing over to buttons instead of tabs and having the user be able to retrieve all their cards, decks, and matches. 

**12/2/21**
Made changes to the way the *My Stuff* items are displayed and fixed the problem occuring with them. Next update will be having the user be able to retrieve all their cards, decks, and matches (along with the cards and decks associating each respected deck or match).

**12/4/21**
Completed the user retrieval (cards, decks, matches and everything related to the matches and decks retrieved). The user is now able to view all of their owned cars / wishlisted cards, decks, and matches. The next update will be cleanup or being able to view a card in a detail window that allows the user to add the card to owned or wishlisted.

**12/4/21**
Completed the specific card retrieval information, and finally converted the card retrieval into a user enabled mode. The user is now able to view a specific card in a detailed window with the card information in a much more user friendly mode. Along with that, the user is now able to view which cards they have owned or wishlisted among all the cards in the database. Every card instance now follows this model. The next update will allow the user to add cards to their owned or wishlisted (update, insert, or delete a card).

**12/8/21**
Began work on update and insert user cards. Next update will have the fully complete user card update and insert.

**12/8/21**
Completed insert, update, and delete user card. The user is now able to fully insert a new card into their user cards (*owned or wishlisted*), update a card from their user cards (*changing a card from owned / wishlisted*), and remove a card from their use cards. The next update will involve image retrieval and displayal.

**12/9/21**
Completed image retrieval. The user is now able to view the image associated with the card when selected. The next update will involve work with the CRUD functions with the decks and having the interface refresh when changes are made to reflect them on the screen. 

**12/11/21**
Completed deck insert, update, and delete. Completed a design overhaul compeletely changing the design to be better looking. Major changes and tweaks all around the application were done and added CardCount to deck cards to count the amount of a singular card appears in a deck (still need to work out). The user is now able to view their decks and add a new deck, update the deck name and if its public, and delete a deck. The next update will include the remaining deck card CRUD functions with some hopeful match CRUD functions as well. 

**12/12/21**
Completed deck card insert. Various bug fixed and changes done. The user is now able to add a card to a deck they own with the amount specified. The next update will include updating and deleting card from decks. 

**12/13/21**
Completed remaining deck card crud functions. Made changes to support the data grid refreshing whenever the user does any sort of CRUD function to display the changes made. Moved some of the buttons and other small details around to better suit the overall design. The user is able to fully add cards with their amounts to their decks, update the card amount in a deck, and delete a card from a deck. Also, fixed the issue whenever the user deleted a deck with cards in it that it wouldn't let them. The next update will be the final leg of completing all the remaining CRUD functions (match insert, match update, match delete, match deck insert, match deck update, match deck delete). 

**12/13/21**
Completed all match CRUD functions. The user is now able to create, update, and delete any of their decks. The next update will include the final match deck CRUD functions with any other small tweaks needed. 

**12/14/21**
Completed the last remaining match deck CRUD functions needed. Completed other minor fixes and tweaks all around like labeling and proper match deck retrievals. The user is now able to insert decks into matches and remove decks from matches (no update since the only thing being changed would be winner). The application is now complete and is fully operational. Any other updates will be small tweaks or bug fixes.

**03/08/22**
Created the MVCPresentation project and implemented all identity frameworks.