using MagazineStore.DataEntities;
using VERBS = MagazineStore.DataEntities.Verbiage.Constants;

namespace MagazineStoreApp.Business
{
    /// <summary>
    /// Ineternal Special Client to BO to access Standard Client based on Get Methods by Entity, passing in tokens
    /// </summary>
    internal class MagazineStoreClient 
    {
  
        public static async Task<Entity?> GetToken() => await StandardClientAPI.ReadAPI<Entity>(VERBS.Url, VERBS.api_action_token);

        public static async Task<CategoriesEntity?> GetCategories(string token) => await StandardClientAPI.ReadAPI<CategoriesEntity>(VERBS.Url, VERBS.api_action_category, token);

        public static async Task<SubscribersEntity?> GetSubscibers(string token) => await StandardClientAPI.ReadAPI<SubscribersEntity>(VERBS.Url, VERBS.api_action_subscribers, token);

        public static async Task<MagazinesEntity?> GetMagazines(string token, string cat) => await StandardClientAPI.ReadAPI<MagazinesEntity>(VERBS.Url, VERBS.api_action_magazines, token, cat);

        public static async Task<AnswerResultEntity?> ReportSubscribers(string token, SubscriberList list)
        {
            return await StandardClientAPI.PostAPI<AnswerResultEntity>(VERBS.Url, $"{VERBS.api_action_send_answer}/{token}", list);
        }

    }


}
