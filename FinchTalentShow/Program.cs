using FinchAPI;
using System;
using System.Linq;

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
            Back
        }

        public enum LightSensorMenu : byte
        {
            Light = 1,
            Average,
            Back
        }

        public enum TempSensorMenu : byte
        {
            Temp = 1,
            Back
        }

        public enum AlarmSystemMenu : byte
        {
            Light = 1,
            Temp,
            Both,
            Back
        }

        public enum LightAlarmMenu : byte
        {
            Infinite = 1,
            Timed,
            Back
        }

        public enum TempAlarmMenu : byte
        {
            Infinite = 1,
            Timed,
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
            Console.WriteLine("So our time has come to an end. I enjoyed working with your Finch today!");
            Console.WriteLine();
            Console.SetCursorPosition(1, 7);
            Console.WriteLine("Thank you for using this program. Until next time!");
            DisplayContinuePrompt();
            quit();
        }

        #endregion

        #region Menu's of Application

        #region Main Menu

        
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

  4. Alarm System

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
                    getDataRecorderMenuOption(DisplayDataRecorderMenu());
                    break;
                case Menu.AlarmSystem:
                    getDisplayAlarmSystemMenuOption(DisplayAlarmSystemMenu());
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

        #endregion

        #region TalentShow Menus

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

        #region DataRecorder Menus

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

  3. Back














  Option (1-3): ", 1, 3);
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
                    getLightSensorMenuOption(DisplayLightSensorMenu());
                    break;
                case DataRedcorderMenu.Temp:
                    getTempSensorMenuOption(DisplayTempSensorMenu());
                    break;
                case DataRedcorderMenu.Back:
                    DisplayDataRecorderMenu();
                    break;
                default:
                    DisplayConsoleUI("Invlaid Response");
                    Console.WriteLine("Sorry I don't understand your response, please try again.");
                    DisplayContinuePrompt();
                    break;
            }
        }

        /// <summary>
        /// Displays the Light Sensor Menu
        /// </summary>
        /// <returns>Returns the user option for the menu</returns>
        private static int DisplayLightSensorMenu()
        {
            int userResponse = isValidMenuOption("Light Sensor Menu", @"
             Light Sensor

  1. Get Light Sensor Data

  2. Get Light Sensor Data Averaged

  3. Back














  Option (1-3): ", 1, 3);
            return userResponse;
        }

        private static void getLightSensorMenuOption(int option)
        {
            LightSensorMenu menuOption = (LightSensorMenu)option;

            switch (menuOption)
            {
                case LightSensorMenu.Light:
                    DisplayConnectFinch();
                    DisplayLightSensorData(getLightSensorData());
                    break;
                case LightSensorMenu.Average:
                    DisplayConnectFinch();
                    DisplayLightSensorDataAverage(getLightSensorData());
                    break;
                case LightSensorMenu.Back:
                    getDataRecorderMenuOption(DisplayDataRecorderMenu());
                    break;
                default:
                    DisplayConsoleUI("Invlaid Response");
                    Console.WriteLine("Sorry I don't understand your response, please try again.");
                    DisplayContinuePrompt();
                    break;
            }
        }

        private static int DisplayTempSensorMenu()
        {
            int userResponse = isValidMenuOption("Temperature Sensor Menu", @"
             Temperature Sensor

  1. Get Temperature Sensor Data

  2. Back
















  options (1-2): ", 1, 2);
            return userResponse;
        }

        private static void getTempSensorMenuOption(int option)
        {
            TempSensorMenu menuChoice = (TempSensorMenu)option;

            switch (menuChoice)
            {
                case TempSensorMenu.Temp:
                    DisplayConnectFinch();
                    DisplayTempSensorData();
                    break;
                case TempSensorMenu.Back:
                    DisplayDataRecorderMenu();
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
            int userResponse = isValidMenuOption("Alarm System Menu", @"

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
        private static void getDisplayAlarmSystemMenuOption(int option)
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
                    getMenuOption(DisplayMainMenu());
                    break;
                default:
                    DisplayConsoleUI("Invlaid Response");
                    Console.WriteLine("Sorry I don't understand your response, please try again.");
                    DisplayContinuePrompt();
                    break;
            }
        }

        #endregion

        #endregion

        #region Control Systems

        #region TalentShow Control

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

        #endregion

        #region DataRecorder Control

        /// <summary>
        /// Get the Light Sensor Data Parameters
        /// </summary>
        /// <returns>Returns a tuple of the Light Sensor Parameters</returns>
        private static (int time, int dataPoints) getLightSensorParams()
        {
            DisplayConsoleUI("Get Light Sensor Parameters");
            Console.ForegroundColor = ConsoleColor.Green;
            int time = isValidInt("Please enter the Frequency you wish to collect Sensor Data (seconds): ", 1, 100000);
            int dataPoints = isValidInt("Please enter the amount of Data Points you wish to collect: ", 1, 100000);
            Console.WriteLine();
            Console.WriteLine($"Gathering the Data. Time until completion: {(double)(time * dataPoints) / 60:F1} mins");
            return (time, dataPoints);
        }

        /// <summary>
        /// Get the Light Sensor Data
        /// </summary>
        /// <returns>Returns Multidimensional Array of Left and Right Light Sensor Readings</returns>
        private static int[][] getLightSensorData()
        {
            var values = getLightSensorParams();

            int[][] sensorData = new int[values.dataPoints][];
            for (int i = 0; i < values.dataPoints; i++)
            {
                sensorData[i] = myFinch.getLightSensors();
                myFinch.wait(values.time * 1000);
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
        private static (int time, int dataPoints) getTempSensorParams()
        {
            DisplayConsoleUI("Get Temp Sensor Parameters");
            Console.ForegroundColor = ConsoleColor.Green;
            int time = isValidInt("Please enter the Frequency you wish to collect Sensor Data (seconds): ", 1, 100000);
            int dataPoints = isValidInt("Please enter the amount of Data Points you wish to collect: ", 1, 100000);
            Console.WriteLine();
            Console.WriteLine($"Gathering the Data. Time until completion: {(double)(time * dataPoints) / 60:F1} mins");
            return (time, dataPoints);
        }

        /// <summary>
        /// Gets the Temperature Sensor Data
        /// </summary>
        /// <returns>Returns tuple with double array of sensorData and bool for Farenheight to Celcius</returns>
        private static (double[] sensorData, bool CtoFa) getTempSensorData()
        {
            var values = getTempSensorParams();
            bool CtoFa = CtoF();
            double[] sensorData = new double[values.dataPoints];
            for (int i = 0; i < values.dataPoints; i++)
            {
                sensorData[i] = myFinch.getTemperature();
                myFinch.wait(values.time * 1000);
            }
            if (CtoFa)
            {
                sensorData = convertCtoF(sensorData);
            }
            return (sensorData, CtoFa);
        }

        /// <summary>
        /// Displays the Temperature Sensor Data to the Screen
        /// </summary>
        private static void DisplayTempSensorData()
        {
            var values = getTempSensorData();

            bool CtoF = values.CtoFa;
            string tempFormat;

            if (CtoF)
            {
                tempFormat = "°F";
            }
            else
            {
                tempFormat = "°C";
            }

            DisplayConsoleUI("Temperature Sensor Data", (values.sensorData.Length) + 7);
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("Temperature Sensor Data");
            Console.WriteLine();
            for (int i = 0; i < values.sensorData.Length; i++)
            {
                if (values.sensorData.Length < 9)
                {
                    Console.WriteLine($"Data Point #{i + 1} {values.sensorData[i]:F2}{tempFormat}");
                }
                else
                {
                    while (i < 9)
                    {
                        Console.WriteLine($"Data Point # {i + 1} {values.sensorData[i]:F2}{tempFormat}");
                        i++;
                    }
                    Console.WriteLine($"Data Point #{i + 1} {values.sensorData[i]:F2}{tempFormat}");
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
        private static double[] convertCtoF(double[] sensorData)
        {
            double[] convertedSensorData = new double[sensorData.Length];
            for (int i = 0; i < sensorData.Length; i++)
            {
                convertedSensorData[i] = (sensorData[i] * 1.8) + 32;
            }
            return convertedSensorData;
        }

        #endregion

        #region AlarmSystem Control

        /// <summary>
        /// Get the User parameters for the Alarm System
        /// </summary>
        /// <param name="alarmType">Type of Alarm the user wants to run</param>
        /// <returns>Returns a tuple with the alarm parameters</returns>
        private static (int time, int lightLowerThreshold, int lightUpperThreshold, double tempLowerThreshold, double tempUpperThreshold) getMultipleAlarmSystemParams(string alarmType)
        {
            DisplayConsoleUI("Get Both Light and Temp Alarm System Parameters");
            int lightLowerThreshold = 0;
            int lightUpperThreshold = 0;
            double tempLowerThreshold = 0;
            double tempUpperThreshold = 0;
            int time = isValidInt("Please enter the time (seconds) you wish to run this monitoring system: ", 1, 100000);
            if (alarmType.Equals("light"))
            {
                lightLowerThreshold = isValidInt("Please enter the lower light sensor threshhold (0-255): ", 0, 255);
                lightUpperThreshold = isValidInt("Please enter the upper light sensor threshold (0-255): ", 0, 255);

                Console.WriteLine("Ok, now that you have entered in the thresholds for the alarm system, hit Enter to Activate the alarm!");
                DisplayContinuePrompt();
                return (time, lightLowerThreshold, lightUpperThreshold, tempLowerThreshold, tempUpperThreshold);
            }
            else if (alarmType.Equals("temp"))
            {
                tempLowerThreshold = isValidDouble("Please enter the lower temp sensor threshold (-40.00 - 150.00): ", -40.00, 150.00);
                tempUpperThreshold = isValidDouble("Please eneter the upper temp sensor threshold (-40.00 - 150.00): ", -40.00, 150.00);

                Console.WriteLine("Ok, now that you have entered in the thresholds for the alarm system, hit Enter to Activate the alarm!");
                DisplayContinuePrompt();
                return (time, lightLowerThreshold, lightUpperThreshold, tempLowerThreshold, tempUpperThreshold);
            }
            else
            {
                lightLowerThreshold = isValidInt("Please enter the lower light sensor threshhold (0-255): ", 0, 255);
                lightUpperThreshold = isValidInt("Please enter the upper light sensor threshold (0-255): ", 0, 255);
                tempLowerThreshold = isValidDouble("Please enter the lower temp sensor threshold (-40.00 - 150.00): ", -40.00, 150.00);
                tempUpperThreshold = isValidDouble("Please eneter the upper temp sensor threshold (-40.00 - 150.00): ", -40.00, 150.00);

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
            var values = getMultipleAlarmSystemParams(alarmType);
            DisplayConsoleUI("Light Alarm System Active");
            int count = 1;
            int ambientLight = myFinch.getLeftLightSensor();
            Console.WriteLine();
            Console.WriteLine($"Lower Light Threshold = {values.lightLowerThreshold}");
            Console.WriteLine($"Upper Light Threshold = {values.lightUpperThreshold}");
            Console.WriteLine($"Ambient Light Reading = {ambientLight}");
            Console.WriteLine();
            Console.WriteLine();
            if (ambientLight > values.lightLowerThreshold && ambientLight < values.lightUpperThreshold && count <= values.time)
            {
                while (true)
                {
                    int currentLightReading = myFinch.getLeftLightSensor();
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
                    Console.Write($"Current Light Sensor Value = {currentLightReading}");
                    myFinch.wait(1000);
                    count++;
                    if (currentLightReading < values.lightLowerThreshold || currentLightReading > values.lightUpperThreshold)
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
                    else if (count >= values.time)
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
            var values = getMultipleAlarmSystemParams(alarmType);
            DisplayConsoleUI("Temp Alarm System Active");
            int count = 1;
            double ambientTemp = convertToF(myFinch.getTemperature());
            Console.WriteLine();
            Console.WriteLine($"Lower Temp Threshold = {values.tempLowerThreshold}");
            Console.WriteLine($"Upper Temp Threshold = {values.tempUpperThreshold}");
            Console.WriteLine($"Ambient Temp Reading = {ambientTemp:F2}");
            Console.WriteLine();
            Console.WriteLine();
            if (ambientTemp > values.tempLowerThreshold && ambientTemp < values.tempUpperThreshold && count <= values.time)
            {
                while (true)
                {
                    double currentTempReading = convertToF(myFinch.getTemperature());
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
                    Console.Write($"Current Temp Sensor Value = {currentTempReading:F2}");
                    myFinch.wait(1000);
                    count++;
                    if (currentTempReading < values.tempLowerThreshold || currentTempReading > values.tempUpperThreshold)
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
                    else if (count >= values.time)
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
            var values = getMultipleAlarmSystemParams(alarmType);
            DisplayConsoleUI("Light and Temp Alarm System Active");
            int count = 1;
            double ambientTemp = convertToF(myFinch.getTemperature());
            int ambientLight = myFinch.getLeftLightSensor();
            Console.WriteLine();
            Console.WriteLine($"Lower Light Threshold = {values.lightLowerThreshold}");
            Console.WriteLine($"Upper Light Threshold = {values.lightUpperThreshold}");
            Console.WriteLine($"Ambient Light Reading = {ambientLight}");
            Console.WriteLine();
            Console.WriteLine($"Lower Temp Threshold = {values.tempLowerThreshold}");
            Console.WriteLine($"Upper Temp Threshold = {values.tempUpperThreshold}");
            Console.WriteLine($"Ambient Temp Reading = {ambientTemp:F2}");
            Console.WriteLine();
            Console.WriteLine();
            if (ambientLight > values.lightLowerThreshold && ambientLight < values.lightUpperThreshold && ambientTemp > values.tempLowerThreshold && ambientTemp < values.tempUpperThreshold && count <= values.time)
            {
                while (true)
                {
                    double currentTempReading = convertToF(myFinch.getTemperature());
                    int currentLightReading = myFinch.getLeftLightSensor();
                    Console.SetCursorPosition(0, 15);
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
                    Console.Write($"Current Light Sensor Value = {currentLightReading}");
                    Console.SetCursorPosition(0, 16);
                    Console.Write("\r" + new string(' ', Console.WindowWidth - 1) + "\r");
                    Console.Write($"Current Temp Sensor Value = {currentTempReading:F2}");
                    myFinch.wait(1000);
                    count++;
                    if (currentLightReading < values.lightLowerThreshold || currentLightReading > values.lightUpperThreshold || currentTempReading < values.tempLowerThreshold || currentTempReading > values.tempUpperThreshold)
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
                    else if (count >= values.time)
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

        /// <summary>
        /// Converts the temp from the Finch to Farenheight
        /// </summary>
        /// <param name="temp">The temp in C as returned by the Finch</param>
        /// <returns>Returns the temp converted to Farenheight</returns>
        private static double convertToF(double temp)
        {
            temp = (temp * 1.8) + 32;
            return temp;
        }

        #endregion

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
            const int MAX_TRIES = 3;
            int i = 0;
            while (!isValidInt && i <= MAX_TRIES)
            {
                Console.Write($"{prompt}");
                isValidInt = int.TryParse(Console.ReadLine(), out x);
                if (!isValidInt || x < min || x > max)
                {
                    Console.WriteLine("Sorry, that response isn't valid. Please try again!");
                    isValidInt = false;
                    i++;
                    if (i > MAX_TRIES)
                    {
                        Console.WriteLine("Sorry, but you have tried too many times and failed to put in the correct data. I am exiting the program. Please try again later.");
                        DisplayContinuePrompt();
                        quit();
                        
                    }
                }
            }
            return x;
        }

        private static double isValidDouble(string prompt, double min = 0, double max = 1000)
        {
            bool isValidDouble = false;
            double x = 0;
            const int MAX_TRIES = 3;
            int i = 0;
            while (!isValidDouble && i <= MAX_TRIES)
            {
                Console.Write($"{prompt}");
                isValidDouble = double.TryParse(Console.ReadLine(), out x);
                if (!isValidDouble || x < min || x > max)
                {
                    Console.WriteLine("Sorry, that response isn't valid. Please try again!");
                    isValidDouble = false;
                    i++;
                    if (i > MAX_TRIES)
                    {
                        Console.WriteLine("Sorry, but you have tried too many times and failed to put in the correct data. I am exiting the program. Please try again later.");
                        DisplayContinuePrompt();
                        quit();
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
        private static int isValidMenuOption(string screenName, string prompt, int min, int max)
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
                        quit();
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