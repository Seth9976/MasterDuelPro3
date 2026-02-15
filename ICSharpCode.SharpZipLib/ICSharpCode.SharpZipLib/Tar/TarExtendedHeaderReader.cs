using System;
using System.Collections.Generic;
using System.Text;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000077 RID: 119
	public class TarExtendedHeaderReader
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0001387B File Offset: 0x00011A7B
		public TarExtendedHeaderReader()
		{
			this.ResetBuffers();
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000138BC File Offset: 0x00011ABC
		public void Read(byte[] buffer, int length)
		{
			for (int i = 0; i < length; i++)
			{
				byte b = buffer[i];
				if ((this.state == 2) ? (this.currHeaderRead == this.currHeaderLength - 1) : (b == TarExtendedHeaderReader.StateNext[this.state]))
				{
					this.Flush();
					this.headerParts[this.state] = this.sb.ToString();
					this.sb.Clear();
					int num = this.state + 1;
					this.state = num;
					if (num == 3)
					{
						if (!this.headers.ContainsKey(this.headerParts[1]))
						{
							this.headers.Add(this.headerParts[1], this.headerParts[2]);
						}
						this.headerParts = new string[3];
						this.currHeaderLength = 0;
						this.currHeaderRead = 0;
						this.state = 0;
					}
					else
					{
						this.currHeaderRead++;
					}
					int num2;
					if (this.state == 2 && int.TryParse(this.headerParts[0], out num2))
					{
						this.currHeaderLength = num2;
					}
				}
				else
				{
					byte[] array = this.byteBuffer;
					int num = this.bbIndex;
					this.bbIndex = num + 1;
					array[num] = b;
					this.currHeaderRead++;
					if (this.bbIndex == 4)
					{
						this.Flush();
					}
				}
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00013A08 File Offset: 0x00011C08
		private void Flush()
		{
			int num;
			int num2;
			bool flag;
			this.decoder.Convert(this.byteBuffer, 0, this.bbIndex, this.charBuffer, 0, 4, false, out num, out num2, out flag);
			this.sb.Append(this.charBuffer, 0, num2);
			this.ResetBuffers();
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00013A56 File Offset: 0x00011C56
		private void ResetBuffers()
		{
			this.charBuffer = new char[4];
			this.byteBuffer = new byte[4];
			this.bbIndex = 0;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00013A77 File Offset: 0x00011C77
		public Dictionary<string, string> Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x040002CB RID: 715
		private const byte LENGTH = 0;

		// Token: 0x040002CC RID: 716
		private const byte KEY = 1;

		// Token: 0x040002CD RID: 717
		private const byte VALUE = 2;

		// Token: 0x040002CE RID: 718
		private const byte END = 3;

		// Token: 0x040002CF RID: 719
		private readonly Dictionary<string, string> headers = new Dictionary<string, string>();

		// Token: 0x040002D0 RID: 720
		private string[] headerParts = new string[3];

		// Token: 0x040002D1 RID: 721
		private int bbIndex;

		// Token: 0x040002D2 RID: 722
		private byte[] byteBuffer;

		// Token: 0x040002D3 RID: 723
		private char[] charBuffer;

		// Token: 0x040002D4 RID: 724
		private readonly StringBuilder sb = new StringBuilder();

		// Token: 0x040002D5 RID: 725
		private readonly Decoder decoder = Encoding.UTF8.GetDecoder();

		// Token: 0x040002D6 RID: 726
		private int state;

		// Token: 0x040002D7 RID: 727
		private int currHeaderLength;

		// Token: 0x040002D8 RID: 728
		private int currHeaderRead;

		// Token: 0x040002D9 RID: 729
		private static readonly byte[] StateNext = new byte[] { 32, 61, 10 };
	}
}
