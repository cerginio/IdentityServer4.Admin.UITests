using IdentityServer4.Admin.UITests.Html.Extensions;
using OpenQA.Selenium;

namespace IdentityServer4.Admin.UITests.Html.Elements.Bootstrap
{
    public class ToggleSwitch : HtmlCheckBox
    {
        // webElement vs inputElement
        // In bootstrap toggle switch state storing in non ckickable input,
        // so state On | Off will get from that input element
        // But click on webElement
        private readonly HtmlInput _inputElement;

        protected ToggleSwitch(IWebElement webElement, HtmlInput inputElement) : base(webElement)
        {
            _inputElement = inputElement;
        }

        public ToggleSwitch(IWebDriver driver, string inputPath) : 
            this(driver.FindElement(By.XPath($"{inputPath}/following-sibling::div")), new HtmlInput(driver.FindElement(By.XPath($"{inputPath}"))))
        {
        }
   

        public void On()
        {
            this.Checked = true;
        }

        public void Off()
        {
            this.Checked = false;
        }

        public bool Switch()
        {
            this.Checked = !this.Checked;
            return Checked;
        }

        public override bool Checked
        {
            get => _inputElement.Selected;
            set { _inputElement.Do(Click).Until(self => _inputElement.Selected == value); }
        }

    }
}
