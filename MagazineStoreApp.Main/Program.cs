
using MagazineStoreApp.Business;
using VERBS = MagazineStore.DataEntities.Verbiage.Constants;
bool retest = true;

//start app
Console.WriteLine(VERBS.title);

//while not x pressed
while (retest)
{
    //disposable BO store to call functions
    using var bo = new MagazineStoreBO();
    try
    {
      
        Console.WriteLine($"\n{VERBS.app_starting}");

        //wait for Initial data Load
        if (await bo.InitializeMagazineStoredata())
        {            
            Console.WriteLine($"{VERBS.init_successful}");

            //build subscriber List and post to report
            var s = bo.SubscibersWithMagazinesInAllCategory();

            Console.WriteLine(await bo.ReportSubscribersinAllCategories(s));
        }
    }
    catch (Exception ex) { Console.WriteLine($"{VERBS.fatal_error}{ex.Message}"); }
    finally {
        bo?.Dispose();
        Console.WriteLine($"\n{VERBS.keypress_message}"); 
        retest = Console.ReadKey().KeyChar != VERBS.exit;    
    }
}



