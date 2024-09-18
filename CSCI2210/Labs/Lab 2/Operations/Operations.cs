using IOperationsNS;

namespace Operations
{
    public class MyOperator : IOperations 
    {
		public bool IsEven(int x)
		{
			return x % 2 == 0;
		}

		public bool IsValid(double x)
		{
			return (x >= 0) && (x <= 100);
		}

		public double Triple(double x)
		{
			return 3.0 * x;
		}

		public int Operate(int x)
		{
			if(x >= int.MinValue && x <= -1073741874) throw new ArgumentOutOfRangeException("Invalid Value as Operate will cause Overflow");
			if(x < 0) return 2 * x + 100;
			if(x >= 0 && x < 1000) return 5 * x;
			if(x >= 1000 && x < 2147483148) return x + 500;
			
			throw new ArgumentOutOfRangeException("Invalid Value as Operate will cause Overflow");
		}

    }
}
