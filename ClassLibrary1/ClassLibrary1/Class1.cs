using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.AutomationElements;
using TechTalk.SpecFlow;
using System;
using System.Threading;
using NUnit.Framework;

[Binding]
public class ClockAppSteps
{
    private Application _app;
    private UIA3Automation _automation;
    private Window _mainWindow;

    [Given(@"I launch the Clock app")]
    public void GivenILaunchTheClockApp()
    {
        var process = System.Diagnostics.Process.Start("explorer.exe", "shell:Appsfolder\\Microsoft.WindowsAlarms_8wekyb3d8bbwe!App");
        if (process == null)
        {
            throw new Exception("Failed to launch the Clock app.");
        }

        Thread.Sleep(5000); // Wait for the app to load

        _app = Application.Attach(process);
        _automation = new UIA3Automation();
        _mainWindow = _app.GetMainWindow(_automation);
    }

    [Given(@"I open the Alarm creation screen")]
    public void GivenIOpenTheAlarmCreationScreen()
    {
        var addButton = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("AddAlarmButton"));
        addButton?.AsButton().Invoke();
        Thread.Sleep(2000);
    }

    [When(@"I set the alarm time to ""(.*)""")]
    public void WhenISetTheAlarmTimeTo(string time)
    {
        // Set the time
        var hourSpinner = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("HourSpinner"))?.AsSpinner();
        var minuteSpinner = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("MinuteSpinner"))?.AsSpinner();
        // Similar logic as before...
    }

    [When(@"I set the alarm name to ""(.*)""")]
    public void WhenISetTheAlarmNameTo(string alarmName)
    {
        var alarmNameField = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("AlarmNameTextBox"));
        alarmNameField?.AsTextBox().Enter(alarmName);
    }

    [When(@"I enable the alarm")]
    public void WhenIEnableTheAlarm()
    {
        var enableButton = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("OnOffToggleButton"));
        enableButton?.AsToggleButton().Toggle();
    }

    [When(@"I select repeat days as Monday to Friday")]
    public void WhenISelectRepeatDaysAsMondayToFriday()
    {
        var repeatDropdown = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("RepeatComboBox"));
        if (repeatDropdown == null)
        {
            throw new Exception("Repeat ComboBox not found.");
        }

        var weekdays = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
        foreach (var day in weekdays)
        {
            var dayOption = repeatDropdown.FindFirstDescendant(cf => cf.ByName(day));
            if (dayOption != null)
            {
                dayOption.AsButton().Invoke();
            }
        }
    }

    [When(@"I disable snooze")]
    public void WhenIDisableSnooze()
    {
        var snoozeDropdown = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("SnoozeComboBox"));
        var snoozeDisabledOption = snoozeDropdown?.FindFirstDescendant(cf => cf.ByName("Disabled"));
        snoozeDisabledOption?.AsButton().Invoke();
    }

    [Then(@"the alarm should be saved successfully")]
    public void ThenTheAlarmShouldBeSavedSuccessfully()
    {
        var alarmItem = _mainWindow.FindFirstDescendant(cf => cf.ByName("Trumpf Metamation - Login Time"));
        Assert.IsNotNull(alarmItem, "The alarm was not saved successfully.");
    }

    [Then(@"the alarm should repeat from Monday to Friday only")]
    public void ThenTheAlarmShouldRepeatFromMondayToFridayOnly()
    {
        var repeatDropdown = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("RepeatComboBox"));
        var repeatText = repeatDropdown?.Name;
        Assert.IsTrue(repeatText.Contains("Monday to Friday"), "Repeat days are not set correctly.");
    }
}
