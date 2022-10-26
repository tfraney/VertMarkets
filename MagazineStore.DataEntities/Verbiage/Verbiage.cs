
namespace MagazineStore.DataEntities.Verbiage
{
    /// <summary>
    /// Verbiagae and constants used for string literals
    /// </summary>
    public static class Constants
    {
        //chars
        public const char exit = 'x';

        //general error messages
        public const string fatal_error = @"Fatal Error: ";
        public const string unknown_error = @"Unknown error.";
        public const string no_token_error = @"No token passed.";
        public const string no_categories = @"No categories passed.";
        public const string mag_error = @"Error Reading Magazines: ";
        public const string cat_error = @"Error Reading Categories: ";
        public const string mag_loading_error = @"Loading Magazines Error: ";
        public const string sub_error = @"Error Reading Subscribers: ";
        public const string sub_loading_error = @"Loading Subscribers Error: ";
        public const string token_error = @"Error Reading Token: ";
        public const string token_loading_error = @"Loading Token Error: ";

        public const string post_sub_error = @"Error Posting Subscriber List: ";

        //reporting messages after post: requires a non-literal for usiage of new lines
        public const string report_failed = "\n\nReport has failed to load.";
        public const string report_correct = "subscribers were correct.";
        public const string report_incorrect = "subscribers were incorrect.\nShould Be: ";
        public const string report_totaltime = "\n\nReport:\nTotalTime:  ";
        public const string report_status = "\nStatus: ";

        //api keys and url
        public const string Url = @"http://magazinestore.azurewebsites.net/";
        public const string api_action_token = @"/api/token";
        public const string api_action_category = @"/api/categories";
        public const string api_action_subscribers = @"/api/subscribers";
        public const string api_action_magazines = @"/api/magazines";
        public const string api_action_send_answer = @"/api/answer";

        //Main Application messages
        public const string title = @"VertMarkets Magazine Store Subscriber Task";
        public const string app_starting = @"Starting....";
        public const string init_successful = @"Initialized Successfully.";     
        public const string keypress_message = @"Hit x to quit, or any to try again.";
    }
}
