using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using NUnit.Framework;

namespace AlarmAutomation
{
    class Program
    {
        private static WindowsDriver driver;

        static void Main(string[] args)
        {
            try
            {
                // Initialize AppiumOptions and set capabilities
                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", "Microsoft.WindowsAlarms_8wekyb3d8bbwe!App");

                // Initialize the WindowsDriver with the Appium options
                driver = new WindowsDriver(new Uri("http://127.0.0.1:4723/wd/hub"), appiumOptions);

                // Run the automation method
                CreateAndVerifyAlarm();

                Console.WriteLine("Automation Completed Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // Clean up and close the driver
                driver?.Quit();
            }

            Console.ReadKey();
        }

        static void CreateAndVerifyAlarm()
        {
            // Open the Alarm section
            var alarmTabButton = driver.FindElement(By.Name("Alarm"));
            alarmTabButton.Click();

            // Click to Add New Alarm
            var addAlarmButton = driver.FindElement(By.Name("Add alarm"));
            addAlarmButton.Click();

            // Set the time (e.g., 9:00 AM)
            var timePicker = driver.FindElement(By.Name("Set time"));
            timePicker.Click();
            var hour = driver.FindElement(By.Name("9"));
            var minute = driver.FindElement(By.Name("00"));
            hour.Click();
            minute.Click();

            // Set Alarm Label
            var labelField = driver.FindElement(By.Name("Label"));
            labelField.SendKeys("Trumpf Metamation - Login Time");

            // Set Repeat alarm option
            var repeatSwitch = driver.FindElement(By.Name("Repeat"));
            repeatSwitch.Click();

            // Set Alarm Sound to Jingle
            var soundDropdown = driver.FindElement(By.Name("Sound"));
            soundDropdown.Click();
            var jingleOption = driver.FindElement(By.Name("Jingle"));
            jingleOption.Click();

            // Save the alarm
            var saveButton = driver.FindElement(By.Name("Save"));
            saveButton.Click();

            // Verify the alarm is saved
            var alarmList = driver.FindElement(By.Name("Alarm List"));
            if (alarmList.Text.Contains("Trumpf Metamation - Login Time"))
            {
                Console.WriteLine("Alarm created and verified successfully.");
            }
            else
            {
                Console.WriteLine("Failed to create alarm.");
            }

            // Delete the alarm
            var deleteButton = driver.FindElement(By.Name("Delete"));
            deleteButton.Click();

            // Validate that the alarm is removed
            if (!alarmList.Text.Contains("Trumpf Metamation - Login Time"))
            {
                Console.WriteLine("Alarm deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete alarm.");
            }
        }
    }
}
