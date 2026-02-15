using System;
using System.Text;

namespace System.IO
{
	// Token: 0x020007E6 RID: 2022
	internal class CStreamWriter : StreamWriter
	{
		// Token: 0x06004124 RID: 16676 RVA: 0x000FB790 File Offset: 0x000F9990
		public CStreamWriter(Stream stream, Encoding encoding, bool leaveOpen)
			: base(stream, encoding, 1024, leaveOpen)
		{
			this.driver = (TermInfoDriver)ConsoleDriver.driver;
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x000FB7B0 File Offset: 0x000F99B0
		public override void Write(char[] buffer, int index, int count)
		{
			if (count <= 0)
			{
				return;
			}
			if (!this.driver.Initialized)
			{
				try
				{
					base.Write(buffer, index, count);
				}
				catch (IOException)
				{
				}
				return;
			}
			lock (this)
			{
				int num = index + count;
				int num2 = index;
				int num3 = 0;
				do
				{
					char c = buffer[num2++];
					if (this.driver.IsSpecialKey(c))
					{
						if (num3 > 0)
						{
							try
							{
								base.Write(buffer, index, num3);
							}
							catch (IOException)
							{
							}
							num3 = 0;
						}
						this.driver.WriteSpecialKey(c);
						index = num2;
					}
					else
					{
						num3++;
					}
				}
				while (num2 < num);
				if (num3 > 0)
				{
					try
					{
						base.Write(buffer, index, num3);
					}
					catch (IOException)
					{
					}
				}
			}
		}

		// Token: 0x06004126 RID: 16678 RVA: 0x000FB894 File Offset: 0x000F9A94
		public override void Write(char val)
		{
			lock (this)
			{
				try
				{
					if (this.driver.IsSpecialKey(val))
					{
						this.driver.WriteSpecialKey(val);
					}
					else
					{
						this.InternalWriteChar(val);
					}
				}
				catch (IOException)
				{
				}
			}
		}

		// Token: 0x06004127 RID: 16679 RVA: 0x000FB8FC File Offset: 0x000F9AFC
		public void InternalWriteString(string val)
		{
			try
			{
				base.Write(val);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x000FB928 File Offset: 0x000F9B28
		public void InternalWriteChar(char val)
		{
			try
			{
				base.Write(val);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x000FB954 File Offset: 0x000F9B54
		public void InternalWriteChars(char[] buffer, int n)
		{
			try
			{
				base.Write(buffer, 0, n);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x000FB980 File Offset: 0x000F9B80
		public override void Write(char[] val)
		{
			this.Write(val, 0, val.Length);
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x000FB990 File Offset: 0x000F9B90
		public override void Write(string val)
		{
			if (val == null)
			{
				return;
			}
			if (this.driver.Initialized)
			{
				this.Write(val.ToCharArray());
				return;
			}
			try
			{
				base.Write(val);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x000FB9D8 File Offset: 0x000F9BD8
		public override void WriteLine(string val)
		{
			this.Write(val);
			this.Write(this.NewLine);
		}

		// Token: 0x04002124 RID: 8484
		private TermInfoDriver driver;
	}
}
