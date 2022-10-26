using System.Threading.Tasks;
using MagazineStore.DataEntities;
using VERBS = MagazineStore.DataEntities.Verbiage.Constants;

namespace MagazineStoreApp.Business
{
    /// <summary>
    /// True Business Object that application utlilizes to run the APIs.
    /// </summary>
    public class MagazineStoreBO : IDisposable
    {
       
        
        private string token = string.Empty;     
        public int TotalCategoriesFound { get; private set; } = 0;
        public MagazinesEntity? Magazines { get; private set; } = null;       
        public SubscribersEntity? Subscribers { get; private set; } = null;

        public MagazineStoreBO() { }


        /// <summary>
        /// Disposes all lists in entities for clean up
        /// </summary>
        public void Dispose() {            
            Magazines?.Data?.Clear();
            Subscribers?.Data?.Clear();
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Builds the Subscriber List based on having one or more magazines for all categories 
        /// Using LINQ would be effecient in this one call to build a SubscriberLit Entity.
        /// </summary>
        /// <returns>Subscriber List Entity (not async task) </returns>
        public SubscriberList SubscibersWithMagazinesInAllCategory()
        {
            if ((Subscribers?.Data?.Any() ?? false) && (Magazines?.Data?.Any() ?? false)) //if any data to join
            {
                //distinct list of subscriber ID and category ID by join
                var list = Subscribers?.Data?.Select(x => (x.Id, x.MagazineIds.Join(Magazines.Data, a => a, mag => mag.Id,
                                                                                     (a, mag) => mag.Category)?.Select(m => m)?.Distinct() ?? new List<string>()));

                //refines list to count of distinct categories equal to total number of categories found
                return new SubscriberList(list?.Where(l => (l.Item2?.Count() ?? 0) == TotalCategoriesFound).Select(s => s.Id));
            }
            else return new SubscriberList(null);
        }


        /// <summary>
        /// Taking subscriiber list, posting into ReportSubscribers Method
        /// To return a response based on correct count and status or error message if failed.
        /// </summary>
        /// <param name="list"></param>
        /// <returns>String output of response.</returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> ReportSubscribersinAllCategories(SubscriberList list)
        {
            AnswerResultEntity? a;
            list.Subscribers ??= new List<string>();

            //await post and check for result
            if (!((a = await MagazineStoreClient.ReportSubscribers(token, list))?.Success ?? false) && a?.Data == null)
            {
                throw new Exception($"{VERBS.post_sub_error}{a?.Message ?? VERBS.unknown_error}");
            }
            else if (a != null)
            {
                //build verbiage of results to print out later.
                var resp = a.Data.AnswerCorrect ? $"{list.Subscribers.Count} {VERBS.report_correct}."
                                               : $"{list.Subscribers.Count} {VERBS.report_incorrect}{string.Join(",", a.Data.ShouldBe) }";
                return $"{VERBS.report_totaltime}{a.Data.TotalTime} seconds. {VERBS.report_status}{resp}";
            }
            else return VERBS.report_failed;
        }




        /// <summary>
        /// Initializes the Data in one Task, starting with token and other API calls in Task parallelism.
        /// </summary>
        /// <returns>Success</returns>
        public async Task<bool> InitializeMagazineStoredata()
        {
            List<Task>? tasks = null;
            Task? t = null;

            try  {
                await (t = LoadToken());

                //if token is accepted start loading data
                if (t.IsCompleted && t.Exception == null)  {
                    tasks = new List<Task>(new[] { LoadSubscribers(), LoadCategoriesAndMagazines() }.ToArray());

                    //wait until two Task functions complete bfore returning true or exceptions
                    t = Task.WhenAll(tasks).ContinueWith(x => { if (x.Exception != null) throw x.Exception; });
                    await t;                    
                }
                if (t?.Exception != null) throw t.Exception.InnerException ?? t.Exception;
                return true;
            }
            catch (Exception) { throw; }
            finally { tasks?.Clear(); }
        }


      
        /// <summary>
        /// Gets categories in one task, and continues with all tasks looped through categories to find magazines, under one async call
        /// </summary>
        /// <returns>Private Async Task</returns>
        /// <exception cref="Exception"></exception>
        private async Task LoadCategoriesAndMagazines()
        {
            try  {
                var ct = MagazineStoreClient.GetCategories(token).ContinueWith(async categories => {
                  
                    CategoriesEntity? cat;
                    if ( (cat = await categories) != null && cat.Success)  {
                        TotalCategoriesFound = cat.Data?.Count ?? 0;

                        //if count > 0
                        if (TotalCategoriesFound > 0) {
                            
                            Magazines = new MagazinesEntity();
                            List<Task<MagazinesEntity?>> tasks = new();

                            //run all taks with each category
                            cat.Data?.ForEach(x => tasks.Add(Task.Run(() =>   MagazineStoreClient.GetMagazines(token, x))));
                            Task.WaitAll(tasks.ToArray());

                            //assign magazine after completion and outside of cross-threat scope.
                            tasks.ForEach(t =>  {
                                if (t.Result?.Success ?? false) Magazines.Data.AddRange(t.Result.Data);
                                else throw new Exception($"{VERBS.mag_error}{t.Result?.Message ?? VERBS.unknown_error}");
                            });
                        }
                        else throw new Exception(VERBS.no_categories);
                    } 
                    else throw new Exception($"{VERBS.cat_error}{cat?.Message ?? VERBS.unknown_error}");
                   
                });
                await ct; //wait for all during asynch call
                if (ct.Exception != null) throw ct.Exception;
            }
            catch (Exception ex) { throw new Exception($"{VERBS.mag_loading_error}{ ex.InnerException?.Message ?? ex.Message }"); }
        }


        /// <summary>
        /// Loads subscribers entity within one task during async call
        /// </summary>
        /// <returns>Private Async Task</returns>
        /// <exception cref="Exception"></exception>
        private async Task LoadSubscribers()
        {
            if (!((Subscribers = await MagazineStoreClient.GetSubscibers(token))?.Success ?? false))   {
                throw new Exception($"{VERBS.sub_error}{Subscribers?.Message ?? VERBS.unknown_error}");
            }
        }



        /// <summary>
        /// Loads Token to Start under on Aync task
        /// </summary>
        /// <returns>Private Async Task</returns>
        /// <exception cref="Exception"></exception>
        private async Task LoadToken()
        {

            Entity? token;

            if (!((token = await MagazineStoreClient.GetToken())?.Success ?? false))  {
                throw new Exception($"{VERBS.token_error}{Subscribers?.Message ?? VERBS.unknown_error}");
            }
            else if (string.IsNullOrWhiteSpace(token.Token)) throw new Exception(VERBS.no_token_error);
            this.token = token.Token;
        }
    }
}
