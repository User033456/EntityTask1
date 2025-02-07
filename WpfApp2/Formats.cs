using System.Runtime.CompilerServices;
using System.Windows;

namespace WpfApp2;

public static class Formats
{
    public static bool ISNumber(char c)
    {
        char[] nums = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        foreach (var i in nums)
        {
            if (c == i)
            {
                return true;
            }
        }
        return false;
    }
    public static bool DateFormat(string line)
    {
        bool result = true;
        if (line.Length != 10)
        {
            result = false;
        }
        else
        {
            if (ISNumber(line[0]) && ISNumber(line[1]) && line[2] == '-' && ISNumber(line[3]) && ISNumber(line[4]) &&
                line[5] == '-' &&
                ISNumber(line[6]) && ISNumber(line[7]) && ISNumber(line[8]) && ISNumber(line[9]))
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
        return result;
    }

    public static bool OnlyNumbers(string line)
    {
        foreach (var i in line )
        {
            if (ISNumber(i) == false)
            {
                return false;
            }
        }
        return true;
    }
}