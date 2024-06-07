namespace GameCollectionTracker;

public static class ConnectionStringHelper
{
    public static string GetConnectionString()
    {
        if (File.Exists("C:/Users/U0K0JI/Training/SQLConnection.txt")) //Phil
        {
            return File.ReadAllText("C:/Users/U0K0JI/Training/SQLConnection.txt");
        }
        else if (File.Exists("C:/Users/A234611/Desktop/Bootcamp/ConnectionString.txt")) //Aaron
        {
            return File.ReadAllText("C:/Users/A234611/Desktop/Bootcamp/ConnectionString.txt");
        }
        else if (File.Exists("C:/.NET Bootcamp/240415 .NET/connstringGT.txt")) //Helen
        {
            return File.ReadAllText("C:/.NET Bootcamp/240415 .NET/connstringGT.txt");
        }
        else //Marcus
        {
            return File.ReadAllText("C:/Users/U82XLW/GameTrackerDB.txt");
        }
    }
}