
public class TupleDemo
{
    public void Run()
    {
        var tuple = GetTuple();
        Console.WriteLine($"GetTuple result is:{tuple}, include: {tuple.Item1},{tuple.Item2}");

        var valueTuple = GetValueTuple();
        Console.WriteLine($"GetValueTuple result is: {valueTuple}, include: {valueTuple.Item1},{valueTuple.Item2}");

        var namedValueTuple = GetNamedValueTuple();
        Console.WriteLine($"GetNamedValueTuple result is: {namedValueTuple}, include: {namedValueTuple.id},{namedValueTuple.name}");

        var anonymousValueTuple = GetAnonymousValueTuple();
        Console.WriteLine($"GetAnonymousValueTuple result is: {anonymousValueTuple}, include: {anonymousValueTuple.Item1},{anonymousValueTuple.Item2}");

        var innerNamedValueTuple = GetInnerNamedValueTuple();
        Console.WriteLine($"GetInnerNamedValueTuple result is: {innerNamedValueTuple}, include: {innerNamedValueTuple.Item1},{innerNamedValueTuple.Item2}");

        var assignedValueTuple = GetAssignedValueTuple();
        Console.WriteLine($"GetAssignedValueTuple result is: {assignedValueTuple}, include: {assignedValueTuple.Item1},{assignedValueTuple.Item2}");
    }

    public Tuple<int, string> GetTuple()
    {
        return Tuple.Create(1, "Hello Tuple!");
    }

    public ValueTuple<int, string> GetValueTuple()
    {
        return ValueTuple.Create(2, "Hello ValueTuple!");
    }

    public (int id, string name) GetNamedValueTuple()
    {
        return (3, "Hello Named ValueTuple!");
    }

    public (int, string) GetAnonymousValueTuple()
    {
        return (4, "Hello Anonymous ValueTuple!");
    }

    public (int, string) GetInnerNamedValueTuple()
    {
        var id = 5;
        var name = "Hello Inner Named ValueTuple!";
        return (id, name);
    }

    public (int, string) GetAssignedValueTuple()
    {
        (int, string) t = (6, "Hello Assigned ValueTuple!");
        return t;
    }
}