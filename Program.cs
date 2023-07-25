namespace DependencyInversionPrincipleDemo
{

    public enum Relationship
    {
        Parent,
        Child,
        Siblings
    }

    public class Person
    {
        public string Name;
    }

    public class Relationships : IResearchanalysis
    {
        private List<(Person,Relationship,Person)> _relationships = new List<(Person,Relationship,Person)> ();

        public void AddRealtionShips(Person parent,Person child)
        {
            _relationships.Add((parent, Relationship.Parent, child));
            _relationships.Add ((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildren(string name)
        {
            foreach (var p in _relationships.Where(x => x.Item1.Name == name
             && x.Item2 == Relationship.Parent))
            {
                yield return p.Item3;
            }
        }
    }

    public interface IResearchanalysis
    {
        public IEnumerable<Person> FindAllChildren(string  name);
    }
    internal class Program
    {
        //public Program(Relationships relationships)
        //{
        //    var relation = relationships.Relations;

        //    foreach(var p in relation.Where(x => x.Item1.Name == "John" 
        //    && x.Item2 == Relationship.Parent))
        //    {
        //        Console.WriteLine($"John has a child {p.Item3.Name}");
        //    }

        //}

        public Program(IResearchanalysis researchanalysis)
        {
           foreach(var p in  researchanalysis.FindAllChildren("John"))
            {
                Console.WriteLine($"John has a child named  {p.Name}");
            }
        }
        static void Main(string[] args)
        {
            
            var parent = new Person () { Name = "John"};
            var child1 = new Person() { Name = "Joe" };
            var child2 = new Person() { Name = "Janet" };

            var relationships = new Relationships ();

            relationships.AddRealtionShips(parent, child1);
            relationships.AddRealtionShips(parent, child2);


            new Program(relationships);
            
        }

    }
}