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

        public static readonly TimeSpan WAITING_TIME = new TimeSpan(0, 0, 10);
        public static readonly string XPATH_LOCATOR_FOR_ALL_MESSAGES_IN_CURRENT_FOLDER = "//span[@email]";
        #region folder labels
        public static readonly string SPAM_FOLDER_LABEL = "in: spam";
        public static readonly string INBOX_FOLDER_LABEL = "in: label:inbox";
        public static readonly string IMPORTANT_FOLDER_LABEL = "in: label:important";
        public static readonly string TRASH_FOLDER_LABEL = "in:trash" ;
        public static readonly string TRASH_IMPORTANT_LABEL = "is:important in:trash ";
        #endregion
        #region Spam        
        public static readonly string XPATH_LOCATOR_FOR_SPAM_BUTTON = "//div[@gh='mtb']//div[@act='9']";
        public static readonly string XPATH_LOCATOR_FOR_CLEAR_SPAM_BUTTON = "//div[@gh='mtb']//div[@act='10']";
        #endregion

        #region Settings
        public static readonly string XPATH_LOCATOR_FOR_SUBMIT_BUTTON_IN_NEW_WINDOW = "//input[@type='submit']";
        public static readonly string GOOGLE_NOREPLY_EMAIL = "forwarding-noreply@google.com";
        public static readonly string XPATH_LOCATOR_FOR_FORWARDING_SETTINGS_SAVE_CHANGES_BUTTONS = "//div[@role='main']//tr[@guidedhelpid='save_changes_row']//button[@guidedhelpid]";
        public static readonly string XPATH_LOCATOR_FOR_CREATE_NEW_FILTER_BASED_ON_SEARCH_LINK = "//div[contains(text(), 'Create filter with this search')]";
        public static readonly string XPATH_LOCATOR_FOR_SUBMIT_CREATE_NEW_FILTER_BUTTON = "//div[contains(text(), 'Create filter')]";
        public static readonly string FILE_NAME_FOR_GDRIVE_ATTACH_NORMALY_SIZE = "5MB.test";
        #endregion

        #region Attach file to message from GDrive
        public static readonly string XPATH_LOCATOR_FOR_ATTACH_FILE_FROM_GDRIVE_BUTTON = "//div[@aria-label='Insert files using Drive']";
        public static readonly string XPATH_LOCATOR_FOR_ATTACH_IFRAME = "//iframe[contains(@src, 'google.com/picker')]";
        public static readonly string XPATH_LOCATOR_FOR_ATTACH_SELECTOR = "//div[@value='attach']";
        public static readonly string ID_LOCATOR_FOR_INSERT_FILE_BUTTON = "picker:ap:0";
        #endregion


    }
}
