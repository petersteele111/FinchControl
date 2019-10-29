using FinchAPI;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace FinchTalentShow
{
    class Program
    {
        #region Enums for Menu's

        /// <summary>
        /// Main Menu Enum
        /// </summary>
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

        /// <summary>
        /// Talent Show Menu enum
        /// </summary>
        public enum TalentShowMenu : byte
        {
            LED = 1,
            Buzzer,
            Wheels,
            Back
        }

        /// <summary>
        /// LED Menu enum
        /// </summary>
        public enum LEDMenu : byte
        {
            On = 1,
            Blink,
            MultipleLED,
            Pulse,
            Back
        }

        /// <summary>
        /// Buzzer Menu enum
        /// </summary>
        public enum BuzzerMenu : byte
        {
            On = 1,
            StarWars,
            HappyBirthday,
            Back
        }

        /// <summary>
        /// Wheels Menu enum
        /// </summary>
        public enum WheelsMenu : byte
        {
            Forward = 1,
            Backward,
            Right,
            Left,
            Back
        }

        /// <summary>
        /// DataRecorder Menu enum
        /// </summary>
        public enum DataRedcorderMenu : byte
        {
            Light = 1,
            Temp,
            Back
        }

        /// <summary>
        /// Light Sensor Menu enum
        /// </summary>
        public enum LightSensorMenu : byte
        {
            Light = 1,
            Average,
            Back
        }

        /// <summary>
        /// Temperature Sensor Menu enum
        /// </summary>
        public enum TempSensorMenu : byte
        {
            Temp = 1,
            Back
        }

        /// <summary>
        /// Alarm System Menu enum
        /// </summary>
        public enum AlarmSystemMenu : byte
        {
            Light = 1,
            Temp,
            Both,
            Back
        }

        /// <summary>
        /// User Control Menu enum
        /// </summary>
        public enum UserControlMenu : byte
        {
            Parameters = 1,
            InsertCommands,
            ViewCommands,
            ExecuteCommands,
            Clear,
            Back
        }

        /// <summary>
        /// User Control Commands enum
        /// </summary>
        public enum UserControlCommands : byte
        {
            None,
            MoveForward,
            MoveBackward,
            StopMotors,
            TurnRight,
            TurnLeft,
            LEDOn,
            LEDOff,
            BuzzerOn,
            BuzzerOff,
            Spin,
            Temp,
            Light,
            StarWars,
            Done
        }

        public enum Login : byte
        {
            Login = 1,
            Register
        }

        #endregion

        #region Main method of the program

        readonly static Finch myFinch = new Finch();

        /// <summary>
        /// Main Method of the application
        /// </summary>
        static void Main()
        {
            bool IsLoggedIn = false;
            DisplayWelcomeScreen();
            GetUserAuthMenuOption(DisplayUserAuth());
            while (IsLoggedIn)
            {
                DisplayConsoleUI("Finch Control v1.0");
                GetMenuOption(DisplayMainMenu());
            }
        }


        #endregion

        #region Generate UI

        /// <summary>
        /// Generate the main console for the UI
        /// </summary>
        private static void DisplayConsoleUI(string programName, int bottom = 28)
        {
            // Setup UI for application
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
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
                Console.SetCursorPosition(0 + i, bottom);
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
            Console.WriteLine("So our time has sadly come to an end. I enjoyed working with your Finch today!");
            Console.WriteLine();
            Console.SetCursorPosition(1, 7);
            Console.WriteLine("Thank you for using this program. Until next time!");
            DisplayContinuePrompt();
            Quit();
        }

        /// <summary>
        /// Displays the Welcome Screen
        /// </summary>
        private static void DisplayWelcomeScreen()
        {
            DisplayConsoleUI("Welcome to The Finch Project");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to my Finch Project. I hope you enjoy this program. ");
            DisplayContinuePrompt();
        }

        #endregion

        #region Menu's of Application

        #region Main Menu


        /// <summary>
        /// Generate the main menu for the console
        /// </summary>
        private static int DisplayMainMenu()
        {
            int userResponse = IsValidMenuOption("Finch Control v1.0", @"
             Menu

  1. Connect Finch Robot
  
  2. Talent Show
  
  3. Data Recorder

  4. Alarm System

  5. User Programming 

  6. Disconnect Finch Robot

  7. Quit Application


  



  Option (1-7): ", 1, 7);
            return userResponse;
        }

        /// <summary>
        /// Performs an action based on the menu option selected
        /// </summary>
        /// <param name="option">Menu option chosen</param>
        private static void GetMenuOption(int option)
        {
            Menu menuChoice = (Menu)option;

            switch (menuChoice)
            {
                case Menu.ConnectFinch:
                    DisplayConnectFinch();
                    break;
                case Menu.TalentShow:
                    DisplayTalentShowMenuInstructions();
                    GetTalentShowMenuOption(DisplayTalentShowMenu());
                    break;
                case Menu.DataRecorder:
                    DisplayDataRecorderMenuInstructions();
                    GetDataRecorderMenuOption(DisplayDataRecorderMenu());
                    break;
                case Menu.AlarmSystem:
                    DisplayAlarmSystemInstructions();
                    GetDisplayAlarmSystemMenuOption(DisplayAlarmSystemMenu());
                    DisplayContinuePrompt();
                    break;
                case Menu.UserProgramming:
                    DisplayUserControlMenu();
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

        #endregion

        #region TalentShow Menus

        /// <summary>
        /// Displays the Talent Show Menu
        /// </summary>
        /// <returns>Returns an int of the menu option chosen</returns>
        private static int DisplayTalentShowMenu()
        {
            int userResponse = IsValidMenuOption("Talent Show Control Menu", @"
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
        private static void GetTalentShowMenuOption(int option)
        {
            TalentShowMenu menuOption = (TalentShowMenu)option;

            switch (menuOption)
            {
                case TalentShowMenu.LED:
                    DisplayLEDMenuInstructions();
                    GetLEDMenuOption(DisplayLEDMenu());
                    break;
                case TalentShowMenu.Buzzer:
                    DisplayBuzzerMenuInstructions();
                    GetBuzzerMenuOption(DisplayBuzzerMenu());
                    break;
                case TalentShowMenu.Wheels:
                    DisplayWheelMenuInstructions();
                    GetWheelMenuOption(DisplayWheelsMenu());
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
        /// Displays LED Menu
        /// </summary>
        /// <returns>Menu Option Chosen</returns>
        private static int DisplayLEDMenu()
        {
            int userResponse = IsValidMenuOption("LED Control Menu", @"
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
        private static void GetLEDMenuOption(int option)
        {
            LEDMenu menuChoice = (LEDMenu)option;

            switch (menuChoice)
            {
                case LEDMenu.On:
                    DisplayConnectFinch();
                    DisplayConsoleUI("LED On");
                    SetLEDOn();
                    SetLEDOff();
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
            int userResponse = IsValidMenuOption("Buzzer Control Menu", @"
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
        private static void GetBuzzerMenuOption(int option)
        {
            BuzzerMenu menuChoice = (BuzzerMenu)option;

            switch (menuChoice)
            {
                case BuzzerMenu.On:
                    DisplayConnectFinch();
                    SetBuzzerOn();
                    break;
                case BuzzerMenu.StarWars:
                    DisplayConnectFinch();
                    BuzzerStarWars();
                    break;
                case BuzzerMenu.HappyBirthday:
                    DisplayConnectFinch();
                    BuzzerHappyBirthday();
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
            int userResponse = IsValidMenuOption("Wheels Control Menu", @"            
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
        private static void GetWheelMenuOption(int option)
        {
            WheelsMenu menuOption = (WheelsMenu)option;

            switch (menuOption)
            {
                case WheelsMenu.Forward:
                    DisplayConnectFinch();
                    SetWheelsForward();
                    break;
                case WheelsMenu.Backward:
                    DisplayConnectFinch();
                    SetWheelsBackwards();
                    break;
                case WheelsMenu.Right:
                    DisplayConnectFinch();
                    SetWheelsRight();
                    break;
                case WheelsMenu.Left:
                    DisplayConnectFinch();
                    SetWheelsLeft();
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

        #region DataRecorder Menus

        /// <summary>
        /// Displays the Data Recording Menu
        /// </summary>
        /// <returns>Returns user option for menu</returns>
        private static int DisplayDataRecorderMenu()
        {
            int userResponse = IsValidMenuOption("Data Recorder Control Menu", @"
             Data Recorder Menu
  
  1. Light Sensor Control

  2. Temp Sensor Control

  3. Back














  Option (1-3): ", 1, 3);
            return userResponse;
        }

        /// <summary>
        /// Gets the Menu option for the Data Recorder Menu
        /// </summary>
        /// <param name="option">User selected option for the menu</param>
        private static void GetDataRecorderMenuOption(int option)
        {
            DataRedcorderMenu menuOption = (DataRedcorderMenu)option;

            switch (menuOption)
            {
                case DataRedcorderMenu.Light:
                    GetLightSensorMenuOption(DisplayLightSensorMenu());
                    break;
                case DataRedcorderMenu.Temp:
                    GetTempSensorMenuOption(DisplayTempSensorMenu());
                    break;
                case DataRedcorderMenu.Back:
                    GetMenuOption(DisplayMainMenu());
                    break;
                default:
                    DisplayConsoleUI("Invlaid Response");
                    Console.WriteLine("Sorry I don't understand your response, please try again.");
                    DisplayContinuePrompt();
                    break;
            }
        }

        /// <summary>
        /// Display the Light Sensor Menu
        /// </summary>
        /// <returns>Returns the user option for the menu</returns>
        private static int DisplayLightSensorMenu()
        {
            int userResponse = IsValidMenuOption("Light Sensor Menu", @"
             Light Sensor

  1. Get Light Sensor Data

  2. Get Light Sensor Data Averaged

  3. Back














  Option (1-3): ", 1, 3);
            return userResponse;
        }

        /// <summary>
        /// Get Light Sensor Menu User Choice
        /// </summary>
        /// <param name="option">users menu choice</param>
        private static void GetLightSensorMenuOption(int option)
        {
            LightSensorMenu menuOption = (LightSensorMenu)option;

            switch (menuOption)
            {
                case LightSensorMenu.Light:
                    DisplayConnectFinch();
                    DisplayLightSensorData(GetLightSensorData());
                    break;
                case LightSensorMenu.Average:
                    DisplayConnectFinch();
                    DisplayLightSensorDataAverage(GetLightSensorData());
                    break;
                case LightSensorMenu.Back:
                    GetDataRecorderMenuOption(DisplayDataRecorderMenu());
                    break;
                default:
                    DisplayConsoleUI("Invlaid Response");
                    Console.WriteLine("Sorry I don't understand your response, please try again.");
                    DisplayContinuePrompt();
                    break;
            }
        }

        /// <summary>
        /// Display the Temp Sensor Menu
        /// </summary>
        /// <returns>Retuns user menu choice</returns>
        private static int DisplayTempSensorMenu()
        {
            int userResponse = IsValidMenuOption("Temperature Sensor Menu", @"
             Temperature Sensor

  1. Get Temperature Sensor Data

  2. Back
















  options (1-2): ", 1, 2);
            return userResponse;
        }

        /// <summary>
        /// Get Temp Sensor Menu User Choice
        /// </summary>
        /// <param name="option">users menu choice</param>
        private static void GetTempSensorMenuOption(int option)
        {
            TempSensorMenu menuChoice = (TempSensorMenu)option;

            switch (menuChoice)
            {
                case TempSensorMenu.Temp:
                    DisplayConnectFinch();
                    DisplayTempSensorData();
                    break;
                case TempSensorMenu.Back:
                    GetDataRecorderMenuOption(DisplayDataRecorderMenu());
                    break;
                default:
                    DisplayConsoleUI("Invlaid Response");
                    Console.WriteLine("Sorry I don't understand your response, please try again.");
                    DisplayContinuePrompt();
                    break;
            }
        }

        #endregion

        #region AlarmSystem Menus

        /// <summary>
        /// Display the Alarm System Main Menu
        /// </summary>
        /// <returns>Returns the menu choice chosen by the user</returns>
        private static int DisplayAlarmSystemMenu()
        {
            int userResponse = IsValidMenuOption("Alarm System Menu", @"

             Alarm System Menu

  1. Light Sensor Alarm

  2. Temp Sensor Alarm

  3. Both Light and Temp Alarm

  4. Back











  option(1-4): ", 1, 4);
            return userResponse;
        }

        /// <summary>
        /// Performs an action based on the menu option selected
        /// </summary>
        /// <param name="option">Menu Option Chosen</param>
        private static void GetDisplayAlarmSystemMenuOption(int option)
        {
            string alarmType;
            AlarmSystemMenu menuChoice = (AlarmSystemMenu)option;

            switch (menuChoice)
            {
                case AlarmSystemMenu.Light:
                    alarmType = "light";
                    DisplayConnectFinch();
                    DisplayLightAlarm(alarmType);
                    break;
                case AlarmSystemMenu.Temp:
                    alarmType = "temp";
                    DisplayConnectFinch();
                    DisplayTempAlarm(alarmType);
                    break;
                case AlarmSystemMenu.Both:
                    alarmType = "both";
                    DisplayConnectFinch();
                    DisplayBothAlarms(alarmType);
                    break;
                case AlarmSystemMenu.Back:
                    GetMenuOption(DisplayMainMenu());
                    break;
                default:
                    DisplayConsoleUI("Invlaid Response");
                    Console.WriteLine("Sorry I don't understand your response, please try again.");
                    DisplayContinuePrompt();
                    break;
            }
        }

        #endregion

        #region User Control Menus

        /// <summary>
        /// Displays the User Control Menu
        /// </summary>
        /// <returns>Returns the users menu choice</returns>
        private static void DisplayUserControlMenu()
        {
            List<Tuple<UserControlCommands, int>> commands = new List<Tuple<UserControlCommands, int>>();
            (int wheelSpeed, int r, int g, int b, int hertz) userControlParams;
            userControlParams.wheelSpeed = 0;
            userControlParams.r = 0;
            userControlParams.g = 0;
            userControlParams.b = 0;
            userControlParams.hertz = 0;
            DisplayUserControlInstructions();
            DisplayConsoleUI("User Control Params");
            userControlParams = GetUserControlParams();
            bool back = false;
            do
            {
                int userResponse = IsValidMenuOption("User Control Menu", @"

             User Control Menu

  1. Input Parameters

  2. Insert Commands

  3. View Commands

  4. Execute Commands

  5. Clear Commands (Reset)

  6. Back







  Option (1-4): ", 1, 6);
                UserControlMenu menuChoice = (UserControlMenu)userResponse;
                switch (menuChoice)
                {
                    case UserControlMenu.Parameters:
                        userControlParams = GetUserControlParams();
                        break;
                    case UserControlMenu.InsertCommands:
                        commands = DisplayUserControlCommands(commands);
                        break;
                    case UserControlMenu.ViewCommands:
                        ViewUserControlCommandsInput(commands);
                        break;
                    case UserControlMenu.ExecuteCommands:
                        DisplayConnectFinch();
                        ExecuteUserControlCommands(commands, userControlParams);
                        break;
                    case UserControlMenu.Clear:
                        commands = ClearUserControlCommandList(commands);
                        break;
                    case UserControlMenu.Back:
                        back = true;
                        break;
                    default:
                        DisplayConsoleUI("Invlaid Response");
                        Console.WriteLine("Sorry I don't understand your response, please try again.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!back);
        }
        #endregion

        #endregion

        #region Control Systems

        #region TalentShow Control

        #region LED Control
        /// <summary>
        /// Get's the LED parameters and passes it to the SetLEDOn Method
        /// </summary>
        private static (int r, int g, int b, int time) GetLEDParams()
        {
            DisplayConsoleUI("Set LED Parameters");
            Console.ForegroundColor = ConsoleColor.Green;
            int r = IsValidInt("Enter value for Red LED <0(Off) - 255(Max Brightness)>: ", 0, 255);
            int g = IsValidInt("Enter Value for Green LED <0(Off) - 255(Max Brightness)>: ", 0, 255);
            int b = IsValidInt("Enter Value for Blue LED <0(Off) - 255(Max Brightness)>: ", 0, 255);
            int time = IsValidInt("Enter Value for Time (seconds): ", 0, 100000);
            time *= 1000;
            return (r, g, b, time);
        }

        /// <summary>
        /// Prompts the user for the LED Parameters and time to turn the LED on
        /// Turns the LED on for the specified color and time set by the user
        /// </summary>
        private static void SetLEDOn()
        {
            var (r, g, b, time) = GetLEDParams();

            myFinch.setLED(r, g, b);
            myFinch.wait(time);
        }

        /// <summary>
        /// Sets the LED to On with manually passed parameters
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="time"></param>
        private static void SetLEDOn(int r = 255, int g = 255, int b = 255, int time = 1000)
        {
            myFinch.setLED(r, g, b);
            myFinch.wait(time);

        }

        /// <summary>
        /// Turns the LED off
        /// </summary>
        /// <param name="time">Specifies the wait time for the LED to be off. Used for blinking the LED</param>
        private static void SetLEDOff(int time = 500)
        {
            myFinch.setLED(0, 0, 0);
            myFinch.wait(time);
        }

        /// <summary>
        /// Blinks the LED based on user input parameters
        /// </summary>
        private static void LEDBlink()
        {
            var (r, g, b, time) = GetLEDParams();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Enter the number of blinks, 0 is infinite: ");
            int.TryParse(Console.ReadLine(), out int numberOfBlinks);
            if (numberOfBlinks == 0)
            {
                while (true)
                {
                    SetLEDOn(r, g, b, time);
                    SetLEDOff();
                }
            }
            else
            {
                for (int i = 0; i < numberOfBlinks; i++)
                {
                    SetLEDOn(r, g, b, time);
                    SetLEDOff();
                }
            }
        }

        /// <summary>
        /// Shows multiple LED colors at varying times. Hard Coded values
        /// </summary>
        private static void ShowMultipleLED()
        {

            SetLEDOn(255, 255, 255, 1000);
            SetLEDOff(500);
            SetLEDOn(255, 0, 0, 500);
            SetLEDOff(500);
            SetLEDOn(0, 255, 0, 2000);
            SetLEDOff(500);
            SetLEDOn(0, 0, 255, 1500);
            SetLEDOff(500);
        }

        /// <summary>
        /// Breathes the LED from white to Red to Green to Blue
        /// </summary>
        private static void LEDPulse()
        {
            for (int i = 0; i < 255; i++)
            {
                SetLEDOn(i, 0, 0, 1);
            }
            for (int i = 255; i >= 0; i--)
            {
                SetLEDOn(i, 0, 0, 1);
            }

            for (int i = 0; i < 255; i++)
            {
                SetLEDOn(0, i, 0, 1);
            }
            for (int i = 255; i >= 0; i--)
            {
                SetLEDOn(0, i, 0, 1);
            }

            for (int i = 0; i < 255; i++)
            {
                SetLEDOn(0, 0, i, 1);
            }
            for (int i = 255; i >= 0; i--)
            {
                SetLEDOn(0, 0, i, 1);
            }
        }

        #endregion

        #region Buzzer Control

        /// <summary>
        /// Gets Buzzer Parameters
        /// </summary>
        /// <returns>Returns Tuple of parameters</returns>
        private static (int hertz, int time) GetBuzzerParams()
        {
            DisplayConsoleUI("Get Buzzer Parameters");
            Console.ForegroundColor = ConsoleColor.Green;
            int hertz = IsValidInt("Please enter the Frequency of the Buzzer (hertz): ", 0, 22000);
            int time = IsValidInt("Please enter the length of time for the Buzzer to be on (seconds): ", 0, 100000);
            time *= 1000;
            return (hertz, time);
        }

        /// <summary>
        /// Sets the buzzer to on based on the user specified parameters
        /// </summary>
        private static void SetBuzzerOn()
        {
            var (hertz, time) = GetBuzzerParams();

            myFinch.noteOn(hertz);
            myFinch.wait(time);
            myFinch.noteOff();
        }

        /// <summary>
        /// Sets the buzzer to on based on hardcoded parameters
        /// </summary>
        /// <param name="hertz">Tone generated in hertz</param>
        /// <param name="time">Time to play tone</param>
        private static void SetBuzzerOn(int hertz, int time)
        {
            myFinch.noteOn(hertz);
            myFinch.wait(time);
            myFinch.noteOff();
        }

        /// <summary>
        /// Plays Star Wars Song
        /// </summary>
        private static void BuzzerStarWars()
        {
            SetBuzzerOn(300, 500);
            myFinch.wait(50);
            SetBuzzerOn(300, 500);
            myFinch.wait(50);
            SetBuzzerOn(300, 500);
            myFinch.wait(50);
            SetBuzzerOn(250, 500);
            myFinch.wait(50);
            SetBuzzerOn(350, 250);
            SetBuzzerOn(300, 500);
            myFinch.wait(50);
            SetBuzzerOn(250, 500);
            myFinch.wait(50);
            SetBuzzerOn(350, 250);
            SetBuzzerOn(300, 500);
            myFinch.wait(50);
        }

        /// <summary>
        /// Plays Happy Birthday
        /// </summary>
        private static void BuzzerHappyBirthday()
        {
            SetBuzzerOn(264, 125);
            myFinch.wait(250);
            SetBuzzerOn(264, 125);
            myFinch.wait(125);
            SetBuzzerOn(297, 500);
            myFinch.wait(125);
            SetBuzzerOn(264, 500);
            myFinch.wait(125);
            SetBuzzerOn(352, 500);
            myFinch.wait(125);
            SetBuzzerOn(330, 1000);
            myFinch.wait(250);
            SetBuzzerOn(264, 125);
            myFinch.wait(250);
            SetBuzzerOn(264, 125);
            myFinch.wait(125);
            SetBuzzerOn(297, 500);
            myFinch.wait(125);
            SetBuzzerOn(264, 500);
            myFinch.wait(125);
            SetBuzzerOn(396, 500);
            myFinch.wait(125);
            SetBuzzerOn(352, 1000);
            myFinch.wait(250);
            SetBuzzerOn(264, 125);
            myFinch.wait(250);
            SetBuzzerOn(264, 125);
            myFinch.wait(125);
            SetBuzzerOn(2642, 500);
            myFinch.wait(125);
            SetBuzzerOn(440, 500);
            myFinch.wait(125);
            SetBuzzerOn(352, 250);
            myFinch.wait(125);
            SetBuzzerOn(352, 125);
            myFinch.wait(125);
            SetBuzzerOn(330, 500);
            myFinch.wait(125);
            SetBuzzerOn(297, 1000);
            myFinch.wait(250);
            SetBuzzerOn(466, 125);
            myFinch.wait(250);
            SetBuzzerOn(466, 125);
            myFinch.wait(125);
            SetBuzzerOn(440, 500);
            myFinch.wait(125);
            SetBuzzerOn(352, 500);
            myFinch.wait(125);
            SetBuzzerOn(396, 500);
            myFinch.wait(125);
            SetBuzzerOn(352, 1000);
        }

        #endregion

        #region Wheels Control

        /// <summary>
        /// Gets Wheel Parameters
        /// </summary>
        /// <returns>Returns a tuple for wheel parameters</returns>
        private static (int left, int right, int time) GetWheelsParams()
        {
            DisplayConsoleUI("Set Wheel Parameters");
            Console.ForegroundColor = ConsoleColor.Green;
            int left = IsValidInt("Enter the speed of the Left Wheel <0(Stopped) - 255(Full Speed)>: ", 0, 255);
            int right = IsValidInt("Enter the speed of the Right Wheel <0(Stopped) - 255(Full Speed)>", 0, 255);
            int time = IsValidInt("Enter the time for the Finch Robot to Drive (seconds): ", 0, 100000);
            time *= 1000;
            return (left, right, time);
        }

        /// <summary>
        /// Sets the wheels to move forward based on user defined parameters
        /// </summary>
        private static void SetWheelsForward()
        {
            var (left, right, time) = GetWheelsParams();

            myFinch.setMotors(left, right);
            myFinch.wait(time);
            myFinch.setMotors(0, 0);
        }

        /// <summary>
        /// Sets the wheels to move backwards based on user parameters
        /// </summary>
        private static void SetWheelsBackwards()
        {
            var (left, right, time) = GetWheelsParams();

            myFinch.setMotors(-left, -right);
            myFinch.wait(time);
            myFinch.setMotors(0, 0);
        }

        /// <summary>
        /// Sets the wheels to turn right based on hardcoded parameters
        /// </summary>
        private static void SetWheelsRight()
        {
            myFinch.setMotors(0, 100);
            myFinch.wait(1000);
            myFinch.setMotors(0, 0);
        }

        /// <summary>
        /// Sets the wheels to turn left based on hardcoded parameters
        /// </summary>
        private static void SetWheelsLeft()
        {
            myFinch.setMotors(100, 0);
            myFinch.wait(1000);
            myFinch.setMotors(0, 0);
        }

        #endregion

        #endregion

        #region DataRecorder Control

        /// <summary>
        /// Get the Light Sensor Data Parameters
        /// </summary>
        /// <returns>Returns a tuple of the Light Sensor Parameters</returns>
        private static (int time, int dataPoints) GetLightSensorParams()
        {
            DisplayConsoleUI("Get Light Sensor Parameters");
            Console.ForegroundColor = ConsoleColor.Green;
            int time = IsValidInt("Please enter the Frequency you wish to collect Sensor Data (seconds): ", 1, 100000);
            int dataPoints = IsValidInt("Please enter the amount of Data Points you wish to collect: ", 1, 100000);
            Console.WriteLine();
            Console.WriteLine($"Gathering the Data. Time until completion: {(double)(time * dataPoints) / 60:F1} mins");
            time *= 1000;
            return (time, dataPoints);
        }

        /// <summary>
        /// Get the Light Sensor Data
        /// </summary>
        /// <returns>Returns Multidimensional Array of Left and Right Light Sensor Readings</returns>
        private static int[][] GetLightSensorData()
        {
            var (time, dataPoints) = GetLightSensorParams();

            int[][] sensorData = new int[dataPoints][];
            for (int i = 0; i < dataPoints; i++)
            {
                sensorData[i] = myFinch.getLightSensors();
                myFinch.wait(time);
            }
            return sensorData;
        }

        /// <summary>
        /// Displays the Light Sensor Data
        /// </summary>
        /// <param name="sensorData">Jagged Array containing Left and Right Light Sensor Data</param>
        private static void DisplayLightSensorData(int[][] sensorData)
        {
            DisplayConsoleUI("Light Sensor Data", (sensorData.Length) + 7);
            Console.SetCursorPosition(15, 5);
            Console.Write(string.Format($"{"Left Sensor Value",17}  |  {"Right Sensor Value",18}"));
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < sensorData.Length; i++)
            {
                if (sensorData.Length <= 9)
                {
                    Console.WriteLine(string.Format($"Data Point #{i + 1} {sensorData[i][0],10}{sensorData[i][1],20}"));
                }
                else
                {
                    while (i < 9)
                    {
                        Console.WriteLine(string.Format($"Data Point # {i + 1} {sensorData[i][0],10}{sensorData[i][1],20}"));
                        i++;
                    }
                    Console.WriteLine(string.Format($"Data Point #{i + 1} {sensorData[i][0],10}{sensorData[i][1],20}"));
                }
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Displays the Light Sensor Data Averaged
        /// </summary>
        /// <param name="sensorData">Jagged Int Array that holds the light sensor data</param>
        private static void DisplayLightSensorDataAverage(int[][] sensorData)
        {
            DisplayConsoleUI("Light Sensor Data", (sensorData.Length) + 7);
            Console.SetCursorPosition(15, 5);
            Console.WriteLine("Averaged Light Sensor Data");
            Console.WriteLine();
            for (int i = 0; i < sensorData.Length; i++)
            {
                if (sensorData.Length <= 9)
                {
                    Console.WriteLine($"Data Point #{i + 1} {(sensorData[i][0] + sensorData[i][1]) / 2}");
                }
                else
                {
                    while (i < 9)
                    {
                        Console.WriteLine($"Data Point # {i + 1} {(sensorData[i][0] + sensorData[i][1]) / 2}");
                        i++;
                    }
                    Console.WriteLine($"Data Point #{i + 1} {(sensorData[i][0] + sensorData[i][1]) / 2}");
                }
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Get the Parameters for the Temp Sensor Data
        /// </summary>
        /// <returns>Returns a Tuple for Frequency and Data Points</returns>
        private static (int time, int dataPoints) GetTempSensorParams()
        {
            DisplayConsoleUI("Get Temp Sensor Parameters");
            Console.ForegroundColor = ConsoleColor.Green;
            int time = IsValidInt("Please enter the Frequency you wish to collect Sensor Data (seconds): ", 1, 100000);
            int dataPoints = IsValidInt("Please enter the amount of Data Points you wish to collect: ", 1, 100000);
            Console.WriteLine();
            Console.WriteLine($"Gathering the Data. Time until completion: {(double)(time * dataPoints) / 60:F1} mins");
            return (time, dataPoints);
        }

        /// <summary>
        /// Gets the Temperature Sensor Data
        /// </summary>
        /// <returns>Returns tuple with double array of sensorData and bool for Farenheight to Celcius</returns>
        private static (double[] sensorData, bool CtoFa) GetTempSensorData()
        {
            var (time, dataPoints) = GetTempSensorParams();
            bool CtoFa = CtoF();
            double[] sensorData = new double[dataPoints];
            for (int i = 0; i < dataPoints; i++)
            {
                sensorData[i] = myFinch.getTemperature();
                myFinch.wait(time * 1000);
            }
            if (CtoFa)
            {
                sensorData = ConvertCtoF(sensorData);
            }
            return (sensorData, CtoFa);
        }

        /// <summary>
        /// Displays the Temperature Sensor Data to the Screen
        /// </summary>
        private static void DisplayTempSensorData()
        {
            var (sensorData, CtoFa) = GetTempSensorData();

            bool CtoF = CtoFa;
            string tempFormat;

            if (CtoF)
            {
                tempFormat = "°F";
            }
            else
            {
                tempFormat = "°C";
            }

            DisplayConsoleUI("Temperature Sensor Data", (sensorData.Length) + 7);
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("Temperature Sensor Data");
            Console.WriteLine();
            for (int i = 0; i < sensorData.Length; i++)
            {
                if (sensorData.Length < 9)
                {
                    Console.WriteLine($"Data Point #{i + 1} {sensorData[i]:F2}{tempFormat}");
                }
                else
                {
                    while (i < 9)
                    {
                        Console.WriteLine($"Data Point # {i + 1} {sensorData[i]:F2}{tempFormat}");
                        i++;
                    }
                    Console.WriteLine($"Data Point #{i + 1} {sensorData[i]:F2}{tempFormat}");
                }
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Prompts the user if they want the Temperature Sensor Data in Celcius or Farenheight
        /// </summary>
        /// <returns>Returns boolean value indicating if they wish to convert to Farenheight or not</returns>
        private static bool CtoF()
        {
            bool isValid = false;
            bool FtoC = false;
            while (!isValid)
            {
                Console.WriteLine();
                Console.Write("Would you like the Temperature to be in Celcius or Farenheight (C or F): ");
                string userResponse = Console.ReadLine().ToUpper().Trim();
                if (userResponse.Equals("C") || userResponse.Equals("F"))
                {
                    isValid = true;
                    if (userResponse.Equals("F"))
                    {
                        FtoC = true;
                    }
                }
                else
                {
                    Console.WriteLine("Sorry I don't understand your response. Please only input C or F");
                    Console.WriteLine();
                }
            }
            return FtoC;
        }

        /// <summary>
        /// Converts the Celcius data to Farenheight
        /// </summary>
        /// <param name="sensorData">Double Array of temperature sensor data</param>
        /// <returns>Returns converted Double Array of temperature data in Farenheight</returns>
        private static double[] ConvertCtoF(double[] sensorData)
        {
            double[] convertedSensorData = new double[sensorData.Length];
            for (int i = 0; i < sensorData.Length; i++)
            {
                convertedSensorData[i] = (sensorData[i] * 1.8) + 32;
            }
            return convertedSensorData;
        }

        /// <summary>
        /// Converts Celcius to Farenheight
        /// </summary>
        /// <param name="temp">Double of temperature data</param>
        /// <returns>Returns the converted temperature in farenheight</returns>
        private static double ConvertCtoF(double temp)
        {
            return (temp * 1.8) + 32;
        }

        #endregion

        #region AlarmSystem Control

        /// <summary>
        /// Get the User parameters for the Alarm System
        /// </summary>
        /// <param name="alarmType">Type of Alarm the user wants to run</param>
        /// <returns>Returns a tuple with the alarm parameters</returns>
        private static (int time, int lightLowerThreshold, int lightUpperThreshold, double tempLowerThreshold, double tempUpperThreshold) GetMultipleAlarmSystemParams(string alarmType)
        {
            DisplayConsoleUI("Get Both Light and Temp Alarm System Parameters");
            Console.ForegroundColor = ConsoleColor.Green;
            int lightLowerThreshold = 0;
            int lightUpperThreshold = 0;
            double tempLowerThreshold = 0;
            double tempUpperThreshold = 0;
            int time = IsValidInt("Please enter the time (seconds) you wish to run this monitoring system: ", 1, 100000);
            if (alarmType.Equals("light"))
            {
                lightLowerThreshold = IsValidInt("Please enter the lower light sensor threshhold (0-255): ", 0, 255);
                lightUpperThreshold = IsValidInt("Please enter the upper light sensor threshold (0-255): ", 0, 255);

                Console.WriteLine("Ok, now that you have entered in the thresholds for the alarm system, hit Enter to Activate the alarm!");
                DisplayContinuePrompt();
                return (time, lightLowerThreshold, lightUpperThreshold, tempLowerThreshold, tempUpperThreshold);
            }
            else if (alarmType.Equals("temp"))
            {
                tempLowerThreshold = IsValidDouble("Please enter the lower temp sensor threshold (-40.00 - 150.00): ", -40.00, 150.00);
                tempUpperThreshold = IsValidDouble("Please eneter the upper temp sensor threshold (-40.00 - 150.00): ", -40.00, 150.00);

                Console.WriteLine("Ok, now that you have entered in the thresholds for the alarm system, hit Enter to Activate the alarm!");
                DisplayContinuePrompt();
                return (time, lightLowerThreshold, lightUpperThreshold, tempLowerThreshold, tempUpperThreshold);
            }
            else
            {
                lightLowerThreshold = IsValidInt("Please enter the lower light sensor threshhold (0-255): ", 0, 255);
                lightUpperThreshold = IsValidInt("Please enter the upper light sensor threshold (0-255): ", 0, 255);
                tempLowerThreshold = IsValidDouble("Please enter the lower temp sensor threshold (-40.00 - 150.00): ", -40.00, 150.00);
                tempUpperThreshold = IsValidDouble("Please eneter the upper temp sensor threshold (-40.00 - 150.00): ", -40.00, 150.00);

                Console.WriteLine("Ok, now that you have entered in the thresholds for the alarm system, hit Enter to Activate the alarm!");
                DisplayContinuePrompt();
                return (time, lightLowerThreshold, lightUpperThreshold, tempLowerThreshold, tempUpperThreshold);
            }
        }

        /// <summary>
        /// Displays the Light Alarm System
        /// </summary>
        /// <param name="alarmType">Type of Alarm the user wants to run</param>
        private static void DisplayLightAlarm(string alarmType)
        {
            var (time, lightLowerThreshold, lightUpperThreshold, _, _) = GetMultipleAlarmSystemParams(alarmType); //Using Discards for data I don't need
            DisplayConsoleUI("Light Alarm System Active");
            int count = 1;
            int ambientLight = myFinch.getLeftLightSensor();
            Console.WriteLine();
            Console.WriteLine($"Lower Light Threshold = {lightLowerThreshold}");
            Console.WriteLine($"Upper Light Threshold = {lightUpperThreshold}");
            Console.WriteLine($"Ambient Light Reading = {ambientLight}");
            Console.WriteLine();
            Console.WriteLine();
            if (ambientLight > lightLowerThreshold && ambientLight < lightUpperThreshold && count <= time)
            {
                while (true)
                {
                    int currentLightReading = myFinch.getLeftLightSensor();
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
                    Console.Write($"Current Light Sensor Value = {currentLightReading}");
                    myFinch.wait(1000);
                    count++;
                    if (currentLightReading < lightLowerThreshold || currentLightReading > lightUpperThreshold)
                    {
                        DisplayConsoleUI("Alarm Triggered!!!!!");
                        Console.WriteLine();
                        Console.WriteLine($"Current Light Reading = {currentLightReading}");
                        Console.WriteLine("Threshold's exceeded. Alarm Triggered");
                        myFinch.setLED(255, 0, 0);
                        int buzzer = 5;
                        for (int i = 0; i < buzzer; i++)
                        {
                            myFinch.noteOn(2000);
                            myFinch.wait(500);
                            myFinch.noteOff();
                            myFinch.wait(500);
                        }
                        myFinch.setLED(0, 0, 0);
                        myFinch.noteOff();
                        break;
                    }
                    else if (count >= time)
                    {
                        DisplayConsoleUI("No Alarm Trigged");
                        Console.WriteLine("Alarm System has timed out. No events detected!");
                        myFinch.setLED(0, 255, 0);
                        myFinch.wait(5000);
                        myFinch.setLED(0, 0, 0);
                    }
                }
            }
            else
            {
                Console.WriteLine("Sorry, but the ambient light reading is already either lower or higher than the specified Thresholds. Please try again!");
            }
        }

        /// <summary>
        /// Displays the Temp Alarm System
        /// </summary>
        /// <param name="alarmType">Type of Alarm the user wants to run</param>
        private static void DisplayTempAlarm(string alarmType)
        {
            var (time, _, _, tempLowerThreshold, tempUpperThreshold) = GetMultipleAlarmSystemParams(alarmType);
            DisplayConsoleUI("Temp Alarm System Active");
            int count = 1;
            double ambientTemp = ConvertCtoF(myFinch.getTemperature());
            Console.WriteLine();
            Console.WriteLine($"Lower Temp Threshold = {tempLowerThreshold}");
            Console.WriteLine($"Upper Temp Threshold = {tempUpperThreshold}");
            Console.WriteLine($"Ambient Temp Reading = {ambientTemp:F2}");
            Console.WriteLine();
            Console.WriteLine();
            if (ambientTemp > tempLowerThreshold && ambientTemp < tempUpperThreshold && count <= time)
            {
                while (true)
                {
                    double currentTempReading = ConvertCtoF(myFinch.getTemperature());
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
                    Console.Write($"Current Temp Sensor Value = {currentTempReading:F2}");
                    myFinch.wait(1000);
                    count++;
                    if (currentTempReading < tempLowerThreshold || currentTempReading > tempUpperThreshold)
                    {
                        DisplayConsoleUI("Alarm Triggered!!!!!");
                        Console.WriteLine();
                        Console.WriteLine($"Current Temp Reading = {currentTempReading}");
                        Console.WriteLine("Threshold's exceeded. Alarm Triggered");
                        myFinch.setLED(255, 0, 0);
                        int buzzer = 5;
                        for (int i = 0; i < buzzer; i++)
                        {
                            myFinch.noteOn(2000);
                            myFinch.wait(500);
                            myFinch.noteOff();
                            myFinch.wait(500);
                        }
                        myFinch.setLED(0, 0, 0);
                        myFinch.noteOff();
                        break;
                    }
                    else if (count >= time)
                    {
                        DisplayConsoleUI("No Alarm Trigged");
                        Console.WriteLine("Alarm System has timed out. No events detected!");
                        myFinch.setLED(0, 255, 0);
                        myFinch.wait(5000);
                        myFinch.setLED(0, 0, 0);
                    }
                }
            }
            else
            {
                Console.WriteLine("Sorry, but the ambient Temp reading is already either lower or higher than the specified Thresholds. Please try again!");
            }
        }

        /// <summary>
        /// Displays both Light and Temp Alarm Systems
        /// </summary>
        /// <param name="alarmType">Type of Alarm the user wants to run</param>
        private static void DisplayBothAlarms(string alarmType)
        {
            var (time, lightLowerThreshold, lightUpperThreshold, tempLowerThreshold, tempUpperThreshold) = GetMultipleAlarmSystemParams(alarmType);
            DisplayConsoleUI("Light and Temp Alarm System Active");
            int count = 1;
            double ambientTemp = ConvertCtoF(myFinch.getTemperature());
            int ambientLight = myFinch.getLeftLightSensor();
            Console.WriteLine();
            Console.WriteLine($"Lower Light Threshold = {lightLowerThreshold}");
            Console.WriteLine($"Upper Light Threshold = {lightUpperThreshold}");
            Console.WriteLine($"Ambient Light Reading = {ambientLight}");
            Console.WriteLine();
            Console.WriteLine($"Lower Temp Threshold = {tempLowerThreshold}");
            Console.WriteLine($"Upper Temp Threshold = {tempUpperThreshold}");
            Console.WriteLine($"Ambient Temp Reading = {ambientTemp:F2}");
            Console.WriteLine();
            Console.WriteLine();
            if (ambientLight > lightLowerThreshold && ambientLight < lightUpperThreshold && ambientTemp > tempLowerThreshold && ambientTemp < tempUpperThreshold && count <= time)
            {
                while (true)
                {
                    double currentTempReading = ConvertCtoF(myFinch.getTemperature());
                    int currentLightReading = myFinch.getLeftLightSensor();
                    Console.SetCursorPosition(0, 15);
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
                    Console.Write($"Current Light Sensor Value = {currentLightReading}");
                    Console.SetCursorPosition(0, 16);
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
                    Console.Write($"Current Temp Sensor Value = {currentTempReading:F2}");
                    myFinch.wait(1000);
                    count++;
                    if (currentLightReading < lightLowerThreshold || currentLightReading > lightUpperThreshold || currentTempReading < tempLowerThreshold || currentTempReading > tempUpperThreshold)
                    {
                        DisplayConsoleUI("Alarm Triggered!!!!!");
                        Console.WriteLine();
                        Console.WriteLine($"Current Light Reading = {currentLightReading}");
                        Console.WriteLine($"Current Temp Reading = {currentTempReading}");
                        Console.WriteLine("Threshold's exceeded. Alarm Triggered");
                        myFinch.setLED(255, 0, 0);
                        int buzzer = 5;
                        for (int i = 0; i < buzzer; i++)
                        {
                            myFinch.noteOn(2000);
                            myFinch.wait(500);
                            myFinch.noteOff();
                            myFinch.wait(500);
                        }
                        myFinch.setLED(0, 0, 0);
                        myFinch.noteOff();
                        break;
                    }
                    else if (count >= time)
                    {
                        DisplayConsoleUI("No Alarm Trigged");
                        Console.WriteLine("Alarm System has timed out. No events detected!");
                        myFinch.setLED(0, 255, 0);
                        myFinch.wait(5000);
                        myFinch.setLED(0, 0, 0);
                    }
                }
            }
            else
            {
                Console.WriteLine("Sorry, but the ambient Light or Temp reading is already either lower or higher than the specified Thresholds. Please try again!");
            }
        }


        #endregion

        #region User Control

        /// <summary>
        /// Gets the default User Control Parameters
        /// </summary>
        /// <returns>Returns a tuple with the default User Control Parameters</returns>
        private static (int wheelSpeed, int r, int g, int b, int hertz) GetUserControlParams()
        {
            DisplayConsoleUI("User Control Parameters");
            Console.ForegroundColor = ConsoleColor.Green;
            int wheelSpeed = IsValidInt("Please enter a speed for the finch < 1=(slow) - 255=(Max Speed) >: ", 1, 255);
            int r = IsValidInt("Please enter the brightness for the RED LED < 0=(Off) - 255=(Full Brightness) >: ", 0, 255);
            int g = IsValidInt("Please enter the brightness for the GREEN LED < 0=(Off) - 255= (Full Brightness) >: ", 0, 255);
            int b = IsValidInt("Please enter the brightness for the BLUE LED < 0=(Off) - 255=(Full Brightness) >: ", 0, 255);
            int hertz = IsValidInt("Please enter the frequency for the Buzzer < 0=(Off) - 22000=(High) >: ", 0, 22000);
            return (wheelSpeed, r, g, b, hertz);
        }

        /// <summary>
        /// Displays the User Control Commands for the user to create their own program
        /// </summary>
        /// <param name="commands">List with tuples inside of the user command and time to run the command</param>
        /// <returns>Returns back a List with tuples of each command and time to run the command</returns>
        private static List<Tuple<UserControlCommands, int>> DisplayUserControlCommands(List<Tuple<UserControlCommands, int>> commands)
        {
            DisplayUserCommandsInstructions();
            UserControlCommands command = UserControlCommands.None;
            while (command != UserControlCommands.Done)
            {
                DisplayConsoleUI("User Control Commands");
                int userResponse = IsValidMenuOption("User Control Commands", @"
             User Control Command List

   1. Move Forward
   2. Move Backward
   3. Stop Motors
   4. Turn Right
   5. Turn Left
   6. LED On
   7. LED Off
   8. Buzzer On
   9. Buzzer Off
  10. Spin
  11. Read Temp
  12. Read Light Level
  13. Play Star Wars
  14. Done




  Option (1-14): ", 1, 14);
                Enum.TryParse(Convert.ToString(userResponse), out command);
                int time = IsValidInt("Enter runtime in Seconds. When selecting Option 14 (Done) enter 0. (0-1000): ", 0, 1000);
                if (command != UserControlCommands.None)
                {
                    int counter = 1;
                    commands.Add( new Tuple<UserControlCommands, int>(command, time));
                    DisplayConsoleUI("Command Added", commands.Count + 9);
                    Console.WriteLine();
                    foreach (Tuple<UserControlCommands, int> userCommand in commands)
                    {
                        UserControlCommands userCommands = userCommand.Item1;
                        time = userCommand.Item2;
                        Console.WriteLine($"Command #{counter}: {userCommands} with a runtime of {time} seconds");
                        counter++;
                    }
                    DisplayContinuePrompt();
                }
            }
            return commands;
        }

        /// <summary>
        /// Displays the list of User Commands and the time to run each command input by the user
        /// </summary>
        /// <param name="commands">List with tuples inside of the user command and time to run the command</param>
        private static void ViewUserControlCommandsInput(List<Tuple<UserControlCommands, int>> commands)
        {
            if (commands.Count < 20)
            {
                DisplayConsoleUI("Commands Entered");
            }
            else
            {
                DisplayConsoleUI("Commands Entered", (commands.Count) + 8);
            }

            int counter = 1;
            if (commands.Count == 0)
            {
                Console.WriteLine("There are no commands here. Please go back and enter 2 to insert some commands first!");
            }
            else
            {
                Console.WriteLine();
                foreach (Tuple<UserControlCommands, int> command in commands)
                {
                    UserControlCommands userCommand = command.Item1;
                    int time = command.Item2;
                    Console.WriteLine($"Command #{counter}: {userCommand} with a runtime of {time} seconds");
                    counter++;
                }
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Executes the user control commands input by the user
        /// </summary>
        /// <param name="commands">List with tuples inside of the user command and time to run the command</param>
        /// <param name="userControlParams">A tuple with the default User Control Parameters</param>
        private static void ExecuteUserControlCommands(List<Tuple<UserControlCommands, int>> commands, (int wheelSpeed, int r, int g, int b, int hertz) userControlParams)
        {
            DisplayConsoleUI("Execute Commands");

            int motorSpeed = userControlParams.wheelSpeed;
            int r = userControlParams.r;
            int g = userControlParams.g;
            int b = userControlParams.b;
            int hertz = userControlParams.hertz;


            foreach (Tuple<UserControlCommands, int> command in commands)
            {
                UserControlCommands userCommand = command.Item1;
                int time = command.Item2 * 1000;

                switch (command.Item1)
                {
                    case UserControlCommands.None:
                        break;
                    case UserControlCommands.MoveForward:
                        myFinch.setMotors(motorSpeed, motorSpeed);
                        myFinch.wait(time);
                        break;
                    case UserControlCommands.MoveBackward:
                        myFinch.setMotors(-motorSpeed, -motorSpeed);
                        myFinch.wait(time);
                        break;
                    case UserControlCommands.StopMotors:
                        myFinch.setMotors(0, 0);
                        myFinch.wait(time);
                        break;
                    case UserControlCommands.TurnRight:
                        myFinch.setMotors(0, motorSpeed);
                        myFinch.wait(time);
                        break;
                    case UserControlCommands.TurnLeft:
                        myFinch.setMotors(motorSpeed, 0);
                        myFinch.wait(time);
                        break;
                    case UserControlCommands.LEDOn:
                        myFinch.setLED(r, g, b);
                        myFinch.wait(time);
                        break;
                    case UserControlCommands.LEDOff:
                        myFinch.setLED(0, 0, 0);
                        myFinch.wait(time);
                        break;
                    case UserControlCommands.BuzzerOn:
                        myFinch.noteOn(hertz);
                        myFinch.wait(time);
                        break;
                    case UserControlCommands.BuzzerOff:
                        myFinch.noteOff();
                        myFinch.wait(time);
                        break;
                    case UserControlCommands.Spin:
                        myFinch.setMotors(motorSpeed, 0);
                        myFinch.wait(time);
                        myFinch.setMotors(0, 0);
                        break;
                    case UserControlCommands.Temp:
                        Console.WriteLine();
                        Console.WriteLine("Current Temperature Reading: {0}°f",ConvertCtoF(myFinch.getTemperature()));
                        myFinch.wait(time);
                        break;
                    case UserControlCommands.Light:
                        Console.WriteLine();
                        int[] lightLevel = myFinch.getLightSensors();
                        Console.WriteLine($"Current Light Level: Left Light Sensor: {lightLevel[0]} Right Light Sensor: {lightLevel[1]}");
                        myFinch.wait(time);
                        break;
                    case UserControlCommands.StarWars:
                        BuzzerStarWars();
                        myFinch.wait(time);
                        break;
                    case UserControlCommands.Done:
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Program has completed successfully! Clear the command list to start a new program or run it again!");
            myFinch.disConnect();
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Clears the list of the input user commands
        /// </summary>
        /// <param name="commands">List with tuples inside of the user command and time to run the command</param>
        /// <returns>Returns the List with tuples inside of the user commands and the time to run each command. Empty if cleared</returns>
        private static List<Tuple<UserControlCommands, int>> ClearUserControlCommandList(List<Tuple<UserControlCommands, int>> commands)
        {
            DisplayConsoleUI("Clear User Control Commands");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Are you sure you wish to clear the command list [Y]es or [N]o: ");
            string userResponse = Console.ReadLine().ToUpper().Trim(); 
            if (userResponse == "Y" || userResponse == "YES")
            {
                commands.Clear();
                Console.WriteLine();
                Console.WriteLine("Ok, all commands have been deleted. Please input new commands!");
                DisplayContinuePrompt();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Ok, not deleting any of your commands");
                DisplayContinuePrompt();
            }

            return commands;
        }

        #endregion

        #endregion

        #region Finch Robot Connection/Disconnect Logic
        /// <summary>
        /// Connect to the Finch Robot
        /// </summary>
        private static void DisplayConnectFinch()
        {
            DisplayConsoleUI("Let's get connected");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Let's get your Finch connected before proceeding further.");
            Console.WriteLine();
            Console.SetCursorPosition(1, 8);
            Console.WriteLine("Make sure to plug in the USB cable to the Computer and then to the Finch before proceeding!");
            DisplayContinuePrompt();
            if (!ConnectFinch())
            {
                DisplayConsoleUI("Error Cannot Connect");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Sorry I am unable to connect to the Finch at this time.");
                Console.WriteLine();
                Console.SetCursorPosition(1, 7);
                Console.WriteLine("Please check that the cable is connected properly and that Windows see's your device");
                Console.SetCursorPosition(1, 9);
                Console.WriteLine("Unfortunately, I have to Quit the application since the Finch cannot be connected after the fact.");
                Console.SetCursorPosition(1,11);
                Console.WriteLine("Please try again.");
                DisplayContinuePrompt();
                Environment.Exit(0);
            }
            else
            {
                DisplayConsoleUI("Finch is connected");
                Console.ForegroundColor = ConsoleColor.Cyan;
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Let's get your Finch disconnected. I will disconnect your Finch and Validate it is disconnected next.");
            DisplayContinuePrompt();
            if (DisconnectFinch())
            {
                DisplayConsoleUI("Finch is disconnected");
                Console.ForegroundColor = ConsoleColor.Cyan;
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

        #region User Authentication

        private static int DisplayUserAuth()
        {
            int userResponse = IsValidMenuOption("     User Authentication", @"
                                             





                                       

                                                   1. Login

                                                   2. Register










  Option (1-2): ", 1, 2);
            return userResponse;
        }

        private static void GetUserAuthMenuOption(int option)
        {
            Login menuChoice = (Login)option;

            switch (menuChoice)
            {
                case Login.Login:
                    UserLogin();
                    break;
                case Login.Register:
                    Register();
                    break;
                default:
                    break;
            }
        }

        private static void UserLogin()
        {
            DisplayConsoleUI("Login");

            Console.Write("Username: ");
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine(); 


            bool IsValid = IsUserValid(userName, password);
            if (IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private static bool IsUserValid(string userName, string password)
        {
            string dataPath = "Data\\userdata.txt";

            string[] userData = File.ReadAllLines(dataPath);

            foreach (string info in userData)
            {
                string[] userInfo = info.Split(':');
                string storedUserName = userInfo[1];
                string passwordHash = userInfo[3];
            }

            if (userName.Equals(storedUserName))
            {

            }

        }

        /// <summary>
        /// Registers the User to the System 
        /// Asks the user for a username and checks it against a regex
        /// Asks the user for a password and secures it with PBKDF2 and a random SALT
        /// If the username meets the requirements, the username and password are stored in the userData file
        /// </summary>
        private static void Register()
        {
            DisplayConsoleUI("        Register");

            string dataPath = "Data\\userData.txt";
            string newLine = "\n";

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Username must be between 1-15 characters");
            Console.WriteLine("First character must start with a letter");
            Console.WriteLine("Can contain letters, numbers, and special chars (_ - .)");
            Console.WriteLine("Cannot end with (_ - .)");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
           
            Console.Write("Username: ");
            string userName = Console.ReadLine();
            Regex regex = new Regex(@"^(?=[a-zA-Z])[-\w.]{0,15}([a-zA-Z\d]|(?<![-.])_)$");
            bool IsValid = regex.IsMatch(userName);

            Console.Write("Password: ");
            string password = Console.ReadLine();

            var passwordSalt = CryptoService.GenerateSalt();
            var passwordHash = CryptoService.ComputeHash(password, passwordSalt);

            if (IsValid)
            {
                File.AppendAllText(dataPath, "Username:" + userName + newLine);
                File.AppendAllText(dataPath, "Password:" + Convert.ToBase64String(passwordHash));
            }
            else
            {
                Console.WriteLine("Sorry, that username is not valid. ");
                Console.WriteLine("First character must be a letter. Remaining characters can be letters, numbers, _, -, and .");
                Console.WriteLine("Cannot end with an _, -, . and must be between 1 and 15 characters");
            }
            Console.ReadLine(); 
        }

        #endregion

        #region Instruction Screens

        /// <summary>
        /// Displays the Talent Show Menu Instructions
        /// </summary>
        private static void DisplayTalentShowMenuInstructions()
        {
            DisplayConsoleUI("Talent Show Menu Instructions");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
     Welcome to the Talent Show portion of the program!
     On the next screen, you will be prompted to select what talent show control you want to use.

     1. LED Menu allows you to control the Finch's LED various ways
     2. The Buzzer Menu allows you to control the Finch's Buzzer various ways
     3. The Wheels Menu allows you to control the Finch's movement various ways
     I hope you enjoy this portion of the program!
");
            DisplayContinuePrompt();
        }

        /// <summary>
        /// LED Talent Show Menu Instructions/Description
        /// </summary>
        private static void DisplayLEDMenuInstructions()
        {
            DisplayConsoleUI("LED Menu Instructions");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
     The LED Menu has several different components. 

     1. You can turn the LED On for a specified color (Mix between red, green, and blue)
     2. You can make the LED Blink for a specified amount of time the LED is ON and # of Blinks
     3. You can have multiple LED colors blink for varying times (predefined time and pattern)
     4. You can have the LED Pulse all colors. Goes from off to red, red to off, off to green, green to off,
        off to blue, and finally blue to off. Cycles through these colors once (predefined timing)

     This menu is to highlight some of the Finch's abilities regarding the LED Light
");
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Displays the Buzzer Talent Show Menu Instructions
        /// </summary>
        private static void DisplayBuzzerMenuInstructions()
        {
            DisplayConsoleUI("Buzzer Menu Instructions");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
     The Buzzer Menu has several different components.
     
     1. You can turn the Buzzer on for a specified amount of time
     2. You can play the Star Wars Theme song
     3. You can play the Happy Birthday song
");
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Displays the Wheel Menu Instructions
        /// </summary>
        private static void DisplayWheelMenuInstructions()
        {
            DisplayConsoleUI("Wheel Menu Instructions");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
     The Wheel Menu has several different components.
    
     1. You can move the Finch forward for a specified amount of time
     2. You can move the Finch backward for a specified amount of time
     3. You can turn the Finch right 90 degrees (prespecified)
     4. You can turn the Finch left 90 degrees (prespecified)

");
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Displays the Data Recorder Menu Instructions
        /// </summary>
        private static void DisplayDataRecorderMenuInstructions()
        {
            DisplayConsoleUI("Data Recorder Menu Instructions");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
     The Data Recorder Menu has several components.
     1. The Light Sensor Control allows you to collect RAW or Averaged Light Sensor data at 
        specified intervals and for a specified amount of data points.
     2. The Temp Sensor Control allows you to collect RAW temp readings in either 
        Celcius or Farenheight.
     I hope you enjoy this program!
");
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Displays the Alarm System Instructions
        /// </summary>
        private static void DisplayAlarmSystemInstructions()
        {
            DisplayConsoleUI("Alarm System Instructions");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
     The Alarm System Menu has several different components.

     1. The Light Sensor alarm allows you to monitor a lower and upper limit of the light sensor
        and trigger an alarm when either of those thresholds are crossed.
     2. The Temp Sensor alarm allows you to monitor a lower and upper limit of the temp sensor
        and trigger an alarm when either of those thresholds are crossed.
     3. You can also monitor both the Light Sensor and Temp sensor simultaneously with separate
        lower and upper limits for each sensor to monitor (4 in total). When any threshold is 
        crossed, an alarm is triggered.");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Displays the User Instructions on how to program the Finch
        /// </summary>
        private static void DisplayUserCommandsInstructions()
        {
            DisplayConsoleUI("User Command Instructions");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"

     On the next screen, you will be presented with a list of commands you may enter to control your finch.
     Please read the following instructions carefully for optimum results.
  1. Select the numeric (number) value of the command you wish to add to the list and press <Enter>.
  2. Please input the time (in seconds) you wish for the command to run and then press <Enter>.
  3. Please note, that if you run the Motors, LED, or Buzzer and do not specify the proper off command, 
     they will run indefinitely. 
  4. I advise you specify an Off condition to prevent the Finch from running indefinitely. If you en up with an
     infinite loop please hit <CTRL> + C
  5. For instances that you are turning off the LED, Buzzer, or Motor, you should set the run time seconds to 0 
     so you do not need to wait until the next command is executed. If you wish to pause, after turning something
     off, then specify the time as required before the next command runs.
  6. You may enter as many commands as you wish. 
  7. When you are finished, you must select option 14 (Done) and enter any time value you wish. Preferrably 0. 
  8. After you have entered the commands, you will be taken back to the Control Screen, where you may view the
     full list of commands in order, clear the list and start over, or execute the commands and begin your program. 
  I hope you enjoy this program and have fun playing around with the Finch. Thank you!


");
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Displays the Main User Control instructions
        /// </summary>
        private static void DisplayUserControlInstructions()
        {
            DisplayConsoleUI("User Control Instructions");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"

     Welcome to the User Control portion of the Finch Robot program!
     These instructions will guide you on how to properly utilize this section of the program.

  1. On the next screen, you will be asked to input some base parameters for the Finch Robot.
  2. Please enter the speed of the robot. Values range from 0-255. 0 being dead stopped, and 255 
     being max speed.
  3. Next you will enter the LED value for the RED LED. 0 is off, and 255 is Max Brightness.
  4. Next you will enter the LED value for the GREEN LED. Same as above.
  5. Next you will enter the LED value for the BLUE LED. Same as above.
  6. Next you will enter the frequency(tone) of the buzzer. 0 is off, and 22000 is high pitched.
  7. After this, you will be taken to the User Control Panel, where you can enter commands,
     view the commands, execute the commands, clear the commands, or re-enter the default params above.
  8. I hope you enjoy this program!
");
            DisplayContinuePrompt();
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
        private static int IsValidInt(string prompt, int min = 0, int max = 1000)
        {
            bool IsValidInt = false;
            int x = 0;
            const int MAX_TRIES = 3;
            int i = 0;
            while (!IsValidInt && i <= MAX_TRIES)
            {
                Console.Write($"{prompt}");
                IsValidInt = int.TryParse(Console.ReadLine(), out x);
                if (!IsValidInt || x < min || x > max)
                {
                    Console.WriteLine("Sorry, that response isn't valid. Please try again!");
                    IsValidInt = false;
                    i++;
                    if (i > MAX_TRIES)
                    {
                        Console.WriteLine("Sorry, but you have tried too many times and failed to put in the correct data. I am exiting the program. Please try again later.");
                        DisplayContinuePrompt();
                        Quit();

                    }
                }
            }
            return x;
        }

        /// <summary>
        /// Checks to see if the user response is a valid double
        /// </summary>
        /// <param name="prompt">Prompts the user with a question</param>
        /// <param name="min">Lower bound of accepted range</param>
        /// <param name="max">Upper Bound of accepted range</param>
        /// <returns>returns user input double</returns>
        private static double IsValidDouble(string prompt, double min = 0, double max = 1000)
        {
            bool IsValidDouble = false;
            double x = 0;
            const int MAX_TRIES = 3;
            int i = 0;
            while (!IsValidDouble && i <= MAX_TRIES)
            {
                Console.Write($"{prompt}");
                IsValidDouble = double.TryParse(Console.ReadLine(), out x);
                if (!IsValidDouble || x < min || x > max)
                {
                    Console.WriteLine("Sorry, that response isn't valid. Please try again!");
                    IsValidDouble = false;
                    i++;
                    if (i > MAX_TRIES)
                    {
                        Console.WriteLine("Sorry, but you have tried too many times and failed to put in the correct data. I am exiting the program. Please try again later.");
                        DisplayContinuePrompt();
                        Quit();
                    }
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
        private static int IsValidMenuOption(string screenName, string prompt, int min, int max)
        {
            bool isValidMenuChoice = false;
            int userResponse = 0;
            const int MAX_TRIES = 3;
            int i = 0;
            while (!isValidMenuChoice && i <= MAX_TRIES)
            {
                DisplayConsoleUI($"{screenName}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($@"{prompt}");
                isValidMenuChoice = int.TryParse(Console.ReadLine(), out userResponse);
                if (!isValidMenuChoice || userResponse < min || userResponse > max)
                {
                    DisplayConsoleUI("Invalid Option");
                    Console.WriteLine("Sorry, that is not a valid option. Please try again!");
                    isValidMenuChoice = false;
                    i++;
                    if (i > MAX_TRIES)
                    {
                        Console.WriteLine("Sorry, but you have tried too many times and failed to put in the correct data. I am exiting the program. Please try again later.");
                        DisplayContinuePrompt();
                        Quit();
                    }
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
        private static void Quit()
        {
            Environment.Exit(0);
        }

        #endregion
    }
}