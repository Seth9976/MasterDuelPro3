using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x020007E5 RID: 2021
	internal class CStreamReader : StreamReader
	{
		// Token: 0x0600411E RID: 16670 RVA: 0x000FB62C File Offset: 0x000F982C
		public CStreamReader(Stream stream, Encoding encoding)
			: base(stream, encoding)
		{
			this.driver = (TermInfoDriver)ConsoleDriver.driver;
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x000FB648 File Offset: 0x000F9848
		public override int Peek()
		{
			try
			{
				return base.Peek();
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x000FB674 File Offset: 0x000F9874
		public override int Read()
		{
			try
			{
				return (int)Console.ReadKey().KeyChar;
			}
			catch (IOException)
			{
			}
			return -1;
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x000FB6A8 File Offset: 0x000F98A8
		public override int Read([In] [Out] char[] dest, int index, int count)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (index > dest.Length - count)
			{
				throw new ArgumentException("index + count > dest.Length");
			}
			try
			{
				return this.driver.Read(dest, index, count);
			}
			catch (IOException)
			{
			}
			return 0;
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x000FB728 File Offset: 0x000F9928
		public override string ReadLine()
		{
			try
			{
				return this.driver.ReadLine();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x000FB75C File Offset: 0x000F995C
		public override string ReadToEnd()
		{
			try
			{
				return this.driver.ReadToEnd();
			}
			catch (IOException)
			{
			}
			return null;
		}

		// Token: 0x04002123 RID: 8483
		private TermInfoDriver driver;
	}
}
