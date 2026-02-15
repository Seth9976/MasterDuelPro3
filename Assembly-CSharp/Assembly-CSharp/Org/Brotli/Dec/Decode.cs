using System;

namespace Org.Brotli.Dec
{
	// Token: 0x02000078 RID: 120
	internal sealed class Decode
	{
		// Token: 0x0600024B RID: 587 RVA: 0x00007684 File Offset: 0x00005884
		private static int DecodeVarLenUnsignedByte(BitReader br)
		{
			if (BitReader.ReadBits(br, 1) == 0)
			{
				return 0;
			}
			int i = BitReader.ReadBits(br, 3);
			if (i == 0)
			{
				return 1;
			}
			return BitReader.ReadBits(br, i) + (1 << i);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000076B8 File Offset: 0x000058B8
		private static void DecodeMetaBlockLength(BitReader br, State state)
		{
			state.inputEnd = BitReader.ReadBits(br, 1) == 1;
			state.metaBlockLength = 0;
			state.isUncompressed = false;
			state.isMetadata = false;
			if (state.inputEnd && BitReader.ReadBits(br, 1) != 0)
			{
				return;
			}
			int sizeNibbles = BitReader.ReadBits(br, 2) + 4;
			if (sizeNibbles == 7)
			{
				state.isMetadata = true;
				if (BitReader.ReadBits(br, 1) != 0)
				{
					throw new BrotliRuntimeException("Corrupted reserved bit");
				}
				int sizeBytes = BitReader.ReadBits(br, 2);
				if (sizeBytes == 0)
				{
					return;
				}
				for (int i = 0; i < sizeBytes; i++)
				{
					int bits = BitReader.ReadBits(br, 8);
					if (bits == 0 && i + 1 == sizeBytes && sizeBytes > 1)
					{
						throw new BrotliRuntimeException("Exuberant nibble");
					}
					state.metaBlockLength |= bits << i * 8;
				}
			}
			else
			{
				for (int j = 0; j < sizeNibbles; j++)
				{
					int bits2 = BitReader.ReadBits(br, 4);
					if (bits2 == 0 && j + 1 == sizeNibbles && sizeNibbles > 4)
					{
						throw new BrotliRuntimeException("Exuberant nibble");
					}
					state.metaBlockLength |= bits2 << j * 4;
				}
			}
			state.metaBlockLength++;
			if (!state.inputEnd)
			{
				state.isUncompressed = BitReader.ReadBits(br, 1) == 1;
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000077E4 File Offset: 0x000059E4
		private static int ReadSymbol(int[] table, int offset, BitReader br)
		{
			int val = (int)((ulong)br.accumulator >> br.bitOffset);
			offset += val & 255;
			int bits = table[offset] >> 16;
			int sym = table[offset] & 65535;
			if (bits <= 8)
			{
				br.bitOffset += bits;
				return sym;
			}
			offset += sym;
			int mask = (1 << bits) - 1;
			offset += (int)((uint)(val & mask) >> 8);
			br.bitOffset += (table[offset] >> 16) + 8;
			return table[offset] & 65535;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00007868 File Offset: 0x00005A68
		private static int ReadBlockLength(int[] table, int offset, BitReader br)
		{
			BitReader.FillBitWindow(br);
			int code = Decode.ReadSymbol(table, offset, br);
			int i = Prefix.BlockLengthNBits[code];
			return Prefix.BlockLengthOffset[code] + BitReader.ReadBits(br, i);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000789B File Offset: 0x00005A9B
		private static int TranslateShortCodes(int code, int[] ringBuffer, int index)
		{
			if (code < 16)
			{
				index += Decode.DistanceShortCodeIndexOffset[code];
				index &= 3;
				return ringBuffer[index] + Decode.DistanceShortCodeValueOffset[code];
			}
			return code - 16 + 1;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000078C4 File Offset: 0x00005AC4
		private static void MoveToFront(int[] v, int index)
		{
			int value = v[index];
			while (index > 0)
			{
				v[index] = v[index - 1];
				index--;
			}
			v[0] = value;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000078EC File Offset: 0x00005AEC
		private static void InverseMoveToFrontTransform(byte[] v, int vLen)
		{
			int[] mtf = new int[256];
			for (int i = 0; i < 256; i++)
			{
				mtf[i] = i;
			}
			for (int j = 0; j < vLen; j++)
			{
				int index = (int)(v[j] & byte.MaxValue);
				v[j] = (byte)mtf[index];
				if (index != 0)
				{
					Decode.MoveToFront(mtf, index);
				}
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00007940 File Offset: 0x00005B40
		private static void ReadHuffmanCodeLengths(int[] codeLengthCodeLengths, int numSymbols, int[] codeLengths, BitReader br)
		{
			int symbol = 0;
			int prevCodeLen = 8;
			int repeat = 0;
			int repeatCodeLen = 0;
			int space = 32768;
			int[] table = new int[32];
			Huffman.BuildHuffmanTable(table, 0, 5, codeLengthCodeLengths, 18);
			while (symbol < numSymbols && space > 0)
			{
				BitReader.ReadMoreInput(br);
				BitReader.FillBitWindow(br);
				int p = (int)((ulong)br.accumulator >> br.bitOffset) & 31;
				br.bitOffset += table[p] >> 16;
				int codeLen = table[p] & 65535;
				if (codeLen < 16)
				{
					repeat = 0;
					codeLengths[symbol++] = codeLen;
					if (codeLen != 0)
					{
						prevCodeLen = codeLen;
						space -= 32768 >> codeLen;
					}
				}
				else
				{
					int extraBits = codeLen - 14;
					int newLen = 0;
					if (codeLen == 16)
					{
						newLen = prevCodeLen;
					}
					if (repeatCodeLen != newLen)
					{
						repeat = 0;
						repeatCodeLen = newLen;
					}
					int oldRepeat = repeat;
					if (repeat > 0)
					{
						repeat -= 2;
						repeat <<= extraBits;
					}
					repeat += BitReader.ReadBits(br, extraBits) + 3;
					int repeatDelta = repeat - oldRepeat;
					if (symbol + repeatDelta > numSymbols)
					{
						throw new BrotliRuntimeException("symbol + repeatDelta > numSymbols");
					}
					for (int i = 0; i < repeatDelta; i++)
					{
						codeLengths[symbol++] = repeatCodeLen;
					}
					if (repeatCodeLen != 0)
					{
						space -= repeatDelta << 15 - repeatCodeLen;
					}
				}
			}
			if (space != 0)
			{
				throw new BrotliRuntimeException("Unused space");
			}
			Utils.FillWithZeroes(codeLengths, symbol, numSymbols - symbol);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007A90 File Offset: 0x00005C90
		internal static void ReadHuffmanCode(int alphabetSize, int[] table, int offset, BitReader br)
		{
			bool ok = true;
			BitReader.ReadMoreInput(br);
			int[] codeLengths = new int[alphabetSize];
			int simpleCodeOrSkip = BitReader.ReadBits(br, 2);
			if (simpleCodeOrSkip == 1)
			{
				int maxBitsCounter = alphabetSize - 1;
				int maxBits = 0;
				int[] symbols = new int[4];
				int numSymbols = BitReader.ReadBits(br, 2) + 1;
				while (maxBitsCounter != 0)
				{
					maxBitsCounter >>= 1;
					maxBits++;
				}
				for (int i = 0; i < numSymbols; i++)
				{
					symbols[i] = BitReader.ReadBits(br, maxBits) % alphabetSize;
					codeLengths[symbols[i]] = 2;
				}
				codeLengths[symbols[0]] = 1;
				switch (numSymbols)
				{
				case 1:
					goto IL_01EB;
				case 2:
					ok = symbols[0] != symbols[1];
					codeLengths[symbols[1]] = 1;
					goto IL_01EB;
				case 3:
					ok = symbols[0] != symbols[1] && symbols[0] != symbols[2] && symbols[1] != symbols[2];
					goto IL_01EB;
				}
				ok = symbols[0] != symbols[1] && symbols[0] != symbols[2] && symbols[0] != symbols[3] && symbols[1] != symbols[2] && symbols[1] != symbols[3] && symbols[2] != symbols[3];
				if (BitReader.ReadBits(br, 1) == 1)
				{
					codeLengths[symbols[2]] = 3;
					codeLengths[symbols[3]] = 3;
				}
				else
				{
					codeLengths[symbols[0]] = 2;
				}
			}
			else
			{
				int[] codeLengthCodeLengths = new int[18];
				int space = 32;
				int numCodes = 0;
				int j = simpleCodeOrSkip;
				while (j < 18 && space > 0)
				{
					int codeLenIdx = Decode.CodeLengthCodeOrder[j];
					BitReader.FillBitWindow(br);
					int p = (int)((ulong)br.accumulator >> br.bitOffset) & 15;
					br.bitOffset += Decode.FixedTable[p] >> 16;
					int v = Decode.FixedTable[p] & 65535;
					codeLengthCodeLengths[codeLenIdx] = v;
					if (v != 0)
					{
						space -= 32 >> v;
						numCodes++;
					}
					j++;
				}
				ok = numCodes == 1 || space == 0;
				Decode.ReadHuffmanCodeLengths(codeLengthCodeLengths, alphabetSize, codeLengths, br);
			}
			IL_01EB:
			if (!ok)
			{
				throw new BrotliRuntimeException("Can't readHuffmanCode");
			}
			Huffman.BuildHuffmanTable(table, offset, 8, codeLengths, alphabetSize);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00007CA0 File Offset: 0x00005EA0
		private static int DecodeContextMap(int contextMapSize, byte[] contextMap, BitReader br)
		{
			BitReader.ReadMoreInput(br);
			int numTrees = Decode.DecodeVarLenUnsignedByte(br) + 1;
			if (numTrees == 1)
			{
				Utils.FillWithZeroes(contextMap, 0, contextMapSize);
				return numTrees;
			}
			bool flag = BitReader.ReadBits(br, 1) == 1;
			int maxRunLengthPrefix = 0;
			if (flag)
			{
				maxRunLengthPrefix = BitReader.ReadBits(br, 4) + 1;
			}
			int[] table = new int[1080];
			Decode.ReadHuffmanCode(numTrees + maxRunLengthPrefix, table, 0, br);
			int i = 0;
			while (i < contextMapSize)
			{
				BitReader.ReadMoreInput(br);
				BitReader.FillBitWindow(br);
				int code = Decode.ReadSymbol(table, 0, br);
				if (code == 0)
				{
					contextMap[i] = 0;
					i++;
				}
				else if (code <= maxRunLengthPrefix)
				{
					for (int reps = (1 << code) + BitReader.ReadBits(br, code); reps != 0; reps--)
					{
						if (i >= contextMapSize)
						{
							throw new BrotliRuntimeException("Corrupted context map");
						}
						contextMap[i] = 0;
						i++;
					}
				}
				else
				{
					contextMap[i] = (byte)(code - maxRunLengthPrefix);
					i++;
				}
			}
			if (BitReader.ReadBits(br, 1) == 1)
			{
				Decode.InverseMoveToFrontTransform(contextMap, contextMapSize);
			}
			return numTrees;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00007D80 File Offset: 0x00005F80
		private static void DecodeBlockTypeAndLength(State state, int treeType)
		{
			BitReader br = state.br;
			int[] ringBuffers = state.blockTypeRb;
			int offset = treeType * 2;
			BitReader.FillBitWindow(br);
			int blockType = Decode.ReadSymbol(state.blockTypeTrees, treeType * 1080, br);
			state.blockLength[treeType] = Decode.ReadBlockLength(state.blockLenTrees, treeType * 1080, br);
			if (blockType == 1)
			{
				blockType = ringBuffers[offset + 1] + 1;
			}
			else if (blockType == 0)
			{
				blockType = ringBuffers[offset];
			}
			else
			{
				blockType -= 2;
			}
			if (blockType >= state.numBlockTypes[treeType])
			{
				blockType -= state.numBlockTypes[treeType];
			}
			ringBuffers[offset] = ringBuffers[offset + 1];
			ringBuffers[offset + 1] = blockType;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00007E14 File Offset: 0x00006014
		private static void DecodeLiteralBlockSwitch(State state)
		{
			Decode.DecodeBlockTypeAndLength(state, 0);
			int literalBlockType = state.blockTypeRb[1];
			state.contextMapSlice = literalBlockType << 6;
			state.literalTreeIndex = (int)(state.contextMap[state.contextMapSlice] & byte.MaxValue);
			state.literalTree = state.hGroup0.trees[state.literalTreeIndex];
			int contextMode = (int)state.contextModes[literalBlockType];
			state.contextLookupOffset1 = Context.LookupOffsets[contextMode];
			state.contextLookupOffset2 = Context.LookupOffsets[contextMode + 1];
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00007E90 File Offset: 0x00006090
		private static void DecodeCommandBlockSwitch(State state)
		{
			Decode.DecodeBlockTypeAndLength(state, 1);
			state.treeCommandOffset = state.hGroup1.trees[state.blockTypeRb[3]];
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00007EB3 File Offset: 0x000060B3
		private static void DecodeDistanceBlockSwitch(State state)
		{
			Decode.DecodeBlockTypeAndLength(state, 2);
			state.distContextMapSlice = state.blockTypeRb[5] << 2;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00007ECC File Offset: 0x000060CC
		private static void MaybeReallocateRingBuffer(State state)
		{
			int newSize = state.maxRingBufferSize;
			if ((long)newSize > state.expectedTotalSize)
			{
				int minimalNewSize = (int)state.expectedTotalSize + state.customDictionary.Length;
				while (newSize >> 1 > minimalNewSize)
				{
					newSize >>= 1;
				}
				if (!state.inputEnd && newSize < 16384 && state.maxRingBufferSize >= 16384)
				{
					newSize = 16384;
				}
			}
			if (newSize <= state.ringBufferSize)
			{
				return;
			}
			byte[] newBuffer = new byte[newSize + 37];
			if (state.ringBuffer != null)
			{
				Array.Copy(state.ringBuffer, 0, newBuffer, 0, state.ringBufferSize);
			}
			else if (state.customDictionary.Length != 0)
			{
				int length = state.customDictionary.Length;
				int offset = 0;
				if (length > state.maxBackwardDistance)
				{
					offset = length - state.maxBackwardDistance;
					length = state.maxBackwardDistance;
				}
				Array.Copy(state.customDictionary, offset, newBuffer, 0, length);
				state.pos = length;
				state.bytesToIgnore = length;
			}
			state.ringBuffer = newBuffer;
			state.ringBufferSize = newSize;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00007FB8 File Offset: 0x000061B8
		private static void ReadMetablockInfo(State state)
		{
			BitReader br = state.br;
			if (state.inputEnd)
			{
				state.nextRunningState = 10;
				state.bytesToWrite = state.pos;
				state.bytesWritten = 0;
				state.runningState = 12;
				return;
			}
			state.hGroup0.codes = null;
			state.hGroup0.trees = null;
			state.hGroup1.codes = null;
			state.hGroup1.trees = null;
			state.hGroup2.codes = null;
			state.hGroup2.trees = null;
			BitReader.ReadMoreInput(br);
			Decode.DecodeMetaBlockLength(br, state);
			if (state.metaBlockLength == 0 && !state.isMetadata)
			{
				return;
			}
			if (state.isUncompressed || state.isMetadata)
			{
				BitReader.JumpToByteBoundary(br);
				state.runningState = (state.isMetadata ? 4 : 5);
			}
			else
			{
				state.runningState = 2;
			}
			if (state.isMetadata)
			{
				return;
			}
			state.expectedTotalSize += (long)state.metaBlockLength;
			if (state.ringBufferSize < state.maxRingBufferSize)
			{
				Decode.MaybeReallocateRingBuffer(state);
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000080C0 File Offset: 0x000062C0
		private static void ReadMetablockHuffmanCodesAndContextMaps(State state)
		{
			BitReader br = state.br;
			for (int i = 0; i < 3; i++)
			{
				state.numBlockTypes[i] = Decode.DecodeVarLenUnsignedByte(br) + 1;
				state.blockLength[i] = 268435456;
				if (state.numBlockTypes[i] > 1)
				{
					Decode.ReadHuffmanCode(state.numBlockTypes[i] + 2, state.blockTypeTrees, i * 1080, br);
					Decode.ReadHuffmanCode(26, state.blockLenTrees, i * 1080, br);
					state.blockLength[i] = Decode.ReadBlockLength(state.blockLenTrees, i * 1080, br);
				}
			}
			BitReader.ReadMoreInput(br);
			state.distancePostfixBits = BitReader.ReadBits(br, 2);
			state.numDirectDistanceCodes = 16 + (BitReader.ReadBits(br, 4) << state.distancePostfixBits);
			state.distancePostfixMask = (1 << state.distancePostfixBits) - 1;
			int numDistanceCodes = state.numDirectDistanceCodes + (48 << state.distancePostfixBits);
			state.contextModes = new byte[state.numBlockTypes[0]];
			int j = 0;
			while (j < state.numBlockTypes[0])
			{
				int limit = Math.Min(j + 96, state.numBlockTypes[0]);
				while (j < limit)
				{
					state.contextModes[j] = (byte)(BitReader.ReadBits(br, 2) << 1);
					j++;
				}
				BitReader.ReadMoreInput(br);
			}
			state.contextMap = new byte[state.numBlockTypes[0] << 6];
			int numLiteralTrees = Decode.DecodeContextMap(state.numBlockTypes[0] << 6, state.contextMap, br);
			state.trivialLiteralContext = true;
			for (int k = 0; k < state.numBlockTypes[0] << 6; k++)
			{
				if ((int)state.contextMap[k] != k >> 6)
				{
					state.trivialLiteralContext = false;
					break;
				}
			}
			state.distContextMap = new byte[state.numBlockTypes[2] << 2];
			int numDistTrees = Decode.DecodeContextMap(state.numBlockTypes[2] << 2, state.distContextMap, br);
			HuffmanTreeGroup.Init(state.hGroup0, 256, numLiteralTrees);
			HuffmanTreeGroup.Init(state.hGroup1, 704, state.numBlockTypes[1]);
			HuffmanTreeGroup.Init(state.hGroup2, numDistanceCodes, numDistTrees);
			HuffmanTreeGroup.Decode(state.hGroup0, br);
			HuffmanTreeGroup.Decode(state.hGroup1, br);
			HuffmanTreeGroup.Decode(state.hGroup2, br);
			state.contextMapSlice = 0;
			state.distContextMapSlice = 0;
			state.contextLookupOffset1 = Context.LookupOffsets[(int)state.contextModes[0]];
			state.contextLookupOffset2 = Context.LookupOffsets[(int)(state.contextModes[0] + 1)];
			state.literalTreeIndex = 0;
			state.literalTree = state.hGroup0.trees[0];
			state.treeCommandOffset = state.hGroup1.trees[0];
			state.blockTypeRb[0] = (state.blockTypeRb[2] = (state.blockTypeRb[4] = 1));
			state.blockTypeRb[1] = (state.blockTypeRb[3] = (state.blockTypeRb[5] = 0));
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000083B0 File Offset: 0x000065B0
		private static void CopyUncompressedData(State state)
		{
			BitReader br = state.br;
			byte[] ringBuffer = state.ringBuffer;
			if (state.metaBlockLength <= 0)
			{
				BitReader.Reload(br);
				state.runningState = 1;
				return;
			}
			int chunkLength = Math.Min(state.ringBufferSize - state.pos, state.metaBlockLength);
			BitReader.CopyBytes(br, ringBuffer, state.pos, chunkLength);
			state.metaBlockLength -= chunkLength;
			state.pos += chunkLength;
			if (state.pos == state.ringBufferSize)
			{
				state.nextRunningState = 5;
				state.bytesToWrite = state.ringBufferSize;
				state.bytesWritten = 0;
				state.runningState = 12;
				return;
			}
			BitReader.Reload(br);
			state.runningState = 1;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00008464 File Offset: 0x00006664
		private static bool WriteRingBuffer(State state)
		{
			if (state.bytesToIgnore != 0)
			{
				state.bytesWritten += state.bytesToIgnore;
				state.bytesToIgnore = 0;
			}
			int toWrite = Math.Min(state.outputLength - state.outputUsed, state.bytesToWrite - state.bytesWritten);
			if (toWrite != 0)
			{
				Array.Copy(state.ringBuffer, state.bytesWritten, state.output, state.outputOffset + state.outputUsed, toWrite);
				state.outputUsed += toWrite;
				state.bytesWritten += toWrite;
			}
			return state.outputUsed < state.outputLength;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00008505 File Offset: 0x00006705
		internal static void SetCustomDictionary(State state, byte[] data)
		{
			state.customDictionary = ((data == null) ? new byte[0] : data);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000851C File Offset: 0x0000671C
		internal static void Decompress(State state)
		{
			if (state.runningState == 0)
			{
				throw new InvalidOperationException("Can't decompress until initialized");
			}
			if (state.runningState == 11)
			{
				throw new InvalidOperationException("Can't decompress after close");
			}
			BitReader br = state.br;
			int ringBufferMask = state.ringBufferSize - 1;
			byte[] ringBuffer = state.ringBuffer;
			while (state.runningState != 10)
			{
				switch (state.runningState)
				{
				case 1:
					if (state.metaBlockLength < 0)
					{
						throw new BrotliRuntimeException("Invalid metablock length");
					}
					Decode.ReadMetablockInfo(state);
					ringBufferMask = state.ringBufferSize - 1;
					ringBuffer = state.ringBuffer;
					continue;
				case 2:
					Decode.ReadMetablockHuffmanCodesAndContextMaps(state);
					state.runningState = 3;
					break;
				case 3:
					break;
				case 4:
					while (state.metaBlockLength > 0)
					{
						BitReader.ReadMoreInput(br);
						BitReader.ReadBits(br, 8);
						state.metaBlockLength--;
					}
					state.runningState = 1;
					continue;
				case 5:
					Decode.CopyUncompressedData(state);
					continue;
				case 6:
					goto IL_01A6;
				case 7:
					goto IL_05A2;
				case 8:
					Array.Copy(ringBuffer, state.ringBufferSize, ringBuffer, 0, state.copyDst - state.ringBufferSize);
					state.runningState = 3;
					continue;
				case 9:
				{
					if (state.copyLength < 4 || state.copyLength > 24)
					{
						throw new BrotliRuntimeException("Invalid backward reference");
					}
					int offset = Dictionary.OffsetsByLength[state.copyLength];
					int num = state.distance - state.maxDistance - 1;
					int shift = Dictionary.SizeBitsByLength[state.copyLength];
					int mask = (1 << shift) - 1;
					int wordIdx = num & mask;
					int transformIdx = (int)((uint)num >> shift);
					offset += wordIdx * state.copyLength;
					if (transformIdx >= Transform.Transforms.Length)
					{
						throw new BrotliRuntimeException("Invalid backward reference");
					}
					int len = Transform.TransformDictionaryWord(ringBuffer, state.copyDst, Dictionary.GetData(), offset, state.copyLength, Transform.Transforms[transformIdx]);
					state.copyDst += len;
					state.pos += len;
					state.metaBlockLength -= len;
					if (state.copyDst >= state.ringBufferSize)
					{
						state.nextRunningState = 8;
						state.bytesToWrite = state.ringBufferSize;
						state.bytesWritten = 0;
						state.runningState = 12;
						continue;
					}
					state.runningState = 3;
					continue;
				}
				case 10:
				case 11:
					goto IL_0884;
				case 12:
					if (!Decode.WriteRingBuffer(state))
					{
						return;
					}
					if (state.pos >= state.maxBackwardDistance)
					{
						state.maxDistance = state.maxBackwardDistance;
					}
					state.pos &= ringBufferMask;
					state.runningState = state.nextRunningState;
					continue;
				default:
					goto IL_0884;
				}
				if (state.metaBlockLength <= 0)
				{
					state.runningState = 1;
					continue;
				}
				BitReader.ReadMoreInput(br);
				if (state.blockLength[1] == 0)
				{
					Decode.DecodeCommandBlockSwitch(state);
				}
				state.blockLength[1]--;
				BitReader.FillBitWindow(br);
				int cmdCode = Decode.ReadSymbol(state.hGroup1.codes, state.treeCommandOffset, br);
				int rangeIdx = (int)((uint)cmdCode >> 6);
				state.distanceCode = 0;
				if (rangeIdx >= 2)
				{
					rangeIdx -= 2;
					state.distanceCode = -1;
				}
				int insertCode = Prefix.InsertRangeLut[rangeIdx] + (int)(((uint)cmdCode >> 3) & 7U);
				int copyCode = Prefix.CopyRangeLut[rangeIdx] + (cmdCode & 7);
				state.insertLength = Prefix.InsertLengthOffset[insertCode] + BitReader.ReadBits(br, Prefix.InsertLengthNBits[insertCode]);
				state.copyLength = Prefix.CopyLengthOffset[copyCode] + BitReader.ReadBits(br, Prefix.CopyLengthNBits[copyCode]);
				state.j = 0;
				state.runningState = 6;
				IL_01A6:
				if (state.trivialLiteralContext)
				{
					while (state.j < state.insertLength)
					{
						BitReader.ReadMoreInput(br);
						if (state.blockLength[0] == 0)
						{
							Decode.DecodeLiteralBlockSwitch(state);
						}
						state.blockLength[0]--;
						BitReader.FillBitWindow(br);
						ringBuffer[state.pos] = (byte)Decode.ReadSymbol(state.hGroup0.codes, state.literalTree, br);
						state.j++;
						int num2 = state.pos;
						state.pos = num2 + 1;
						if (num2 == ringBufferMask)
						{
							state.nextRunningState = 6;
							state.bytesToWrite = state.ringBufferSize;
							state.bytesWritten = 0;
							state.runningState = 12;
							break;
						}
					}
				}
				else
				{
					int prevByte = (int)(ringBuffer[(state.pos - 1) & ringBufferMask] & byte.MaxValue);
					int prevByte2 = (int)(ringBuffer[(state.pos - 2) & ringBufferMask] & byte.MaxValue);
					while (state.j < state.insertLength)
					{
						BitReader.ReadMoreInput(br);
						if (state.blockLength[0] == 0)
						{
							Decode.DecodeLiteralBlockSwitch(state);
						}
						int literalTreeIndex = (int)(state.contextMap[state.contextMapSlice + (Context.Lookup[state.contextLookupOffset1 + prevByte] | Context.Lookup[state.contextLookupOffset2 + prevByte2])] & byte.MaxValue);
						state.blockLength[0]--;
						prevByte2 = prevByte;
						BitReader.FillBitWindow(br);
						prevByte = Decode.ReadSymbol(state.hGroup0.codes, state.hGroup0.trees[literalTreeIndex], br);
						ringBuffer[state.pos] = (byte)prevByte;
						state.j++;
						int num2 = state.pos;
						state.pos = num2 + 1;
						if (num2 == ringBufferMask)
						{
							state.nextRunningState = 6;
							state.bytesToWrite = state.ringBufferSize;
							state.bytesWritten = 0;
							state.runningState = 12;
							break;
						}
					}
				}
				if (state.runningState != 6)
				{
					continue;
				}
				state.metaBlockLength -= state.insertLength;
				if (state.metaBlockLength <= 0)
				{
					state.runningState = 3;
					continue;
				}
				if (state.distanceCode < 0)
				{
					BitReader.ReadMoreInput(br);
					if (state.blockLength[2] == 0)
					{
						Decode.DecodeDistanceBlockSwitch(state);
					}
					state.blockLength[2]--;
					BitReader.FillBitWindow(br);
					state.distanceCode = Decode.ReadSymbol(state.hGroup2.codes, state.hGroup2.trees[(int)(state.distContextMap[state.distContextMapSlice + ((state.copyLength > 4) ? 3 : (state.copyLength - 2))] & byte.MaxValue)], br);
					if (state.distanceCode >= state.numDirectDistanceCodes)
					{
						state.distanceCode -= state.numDirectDistanceCodes;
						int postfix = state.distanceCode & state.distancePostfixMask;
						state.distanceCode = (int)((uint)state.distanceCode >> state.distancePostfixBits);
						int i = (int)(((uint)state.distanceCode >> 1) + 1U);
						int offset2 = (2 + (state.distanceCode & 1) << i) - 4;
						state.distanceCode = state.numDirectDistanceCodes + postfix + (offset2 + BitReader.ReadBits(br, i) << state.distancePostfixBits);
					}
				}
				state.distance = Decode.TranslateShortCodes(state.distanceCode, state.distRb, state.distRbIdx);
				if (state.distance < 0)
				{
					throw new BrotliRuntimeException("Negative distance");
				}
				if (state.maxDistance != state.maxBackwardDistance && state.pos < state.maxBackwardDistance)
				{
					state.maxDistance = state.pos;
				}
				else
				{
					state.maxDistance = state.maxBackwardDistance;
				}
				state.copyDst = state.pos;
				if (state.distance > state.maxDistance)
				{
					state.runningState = 9;
					continue;
				}
				if (state.distanceCode > 0)
				{
					state.distRb[state.distRbIdx & 3] = state.distance;
					state.distRbIdx++;
				}
				if (state.copyLength > state.metaBlockLength)
				{
					throw new BrotliRuntimeException("Invalid backward reference");
				}
				state.j = 0;
				state.runningState = 7;
				IL_05A2:
				int src = (state.pos - state.distance) & ringBufferMask;
				int dst = state.pos;
				int copyLength = state.copyLength - state.j;
				if (src + copyLength < ringBufferMask && dst + copyLength < ringBufferMask)
				{
					for (int j = 0; j < copyLength; j++)
					{
						ringBuffer[dst++] = ringBuffer[src++];
					}
					state.j += copyLength;
					state.metaBlockLength -= copyLength;
					state.pos += copyLength;
				}
				else
				{
					while (state.j < state.copyLength)
					{
						ringBuffer[state.pos] = ringBuffer[(state.pos - state.distance) & ringBufferMask];
						state.metaBlockLength--;
						state.j++;
						int num2 = state.pos;
						state.pos = num2 + 1;
						if (num2 == ringBufferMask)
						{
							state.nextRunningState = 7;
							state.bytesToWrite = state.ringBufferSize;
							state.bytesWritten = 0;
							state.runningState = 12;
							break;
						}
					}
				}
				if (state.runningState == 7)
				{
					state.runningState = 3;
					continue;
				}
				continue;
				IL_0884:
				throw new BrotliRuntimeException("Unexpected state " + state.runningState.ToString());
			}
			if (state.runningState == 10)
			{
				if (state.metaBlockLength < 0)
				{
					throw new BrotliRuntimeException("Invalid metablock length");
				}
				BitReader.JumpToByteBoundary(br);
				BitReader.CheckHealth(state.br, true);
			}
		}

		// Token: 0x040002D5 RID: 725
		private const int DefaultCodeLength = 8;

		// Token: 0x040002D6 RID: 726
		private const int CodeLengthRepeatCode = 16;

		// Token: 0x040002D7 RID: 727
		private const int NumLiteralCodes = 256;

		// Token: 0x040002D8 RID: 728
		private const int NumInsertAndCopyCodes = 704;

		// Token: 0x040002D9 RID: 729
		private const int NumBlockLengthCodes = 26;

		// Token: 0x040002DA RID: 730
		private const int LiteralContextBits = 6;

		// Token: 0x040002DB RID: 731
		private const int DistanceContextBits = 2;

		// Token: 0x040002DC RID: 732
		private const int HuffmanTableBits = 8;

		// Token: 0x040002DD RID: 733
		private const int HuffmanTableMask = 255;

		// Token: 0x040002DE RID: 734
		private const int CodeLengthCodes = 18;

		// Token: 0x040002DF RID: 735
		private static readonly int[] CodeLengthCodeOrder = new int[]
		{
			1, 2, 3, 4, 0, 5, 17, 6, 16, 7,
			8, 9, 10, 11, 12, 13, 14, 15
		};

		// Token: 0x040002E0 RID: 736
		private const int NumDistanceShortCodes = 16;

		// Token: 0x040002E1 RID: 737
		private static readonly int[] DistanceShortCodeIndexOffset = new int[]
		{
			3, 2, 1, 0, 3, 3, 3, 3, 3, 3,
			2, 2, 2, 2, 2, 2
		};

		// Token: 0x040002E2 RID: 738
		private static readonly int[] DistanceShortCodeValueOffset = new int[]
		{
			0, 0, 0, 0, -1, 1, -2, 2, -3, 3,
			-1, 1, -2, 2, -3, 3
		};

		// Token: 0x040002E3 RID: 739
		private static readonly int[] FixedTable = new int[]
		{
			131072, 131076, 131075, 196610, 131072, 131076, 131075, 262145, 131072, 131076,
			131075, 196610, 131072, 131076, 131075, 262149
		};
	}
}
