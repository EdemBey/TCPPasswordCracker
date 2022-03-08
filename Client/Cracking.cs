using System.Diagnostics;
using System.Security.Cryptography;
using BrutServer;
using BrutServer.Models;

namespace Client;

public class Cracking
    {
        private readonly HashAlgorithm _messageDigest;
        public Cracking()
        {
            _messageDigest = SHA1.Create();
        }
        public void RunCracking(IEnumerable<UserInfo> userInfos)
        {
            var stopwatch = Stopwatch.StartNew();
            Console.WriteLine("passwd opened");

            var result = new List<UserInfoClearText>();

            using FileStream fs = new FileStream("webster-dictionary.txt", FileMode.Open, FileAccess.Read);
            using StreamReader dictionary = new StreamReader(fs);
            while (!dictionary.EndOfStream)
            {
                var dictionaryEntry = dictionary.ReadLine();
                var partialResult = CheckWordWithVariations(dictionaryEntry, userInfos);
                result.AddRange(partialResult);
            }

            stopwatch.Stop();
            Console.WriteLine(string.Join(", ", result));
            Console.WriteLine();
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
        }
        private IEnumerable<UserInfoClearText> CheckWordWithVariations(string? dictionaryEntry, IEnumerable<UserInfo> userInfos)
        {
            var result = new List<UserInfoClearText>(); //might be empty
            var possiblePassword = dictionaryEntry;
            var enumerable = userInfos.ToList();
            var partialResult = CheckSingleWord(enumerable, possiblePassword);
            result.AddRange(partialResult);

            var possiblePasswordUpperCase = dictionaryEntry?.ToUpper();
            var partialResultUpperCase = CheckSingleWord(enumerable, possiblePasswordUpperCase);
            result.AddRange(partialResultUpperCase);

            var possiblePasswordCapitalized = StringUtilities.Capitalize(dictionaryEntry);
            var partialResultCapitalized = CheckSingleWord(enumerable, possiblePasswordCapitalized);
            result.AddRange(partialResultCapitalized);

            var possiblePasswordReverse = StringUtilities.Reverse(dictionaryEntry);
            var partialResultReverse = CheckSingleWord(enumerable, possiblePasswordReverse);
            result.AddRange(partialResultReverse);

            for (var i = 0; i < 100; i++)
            {
                var possiblePasswordEndDigit = dictionaryEntry + i;
                var partialResultEndDigit = CheckSingleWord(enumerable, possiblePasswordEndDigit);
                result.AddRange(partialResultEndDigit);
            }

            for (var i = 0; i < 100; i++)
            {
                var possiblePasswordStartDigit = i + dictionaryEntry;
                var partialResultStartDigit = CheckSingleWord(enumerable, possiblePasswordStartDigit);
                result.AddRange(partialResultStartDigit);
            }

            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    var possiblePasswordStartEndDigit = i + dictionaryEntry + j;
                    var partialResultStartEndDigit = CheckSingleWord(enumerable, possiblePasswordStartEndDigit);
                    result.AddRange(partialResultStartEndDigit);
                }
            }

            return result;
        }
        private IEnumerable<UserInfoClearText> CheckSingleWord(IEnumerable<UserInfo> userInfos, string? possiblePassword)
        {
            var charArray = possiblePassword?.ToCharArray();
            var passwordAsBytes = Array.ConvertAll(charArray ?? Array.Empty<char>(), PasswordFileHandler.GetConverter());

            var encryptedPassword = _messageDigest.ComputeHash(passwordAsBytes);

            var results = new List<UserInfoClearText>();

            foreach (var userInfo in userInfos)
            {
                if (!CompareBytes(userInfo.EncryptedPassword, encryptedPassword)) continue;
                results.Add(new UserInfoClearText(userInfo.Username, possiblePassword));
                Console.WriteLine(userInfo.Username + " " + possiblePassword);
            }
            return results;
        }
        private static bool CompareBytes(ICollection<byte> firstArray, IList<byte> secondArray)
        {
            if (firstArray.Count != secondArray.Count)
            {
                return false;
            }
            return !firstArray.Where((t, i) => t != secondArray[i]).Any();
        }
    }