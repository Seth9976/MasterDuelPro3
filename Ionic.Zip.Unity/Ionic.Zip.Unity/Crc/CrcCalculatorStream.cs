using System;
using System.IO;

namespace Ionic.Crc
{
	// Token: 0x02000071 RID: 113
	public class CrcCalculatorStream : Stream, IDisposable
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x0001E97A File Offset: 0x0001CB7A
		public CrcCalculatorStream(Stream stream)
			: this(true, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001E98A File Offset: 0x0001CB8A
		public CrcCalculatorStream(Stream stream, bool leaveOpen)
			: this(leaveOpen, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001E99A File Offset: 0x0001CB9A
		public CrcCalculatorStream(Stream stream, long length)
			: this(true, length, stream, null)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001E9B6 File Offset: 0x0001CBB6
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen)
			: this(leaveOpen, length, stream, null)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001E9D2 File Offset: 0x0001CBD2
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen, CRC32 crc32)
			: this(leaveOpen, length, stream, crc32)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001E9EF File Offset: 0x0001CBEF
		private CrcCalculatorStream(bool leaveOpen, long length, Stream stream, CRC32 crc32)
		{
			this._innerStream = stream;
			this._Crc32 = crc32 ?? new CRC32();
			this._lengthLimit = length;
			this._leaveOpen = leaveOpen;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001EA26 File Offset: 0x0001CC26
		public long TotalBytesSlurped
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0001EA33 File Offset: 0x0001CC33
		public int Crc
		{
			get
			{
				return this._Crc32.Crc32Result;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0001EA40 File Offset: 0x0001CC40
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x0001EA48 File Offset: 0x0001CC48
		public bool LeaveOpen
		{
			get
			{
				return this._leaveOpen;
			}
			set
			{
				this._leaveOpen = value;
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001EA54 File Offset: 0x0001CC54
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = count;
			if (this._lengthLimit != CrcCalculatorStream.UnsetLengthLimit)
			{
				if (this._Crc32.TotalBytesRead >= this._lengthLimit)
				{
					return 0;
				}
				long num2 = this._lengthLimit - this._Crc32.TotalBytesRead;
				if (num2 < (long)count)
				{
					num = (int)num2;
				}
			}
			int num3 = this._innerStream.Read(buffer, offset, num);
			if (num3 > 0)
			{
				this._Crc32.SlurpBlock(buffer, offset, num3);
			}
			return num3;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001EAC2 File Offset: 0x0001CCC2
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count > 0)
			{
				this._Crc32.SlurpBlock(buffer, offset, count);
			}
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0001EAE4 File Offset: 0x0001CCE4
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00003B06 File Offset: 0x00001D06
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0001EAF1 File Offset: 0x0001CCF1
		public override bool CanWrite
		{
			get
			{
				return this._innerStream.CanWrite;
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001EAFE File Offset: 0x0001CCFE
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0001EB0B File Offset: 0x0001CD0B
		public override long Length
		{
			get
			{
				if (this._lengthLimit == CrcCalculatorStream.UnsetLengthLimit)
				{
					return this._innerStream.Length;
				}
				return this._lengthLimit;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0001EA26 File Offset: 0x0001CC26
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x000052C3 File Offset: 0x000034C3
		public override long Position
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000052C3 File Offset: 0x000034C3
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000052C3 File Offset: 0x000034C3
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00003B69 File Offset: 0x00001D69
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001EB2C File Offset: 0x0001CD2C
		public override void Close()
		{
			base.Close();
			if (!this._leaveOpen)
			{
				this._innerStream.Close();
			}
		}

		// Token: 0x040003DC RID: 988
		private static readonly long UnsetLengthLimit = -99L;

		// Token: 0x040003DD RID: 989
		internal Stream _innerStream;

		// Token: 0x040003DE RID: 990
		private CRC32 _Crc32;

		// Token: 0x040003DF RID: 991
		private long _lengthLimit = -99L;

		// Token: 0x040003E0 RID: 992
		private bool _leaveOpen;
	}
}
