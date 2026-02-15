using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Net
{
	// Token: 0x02000421 RID: 1057
	internal class MonoChunkParser
	{
		// Token: 0x06001A9F RID: 6815 RVA: 0x00073B10 File Offset: 0x00071D10
		public MonoChunkParser(WebHeaderCollection headers)
		{
			this.headers = headers;
			this.saved = new StringBuilder();
			this.chunks = new ArrayList();
			this.chunkSize = -1;
			this.totalWritten = 0;
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00073B43 File Offset: 0x00071D43
		public int Read(byte[] buffer, int offset, int size)
		{
			return this.ReadFromChunks(buffer, offset, size);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00073B50 File Offset: 0x00071D50
		private int ReadFromChunks(byte[] buffer, int offset, int size)
		{
			int count = this.chunks.Count;
			int num = 0;
			List<MonoChunkParser.Chunk> list = new List<MonoChunkParser.Chunk>(count);
			for (int i = 0; i < count; i++)
			{
				MonoChunkParser.Chunk chunk = (MonoChunkParser.Chunk)this.chunks[i];
				if (chunk.Offset == chunk.Bytes.Length)
				{
					list.Add(chunk);
				}
				else
				{
					num += chunk.Read(buffer, offset + num, size - num);
					if (num == size)
					{
						break;
					}
				}
			}
			foreach (MonoChunkParser.Chunk chunk2 in list)
			{
				this.chunks.Remove(chunk2);
			}
			return num;
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00073C0C File Offset: 0x00071E0C
		public void Write(byte[] buffer, int offset, int size)
		{
			if (offset < size)
			{
				this.InternalWrite(buffer, ref offset, size);
			}
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00073C1C File Offset: 0x00071E1C
		private void InternalWrite(byte[] buffer, ref int offset, int size)
		{
			if (this.state == MonoChunkParser.State.None || this.state == MonoChunkParser.State.PartialSize)
			{
				this.state = this.GetChunkSize(buffer, ref offset, size);
				if (this.state == MonoChunkParser.State.PartialSize)
				{
					return;
				}
				this.saved.Length = 0;
				this.sawCR = false;
				this.gotit = false;
			}
			if (this.state == MonoChunkParser.State.Body && offset < size)
			{
				this.state = this.ReadBody(buffer, ref offset, size);
				if (this.state == MonoChunkParser.State.Body)
				{
					return;
				}
			}
			if (this.state == MonoChunkParser.State.BodyFinished && offset < size)
			{
				this.state = this.ReadCRLF(buffer, ref offset, size);
				if (this.state == MonoChunkParser.State.BodyFinished)
				{
					return;
				}
				this.sawCR = false;
			}
			if (this.state == MonoChunkParser.State.Trailer && offset < size)
			{
				this.state = this.ReadTrailer(buffer, ref offset, size);
				if (this.state == MonoChunkParser.State.Trailer)
				{
					return;
				}
				this.saved.Length = 0;
				this.sawCR = false;
				this.gotit = false;
			}
			if (offset < size)
			{
				this.InternalWrite(buffer, ref offset, size);
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x00073D11 File Offset: 0x00071F11
		public bool WantMore
		{
			get
			{
				return this.chunkRead != this.chunkSize || this.chunkSize != 0 || this.state > MonoChunkParser.State.None;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x00073D34 File Offset: 0x00071F34
		public bool DataAvailable
		{
			get
			{
				int count = this.chunks.Count;
				for (int i = 0; i < count; i++)
				{
					MonoChunkParser.Chunk chunk = (MonoChunkParser.Chunk)this.chunks[i];
					if (chunk != null && chunk.Bytes != null && chunk.Bytes.Length != 0 && chunk.Offset < chunk.Bytes.Length)
					{
						return this.state != MonoChunkParser.State.Body;
					}
				}
				return false;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x00073D9D File Offset: 0x00071F9D
		public int ChunkLeft
		{
			get
			{
				return this.chunkSize - this.chunkRead;
			}
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00073DAC File Offset: 0x00071FAC
		private MonoChunkParser.State ReadBody(byte[] buffer, ref int offset, int size)
		{
			if (this.chunkSize == 0)
			{
				return MonoChunkParser.State.BodyFinished;
			}
			int num = size - offset;
			if (num + this.chunkRead > this.chunkSize)
			{
				num = this.chunkSize - this.chunkRead;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(buffer, offset, array, 0, num);
			this.chunks.Add(new MonoChunkParser.Chunk(array));
			offset += num;
			this.chunkRead += num;
			this.totalWritten += num;
			if (this.chunkRead != this.chunkSize)
			{
				return MonoChunkParser.State.Body;
			}
			return MonoChunkParser.State.BodyFinished;
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x00073E40 File Offset: 0x00072040
		private MonoChunkParser.State GetChunkSize(byte[] buffer, ref int offset, int size)
		{
			this.chunkRead = 0;
			this.chunkSize = 0;
			char c = '\0';
			while (offset < size)
			{
				int num = offset;
				offset = num + 1;
				c = (char)buffer[num];
				if (c == '\r')
				{
					if (this.sawCR)
					{
						MonoChunkParser.ThrowProtocolViolation("2 CR found");
					}
					this.sawCR = true;
				}
				else
				{
					if (this.sawCR && c == '\n')
					{
						break;
					}
					if (c == ' ')
					{
						this.gotit = true;
					}
					if (!this.gotit)
					{
						this.saved.Append(c);
					}
					if (this.saved.Length > 20)
					{
						MonoChunkParser.ThrowProtocolViolation("chunk size too long.");
					}
				}
			}
			if (!this.sawCR || c != '\n')
			{
				if (offset < size)
				{
					MonoChunkParser.ThrowProtocolViolation("Missing \\n");
				}
				try
				{
					if (this.saved.Length > 0)
					{
						this.chunkSize = int.Parse(MonoChunkParser.RemoveChunkExtension(this.saved.ToString()), NumberStyles.HexNumber);
					}
				}
				catch (Exception)
				{
					MonoChunkParser.ThrowProtocolViolation("Cannot parse chunk size.");
				}
				return MonoChunkParser.State.PartialSize;
			}
			this.chunkRead = 0;
			try
			{
				this.chunkSize = int.Parse(MonoChunkParser.RemoveChunkExtension(this.saved.ToString()), NumberStyles.HexNumber);
			}
			catch (Exception)
			{
				MonoChunkParser.ThrowProtocolViolation("Cannot parse chunk size.");
			}
			if (this.chunkSize == 0)
			{
				this.trailerState = 2;
				return MonoChunkParser.State.Trailer;
			}
			return MonoChunkParser.State.Body;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00073F98 File Offset: 0x00072198
		private static string RemoveChunkExtension(string input)
		{
			int num = input.IndexOf(';');
			if (num == -1)
			{
				return input;
			}
			return input.Substring(0, num);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x00073FBC File Offset: 0x000721BC
		private MonoChunkParser.State ReadCRLF(byte[] buffer, ref int offset, int size)
		{
			if (!this.sawCR)
			{
				int num = offset;
				offset = num + 1;
				if (buffer[num] != 13)
				{
					MonoChunkParser.ThrowProtocolViolation("Expecting \\r");
				}
				this.sawCR = true;
				if (offset == size)
				{
					return MonoChunkParser.State.BodyFinished;
				}
			}
			if (this.sawCR)
			{
				int num = offset;
				offset = num + 1;
				if (buffer[num] != 10)
				{
					MonoChunkParser.ThrowProtocolViolation("Expecting \\n");
				}
			}
			return MonoChunkParser.State.None;
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0007401C File Offset: 0x0007221C
		private MonoChunkParser.State ReadTrailer(byte[] buffer, ref int offset, int size)
		{
			if (this.trailerState == 2 && buffer[offset] == 13 && this.saved.Length == 0)
			{
				offset++;
				if (offset < size && buffer[offset] == 10)
				{
					offset++;
					return MonoChunkParser.State.None;
				}
				offset--;
			}
			int num = this.trailerState;
			while (offset < size && num < 4)
			{
				int num2 = offset;
				offset = num2 + 1;
				char c = (char)buffer[num2];
				if ((num == 0 || num == 2) && c == '\r')
				{
					num++;
				}
				else if ((num == 1 || num == 3) && c == '\n')
				{
					num++;
				}
				else if (num >= 0)
				{
					this.saved.Append(c);
					num = 0;
					if (this.saved.Length > 4196)
					{
						MonoChunkParser.ThrowProtocolViolation("Error reading trailer (too long).");
					}
				}
			}
			if (num < 4)
			{
				this.trailerState = num;
				if (offset < size)
				{
					MonoChunkParser.ThrowProtocolViolation("Error reading trailer.");
				}
				return MonoChunkParser.State.Trailer;
			}
			StringReader stringReader = new StringReader(this.saved.ToString());
			string text;
			while ((text = stringReader.ReadLine()) != null && text != "")
			{
				this.headers.Add(text);
			}
			return MonoChunkParser.State.None;
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00074132 File Offset: 0x00072332
		private static void ThrowProtocolViolation(string message)
		{
			throw new WebException(message, null, WebExceptionStatus.ServerProtocolViolation, null);
		}

		// Token: 0x04001145 RID: 4421
		private WebHeaderCollection headers;

		// Token: 0x04001146 RID: 4422
		private int chunkSize;

		// Token: 0x04001147 RID: 4423
		private int chunkRead;

		// Token: 0x04001148 RID: 4424
		private int totalWritten;

		// Token: 0x04001149 RID: 4425
		private MonoChunkParser.State state;

		// Token: 0x0400114A RID: 4426
		private StringBuilder saved;

		// Token: 0x0400114B RID: 4427
		private bool sawCR;

		// Token: 0x0400114C RID: 4428
		private bool gotit;

		// Token: 0x0400114D RID: 4429
		private int trailerState;

		// Token: 0x0400114E RID: 4430
		private ArrayList chunks;

		// Token: 0x02000422 RID: 1058
		private enum State
		{
			// Token: 0x04001150 RID: 4432
			None,
			// Token: 0x04001151 RID: 4433
			PartialSize,
			// Token: 0x04001152 RID: 4434
			Body,
			// Token: 0x04001153 RID: 4435
			BodyFinished,
			// Token: 0x04001154 RID: 4436
			Trailer
		}

		// Token: 0x02000423 RID: 1059
		private class Chunk
		{
			// Token: 0x06001AAD RID: 6829 RVA: 0x0007413E File Offset: 0x0007233E
			public Chunk(byte[] chunk)
			{
				this.Bytes = chunk;
			}

			// Token: 0x06001AAE RID: 6830 RVA: 0x00074150 File Offset: 0x00072350
			public int Read(byte[] buffer, int offset, int size)
			{
				int num = ((size > this.Bytes.Length - this.Offset) ? (this.Bytes.Length - this.Offset) : size);
				Buffer.BlockCopy(this.Bytes, this.Offset, buffer, offset, num);
				this.Offset += num;
				return num;
			}

			// Token: 0x04001155 RID: 4437
			public byte[] Bytes;

			// Token: 0x04001156 RID: 4438
			public int Offset;
		}
	}
}
