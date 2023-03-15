using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.AutomationElements;
using System.Threading;
using FlaUI.Core.Conditions;
using FlaUI.Core.Input;
using NUnit.Framework;

namespace MyTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [SetUp]
        public void BaseSetUp()
        {
            //some code
            int a = 2;
        }

        [TestMethod]
        public void TestControls()
        {
            var application = FlaUI.Core.Application.Launch(@"C:\Users\venislav.zdravkov\Documents\Projects\FlaUI\FlauiWinForms\FlaUI\src\TestApplications\WpfApplication\bin\WpfApplication.exe");
            var mainWindow = application.GetMainWindow(new UIA3Automation());
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
            mainWindow.FindFirstDescendant(cf.ByAutomationId("SimpleCheckBox")).AsCheckBox().Click();
            mainWindow.FindFirstDescendant(cf.ByAutomationId("TextBox")).AsTextBox().Text = "Test Venislav";
            mainWindow.FindFirstDescendant(cf.ByAutomationId("PasswordBox")).AsTextBox().Text = "123456";
            mainWindow.FindFirstDescendant(cf.ByName("ListBox Item #2")).AsListBoxItem().Click();
            mainWindow.FindFirstDescendant(cf.ByName("RadioButton2")).AsRadioButton().Click();
            mainWindow.FindFirstDescendant(cf.ByAutomationId("PART_EditableTextBox")).AsTextBox().Enter("Item 1");
            mainWindow.FindFirstDescendant(cf.ByAutomationId("NonEditableCombo")).AsComboBox().Select(2).Click();
            var mySlider = mainWindow.FindFirstDescendant(cf.ByAutomationId("Thumb")).AsThumb();
            mySlider.SlideHorizontally(10);
            mySlider.SlideHorizontally(-30);
            var verticalSlider = mainWindow.FindFirstByXPath("//Pane/ScrollBar[1]/Thumb").AsThumb();
            verticalSlider.SlideVertically(10);
            verticalSlider.SlideVertically(-20);
            var horizontalSlider = mainWindow.FindFirstByXPath("//Pane/ScrollBar[2]/Thumb").AsThumb();
            horizontalSlider.SlideHorizontally(10);
            horizontalSlider.SlideHorizontally(-20);
            Thread.Sleep(5000);
            NUnit.Framework.Assert.IsNotNull(application);
        }

        [TestMethod]
        public void TestPopUps()
        {
            var application = FlaUI.Core.Application.Launch(@"C:\Users\venislav.zdravkov\Documents\Projects\FlaUI\FlauiWinForms\FlaUI\src\TestApplications\WpfApplication\bin\WpfApplication.exe");
            var mainWindow = application.GetMainWindow(new UIA3Automation());
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

            mainWindow.FindFirstDescendant(cf.ByName("Popup Toggle 1")).Click();
            var label = mainWindow.FindFirstDescendant(cf.ByName("This is a popup")).AsLabel();
            NUnit.Framework.Assert.IsNotNull(application);
            NUnit.Framework.Assert.That(label.Text, NUnit.Framework.Is.EqualTo("This is a popup"));
        }

        [TestMethod]
        public void ChangeTabTest()
        {
            var application = FlaUI.Core.Application.Launch(@"C:\Users\venislav.zdravkov\Documents\Projects\FlaUI\FlauiWinForms\FlaUI\src\TestApplications\WpfApplication\bin\WpfApplication.exe");
            var mainWindow = application.GetMainWindow(new UIA3Automation());
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

            mainWindow.FindFirstDescendant(cf.ByName("Complex Controls")).Click();
            Thread.Sleep(2000);
            mainWindow.FindFirstDescendant(cf.ByName("Simple Controls")).Click();
        }

        [TestMethod]
        public void CalendarTest()
        {
            //lauch application and arrange start up
            var application = FlaUI.Core.Application.Launch(@"C:\Users\venislav.zdravkov\Documents\Projects\FlaUI\FlauiWinForms\FlaUI\src\TestApplications\WpfApplication\bin\WpfApplication.exe");
            var mainWindow = application.GetMainWindow(new UIA3Automation());
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

            //switch tab
            mainWindow.FindFirstDescendant(cf.ByName("More Controls")).Click();
            //selecting date
            DateTime today_Plus2 = DateTime.Today.AddDays(2);
            DateTime today = DateTime.Today;
            var selectToday = mainWindow.FindFirstDescendant(cf.ByAutomationId("calendar")).AsCalendar();
            var calendar = mainWindow.FindFirstDescendant(cf.ByAutomationId("calendar")).AsCalendar();
            calendar.SelectDate(today);
            calendar.AddToSelection(today_Plus2);

            DateTime[] selectedDates = calendar.SelectedDates;
            Thread.Sleep(2000);
            NUnit.Framework.Assert.IsNotNull(application);
            NUnit.Framework.Assert.That(selectedDates, NUnit.Framework.Has.Length.EqualTo(2));
        }

        [TestMethod]
        public void TestCalenderPopUp()
        {
            //lauch application and arrange start up
            var application = FlaUI.Core.Application.Launch(@"C:\Users\venislav.zdravkov\Documents\Projects\FlaUI\FlauiWinForms\FlaUI\src\TestApplications\WpfApplication\bin\WpfApplication.exe");
            var mainWindow = application.GetMainWindow(new UIA3Automation());
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
            //switch tab
            mainWindow.FindFirstDescendant(cf.ByName("More Controls")).Click();
            mainWindow.FindFirstDescendant(cf.ByAutomationId("PART_Button")).Click();

            DateTime tomorrow = DateTime.Today.AddDays(2);
            var calendar = mainWindow.FindFirstByXPath("/Window[2]/Tab/TabItem[3]/Custom/Calendar").AsCalendar();
            calendar.SelectDate(tomorrow);
            Thread.Sleep(2000);
            NUnit.Framework.Assert.IsNotNull(application);
            //some change
        }

        [TestMethod]
        public void TestCheckBoxes()
        {
            var application = FlaUI.Core.Application.Launch(@"C:\Users\venislav.zdravkov\Documents\Projects\FlaUI\FlauiWinForms\FlaUI\src\TestApplications\WpfApplication\bin\WpfApplication.exe");
            var mainWindow = application.GetMainWindow(new UIA3Automation());
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
            mainWindow.FindFirstDescendant(cf.ByName("Complex Controls")).Click();

            var checkbox1 = mainWindow.FindFirstByXPath("//DataItem[1]/Custom[3]/CheckBox").AsCheckBox();
            var checkbox2 = mainWindow.FindFirstByXPath("//DataItem[2]/Custom[3]/CheckBox").AsCheckBox();
            checkbox1.DoubleClick();
            //checkbox2.DoubleClick();
            Thread.Sleep(2000);
            NUnit.Framework.Assert.IsNotNull(application);
            NUnit.Framework.Assert.True(checkbox1.IsChecked);
            NUnit.Framework.Assert.True(checkbox2.IsChecked);

        }
    }
}
