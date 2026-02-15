using System;
using System.IO;
using System.Text;
using Ionic.Crc;

namespace Ionic.Zip
{
	// Token: 0x0200003D RID: 61
	public class ZipInputStream : Stream
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x0000FD96 File Offset: 0x0000DF96
		public ZipInputStream(Stream stream)
			: this(stream, false)
		{
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000FDA0 File Offset: 0x0000DFA0
		public ZipInputStream(string fileName)
		{
			Stream stream = File.Open(fileName, 3, 1, 1);
			this._Init(stream, false, fileName);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000FDC6 File Offset: 0x0000DFC6
		public ZipInputStream(Stream stream, bool leaveOpen)
		{
			this._Init(stream, leaveOpen, null);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000FDD8 File Offset: 0x0000DFD8
		private void _Init(Stream stream, bool leaveOpen, string name)
		{
			this._inputStream = stream;
			if (!this._inputStream.CanRead)
			{
				throw new ZipException("The stream must be readable.");
			}
			this._container = new ZipContainer(this);
			this._provisionalAlternateEncoding = Encoding.UTF8;
			this._leaveUnderlyingStreamOpen = leaveOpen;
			this._findRequired = true;
			this._name = name ?? "(stream)";
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000FE39 File Offset: 0x0000E039
		public override string ToString()
		{
			return string.Format("ZipInputStream::{0}(leaveOpen({1})))", this._name, this._leaveUnderlyingStreamOpen);
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000FE56 File Offset: 0x0000E056
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000FE5E File Offset: 0x0000E05E
		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				return this._provisionalAlternateEncoding;
			}
			set
			{
				this._provisionalAlternateEncoding = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000FE67 File Offset: 0x0000E067
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000FE6F File Offset: 0x0000E06F
		public int CodecBufferSize { get; set; }

		// Token: 0x170000A7 RID: 167
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000FE78 File Offset: 0x0000E078
		public string Password
		{
			set
			{
				if (this._closed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._Password = value;
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000FE9B File Offset: 0x0000E09B
		private void SetupStream()
		{
			this._crcStream = this._currentEntry.InternalOpenReader(this._Password);
			this._LeftToRead = this._crcStream.Length;
			this._needSetup = false;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000FECC File Offset: 0x0000E0CC
		internal Stream ReadStream
		{
			get
			{
				return this._inputStream;
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000FED4 File Offset: 0x0000E0D4
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._closed)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			if (this._needSetup)
			{
				this.SetupStream();
			}
			if (this._LeftToRead == 0L)
			{
				return 0;
			}
			int num = ((this._LeftToRead > (long)count) ? count : ((int)this._LeftToRead));
			int num2 = this._crcStream.Read(buffer, offset, num);
			this._LeftToRead -= (long)num2;
			if (this._LeftToRead == 0L)
			{
				int crc = this._crcStream.Crc;
				this._currentEntry.VerifyCrcAfterExtract(crc);
				this._inputStream.Seek(this._endOfEntry, 0);
			}
			return num2;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000FF80 File Offset: 0x0000E180
		public ZipEntry GetNextEntry()
		{
			if (this._findRequired)
			{
				long num = SharedUtilities.FindSignature(this._inputStream, 67324752);
				if (num == -1L)
				{
					return null;
				}
				this._inputStream.Seek(-4L, 1);
			}
			else if (this._firstEntry)
			{
				this._inputStream.Seek(this._endOfEntry, 0);
			}
			this._currentEntry = ZipEntry.ReadEntry(this._container, !this._firstEntry);
			this._endOfEntry = this._inputStream.Position;
			this._firstEntry = true;
			this._needSetup = true;
			this._findRequired = false;
			return this._currentEntry;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00010020 File Offset: 0x0000E220
		protected override void Dispose(bool disposing)
		{
			if (this._closed)
			{
				return;
			}
			if (disposing)
			{
				if (this._exceptionPending)
				{
					return;
				}
				if (!this._leaveUnderlyingStreamOpen)
				{
					this._inputStream.Dispose();
				}
			}
			this._closed = true;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00010051 File Offset: 0x0000E251
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00010054 File Offset: 0x0000E254
		public override bool CanSeek
		{
			get
			{
				return this._inputStream.CanSeek;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00003B06 File Offset: 0x00001D06
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00010061 File Offset: 0x0000E261
		public override long Length
		{
			get
			{
				return this._inputStream.Length;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0001006E File Offset: 0x0000E26E
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0001007B File Offset: 0x0000E27B
		public override long Position
		{
			get
			{
				return this._inputStream.Position;
			}
			set
			{
				this.Seek(value, 0);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00010086 File Offset: 0x0000E286
		public override void Flush()
		{
			throw new NotSupportedException("Flush");
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00010092 File Offset: 0x0000E292
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("Write");
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000100A0 File Offset: 0x0000E2A0
		public override long Seek(long offset, SeekOrigin origin)
		{
			this._findRequired = true;
			return this._inputStream.Seek(offset, origin);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000052C3 File Offset: 0x000034C3
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400016C RID: 364
		private Stream _inputStream;

		// Token: 0x0400016D RID: 365
		private Encoding _provisionalAlternateEncoding;

		// Token: 0x0400016E RID: 366
		private ZipEntry _currentEntry;

		// Token: 0x0400016F RID: 367
		private bool _firstEntry;

		// Token: 0x04000170 RID: 368
		private bool _needSetup;

		// Token: 0x04000171 RID: 369
		private ZipContainer _container;

		// Token: 0x04000172 RID: 370
		private CrcCalculatorStream _crcStream;

		// Token: 0x04000173 RID: 371
		private long _LeftToRead;

		// Token: 0x04000174 RID: 372
		internal string _Password;

		// Token: 0x04000175 RID: 373
		private long _endOfEntry;

		// Token: 0x04000176 RID: 374
		private string _name;

		// Token: 0x04000177 RID: 375
		private bool _leaveUnderlyingStreamOpen;

		// Token: 0x04000178 RID: 376
		private bool _closed;

		// Token: 0x04000179 RID: 377
		private bool _findRequired;

		// Token: 0x0400017A RID: 378
		private bool _exceptionPending;
	}
}
