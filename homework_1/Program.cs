Console.OutputEncoding = System.Text.Encoding.Unicode;
#region Task-1
int GetIndexInAlphabet(char value)
{
    char upper = char.ToUpper(value);
    if (upper < 'A' || upper > 'Z')
    {
        throw new Exception("This method only accepts standard Latin characters.");
    }

    return (int)upper % 32;
}

char ReadChar(string message)
{
    Console.Write(message);
    string input = Console.ReadLine();
    bool succesfullParsing = char.TryParse(input, out char result);
    try
    {
        if (!succesfullParsing)
        {
            throw new Exception("Value should be char!");
        }
        return result;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return ReadChar(message);
    }

}

Console.WriteLine("Знайти позицію літери в алфавіті та перевести її в інший регістр");
char input = ReadChar("Enter char: ");
int indexInAlphabet = GetIndexInAlphabet(input);
char anotherRegister = input == char.ToUpper(input) ? char.ToLower(input) : char.ToUpper(input);
Console.WriteLine($"Char: {input}, position in alphabet: {indexInAlphabet}, another register: {anotherRegister}");
Console.WriteLine();
#endregion

#region Task-2
string[] GetArrayFromString(string sourceString, string separator)
{
    string[] newArray = { };
    void GetSubString()
    {
        int index = sourceString.IndexOf(separator);
        if (index > 0)
        {
            string subString = sourceString.Substring(0, index);
            newArray = newArray.Append(subString).ToArray();
            sourceString = sourceString.Remove(0, subString.Length + 1);
            GetSubString();
        }
        else if (sourceString.Length > 0)
        {
            newArray = newArray.Append(sourceString).ToArray();
        }
    }
    GetSubString();
    return newArray;
}

Console.WriteLine("Розділювач рядка. Дана строка та символ, потрібно розділити строку на кілька строк (масив строк) виходячи із заданого символу. Наприклад: строка = \"Лондон, Париж, Рим\", а символ = ','. Результат = string[] { \"Лондон\", \"Париж\", \"Рим\" }.");
string rowOfCapitals = "Лондон, Париж, Рим";
string[] arrayOfCapitals = GetArrayFromString(rowOfCapitals, ",");
Console.WriteLine();
Console.WriteLine($"Array from string: {string.Join(",", arrayOfCapitals)}");
Console.WriteLine();
#endregion

#region Task-3
string ReadString(string message)
{
    Console.Write(message);
    string input = Console.ReadLine();
    try
    {
        if (String.IsNullOrEmpty(input))
        {
            Console.WriteLine("Field can not be empty");
            return ReadString(message);
        }
        return input;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return ReadString(message);
    }

}
int[] GetArrayOfIndexSubStr(string sourceString, string subStr)
{
    int[] newArray = { };
    void GetIndexOfSubStr()
    {
        int index = sourceString.IndexOf(subStr);
        if (index > -1)
        {
            int subStrIndex = newArray.Length > 0 ? index + newArray[newArray.Length - 1] + subStr.Length : index;
            newArray = newArray.Append(subStrIndex).ToArray();
            sourceString = sourceString.Remove(0, index + subStr.Length);
            GetIndexOfSubStr();
        }
    }
    GetIndexOfSubStr();
    return newArray;
}

Console.WriteLine("Пошук підстроки у строці.");
string sourcestring = ReadString("Enter string: ");
string substr = ReadString("Enter sub string: ");
int[] indexesofsubstr = GetArrayOfIndexSubStr(sourcestring, substr);
Console.WriteLine($"Array of index: {string.Join(",", indexesofsubstr)}");
Console.WriteLine();
#endregion

#region Task-4
Console.WriteLine("Написати програму, яка виводить число літерами. Приклад: 117 - сто сімнадцять");
string[] units = { "нуль", "один", "два", "три", "чотири", "п'ять", "шість", "сім", "вісім", "дев'ять" };
string[] teens = { "", "одинадцять", "дванадцять", "тринадцять", "чотирнадцять", "п'ятнадцять", "шістнадцять", "сімнадцять", "вісімнадцять", "дев'ятнадцять" };
string[] tens = { "", "десять", "двадцять", "тридцять", "сорок", "п'ятдесят", "шістдесят", "сімдесят", "вісімдесят", "дев'яносто" };
string[] hundreds = { "", "сто", "двісті", "триста", "чотириста", "п'ятсот", "шістсот", "сімсот", "вісімсот", "дев'ятсот" };

string NumberToWords(int number)
{
    if (number == 0)
        return units[0];

    if (number < 0)
        return "мінус " + NumberToWords(Math.Abs(number));

    string words = "";

    if ((number / 100) > 0)
    {
        words += hundreds[number / 100] + " ";
        number %= 100;
    }

    if (number > 0)
    {
        if (number < 10)
            words += units[number];
        else if (number < 20)
            words += teens[number - 10];
        else
        {
            words += tens[number / 10] + " ";
            words += units[number % 10];
        }
    }

    return words;
}
int ReadNumber(string message, int max)
{
    Console.Write(message);
    string input = Console.ReadLine();
    bool succesfullParsing = int.TryParse(input, out int result);
    try
    {
        if (!succesfullParsing)
        {
            throw new Exception("You enter wrong number");
        }
        else if (result > max)
        {
            throw new Exception($"You enter number more then max. Max: {max}");
        }
        return result;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return ReadNumber(message, max);
    }

}

int number = ReadNumber("Enter number: ", 999); 
string words = NumberToWords(number);

Console.WriteLine("Number " + number + " by words: " + words);
Console.WriteLine();
#endregion

#region Task-5
Console.WriteLine("Поміняти місцями значення двох змінних (типу int) (без використання 3й)");
int number_1 = 17;
int number_2 = 25;
Console.WriteLine($"Number 1: {number_1}, Number 2: {number_2}");

number_1 *= number_2;
number_2 = number_1 / number_2;
number_1 = number_1 / number_2;
Console.WriteLine($"Number 1: {number_1}, Number 2: {number_2}");

int number_3 = 17;
int number_4 = 25;
Console.WriteLine($"Number 3: {number_3}, Number 4: {number_4}");

number_3 = number_3 ^ number_4;
number_4 = number_4 ^ number_3;
number_3 = number_3 ^ number_4;
Console.WriteLine($"Number 3: {number_3}, Number 4: {number_4}");
#endregion