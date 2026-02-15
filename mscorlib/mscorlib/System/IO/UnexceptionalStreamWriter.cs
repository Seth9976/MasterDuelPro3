using System;
using System.Text;

namespace System.IO
{
	// Token: 0x020007E4 RID: 2020
	internal class UnexceptionalStreamWriter : StreamWriter
	{
		// Token: 0x06004118 RID: 16664 RVA: 0x000FB544 File Offset: 0x000F9744
		public UnexceptionalStreamWriter(Stream stream, Encoding encoding)
			: base(stream, encoding, 1024, true)
		{
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x000FB554 File Offset: 0x000F9754
		public override void Flush()
		{
			try
			{
				base.Flush();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x000FB57C File Offset: 0x000F977C
		public override void Write(char[] buffer, int index, int count)
		{
			try
			{
				base.Write(buffer, index, count);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x000FB5A8 File Offset: 0x000F97A8
		public override void Write(char value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x000FB5D4 File Offset: 0x000F97D4
		public override void Write(char[] value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x000FB600 File Offset: 0x000F9800
		public override void Write(string value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}
	}
}
