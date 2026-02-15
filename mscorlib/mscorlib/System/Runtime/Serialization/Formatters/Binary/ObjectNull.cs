using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004F1 RID: 1265
	internal sealed class ObjectNull
	{
		// Token: 0x0600278F RID: 10127 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal ObjectNull()
		{
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x0009F7D5 File Offset: 0x0009D9D5
		internal void SetNullCount(int nullCount)
		{
			this.nullCount = nullCount;
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x0009F7E0 File Offset: 0x0009D9E0
		public void Write(__BinaryWriter sout)
		{
			if (this.nullCount == 1)
			{
				sout.WriteByte(10);
				return;
			}
			if (this.nullCount < 256)
			{
				sout.WriteByte(13);
				sout.WriteByte((byte)this.nullCount);
				return;
			}
			sout.WriteByte(14);
			sout.WriteInt32(this.nullCount);
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x0009F838 File Offset: 0x0009DA38
		public void Read(__BinaryParser input, BinaryHeaderEnum binaryHeaderEnum)
		{
			switch (binaryHeaderEnum)
			{
			case BinaryHeaderEnum.ObjectNull:
				this.nullCount = 1;
				return;
			case BinaryHeaderEnum.MessageEnd:
			case BinaryHeaderEnum.Assembly:
				break;
			case BinaryHeaderEnum.ObjectNullMultiple256:
				this.nullCount = (int)input.ReadByte();
				return;
			case BinaryHeaderEnum.ObjectNullMultiple:
				this.nullCount = input.ReadInt32();
				break;
			default:
				return;
			}
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Dump()
		{
		}

		// Token: 0x04001370 RID: 4976
		internal int nullCount;
	}
}
