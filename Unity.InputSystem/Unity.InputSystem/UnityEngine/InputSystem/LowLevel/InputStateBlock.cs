using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001D7 RID: 471
	public struct InputStateBlock
	{
		// Token: 0x0600116E RID: 4462 RVA: 0x00051D64 File Offset: 0x0004FF64
		public static int GetSizeOfPrimitiveFormatInBits(FourCC type)
		{
			if (type == InputStateBlock.FormatBit || type == InputStateBlock.FormatSBit)
			{
				return 1;
			}
			if (type == InputStateBlock.FormatInt || type == InputStateBlock.FormatUInt)
			{
				return 32;
			}
			if (type == InputStateBlock.FormatShort || type == InputStateBlock.FormatUShort)
			{
				return 16;
			}
			if (type == InputStateBlock.FormatByte || type == InputStateBlock.FormatSByte)
			{
				return 8;
			}
			if (type == InputStateBlock.FormatLong || type == InputStateBlock.FormatULong)
			{
				return 64;
			}
			if (type == InputStateBlock.FormatFloat)
			{
				return 32;
			}
			if (type == InputStateBlock.FormatDouble)
			{
				return 64;
			}
			if (type == InputStateBlock.FormatVector2)
			{
				return 64;
			}
			if (type == InputStateBlock.FormatVector3)
			{
				return 96;
			}
			if (type == InputStateBlock.FormatQuaternion)
			{
				return 128;
			}
			if (type == InputStateBlock.FormatVector2Short)
			{
				return 32;
			}
			if (type == InputStateBlock.FormatVector3Short)
			{
				return 48;
			}
			if (type == InputStateBlock.FormatVector2Byte)
			{
				return 16;
			}
			if (type == InputStateBlock.FormatVector3Byte)
			{
				return 24;
			}
			return -1;
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00051E94 File Offset: 0x00050094
		public static FourCC GetPrimitiveFormatFromType(Type type)
		{
			if (type == typeof(int))
			{
				return InputStateBlock.FormatInt;
			}
			if (type == typeof(uint))
			{
				return InputStateBlock.FormatUInt;
			}
			if (type == typeof(short))
			{
				return InputStateBlock.FormatShort;
			}
			if (type == typeof(ushort))
			{
				return InputStateBlock.FormatUShort;
			}
			if (type == typeof(byte))
			{
				return InputStateBlock.FormatByte;
			}
			if (type == typeof(sbyte))
			{
				return InputStateBlock.FormatSByte;
			}
			if (type == typeof(long))
			{
				return InputStateBlock.FormatLong;
			}
			if (type == typeof(ulong))
			{
				return InputStateBlock.FormatULong;
			}
			if (type == typeof(float))
			{
				return InputStateBlock.FormatFloat;
			}
			if (type == typeof(double))
			{
				return InputStateBlock.FormatDouble;
			}
			if (type == typeof(Vector2))
			{
				return InputStateBlock.FormatVector2;
			}
			if (type == typeof(Vector3))
			{
				return InputStateBlock.FormatVector3;
			}
			if (type == typeof(Quaternion))
			{
				return InputStateBlock.FormatQuaternion;
			}
			return default(FourCC);
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x00051FA1 File Offset: 0x000501A1
		// (set) Token: 0x06001171 RID: 4465 RVA: 0x00051FA9 File Offset: 0x000501A9
		public FourCC format { readonly get; set; }

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x00051FB2 File Offset: 0x000501B2
		// (set) Token: 0x06001173 RID: 4467 RVA: 0x00051FBA File Offset: 0x000501BA
		public uint byteOffset
		{
			get
			{
				return this.m_ByteOffset;
			}
			set
			{
				this.m_ByteOffset = value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x00051FC3 File Offset: 0x000501C3
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x00051FCB File Offset: 0x000501CB
		public uint bitOffset { readonly get; set; }

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x00051FD4 File Offset: 0x000501D4
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x00051FDC File Offset: 0x000501DC
		public uint sizeInBits { readonly get; set; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x00051FE5 File Offset: 0x000501E5
		internal uint alignedSizeInBytes
		{
			get
			{
				return this.sizeInBits + 7U >> 3;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x00051FF1 File Offset: 0x000501F1
		internal uint effectiveByteOffset
		{
			get
			{
				return this.byteOffset + (this.bitOffset >> 3);
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x00052002 File Offset: 0x00050202
		internal uint effectiveBitOffset
		{
			get
			{
				return this.byteOffset * 8U + this.bitOffset;
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00052014 File Offset: 0x00050214
		public unsafe int ReadInt(void* statePtr)
		{
			byte* valuePtr = (byte*)statePtr + this.byteOffset;
			int fmt = this.format;
			if (fmt <= 1396853076)
			{
				if (fmt <= 1113150533)
				{
					if (fmt != 1112101920)
					{
						if (fmt != 1113150533)
						{
							goto IL_00FA;
						}
						return (int)(*valuePtr);
					}
					else
					{
						if (this.sizeInBits != 1U)
						{
							return (int)MemoryHelpers.ReadMultipleBitsAsUInt((void*)valuePtr, this.bitOffset, this.sizeInBits);
						}
						if (!MemoryHelpers.ReadSingleBit((void*)valuePtr, this.bitOffset))
						{
							return 0;
						}
						return 1;
					}
				}
				else if (fmt != 1229870112)
				{
					if (fmt != 1396853076)
					{
						goto IL_00FA;
					}
					if (this.sizeInBits != 1U)
					{
						return MemoryHelpers.ReadExcessKMultipleBitsAsInt((void*)valuePtr, this.bitOffset, this.sizeInBits);
					}
					if (!MemoryHelpers.ReadSingleBit((void*)valuePtr, this.bitOffset))
					{
						return -1;
					}
					return 1;
				}
			}
			else if (fmt <= 1397248596)
			{
				if (fmt == 1396857172)
				{
					return (int)(*(sbyte*)valuePtr);
				}
				if (fmt != 1397248596)
				{
					goto IL_00FA;
				}
				return (int)(*(short*)valuePtr);
			}
			else if (fmt != 1430867540)
			{
				if (fmt != 1431521364)
				{
					goto IL_00FA;
				}
				return (int)(*(ushort*)valuePtr);
			}
			return *(int*)valuePtr;
			IL_00FA:
			throw new InvalidOperationException(string.Format("State format '{0}' is not supported as integer format", this.format));
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00052138 File Offset: 0x00050338
		public unsafe void WriteInt(void* statePtr, int value)
		{
			byte* valuePtr = (byte*)statePtr + this.byteOffset;
			int fmt = this.format;
			if (fmt <= 1396853076)
			{
				if (fmt <= 1113150533)
				{
					if (fmt != 1112101920)
					{
						if (fmt != 1113150533)
						{
							goto IL_00FB;
						}
						*valuePtr = (byte)value;
						return;
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							MemoryHelpers.WriteSingleBit((void*)valuePtr, this.bitOffset, value != 0);
							return;
						}
						MemoryHelpers.WriteUIntAsMultipleBits((void*)valuePtr, this.bitOffset, this.sizeInBits, (uint)value);
						return;
					}
				}
				else if (fmt != 1229870112)
				{
					if (fmt != 1396853076)
					{
						goto IL_00FB;
					}
					if (this.sizeInBits == 1U)
					{
						MemoryHelpers.WriteSingleBit((void*)valuePtr, this.bitOffset, value > 0);
						return;
					}
					MemoryHelpers.WriteIntAsExcessKMultipleBits((void*)valuePtr, this.bitOffset, this.sizeInBits, value);
					return;
				}
			}
			else if (fmt <= 1397248596)
			{
				if (fmt == 1396857172)
				{
					*valuePtr = (byte)((sbyte)value);
					return;
				}
				if (fmt != 1397248596)
				{
					goto IL_00FB;
				}
				*(short*)valuePtr = (short)value;
				return;
			}
			else if (fmt != 1430867540)
			{
				if (fmt != 1431521364)
				{
					goto IL_00FB;
				}
				*(short*)valuePtr = (short)((ushort)value);
				return;
			}
			*(int*)valuePtr = value;
			return;
			IL_00FB:
			throw new Exception(string.Format("State format '{0}' is not supported as integer format", this.format));
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0005225C File Offset: 0x0005045C
		public unsafe float ReadFloat(void* statePtr)
		{
			byte* valuePtr = (byte*)statePtr + this.byteOffset;
			int fmt = this.format;
			if (fmt <= 1229870112)
			{
				if (fmt <= 1113150533)
				{
					if (fmt != 1112101920)
					{
						if (fmt == 1113150533)
						{
							return NumberHelpers.UIntToNormalizedFloat((uint)(*valuePtr), 0U, 255U);
						}
					}
					else
					{
						if (this.sizeInBits != 1U)
						{
							return MemoryHelpers.ReadMultipleBitsAsNormalizedUInt((void*)valuePtr, this.bitOffset, this.sizeInBits);
						}
						if (!MemoryHelpers.ReadSingleBit((void*)valuePtr, this.bitOffset))
						{
							return 0f;
						}
						return 1f;
					}
				}
				else
				{
					if (fmt == 1145195552)
					{
						return (float)(*(double*)valuePtr);
					}
					if (fmt == 1179407392)
					{
						return *(float*)valuePtr;
					}
					if (fmt == 1229870112)
					{
						return NumberHelpers.IntToNormalizedFloat(*(int*)valuePtr, int.MinValue, int.MaxValue) * 2f - 1f;
					}
				}
			}
			else if (fmt <= 1396857172)
			{
				if (fmt != 1396853076)
				{
					if (fmt == 1396857172)
					{
						return NumberHelpers.IntToNormalizedFloat((int)(*(sbyte*)valuePtr), -128, 127) * 2f - 1f;
					}
				}
				else
				{
					if (this.sizeInBits != 1U)
					{
						return MemoryHelpers.ReadMultipleBitsAsNormalizedUInt((void*)valuePtr, this.bitOffset, this.sizeInBits) * 2f - 1f;
					}
					if (!MemoryHelpers.ReadSingleBit((void*)valuePtr, this.bitOffset))
					{
						return -1f;
					}
					return 1f;
				}
			}
			else
			{
				if (fmt == 1397248596)
				{
					return NumberHelpers.IntToNormalizedFloat((int)(*(short*)valuePtr), -32768, 32767) * 2f - 1f;
				}
				if (fmt == 1430867540)
				{
					return NumberHelpers.UIntToNormalizedFloat(*(uint*)valuePtr, 0U, uint.MaxValue);
				}
				if (fmt == 1431521364)
				{
					return NumberHelpers.UIntToNormalizedFloat((uint)(*(ushort*)valuePtr), 0U, 65535U);
				}
			}
			throw new InvalidOperationException(string.Format("State format '{0}' is not supported as floating-point format", this.format));
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00052428 File Offset: 0x00050628
		public unsafe void WriteFloat(void* statePtr, float value)
		{
			byte* valuePtr = (byte*)statePtr + this.byteOffset;
			int fmt = this.format;
			if (fmt <= 1229870112)
			{
				if (fmt <= 1113150533)
				{
					if (fmt != 1112101920)
					{
						if (fmt == 1113150533)
						{
							*valuePtr = (byte)NumberHelpers.NormalizedFloatToUInt(value, 0U, 255U);
							return;
						}
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							MemoryHelpers.WriteSingleBit((void*)valuePtr, this.bitOffset, value >= 0.5f);
							return;
						}
						MemoryHelpers.WriteNormalizedUIntAsMultipleBits((void*)valuePtr, this.bitOffset, this.sizeInBits, value);
						return;
					}
				}
				else
				{
					if (fmt == 1145195552)
					{
						*(double*)valuePtr = (double)value;
						return;
					}
					if (fmt == 1179407392)
					{
						*(float*)valuePtr = value;
						return;
					}
					if (fmt == 1229870112)
					{
						*(int*)valuePtr = NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, int.MinValue, int.MaxValue);
						return;
					}
				}
			}
			else if (fmt <= 1396857172)
			{
				if (fmt != 1396853076)
				{
					if (fmt == 1396857172)
					{
						*valuePtr = (byte)((sbyte)NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, -128, 127));
						return;
					}
				}
				else
				{
					if (this.sizeInBits == 1U)
					{
						MemoryHelpers.WriteSingleBit((void*)valuePtr, this.bitOffset, value >= 0f);
						return;
					}
					MemoryHelpers.WriteNormalizedUIntAsMultipleBits((void*)valuePtr, this.bitOffset, this.sizeInBits, value * 0.5f + 0.5f);
					return;
				}
			}
			else
			{
				if (fmt == 1397248596)
				{
					*(short*)valuePtr = (short)NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, -32768, 32767);
					return;
				}
				if (fmt == 1430867540)
				{
					*(int*)valuePtr = (int)NumberHelpers.NormalizedFloatToUInt(value, 0U, uint.MaxValue);
					return;
				}
				if (fmt == 1431521364)
				{
					*(short*)valuePtr = (short)((ushort)NumberHelpers.NormalizedFloatToUInt(value, 0U, 65535U));
					return;
				}
			}
			throw new Exception(string.Format("State format '{0}' is not supported as floating-point format", this.format));
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x000525FC File Offset: 0x000507FC
		internal PrimitiveValue FloatToPrimitiveValue(float value)
		{
			int fmt = this.format;
			if (fmt <= 1229870112)
			{
				if (fmt <= 1113150533)
				{
					if (fmt != 1112101920)
					{
						if (fmt == 1113150533)
						{
							return (byte)NumberHelpers.NormalizedFloatToUInt(value, 0U, 255U);
						}
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							return value >= 0.5f;
						}
						return (int)NumberHelpers.NormalizedFloatToUInt(value, 0U, (uint)((1L << (int)this.sizeInBits) - 1L));
					}
				}
				else
				{
					if (fmt == 1145195552)
					{
						return value;
					}
					if (fmt == 1179407392)
					{
						return value;
					}
					if (fmt == 1229870112)
					{
						return NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, int.MinValue, int.MaxValue);
					}
				}
			}
			else if (fmt <= 1396857172)
			{
				if (fmt != 1396853076)
				{
					if (fmt == 1396857172)
					{
						return (sbyte)NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, -128, 127);
					}
				}
				else
				{
					if (this.sizeInBits == 1U)
					{
						return value >= 0f;
					}
					int minValue = (int)(-(int)((int)1L << (int)(this.sizeInBits - 1U)));
					int maxValue = (int)((1L << (int)(this.sizeInBits - 1U)) - 1L);
					return NumberHelpers.NormalizedFloatToInt(value, minValue, maxValue);
				}
			}
			else
			{
				if (fmt == 1397248596)
				{
					return (short)NumberHelpers.NormalizedFloatToInt(value * 0.5f + 0.5f, -32768, 32767);
				}
				if (fmt == 1430867540)
				{
					return NumberHelpers.NormalizedFloatToUInt(value, 0U, uint.MaxValue);
				}
				if (fmt == 1431521364)
				{
					return (ushort)NumberHelpers.NormalizedFloatToUInt(value, 0U, 65535U);
				}
			}
			throw new Exception(string.Format("State format '{0}' is not supported as floating-point format", this.format));
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x000527EC File Offset: 0x000509EC
		public unsafe double ReadDouble(void* statePtr)
		{
			byte* valuePtr = (byte*)statePtr + this.byteOffset;
			int fmt = this.format;
			if (fmt <= 1229870112)
			{
				if (fmt <= 1113150533)
				{
					if (fmt != 1112101920)
					{
						if (fmt == 1113150533)
						{
							return (double)NumberHelpers.UIntToNormalizedFloat((uint)(*valuePtr), 0U, 255U);
						}
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							return (double)(MemoryHelpers.ReadSingleBit((void*)valuePtr, this.bitOffset) ? 1f : 0f);
						}
						return (double)MemoryHelpers.ReadMultipleBitsAsNormalizedUInt((void*)valuePtr, this.bitOffset, this.sizeInBits);
					}
				}
				else
				{
					if (fmt == 1145195552)
					{
						return *(double*)valuePtr;
					}
					if (fmt == 1179407392)
					{
						return (double)(*(float*)valuePtr);
					}
					if (fmt == 1229870112)
					{
						return (double)(NumberHelpers.IntToNormalizedFloat(*(int*)valuePtr, int.MinValue, int.MaxValue) * 2f - 1f);
					}
				}
			}
			else if (fmt <= 1396857172)
			{
				if (fmt != 1396853076)
				{
					if (fmt == 1396857172)
					{
						return (double)(NumberHelpers.IntToNormalizedFloat((int)(*(sbyte*)valuePtr), -128, 127) * 2f - 1f);
					}
				}
				else
				{
					if (this.sizeInBits == 1U)
					{
						return (double)(MemoryHelpers.ReadSingleBit((void*)valuePtr, this.bitOffset) ? 1f : (-1f));
					}
					return (double)(MemoryHelpers.ReadMultipleBitsAsNormalizedUInt((void*)valuePtr, this.bitOffset, this.sizeInBits) * 2f - 1f);
				}
			}
			else
			{
				if (fmt == 1397248596)
				{
					return (double)(NumberHelpers.IntToNormalizedFloat((int)(*(short*)valuePtr), -32768, 32767) * 2f - 1f);
				}
				if (fmt == 1430867540)
				{
					return (double)NumberHelpers.UIntToNormalizedFloat(*(uint*)valuePtr, 0U, uint.MaxValue);
				}
				if (fmt == 1431521364)
				{
					return (double)NumberHelpers.UIntToNormalizedFloat((uint)(*(ushort*)valuePtr), 0U, 65535U);
				}
			}
			throw new Exception(string.Format("State format '{0}' is not supported as floating-point format", this.format));
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x000529C4 File Offset: 0x00050BC4
		public unsafe void WriteDouble(void* statePtr, double value)
		{
			byte* valuePtr = (byte*)statePtr + this.byteOffset;
			int fmt = this.format;
			if (fmt <= 1229870112)
			{
				if (fmt <= 1113150533)
				{
					if (fmt != 1112101920)
					{
						if (fmt == 1113150533)
						{
							*valuePtr = (byte)NumberHelpers.NormalizedFloatToUInt((float)value, 0U, 255U);
							return;
						}
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							MemoryHelpers.WriteSingleBit((void*)valuePtr, this.bitOffset, value >= 0.5);
							return;
						}
						MemoryHelpers.WriteNormalizedUIntAsMultipleBits((void*)valuePtr, this.bitOffset, this.sizeInBits, (float)value);
						return;
					}
				}
				else
				{
					if (fmt == 1145195552)
					{
						*(double*)valuePtr = value;
						return;
					}
					if (fmt == 1179407392)
					{
						*(float*)valuePtr = (float)value;
						return;
					}
					if (fmt == 1229870112)
					{
						*(int*)valuePtr = NumberHelpers.NormalizedFloatToInt((float)value * 0.5f + 0.5f, int.MinValue, int.MaxValue);
						return;
					}
				}
			}
			else if (fmt <= 1396857172)
			{
				if (fmt != 1396853076)
				{
					if (fmt == 1396857172)
					{
						*valuePtr = (byte)((sbyte)NumberHelpers.NormalizedFloatToInt((float)value * 0.5f + 0.5f, -128, 127));
						return;
					}
				}
				else
				{
					if (this.sizeInBits == 1U)
					{
						MemoryHelpers.WriteSingleBit((void*)valuePtr, this.bitOffset, value >= 0.0);
						return;
					}
					MemoryHelpers.WriteNormalizedUIntAsMultipleBits((void*)valuePtr, this.bitOffset, this.sizeInBits, (float)value * 0.5f + 0.5f);
					return;
				}
			}
			else
			{
				if (fmt == 1397248596)
				{
					*(short*)valuePtr = (short)NumberHelpers.NormalizedFloatToInt((float)value * 0.5f + 0.5f, -32768, 32767);
					return;
				}
				if (fmt == 1430867540)
				{
					*(int*)valuePtr = (int)NumberHelpers.NormalizedFloatToUInt((float)value, 0U, uint.MaxValue);
					return;
				}
				if (fmt == 1431521364)
				{
					*(short*)valuePtr = (short)((ushort)NumberHelpers.NormalizedFloatToUInt((float)value, 0U, 65535U));
					return;
				}
			}
			throw new InvalidOperationException(string.Format("State format '{0}' is not supported as floating-point format", this.format));
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00052BA8 File Offset: 0x00050DA8
		public unsafe void Write(void* statePtr, PrimitiveValue value)
		{
			byte* valuePtr = (byte*)statePtr + this.byteOffset;
			int fmt = this.format;
			if (fmt <= 1229870112)
			{
				if (fmt <= 1113150533)
				{
					if (fmt != 1112101920)
					{
						if (fmt == 1113150533)
						{
							*valuePtr = value.ToByte(null);
							return;
						}
					}
					else
					{
						if (this.sizeInBits == 1U)
						{
							MemoryHelpers.WriteSingleBit((void*)valuePtr, this.bitOffset, value.ToBoolean(null));
							return;
						}
						MemoryHelpers.WriteUIntAsMultipleBits((void*)valuePtr, this.bitOffset, this.sizeInBits, value.ToUInt32(null));
						return;
					}
				}
				else
				{
					if (fmt == 1179407392)
					{
						*(float*)valuePtr = value.ToSingle(null);
						return;
					}
					if (fmt == 1229870112)
					{
						*(int*)valuePtr = value.ToInt32(null);
						return;
					}
				}
			}
			else if (fmt <= 1396857172)
			{
				if (fmt != 1396853076)
				{
					if (fmt == 1396857172)
					{
						*valuePtr = (byte)value.ToSByte(null);
						return;
					}
				}
				else
				{
					if (this.sizeInBits == 1U)
					{
						MemoryHelpers.WriteSingleBit((void*)valuePtr, this.bitOffset, value.ToBoolean(null));
						return;
					}
					MemoryHelpers.WriteIntAsExcessKMultipleBits((void*)valuePtr, this.bitOffset, this.sizeInBits, value.ToInt32(null));
					return;
				}
			}
			else
			{
				if (fmt == 1397248596)
				{
					*(short*)valuePtr = value.ToInt16(null);
					return;
				}
				if (fmt == 1430867540)
				{
					*(int*)valuePtr = (int)value.ToUInt32(null);
					return;
				}
				if (fmt == 1431521364)
				{
					*(short*)valuePtr = (short)value.ToUInt16(null);
					return;
				}
			}
			throw new NotImplementedException(string.Format("Writing primitive value of type '{0}' into state block with format '{1}'", value.type, this.format));
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00052D38 File Offset: 0x00050F38
		public unsafe void CopyToFrom(void* toStatePtr, void* fromStatePtr)
		{
			if (this.bitOffset != 0U || this.sizeInBits % 8U != 0U)
			{
				throw new NotImplementedException("Copying bitfields");
			}
			byte* from = (byte*)fromStatePtr + this.byteOffset;
			UnsafeUtility.MemCpy((void*)((byte*)toStatePtr + this.byteOffset), (void*)from, (long)((ulong)this.alignedSizeInBytes));
		}

		// Token: 0x04000A6E RID: 2670
		public const uint InvalidOffset = 4294967295U;

		// Token: 0x04000A6F RID: 2671
		public const uint AutomaticOffset = 4294967294U;

		// Token: 0x04000A70 RID: 2672
		public static readonly FourCC FormatInvalid = new FourCC(0);

		// Token: 0x04000A71 RID: 2673
		internal const int kFormatInvalid = 0;

		// Token: 0x04000A72 RID: 2674
		public static readonly FourCC FormatBit = new FourCC('B', 'I', 'T', ' ');

		// Token: 0x04000A73 RID: 2675
		internal const int kFormatBit = 1112101920;

		// Token: 0x04000A74 RID: 2676
		public static readonly FourCC FormatSBit = new FourCC('S', 'B', 'I', 'T');

		// Token: 0x04000A75 RID: 2677
		internal const int kFormatSBit = 1396853076;

		// Token: 0x04000A76 RID: 2678
		public static readonly FourCC FormatInt = new FourCC('I', 'N', 'T', ' ');

		// Token: 0x04000A77 RID: 2679
		internal const int kFormatInt = 1229870112;

		// Token: 0x04000A78 RID: 2680
		public static readonly FourCC FormatUInt = new FourCC('U', 'I', 'N', 'T');

		// Token: 0x04000A79 RID: 2681
		internal const int kFormatUInt = 1430867540;

		// Token: 0x04000A7A RID: 2682
		public static readonly FourCC FormatShort = new FourCC('S', 'H', 'R', 'T');

		// Token: 0x04000A7B RID: 2683
		internal const int kFormatShort = 1397248596;

		// Token: 0x04000A7C RID: 2684
		public static readonly FourCC FormatUShort = new FourCC('U', 'S', 'H', 'T');

		// Token: 0x04000A7D RID: 2685
		internal const int kFormatUShort = 1431521364;

		// Token: 0x04000A7E RID: 2686
		public static readonly FourCC FormatByte = new FourCC('B', 'Y', 'T', 'E');

		// Token: 0x04000A7F RID: 2687
		internal const int kFormatByte = 1113150533;

		// Token: 0x04000A80 RID: 2688
		public static readonly FourCC FormatSByte = new FourCC('S', 'B', 'Y', 'T');

		// Token: 0x04000A81 RID: 2689
		internal const int kFormatSByte = 1396857172;

		// Token: 0x04000A82 RID: 2690
		public static readonly FourCC FormatLong = new FourCC('L', 'N', 'G', ' ');

		// Token: 0x04000A83 RID: 2691
		internal const int kFormatLong = 1280198432;

		// Token: 0x04000A84 RID: 2692
		public static readonly FourCC FormatULong = new FourCC('U', 'L', 'N', 'G');

		// Token: 0x04000A85 RID: 2693
		internal const int kFormatULong = 1431064135;

		// Token: 0x04000A86 RID: 2694
		public static readonly FourCC FormatFloat = new FourCC('F', 'L', 'T', ' ');

		// Token: 0x04000A87 RID: 2695
		internal const int kFormatFloat = 1179407392;

		// Token: 0x04000A88 RID: 2696
		public static readonly FourCC FormatDouble = new FourCC('D', 'B', 'L', ' ');

		// Token: 0x04000A89 RID: 2697
		internal const int kFormatDouble = 1145195552;

		// Token: 0x04000A8A RID: 2698
		public static readonly FourCC FormatVector2 = new FourCC('V', 'E', 'C', '2');

		// Token: 0x04000A8B RID: 2699
		internal const int kFormatVector2 = 1447379762;

		// Token: 0x04000A8C RID: 2700
		public static readonly FourCC FormatVector3 = new FourCC('V', 'E', 'C', '3');

		// Token: 0x04000A8D RID: 2701
		internal const int kFormatVector3 = 1447379763;

		// Token: 0x04000A8E RID: 2702
		public static readonly FourCC FormatQuaternion = new FourCC('Q', 'U', 'A', 'T');

		// Token: 0x04000A8F RID: 2703
		internal const int kFormatQuaternion = 1364541780;

		// Token: 0x04000A90 RID: 2704
		public static readonly FourCC FormatVector2Short = new FourCC('V', 'C', '2', 'S');

		// Token: 0x04000A91 RID: 2705
		public static readonly FourCC FormatVector3Short = new FourCC('V', 'C', '3', 'S');

		// Token: 0x04000A92 RID: 2706
		public static readonly FourCC FormatVector2Byte = new FourCC('V', 'C', '2', 'B');

		// Token: 0x04000A93 RID: 2707
		public static readonly FourCC FormatVector3Byte = new FourCC('V', 'C', '3', 'B');

		// Token: 0x04000A94 RID: 2708
		public static readonly FourCC FormatPose = new FourCC('P', 'o', 's', 'e');

		// Token: 0x04000A95 RID: 2709
		internal const int kFormatPose = 1349481317;

		// Token: 0x04000A97 RID: 2711
		internal uint m_ByteOffset;
	}
}
