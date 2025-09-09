using System;

namespace MyLibrary;

public static class WeatherCalculator
{
    public static string DetermineSeason(DateOnly date)
    {
        int month = date.Month;
        string season= month switch
        {
            3 or 4 or 5 => "Spring",
            6 or 7 or 8 => "Summer",
            9 or 10 or 11 => "Fall",
            _ => "Winter"
        };
        return season;
    }
}
