using FinchAPI;
using System;

namespace FinchTalentShow
{
    class Program
    {
        #region Enums for Menu's

        public enum Menu : byte
        {
            ConnectFinch = 1,
            TalentShow,
            DataRecorder,
            AlarmSystem,
            UserProgramming,
            DisconnectFinch,
            Quit
        }

        public enum TalentShowMenu : byte
        {
            LED = 1,
            Buzzer,
            Wheels,
            Back
        }

        public enum LEDMenu : byte
        {
            On = 1,
            Blink,
            MultipleLED,
            Pulse,
            Back
        }

        public enum BuzzerMenu : byte
        {
            On = 1,
            StarWars,
            HappyBirthday,
            Back
        }

        public enum WheelsMenu : byte
        {
            Forward = 1,
            Backward,
            Right,
            Left,
            Back
        }

        public enum DataRedcorderMenu : byte
        {
            Light = 1,
            Temp,
            Accel,
            Back
        }

        #endregion

        #region Main method of the program

        static Finch myFinch = new Finch();

        /// <summary>
        /// Main Method of the application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DisplayWelcomeScreen();
            while (true)
            {
                DisplayConsoleUI("Finch Control v1.0");
                getMenuOption(DisplayMainMenu());
            }
        }

        #endregion

        #region Generate UI

        /// <summary>
        /// Generate the main console for the UI
        /// </summary>
        private static void DisplayConsoleUI(string programName)
        {
            // Setup UI for application
            Console.Clear();
            for (int i = 0; i < 120; i++)
            {
                Console.SetCursorPosition(0 + i, 0);
                Console.Write("-");
            }
            for (int i = 0; i < 120; i++)
            {
                Console.SetCursorPosition(0 + i, 4);
                Console.Write("-");
            }
            for (int i = 0; i < 120; i++)
            {
                Console.SetCursorPosition(0 + i, 28);
                Console.Write("-");
            }
            Console.SetCursorPosition(1, 5);

            DisplayProgramName(programName);
        }

        /// <summary>
        /// Display program name to UI
        /// </summary>
        /// <param name="programName">string - The name of the program</param>
        private static void DisplayProgramName(string programName)
        {
            // Setup program name and colors
            Console.SetCursorPosition(45, 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{programName}");
            Console.ResetColor();
            Console.SetCursorPosition(1, 5);
        }

        /// <summary>
        /// Displays the Closing Screen
        /// </summary>
        private static void DisplayClosingScreen()
        {
            DisplayConsoleUI("Goodbye");
            Console.WriteLine("So our time has come to an end. I enjoyed working with your Finch today!");
            Console.WriteLine();
            Console.SetCursorPosition(1, 7);
            Console.WriteLine("Thank you for using this program. Until next time!");
            DisplayContinuePrompt();
            quit();
        }

        #endregion

        #region Menu's of Application

        /// <summary>
        /// Generate the main menu for the console
        /// </summary>
        private static int DisplayMainMenu()
        {
            int userResponse = isValidMenuOption("Finch Control v1.0", @"
             Menu

  1. Connect Finch Robot
  
  2. Talent Show
  
  3. Data Recorder

  4. Alarm System (Under Development)

  5. User Programming (Under Development)

  6. Disconnect Finch Robot

  7. Quit Application


  



  Option (1-7): ", 1, 7);
            return userResponse;
        }
            
        /// <summary>
        /// Performs an action based on the menu option selected
        /// </summary>
        /// <param name="option">Menu option chosen</param>
        private static void getMenuOption(int option)
        {
            Menu menuChoice = (Menu)option;

            switch (menuChoice)
            {
                case Menu.ConnectFinch:
                    DisplayConnectFinch();
                    break;
                case Menu.TalentShow:
                    getTalentShowMenuOption(DisplayTalentShowMenu());
                    break;
                case Menu.DataRecorder:
                    DisplayConsoleUI("Data Recorder");
                    getDataRecorderMenuOption(DisplayDataRecorderMenu());
                    break;
                case Menu.AlarmSystem:
                    DisplayConsoleUI("Under Development");
                    Console.WriteLine("This module is under development");
                    DisplayContinuePrompt();
                    break;
                case Menu.UserProgramming:
                    DisplayConsoleUI("Under Development");
                    Console.WriteLine("This module is under development");
                    DisplayContinuePrompt();
                    break;
                case Menu.DisconnectFinch:
                    DisplayDisconnectFinch();
                    break;
                case Menu.Quit:
                    DisplayClosingScreen();
                    break;
                default:
                    DisplayConsoleUI("Invlaid Response");
                    Console.WriteLine("Sorry I don't understand your response, please try again.");
                    DisplayContinuePrompt();
                    break;

            }
        }

        /// <summary>
        /// Displays the Talent Show Menu
        /// </summary>
        /// <returns>Returns an int of the menu option chosen</returns>
        private static int DisplayTalentShowMenu()
        {
            int userResponse = isValidMenuOption("Talent Show Control Menu", @"
             Talent Show Menu

  1. LED Control

  2. Buzzer Control

  3. Wheels Menu

  4. Back












  Option (1-4): ", 1, 4);
            return userResponse;
        }

        /// <summary>
        /// Performs an action based on the selected Menu item
        /// </summary>
        /// <param name="option">Menu option chosen</param>
        private static void getTalentShowMenuOption(int option)
        {
            TalentShowMenu menuOption = (TalentShowMenu)option;

            switch (menuOption)
            {
                case TalentShowMenu.LED:
                    getLEDMenuOption(DisplayLEDMenu());
                    break;
                case TalentShowMenu.Buzzer:
                    getBuzzerMenuOption(DisplayBuzzerMenu());
                    break;
                case TalentShowMenu.Wheels:
                    getWheelMenuOption(DisplayWheelsMenu());
                    break;
                case TalentShowMenu.Back:
                    break;
                default:
                    DisplayConsoleUI("Invlaid Response");
                    Console.WriteLine("Sorry I don't understand your response, please try again.");
                    DisplayContinuePrompt();
                    break;
            }
        }

        /// <summary>
        /// Displays the Data Recording Menu
        /// </summary>
        /// <returns>Returns user option for menu</returns>
        private static int DisplayDataRecorderMenu()
        {
            int userResponse = isValidMenuOption("Data Recorder Control Menu", @"
             Data Recorder Menu
  
  1. Light Sensor Control

  2. Temp Sensor Control

  3. Accelerometer Menu Control

  4. Back












  Option (1-4): ", 1, 4);
            return userResponse;
        }

        /// <summary>
        /// Gets the Menu option for the Data Recorder Menu
        /// </summary>
        /// <param name="option">User selected option for the menu</param>
        private static void getDataRecorderMenuOption(int option)
        {
            DataRedcorderMenu menuOption = (DataRedcorderMenu)option;

            switch (menuOption)
            {
                case DataRedcorderMenu.Light:
                    break;
                case DataRedcorderMenu.Temp:
                    break;
                case DataRedcorderMenu.Accel:
                    break;
                case DataRedcorderMenu.Back:
                    break;
                default:
                    DisplayConsoleUI("Invlaid Response");
                    Console.WriteLine("Sorry I don't understand your response, please try again.");
                    DisplayContinuePrompt();
                    break;
            }
        }

        /// <summary>
        /// Displays LED Menu
        /// </summary>
        /// <returns>Menu Option Chosen</returns>
        private static int DisplayLEDMenu()
        {
            int userResponse = isValidMenuOption("LED Control Menu", @"
             LED Menu

  1. LED On

  2. LED Blink

  3. Multiple LED On

  4. LED Pulse

  5. Back










  Option (1-5): ", 1, 5);
            return userResponse;
        }

        /// <summary>
        /// Performs an action based on the selected Menu item
        /// </summary>
        /// <param name="option">Menu Option chosen</param>
        private static void getLEDMenuOption(int option)
        {
            LEDMenu menuChoice = (LEDMenu)option;

            switch (menuChoice)
            {
                case LEDMenu.On:
                    DisplayConnectFinch();
                    DisplayConsoleUI("LED On");
                    setLEDOn();
                    setLEDOff();
                    break;
                case LEDMenu.Blink:
                    DisplayConnectFinch();
                    DisplayConsoleUI("LED Blink");
                    LEDBlink();
                    break;
                case LEDMenu.MultipleLED:
                    DisplayConnectFinch();
                    DisplayConsoleUI("Multiple LED Show");
                    ShowMultipleLED();
                    break;
                case LEDMenu.Pulse:
                    DisplayConnectFinch();
                    DisplayConsoleUI("LED Pulse");
                    LEDPulse();
                    break;
                case LEDMenu.Back:
                    DisplayTalentShowMenu();
                    break;
                default:
                    DisplayConsoleUI("Invlaid Response");
                    Console.WriteLine("Sorry I don't understand your response, please try again.");
                    DisplayContinuePrompt();
                    break;
            }
        }

        /// <summary>
        /// Displays Buzzer Menu
        /// </summary>
        /// <returns>Returns Menu Option selected</returns>
        private static int DisplayBuzzerMenu()
        {
            int userResponse = isValidMenuOption("Buzzer Control Menu", @"
             Buzzer Menu

  1. Buzzer On

  2. Buzzer Star Wars

  3. Buzzer Happy Birthday

  4. Back












  Option (1-4): ", 1, 4);
            return userResponse;
        }

        /// <summary>
        /// Performs an action based on the selected Menu item
        /// </summary>
        /// <param name="option">Menu Option chosen</param>
        private static void getBuzzerMenuOption(int option)
        {
            BuzzerMenu menuChoice = (BuzzerMenu)option;

            switch (menuChoice)
            {
                case BuzzerMenu.On:
                    DisplayConnectFinch();
                    setBuzzerOn();
                    break;
                case BuzzerMenu.StarWars:
                    DisplayConnectFinch();
                    buzzerStarWars();
                    break;
                case BuzzerMenu.HappyBirthday:
                    DisplayConnectFinch();
                    buzzerHappyBirthday();
                    break;
                case BuzzerMenu.Back:
                    DisplayTalentShowMenu();
                    break;
                default:
                    DisplayConsoleUI("Invlaid Response");
                    Console.WriteLine("Sorry I don't understand your response, please try again.");
                    DisplayContinuePrompt();
                    break;
            }
        }

        /// <summary>
        /// Displays Wheels Menu
        /// </summary>
        /// <returns>Returns menu option selected</returns>
        private static int DisplayWheelsMenu()
        {
            int userResponse = isValidMenuOption("Wheels Control Menu", @"            
             Wheels Menu

  1.Forward

  2.Backward

  3.Right(90 degrees)

  4.Left(90 degrees)

  5.Back










  Option (1-5): ", 1, 5);
            return userResponse;
        }

        /// <summary>
        /// Performs an action based on the selected Menu Item
        /// </summary>
        /// <param name="option">Menu option chosen</param>
        private static void getWheelMenuOption(int option)
        {
            WheelsMenu menuOption = (WheelsMenu)option;

            switch (menuOption)
            {
                case WheelsMenu.Forward:
                    DisplayConnectFinch();
                    setWheelsForward();
                    break;
                case WheelsMenu.Backward:
                    DisplayConnectFinch();
                    setWheelsBackwards();
                    break;
                case WheelsMenu.Right:
                    DisplayConnectFinch();
                    setWheelsRight();
                    break;
                case WheelsMenu.Left:
                    DisplayConnectFinch();
                    setWheelsLeft();
                    break;
                case WheelsMenu.Back:
                    DisplayTalentShowMenu();
                    break;
                default:
                    DisplayConsoleUI("Invlaid Response");
                    Console.WriteLine("Sorry I don't understand your response, please try again.");
                    DisplayContinuePrompt();
                    break;
            }
        }

        #endregion

        #region LED Control
        /// <summary>
        /// Get's the LED parameters and passes it to the setLEDOn Method
        /// </summary>
        private static (int r, int g, int b, int time) getLEDParams()
        {
            int r = 0;
            int g = 0;
            int b = 0;
            int time = 0;

            DisplayConsoleUI("Set LED Parameters");
            r = isValidInt("Enter value for Red LED (0-255): ", 0, 255);
            g = isValidInt("Enter Value for Green LED (0-255): ", 0, 255);
            b = isValidInt("Enter Value for Blue LED (0-255): ", 0, 255);
            time = isValidInt("Enter Value for Time (ms): ", 0, 100000);
            return (r, g, b, time);
        }

        /// <summary>
        /// Prompts the user for the LED Parameters and time to turn the LED on
        /// Turns the LED on for the specified color and time set by the user
        /// </summary>
        private static void setLEDOn()
        {
            var values = getLEDParams();

            myFinch.setLED(values.r, values.g, values.b);
            myFinch.wait(values.time);
        }

        /// <summary>
        /// Sets the LED to On with manually passed parameters
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="time"></param>
        private static void setLEDOn(int r = 255, int g = 255, int b = 255, int time = 1000)
        {
            myFinch.setLED(r, g, b);
            myFinch.wait(time);

        }

        /// <summary>
        /// Turns the LED off
        /// </summary>
        /// <param name="time">Specifies the wait time for the LED to be off. Used for blinking the LED</param>
        private static void setLEDOff(int time = 500)
        {
            myFinch.setLED(0, 0, 0);
            myFinch.wait(time);
        }

        /// <summary>
        /// Blinks the LED based on user input parameters
        /// </summary>
        private static void LEDBlink()
        {
            var values = getLEDParams();

            Console.Write("Enter the number of blinks, 0 is infinite: ");
            int.TryParse(Console.ReadLine(), out int numberOfBlinks);
            if (numberOfBlinks == 0)
            {
                while (true)
                {
                    setLEDOn(values.r, values.g, values.b, values.time);
                    setLEDOff();
                }
            }
            else
            {
                for (int i = 0; i < numberOfBlinks; i++)
                {
                    setLEDOn(values.r, values.g, values.b, values.time);
                    setLEDOff();
                }
            }
        }

        /// <summary>
        /// Shows multiple LED colors at varying times. Hard Coded values
        /// </summary>
        private static void ShowMultipleLED()
        {

            setLEDOn(255, 255, 255, 1000);
            setLEDOff(500);
            setLEDOn(255, 0, 0, 500);
            setLEDOff(500);
            setLEDOn(0, 255, 0, 2000);
            setLEDOff(500);
            setLEDOn(0, 0, 255, 1500);
            setLEDOff(500);
        }

        /// <summary>
        /// Breathes the LED from white to Red to Green to Blue
        /// </summary>
        private static void LEDPulse()
        {
            for (int i = 0; i < 255; i++)
            {
                setLEDOn(i, 0, 0, 1);
            }
            for (int i = 255; i >= 0; i--)
            {
                setLEDOn(i, 0, 0, 1);
            }

            for (int i = 0; i < 255; i++)
            {
                setLEDOn(0, i, 0, 1);
            }
            for (int i = 255; i >= 0; i--)
            {
                setLEDOn(0, i, 0, 1);
            }

            for (int i = 0; i < 255; i++)
            {
                setLEDOn(0, 0, i, 1);
            }
            for (int i = 255; i >= 0; i--)
            {
                setLEDOn(0, 0, i, 1);
            }
        }

        #endregion

        #region Buzzer Control

        /// <summary>
        /// Gets Buzzer Parameters
        /// </summary>
        /// <returns>Returns Tuple of parameters</returns>
        private static (int hertz, int time) getBuzzerParams()
        {
            int hertz = 0;
            int time = 0;
            DisplayConsoleUI("Get Buzzer Parameters");
            hertz = isValidInt("Please enter the Frequency of the Buzzer (hertz): ", 0, 22000);
            time = isValidInt("Please enter the length of time for the Buzzer to be on (ms): ", 0, 100000);
            return (hertz, time);
        }

        /// <summary>
        /// Sets the buzzer to on based on the user specified parameters
        /// </summary>
        private static void setBuzzerOn()
        {
            var values = getBuzzerParams();

            myFinch.noteOn(values.hertz);
            myFinch.wait(values.time);
            myFinch.noteOff();
        }

        /// <summary>
        /// Sets the buzzer to on based on hardcoded parameters
        /// </summary>
        /// <param name="hertz">Tone generated in hertz</param>
        /// <param name="time">Time to play tone</param>
        private static void setBuzzerOn(int hertz, int time)
        {
            myFinch.noteOn(hertz);
            myFinch.wait(time);
            myFinch.noteOff();
        }

        /// <summary>
        /// Plays Star Wars Song
        /// </summary>
        private static void buzzerStarWars()
        {
            setBuzzerOn(300, 500);
            myFinch.wait(50);
            setBuzzerOn(300, 500);
            myFinch.wait(50);
            setBuzzerOn(300, 500);
            myFinch.wait(50);
            setBuzzerOn(250, 500);
            myFinch.wait(50);
            setBuzzerOn(350, 250);
            setBuzzerOn(300, 500);
            myFinch.wait(50);
            setBuzzerOn(250, 500);
            myFinch.wait(50);
            setBuzzerOn(350, 250);
            setBuzzerOn(300, 500);
            myFinch.wait(50);
        }

        /// <summary>
        /// Plays Happy Birthday
        /// </summary>
        private static void buzzerHappyBirthday()
        {
            setBuzzerOn(264, 125);
            myFinch.wait(250);
            setBuzzerOn(264, 125);
            myFinch.wait(125);
            setBuzzerOn(297, 500);
            myFinch.wait(125);
            setBuzzerOn(264, 500);
            myFinch.wait(125);
            setBuzzerOn(352, 500);
            myFinch.wait(125);
            setBuzzerOn(330, 1000);
            myFinch.wait(250);
            setBuzzerOn(264, 125);
            myFinch.wait(250);
            setBuzzerOn(264, 125);
            myFinch.wait(125);
            setBuzzerOn(297, 500);
            myFinch.wait(125);
            setBuzzerOn(264, 500);
            myFinch.wait(125);
            setBuzzerOn(396, 500);
            myFinch.wait(125);
            setBuzzerOn(352, 1000);
            myFinch.wait(250);
            setBuzzerOn(264, 125);
            myFinch.wait(250);
            setBuzzerOn(264, 125);
            myFinch.wait(125);
            setBuzzerOn(2642, 500);
            myFinch.wait(125);
            setBuzzerOn(440, 500);
            myFinch.wait(125);
            setBuzzerOn(352, 250);
            myFinch.wait(125);
            setBuzzerOn(352, 125);
            myFinch.wait(125);
            setBuzzerOn(330, 500);
            myFinch.wait(125);
            setBuzzerOn(297, 1000);
            myFinch.wait(250);
            setBuzzerOn(466, 125);
            myFinch.wait(250);
            setBuzzerOn(466, 125);
            myFinch.wait(125);
            setBuzzerOn(440, 500);
            myFinch.wait(125);
            setBuzzerOn(352, 500);
            myFinch.wait(125);
            setBuzzerOn(396, 500);
            myFinch.wait(125);
            setBuzzerOn(352, 1000);
        }

        #endregion

        #region Wheels Control

        /// <summary>
        /// Gets Wheel Parameters
        /// </summary>
        /// <returns>Returns a tuple for wheel parameters</returns>
        private static (int left, int right, int time) getWheelsParams()
        {
            int left = 0;
            int right = 0;
            int time = 0;
            DisplayConsoleUI("Set Wheel Parameters");
            left = isValidInt("Enter the speed of the Left Wheel (0-255): ", 0, 255);
            right = isValidInt("Enter the speed of the Right Wheel (0-255)", 0, 255);
            time = isValidInt("Enter the time for the Finch Robot to Drive (ms): ", 0, 100000);
            
            return (left, right, time);
        }

        /// <summary>
        /// Sets the wheels to move forward based on user defined parameters
        /// </summary>
        private static void setWheelsForward()
        {
            var values = getWheelsParams();

            myFinch.setMotors(values.left, values.right);
            myFinch.wait(values.time);
            myFinch.setMotors(0, 0);
        }

        /// <summary>
        /// Sets the wheels to move backwards based on user parameters
        /// </summary>
        private static void setWheelsBackwards()
        {
            var values = getWheelsParams();

            myFinch.setMotors(-values.left, -values.right);
            myFinch.wait(values.time);
            myFinch.setMotors(0, 0);
        }

        /// <summary>
        /// Sets the wheels to turn right based on hardcoded parameters
        /// </summary>
        private static void setWheelsRight()
        {
            myFinch.setMotors(0, 100);
            myFinch.wait(1000);
            myFinch.setMotors(0, 0);
        }

        /// <summary>
        /// Sets the wheels to turn left based on hardcoded parameters
        /// </summary>
        private static void setWheelsLeft()
        {
            myFinch.setMotors(100, 0);
            myFinch.wait(1000);
            myFinch.setMotors(0, 0);
        }

        #endregion

        #region Finch Robot Connection Logic
        /// <summary>
        /// Connect to the Finch Robot
        /// </summary>
        private static void DisplayConnectFinch()
        {
            DisplayConsoleUI("Let's get connected");
            Console.WriteLine("Let's get your Finch connected before proceeding further.");
            Console.WriteLine();
            Console.SetCursorPosition(1, 8);
            Console.WriteLine("Make sure to plug in the USB cable to the Computer and then to the Finch before proceeding!");
            DisplayContinuePrompt();
            if (!ConnectFinch())
            {
                    Console.WriteLine("Sorry I am unable to connect to the Finch at this time.");
                    Console.WriteLine();
                    Console.SetCursorPosition(1, 7);
                    Console.WriteLine("Please check that the cable is connected properly and that Windows see's your device");
                    Console.SetCursorPosition(1, 9);
                    Console.WriteLine("Unfortunately, I have to quit the application. Please try again. ");
                    DisplayContinuePrompt();
                    Environment.Exit(0);
            }
            else
            {
                DisplayConsoleUI("Finch is connected");
                Console.WriteLine("Yay, I can see your Finch!");
                DisplayContinuePrompt();
            }
        }

        /// <summary>
        /// Try to connect to the Finch Robot
        /// </summary>
        private static bool ConnectFinch()
        {
            bool connected = false;
            int MAX_TRIES = 5;
            for (int i = 0; i <= MAX_TRIES; i++)
            {
                if (myFinch.connect())
                { 
                    connected = true;
                }
                else
                {
                    if (i == MAX_TRIES)
                    {
                        connected = false;
                    }
                }
            }
            return connected;
        }

        /// <summary>
        /// Displays the Disconnect Finch Screen
        /// </summary>
        private static void DisplayDisconnectFinch()
        {
            DisplayConsoleUI("Let's get disconnected!");
            Console.WriteLine("Let's get your Finch disconnected. I will disconnect your Finch and Validate it is disconnected next.");
            DisplayContinuePrompt();
            if (DisconnectFinch())
            {
                DisplayConsoleUI("Finch is disconnected");
                Console.WriteLine("Sweet, You are Disconnected. I don't see a Finch Robot anymore.");
                DisplayContinuePrompt();
            }
        }

        /// <summary>
        /// Disconnect the Finch Robot
        /// </summary>
        private static bool DisconnectFinch()
        {
            bool disconnected = false;
            if (!disconnected)
            {
                myFinch.disConnect();
                disconnected = true;
                return disconnected;
            }
            else
            {
                return disconnected;
            }
        }

        #endregion

        #region User Validation

        /// <summary>
        /// Validates if user response is an integer
        /// </summary>
        /// <param name="prompt">Prompts the user for input</param>
        /// /// <param name="min">Lower bounds of the integer</param>
        /// /// <param name="max">Upward bounds of the integer</param>
        /// <returns>Returns the validated integer</returns>
        private static int isValidInt(string prompt, int min = 0, int max = 1000)
        {
            bool isValidInt = false;
            int x = 0;
            while (!isValidInt)
            {
                Console.Write($"{prompt}");
                isValidInt = int.TryParse(Console.ReadLine(), out x);
                if (!isValidInt || x < min || x > max)
                {
                    isValidInt = false;
                    Console.WriteLine("Sorry, that response isn't valid. Please try again!");
                }
            }
            return x;
        }

        /// <summary>
        /// Validates if user response is an integer
        /// </summary>
        /// <param name="screenName">Screen name for Menu</param>
        /// <param name="prompt">Prompts the user with menu</param>
        /// <param name="min">Lower bounds of the menu choice</param>
        /// <param name="max">Upper bounds of the menu choice</param>
        /// <returns>Returns the user menu choiceas a validated int</returns>
        private static int isValidMenuOption(string screenName, string prompt, int min, int max)
        {
            bool isValidMenuChoice = false;
            int userResponse = 0;
            while (!isValidMenuChoice)
            {
                DisplayConsoleUI($"{screenName}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($@"{prompt}");
                isValidMenuChoice = int.TryParse(Console.ReadLine(), out userResponse);
                if (!isValidMenuChoice || userResponse < min || userResponse > max)
                {
                    isValidMenuChoice = false;
                    DisplayConsoleUI("Invalid Option");
                    Console.WriteLine("Sorry, that is not a valid option. Please try again!");
                    DisplayContinuePrompt();
                }
            }
            return userResponse;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Displays the user Continue Prompt
        /// </summary>
        private static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }

        /// <summary>
        /// Quits the Application
        /// </summary>
        private static void quit()
        {
            Environment.Exit(0);
        }

        private static void DisplayWelcomeScreen()
        {
            DisplayConsoleUI("Welcome to The Finch Project");
            Console.WriteLine("Welcome to my Finch Project. I hope you enjoy this program. ");
            DisplayContinuePrompt();
        }

        #endregion
    } 
}