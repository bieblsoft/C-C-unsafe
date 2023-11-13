using System;

namespace ConsoleApplication2
{
	using System;
	class Test
	{
		// The unsafe keyword allows pointers to be used within
		// the following method:
		static unsafe void UnsafeCopy(byte[] src, int srcIndex,
			byte[] dst, int dstIndex, int count)
		{
			if (src == null || srcIndex < 0 ||
				dst == null || dstIndex < 0 || count < 0)
			{
				throw new ArgumentException();
			}
			int srcLen = src.Length;
			int dstLen = dst.Length;
			if (srcLen - srcIndex < count ||
				dstLen - dstIndex < count)
			{
				throw new ArgumentException();
			}
			fixed (byte* pSrc = src, pDst = dst)
			{
				byte* ps = pSrc;
				byte* pd = pDst;
				for (int n = count >> 2; n != 0; n--)
				{
					*((int*)pd) = *((int*)ps);
					pd += 4;
					ps += 4;
				}
				for (count &= 3; count != 0; count--)
				{
					*pd = *ps;
					pd++;
					ps++;
				}
			}
		}
		static void SafeCopy(byte[] src, int srcIndex,
			byte[] dst, int dstIndex, int count)
		{
			for(int i = 0; i<count; i++)
			{
				dst[dstIndex + i] = src[srcIndex + i];
			}
		}
		static void Main(string[] args) 
		{
			byte[] a = new byte[5000000];
			byte[] b = new byte[5000000];
			for(int i=0; i<5000000; ++i) 
			{
				a[i] = (byte)32;
			}
			
			string output;
			Console.WriteLine("SafeCopy Code:");
			long start = Environment.TickCount;
			SafeCopy(a, 0, b, 0, 5000000);
			long end = Environment.TickCount;
			output  = Convert.ToString(end - start);
			Console.WriteLine(output);
			
			Console.WriteLine("   **********   ");
			Console.WriteLine("UnsafeCopy Code:");
			start = Environment.TickCount;
			UnsafeCopy(a, 0, b, 0, 5000000);
			end  = Environment.TickCount;
			output = Convert.ToString(end - start);
			Console.WriteLine(output);

			Console.ReadLine();
			
		}
	}
}
