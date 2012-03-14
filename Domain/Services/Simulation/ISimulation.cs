using NaughtySpirit.SimsRunner.Domain.Services.Simulation.Result;

namespace NaughtySpirit.SimsRunner.Domain.Services.Simulation
{
    public interface ISimulation
    {
        ISimulationResult Run();
    }
}