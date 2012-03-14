namespace NaughtySpirit.SimsRunner.Domain.Services.Simulation
{
    public class Variable
    {
        public string Name { get; protected set; }
        public double Value { get; protected set;}

        public Variable(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
}