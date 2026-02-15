using System;
using System.IO;
using YGOSharp.OCGWrapper.Enums;

namespace YGOSharp
{
	// Token: 0x020001B3 RID: 435
	public class CoreMessage
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0002029D File Offset: 0x0001E49D
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x000202A5 File Offset: 0x0001E4A5
		public GameMessage Message { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x000202AE File Offset: 0x0001E4AE
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x000202B6 File Offset: 0x0001E4B6
		public BinaryReader Reader { get; private set; }

		// Token: 0x0600068F RID: 1679 RVA: 0x000202BF File Offset: 0x0001E4BF
		public CoreMessage(GameMessage msg, BinaryReader reader, byte[] raw)
		{
			this.Message = msg;
			this.Reader = reader;
			this._raw = raw;
			this._stream = (MemoryStream)reader.BaseStream;
			this._startPosition = this._stream.Position;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00020300 File Offset: 0x0001E500
		public byte[] CreateBuffer()
		{
			this.SetEndPosition();
			byte[] buffer = new byte[this._length];
			Array.Copy(this._raw, this._startPosition, buffer, 0L, this._length);
			return buffer;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0002033B File Offset: 0x0001E53B
		private void SetEndPosition()
		{
			this._endPosition = this._stream.Position;
			this._length = this._endPosition - this._startPosition;
		}

		// Token: 0x04000B3F RID: 2879
		private readonly byte[] _raw;

		// Token: 0x04000B40 RID: 2880
		private readonly MemoryStream _stream;

		// Token: 0x04000B41 RID: 2881
		private readonly long _startPosition;

		// Token: 0x04000B42 RID: 2882
		private long _endPosition;

		// Token: 0x04000B43 RID: 2883
		private long _length;
	}
}
