using System.Text;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        #region task-1
        Console.WriteLine("Реверс строки/масиву. без додаткового масиву. складність о(n).");
        string str = ReadString("Enter string: ");
        Console.WriteLine($"String: {str}");
        Console.WriteLine($"Reverse string: {Reverse(str)}");
        Console.WriteLine();
        #endregion

        #region Task-2
        Console.WriteLine("Фільтрування неприпустимих слів у строці. Має бути саме слова, а не частини слів.");
        string[] exceptWords = { "fuck", "fucking", "ass" };
        string str2 = ReadString("Enter string: ");
        string filteredString = FilterExceptWords(str2, exceptWords);
        Console.WriteLine($"String: {str2}");
        Console.WriteLine($"Filtered string {filteredString}");
        Console.WriteLine();
        #endregion

        #region Task-3
        Console.WriteLine("Генератор випадкових символів. На вхід кількість символів, на виході рядок з випадковими символами.");
        int countOfChars = ReadNumber("Enter count of char: ");
        Console.WriteLine($"Randon string: {GetRandomString(countOfChars)}");
        Console.WriteLine();
        #endregion

        #region Task-4
        Console.WriteLine("'Дірка' (пропущене число) у масиві.");
        int[] array = { 1, 2, 3, 4, 6, 7, 8, 0, 9, 5 };
        int missingNumber = FindMissingNumber(array);
        Console.WriteLine($"Array of numbers: {string.Join(",", array)}");
        Console.WriteLine("Missing number: " + missingNumber);
        Console.WriteLine();
        #endregion

        #region Task-5
        Console.WriteLine("Найпростіше стиснення ланцюжка ДНК.");
        int lengthOfDna = ReadNumber("Enter length of dna: ");
        string dna = GetRandonDna(lengthOfDna);
        Console.WriteLine($"DNA: {dna}");
        List<int> compressedDna = CompessDna(dna);
        Console.WriteLine($"compressed dna: {string.Join(",", compressedDna)}");
        string deCompressedDna = DeCompessDna(compressedDna);
        Console.WriteLine($"decompressed dna: {deCompressedDna}");
        Console.WriteLine();
        #endregion

        #region Task-6
        Console.WriteLine("Симетричне шифрування.");
        string str3 = ReadString("Enter string for encoding: ");
        string key = GetRandomString(str3.Length);
        byte[] encodeResult = EncodeData(str3, key);
        string decodeResult = DecodeData(encodeResult, key); 
        Console.WriteLine($"String for encoding: {str3}");
        Console.WriteLine($"Encode result: {string.Join(',', encodeResult)}");
        Console.WriteLine($"Decode result: {decodeResult}");
        #endregion
    }

    private static byte[] EncodeData(string data, string key)
    {
        byte[] bytes = new byte[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            bytes[i] = (byte)(data[i] ^ key[i]);
        }

        return bytes;
    }
    
    private static string DecodeData(byte[] data, string key)
    {
        char[] bytes = new char[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            bytes[i] = (char)(data[i] ^ key[i]);
        }
        return new string(bytes);
    }

    private static List<int> CompessDna(string dna, bool compress = false)
    {
        List<int> bytesList = new List<int>();
        int b = 0;

        for (int i = 0; i < dna.Length; i++)
        {
            switch(dna[i])
            {
                case 'A':
                    b = b | 0;
                    break;
                case 'C':
                    b = b | 1;
                    break;
                case 'G':
                    b = b | 2;
                    break;
                case 'T':
                    b = b | 3;
                    break;
            }
            
            if ((i + 1) % 4 == 0)
            {
                bytesList.Add(b);
                b = 0;
            } else
            {
                b = b << 2;
            }
        }
        return bytesList;
    }
    
    private static string DeCompessDna(List<int> compressedDna)
    {
        List<char> charList = new List<char>();
        for (int i = compressedDna.Count - 1; i > -1; i--)
        {
            int b = compressedDna[i];
            for (int j = 0; j < 4; j++)
            {
                switch (b & 0b11)
                {
                    case 0b00:
                        charList.Insert(0, 'A');
                        break;
                    case 0b01:
                        charList.Insert(0, 'C');
                        break;
                    case 0b10:
                        charList.Insert(0, 'G');
                        break;
                    case 0b11:
                        charList.Insert(0, 'T');
                        break;
                }
                b = b >> 2;
            }
        }
        return string.Join("", charList);
    }

    private static string GetRandonDna(int lengthOfDna)
    {
        char[] arrayOfChars = { 'A', 'C', 'G', 'T' };
        char[] randomDnaArray = new char[lengthOfDna];
        Random random = new Random();
        for (int i = 0; i < randomDnaArray.Length; i++)
        {
            randomDnaArray[i] = arrayOfChars[random.Next(0, 4)];
        }
        return new string(randomDnaArray); ;
    }

    private static string GetRandomString(int countOfChars)
    {
        char[] charArray = new char[countOfChars];
        for (int i = 0; i < charArray.Length; i++)
        {
            Random random = new Random();
            int index = random.Next(65, 122);
            charArray[i] = (char)index;
        }
        return new string(charArray);
    }

    private static string FilterExceptWords(string sourceString, string[] exceptWords)
    {
        char[] charArray = sourceString.ToCharArray();
        char symbol = '*';
        for (int i = 0; i < exceptWords.Length; i++)
        {
            int[] indexesOfExpectWords = GetArrayOfIndexes(sourceString, exceptWords[i]);
            if(indexesOfExpectWords.Length > 0)
            {
                for (int j = 0; j < indexesOfExpectWords.Length; j++)
                {
                    for(int k = 0; k < exceptWords[i].Length; k++)
                    {
                        charArray[indexesOfExpectWords[j] + k] = symbol;
                    }
                }
            }
        }
        return new string(charArray);
    }

    private static string ReadString(string message)
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

    private static string Reverse(string str)
    {
        char[] charArray = new char[str.Length];
        for (int i = 0; i < Math.Ceiling((double)str.Length / 2); i++)
        {
            charArray[i] = str[str.Length - i - 1];
            charArray[str.Length - i - 1] = str[i];
        }
        return new string(charArray);
    }

    private static int[] GetArrayOfIndexes(string sourceString, string subStr)
    {
        bool notChar(char symbol)
        {
            return !(Char.ToUpper(symbol) >= 'A' && Char.ToUpper(symbol) <= 'Z');    
        }
        if (subStr.Length > sourceString.Length)
        {
            return new int[0];
        }
        int arrayLength = 0;
        for (int i = 0; i < sourceString.Length; i++)
        {
            bool isStartOfWord = ((i != 0 && notChar(sourceString[i - 1])) || i == 0);
            bool isEndOfWord = ((i + subStr.Length - 1) == sourceString.Length - 1) || ((i + subStr.Length) < sourceString.Length - 1 && notChar(sourceString[i + subStr.Length]));
            
            if (Char.ToUpper(sourceString[i]) == Char.ToUpper(subStr[0]) && isStartOfWord && isEndOfWord)
            {
                if (subStr.Length == 1)
                {
                    arrayLength++;
                }
                else
                {
                    for (int j = 1; j < subStr.Length; j++)
                    {
                        
                        if (subStr[j] != sourceString[i + j])
                        {
                            break;
                        }
                        
                        if (j == subStr.Length - 1)
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
            bool isStartOfWord = ((i != 0 && notChar(sourceString[i - 1])) || i == 0);
            bool isEndOfWord = ((i + subStr.Length - 1) == sourceString.Length - 1) || ((i + subStr.Length) < sourceString.Length - 1 && notChar(sourceString[i + subStr.Length]));

            if (sourceString[i] == subStr[0] && isStartOfWord && isEndOfWord)
            {
                if (subStr.Length == 1)
                {
                    arrayOfIndexes[currentIndex] = i;
                    currentIndex++;
                }
                else
                {
                    for (int j = 1; j < subStr.Length; j++)
                    {

                        if (subStr[j] != sourceString[i + j])
                        {
                            break;
                        }

                        if (j == subStr.Length - 1)
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

    private static int ReadNumber(string message)
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
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return ReadNumber(message);
        }
    }
    
    private static int FindMissingNumber(int[] array)
    {
        int xorSum = 0;

        for (int i = 0; i < array.Length; i++)
        {
            xorSum ^= array[i];
        }

        for (int i = 1; i <= array.Length; i++)
        {
            xorSum ^= i;
        }

        return xorSum;
    }
}