using System;
using System.Collections.Generic;
using System.Globalization;
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

        private IList<Variable> VariablesOf(String formula)
        {
            return LinesWithEqualSign(formula).Select(line => new Variable(GetName(line), GetValue(line))).ToList();
        }

        private IList<Derivative> DerivativesOf(String formula)
        {
            return LinesWithEqualSign(formula).Select(line => new Derivative(GetName(line), GetExpression(line))).ToList();
        }

        private static IEnumerable<string> LinesWithEqualSign(string formula)
        {
            return formula.Split(new[] {Environment.NewLine}, StringSplitOptions.None).Where(line => line.Contains("'="));
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
            var variables = VariablesOf(_formula);
            var derivatives = DerivativesOf(_formula);
            
            for (var time = _step; time <= _time; time += _step)
            {
                foreach (var derivative in derivatives)
                {
                    var expression = derivative.Expression;
                    foreach (var variable in variables)
                    {
                        expression = expression.Replace(variable.Name, variable.GetValueString());
                    }
                    var variableValue = variables.First(var => var.Name == derivative.Name).Value;
                    var nextValue = variableValue + _step * Convert.ToDouble(new Expression(expression).Evaluate());
                    result.AddPoint(derivative.Name, nextValue);
                }
                //TODO rearange points here
            }
            return result;
        }
    }
}