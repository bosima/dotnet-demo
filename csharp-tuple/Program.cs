// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, Tuple Demo!");
var demo = new TupleDemo();

var tuple = demo.GetTuple();
Console.WriteLine($"GetTuple result is:{tuple}, include: {tuple.Item1},{tuple.Item2}");

var valueTuple = demo.GetValueTuple();
Console.WriteLine($"GetValueTuple result is: {valueTuple}, include: {valueTuple.Item1},{valueTuple.Item2}");

var namedValueTuple= demo.GetNamedValueTuple();
Console.WriteLine($"GetNamedValueTuple result is: {namedValueTuple}, include: {namedValueTuple.id},{namedValueTuple.name}");

var anonymousValueTuple= demo.GetAnonymousValueTuple();
Console.WriteLine($"GetAnonymousValueTuple result is: {anonymousValueTuple}, include: {anonymousValueTuple.Item1},{anonymousValueTuple.Item2}");

var innerNamedValueTuple= demo.GetInnerNamedValueTuple();
Console.WriteLine($"GetInnerNamedValueTuple result is: {innerNamedValueTuple}, include: {innerNamedValueTuple.Item1},{innerNamedValueTuple.Item2}");

var assignedValueTuple= demo.GetAssignedValueTuple();
Console.WriteLine($"GetAssignedValueTuple result is: {assignedValueTuple}, include: {assignedValueTuple.Item1},{assignedValueTuple.Item2}");
