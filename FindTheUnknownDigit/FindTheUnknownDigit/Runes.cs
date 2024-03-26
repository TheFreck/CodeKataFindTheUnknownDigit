using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace FindTheUnknownDigit
{
    public class Runes
    {
        public static string[] ParseEquation(string v)
        {
            var left = v[0].ToString();
            var right = string.Empty;
            var answer = string.Empty;
            var leftComplete = false;
            var rightComplete = false;
            var op = string.Empty;
            for(var i=1; i<v.Length; i++)
            {
                if (v[i] == '=')
                {
                    rightComplete = true;
                    continue;
                }
                else if (!leftComplete && (v[i] == '+' || v[i] == '-' || v[i] == '*'))
                {
                    leftComplete = true;
                    op = v[i].ToString();
                    continue;
                }
                if (rightComplete) answer += v[i];
                else if (leftComplete) right += v[i];
                else left += v[i];
            }
            return new string[] { left, op, right, "=", answer };
        }

        public static int Solve(string[] strings, int zero)
        {
            var allNumbers = new List<int[]>();
            var usedNumbers = new HashSet<int>();
            for(var testDigit=0; testDigit<10; testDigit++)
            {
                if (usedNumbers.Contains(testDigit)) continue;
                var numbers = new int[3];
                for(var j=0; j<5; j += 2)
                {
                    var numberString = string.Empty;
                    for (var k = 0; k < strings[j].Length; k++)
                    {
                        if (strings[j][k] == '?') numberString += testDigit;
                        else if (k == 0 || strings[j][k - 1] == '+' || strings[j][k - 1] == '-' || strings[j][k - 1] == '*' || strings[j][k-1] == '=') numberString += strings[j][k];
                        else numberString += strings[j][k];
                        if (strings[j][k].ToString()[0] == testDigit.ToString()[0]) usedNumbers.Add(int.Parse(strings[j][k].ToString()));
                    }
                    numbers[j / 2] = int.Parse(numberString);
                }
                allNumbers.Add(numbers);
            }
            var missingDigit = -1;
            for(var i= allNumbers.Count-1; i>=zero; i--)
            {
                if (usedNumbers.Contains(i)) continue;
                var first = allNumbers[i][0];
                var second = allNumbers[i][1];
                var answer = allNumbers[i][2];
                switch (strings[1])
                {
                    case "+":
                        if (first + second == answer) missingDigit = i;
                        break;
                    case "-":
                        if(first - second == answer) missingDigit = i;
                        break;
                    case "*":
                        if(first*second== answer) missingDigit = i;
                        break;
                    default:
                        break;
                }
            }
            return missingDigit;
        }

        public static int solveExpression(string expression)
        {
            var reggie = new Regex(@"(?:\?)\A\?|[?]{2}|\-\?");
            var parsed = ParseEquation(expression);
            var zero = 0;
            for(var i=0; i<parsed.Length; i += 2)
            {
                if (parsed[i][0] == '-' && parsed[i][1] == '?' || parsed[i][0] == '?' && parsed[i] != "?") zero = 1;
            }
            if(expression.Contains("??")) zero = 1;
            return Solve(parsed, zero);
        }
    }
}
