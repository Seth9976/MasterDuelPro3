using System;
using System.IO;
using SevenZip.Compression.LZ;
using SevenZip.Compression.RangeCoder;

namespace SevenZip.Compression.LZMA
{
	// Token: 0x02000199 RID: 409
	public class Encoder : ICoder, ISetCoderProperties, IWriteCoderProperties
	{
		// Token: 0x060005DC RID: 1500 RVA: 0x0001BAA4 File Offset: 0x00019CA4
		static Encoder()
		{
			int c = 2;
			Encoder.g_FastPos[0] = 0;
			Encoder.g_FastPos[1] = 1;
			for (byte slotFast = 2; slotFast < 22; slotFast += 1)
			{
				uint i = 1U << (slotFast >> 1) - 1;
				uint j = 0U;
				while (j < i)
				{
					Encoder.g_FastPos[c] = slotFast;
					j += 1U;
					c++;
				}
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001BB1E File Offset: 0x00019D1E
		private static uint GetPosSlot(uint pos)
		{
			if (pos < 2048U)
			{
				return (uint)Encoder.g_FastPos[(int)pos];
			}
			if (pos < 2097152U)
			{
				return (uint)(Encoder.g_FastPos[(int)(pos >> 10)] + 20);
			}
			return (uint)(Encoder.g_FastPos[(int)(pos >> 20)] + 40);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001BB53 File Offset: 0x00019D53
		private static uint GetPosSlot2(uint pos)
		{
			if (pos < 131072U)
			{
				return (uint)(Encoder.g_FastPos[(int)(pos >> 6)] + 12);
			}
			if (pos < 134217728U)
			{
				return (uint)(Encoder.g_FastPos[(int)(pos >> 16)] + 32);
			}
			return (uint)(Encoder.g_FastPos[(int)(pos >> 26)] + 52);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001BB90 File Offset: 0x00019D90
		private void BaseInit()
		{
			this._state.Init();
			this._previousByte = 0;
			for (uint i = 0U; i < 4U; i += 1U)
			{
				this._repDistances[(int)i] = 0U;
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001BBC4 File Offset: 0x00019DC4
		private void Create()
		{
			if (this._matchFinder == null)
			{
				SevenZip.Compression.LZ.BinTree bt = new SevenZip.Compression.LZ.BinTree();
				int numHashBytes = 4;
				if (this._matchFinderType == Encoder.EMatchFinderType.BT2)
				{
					numHashBytes = 2;
				}
				bt.SetType(numHashBytes);
				this._matchFinder = bt;
			}
			this._literalEncoder.Create(this._numLiteralPosStateBits, this._numLiteralContextBits);
			if (this._dictionarySize == this._dictionarySizePrev && this._numFastBytesPrev == this._numFastBytes)
			{
				return;
			}
			this._matchFinder.Create(this._dictionarySize, 4096U, this._numFastBytes, 274U);
			this._dictionarySizePrev = this._dictionarySize;
			this._numFastBytesPrev = this._numFastBytes;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001BC68 File Offset: 0x00019E68
		public Encoder()
		{
			int i = 0;
			while ((long)i < 4096L)
			{
				this._optimum[i] = new Encoder.Optimal();
				i++;
			}
			int j = 0;
			while ((long)j < 4L)
			{
				this._posSlotEncoder[j] = new BitTreeEncoder(6);
				j++;
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001BE31 File Offset: 0x0001A031
		private void SetWriteEndMarkerMode(bool writeEndMarker)
		{
			this._writeEndMark = writeEndMarker;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001BE3C File Offset: 0x0001A03C
		private void Init()
		{
			this.BaseInit();
			this._rangeEncoder.Init();
			for (uint i = 0U; i < 12U; i += 1U)
			{
				for (uint j = 0U; j <= this._posStateMask; j += 1U)
				{
					uint complexState = (i << 4) + j;
					this._isMatch[(int)complexState].Init();
					this._isRep0Long[(int)complexState].Init();
				}
				this._isRep[(int)i].Init();
				this._isRepG0[(int)i].Init();
				this._isRepG1[(int)i].Init();
				this._isRepG2[(int)i].Init();
			}
			this._literalEncoder.Init();
			for (uint i = 0U; i < 4U; i += 1U)
			{
				this._posSlotEncoder[(int)i].Init();
			}
			for (uint i = 0U; i < 114U; i += 1U)
			{
				this._posEncoders[(int)i].Init();
			}
			this._lenEncoder.Init(1U << this._posStateBits);
			this._repMatchLenEncoder.Init(1U << this._posStateBits);
			this._posAlignEncoder.Init();
			this._longestMatchWasFound = false;
			this._optimumEndIndex = 0U;
			this._optimumCurrentIndex = 0U;
			this._additionalOffset = 0U;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001BF84 File Offset: 0x0001A184
		private void ReadMatchDistances(out uint lenRes, out uint numDistancePairs)
		{
			lenRes = 0U;
			numDistancePairs = this._matchFinder.GetMatches(this._matchDistances);
			if (numDistancePairs > 0U)
			{
				lenRes = this._matchDistances[(int)(numDistancePairs - 2U)];
				if (lenRes == this._numFastBytes)
				{
					lenRes += this._matchFinder.GetMatchLen((int)(lenRes - 1U), this._matchDistances[(int)(numDistancePairs - 1U)], 273U - lenRes);
				}
			}
			this._additionalOffset += 1U;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001BFF8 File Offset: 0x0001A1F8
		private void MovePos(uint num)
		{
			if (num > 0U)
			{
				this._matchFinder.Skip(num);
				this._additionalOffset += num;
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001C018 File Offset: 0x0001A218
		private uint GetRepLen1Price(Base.State state, uint posState)
		{
			return this._isRepG0[(int)state.Index].GetPrice0() + this._isRep0Long[(int)((state.Index << 4) + posState)].GetPrice0();
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001C04C File Offset: 0x0001A24C
		private uint GetPureRepPrice(uint repIndex, Base.State state, uint posState)
		{
			uint price;
			if (repIndex == 0U)
			{
				price = this._isRepG0[(int)state.Index].GetPrice0();
				price += this._isRep0Long[(int)((state.Index << 4) + posState)].GetPrice1();
			}
			else
			{
				price = this._isRepG0[(int)state.Index].GetPrice1();
				if (repIndex == 1U)
				{
					price += this._isRepG1[(int)state.Index].GetPrice0();
				}
				else
				{
					price += this._isRepG1[(int)state.Index].GetPrice1();
					price += this._isRepG2[(int)state.Index].GetPrice(repIndex - 2U);
				}
			}
			return price;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001C0FE File Offset: 0x0001A2FE
		private uint GetRepPrice(uint repIndex, uint len, Base.State state, uint posState)
		{
			return this._repMatchLenEncoder.GetPrice(len - 2U, posState) + this.GetPureRepPrice(repIndex, state, posState);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001C11C File Offset: 0x0001A31C
		private uint GetPosLenPrice(uint pos, uint len, uint posState)
		{
			uint lenToPosState = Base.GetLenToPosState(len);
			uint price;
			if (pos < 128U)
			{
				price = this._distancesPrices[(int)(lenToPosState * 128U + pos)];
			}
			else
			{
				price = this._posSlotPrices[(int)((lenToPosState << 6) + Encoder.GetPosSlot2(pos))] + this._alignPrices[(int)(pos & 15U)];
			}
			return price + this._lenEncoder.GetPrice(len - 2U, posState);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001C17C File Offset: 0x0001A37C
		private uint Backward(out uint backRes, uint cur)
		{
			this._optimumEndIndex = cur;
			uint posMem = this._optimum[(int)cur].PosPrev;
			uint backMem = this._optimum[(int)cur].BackPrev;
			do
			{
				if (this._optimum[(int)cur].Prev1IsChar)
				{
					this._optimum[(int)posMem].MakeAsChar();
					this._optimum[(int)posMem].PosPrev = posMem - 1U;
					if (this._optimum[(int)cur].Prev2)
					{
						this._optimum[(int)(posMem - 1U)].Prev1IsChar = false;
						this._optimum[(int)(posMem - 1U)].PosPrev = this._optimum[(int)cur].PosPrev2;
						this._optimum[(int)(posMem - 1U)].BackPrev = this._optimum[(int)cur].BackPrev2;
					}
				}
				uint posPrev = posMem;
				uint backCur = backMem;
				backMem = this._optimum[(int)posPrev].BackPrev;
				posMem = this._optimum[(int)posPrev].PosPrev;
				this._optimum[(int)posPrev].BackPrev = backCur;
				this._optimum[(int)posPrev].PosPrev = cur;
				cur = posPrev;
			}
			while (cur > 0U);
			backRes = this._optimum[0].BackPrev;
			this._optimumCurrentIndex = this._optimum[0].PosPrev;
			return this._optimumCurrentIndex;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001C2A0 File Offset: 0x0001A4A0
		private uint GetOptimum(uint position, out uint backRes)
		{
			if (this._optimumEndIndex != this._optimumCurrentIndex)
			{
				uint num = this._optimum[(int)this._optimumCurrentIndex].PosPrev - this._optimumCurrentIndex;
				backRes = this._optimum[(int)this._optimumCurrentIndex].BackPrev;
				this._optimumCurrentIndex = this._optimum[(int)this._optimumCurrentIndex].PosPrev;
				return num;
			}
			this._optimumCurrentIndex = (this._optimumEndIndex = 0U);
			uint lenMain;
			uint numDistancePairs;
			if (!this._longestMatchWasFound)
			{
				this.ReadMatchDistances(out lenMain, out numDistancePairs);
			}
			else
			{
				lenMain = this._longestMatchLength;
				numDistancePairs = this._numDistancePairs;
				this._longestMatchWasFound = false;
			}
			uint numAvailableBytes = this._matchFinder.GetNumAvailableBytes() + 1U;
			if (numAvailableBytes < 2U)
			{
				backRes = uint.MaxValue;
				return 1U;
			}
			if (numAvailableBytes > 273U)
			{
			}
			uint repMaxIndex = 0U;
			for (uint i = 0U; i < 4U; i += 1U)
			{
				this.reps[(int)i] = this._repDistances[(int)i];
				this.repLens[(int)i] = this._matchFinder.GetMatchLen(-1, this.reps[(int)i], 273U);
				if (this.repLens[(int)i] > this.repLens[(int)repMaxIndex])
				{
					repMaxIndex = i;
				}
			}
			if (this.repLens[(int)repMaxIndex] >= this._numFastBytes)
			{
				backRes = repMaxIndex;
				uint lenRes = this.repLens[(int)repMaxIndex];
				this.MovePos(lenRes - 1U);
				return lenRes;
			}
			if (lenMain >= this._numFastBytes)
			{
				backRes = this._matchDistances[(int)(numDistancePairs - 1U)] + 4U;
				this.MovePos(lenMain - 1U);
				return lenMain;
			}
			byte currentByte = this._matchFinder.GetIndexByte(-1);
			byte matchByte = this._matchFinder.GetIndexByte((int)(0U - this._repDistances[0] - 1U - 1U));
			if (lenMain < 2U && currentByte != matchByte && this.repLens[(int)repMaxIndex] < 2U)
			{
				backRes = uint.MaxValue;
				return 1U;
			}
			this._optimum[0].State = this._state;
			uint posState = position & this._posStateMask;
			this._optimum[1].Price = this._isMatch[(int)((this._state.Index << 4) + posState)].GetPrice0() + this._literalEncoder.GetSubCoder(position, this._previousByte).GetPrice(!this._state.IsCharState(), matchByte, currentByte);
			this._optimum[1].MakeAsChar();
			uint matchPrice = this._isMatch[(int)((this._state.Index << 4) + posState)].GetPrice1();
			uint repMatchPrice = matchPrice + this._isRep[(int)this._state.Index].GetPrice1();
			if (matchByte == currentByte)
			{
				uint shortRepPrice = repMatchPrice + this.GetRepLen1Price(this._state, posState);
				if (shortRepPrice < this._optimum[1].Price)
				{
					this._optimum[1].Price = shortRepPrice;
					this._optimum[1].MakeAsShortRep();
				}
			}
			uint lenEnd = ((lenMain >= this.repLens[(int)repMaxIndex]) ? lenMain : this.repLens[(int)repMaxIndex]);
			if (lenEnd < 2U)
			{
				backRes = this._optimum[1].BackPrev;
				return 1U;
			}
			this._optimum[1].PosPrev = 0U;
			this._optimum[0].Backs0 = this.reps[0];
			this._optimum[0].Backs1 = this.reps[1];
			this._optimum[0].Backs2 = this.reps[2];
			this._optimum[0].Backs3 = this.reps[3];
			uint len = lenEnd;
			do
			{
				this._optimum[(int)len--].Price = 268435455U;
			}
			while (len >= 2U);
			for (uint i = 0U; i < 4U; i += 1U)
			{
				uint repLen = this.repLens[(int)i];
				if (repLen >= 2U)
				{
					uint price = repMatchPrice + this.GetPureRepPrice(i, this._state, posState);
					do
					{
						uint curAndLenPrice = price + this._repMatchLenEncoder.GetPrice(repLen - 2U, posState);
						Encoder.Optimal optimum = this._optimum[(int)repLen];
						if (curAndLenPrice < optimum.Price)
						{
							optimum.Price = curAndLenPrice;
							optimum.PosPrev = 0U;
							optimum.BackPrev = i;
							optimum.Prev1IsChar = false;
						}
					}
					while ((repLen -= 1U) >= 2U);
				}
			}
			uint normalMatchPrice = matchPrice + this._isRep[(int)this._state.Index].GetPrice0();
			len = ((this.repLens[0] >= 2U) ? (this.repLens[0] + 1U) : 2U);
			if (len <= lenMain)
			{
				uint offs = 0U;
				while (len > this._matchDistances[(int)offs])
				{
					offs += 2U;
				}
				for (;;)
				{
					uint distance = this._matchDistances[(int)(offs + 1U)];
					uint curAndLenPrice2 = normalMatchPrice + this.GetPosLenPrice(distance, len, posState);
					Encoder.Optimal optimum2 = this._optimum[(int)len];
					if (curAndLenPrice2 < optimum2.Price)
					{
						optimum2.Price = curAndLenPrice2;
						optimum2.PosPrev = 0U;
						optimum2.BackPrev = distance + 4U;
						optimum2.Prev1IsChar = false;
					}
					if (len == this._matchDistances[(int)offs])
					{
						offs += 2U;
						if (offs == numDistancePairs)
						{
							break;
						}
					}
					len += 1U;
				}
			}
			uint cur = 0U;
			uint newLen;
			for (;;)
			{
				cur += 1U;
				if (cur == lenEnd)
				{
					break;
				}
				this.ReadMatchDistances(out newLen, out numDistancePairs);
				if (newLen >= this._numFastBytes)
				{
					goto Block_24;
				}
				position += 1U;
				uint posPrev = this._optimum[(int)cur].PosPrev;
				Base.State state;
				if (this._optimum[(int)cur].Prev1IsChar)
				{
					posPrev -= 1U;
					if (this._optimum[(int)cur].Prev2)
					{
						state = this._optimum[(int)this._optimum[(int)cur].PosPrev2].State;
						if (this._optimum[(int)cur].BackPrev2 < 4U)
						{
							state.UpdateRep();
						}
						else
						{
							state.UpdateMatch();
						}
					}
					else
					{
						state = this._optimum[(int)posPrev].State;
					}
					state.UpdateChar();
				}
				else
				{
					state = this._optimum[(int)posPrev].State;
				}
				if (posPrev == cur - 1U)
				{
					if (this._optimum[(int)cur].IsShortRep())
					{
						state.UpdateShortRep();
					}
					else
					{
						state.UpdateChar();
					}
				}
				else
				{
					uint pos;
					if (this._optimum[(int)cur].Prev1IsChar && this._optimum[(int)cur].Prev2)
					{
						posPrev = this._optimum[(int)cur].PosPrev2;
						pos = this._optimum[(int)cur].BackPrev2;
						state.UpdateRep();
					}
					else
					{
						pos = this._optimum[(int)cur].BackPrev;
						if (pos < 4U)
						{
							state.UpdateRep();
						}
						else
						{
							state.UpdateMatch();
						}
					}
					Encoder.Optimal opt = this._optimum[(int)posPrev];
					if (pos < 4U)
					{
						if (pos == 0U)
						{
							this.reps[0] = opt.Backs0;
							this.reps[1] = opt.Backs1;
							this.reps[2] = opt.Backs2;
							this.reps[3] = opt.Backs3;
						}
						else if (pos == 1U)
						{
							this.reps[0] = opt.Backs1;
							this.reps[1] = opt.Backs0;
							this.reps[2] = opt.Backs2;
							this.reps[3] = opt.Backs3;
						}
						else if (pos == 2U)
						{
							this.reps[0] = opt.Backs2;
							this.reps[1] = opt.Backs0;
							this.reps[2] = opt.Backs1;
							this.reps[3] = opt.Backs3;
						}
						else
						{
							this.reps[0] = opt.Backs3;
							this.reps[1] = opt.Backs0;
							this.reps[2] = opt.Backs1;
							this.reps[3] = opt.Backs2;
						}
					}
					else
					{
						this.reps[0] = pos - 4U;
						this.reps[1] = opt.Backs0;
						this.reps[2] = opt.Backs1;
						this.reps[3] = opt.Backs2;
					}
				}
				this._optimum[(int)cur].State = state;
				this._optimum[(int)cur].Backs0 = this.reps[0];
				this._optimum[(int)cur].Backs1 = this.reps[1];
				this._optimum[(int)cur].Backs2 = this.reps[2];
				this._optimum[(int)cur].Backs3 = this.reps[3];
				uint price2 = this._optimum[(int)cur].Price;
				currentByte = this._matchFinder.GetIndexByte(-1);
				matchByte = this._matchFinder.GetIndexByte((int)(0U - this.reps[0] - 1U - 1U));
				posState = position & this._posStateMask;
				uint curAnd1Price = price2 + this._isMatch[(int)((state.Index << 4) + posState)].GetPrice0() + this._literalEncoder.GetSubCoder(position, this._matchFinder.GetIndexByte(-2)).GetPrice(!state.IsCharState(), matchByte, currentByte);
				Encoder.Optimal nextOptimum = this._optimum[(int)(cur + 1U)];
				bool nextIsChar = false;
				if (curAnd1Price < nextOptimum.Price)
				{
					nextOptimum.Price = curAnd1Price;
					nextOptimum.PosPrev = cur;
					nextOptimum.MakeAsChar();
					nextIsChar = true;
				}
				matchPrice = price2 + this._isMatch[(int)((state.Index << 4) + posState)].GetPrice1();
				repMatchPrice = matchPrice + this._isRep[(int)state.Index].GetPrice1();
				if (matchByte == currentByte && (nextOptimum.PosPrev >= cur || nextOptimum.BackPrev != 0U))
				{
					uint shortRepPrice2 = repMatchPrice + this.GetRepLen1Price(state, posState);
					if (shortRepPrice2 <= nextOptimum.Price)
					{
						nextOptimum.Price = shortRepPrice2;
						nextOptimum.PosPrev = cur;
						nextOptimum.MakeAsShortRep();
						nextIsChar = true;
					}
				}
				uint numAvailableBytesFull = this._matchFinder.GetNumAvailableBytes() + 1U;
				numAvailableBytesFull = Math.Min(4095U - cur, numAvailableBytesFull);
				numAvailableBytes = numAvailableBytesFull;
				if (numAvailableBytes >= 2U)
				{
					if (numAvailableBytes > this._numFastBytes)
					{
						numAvailableBytes = this._numFastBytes;
					}
					if (!nextIsChar && matchByte != currentByte)
					{
						uint t = Math.Min(numAvailableBytesFull - 1U, this._numFastBytes);
						uint lenTest2 = this._matchFinder.GetMatchLen(0, this.reps[0], t);
						if (lenTest2 >= 2U)
						{
							Base.State state2 = state;
							state2.UpdateChar();
							uint posStateNext = (position + 1U) & this._posStateMask;
							uint nextRepMatchPrice = curAnd1Price + this._isMatch[(int)((state2.Index << 4) + posStateNext)].GetPrice1() + this._isRep[(int)state2.Index].GetPrice1();
							uint offset = cur + 1U + lenTest2;
							while (lenEnd < offset)
							{
								this._optimum[(int)(lenEnd += 1U)].Price = 268435455U;
							}
							uint curAndLenPrice3 = nextRepMatchPrice + this.GetRepPrice(0U, lenTest2, state2, posStateNext);
							Encoder.Optimal optimum3 = this._optimum[(int)offset];
							if (curAndLenPrice3 < optimum3.Price)
							{
								optimum3.Price = curAndLenPrice3;
								optimum3.PosPrev = cur + 1U;
								optimum3.BackPrev = 0U;
								optimum3.Prev1IsChar = true;
								optimum3.Prev2 = false;
							}
						}
					}
					uint startLen = 2U;
					for (uint repIndex = 0U; repIndex < 4U; repIndex += 1U)
					{
						uint lenTest3 = this._matchFinder.GetMatchLen(-1, this.reps[(int)repIndex], numAvailableBytes);
						if (lenTest3 >= 2U)
						{
							uint lenTestTemp = lenTest3;
							for (;;)
							{
								if (lenEnd >= cur + lenTest3)
								{
									uint curAndLenPrice4 = repMatchPrice + this.GetRepPrice(repIndex, lenTest3, state, posState);
									Encoder.Optimal optimum4 = this._optimum[(int)(cur + lenTest3)];
									if (curAndLenPrice4 < optimum4.Price)
									{
										optimum4.Price = curAndLenPrice4;
										optimum4.PosPrev = cur;
										optimum4.BackPrev = repIndex;
										optimum4.Prev1IsChar = false;
									}
									if ((lenTest3 -= 1U) < 2U)
									{
										break;
									}
								}
								else
								{
									this._optimum[(int)(lenEnd += 1U)].Price = 268435455U;
								}
							}
							lenTest3 = lenTestTemp;
							if (repIndex == 0U)
							{
								startLen = lenTest3 + 1U;
							}
							if (lenTest3 < numAvailableBytesFull)
							{
								uint t2 = Math.Min(numAvailableBytesFull - 1U - lenTest3, this._numFastBytes);
								uint lenTest4 = this._matchFinder.GetMatchLen((int)lenTest3, this.reps[(int)repIndex], t2);
								if (lenTest4 >= 2U)
								{
									Base.State state3 = state;
									state3.UpdateRep();
									uint posStateNext2 = (position + lenTest3) & this._posStateMask;
									uint num2 = repMatchPrice + this.GetRepPrice(repIndex, lenTest3, state, posState) + this._isMatch[(int)((state3.Index << 4) + posStateNext2)].GetPrice0() + this._literalEncoder.GetSubCoder(position + lenTest3, this._matchFinder.GetIndexByte((int)(lenTest3 - 1U - 1U))).GetPrice(true, this._matchFinder.GetIndexByte((int)(lenTest3 - 1U - (this.reps[(int)repIndex] + 1U))), this._matchFinder.GetIndexByte((int)(lenTest3 - 1U)));
									state3.UpdateChar();
									posStateNext2 = (position + lenTest3 + 1U) & this._posStateMask;
									uint nextRepMatchPrice2 = num2 + this._isMatch[(int)((state3.Index << 4) + posStateNext2)].GetPrice1() + this._isRep[(int)state3.Index].GetPrice1();
									uint offset2 = lenTest3 + 1U + lenTest4;
									while (lenEnd < cur + offset2)
									{
										this._optimum[(int)(lenEnd += 1U)].Price = 268435455U;
									}
									uint curAndLenPrice5 = nextRepMatchPrice2 + this.GetRepPrice(0U, lenTest4, state3, posStateNext2);
									Encoder.Optimal optimum5 = this._optimum[(int)(cur + offset2)];
									if (curAndLenPrice5 < optimum5.Price)
									{
										optimum5.Price = curAndLenPrice5;
										optimum5.PosPrev = cur + lenTest3 + 1U;
										optimum5.BackPrev = 0U;
										optimum5.Prev1IsChar = true;
										optimum5.Prev2 = true;
										optimum5.PosPrev2 = cur;
										optimum5.BackPrev2 = repIndex;
									}
								}
							}
						}
					}
					if (newLen > numAvailableBytes)
					{
						newLen = numAvailableBytes;
						numDistancePairs = 0U;
						while (newLen > this._matchDistances[(int)numDistancePairs])
						{
							numDistancePairs += 2U;
						}
						this._matchDistances[(int)numDistancePairs] = newLen;
						numDistancePairs += 2U;
					}
					if (newLen >= startLen)
					{
						normalMatchPrice = matchPrice + this._isRep[(int)state.Index].GetPrice0();
						while (lenEnd < cur + newLen)
						{
							this._optimum[(int)(lenEnd += 1U)].Price = 268435455U;
						}
						uint offs2 = 0U;
						while (startLen > this._matchDistances[(int)offs2])
						{
							offs2 += 2U;
						}
						uint lenTest5 = startLen;
						for (;;)
						{
							uint curBack = this._matchDistances[(int)(offs2 + 1U)];
							uint curAndLenPrice6 = normalMatchPrice + this.GetPosLenPrice(curBack, lenTest5, posState);
							Encoder.Optimal optimum6 = this._optimum[(int)(cur + lenTest5)];
							if (curAndLenPrice6 < optimum6.Price)
							{
								optimum6.Price = curAndLenPrice6;
								optimum6.PosPrev = cur;
								optimum6.BackPrev = curBack + 4U;
								optimum6.Prev1IsChar = false;
							}
							if (lenTest5 == this._matchDistances[(int)offs2])
							{
								if (lenTest5 < numAvailableBytesFull)
								{
									uint t3 = Math.Min(numAvailableBytesFull - 1U - lenTest5, this._numFastBytes);
									uint lenTest6 = this._matchFinder.GetMatchLen((int)lenTest5, curBack, t3);
									if (lenTest6 >= 2U)
									{
										Base.State state4 = state;
										state4.UpdateMatch();
										uint posStateNext3 = (position + lenTest5) & this._posStateMask;
										uint num3 = curAndLenPrice6 + this._isMatch[(int)((state4.Index << 4) + posStateNext3)].GetPrice0() + this._literalEncoder.GetSubCoder(position + lenTest5, this._matchFinder.GetIndexByte((int)(lenTest5 - 1U - 1U))).GetPrice(true, this._matchFinder.GetIndexByte((int)(lenTest5 - (curBack + 1U) - 1U)), this._matchFinder.GetIndexByte((int)(lenTest5 - 1U)));
										state4.UpdateChar();
										posStateNext3 = (position + lenTest5 + 1U) & this._posStateMask;
										uint nextRepMatchPrice3 = num3 + this._isMatch[(int)((state4.Index << 4) + posStateNext3)].GetPrice1() + this._isRep[(int)state4.Index].GetPrice1();
										uint offset3 = lenTest5 + 1U + lenTest6;
										while (lenEnd < cur + offset3)
										{
											this._optimum[(int)(lenEnd += 1U)].Price = 268435455U;
										}
										curAndLenPrice6 = nextRepMatchPrice3 + this.GetRepPrice(0U, lenTest6, state4, posStateNext3);
										optimum6 = this._optimum[(int)(cur + offset3)];
										if (curAndLenPrice6 < optimum6.Price)
										{
											optimum6.Price = curAndLenPrice6;
											optimum6.PosPrev = cur + lenTest5 + 1U;
											optimum6.BackPrev = 0U;
											optimum6.Prev1IsChar = true;
											optimum6.Prev2 = true;
											optimum6.PosPrev2 = cur;
											optimum6.BackPrev2 = curBack + 4U;
										}
									}
								}
								offs2 += 2U;
								if (offs2 == numDistancePairs)
								{
									break;
								}
							}
							lenTest5 += 1U;
						}
					}
				}
			}
			return this.Backward(out backRes, cur);
			Block_24:
			this._numDistancePairs = numDistancePairs;
			this._longestMatchLength = newLen;
			this._longestMatchWasFound = true;
			return this.Backward(out backRes, cur);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001D296 File Offset: 0x0001B496
		private bool ChangePair(uint smallDist, uint bigDist)
		{
			return smallDist < 33554432U && bigDist >= smallDist << 7;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001D2AC File Offset: 0x0001B4AC
		private void WriteEndMarker(uint posState)
		{
			if (!this._writeEndMark)
			{
				return;
			}
			this._isMatch[(int)((this._state.Index << 4) + posState)].Encode(this._rangeEncoder, 1U);
			this._isRep[(int)this._state.Index].Encode(this._rangeEncoder, 0U);
			this._state.UpdateMatch();
			uint len = 2U;
			this._lenEncoder.Encode(this._rangeEncoder, len - 2U, posState);
			uint posSlot = 63U;
			uint lenToPosState = Base.GetLenToPosState(len);
			this._posSlotEncoder[(int)lenToPosState].Encode(this._rangeEncoder, posSlot);
			int footerBits = 30;
			uint posReduced = (1U << footerBits) - 1U;
			this._rangeEncoder.EncodeDirectBits(posReduced >> 4, footerBits - 4);
			this._posAlignEncoder.ReverseEncode(this._rangeEncoder, posReduced & 15U);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001D383 File Offset: 0x0001B583
		private void Flush(uint nowPos)
		{
			this.ReleaseMFStream();
			this.WriteEndMarker(nowPos & this._posStateMask);
			this._rangeEncoder.FlushData();
			this._rangeEncoder.FlushStream();
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001D3B0 File Offset: 0x0001B5B0
		public void CodeOneBlock(out long inSize, out long outSize, out bool finished)
		{
			inSize = 0L;
			outSize = 0L;
			finished = true;
			if (this._inStream != null)
			{
				this._matchFinder.SetStream(this._inStream);
				this._matchFinder.Init();
				this._needReleaseMFStream = true;
				this._inStream = null;
				if (this._trainSize > 0U)
				{
					this._matchFinder.Skip(this._trainSize);
				}
			}
			if (this._finished)
			{
				return;
			}
			this._finished = true;
			long progressPosValuePrev = this.nowPos64;
			if (this.nowPos64 == 0L)
			{
				if (this._matchFinder.GetNumAvailableBytes() == 0U)
				{
					this.Flush((uint)this.nowPos64);
					return;
				}
				uint len;
				uint numDistancePairs;
				this.ReadMatchDistances(out len, out numDistancePairs);
				uint posState = (uint)this.nowPos64 & this._posStateMask;
				this._isMatch[(int)((this._state.Index << 4) + posState)].Encode(this._rangeEncoder, 0U);
				this._state.UpdateChar();
				byte curByte = this._matchFinder.GetIndexByte((int)(0U - this._additionalOffset));
				this._literalEncoder.GetSubCoder((uint)this.nowPos64, this._previousByte).Encode(this._rangeEncoder, curByte);
				this._previousByte = curByte;
				this._additionalOffset -= 1U;
				this.nowPos64 += 1L;
			}
			if (this._matchFinder.GetNumAvailableBytes() == 0U)
			{
				this.Flush((uint)this.nowPos64);
				return;
			}
			for (;;)
			{
				uint pos;
				uint len2 = this.GetOptimum((uint)this.nowPos64, out pos);
				uint posState2 = (uint)this.nowPos64 & this._posStateMask;
				uint complexState = (this._state.Index << 4) + posState2;
				if (len2 == 1U && pos == 4294967295U)
				{
					this._isMatch[(int)complexState].Encode(this._rangeEncoder, 0U);
					byte curByte2 = this._matchFinder.GetIndexByte((int)(0U - this._additionalOffset));
					Encoder.LiteralEncoder.Encoder2 subCoder = this._literalEncoder.GetSubCoder((uint)this.nowPos64, this._previousByte);
					if (!this._state.IsCharState())
					{
						byte matchByte = this._matchFinder.GetIndexByte((int)(0U - this._repDistances[0] - 1U - this._additionalOffset));
						subCoder.EncodeMatched(this._rangeEncoder, matchByte, curByte2);
					}
					else
					{
						subCoder.Encode(this._rangeEncoder, curByte2);
					}
					this._previousByte = curByte2;
					this._state.UpdateChar();
				}
				else
				{
					this._isMatch[(int)complexState].Encode(this._rangeEncoder, 1U);
					if (pos < 4U)
					{
						this._isRep[(int)this._state.Index].Encode(this._rangeEncoder, 1U);
						if (pos == 0U)
						{
							this._isRepG0[(int)this._state.Index].Encode(this._rangeEncoder, 0U);
							if (len2 == 1U)
							{
								this._isRep0Long[(int)complexState].Encode(this._rangeEncoder, 0U);
							}
							else
							{
								this._isRep0Long[(int)complexState].Encode(this._rangeEncoder, 1U);
							}
						}
						else
						{
							this._isRepG0[(int)this._state.Index].Encode(this._rangeEncoder, 1U);
							if (pos == 1U)
							{
								this._isRepG1[(int)this._state.Index].Encode(this._rangeEncoder, 0U);
							}
							else
							{
								this._isRepG1[(int)this._state.Index].Encode(this._rangeEncoder, 1U);
								this._isRepG2[(int)this._state.Index].Encode(this._rangeEncoder, pos - 2U);
							}
						}
						if (len2 == 1U)
						{
							this._state.UpdateShortRep();
						}
						else
						{
							this._repMatchLenEncoder.Encode(this._rangeEncoder, len2 - 2U, posState2);
							this._state.UpdateRep();
						}
						uint distance = this._repDistances[(int)pos];
						if (pos != 0U)
						{
							for (uint i = pos; i >= 1U; i -= 1U)
							{
								this._repDistances[(int)i] = this._repDistances[(int)(i - 1U)];
							}
							this._repDistances[0] = distance;
						}
					}
					else
					{
						this._isRep[(int)this._state.Index].Encode(this._rangeEncoder, 0U);
						this._state.UpdateMatch();
						this._lenEncoder.Encode(this._rangeEncoder, len2 - 2U, posState2);
						pos -= 4U;
						uint posSlot = Encoder.GetPosSlot(pos);
						uint lenToPosState = Base.GetLenToPosState(len2);
						this._posSlotEncoder[(int)lenToPosState].Encode(this._rangeEncoder, posSlot);
						if (posSlot >= 4U)
						{
							int footerBits = (int)((posSlot >> 1) - 1U);
							uint baseVal = (2U | (posSlot & 1U)) << footerBits;
							uint posReduced = pos - baseVal;
							if (posSlot < 14U)
							{
								BitTreeEncoder.ReverseEncode(this._posEncoders, baseVal - posSlot - 1U, this._rangeEncoder, footerBits, posReduced);
							}
							else
							{
								this._rangeEncoder.EncodeDirectBits(posReduced >> 4, footerBits - 4);
								this._posAlignEncoder.ReverseEncode(this._rangeEncoder, posReduced & 15U);
								this._alignPriceCount += 1U;
							}
						}
						uint distance2 = pos;
						for (uint j = 3U; j >= 1U; j -= 1U)
						{
							this._repDistances[(int)j] = this._repDistances[(int)(j - 1U)];
						}
						this._repDistances[0] = distance2;
						this._matchPriceCount += 1U;
					}
					this._previousByte = this._matchFinder.GetIndexByte((int)(len2 - 1U - this._additionalOffset));
				}
				this._additionalOffset -= len2;
				this.nowPos64 += (long)((ulong)len2);
				if (this._additionalOffset == 0U)
				{
					if (this._matchPriceCount >= 128U)
					{
						this.FillDistancesPrices();
					}
					if (this._alignPriceCount >= 16U)
					{
						this.FillAlignPrices();
					}
					inSize = this.nowPos64;
					outSize = this._rangeEncoder.GetProcessedSizeAdd();
					if (this._matchFinder.GetNumAvailableBytes() == 0U)
					{
						break;
					}
					if (this.nowPos64 - progressPosValuePrev >= 4096L)
					{
						goto Block_24;
					}
				}
			}
			this.Flush((uint)this.nowPos64);
			return;
			Block_24:
			this._finished = false;
			finished = false;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001D9AA File Offset: 0x0001BBAA
		private void ReleaseMFStream()
		{
			if (this._matchFinder != null && this._needReleaseMFStream)
			{
				this._matchFinder.ReleaseStream();
				this._needReleaseMFStream = false;
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001D9CE File Offset: 0x0001BBCE
		private void SetOutStream(Stream outStream)
		{
			this._rangeEncoder.SetStream(outStream);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001D9DC File Offset: 0x0001BBDC
		private void ReleaseOutStream()
		{
			this._rangeEncoder.ReleaseStream();
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001D9E9 File Offset: 0x0001BBE9
		private void ReleaseStreams()
		{
			this.ReleaseMFStream();
			this.ReleaseOutStream();
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001D9F8 File Offset: 0x0001BBF8
		private void SetStreams(Stream inStream, Stream outStream, long inSize, long outSize)
		{
			this._inStream = inStream;
			this._finished = false;
			this.Create();
			this.SetOutStream(outStream);
			this.Init();
			this.FillDistancesPrices();
			this.FillAlignPrices();
			this._lenEncoder.SetTableSize(this._numFastBytes + 1U - 2U);
			this._lenEncoder.UpdateTables(1U << this._posStateBits);
			this._repMatchLenEncoder.SetTableSize(this._numFastBytes + 1U - 2U);
			this._repMatchLenEncoder.UpdateTables(1U << this._posStateBits);
			this.nowPos64 = 0L;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001DA90 File Offset: 0x0001BC90
		public void Code(Stream inStream, Stream outStream, long inSize, long outSize, ICodeProgress progress)
		{
			this._needReleaseMFStream = false;
			try
			{
				this.SetStreams(inStream, outStream, inSize, outSize);
				for (;;)
				{
					long processedInSize;
					long processedOutSize;
					bool finished;
					this.CodeOneBlock(out processedInSize, out processedOutSize, out finished);
					if (finished)
					{
						break;
					}
					if (progress != null)
					{
						progress.SetProgress(processedInSize, processedOutSize);
					}
				}
			}
			finally
			{
				this.ReleaseStreams();
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001DAE8 File Offset: 0x0001BCE8
		public void WriteCoderProperties(Stream outStream)
		{
			this.properties[0] = (byte)((this._posStateBits * 5 + this._numLiteralPosStateBits) * 9 + this._numLiteralContextBits);
			for (int i = 0; i < 4; i++)
			{
				this.properties[1 + i] = (byte)((this._dictionarySize >> 8 * i) & 255U);
			}
			outStream.Write(this.properties, 0, 5);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001DB50 File Offset: 0x0001BD50
		private void FillDistancesPrices()
		{
			for (uint i = 4U; i < 128U; i += 1U)
			{
				uint posSlot = Encoder.GetPosSlot(i);
				int footerBits = (int)((posSlot >> 1) - 1U);
				uint baseVal = (2U | (posSlot & 1U)) << footerBits;
				this.tempPrices[(int)i] = BitTreeEncoder.ReverseGetPrice(this._posEncoders, baseVal - posSlot - 1U, footerBits, i - baseVal);
			}
			for (uint lenToPosState = 0U; lenToPosState < 4U; lenToPosState += 1U)
			{
				BitTreeEncoder encoder = this._posSlotEncoder[(int)lenToPosState];
				uint st = lenToPosState << 6;
				for (uint posSlot2 = 0U; posSlot2 < this._distTableSize; posSlot2 += 1U)
				{
					this._posSlotPrices[(int)(st + posSlot2)] = encoder.GetPrice(posSlot2);
				}
				for (uint posSlot2 = 14U; posSlot2 < this._distTableSize; posSlot2 += 1U)
				{
					this._posSlotPrices[(int)(st + posSlot2)] += (posSlot2 >> 1) - 1U - 4U << 6;
				}
				uint st2 = lenToPosState * 128U;
				uint j;
				for (j = 0U; j < 4U; j += 1U)
				{
					this._distancesPrices[(int)(st2 + j)] = this._posSlotPrices[(int)(st + j)];
				}
				while (j < 128U)
				{
					this._distancesPrices[(int)(st2 + j)] = this._posSlotPrices[(int)(st + Encoder.GetPosSlot(j))] + this.tempPrices[(int)j];
					j += 1U;
				}
			}
			this._matchPriceCount = 0U;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001DC9C File Offset: 0x0001BE9C
		private void FillAlignPrices()
		{
			for (uint i = 0U; i < 16U; i += 1U)
			{
				this._alignPrices[(int)i] = this._posAlignEncoder.ReverseGetPrice(i);
			}
			this._alignPriceCount = 0U;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001DCD4 File Offset: 0x0001BED4
		private static int FindMatchFinder(string s)
		{
			for (int i = 0; i < Encoder.kMatchFinderIDs.Length; i++)
			{
				if (s == Encoder.kMatchFinderIDs[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001DD08 File Offset: 0x0001BF08
		public void SetCoderProperties(CoderPropID[] propIDs, object[] properties)
		{
			uint i = 0U;
			while ((ulong)i < (ulong)((long)properties.Length))
			{
				object prop = properties[(int)i];
				switch (propIDs[(int)i])
				{
				case CoderPropID.DictionarySize:
				{
					if (!(prop is int))
					{
						throw new InvalidParamException();
					}
					int dictionarySize = (int)prop;
					if ((long)dictionarySize < 1L || (long)dictionarySize > 1073741824L)
					{
						throw new InvalidParamException();
					}
					this._dictionarySize = (uint)dictionarySize;
					int dicLogSize = 0;
					while ((long)dicLogSize < 30L && (long)dictionarySize > (long)(1UL << (dicLogSize & 31)))
					{
						dicLogSize++;
					}
					this._distTableSize = (uint)(dicLogSize * 2);
					break;
				}
				case CoderPropID.UsedMemorySize:
				case CoderPropID.Order:
				case CoderPropID.BlockSize:
				case CoderPropID.MatchFinderCycles:
				case CoderPropID.NumPasses:
				case CoderPropID.NumThreads:
					goto IL_021C;
				case CoderPropID.PosStateBits:
				{
					if (!(prop is int))
					{
						throw new InvalidParamException();
					}
					int v = (int)prop;
					if (v < 0 || (long)v > 4L)
					{
						throw new InvalidParamException();
					}
					this._posStateBits = v;
					this._posStateMask = (1U << this._posStateBits) - 1U;
					break;
				}
				case CoderPropID.LitContextBits:
				{
					if (!(prop is int))
					{
						throw new InvalidParamException();
					}
					int v2 = (int)prop;
					if (v2 < 0 || (long)v2 > 8L)
					{
						throw new InvalidParamException();
					}
					this._numLiteralContextBits = v2;
					break;
				}
				case CoderPropID.LitPosBits:
				{
					if (!(prop is int))
					{
						throw new InvalidParamException();
					}
					int v3 = (int)prop;
					if (v3 < 0 || (long)v3 > 4L)
					{
						throw new InvalidParamException();
					}
					this._numLiteralPosStateBits = v3;
					break;
				}
				case CoderPropID.NumFastBytes:
				{
					if (!(prop is int))
					{
						throw new InvalidParamException();
					}
					int numFastBytes = (int)prop;
					if (numFastBytes < 5 || (long)numFastBytes > 273L)
					{
						throw new InvalidParamException();
					}
					this._numFastBytes = (uint)numFastBytes;
					break;
				}
				case CoderPropID.MatchFinder:
				{
					if (!(prop is string))
					{
						throw new InvalidParamException();
					}
					Encoder.EMatchFinderType matchFinderIndexPrev = this._matchFinderType;
					int j = Encoder.FindMatchFinder(((string)prop).ToUpper());
					if (j < 0)
					{
						throw new InvalidParamException();
					}
					this._matchFinderType = (Encoder.EMatchFinderType)j;
					if (this._matchFinder != null && matchFinderIndexPrev != this._matchFinderType)
					{
						this._dictionarySizePrev = uint.MaxValue;
						this._matchFinder = null;
					}
					break;
				}
				case CoderPropID.Algorithm:
					break;
				case CoderPropID.EndMarker:
					if (!(prop is bool))
					{
						throw new InvalidParamException();
					}
					this.SetWriteEndMarkerMode((bool)prop);
					break;
				default:
					goto IL_021C;
				}
				i += 1U;
				continue;
				IL_021C:
				throw new InvalidParamException();
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001DF46 File Offset: 0x0001C146
		public void SetTrainSize(uint trainSize)
		{
			this._trainSize = trainSize;
		}

		// Token: 0x04000A8A RID: 2698
		private const uint kIfinityPrice = 268435455U;

		// Token: 0x04000A8B RID: 2699
		private static byte[] g_FastPos = new byte[2048];

		// Token: 0x04000A8C RID: 2700
		private Base.State _state;

		// Token: 0x04000A8D RID: 2701
		private byte _previousByte;

		// Token: 0x04000A8E RID: 2702
		private uint[] _repDistances = new uint[4];

		// Token: 0x04000A8F RID: 2703
		private const int kDefaultDictionaryLogSize = 22;

		// Token: 0x04000A90 RID: 2704
		private const uint kNumFastBytesDefault = 32U;

		// Token: 0x04000A91 RID: 2705
		private const uint kNumLenSpecSymbols = 16U;

		// Token: 0x04000A92 RID: 2706
		private const uint kNumOpts = 4096U;

		// Token: 0x04000A93 RID: 2707
		private Encoder.Optimal[] _optimum = new Encoder.Optimal[4096];

		// Token: 0x04000A94 RID: 2708
		private IMatchFinder _matchFinder;

		// Token: 0x04000A95 RID: 2709
		private Encoder _rangeEncoder = new Encoder();

		// Token: 0x04000A96 RID: 2710
		private BitEncoder[] _isMatch = new BitEncoder[192];

		// Token: 0x04000A97 RID: 2711
		private BitEncoder[] _isRep = new BitEncoder[12];

		// Token: 0x04000A98 RID: 2712
		private BitEncoder[] _isRepG0 = new BitEncoder[12];

		// Token: 0x04000A99 RID: 2713
		private BitEncoder[] _isRepG1 = new BitEncoder[12];

		// Token: 0x04000A9A RID: 2714
		private BitEncoder[] _isRepG2 = new BitEncoder[12];

		// Token: 0x04000A9B RID: 2715
		private BitEncoder[] _isRep0Long = new BitEncoder[192];

		// Token: 0x04000A9C RID: 2716
		private BitTreeEncoder[] _posSlotEncoder = new BitTreeEncoder[4];

		// Token: 0x04000A9D RID: 2717
		private BitEncoder[] _posEncoders = new BitEncoder[114];

		// Token: 0x04000A9E RID: 2718
		private BitTreeEncoder _posAlignEncoder = new BitTreeEncoder(4);

		// Token: 0x04000A9F RID: 2719
		private Encoder.LenPriceTableEncoder _lenEncoder = new Encoder.LenPriceTableEncoder();

		// Token: 0x04000AA0 RID: 2720
		private Encoder.LenPriceTableEncoder _repMatchLenEncoder = new Encoder.LenPriceTableEncoder();

		// Token: 0x04000AA1 RID: 2721
		private Encoder.LiteralEncoder _literalEncoder = new Encoder.LiteralEncoder();

		// Token: 0x04000AA2 RID: 2722
		private uint[] _matchDistances = new uint[548];

		// Token: 0x04000AA3 RID: 2723
		private uint _numFastBytes = 32U;

		// Token: 0x04000AA4 RID: 2724
		private uint _longestMatchLength;

		// Token: 0x04000AA5 RID: 2725
		private uint _numDistancePairs;

		// Token: 0x04000AA6 RID: 2726
		private uint _additionalOffset;

		// Token: 0x04000AA7 RID: 2727
		private uint _optimumEndIndex;

		// Token: 0x04000AA8 RID: 2728
		private uint _optimumCurrentIndex;

		// Token: 0x04000AA9 RID: 2729
		private bool _longestMatchWasFound;

		// Token: 0x04000AAA RID: 2730
		private uint[] _posSlotPrices = new uint[256];

		// Token: 0x04000AAB RID: 2731
		private uint[] _distancesPrices = new uint[512];

		// Token: 0x04000AAC RID: 2732
		private uint[] _alignPrices = new uint[16];

		// Token: 0x04000AAD RID: 2733
		private uint _alignPriceCount;

		// Token: 0x04000AAE RID: 2734
		private uint _distTableSize = 44U;

		// Token: 0x04000AAF RID: 2735
		private int _posStateBits = 2;

		// Token: 0x04000AB0 RID: 2736
		private uint _posStateMask = 3U;

		// Token: 0x04000AB1 RID: 2737
		private int _numLiteralPosStateBits;

		// Token: 0x04000AB2 RID: 2738
		private int _numLiteralContextBits = 3;

		// Token: 0x04000AB3 RID: 2739
		private uint _dictionarySize = 4194304U;

		// Token: 0x04000AB4 RID: 2740
		private uint _dictionarySizePrev = uint.MaxValue;

		// Token: 0x04000AB5 RID: 2741
		private uint _numFastBytesPrev = uint.MaxValue;

		// Token: 0x04000AB6 RID: 2742
		private long nowPos64;

		// Token: 0x04000AB7 RID: 2743
		private bool _finished;

		// Token: 0x04000AB8 RID: 2744
		private Stream _inStream;

		// Token: 0x04000AB9 RID: 2745
		private Encoder.EMatchFinderType _matchFinderType = Encoder.EMatchFinderType.BT4;

		// Token: 0x04000ABA RID: 2746
		private bool _writeEndMark;

		// Token: 0x04000ABB RID: 2747
		private bool _needReleaseMFStream;

		// Token: 0x04000ABC RID: 2748
		private uint[] reps = new uint[4];

		// Token: 0x04000ABD RID: 2749
		private uint[] repLens = new uint[4];

		// Token: 0x04000ABE RID: 2750
		private const int kPropSize = 5;

		// Token: 0x04000ABF RID: 2751
		private byte[] properties = new byte[5];

		// Token: 0x04000AC0 RID: 2752
		private uint[] tempPrices = new uint[128];

		// Token: 0x04000AC1 RID: 2753
		private uint _matchPriceCount;

		// Token: 0x04000AC2 RID: 2754
		private static string[] kMatchFinderIDs = new string[] { "BT2", "BT4" };

		// Token: 0x04000AC3 RID: 2755
		private uint _trainSize;

		// Token: 0x0200019A RID: 410
		private enum EMatchFinderType
		{
			// Token: 0x04000AC5 RID: 2757
			BT2,
			// Token: 0x04000AC6 RID: 2758
			BT4
		}

		// Token: 0x0200019B RID: 411
		private class LiteralEncoder
		{
			// Token: 0x060005FC RID: 1532 RVA: 0x0001DF50 File Offset: 0x0001C150
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
				this.m_Coders = new Encoder.LiteralEncoder.Encoder2[numStates];
				for (uint i = 0U; i < numStates; i += 1U)
				{
					this.m_Coders[(int)i].Create();
				}
			}

			// Token: 0x060005FD RID: 1533 RVA: 0x0001DFD0 File Offset: 0x0001C1D0
			public void Init()
			{
				uint numStates = 1U << this.m_NumPrevBits + this.m_NumPosBits;
				for (uint i = 0U; i < numStates; i += 1U)
				{
					this.m_Coders[(int)i].Init();
				}
			}

			// Token: 0x060005FE RID: 1534 RVA: 0x0001E00D File Offset: 0x0001C20D
			public Encoder.LiteralEncoder.Encoder2 GetSubCoder(uint pos, byte prevByte)
			{
				return this.m_Coders[(int)(((pos & this.m_PosMask) << this.m_NumPrevBits) + (uint)(prevByte >> 8 - this.m_NumPrevBits))];
			}

			// Token: 0x04000AC7 RID: 2759
			private Encoder.LiteralEncoder.Encoder2[] m_Coders;

			// Token: 0x04000AC8 RID: 2760
			private int m_NumPrevBits;

			// Token: 0x04000AC9 RID: 2761
			private int m_NumPosBits;

			// Token: 0x04000ACA RID: 2762
			private uint m_PosMask;

			// Token: 0x0200019C RID: 412
			public struct Encoder2
			{
				// Token: 0x06000600 RID: 1536 RVA: 0x0001E03A File Offset: 0x0001C23A
				public void Create()
				{
					this.m_Encoders = new BitEncoder[768];
				}

				// Token: 0x06000601 RID: 1537 RVA: 0x0001E04C File Offset: 0x0001C24C
				public void Init()
				{
					for (int i = 0; i < 768; i++)
					{
						this.m_Encoders[i].Init();
					}
				}

				// Token: 0x06000602 RID: 1538 RVA: 0x0001E07C File Offset: 0x0001C27C
				public void Encode(Encoder rangeEncoder, byte symbol)
				{
					uint context = 1U;
					for (int i = 7; i >= 0; i--)
					{
						uint bit = (uint)((symbol >> i) & 1);
						this.m_Encoders[(int)context].Encode(rangeEncoder, bit);
						context = (context << 1) | bit;
					}
				}

				// Token: 0x06000603 RID: 1539 RVA: 0x0001E0BC File Offset: 0x0001C2BC
				public void EncodeMatched(Encoder rangeEncoder, byte matchByte, byte symbol)
				{
					uint context = 1U;
					bool same = true;
					for (int i = 7; i >= 0; i--)
					{
						uint bit = (uint)((symbol >> i) & 1);
						uint state = context;
						if (same)
						{
							uint matchBit = (uint)((matchByte >> i) & 1);
							state += 1U + matchBit << 8;
							same = matchBit == bit;
						}
						this.m_Encoders[(int)state].Encode(rangeEncoder, bit);
						context = (context << 1) | bit;
					}
				}

				// Token: 0x06000604 RID: 1540 RVA: 0x0001E120 File Offset: 0x0001C320
				public uint GetPrice(bool matchMode, byte matchByte, byte symbol)
				{
					uint price = 0U;
					uint context = 1U;
					int i = 7;
					if (matchMode)
					{
						while (i >= 0)
						{
							uint matchBit = (uint)((matchByte >> i) & 1);
							uint bit = (uint)((symbol >> i) & 1);
							price += this.m_Encoders[(int)((1U + matchBit << 8) + context)].GetPrice(bit);
							context = (context << 1) | bit;
							if (matchBit != bit)
							{
								i--;
								break;
							}
							i--;
						}
					}
					while (i >= 0)
					{
						uint bit2 = (uint)((symbol >> i) & 1);
						price += this.m_Encoders[(int)context].GetPrice(bit2);
						context = (context << 1) | bit2;
						i--;
					}
					return price;
				}

				// Token: 0x04000ACB RID: 2763
				private BitEncoder[] m_Encoders;
			}
		}

		// Token: 0x0200019D RID: 413
		private class LenEncoder
		{
			// Token: 0x06000605 RID: 1541 RVA: 0x0001E1B4 File Offset: 0x0001C3B4
			public LenEncoder()
			{
				for (uint posState = 0U; posState < 16U; posState += 1U)
				{
					this._lowCoder[(int)posState] = new BitTreeEncoder(3);
					this._midCoder[(int)posState] = new BitTreeEncoder(3);
				}
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x0001E220 File Offset: 0x0001C420
			public void Init(uint numPosStates)
			{
				this._choice.Init();
				this._choice2.Init();
				for (uint posState = 0U; posState < numPosStates; posState += 1U)
				{
					this._lowCoder[(int)posState].Init();
					this._midCoder[(int)posState].Init();
				}
				this._highCoder.Init();
			}

			// Token: 0x06000607 RID: 1543 RVA: 0x0001E27C File Offset: 0x0001C47C
			public void Encode(Encoder rangeEncoder, uint symbol, uint posState)
			{
				if (symbol < 8U)
				{
					this._choice.Encode(rangeEncoder, 0U);
					this._lowCoder[(int)posState].Encode(rangeEncoder, symbol);
					return;
				}
				symbol -= 8U;
				this._choice.Encode(rangeEncoder, 1U);
				if (symbol < 8U)
				{
					this._choice2.Encode(rangeEncoder, 0U);
					this._midCoder[(int)posState].Encode(rangeEncoder, symbol);
					return;
				}
				this._choice2.Encode(rangeEncoder, 1U);
				this._highCoder.Encode(rangeEncoder, symbol - 8U);
			}

			// Token: 0x06000608 RID: 1544 RVA: 0x0001E304 File Offset: 0x0001C504
			public void SetPrices(uint posState, uint numSymbols, uint[] prices, uint st)
			{
				uint a0 = this._choice.GetPrice0();
				uint price = this._choice.GetPrice1();
				uint b0 = price + this._choice2.GetPrice0();
				uint b = price + this._choice2.GetPrice1();
				uint i;
				for (i = 0U; i < 8U; i += 1U)
				{
					if (i >= numSymbols)
					{
						return;
					}
					prices[(int)(st + i)] = a0 + this._lowCoder[(int)posState].GetPrice(i);
				}
				while (i < 16U)
				{
					if (i >= numSymbols)
					{
						return;
					}
					prices[(int)(st + i)] = b0 + this._midCoder[(int)posState].GetPrice(i - 8U);
					i += 1U;
				}
				while (i < numSymbols)
				{
					prices[(int)(st + i)] = b + this._highCoder.GetPrice(i - 8U - 8U);
					i += 1U;
				}
			}

			// Token: 0x04000ACC RID: 2764
			private BitEncoder _choice;

			// Token: 0x04000ACD RID: 2765
			private BitEncoder _choice2;

			// Token: 0x04000ACE RID: 2766
			private BitTreeEncoder[] _lowCoder = new BitTreeEncoder[16];

			// Token: 0x04000ACF RID: 2767
			private BitTreeEncoder[] _midCoder = new BitTreeEncoder[16];

			// Token: 0x04000AD0 RID: 2768
			private BitTreeEncoder _highCoder = new BitTreeEncoder(8);
		}

		// Token: 0x0200019E RID: 414
		private class LenPriceTableEncoder : Encoder.LenEncoder
		{
			// Token: 0x06000609 RID: 1545 RVA: 0x0001E3BE File Offset: 0x0001C5BE
			public void SetTableSize(uint tableSize)
			{
				this._tableSize = tableSize;
			}

			// Token: 0x0600060A RID: 1546 RVA: 0x0001E3C7 File Offset: 0x0001C5C7
			public uint GetPrice(uint symbol, uint posState)
			{
				return this._prices[(int)(posState * 272U + symbol)];
			}

			// Token: 0x0600060B RID: 1547 RVA: 0x0001E3D9 File Offset: 0x0001C5D9
			private void UpdateTable(uint posState)
			{
				base.SetPrices(posState, this._tableSize, this._prices, posState * 272U);
				this._counters[(int)posState] = this._tableSize;
			}

			// Token: 0x0600060C RID: 1548 RVA: 0x0001E404 File Offset: 0x0001C604
			public void UpdateTables(uint numPosStates)
			{
				for (uint posState = 0U; posState < numPosStates; posState += 1U)
				{
					this.UpdateTable(posState);
				}
			}

			// Token: 0x0600060D RID: 1549 RVA: 0x0001E424 File Offset: 0x0001C624
			public new void Encode(Encoder rangeEncoder, uint symbol, uint posState)
			{
				base.Encode(rangeEncoder, symbol, posState);
				uint[] counters = this._counters;
				uint num = counters[(int)posState] - 1U;
				counters[(int)posState] = num;
				if (num == 0U)
				{
					this.UpdateTable(posState);
				}
			}

			// Token: 0x04000AD1 RID: 2769
			private uint[] _prices = new uint[4352];

			// Token: 0x04000AD2 RID: 2770
			private uint _tableSize;

			// Token: 0x04000AD3 RID: 2771
			private uint[] _counters = new uint[16];
		}

		// Token: 0x0200019F RID: 415
		private class Optimal
		{
			// Token: 0x0600060F RID: 1551 RVA: 0x0001E47C File Offset: 0x0001C67C
			public void MakeAsChar()
			{
				this.BackPrev = uint.MaxValue;
				this.Prev1IsChar = false;
			}

			// Token: 0x06000610 RID: 1552 RVA: 0x0001E48C File Offset: 0x0001C68C
			public void MakeAsShortRep()
			{
				this.BackPrev = 0U;
				this.Prev1IsChar = false;
			}

			// Token: 0x06000611 RID: 1553 RVA: 0x0001E49C File Offset: 0x0001C69C
			public bool IsShortRep()
			{
				return this.BackPrev == 0U;
			}

			// Token: 0x04000AD4 RID: 2772
			public Base.State State;

			// Token: 0x04000AD5 RID: 2773
			public bool Prev1IsChar;

			// Token: 0x04000AD6 RID: 2774
			public bool Prev2;

			// Token: 0x04000AD7 RID: 2775
			public uint PosPrev2;

			// Token: 0x04000AD8 RID: 2776
			public uint BackPrev2;

			// Token: 0x04000AD9 RID: 2777
			public uint Price;

			// Token: 0x04000ADA RID: 2778
			public uint PosPrev;

			// Token: 0x04000ADB RID: 2779
			public uint BackPrev;

			// Token: 0x04000ADC RID: 2780
			public uint Backs0;

			// Token: 0x04000ADD RID: 2781
			public uint Backs1;

			// Token: 0x04000ADE RID: 2782
			public uint Backs2;

			// Token: 0x04000ADF RID: 2783
			public uint Backs3;
		}
	}
}
