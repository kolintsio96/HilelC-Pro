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
int[] GetArrayOfIndexes(string sourceString, string separator)
{
    if (separator.Length > sourceString.Length)
    {
        return new int[0];
    }
    int arrayLength = 0;
    for(int i = 0;  i < sourceString.Length; i++)
    {
        if (sourceString[i] == separator[0])
        {
            if (separator.Length == 1)
            {
                arrayLength++;
            } else
            {
                for (int j = 1; j < separator.Length; j++)
                {
                    if (separator[j] != sourceString[i + j])
                    {
                        break;
                    }
                    if (j == separator.Length - 1)
                    {
                        arrayLength++;
                    }
                }
            }
        }
    }
    int[] arrayOfIndexes = new int[arrayLength];

    int currentIndex = 0;
    for (int i = 0; i < sourceString.Length; i++)
    {
        if (sourceString[i] == separator[0])
        {
            if (separator.Length == 1)
            {
                arrayOfIndexes[currentIndex] = i;
                currentIndex++;
            }
            else
            {
                for (int j = 1; j < separator.Length; j++)
                {
                    if (separator[j] != sourceString[i + j])
                    {
                        break;
                    }
                    if (j == separator.Length - 1)
                    {
                        arrayOfIndexes[currentIndex] = i;
                        currentIndex++;
                    }
                }
            }
        }
    }

    return arrayOfIndexes;
}
string[] GetArrayFromString(string sourceString, string separator)
{
    if (separator.Length > sourceString.Length)
    {
        return new string[0];
    }
    int[] arrayOfIndexes = GetArrayOfIndexes(sourceString, separator);
    string[] newArray = new string[arrayOfIndexes.Length + 1];
    for (int i = 0; i < arrayOfIndexes.Length; i++)
    {
        if (i == 0)
        {
            newArray[i] = sourceString.Substring(0, arrayOfIndexes[i]);
        } else
        {
            newArray[i] = sourceString.Substring(arrayOfIndexes[i - 1] + 1, arrayOfIndexes[i] - arrayOfIndexes[i - 1] - 1);

            if (i == arrayOfIndexes.Length - 1)
            {
                newArray[i + 1] = sourceString.Substring(arrayOfIndexes[i] + 1, sourceString.Length - arrayOfIndexes[i] - 1);
            }
        } 
    }
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

Console.WriteLine("Пошук підстроки у строці.");
string sourcestring = ReadString("Enter string: ");
string substr = ReadString("Enter sub string: ");
int[] indexesofsubstr = GetArrayOfIndexes(sourcestring, substr);
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