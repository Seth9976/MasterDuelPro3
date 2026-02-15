using System;
using System.IO;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004E2 RID: 1250
	internal sealed class SerializationHeaderRecord
	{
		// Token: 0x06002744 RID: 10052 RVA: 0x0009E24F File Offset: 0x0009C44F
		internal SerializationHeaderRecord()
		{
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x0009E25E File Offset: 0x0009C45E
		internal SerializationHeaderRecord(BinaryHeaderEnum binaryHeaderEnum, int topId, int headerId, int majorVersion, int minorVersion)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
			this.topId = topId;
			this.headerId = headerId;
			this.majorVersion = majorVersion;
			this.minorVersion = minorVersion;
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x0009E294 File Offset: 0x0009C494
		public void Write(__BinaryWriter sout)
		{
			this.majorVersion = this.binaryFormatterMajorVersion;
			this.minorVersion = this.binaryFormatterMinorVersion;
			sout.WriteByte((byte)this.binaryHeaderEnum);
			sout.WriteInt32(this.topId);
			sout.WriteInt32(this.headerId);
			sout.WriteInt32(this.binaryFormatterMajorVersion);
			sout.WriteInt32(this.binaryFormatterMinorVersion);
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x0008E237 File Offset: 0x0008C437
		private static int GetInt32(byte[] buffer, int index)
		{
			return (int)buffer[index] | ((int)buffer[index + 1] << 8) | ((int)buffer[index + 2] << 16) | ((int)buffer[index + 3] << 24);
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x0009E2F8 File Offset: 0x0009C4F8
		public void Read(__BinaryParser input)
		{
			byte[] array = input.ReadBytes(17);
			if (array.Length < 17)
			{
				__Error.EndOfFile();
			}
			this.majorVersion = SerializationHeaderRecord.GetInt32(array, 9);
			if (this.majorVersion > this.binaryFormatterMajorVersion)
			{
				throw new SerializationException(Environment.GetResourceString("The input stream is not a valid binary format. The starting contents (in bytes) are: {0} ...", new object[] { BitConverter.ToString(array) }));
			}
			this.binaryHeaderEnum = (BinaryHeaderEnum)array[0];
			this.topId = SerializationHeaderRecord.GetInt32(array, 1);
			this.headerId = SerializationHeaderRecord.GetInt32(array, 5);
			this.minorVersion = SerializationHeaderRecord.GetInt32(array, 13);
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Dump()
		{
		}

		// Token: 0x04001328 RID: 4904
		internal int binaryFormatterMajorVersion = 1;

		// Token: 0x04001329 RID: 4905
		internal int binaryFormatterMinorVersion;

		// Token: 0x0400132A RID: 4906
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x0400132B RID: 4907
		internal int topId;

		// Token: 0x0400132C RID: 4908
		internal int headerId;

		// Token: 0x0400132D RID: 4909
		internal int majorVersion;

		// Token: 0x0400132E RID: 4910
		internal int minorVersion;
	}
}
