using Machine.Specifications;

namespace FindTheUnknownDigit.Specs
{
    public class When_Parsing_A_String_Equation
    {
        Establish context = () =>
        {
            inputs = new string[]
            {
                //"12+23=35",
                //"51-3=48",
                //"32*41=1312",
                //"?2*41=1?12",
                //"?3+2?=?6",
                "-5?*-1=5?",
            };
            expects = new string[][]
            {
                //new string[] {"12","+","23","=","35"},
                //new string[] {"51","-","3","=","48" },
                //new string[] {"32","*","41","=","1312"},
                //new string[]{"?2","*","41","=","1?12"},
                //new string[]{"?3","+","2?","=","?6"},
                new string[]{"-5?","*","-1","=","5?"}
            };
            answers = new string[expects.Length][];
        };

        Because of = () =>
        {
            for (var i = 0; i < inputs.Length; i++)
            {
                answers[i] = Runes.ParseEquation(inputs[i]);
            }
        };

        It Should_Return_A_String_Array_Separated_By_Operators = () =>
        {
            for (var i = 0; i < answers.Length; i++)
            {
                for(var j=0; j < answers[i].Length; j++)
                {
                    if (answers[i][j] != expects[i][j])
                    {
                        var ans = answers[i][j];
                        var exp = expects[i][j];
                    }
                    answers[i][j].ShouldEqual(expects[i][j]);
                }
            }
        };

        private static string[] inputs;
        private static string[][] expects;
        private static string[][] answers;
    }

    public class When_Solving_For_Missing_Digit
    {
        Establish context = () =>
        {
            input = new string[][]
            {
                new string[]{ "?2","*","41","=","1?12" },
                new string[]{ "1?","+","2?","=","?6" },
                new string[]{ "4?","-","?","=","40" },
                new string[]{ "1","+","1","=","?" },
                new string[]{ "123","*","45?","=","5?088" },
                new string[]{ "-5?","*","-1","=","5?" },
                new string[]{ "19","-","-45","=","5?" },
                new string[]{ "??","*","??","=","302?" },
                new string[]{ "?","*","11","=","??" },
                new string[]{ "??","*","1","=","??" },
                new string[]{ "??","+","??","=","??" }
            };
            zeros = new int[]
            {
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                1,
                1,
                1,
                1,
            };
            expect = new int[]
            {
                3,
                3,
                1,
                2,
                6,
                0,
                -1,
                5,
                2,
                2,
                -1
            };
            answer = new int[expect.Length];
        };

        Because of = () =>
        {
            for (var i = 0; i < input.Length; i++)
            {
                answer[i] = Runes.Solve(input[i], zeros[i]);
            }
        };

        It Should_Return_The_Missing_Integer = () =>
        {
            for (var i = 0; i < answer.Length; i++)
            {
                if (answer[i] != expect[i])
                {
                    var ans = answer[i];
                    var exp = expect[i];
                }
                answer[i].ShouldEqual(expect[i]);
            }
        };

        private static string[][] input;
        private static int[] zeros;
        private static int[] expect;
        private static int[] answer;
    }

    public class When_Solving_String_Equation
    {
        Establish context = () =>
        {
            input = new string[] 
            {
                //"1+1=?",
                //"123*45?=5?088",
                //"-5?*-1=5?",
                //"19--45=5?",
                //"??*??=302?",
                //"?*11=??",
                //"??*1=??",
                //"??+??=??",
                //"?2*41=1?12",
                //"?3+2?=?6",
                //"-?56373--9216=-?47157",
                "123?45+?=123?45",
                "123?45-?=123?45",
            };
            expect = new int[]
            {
                //2,
                //6,
                //0,
                //-1,
                //5,
                //2,
                //2,
                //-1,
                //3,
                //-1,
                //8,
                0,
                0,
            };
            answer = new int[expect.Length];
        };

        Because of = () =>
        {
            for (var i = 0; i < input.Length; i++)
            {
                answer[i] = Runes.solveExpression(input[i]);
            }
        };

        It Should_Return_The_Digit_Represented_By_Q = () =>
        {
            for(var i=0; i<answer.Length; i++)
            {
                answer[i].ShouldEqual(expect[i]);
            }
        };

        private static string[] input;
        private static int[] expect;
        private static int[] answer;
    }
}