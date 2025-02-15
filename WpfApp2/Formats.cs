using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2;

public static class Formats
{
    /// <summary>
    /// Проверка на то, является ли символ цифрой
    /// </summary>
    /// <param name="c">символ</param>
    /// <returns></returns>
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
    /// <summary>
    /// Проверка форматы даты
    /// </summary>
    /// <param name="line">Строка с датой</param>
    /// <returns></returns>
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
    /// <summary>
    /// Проверка строки на наличие каких - либо символов, помимо цифр
    /// </summary>
    /// <param name="line">Строка</param>
    /// <returns></returns>
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
    /// <summary>
    /// Проверка заполненности элемента TextBox
    /// </summary>
    /// <param name="textBox">ТекстБокс</param>
    /// <returns></returns>
    public static bool isNullTextBox(TextBox textBox)
    {
        if (textBox.Text != null && textBox.Text != "")
        {
            foreach (var i in textBox.Text)
            {
                // Если есть какой - либо символ, кроме пробела, ТекстБокс не пустой
                if (i != ' ')
                {
                    return true;
                    break;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// Проверка на то, относится ли символ к Английскому алфавиту
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static bool IsEnglish(char c)
    {
        if (c >= 95 && c <= 90  || c >= 97 && c <= 122)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Проверка формата email
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    public static bool EmailFormat(string line)
    {
        // Если в строке нет одной точки и одной @, она не может быть эмейлом
        if (line.Count(c => c == '@') != 1 || line.Count(c => c == '.') != 1 )
        {
            return false;
        }
        else
        {
            // если точка появилась раньше @, строка не может быть емейлом
            if (line.IndexOf('@') > line.IndexOf('.'))
            {
                return false;
            }
        }
        foreach (var i in line)
        {
            // Если хоть один символ, кроме вышеописанных, не является английской буквой, строка не может быть емейлом
            if (Formats.IsEnglish(i) == false && i != '.' && i != '@')
            {
                return false;
            }
        }
        return true;
    }
}