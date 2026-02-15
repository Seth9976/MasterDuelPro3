using System;
using System.IO;
using SevenZip.Compression.LZ;
using SevenZip.Compression.RangeCoder;

namespace SevenZip.Compression.LZMA
{
	// Token: 0x02000195 RID: 405
	public class Decoder : ICoder, ISetDecoderProperties
	{
		// Token: 0x060005C6 RID: 1478 RVA: 0x0001B0C4 File Offset: 0x000192C4
		public Decoder()
		{
			this.m_DictionarySize = uint.MaxValue;
			int i = 0;
			while ((long)i < 4L)
			{
				this.m_PosSlotDecoder[i] = new BitTreeDecoder(6);
				i++;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001B1B0 File Offset: 0x000193B0
		private void SetDictionarySize(uint dictionarySize)
		{
			if (this.m_DictionarySize != dictionarySize)
			{
				this.m_DictionarySize = dictionarySize;
				this.m_DictionarySizeCheck = Math.Max(this.m_DictionarySize, 1U);
				uint blockSize = Math.Max(this.m_DictionarySizeCheck, 4096U);
				this.m_OutWindow.Create(blockSize);
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001B1FC File Offset: 0x000193FC
		private void SetLiteralProperties(int lp, int lc)
		{
			if (lp > 8)
			{
				throw new InvalidParamException();
			}
			if (lc > 8)
			{
				throw new InvalidParamException();
			}
			this.m_LiteralDecoder.Create(lp, lc);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001B220 File Offset: 0x00019420
		private void SetPosBitsProperties(int pb)
		{
			if (pb > 4)
			{
				throw new InvalidParamException();
			}
			uint numPosStates = 1U << pb;
			this.m_LenDecoder.Create(numPosStates);
			this.m_RepLenDecoder.Create(numPosStates);
			this.m_PosStateMask = numPosStates - 1U;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001B260 File Offset: 0x00019460
		private void Init(Stream inStream, Stream outStream)
		{
			this.m_RangeDecoder.Init(inStream);
			this.m_OutWindow.Init(outStream, this._solid);
			for (uint i = 0U; i < 12U; i += 1U)
			{
				for (uint j = 0U; j <= this.m_PosStateMask; j += 1U)
				{
					uint index = (i << 4) + j;
					this.m_IsMatchDecoders[(int)index].Init();
					this.m_IsRep0LongDecoders[(int)index].Init();
				}
				this.m_IsRepDecoders[(int)i].Init();
				this.m_IsRepG0Decoders[(int)i].Init();
				this.m_IsRepG1Decoders[(int)i].Init();
				this.m_IsRepG2Decoders[(int)i].Init();
			}
			this.m_LiteralDecoder.Init();
			for (uint i = 0U; i < 4U; i += 1U)
			{
				this.m_PosSlotDecoder[(int)i].Init();
			}
			for (uint i = 0U; i < 114U; i += 1U)
			{
				this.m_PosDecoders[(int)i].Init();
			}
			this.m_LenDecoder.Init();
			this.m_RepLenDecoder.Init();
			this.m_PosAlignDecoder.Init();
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001B384 File Offset: 0x00019584
		public void Code(Stream inStream, Stream outStream, long inSize, long outSize, ICodeProgress progress)
		{
			this.Init(inStream, outStream);
			Base.State state = default(Base.State);
			state.Init();
			uint rep0 = 0U;
			uint rep = 0U;
			uint rep2 = 0U;
			uint rep3 = 0U;
			ulong nowPos64 = 0UL;
			if (nowPos64 < (ulong)outSize)
			{
				if (this.m_IsMatchDecoders[(int)((int)state.Index << 4)].Decode(this.m_RangeDecoder) != 0U)
				{
					throw new DataErrorException();
				}
				state.UpdateChar();
				byte b = this.m_LiteralDecoder.DecodeNormal(this.m_RangeDecoder, 0U, 0);
				this.m_OutWindow.PutByte(b);
				nowPos64 += 1UL;
			}
			while (nowPos64 < (ulong)outSize)
			{
				uint posState = (uint)nowPos64 & this.m_PosStateMask;
				if (this.m_IsMatchDecoders[(int)((state.Index << 4) + posState)].Decode(this.m_RangeDecoder) == 0U)
				{
					byte prevByte = this.m_OutWindow.GetByte(0U);
					byte b2;
					if (!state.IsCharState())
					{
						b2 = this.m_LiteralDecoder.DecodeWithMatchByte(this.m_RangeDecoder, (uint)nowPos64, prevByte, this.m_OutWindow.GetByte(rep0));
					}
					else
					{
						b2 = this.m_LiteralDecoder.DecodeNormal(this.m_RangeDecoder, (uint)nowPos64, prevByte);
					}
					this.m_OutWindow.PutByte(b2);
					state.UpdateChar();
					nowPos64 += 1UL;
				}
				else
				{
					uint len;
					if (this.m_IsRepDecoders[(int)state.Index].Decode(this.m_RangeDecoder) == 1U)
					{
						if (this.m_IsRepG0Decoders[(int)state.Index].Decode(this.m_RangeDecoder) == 0U)
						{
							if (this.m_IsRep0LongDecoders[(int)((state.Index << 4) + posState)].Decode(this.m_RangeDecoder) == 0U)
							{
								state.UpdateShortRep();
								this.m_OutWindow.PutByte(this.m_OutWindow.GetByte(rep0));
								nowPos64 += 1UL;
								continue;
							}
						}
						else
						{
							uint distance;
							if (this.m_IsRepG1Decoders[(int)state.Index].Decode(this.m_RangeDecoder) == 0U)
							{
								distance = rep;
							}
							else
							{
								if (this.m_IsRepG2Decoders[(int)state.Index].Decode(this.m_RangeDecoder) == 0U)
								{
									distance = rep2;
								}
								else
								{
									distance = rep3;
									rep3 = rep2;
								}
								rep2 = rep;
							}
							rep = rep0;
							rep0 = distance;
						}
						len = this.m_RepLenDecoder.Decode(this.m_RangeDecoder, posState) + 2U;
						state.UpdateRep();
					}
					else
					{
						rep3 = rep2;
						rep2 = rep;
						rep = rep0;
						len = 2U + this.m_LenDecoder.Decode(this.m_RangeDecoder, posState);
						state.UpdateMatch();
						uint posSlot = this.m_PosSlotDecoder[(int)Base.GetLenToPosState(len)].Decode(this.m_RangeDecoder);
						if (posSlot >= 4U)
						{
							int numDirectBits = (int)((posSlot >> 1) - 1U);
							rep0 = (2U | (posSlot & 1U)) << numDirectBits;
							if (posSlot < 14U)
							{
								rep0 += BitTreeDecoder.ReverseDecode(this.m_PosDecoders, rep0 - posSlot - 1U, this.m_RangeDecoder, numDirectBits);
							}
							else
							{
								rep0 += this.m_RangeDecoder.DecodeDirectBits(numDirectBits - 4) << 4;
								rep0 += this.m_PosAlignDecoder.ReverseDecode(this.m_RangeDecoder);
							}
						}
						else
						{
							rep0 = posSlot;
						}
					}
					if ((ulong)rep0 >= (ulong)this.m_OutWindow.TrainSize + nowPos64 || rep0 >= this.m_DictionarySizeCheck)
					{
						if (rep0 != 4294967295U)
						{
							throw new DataErrorException();
						}
						break;
					}
					else
					{
						this.m_OutWindow.CopyBlock(rep0, len);
						nowPos64 += (ulong)len;
					}
				}
			}
			this.m_OutWindow.Flush();
			this.m_OutWindow.ReleaseStream();
			this.m_RangeDecoder.ReleaseStream();
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001B6DC File Offset: 0x000198DC
		public void SetDecoderProperties(byte[] properties)
		{
			if (properties.Length < 5)
			{
				throw new InvalidParamException();
			}
			int lc = (int)(properties[0] % 9);
			byte b = properties[0] / 9;
			int lp = (int)(b % 5);
			int pb = (int)(b / 5);
			if (pb > 4)
			{
				throw new InvalidParamException();
			}
			uint dictionarySize = 0U;
			for (int i = 0; i < 4; i++)
			{
				dictionarySize += (uint)((uint)properties[1 + i] << i * 8);
			}
			this.SetDictionarySize(dictionarySize);
			this.SetLiteralProperties(lp, lc);
			this.SetPosBitsProperties(pb);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001B74C File Offset: 0x0001994C
		public bool Train(Stream stream)
		{
			this._solid = true;
			return this.m_OutWindow.Train(stream);
		}

		// Token: 0x04000A6D RID: 2669
		private SevenZip.Compression.LZ.OutWindow m_OutWindow = new SevenZip.Compression.LZ.OutWindow();

		// Token: 0x04000A6E RID: 2670
		private Decoder m_RangeDecoder = new Decoder();

		// Token: 0x04000A6F RID: 2671
		private BitDecoder[] m_IsMatchDecoders = new BitDecoder[192];

		// Token: 0x04000A70 RID: 2672
		private BitDecoder[] m_IsRepDecoders = new BitDecoder[12];

		// Token: 0x04000A71 RID: 2673
		private BitDecoder[] m_IsRepG0Decoders = new BitDecoder[12];

		// Token: 0x04000A72 RID: 2674
		private BitDecoder[] m_IsRepG1Decoders = new BitDecoder[12];

		// Token: 0x04000A73 RID: 2675
		private BitDecoder[] m_IsRepG2Decoders = new BitDecoder[12];

		// Token: 0x04000A74 RID: 2676
		private BitDecoder[] m_IsRep0LongDecoders = new BitDecoder[192];

		// Token: 0x04000A75 RID: 2677
		private BitTreeDecoder[] m_PosSlotDecoder = new BitTreeDecoder[4];

		// Token: 0x04000A76 RID: 2678
		private BitDecoder[] m_PosDecoders = new BitDecoder[114];

		// Token: 0x04000A77 RID: 2679
		private BitTreeDecoder m_PosAlignDecoder = new BitTreeDecoder(4);

		// Token: 0x04000A78 RID: 2680
		private Decoder.LenDecoder m_LenDecoder = new Decoder.LenDecoder();

		// Token: 0x04000A79 RID: 2681
		private Decoder.LenDecoder m_RepLenDecoder = new Decoder.LenDecoder();

		// Token: 0x04000A7A RID: 2682
		private Decoder.LiteralDecoder m_LiteralDecoder = new Decoder.LiteralDecoder();

		// Token: 0x04000A7B RID: 2683
		private uint m_DictionarySize;

		// Token: 0x04000A7C RID: 2684
		private uint m_DictionarySizeCheck;

		// Token: 0x04000A7D RID: 2685
		private uint m_PosStateMask;

		// Token: 0x04000A7E RID: 2686
		private bool _solid;

		// Token: 0x02000196 RID: 406
		private class LenDecoder
		{
			// Token: 0x060005CE RID: 1486 RVA: 0x0001B764 File Offset: 0x00019964
			public void Create(uint numPosStates)
			{
				for (uint posState = this.m_NumPosStates; posState < numPosStates; posState += 1U)
				{
					this.m_LowCoder[(int)posState] = new BitTreeDecoder(3);
					this.m_MidCoder[(int)posState] = new BitTreeDecoder(3);
				}
				this.m_NumPosStates = numPosStates;
			}

			// Token: 0x060005CF RID: 1487 RVA: 0x0001B7B0 File Offset: 0x000199B0
			public void Init()
			{
				this.m_Choice.Init();
				for (uint posState = 0U; posState < this.m_NumPosStates; posState += 1U)
				{
					this.m_LowCoder[(int)posState].Init();
					this.m_MidCoder[(int)posState].Init();
				}
				this.m_Choice2.Init();
				this.m_HighCoder.Init();
			}

			// Token: 0x060005D0 RID: 1488 RVA: 0x0001B814 File Offset: 0x00019A14
			public uint Decode(Decoder rangeDecoder, uint posState)
			{
				if (this.m_Choice.Decode(rangeDecoder) == 0U)
				{
					return this.m_LowCoder[(int)posState].Decode(rangeDecoder);
				}
				uint symbol = 8U;
				if (this.m_Choice2.Decode(rangeDecoder) == 0U)
				{
					symbol += this.m_MidCoder[(int)posState].Decode(rangeDecoder);
				}
				else
				{
					symbol += 8U;
					symbol += this.m_HighCoder.Decode(rangeDecoder);
				}
				return symbol;
			}

			// Token: 0x04000A7F RID: 2687
			private BitDecoder m_Choice;

			// Token: 0x04000A80 RID: 2688
			private BitDecoder m_Choice2;

			// Token: 0x04000A81 RID: 2689
			private BitTreeDecoder[] m_LowCoder = new BitTreeDecoder[16];

			// Token: 0x04000A82 RID: 2690
			private BitTreeDecoder[] m_MidCoder = new BitTreeDecoder[16];

			// Token: 0x04000A83 RID: 2691
			private BitTreeDecoder m_HighCoder = new BitTreeDecoder(8);

			// Token: 0x04000A84 RID: 2692
			private uint m_NumPosStates;
		}

		// Token: 0x02000197 RID: 407
		private class LiteralDecoder
		{
			// Token: 0x060005D2 RID: 1490 RVA: 0x0001B8AC File Offset: 0x00019AAC
			public void Create(int numPosBits, int numPrevBits)
			{
				if (this.m_Coders != null && this.m_NumPrevBits == numPrevBits && this.m_NumPosBits == numPosBits)
				{
					return;
				}
				this.m_NumPosBits = numPosBits;
				this.m_PosMask = (1U << numPosBits) - 1U;
				this.m_NumPrevBits = numPrevBits;
				uint numStates = 1U << this.m_NumPrevBits + this.m_NumPosBits;
				this.m_Coders = new Decoder.LiteralDecoder.Decoder2[numStates];
				for (uint i = 0U; i < numStates; i += 1U)
				{
					this.m_Coders[(int)i].Create();
				}
			}

			// Token: 0x060005D3 RID: 1491 RVA: 0x0001B92C File Offset: 0x00019B2C
			public void Init()
			{
				uint numStates = 1U << this.m_NumPrevBits + this.m_NumPosBits;
				for (uint i = 0U; i < numStates; i += 1U)
				{
					this.m_Coders[(int)i].Init();
				}
			}

			// Token: 0x060005D4 RID: 1492 RVA: 0x0001B969 File Offset: 0x00019B69
			private uint GetState(uint pos, byte prevByte)
			{
				return ((pos & this.m_PosMask) << this.m_NumPrevBits) + (uint)(prevByte >> 8 - this.m_NumPrevBits);
			}

			// Token: 0x060005D5 RID: 1493 RVA: 0x0001B98B File Offset: 0x00019B8B
			public byte DecodeNormal(Decoder rangeDecoder, uint pos, byte prevByte)
			{
				return this.m_Coders[(int)this.GetState(pos, prevByte)].DecodeNormal(rangeDecoder);
			}

			// Token: 0x060005D6 RID: 1494 RVA: 0x0001B9A6 File Offset: 0x00019BA6
			public byte DecodeWithMatchByte(Decoder rangeDecoder, uint pos, byte prevByte, byte matchByte)
			{
				return this.m_Coders[(int)this.GetState(pos, prevByte)].DecodeWithMatchByte(rangeDecoder, matchByte);
			}

			// Token: 0x04000A85 RID: 2693
			private Decoder.LiteralDecoder.Decoder2[] m_Coders;

			// Token: 0x04000A86 RID: 2694
			private int m_NumPrevBits;

			// Token: 0x04000A87 RID: 2695
			private int m_NumPosBits;

			// Token: 0x04000A88 RID: 2696
			private uint m_PosMask;

			// Token: 0x02000198 RID: 408
			private struct Decoder2
			{
				// Token: 0x060005D8 RID: 1496 RVA: 0x0001B9C3 File Offset: 0x00019BC3
				public void Create()
				{
					this.m_Decoders = new BitDecoder[768];
				}

				// Token: 0x060005D9 RID: 1497 RVA: 0x0001B9D8 File Offset: 0x00019BD8
				public void Init()
				{
					for (int i = 0; i < 768; i++)
					{
						this.m_Decoders[i].Init();
					}
				}

				// Token: 0x060005DA RID: 1498 RVA: 0x0001BA08 File Offset: 0x00019C08
				public byte DecodeNormal(Decoder rangeDecoder)
				{
					uint symbol = 1U;
					do
					{
						symbol = (symbol << 1) | this.m_Decoders[(int)symbol].Decode(rangeDecoder);
					}
					while (symbol < 256U);
					return (byte)symbol;
				}

				// Token: 0x060005DB RID: 1499 RVA: 0x0001BA38 File Offset: 0x00019C38
				public byte DecodeWithMatchByte(Decoder rangeDecoder, byte matchByte)
				{
					uint symbol = 1U;
					for (;;)
					{
						uint matchBit = (uint)((matchByte >> 7) & 1);
						matchByte = (byte)(matchByte << 1);
						uint bit = this.m_Decoders[(int)((1U + matchBit << 8) + symbol)].Decode(rangeDecoder);
						symbol = (symbol << 1) | bit;
						if (matchBit != bit)
						{
							break;
						}
						if (symbol >= 256U)
						{
							goto IL_005C;
						}
					}
					while (symbol < 256U)
					{
						symbol = (symbol << 1) | this.m_Decoders[(int)symbol].Decode(rangeDecoder);
					}
					IL_005C:
					return (byte)symbol;
				}

				// Token: 0x04000A89 RID: 2697
				private BitDecoder[] m_Decoders;
			}
		}
	}
}
