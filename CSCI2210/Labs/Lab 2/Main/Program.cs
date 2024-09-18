using Operations;

namespace Main
{

    class Program
    {
        static void Main(string[] args)
        {
            MyOperator testOp = new MyOperator();

            int[] intValues = new int[] {-1073741873, -1000, -555, -1, 0, 1, 555, 1000, 2147483147};
            double[] doubleVals = new double[]{-1000000.0, -5235.2352, 0, 1.0, 24.02352352, 2357235.325253};
            
            Console.Write("Values in int[]: ");
            for(int i = 0; i < intValues.Length -1; i++)
            {
                Console.Write($"{intValues[i]}, ");
            }
            Console.WriteLine($"{intValues[^1]}");

            foreach(int i in intValues)
            {
                int OpVal = testOp.Operate(i);
                Console.WriteLine($"IsEven({i}) = {testOp.IsEven(i)}\nOperate({i}) = {OpVal}");
            }


            Console.Write("\n\nValues in double[]: ");
            for(int i = 0; i < doubleVals.Length - 1; i++)
            {
                Console.Write($"{doubleVals[i]}, ");
            }
            Console.WriteLine($"{doubleVals[^1]}");

            foreach(double d in doubleVals)
            {
                double tripVal = testOp.Triple(d);

                Console.WriteLine($"IsValid({d}) = {testOp.IsValid(d)}\nTriple({d}) = {testOp.Triple(d)}");
            }

            int[] badVals = new int[] {int.MinValue, -1073741874, 2147483148};

            foreach(int i in badVals)
            {
                try{
                    Console.WriteLine($"Operate({i}) = {testOp.Operate(i)}");
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Value: {i} caused Exception: {e.Message}");
                }
            }
        }
    }
}
