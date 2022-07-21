// See https://aka.ms/new-console-template for more information
using Monads;

Func<Int32, Int32, Maybe<Int32>> safeDiv = (x, y) => Maybe<Int32>.Unit(() => x / y, y != 0);
Func<Int32, Int32, Carry<Maybe<Int32>, String>> safeDivLog = (x, y) => Carry<Maybe<Int32>, String>.Unit(safeDiv(x, y), $"performed {x}/{y}");

Func<Int32, Int32, Boolean> canAdd = (x, y) => !(x > 0 && y > 0 && y > (Int32.MaxValue - x)) && !(x < 0 && y < 0 && y < (Int32.MinValue - x));
Func<Int32, Int32, Maybe<Int32>> safeAdd = (x, y) => Maybe<Int32>.Unit(() => x + y, canAdd(x, y));
Func<Int32, Carry<Maybe<Int32>, String>> addLots = i => Carry<Maybe<Int32>, String>.Unit(safeAdd(i, int.MaxValue - 10), $"performed {i}+Lots");

Func<Carry<Int32, String>> readOp = () =>
{
    var operand = Int32.Parse(Console.ReadLine());
    return Carry<Int32, String>.Unit(operand, $"read {operand}");
};

while (true)
{
    var safeOverflow = readOp().Bind(addLots);
    Console.WriteLine(safeOverflow);

    var safeDivided = readOp().Bind(a => readOp().Bind(b => safeDivLog(a, b)));
    Console.WriteLine(safeDivided);
}