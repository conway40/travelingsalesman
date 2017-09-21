using System;
using System.Text;

namespace CodingActivity_TheTravelingSalesperson
{
    /// <summary>
    /// Console class for the MVC pattern
    /// </summary>
    public class ConsoleView
    {
        #region FIELDS

        //
        // declare a Salesperson object for the Controller to use
        // Note: There is no need for a Salesperson property given the Controller already 
        //       has access to the same Salesperson object.
        //
        private Salesperson _salesperson;

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// default constructor to create the console view objects
        /// </summary>
        public ConsoleView(Salesperson salesperson)
        {
            _salesperson = salesperson;

            InitializeConsole();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initialize all console settings
        /// </summary>
        private void InitializeConsole()
        {
            ConsoleUtil.WindowTitle = "The Traveling Salesman";
            ConsoleUtil.HeaderText = "The Traveling Salesman";
        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            ConsoleUtil.DisplayMessage("Press any key to continue.");
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the Exit prompt on a clean screen
        /// </summary>
        public void DisplayExitPrompt()
        {
            ConsoleUtil.DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            ConsoleUtil.DisplayMessage("Thank you for using the application. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }


        /// <summary>
        /// display the welcome screen
        /// </summary>
        public void DisplayWelcomeScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Welcome to The Traveling Salesman game!");
            Console.WriteLine();
            ConsoleUtil.DisplayMessage("Written by Caitlin Conway");
            ConsoleUtil.DisplayMessage("Northwestern Michigan College");
            Console.WriteLine();
            ConsoleUtil.DisplayMessage("You are a traveling salesperson buying and selling widgets around the country.You will be prompted regarding which city you wish to travel to and will then be asked whether you wish to buy or sell widgets.");

            sb.Clear();
            sb.AppendFormat("Your first task will be to set up your account details.");
            ConsoleUtil.DisplayMessage(sb.ToString());

            DisplayContinuePrompt();
        }

        /// <summary>
        /// setup the new salesperson object with the initial data
        /// Note: To maintain the pattern of only the Controller changing the data this method should
        ///       return a Salesperson object with the initial data to the controller. For simplicity in 
        ///       this demo, the ConsoleView object is allowed to access the Salesperson object's properties.
        /// </summary>
        public void DisplaySetupAccount()
        {
            ConsoleUtil.HeaderText = "Account Setup";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Setup your account now.");
            Console.WriteLine();

            ConsoleUtil.DisplayPromptMessage("Enter your first name: ");
            _salesperson.FirstName = Console.ReadLine();
            Console.WriteLine();

            ConsoleUtil.DisplayPromptMessage("Enter your last name: ");
            _salesperson.LastName = Console.ReadLine();
            Console.WriteLine();

            ConsoleUtil.DisplayPromptMessage("Enter your account ID: ");
            _salesperson.AccountID = Console.ReadLine();
            Console.WriteLine();

            ConsoleUtil.DisplayMessage("Widget Types:");
            foreach (WidgetItemStock.WidgetType type in Enum.GetValues(typeof(WidgetItemStock.WidgetType)))
            {
                ConsoleUtil.DisplayMessage(type.ToString());
            }
            Console.WriteLine();
            ConsoleUtil.DisplayPromptMessage("Enter the type of item you would like to sell: ");

            // TODO Validate widget answer

            WidgetItemStock.WidgetType itemType;
            Enum.TryParse<WidgetItemStock.WidgetType>(Console.ReadLine(), out itemType);
            _salesperson.CurrentStock.Type = itemType;
            Console.WriteLine();

            // TODO Validate user answer as int

            ConsoleUtil.DisplayPromptMessage("Enter the number of items you have in stock: ");
            int numberOfItems = int.Parse(Console.ReadLine());
            _salesperson.CurrentStock.AddWidgets(numberOfItems);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display a closing screen when the user quits the application
        /// </summary>
        public void DisplayClosingScreen()
        {
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Thank you for playing The Traveling Salesman!");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// get the menu choice from the user
        /// </summary>
        public MenuOption DisplayGetUserMenuChoice()
        {
            MenuOption userMenuChoice = MenuOption.None;
            bool usingMenu = true;

            //
            // TODO enable each application function separately and test
            //
            while (usingMenu)
            {
                //
                // set up display area
                //
                ConsoleUtil.DisplayReset();
                Console.CursorVisible = false;

                //
                // display the menu
                //
                ConsoleUtil.DisplayMessage("Please type the number of your menu choice.");
                Console.WriteLine();
                Console.WriteLine(
                    "\t" + "1. Travel" + Environment.NewLine +
                    "\t" + "2. Buy" + Environment.NewLine +
                    "\t" + "3. Sell" + Environment.NewLine +
                    "\t" + "4. Display Inventory" + Environment.NewLine +
                    "\t" + "5. Display Cities" + Environment.NewLine +
                    "\t" + "6. Display Account Info" + Environment.NewLine +
                    "\t" + "E. Exit" + Environment.NewLine);

                //
                // get and process the user's response
                // note: ReadKey argument set to "true" disables the echoing of the key press
                //
                ConsoleKeyInfo userResponse = Console.ReadKey(true);
                switch (userResponse.KeyChar)
                {
                    case '1':
                        userMenuChoice = MenuOption.Travel;
                        usingMenu = false;
                        break;
                    case '2':
                        userMenuChoice = MenuOption.Buy;
                        usingMenu = false;
                        break;
                    case '3':
                        userMenuChoice = MenuOption.Sell;
                        usingMenu = false;
                        break;
                    case '4':
                        userMenuChoice = MenuOption.DisplayInventory;
                        usingMenu = false;
                        break;
                    case '5':
                        userMenuChoice = MenuOption.DisplayCities;
                        usingMenu = false;
                        break;
                    case '6':
                        userMenuChoice = MenuOption.DisplayAccountInfo;
                        usingMenu = false;
                        break;
                    case 'E':
                    case 'e':
                        userMenuChoice = MenuOption.Exit;
                        usingMenu = false;
                        break;
                    default:
                        //
                        // TODO handle invalid menu responses from user
                        //
                        break;
                }
            }
            Console.CursorVisible = true;

            return userMenuChoice;
        }
        /// <summary>
        /// get the next city to travel to from the user
        /// </summary>
        /// <returns>string City</returns>
        public string DisplayGetNextCity()
        {
            string nextCity = "";

            ConsoleUtil.HeaderText = "Next City of Travel";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayPromptMessage("Enter the city you wish to travel to: ");
            nextCity = Console.ReadLine();
            
            return nextCity;
        }

        /// <summary>
        /// get the number of widget units to buy from the user
        /// </summary>
        /// <returns>int number of units to buy</returns>
        public int DisplayGetNumberOfUnitsToBuy()
        {
            int numberOfUnitsToAdd = 0;

            ConsoleUtil.HeaderText = "Buy Inventory";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Number of " + _salesperson.CurrentStock.Type + " you have currently: " + _salesperson.CurrentStock.NumberOfUnits);

            ConsoleUtil.DisplayPromptMessage("Please enter the number of widget units you would like to buy: ");

            // TODO Validate user response as int

            numberOfUnitsToAdd = int.Parse(Console.ReadLine());

            DisplayContinuePrompt();

            return numberOfUnitsToAdd;
        }

        /// <summary>
        /// get the number of widget units to sell from the user
        /// </summary>
        /// <returns>int number of units to buy</returns>
        public int DisplayGetNumberOfUnitsToSell()
        {
            int numberOfUnitsToSell = 0;

            ConsoleUtil.HeaderText = "Sell Inventory";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Number of " + _salesperson.CurrentStock.Type + " you have currently: " + _salesperson.CurrentStock.NumberOfUnits);

            // TODO Validate user response as int

            ConsoleUtil.DisplayPromptMessage("Please enter the number of widget units you would like to sell: ");
            numberOfUnitsToSell = int.Parse(Console.ReadLine());

            DisplayContinuePrompt();

            return numberOfUnitsToSell;
        }

        /// <summary>
        /// display the current inventory
        /// </summary>
        public void DisplayInventory()
        {
            ConsoleUtil.HeaderText = "Current Inventory";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Number of " + _salesperson.CurrentStock.Type + ": " + _salesperson.CurrentStock.NumberOfUnits);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display a list of the cities traveled
        /// </summary>
        public void DisplayCitiesTraveled()
        {
            ConsoleUtil.HeaderText = "Cities Traveled To";
            ConsoleUtil.DisplayReset();

            foreach (string city in _salesperson.CitiesVisited)
            {
                ConsoleUtil.DisplayMessage(city);
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the current account information
        /// </summary>
        public void DisplayAccountInfo()
        {
            ConsoleUtil.HeaderText = "Account Info";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("First Name: " + _salesperson.FirstName);
            ConsoleUtil.DisplayMessage("Last Name: " + _salesperson.LastName);
            ConsoleUtil.DisplayMessage("Account ID: " + _salesperson.AccountID);
            ConsoleUtil.DisplayMessage("Type of Item: "  + _salesperson.CurrentStock.Type);
            ConsoleUtil.DisplayMessage("Number of " + _salesperson.CurrentStock.Type + ": " + _salesperson.CurrentStock.NumberOfUnits);

            bool validResponse = false;
            while (!validResponse) {
                ConsoleUtil.DisplayPromptMessage("\nWould you like to update your account info? (Yes/No)");
                string userResponse = Console.ReadLine().ToLower();
                if (userResponse == "yes")
                {
                    validResponse = true;
                    DisplayUpdateAccountInfo();
                    
                }
                else if (userResponse == "no")
                {
                    validResponse = true;
                    DisplayContinuePrompt();
                }
                else
                {
                    ConsoleUtil.DisplayMessage("Please enter a valid answer (yes or no)");
                }
            }
        }

        public void DisplayUpdateAccountInfo()
        {
            ConsoleUtil.HeaderText = "Update Account Info";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Information to update: ");
            ConsoleUtil.DisplayMessage("1. First Name");
            ConsoleUtil.DisplayMessage("2. Last Name");
            ConsoleUtil.DisplayMessage("3. Account ID");
            ConsoleUtil.DisplayMessage("4. Item Type");
            ConsoleUtil.DisplayMessage("5. Number of Items");
            ConsoleUtil.DisplayPromptMessage("\nWhat would you like to update? (1-5) ");
            Console.WriteLine();

            ConsoleKeyInfo userResponse = Console.ReadKey(true);
            switch (userResponse.KeyChar)
            {
                case '1':
                    ConsoleUtil.DisplayPromptMessage("What would you like to change your First Name to?");
                    _salesperson.FirstName = Console.ReadLine();
                    ConsoleUtil.DisplayMessage("Your First Name has been changed to " + _salesperson.FirstName + ".");
                    break;
                case '2':
                    ConsoleUtil.DisplayPromptMessage("What would you like to change your Last Name to?");
                    _salesperson.LastName = Console.ReadLine();
                    ConsoleUtil.DisplayMessage("Your Last Name has been changed to " + _salesperson.LastName + ".");
                    break;
                case '3':
                    ConsoleUtil.DisplayPromptMessage("What would you like to change your Account ID to?");
                    _salesperson.AccountID = Console.ReadLine();
                    ConsoleUtil.DisplayMessage("Your Account ID has been changed to " + _salesperson.AccountID + ".");
                    break;
                case '4':
                    ConsoleUtil.DisplayPromptMessage("What would you like to change your Item Type to?");
                    WidgetItemStock.WidgetType itemType;
                    Enum.TryParse<WidgetItemStock.WidgetType>(Console.ReadLine(), out itemType);
                    _salesperson.CurrentStock.Type = itemType;
                    ConsoleUtil.DisplayMessage("Your Item Type has been changed to " + _salesperson.CurrentStock.Type + ".");
                    break;
                case '5':
                    ConsoleUtil.DisplayPromptMessage("What would you like to change your Number of Items to?");
                    int numberOfItems = int.Parse(Console.ReadLine());
                    _salesperson.CurrentStock.SetNumberOfWidgets(numberOfItems);
                    ConsoleUtil.DisplayMessage("Your Number of Items has been changed to " + _salesperson.CurrentStock.NumberOfUnits + ".");
                    break;
                default:
                    ConsoleUtil.DisplayMessage("You did not enter a valid number.");
                    break;
            }
            DisplayContinuePrompt();

        }

        #endregion
    }
}
