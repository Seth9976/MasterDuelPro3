using System;
using System.IO;

namespace SevenZip.Compression.LZ
{
	// Token: 0x020001A2 RID: 418
	public class BinTree : InWindow, IMatchFinder, IInWindowStream
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x0001E4A8 File Offset: 0x0001C6A8
		public void SetType(int numHashBytes)
		{
			this.HASH_ARRAY = numHashBytes > 2;
			if (this.HASH_ARRAY)
			{
				this.kNumHashDirectBytes = 0U;
				this.kMinMatchCheck = 4U;
				this.kFixHashSize = 66560U;
				return;
			}
			this.kNumHashDirectBytes = 2U;
			this.kMinMatchCheck = 3U;
			this.kFixHashSize = 0U;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001E4F6 File Offset: 0x0001C6F6
		public new void SetStream(Stream stream)
		{
			base.SetStream(stream);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001E4FF File Offset: 0x0001C6FF
		public new void ReleaseStream()
		{
			base.ReleaseStream();
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001E508 File Offset: 0x0001C708
		public new void Init()
		{
			base.Init();
			for (uint i = 0U; i < this._hashSizeSum; i += 1U)
			{
				this._hash[(int)i] = 0U;
			}
			this._cyclicBufferPos = 0U;
			base.ReduceOffsets(-1);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001E544 File Offset: 0x0001C744
		public new void MovePos()
		{
			uint num = this._cyclicBufferPos + 1U;
			this._cyclicBufferPos = num;
			if (num >= this._cyclicBufferSize)
			{
				this._cyclicBufferPos = 0U;
			}
			base.MovePos();
			if (this._pos == 2147483647U)
			{
				this.Normalize();
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001E58A File Offset: 0x0001C78A
		public new byte GetIndexByte(int index)
		{
			return base.GetIndexByte(index);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001E593 File Offset: 0x0001C793
		public new uint GetMatchLen(int index, uint distance, uint limit)
		{
			return base.GetMatchLen(index, distance, limit);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001E59E File Offset: 0x0001C79E
		public new uint GetNumAvailableBytes()
		{
			return base.GetNumAvailableBytes();
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001E5A8 File Offset: 0x0001C7A8
		public void Create(uint historySize, uint keepAddBufferBefore, uint matchMaxLen, uint keepAddBufferAfter)
		{
			if (historySize > 2147483391U)
			{
				throw new Exception();
			}
			this._cutValue = 16U + (matchMaxLen >> 1);
			uint windowReservSize = (historySize + keepAddBufferBefore + matchMaxLen + keepAddBufferAfter) / 2U + 256U;
			base.Create(historySize + keepAddBufferBefore, matchMaxLen + keepAddBufferAfter, windowReservSize);
			this._matchMaxLen = matchMaxLen;
			uint cyclicBufferSize = historySize + 1U;
			if (this._cyclicBufferSize != cyclicBufferSize)
			{
				this._son = new uint[(this._cyclicBufferSize = cyclicBufferSize) * 2U];
			}
			uint hs = 65536U;
			if (this.HASH_ARRAY)
			{
				hs = historySize - 1U;
				hs |= hs >> 1;
				hs |= hs >> 2;
				hs |= hs >> 4;
				hs |= hs >> 8;
				hs >>= 1;
				hs |= 65535U;
				if (hs > 16777216U)
				{
					hs >>= 1;
				}
				this._hashMask = hs;
				hs += 1U;
				hs += this.kFixHashSize;
			}
			if (hs != this._hashSizeSum)
			{
				this._hash = new uint[this._hashSizeSum = hs];
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001E690 File Offset: 0x0001C890
		public uint GetMatches(uint[] distances)
		{
			uint lenLimit;
			if (this._pos + this._matchMaxLen <= this._streamPos)
			{
				lenLimit = this._matchMaxLen;
			}
			else
			{
				lenLimit = this._streamPos - this._pos;
				if (lenLimit < this.kMinMatchCheck)
				{
					this.MovePos();
					return 0U;
				}
			}
			uint offset = 0U;
			uint matchMinPos = ((this._pos > this._cyclicBufferSize) ? (this._pos - this._cyclicBufferSize) : 0U);
			uint cur = this._bufferOffset + this._pos;
			uint maxLen = 1U;
			uint hash2Value = 0U;
			uint hash3Value = 0U;
			uint hashValue;
			if (this.HASH_ARRAY)
			{
				uint num = CRC.Table[(int)this._bufferBase[(int)cur]] ^ (uint)this._bufferBase[(int)(cur + 1U)];
				hash2Value = num & 1023U;
				uint num2 = num ^ (uint)((uint)this._bufferBase[(int)(cur + 2U)] << 8);
				hash3Value = num2 & 65535U;
				hashValue = (num2 ^ (CRC.Table[(int)this._bufferBase[(int)(cur + 3U)]] << 5)) & this._hashMask;
			}
			else
			{
				hashValue = (uint)((int)this._bufferBase[(int)cur] ^ ((int)this._bufferBase[(int)(cur + 1U)] << 8));
			}
			uint curMatch = this._hash[(int)(this.kFixHashSize + hashValue)];
			if (this.HASH_ARRAY)
			{
				uint curMatch2 = this._hash[(int)hash2Value];
				uint curMatch3 = this._hash[(int)(1024U + hash3Value)];
				this._hash[(int)hash2Value] = this._pos;
				this._hash[(int)(1024U + hash3Value)] = this._pos;
				if (curMatch2 > matchMinPos && this._bufferBase[(int)(this._bufferOffset + curMatch2)] == this._bufferBase[(int)cur])
				{
					maxLen = (distances[(int)offset++] = 2U);
					distances[(int)offset++] = this._pos - curMatch2 - 1U;
				}
				if (curMatch3 > matchMinPos && this._bufferBase[(int)(this._bufferOffset + curMatch3)] == this._bufferBase[(int)cur])
				{
					if (curMatch3 == curMatch2)
					{
						offset -= 2U;
					}
					maxLen = (distances[(int)offset++] = 3U);
					distances[(int)offset++] = this._pos - curMatch3 - 1U;
					curMatch2 = curMatch3;
				}
				if (offset != 0U && curMatch2 == curMatch)
				{
					offset -= 2U;
					maxLen = 1U;
				}
			}
			this._hash[(int)(this.kFixHashSize + hashValue)] = this._pos;
			uint ptr0 = (this._cyclicBufferPos << 1) + 1U;
			uint ptr = this._cyclicBufferPos << 1;
			uint len2;
			uint len = (len2 = this.kNumHashDirectBytes);
			if (this.kNumHashDirectBytes != 0U && curMatch > matchMinPos && this._bufferBase[(int)(this._bufferOffset + curMatch + this.kNumHashDirectBytes)] != this._bufferBase[(int)(cur + this.kNumHashDirectBytes)])
			{
				maxLen = (distances[(int)offset++] = this.kNumHashDirectBytes);
				distances[(int)offset++] = this._pos - curMatch - 1U;
			}
			uint count = this._cutValue;
			while (curMatch > matchMinPos && count-- != 0U)
			{
				uint delta = this._pos - curMatch;
				uint cyclicPos = ((delta <= this._cyclicBufferPos) ? (this._cyclicBufferPos - delta) : (this._cyclicBufferPos - delta + this._cyclicBufferSize)) << 1;
				uint pby = this._bufferOffset + curMatch;
				uint len3 = Math.Min(len2, len);
				if (this._bufferBase[(int)(pby + len3)] == this._bufferBase[(int)(cur + len3)])
				{
					while ((len3 += 1U) != lenLimit && this._bufferBase[(int)(pby + len3)] == this._bufferBase[(int)(cur + len3)])
					{
					}
					if (maxLen < len3)
					{
						maxLen = (distances[(int)offset++] = len3);
						distances[(int)offset++] = delta - 1U;
						if (len3 == lenLimit)
						{
							this._son[(int)ptr] = this._son[(int)cyclicPos];
							this._son[(int)ptr0] = this._son[(int)(cyclicPos + 1U)];
							IL_03D1:
							this.MovePos();
							return offset;
						}
					}
				}
				if (this._bufferBase[(int)(pby + len3)] < this._bufferBase[(int)(cur + len3)])
				{
					this._son[(int)ptr] = curMatch;
					ptr = cyclicPos + 1U;
					curMatch = this._son[(int)ptr];
					len = len3;
				}
				else
				{
					this._son[(int)ptr0] = curMatch;
					ptr0 = cyclicPos;
					curMatch = this._son[(int)ptr0];
					len2 = len3;
				}
			}
			this._son[(int)ptr0] = (this._son[(int)ptr] = 0U);
			goto IL_03D1;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001EA78 File Offset: 0x0001CC78
		public void Skip(uint num)
		{
			for (;;)
			{
				uint lenLimit;
				if (this._pos + this._matchMaxLen <= this._streamPos)
				{
					lenLimit = this._matchMaxLen;
					goto IL_0040;
				}
				lenLimit = this._streamPos - this._pos;
				if (lenLimit >= this.kMinMatchCheck)
				{
					goto IL_0040;
				}
				this.MovePos();
				IL_029A:
				if ((num -= 1U) == 0U)
				{
					break;
				}
				continue;
				IL_0040:
				uint matchMinPos = ((this._pos > this._cyclicBufferSize) ? (this._pos - this._cyclicBufferSize) : 0U);
				uint cur = this._bufferOffset + this._pos;
				uint hashValue;
				if (this.HASH_ARRAY)
				{
					uint num2 = CRC.Table[(int)this._bufferBase[(int)cur]] ^ (uint)this._bufferBase[(int)(cur + 1U)];
					uint hash2Value = num2 & 1023U;
					this._hash[(int)hash2Value] = this._pos;
					uint num3 = num2 ^ (uint)((uint)this._bufferBase[(int)(cur + 2U)] << 8);
					uint hash3Value = num3 & 65535U;
					this._hash[(int)(1024U + hash3Value)] = this._pos;
					hashValue = (num3 ^ (CRC.Table[(int)this._bufferBase[(int)(cur + 3U)]] << 5)) & this._hashMask;
				}
				else
				{
					hashValue = (uint)((int)this._bufferBase[(int)cur] ^ ((int)this._bufferBase[(int)(cur + 1U)] << 8));
				}
				uint curMatch = this._hash[(int)(this.kFixHashSize + hashValue)];
				this._hash[(int)(this.kFixHashSize + hashValue)] = this._pos;
				uint ptr0 = (this._cyclicBufferPos << 1) + 1U;
				uint ptr = this._cyclicBufferPos << 1;
				uint len2;
				uint len = (len2 = this.kNumHashDirectBytes);
				uint count = this._cutValue;
				while (curMatch > matchMinPos && count-- != 0U)
				{
					uint delta = this._pos - curMatch;
					uint cyclicPos = ((delta <= this._cyclicBufferPos) ? (this._cyclicBufferPos - delta) : (this._cyclicBufferPos - delta + this._cyclicBufferSize)) << 1;
					uint pby = this._bufferOffset + curMatch;
					uint len3 = Math.Min(len2, len);
					if (this._bufferBase[(int)(pby + len3)] == this._bufferBase[(int)(cur + len3)])
					{
						while ((len3 += 1U) != lenLimit && this._bufferBase[(int)(pby + len3)] == this._bufferBase[(int)(cur + len3)])
						{
						}
						if (len3 == lenLimit)
						{
							this._son[(int)ptr] = this._son[(int)cyclicPos];
							this._son[(int)ptr0] = this._son[(int)(cyclicPos + 1U)];
							IL_0294:
							this.MovePos();
							goto IL_029A;
						}
					}
					if (this._bufferBase[(int)(pby + len3)] < this._bufferBase[(int)(cur + len3)])
					{
						this._son[(int)ptr] = curMatch;
						ptr = cyclicPos + 1U;
						curMatch = this._son[(int)ptr];
						len = len3;
					}
					else
					{
						this._son[(int)ptr0] = curMatch;
						ptr0 = cyclicPos;
						curMatch = this._son[(int)ptr0];
						len2 = len3;
					}
				}
				this._son[(int)ptr0] = (this._son[(int)ptr] = 0U);
				goto IL_0294;
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001ED2C File Offset: 0x0001CF2C
		private void NormalizeLinks(uint[] items, uint numItems, uint subValue)
		{
			for (uint i = 0U; i < numItems; i += 1U)
			{
				uint value = items[(int)i];
				if (value <= subValue)
				{
					value = 0U;
				}
				else
				{
					value -= subValue;
				}
				items[(int)i] = value;
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001ED5C File Offset: 0x0001CF5C
		private void Normalize()
		{
			uint subValue = this._pos - this._cyclicBufferSize;
			this.NormalizeLinks(this._son, this._cyclicBufferSize * 2U, subValue);
			this.NormalizeLinks(this._hash, this._hashSizeSum, subValue);
			base.ReduceOffsets((int)subValue);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001EDA6 File Offset: 0x0001CFA6
		public void SetCutValue(uint cutValue)
		{
			this._cutValue = cutValue;
		}

		// Token: 0x04000AE0 RID: 2784
		private uint _cyclicBufferPos;

		// Token: 0x04000AE1 RID: 2785
		private uint _cyclicBufferSize;

		// Token: 0x04000AE2 RID: 2786
		private uint _matchMaxLen;

		// Token: 0x04000AE3 RID: 2787
		private uint[] _son;

		// Token: 0x04000AE4 RID: 2788
		private uint[] _hash;

		// Token: 0x04000AE5 RID: 2789
		private uint _cutValue = 255U;

		// Token: 0x04000AE6 RID: 2790
		private uint _hashMask;

		// Token: 0x04000AE7 RID: 2791
		private uint _hashSizeSum;

		// Token: 0x04000AE8 RID: 2792
		private bool HASH_ARRAY = true;

		// Token: 0x04000AE9 RID: 2793
		private const uint kHash2Size = 1024U;

		// Token: 0x04000AEA RID: 2794
		private const uint kHash3Size = 65536U;

		// Token: 0x04000AEB RID: 2795
		private const uint kBT2HashSize = 65536U;

		// Token: 0x04000AEC RID: 2796
		private const uint kStartMaxLen = 1U;

		// Token: 0x04000AED RID: 2797
		private const uint kHash3Offset = 1024U;

		// Token: 0x04000AEE RID: 2798
		private const uint kEmptyHashValue = 0U;

		// Token: 0x04000AEF RID: 2799
		private const uint kMaxValForNormalize = 2147483647U;

		// Token: 0x04000AF0 RID: 2800
		private uint kNumHashDirectBytes;

		// Token: 0x04000AF1 RID: 2801
		private uint kMinMatchCheck = 4U;

		// Token: 0x04000AF2 RID: 2802
		private uint kFixHashSize = 66560U;
	}
}
