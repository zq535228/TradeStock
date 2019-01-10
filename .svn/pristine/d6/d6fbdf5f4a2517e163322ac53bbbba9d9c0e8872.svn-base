using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public static class EvalHelper {
    static readonly Regex Num = new Regex(@"(\-?\d+\.?\d*)");
    static readonly Regex Power = new Regex(Num.ToString() + @"\^" + Num.ToString());
    static readonly Regex AddSub = new Regex(Num.ToString() + "([+-])" + Num.ToString());
    static readonly Regex MulDiv = new Regex(Num.ToString() + "([*/])" + Num.ToString());
    static readonly Regex InnerRegex = new Regex(@"\([^\(\)]+\)");
    static readonly Regex FunctionRegex = new Regex(@"(ln|lg|sin|cos|tan|ctg|sh|ch|th|arcsin|arccos|arctan|not)" + Num.ToString());
    static readonly Regex LBrackets = new Regex(@"\{|\[");
    static readonly Regex RBrackets = new Regex(@"\}|\]");

    /// <summary>计算表达式</summary>
    /// <param name="Expressions">表达式</param>
    public static string DoEval(string Expressions) {
        Expressions = Expressions.ToLower();
        Expressions = Expressions.Replace("pi", Math.PI.ToString()).Replace("e", Math.E.ToString());//替换常量
        Expressions = LBrackets.Replace(Expressions, (Match match) => {
            return "(";
        });//统一使用小括号
        Expressions = RBrackets.Replace(Expressions, (Match match) => {
            return ")";
        });
        if (Check(Expressions)) {
            do {
                Expressions = InnerRegex.Replace(Expressions, (Match match) => {
                    return EvalInner(match.Value.Substring(1, match.Value.Length - 2));
                });
            }
            while (InnerRegex.IsMatch(Expressions));
        }
        return EvalInner(Expressions);
    }

    /// <summary>用于计算不含括号的表达式的值</summary>
    /// <param name="Expressions">要计算的表达式</param>
    /// <returns>计算结果</returns>
    private static string EvalInner(string Expressions) {
        while (FunctionRegex.IsMatch(Expressions)) {
            Expressions = FunctionRegex.Replace(Expressions, (Match match) => {
                return EvalSingle(match.Value);
            });
        }

        while (Power.IsMatch(Expressions)) {
            Expressions = Power.Replace(Expressions, (Match match) => {
                return System.Math.Pow(double.Parse(match.Groups[0].Value), double.Parse(match.Groups[2].Value)).ToString();
            });
        }

        while (MulDiv.IsMatch(Expressions)) {
            Expressions = MulDiv.Replace(Expressions, (Match match) => {
                if (match.Value.Contains("*"))
                    return (double.Parse(match.Groups[1].Value) * double.Parse(match.Groups[3].Value)).ToString();
                else
                    return (double.Parse(match.Groups[1].Value) / double.Parse(match.Groups[3].Value)).ToString();
            });
        }

        while (AddSub.IsMatch(Expressions)) {
            Expressions = AddSub.Replace(Expressions, (Match match) => {
                if (match.Value.Contains("+"))
                    return (double.Parse(match.Groups[1].Value) + double.Parse(match.Groups[3].Value)).ToString();
                else
                    return (double.Parse(match.Groups[1].Value) - double.Parse(match.Groups[3].Value)).ToString();
            });
        }

        return Expressions;
    }

    /// <summary>用于计算单变量函数表达式的值</summary>
    /// <param name="Expressions">要计算的表达式</param>
    /// <returns>计算结果</returns>
    private static string EvalSingle(string Expressions) {
        while (FunctionRegex.IsMatch(Expressions)) {
            Expressions = FunctionRegex.Replace(Expressions, (Match match) => {
                return EvalSingle(match);
            });
        }

        return Expressions;
    }

    private static string EvalSingle(Match match) {
        //ln|lg|sin|cos|tan|ctg|sh|ch|th|arcsin|arccos|arctan
        double Param = double.Parse(match.Groups[2].Value.Trim('(', ')'));
        double Result = double.NaN;

        if (!double.IsNaN(Param)) {
            if (match.Value.StartsWith("ln"))
                Result = System.Math.Log(Param);
            else if (match.Value.StartsWith("lg"))
                Result = System.Math.Log10(Param);
            else if (match.Value.StartsWith("sin"))
                Result = System.Math.Sin(Param * Math.PI / 180);
            else if (match.Value.StartsWith("sqrt"))
                Result = System.Math.Sqrt(Param);
            else if (match.Value.StartsWith("cos"))
                Result = System.Math.Cos(Param * Math.PI / 180);
            else if (match.Value.StartsWith("tan"))
                Result = System.Math.Tan(Param * Math.PI / 180);
            else if (match.Value.StartsWith("ctg"))
                Result = 1 / System.Math.Tan(Param * Math.PI / 180);
            else if (match.Value.StartsWith("sh"))
                Result = System.Math.Sinh(Param);
            else if (match.Value.StartsWith("ch"))
                Result = System.Math.Cosh(Param);
            else if (match.Value.StartsWith("th"))
                Result = System.Math.Tanh(Param);
            else if (match.Value.StartsWith("arcsin"))
                Result = System.Math.Asin(Param);
            else if (match.Value.StartsWith("arccos"))
                Result = System.Math.Acos(Param);
            else if (match.Value.StartsWith("arctan"))
                Result = System.Math.Atan(Param);
        }

        return Result.ToString();
    }

    /// <summary>对表达式进行检查，确保其格式符合要求</summary>
    /// <param name="Expressions">表达式</param>
    /// <returns>是否符合要求</returns>
    public static bool Check(string Expressions) {
        return CheckBrackets(Expressions);
    }

    /// <summary>检查括号是否匹配</summary>
    /// <param name="Expressions">表达式</param>
    /// <returns>是否匹配</returns>
    private static bool CheckBrackets(string Expressions) {
        int i = 0;
        foreach (char c in Expressions) {
            if (c == '(') {
                i++;
            }
            if (c == ')') {
                i--;
            }
        }
        return i == 0;
    }
}
