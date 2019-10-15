using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Jopalesha.Common.Client.Http.Components
{
    internal static class ChallengeSolver
    {
        public static ChallengeSolution Solve(string challengePageContent, string targetHost)
        {
            var answer = DecodeSecretNumber(challengePageContent, targetHost);
            var verificationCode = Regex.Match(challengePageContent, "name=\"jschl_vc\" value=\"(?<jschl_vc>[^\"]+)")
                .Groups["jschl_vc"].Value;
            var pass = Regex.Match(challengePageContent, "name=\"pass\" value=\"(?<pass>[^\"]+)").Groups["pass"]
                .Value;
            return new ChallengeSolution(
                Regex.Match(challengePageContent, "id=\"challenge-form\" action=\"(?<action>[^\"]+)").Groups["action"]
                    .Value, verificationCode, pass, answer);
        }

        private static int DecodeSecretNumber(string challengePageContent, string targetHost)
        {
            var input = Regex
                .Matches(challengePageContent, "<script\\b[^>]*>(?<Content>.*?)<\\/script>", RegexOptions.Singleline)
                .Cast<Match>().Select(m => m.Groups["Content"].Value).First(c => c.Contains("jschl-answer"));
            var seed = DeobfuscateNumber(Regex.Match(input, ":(?<Number>[\\(\\)\\+\\!\\[\\]]+)").Groups["Number"]
                .Value);
            return Regex.Matches(input, "(?<Operator>[\\+\\-\\*\\/]{1})\\=(?<Number>[\\(\\)\\+\\!\\[\\]]+)")
                       .Cast<Match>()
                       .Select(s =>
                           new Tuple<string, int>(s.Groups["Operator"].Value,
                               DeobfuscateNumber(s.Groups["Number"].Value)))
                       .Aggregate(seed, ApplyDecodingStep) + targetHost.Length;
        }

        private static int DeobfuscateNumber(string obfuscatedNumber)
        {
            var str = SimplifyObfuscatedNumber(obfuscatedNumber);
            if (!str.Contains("("))
                return CountOnes(str);
            return int.Parse(Regex.Matches(str, "\\([1+[\\]]+\\)").Cast<Match>().Select(m => CountOnes(m.Value))
                .Aggregate(string.Empty, (number, digit) => number + digit));
        }

        private static string SimplifyObfuscatedNumber(string obfuscatedNumber)
        {
            return obfuscatedNumber.Replace("!![]", "1").Replace("!+[]", "1");
        }

        private static int CountOnes(string obfuscatedNumber)
        {
            return obfuscatedNumber.ToCharArray().Count(c => c == '1');
        }

        private static int ApplyDecodingStep(int number, Tuple<string, int> step)
        {
            string str1 = step.Item1;
            int num = step.Item2;
            string str2 = str1;
            if (str2 == "+")
                return number + num;
            if (str2 == "-")
                return number - num;
            if (str2 == "*")
                return number * num;
            if (str2 == "/")
                return number / num;
            throw new ArgumentOutOfRangeException($"Unknown operator: {str1}");
        }
    }
}
