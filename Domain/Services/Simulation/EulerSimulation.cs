using System;
using System.Collections.Generic;
using System.Linq;
using NCalc;
using NaughtySpirit.SimsRunner.Domain.Services.Simulation.Result;

namespace NaughtySpirit.SimsRunner.Domain.Services.Simulation
{
    public class EulerSimulation : ISimulation
    {
        private readonly string _formula;
        private readonly int _time;
        private readonly int _step;

        public EulerSimulation(string formula, int time, int step)
        {
            _formula = formula;
            _time = time;
            _step = step;
        }

        private IList<Variable> GetVariables(String formula)
        {
            return LinesWithEqualSign(formula).Select(line => new Variable(GetName(line), GetValue(line))).ToList();
        }

        private IList<Derivative> GetDerivatives(String formula)
        {
            return LinesWithEqualSign(formula).Select(line => new Derivative(GetName(line), GetExpression(line))).ToList();
        }

        private static IEnumerable<string> LinesWithEqualSign(string formula)
        {
            var lines = formula.Split(new[] {Environment.NewLine}, StringSplitOptions.None).Where(line => line.Contains("'="));
            return lines;
        }

        private double GetValue(string line)
        {
            var expression = line.Substring(FirstEqualIndexIn(line) + 1);
            return Convert.ToDouble(new Expression(expression).Evaluate());
        }

        private string GetExpression(string line)
        {
            return line.Substring(FirstEqualIndexIn(line) + 2);
        }

        private string GetName(string line)
        {
            return line.Substring(0, FirstEqualIndexIn(line));
        }

        private static int FirstEqualIndexIn(string line)
        {
            return line.IndexOf("'=", StringComparison.Ordinal);
        }

        public ISimulationResult Run()
        {
            var result = new SimpleSimulationResult();
            var variable = GetVariables(_formula);
            var derivatives = GetDerivatives(_formula);
            
//            result.AddPoint()
            for (var time = _step; time <= _time; time += _step)
            {
                // for each derivate as d
                // evaluate d and add as next value
            }
            return result;
        }
    }
}