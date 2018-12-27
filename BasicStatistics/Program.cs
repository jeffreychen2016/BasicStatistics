using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{
    static void Main(String[] args)
    {
        /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
        //int N = Convert.ToInt32(Console.ReadLine());

        //decimal[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), arrTemp => Convert.ToDecimal(arrTemp));
        var arr = new decimal[10] { 64630, 11735,14216,99233,14470,4978,73429,38120,51135,67060};
        // var arr = Console.ReadLine();
        decimal total = 0.0m;
        decimal mean = 0.0m;
        decimal median = 0.0m;
        decimal mode = 0.0m;
        decimal deviation = 0.0m;

        // find mean
        foreach (decimal i in arr)
        {
            total += i;
        }

        mean = total / arr.Length;

        // find meadian
        Array.Sort(arr, (x, y) => x.CompareTo(y));

        if (arr.Length % 2 == 0)
        {
            var firstNum = arr[arr.Length / 2 - 1];
            var secondNum = arr[arr.Length / 2];
            median = (firstNum + secondNum) / 2;
        }
        else
        {
            median = arr[(arr.Length + 1) / 2];
        }

        // find mode
        mode = arr
                .GroupBy(n => n)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .First();

        // find deviation
        // (x1-m)2
        for (var i = 0; i < arr.Length; i++)
        {
            arr[i] = (arr[i] - mean) * (arr[i] - mean);
        }

        // (((x1-m)2+(x2-m)2+(x3-m)2+(x4-m)2+...(xN-m)2))/N)0.5
        // Math.Sqrt only takes in double type as parameter
        // the result (decimal)Math.Sqrt((double)arr.Sum() / arr.Length) has more than 1 decimals
        // use  Math.Truncate(deviation * 10) / 10 to get rid of extra decimals
        deviation = (decimal)Math.Sqrt((double)arr.Sum() / arr.Length);
        deviation = Math.Truncate(deviation * 10) / 10;

        // find boundries
        // two tails, look up z-table 0.475 is 1.96
        // x̅ ± Za/2 * σ/√(n)
        // σ/√(n) is standard error
        var val = 1.96m;
        var size = arr.Length;
        var standardError = deviation / (decimal)Math.Sqrt(size);
        var lower = Math.Round(mean - (standardError * val), 1);
        var upper = Math.Round(mean + (standardError * val), 1);

        Console.WriteLine(mean);
        Console.WriteLine(median);
        Console.WriteLine(mode);
        Console.WriteLine(deviation);
        Console.WriteLine($"{lower} {upper}");
    }
}