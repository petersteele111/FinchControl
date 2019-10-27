# FinchControl

## Welcome
Hello everyone! This is my project for the Finch Robot used amoung many schools from Elementary School to Colleges to teach student about programming.

I was tasked with creating an application that performed various functions for the Finch Robot in C#. Now as some of you may know, C# is not officially supported anymore. Luckily, I have the FinchAPI and HID .dll files in my project that allow you to use the Finch robot for C#. There are only 2 .dll files that need to be referenced in order for this to work. I will also upload the .pdf that includes a breakdown of the methods available, along with what data types they use and return. If you wish to use the Finch to teach programming or to just play around with, know that the Finch 2.0 is not supported by these files. You must use the original Finch robot in order to program it in C#. So enough about that, lets look at what we have here.

## Finch Control Application
So I have created a custom Console App that allows users to interact with the Finch and play around with its various sensors and inputs. I do not have anything regarding obstacle detection, as it was not required for the project, but can be easily implemented. **Note** *The obstacle detection on the Finch is finicky at best. Sometimes it works great, other times it fails completely. Refelective surfaces are a must*. Ok, so what can you do with this program? Glad you asked. I will break each section down below.

## Connect to the Finch
So the first section you will come across in the program is the Connect the Finch option. This option will attempt to connect to the Finch Robot because a valid connection is needed in order for many of the following functions in the program to work. I have it set to attempt to connect *3* times before gracefully failing and exiting the program after some user feedback. If for some reason you connected the Finch Robot prior in the program, and then disconnected it, you will need to restart the program. No matter how many times you attempt to reconnect, it will fail, and this is a limitation of the HID.dll and the Finch, not my program. 

## Talent Show
So the Talent Show section covers various aspects of the Finch Robot and its sensors. I have an LED control module that lets you set the custom color of the LED and the time it is turned on for. I also have a section for Blinking the LED, Pulsing the LED through the three primary colors, and flashing the 3 primary colors for varying lengths.
 
 * LED Control
   * LED On
   * LED Blink
   * Multiple LED On
   * LED Pulse

The next section in the Talen Show Menu is the Buzzer Control. This will allow you to set the frequency in hertz of the buzzer, along with a time to run in seconds *all seconds are converted to milliseconds since that is what the Finch Robot uses for its wait() method*. After that you can select to play the Star Wars theme song (short clip) and the Happy Birthday song.

* Buzzer Control
  * Buzzer On
  * Buzzer Star Wars
  * Buzzer Happy Birthday

The last section of the Talen Show is the Wheels Control. This section allows you to move the Finch Robot forward, backwards, left(90°), and right(90°). Left and Right are both predefined values for time and wheel speed. Moving forwards and backwards is done by the user input parameters.

* Wheels Control
  * Forward
  * Backward
  * Right
  * Left
  
The Talent Show section is meant to just highlight some of the things that you can do with the Finch Robot and meets all the requirements of my project.

## Data Recorder
So the next section in my program is the Data Recorder Menu. This module will record light or temperature data at user specified intervals with user specified data points. Simply input the interval of time (in seconds) that you wish to collect the data, and specify how many data points you wish to collect. The program will calculate the approximate time it will take to complete the recording of data and display that value to the screen in minutes. After the collection of the data has completed, the data will be posted to the screen in order for you to review. This is done in a grid system and is easy to see and interpret. I have 3 sections in this module that allows you to record data. The first one is simply a light sensor reading from both the left and the right sensors. The next one will average the light sensor readings between the two sensors and display one value for each data point. Finally, you can collect temperature data in either celcius or farenheight and display that to the screen as well.

* Data Recorder
  * Light Sensor Control
    * Get Light Sensor Data
    * Get Light Sensor Data Averaged
  * Temp Sensor Control
    * Get Temperature Sensor Data
    
As you can see, this section is a bit more useful for using the Finch to collect Light and Temp data in its environment. The next section will expand upon this and make this feature even more useful.

## Alarm System
So with the previous section, I was able to collect a user defined amount of data points at specified intervals, and to expand upon that, we will now monitor that data for an Alarm System. To do this, we no longer collect x amount of data points, but instead run the Alarm System for a user defined amount of time, and monitor a lower and upper threshold for each type of sensor. If either the lower or upper threshholds are crossed, and alarm is triggered activating the Finch's LED to Red and beeping 5 times, 1 second at a time. I have also built in the ability to monitor both the light sensor and temp sensor at the same time, and monitoring all 4 of the thresholds at once. 

* Alarm System
  * Light Sensor Alarm
  * Temp Sensor Alarm
  * Both Light and Temp Alarm
  
As you can see, this makes recording data from the Finch somewhat userful, albeit only really in an academic sense. The last section is the peace de restiance of this program and culminates all of the above features into a handy module.

## User Programming
So the last module I have included with this program is a User Programming module. This module allows you to key in various functions of the Finch Robot and daisy chain them together to create your own functionality! The main menu of this section allows you to do many different things like:

* Input Parameters
* Insert Commands
* View Commands
* Execute Commands
* Clear Commands (Reset)

### Default Parameters for User Programming
Upong entering the User Programming module, you will be greeted with a simple explanation/instruction screen on how to fill out the proceeding user defined parameters. The default parameters needed for the Finch to work in this module are as follows:

* Wheel Speed
* Red LED value
* Green LED value
* Blue LED value
* Tone of Buzzer (hertz)

These can be changed at anytime, but upon first entry into the program, you will be required to input them before moving forward. This is to prevent any unintended behavior when executing your command list.

You also have access to over 13 different functions of the Finch Robot to work with when inserting your commands. They are as follows:

* Insert Commands
  * Move Forward 
  * Move Backward
  * Stop Motors
  * Turn Right
  * Turn Left
  * LED On
  * LED Off
  * Buzzer On
  * Buzzer Off
  * Spin
  * Read Temp
  * Read Light Level
  * Play Star Wars
  
These commands are entered via numerical values i.e. 1-13 and then allow you to specify a runtime in seconds for each command. For the commands that turn on a feature, you can specify any time value you wish. For commands that turn off a feature, typically you will specify 0 to move immediately to the next command in your list. One reason I can see not doing this is turning the LED on and Off to blink ot or the Buzzer on or off as well. Specifying a time for the off feature when utilizing these features will make a true beep or blinking pattern that is equal between the on state and off state. 

You may pick as many commands as you want, in any order that you want, to create a custom program to execute. This allows those without programming experience, to easily program and control the Finch Robot. After you have succesfully input all of your commands, you may view a list of the commands you entered, you may execute the list of commands enetered, setup the default parameters (Wheel speed, Red LED, Green LED, Blue LED, and Tone of the buzzer in hertz), or clear the list of commands and start over. Speaking of default parameters lets look at that next.

As you can see, this is probably the most useful feature of this program. It allows you to create a simple control logic for the Finch Robot with no programming experience required or programming required. 
## Disconnect the Finch
So in this section, you simply disconnect the Finch Robot in order to cleanly exit. I have built in safeguards to prevent the program from exiting before the finch has properly been disconnected so you should honestly never really *NEED* to manually disconnect. This feature was added primarly to meet the requirements of the program as specified.

## Conclusion
So that about wraps this program up. It is easy to use, includes instruction screens with great detail on what data and inputs are expected to make everything work correctly, and can be run over and over until you are ready to quit the application. I hope you all enjoy this program and if you have any questions or concerns, please email them to info@pbsteele.com. I had great fun building this application out and learning the syntax of C#. While I am not new to programming, I am not primarily a C# developer so I needed to learn the syntax. 
