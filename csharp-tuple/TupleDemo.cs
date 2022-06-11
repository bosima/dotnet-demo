
public class TupleDemo
{
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