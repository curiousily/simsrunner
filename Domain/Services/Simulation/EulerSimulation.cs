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

        public ISimulationResult Run()
        {
            ISimulationResult result = new SimpleSimulationResult();
            for (var time = _step; time <= _time; time += _step)
            {
                // for each derivate as d
                // evaluate d and add as next value
            }
            return result;
        }
    }
}