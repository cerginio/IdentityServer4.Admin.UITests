using System;
using System.Linq;
using System.Threading;
using IdentityServer4.Admin.UITests.Html.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace IdentityServer4.Admin.UITests.Html.Elements.Bootstrap
{
    public class TagPicker : HtmlInput
    {


        public HtmlElement[] SelectedTags => _selectedTagsFunc.Invoke();
        public HtmlInput[] FoundTags => _searchResultTagsFunc.Invoke();

        public string[] SelectedTagsValues => _selectedTagsFunc.Invoke().Select(x=>x.Text).ToArray();


        private readonly Func<HtmlInput> _searchInputFunc;
        private readonly Func<HtmlElement[]> _selectedTagsFunc;

        private readonly Func<int, HtmlInput> _addTagFromFoundFunc;
        private readonly Func<HtmlInput[]> _searchResultTagsFunc;

        private readonly Func<bool> _waitForAjaxFunc;
        private readonly Func<IWebElement> _spinnerFunc;


        // Sample: allowed scopes items
        // input:  $x("//*[@id='nav-basics']/div/div/div[9]/div/div/div/div/div/input[1]")
        // delete buttons: $x("//*[@id='nav-basics']/div/div/div[9]/div/div/div/div/div/input[1]/following-sibling::div/button")
        // spinner $x('//*[@id="nav-basics"]/div/div/div[9]/div/div/div/div/div/img')
        protected TagPicker(IWebElement webElement) : base(webElement)
        {
            _searchInputFunc = () => new HtmlInput(webElement);
        }


        public TagPicker(IWebDriver driver, string inputPath) :
            this(driver.FindElement(By.XPath($"{inputPath}")))
        {
            _addTagFromFoundFunc = (i) => new HtmlInput(driver.FindElement(By.XPath($"{inputPath}/following-sibling::div/input[{(i == 0 ? 1 : i)}]")));// i=1 by default
            _selectedTagsFunc = () => driver.FindElements(By.XPath($"{inputPath}/following-sibling::div/button")).Select(x => new HtmlElement(x)).ToArray();
            _searchResultTagsFunc = () => { return driver.FindElements(By.XPath($"{inputPath}/following-sibling::div/input")).Select(x => new HtmlInput(x)).ToArray(); };
            _spinnerFunc = () => driver.FindElement(By.XPath($"{inputPath}/following-sibling::img"));

            _waitForAjaxFunc = () => WaitForAjax(driver);
        }
      
        public bool WaitForAjax(IWebDriver driver)
        {
            // as NO ANY of that approaches works:
            _spinnerFunc.Invoke().WaitUntilHidden(TimeSpan.FromSeconds(5));
            _spinnerFunc.Invoke().WaitUntil(img => img.Location.X == 0);
            // <img data-bind="visible: loading()"/>  - find the way to invoke from JS ko view model to get loading()
            // Sample poc
            // ko.components.get('picker', function(vm){ modelbulder = vm})
            // undefined
            // var vm = modelbulder.createViewModel("");
            // undefined
            // vm.loading()
            // false

            // jQuery not an option
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(d => (bool)(d as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));


            // so, greet ans welcome dirty Sleep!
            Thread.Sleep(1000);

            return true;
        }


        public bool AddItem(string tagName)
        {
            //if (SelectedTags.Any(x => x.Text == tagName))
            //{
            //    // item exists
            //    return false;
            //}

            EnterTagName(tagName);// spaces added for more strict search

            _waitForAjaxFunc.Invoke();

            PutTagFromFoundResults(tagName);

            return true;

        }

        private void EnterTagName(string tagName)
        {
            //HtmlInput search = null;
            //_searchInput.Do(() => search = _searchInput.Invoke()).Until(() => search!=null && search.IsPresent());
            //search.EnterText(tagName);

            _searchInputFunc.Invoke().EnterText(tagName);
        }

        private void PutTagFromFoundResults(string tagName)
        {
            //HtmlInput addButton = null;
            //_addTag.Do(() => addButton = _addTag.Invoke()).Until(() => addButton != null && addButton.IsPresent());
            //addButton.Click();

            var list = this.FoundTags.Select(x => x.Value).ToList();
            var index = list.FindIndex(x => x == tagName);

            if (index < 0)
                index = list.FindIndex(x => x.StartsWith(tagName));

            // if found in the list => change List numeration base from 0 to 1 (XPath)
            if (index >= 0)
                index++;
            // use first element in XPath numeration base 1
            else
                index = 1;

            _addTagFromFoundFunc.Invoke(index).Click();
        }

        public int AddItems(string[] tagNames)
        {
            int count = 0;
            foreach (var tag in tagNames)
            {
                if (AddItem(tag))
                {
                    count++;
                }
            }

            return count;
        }

        public bool RemoveItem(string tagName)
        {
            for (int i = SelectedTags.Length; i > 0; i++)
            {
                var item = SelectedTags[i];
                if (item.Text == tagName)
                {
                    item.Click();
                    return true;
                }
            }

            return false;
        }

        public int ClearSelectedItems()
        {
            var count = SelectedTags.Length;
            for (int i = SelectedTags.Length - 1; i >= 0; i--)
            {
                SelectedTags[i].Click();
            }

            return count;
        }
    }
}
