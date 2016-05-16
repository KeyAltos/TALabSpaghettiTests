using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TALabSpaghettiTestsKapatsevich.WebDriverFactory;

namespace TALabSpaghettiTestsKapatsevich.TestsConstants
{
    public static class Constants
    {
        public static readonly WebBrowsers browserForTesting=WebBrowsers.FireFox;

        public static readonly User USER_ONE = new User()
        {
            Username = "kapatsevich.userone@gmail.com",
            Password = "nibumbum"
        };

        public static readonly User USER_TWO = new User()
        {
            Username = "kapatsevich.usertwo@gmail.com",
            Password = "nibumbum"
        };

        public static readonly User USER_THREE = new User()
        {
            Username = "kapatsevich.mailfortest@gmail.com",
            Password = "nibumbum"
        };

        public static readonly Message SPAM_MESSAGE = new Message()
        {
            Title = "Message for spammarking №",
            Text = @"Spam message text.
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus vitae orci nec orci dapibus porttitor non id est. Aenean sodales lacinia scelerisque. Vestibulum cursus hendrerit magna, ut gravida nulla pharetra eget. Aliquam lacinia pulvinar metus, vel blandit lectus varius eu. Donec feugiat tellus id dolor ultricies tincidunt. Maecenas et odio erat. Phasellus id gravida nisi. Sed ornare nec augue quis malesuada."
        };

        public static readonly Message NOT_SPAM_MESSAGE = new Message()
        {
            Title = "Simple message №",
            Text = @"Not spam message text.
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus vitae orci nec orci dapibus porttitor non id est. Aenean sodales lacinia scelerisque. Vestibulum cursus hendrerit magna, ut gravida nulla pharetra eget. Aliquam lacinia pulvinar metus, vel blandit lectus varius eu. Donec feugiat tellus id dolor ultricies tincidunt. Maecenas et odio erat. Phasellus id gravida nisi. Sed ornare nec augue quis malesuada."
        };


        public static readonly TimeSpan WAITING_TIME = new TimeSpan(0, 0, 10);
        public static readonly string XPATH_LOCATOR_FOR_ALL_MESSAGES_IN_CURRENT_FOLDER = "//span[@email]";

        #region Spam        
        public static readonly string XPATH_LOCATOR_FOR_SPAM_BUTTON = "//div[@gh='mtb']//div[@act='9']";
        public static readonly string XPATH_LOCATOR_FOR_CLEAR_SPAM_BUTTON = "//div[@gh='mtb']//div[@act='10']";
        #endregion

        #region Forward
        public static readonly string XPATH_LOCATOR_FOR_SUBMIT_BUTTON_IN_NEW_WINDOW = "//input[@type='submit']";
        public static readonly string GOOGLE_NOREPLY_EMAIL = "forwarding-noreply@google.com";
        public static readonly string XPATH_LOCATOR_FOR_FORWARDING_SETTINGS_SAVE_CHANGES_BUTTONS = "//div[@role='main']//tr[@guidedhelpid='save_changes_row']//button[@guidedhelpid]";
        public static readonly string XPATH_LOCATOR_FOR_CREATE_NEW_FILTER_BASED_ON_SEARCH_LINK = "//div[contains(text(), 'Create filter with this search')]";
        public static readonly string XPATH_LOCATOR_FOR_SUBMIT_CREATE_NEW_FILTER_BUTTON = "//div[contains(text(), 'Create filter')]";
        #endregion


    }
}
