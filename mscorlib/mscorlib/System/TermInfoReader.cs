using System;
using System.IO;
using System.Text;

namespace System
{
	// Token: 0x020001E7 RID: 487
	internal class TermInfoReader
	{
		// Token: 0x060012F5 RID: 4853 RVA: 0x0004DA88 File Offset: 0x0004BC88
		public TermInfoReader(string term, string filename)
		{
			using (FileStream fileStream = File.OpenRead(filename))
			{
				long length = fileStream.Length;
				if (length > 4096L)
				{
					throw new Exception("File must be smaller than 4K");
				}
				this.buffer = new byte[(int)length];
				if (fileStream.Read(this.buffer, 0, this.buffer.Length) != this.buffer.Length)
				{
					throw new Exception("Short read");
				}
				this.ReadHeader(this.buffer, ref this.booleansOffset);
				this.ReadNames(this.buffer, ref this.booleansOffset);
			}
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0004DB34 File Offset: 0x0004BD34
		public TermInfoReader(string term, byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.buffer = buffer;
			this.ReadHeader(buffer, ref this.booleansOffset);
			this.ReadNames(buffer, ref this.booleansOffset);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0004DB6B File Offset: 0x0004BD6B
		private void DetermineVersion(short magic)
		{
			if (magic == 282)
			{
				this.intOffset = 2;
				return;
			}
			if (magic == 542)
			{
				this.intOffset = 4;
				return;
			}
			throw new Exception(string.Format("Magic number is unexpected: {0}", magic));
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0004DBA4 File Offset: 0x0004BDA4
		private void ReadHeader(byte[] buffer, ref int position)
		{
			short @int = this.GetInt16(buffer, position);
			position += 2;
			this.DetermineVersion(@int);
			this.GetInt16(buffer, position);
			position += 2;
			this.boolSize = (int)this.GetInt16(buffer, position);
			position += 2;
			this.numSize = (int)this.GetInt16(buffer, position);
			position += 2;
			this.strOffsets = (int)this.GetInt16(buffer, position);
			position += 2;
			this.GetInt16(buffer, position);
			position += 2;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0004DC28 File Offset: 0x0004BE28
		private void ReadNames(byte[] buffer, ref int position)
		{
			string @string = this.GetString(buffer, position);
			position += @string.Length + 1;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0004DC4C File Offset: 0x0004BE4C
		public int Get(TermInfoNumbers number)
		{
			if (number < TermInfoNumbers.Columns || number >= TermInfoNumbers.Last || number > (TermInfoNumbers)this.numSize)
			{
				return -1;
			}
			int num = this.booleansOffset + this.boolSize;
			if (num % 2 == 1)
			{
				num++;
			}
			num = (int)(num + number * (TermInfoNumbers)this.intOffset);
			return (int)this.GetInt16(this.buffer, num);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0004DCA0 File Offset: 0x0004BEA0
		public string Get(TermInfoStrings tstr)
		{
			if (tstr < TermInfoStrings.BackTab || tstr >= TermInfoStrings.Last || tstr > (TermInfoStrings)this.strOffsets)
			{
				return null;
			}
			int num = this.booleansOffset + this.boolSize;
			if (num % 2 == 1)
			{
				num++;
			}
			num += this.numSize * this.intOffset;
			int @int = (int)this.GetInt16(this.buffer, (int)(num + tstr * TermInfoStrings.CarriageReturn));
			if (@int == -1)
			{
				return null;
			}
			return this.GetString(this.buffer, num + this.strOffsets * 2 + @int);
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0004DD20 File Offset: 0x0004BF20
		public byte[] GetStringBytes(TermInfoStrings tstr)
		{
			if (tstr < TermInfoStrings.BackTab || tstr >= TermInfoStrings.Last || tstr > (TermInfoStrings)this.strOffsets)
			{
				return null;
			}
			int num = this.booleansOffset + this.boolSize;
			if (num % 2 == 1)
			{
				num++;
			}
			num += this.numSize * this.intOffset;
			int @int = (int)this.GetInt16(this.buffer, (int)(num + tstr * TermInfoStrings.CarriageReturn));
			if (@int == -1)
			{
				return null;
			}
			return this.GetStringBytes(this.buffer, num + this.strOffsets * 2 + @int);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0004DDA0 File Offset: 0x0004BFA0
		private short GetInt16(byte[] buffer, int offset)
		{
			int num = (int)buffer[offset];
			int num2 = (int)buffer[offset + 1];
			if (num == 255 && num2 == 255)
			{
				return -1;
			}
			return (short)(num + num2 * 256);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0004DDD4 File Offset: 0x0004BFD4
		private string GetString(byte[] buffer, int offset)
		{
			int num = 0;
			int num2 = offset;
			while (buffer[num2++] != 0)
			{
				num++;
			}
			return Encoding.ASCII.GetString(buffer, offset, num);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0004DE04 File Offset: 0x0004C004
		private byte[] GetStringBytes(byte[] buffer, int offset)
		{
			int num = 0;
			int num2 = offset;
			while (buffer[num2++] != 0)
			{
				num++;
			}
			byte[] array = new byte[num];
			Buffer.InternalBlockCopy(buffer, offset, array, 0, num);
			return array;
		}

		// Token: 0x040007D6 RID: 2006
		private int boolSize;

		// Token: 0x040007D7 RID: 2007
		private int numSize;

		// Token: 0x040007D8 RID: 2008
		private int strOffsets;

		// Token: 0x040007D9 RID: 2009
		private byte[] buffer;

		// Token: 0x040007DA RID: 2010
		private int booleansOffset;

		// Token: 0x040007DB RID: 2011
		private int intOffset;
	}
}
