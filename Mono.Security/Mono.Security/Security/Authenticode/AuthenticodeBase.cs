using System;
using System.IO;
using System.Security.Cryptography;

namespace Mono.Security.Authenticode
{
	// Token: 0x02000053 RID: 83
	public class AuthenticodeBase
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000BDEE File Offset: 0x00009FEE
		public AuthenticodeBase()
		{
			this.fileblock = new byte[4096];
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000BE06 File Offset: 0x0000A006
		internal int PEOffset
		{
			get
			{
				if (this.blockNo < 1)
				{
					this.ReadFirstBlock();
				}
				return this.peOffset;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000BE1D File Offset: 0x0000A01D
		internal void Open(string filename)
		{
			if (this.fs != null)
			{
				this.Close();
			}
			this.fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.blockNo = 0;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000BE43 File Offset: 0x0000A043
		internal void Open(byte[] rawdata)
		{
			if (this.fs != null)
			{
				this.Close();
			}
			this.fs = new MemoryStream(rawdata, false);
			this.blockNo = 0;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000BE67 File Offset: 0x0000A067
		internal void Close()
		{
			if (this.fs != null)
			{
				this.fs.Close();
				this.fs = null;
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000BE84 File Offset: 0x0000A084
		internal void ReadFirstBlock()
		{
			int num = this.ProcessFirstBlock();
			if (num != 0)
			{
				throw new NotSupportedException(Locale.GetText("Cannot sign non PE files, e.g. .CAB or .MSI files (error {0}).", new object[] { num }));
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000BEBC File Offset: 0x0000A0BC
		internal int ProcessFirstBlock()
		{
			if (this.fs == null)
			{
				return 1;
			}
			this.fs.Position = 0L;
			this.blockLength = this.fs.Read(this.fileblock, 0, this.fileblock.Length);
			this.blockNo = 1;
			if (this.blockLength < 64)
			{
				return 2;
			}
			if (BitConverterLE.ToUInt16(this.fileblock, 0) != 23117)
			{
				return 3;
			}
			this.peOffset = BitConverterLE.ToInt32(this.fileblock, 60);
			if (this.peOffset > this.fileblock.Length)
			{
				throw new NotSupportedException(string.Format(Locale.GetText("Header size too big (> {0} bytes)."), this.fileblock.Length));
			}
			if ((long)this.peOffset > this.fs.Length)
			{
				return 4;
			}
			if (BitConverterLE.ToUInt32(this.fileblock, this.peOffset) != 17744U)
			{
				return 5;
			}
			ushort num = BitConverterLE.ToUInt16(this.fileblock, this.peOffset + 24);
			this.pe64 = num == 523;
			if (this.pe64)
			{
				this.dirSecurityOffset = BitConverterLE.ToInt32(this.fileblock, this.peOffset + 168);
				this.dirSecuritySize = BitConverterLE.ToInt32(this.fileblock, this.peOffset + 168 + 4);
			}
			else
			{
				this.dirSecurityOffset = BitConverterLE.ToInt32(this.fileblock, this.peOffset + 152);
				this.dirSecuritySize = BitConverterLE.ToInt32(this.fileblock, this.peOffset + 156);
			}
			this.coffSymbolTableOffset = BitConverterLE.ToInt32(this.fileblock, this.peOffset + 12);
			return 0;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000C05C File Offset: 0x0000A25C
		internal byte[] GetSecurityEntry()
		{
			if (this.blockNo < 1)
			{
				this.ReadFirstBlock();
			}
			if (this.dirSecuritySize > 8)
			{
				byte[] array = new byte[this.dirSecuritySize - 8];
				this.fs.Position = (long)(this.dirSecurityOffset + 8);
				this.fs.Read(array, 0, array.Length);
				return array;
			}
			return null;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
		internal byte[] GetHash(HashAlgorithm hash)
		{
			if (this.blockNo < 1)
			{
				this.ReadFirstBlock();
			}
			this.fs.Position = (long)this.blockLength;
			int num = 0;
			long num2;
			if (this.dirSecurityOffset > 0)
			{
				if (this.dirSecurityOffset < this.blockLength)
				{
					this.blockLength = this.dirSecurityOffset;
					num2 = 0L;
				}
				else
				{
					num2 = (long)(this.dirSecurityOffset - this.blockLength);
				}
			}
			else if (this.coffSymbolTableOffset > 0)
			{
				this.fileblock[this.PEOffset + 12] = 0;
				this.fileblock[this.PEOffset + 13] = 0;
				this.fileblock[this.PEOffset + 14] = 0;
				this.fileblock[this.PEOffset + 15] = 0;
				this.fileblock[this.PEOffset + 16] = 0;
				this.fileblock[this.PEOffset + 17] = 0;
				this.fileblock[this.PEOffset + 18] = 0;
				this.fileblock[this.PEOffset + 19] = 0;
				if (this.coffSymbolTableOffset < this.blockLength)
				{
					this.blockLength = this.coffSymbolTableOffset;
					num2 = 0L;
				}
				else
				{
					num2 = (long)(this.coffSymbolTableOffset - this.blockLength);
				}
			}
			else
			{
				num = (int)(this.fs.Length & 7L);
				if (num > 0)
				{
					num = 8 - num;
				}
				num2 = this.fs.Length - (long)this.blockLength;
			}
			int num3 = this.peOffset + 88;
			hash.TransformBlock(this.fileblock, 0, num3, this.fileblock, 0);
			num3 += 4;
			if (this.pe64)
			{
				hash.TransformBlock(this.fileblock, num3, 76, this.fileblock, num3);
				num3 += 84;
			}
			else
			{
				hash.TransformBlock(this.fileblock, num3, 60, this.fileblock, num3);
				num3 += 68;
			}
			if (num2 == 0L)
			{
				hash.TransformFinalBlock(this.fileblock, num3, this.blockLength - num3);
			}
			else
			{
				hash.TransformBlock(this.fileblock, num3, this.blockLength - num3, this.fileblock, num3);
				long num4 = num2 >> 12;
				int num5 = (int)(num2 - (num4 << 12));
				if (num5 == 0)
				{
					num4 -= 1L;
					num5 = 4096;
				}
				for (;;)
				{
					long num6 = num4;
					num4 = num6 - 1L;
					if (num6 <= 0L)
					{
						break;
					}
					this.fs.Read(this.fileblock, 0, this.fileblock.Length);
					hash.TransformBlock(this.fileblock, 0, this.fileblock.Length, this.fileblock, 0);
				}
				if (this.fs.Read(this.fileblock, 0, num5) != num5)
				{
					return null;
				}
				if (num > 0)
				{
					hash.TransformBlock(this.fileblock, 0, num5, this.fileblock, 0);
					hash.TransformFinalBlock(new byte[num], 0, num);
				}
				else
				{
					hash.TransformFinalBlock(this.fileblock, 0, num5);
				}
			}
			return hash.Hash;
		}

		// Token: 0x04000220 RID: 544
		private byte[] fileblock;

		// Token: 0x04000221 RID: 545
		private Stream fs;

		// Token: 0x04000222 RID: 546
		private int blockNo;

		// Token: 0x04000223 RID: 547
		private int blockLength;

		// Token: 0x04000224 RID: 548
		private int peOffset;

		// Token: 0x04000225 RID: 549
		private int dirSecurityOffset;

		// Token: 0x04000226 RID: 550
		private int dirSecuritySize;

		// Token: 0x04000227 RID: 551
		private int coffSymbolTableOffset;

		// Token: 0x04000228 RID: 552
		private bool pe64;
	}
}
