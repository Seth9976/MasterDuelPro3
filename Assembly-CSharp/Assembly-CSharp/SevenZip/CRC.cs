using System;

namespace SevenZip
{
	// Token: 0x02000184 RID: 388
	internal class CRC
	{
		// Token: 0x06000582 RID: 1410 RVA: 0x0001A508 File Offset: 0x00018708
		static CRC()
		{
			for (uint i = 0U; i < 256U; i += 1U)
			{
				uint r = i;
				for (int j = 0; j < 8; j++)
				{
					if ((r & 1U) != 0U)
					{
						r = (r >> 1) ^ 3988292384U;
					}
					else
					{
						r >>= 1;
					}
				}
				CRC.Table[(int)i] = r;
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001A55F File Offset: 0x0001875F
		public void Init()
		{
			this._value = uint.MaxValue;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001A568 File Offset: 0x00018768
		public void UpdateByte(byte b)
		{
			this._value = CRC.Table[(int)((byte)this._value ^ b)] ^ (this._value >> 8);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001A588 File Offset: 0x00018788
		public void Update(byte[] data, uint offset, uint size)
		{
			for (uint i = 0U; i < size; i += 1U)
			{
				this._value = CRC.Table[(int)((byte)this._value ^ data[(int)(offset + i)])] ^ (this._value >> 8);
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001A5C3 File Offset: 0x000187C3
		public uint GetDigest()
		{
			return this._value ^ uint.MaxValue;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001A5CD File Offset: 0x000187CD
		private static uint CalculateDigest(byte[] data, uint offset, uint size)
		{
			CRC crc = new CRC();
			crc.Update(data, offset, size);
			return crc.GetDigest();
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001A5E2 File Offset: 0x000187E2
		private static bool VerifyDigest(uint digest, byte[] data, uint offset, uint size)
		{
			return CRC.CalculateDigest(data, offset, size) == digest;
		}

		// Token: 0x04000A25 RID: 2597
		public static readonly uint[] Table = new uint[256];

		// Token: 0x04000A26 RID: 2598
		private uint _value = uint.MaxValue;
	}
}
