namespace NaughtySpirit.SimsRunner.Domain.Services.Simulation
{
    public class Derivative
    {
        public string Name { get; protected set; }
        public string Expression { get; protected set;}

        public Derivative(string name, string expression)
        {
            Name = name;
            Expression = expression;
        } 
    }
}