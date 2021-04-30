/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2021 Ingo Herbote
 * https://www.yetanotherforum.net/
 * 
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at

 * http://www.apache.org/licenses/LICENSE-2.0

 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

namespace YAF.Tests.Utils.Extensions
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// WebDriver Extensions
    /// </summary>
    public static class WebDriverExtensions
    {
        /// <summary>
        /// Finds the element.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="by">The by.</param>
        /// <param name="timeoutInSeconds">The timeout in seconds.</param>
        /// <returns>Returns the Web Element</returns>
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds <= 0)
            {
                return driver.FindElement(by);
            }

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(drv => drv.FindElement(by));
        }

        /// <summary>
        /// Sets the text for control.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="text">The text.</param>
        public static void SetTextForControl(this IWebDriver browser, By locator, string text)
        {
            WaitFor.ElementPresent(browser, locator);
            var element = browser.FindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        /// <summary>
        /// Selects the yes no RadioButton.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="isSelected">if set to <c>true</c> [is selected].</param>
        public static void SelectYesNoRadioButton(this IWebDriver browser, By locator, bool isSelected)
        {
            WaitFor.ElementPresent(browser, locator);
            var elements = browser.FindElements(locator);

            if (isSelected)
            {
                elements[0].Click();
            }
            else
            {
                elements[1].Click();
            }
        }

        /// <summary>
        /// Selects the RadioButton.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="index">The index.</param>
        public static void SelectRadioButton(this IWebDriver browser, By locator, int index)
        {
            WaitFor.ElementPresent(browser, locator);
            var radioButtons = browser.FindElements(locator);
            radioButtons[index].Click();
        }

        /// <summary>
        /// Selects the drop down by text.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="text">The text.</param>
        public static void SelectDropDownByText(this IWebDriver browser, By locator, string text)
        {
            WaitFor.ElementPresent(browser, locator);
            var selectElement = new SelectElement(browser.FindElement(locator));
            selectElement.SelectByText(text);
        }

        /// <summary>
        /// Selects the drop down by value.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="value">The value.</param>
        public static void SelectDropDownByValue(this IWebDriver browser, By locator, string value)
        {
            WaitFor.ElementPresent(browser, locator);
            var selectElement = new SelectElement(browser.FindElement(locator));
            selectElement.SelectByValue(value);
        }

        /// <summary>
        /// Selects the CheckBox.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <param name="locator">The locator.</param>
        public static void SelectCheckBox(this IWebDriver browser, By locator)
        {
            WaitFor.ElementPresent(browser, locator);
            var checkBox = browser.FindElement(locator);
            checkBox.Click();
        }

        /// <summary>
        /// Clicks the link.
        /// </summary>
        /// <param name="browser">The browser.</param>
        /// <param name="locator">The locator.</param>
        public static void ClickLink(this IWebDriver browser, By locator)
        {
            WaitFor.ElementPresent(browser, locator);
            var element = browser.FindElement(locator);
            element.Click();
        }

        /// <summary>
        /// Clicks the submit button.
        /// </summary>
        /// <param name="browser">The browser.</param>
        public static void ClickSubmitButton(this ISearchContext browser)
        {
            var element = browser.FindElement(By.XPath("//input[@type='submit']"));
            element.Click();
        }

        /// <summary>
        /// Downloads the file.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="url">The URL.</param>
        /// <param name="localPath">The local path.</param>
        public static void DownloadFile(this IWebDriver driver, string url, string localPath)
        {
            var client = new WebClient { Headers = { [HttpRequestHeader.Cookie] = CookieString(driver) } };

            client.DownloadFile(url, localPath);
        }

        /// <summary>
        /// Elements the exists.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="by">The by.</param>
        /// <returns>Returns if the element exits or not.</returns>
        public static bool ElementExists(this IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Elements the exists.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="sleep">The sleep.</param>
        public static void ClickAndWait(this IWebElement element, int sleep = 400)
        {
            element.Click();

            Thread.Sleep(sleep);
        }

        /// <summary>
        /// Clicks the and wait until visible.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="by">The by.</param>
        /// <param name="sleep">The sleep.</param>
        public static void ClickAndWaitUntilVisible(this IWebDriver driver, By by, int sleep = 400)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(sleep));

            var element = wait.Until(c => c.FindElement(by));

            element.Click();

            Thread.Sleep(sleep);
        }

        /// <summary>
        /// Gets Cookies as string.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <returns>Returns the Cookie string</returns>
        private static string CookieString(IWebDriver driver)
        {
            var cookies = driver.Manage().Cookies.AllCookies;
            return string.Join("; ", cookies.Select(c => $"{c.Name}={c.Value}"));
        }
    }
}