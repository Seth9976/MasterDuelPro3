using System;
using System.Globalization;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Numerics
{
	// Token: 0x02000688 RID: 1672
	[Intrinsic]
	public struct Vector<T> : IEquatable<Vector<T>>, IFormattable where T : struct
	{
		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06003449 RID: 13385 RVA: 0x000C463A File Offset: 0x000C283A
		public static int Count
		{
			[Intrinsic]
			get
			{
				return Vector<T>.s_count;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x0600344A RID: 13386 RVA: 0x000C4641 File Offset: 0x000C2841
		public static Vector<T> Zero
		{
			[Intrinsic]
			get
			{
				return Vector<T>.s_zero;
			}
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x000C4648 File Offset: 0x000C2848
		private unsafe static int InitializeCount()
		{
			Vector<T>.VectorSizeHelper vectorSizeHelper;
			byte* ptr = &vectorSizeHelper._placeholder.register.byte_0;
			int num = (int)((long)(&vectorSizeHelper._byte - ptr));
			int num2;
			if (typeof(T) == typeof(byte))
			{
				num2 = 1;
			}
			else if (typeof(T) == typeof(sbyte))
			{
				num2 = 1;
			}
			else if (typeof(T) == typeof(ushort))
			{
				num2 = 2;
			}
			else if (typeof(T) == typeof(short))
			{
				num2 = 2;
			}
			else if (typeof(T) == typeof(uint))
			{
				num2 = 4;
			}
			else if (typeof(T) == typeof(int))
			{
				num2 = 4;
			}
			else if (typeof(T) == typeof(ulong))
			{
				num2 = 8;
			}
			else if (typeof(T) == typeof(long))
			{
				num2 = 8;
			}
			else if (typeof(T) == typeof(float))
			{
				num2 = 4;
			}
			else
			{
				if (!(typeof(T) == typeof(double)))
				{
					throw new NotSupportedException("Specified type is not supported");
				}
				num2 = 8;
			}
			return num / num2;
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x000C47D0 File Offset: 0x000C29D0
		[Intrinsic]
		public unsafe Vector(T value)
		{
			this = default(Vector<T>);
			if (Vector.IsHardwareAccelerated)
			{
				if (typeof(T) == typeof(byte))
				{
					fixed (byte* ptr = &this.register.byte_0)
					{
						byte* ptr2 = ptr;
						for (int i = 0; i < Vector<T>.Count; i++)
						{
							ptr2[i] = (byte)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(sbyte))
				{
					fixed (sbyte* ptr3 = &this.register.sbyte_0)
					{
						sbyte* ptr4 = ptr3;
						for (int j = 0; j < Vector<T>.Count; j++)
						{
							ptr4[j] = (sbyte)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(ushort))
				{
					fixed (ushort* ptr5 = &this.register.uint16_0)
					{
						ushort* ptr6 = ptr5;
						for (int k = 0; k < Vector<T>.Count; k++)
						{
							ptr6[k] = (ushort)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(short))
				{
					fixed (short* ptr7 = &this.register.int16_0)
					{
						short* ptr8 = ptr7;
						for (int l = 0; l < Vector<T>.Count; l++)
						{
							ptr8[l] = (short)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(uint))
				{
					fixed (uint* ptr9 = &this.register.uint32_0)
					{
						uint* ptr10 = ptr9;
						for (int m = 0; m < Vector<T>.Count; m++)
						{
							ptr10[m] = (uint)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(int))
				{
					fixed (int* ptr11 = &this.register.int32_0)
					{
						int* ptr12 = ptr11;
						for (int n = 0; n < Vector<T>.Count; n++)
						{
							ptr12[n] = (int)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(ulong))
				{
					fixed (ulong* ptr13 = &this.register.uint64_0)
					{
						ulong* ptr14 = ptr13;
						for (int num = 0; num < Vector<T>.Count; num++)
						{
							ptr14[num] = (ulong)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(long))
				{
					fixed (long* ptr15 = &this.register.int64_0)
					{
						long* ptr16 = ptr15;
						for (int num2 = 0; num2 < Vector<T>.Count; num2++)
						{
							ptr16[num2] = (long)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(float))
				{
					fixed (float* ptr17 = &this.register.single_0)
					{
						float* ptr18 = ptr17;
						for (int num3 = 0; num3 < Vector<T>.Count; num3++)
						{
							ptr18[num3] = (float)((object)value);
						}
					}
					return;
				}
				if (typeof(T) == typeof(double))
				{
					fixed (double* ptr19 = &this.register.double_0)
					{
						double* ptr20 = ptr19;
						for (int num4 = 0; num4 < Vector<T>.Count; num4++)
						{
							ptr20[num4] = (double)((object)value);
						}
					}
					return;
				}
			}
			else
			{
				if (typeof(T) == typeof(byte))
				{
					this.register.byte_0 = (byte)((object)value);
					this.register.byte_1 = (byte)((object)value);
					this.register.byte_2 = (byte)((object)value);
					this.register.byte_3 = (byte)((object)value);
					this.register.byte_4 = (byte)((object)value);
					this.register.byte_5 = (byte)((object)value);
					this.register.byte_6 = (byte)((object)value);
					this.register.byte_7 = (byte)((object)value);
					this.register.byte_8 = (byte)((object)value);
					this.register.byte_9 = (byte)((object)value);
					this.register.byte_10 = (byte)((object)value);
					this.register.byte_11 = (byte)((object)value);
					this.register.byte_12 = (byte)((object)value);
					this.register.byte_13 = (byte)((object)value);
					this.register.byte_14 = (byte)((object)value);
					this.register.byte_15 = (byte)((object)value);
					return;
				}
				if (typeof(T) == typeof(sbyte))
				{
					this.register.sbyte_0 = (sbyte)((object)value);
					this.register.sbyte_1 = (sbyte)((object)value);
					this.register.sbyte_2 = (sbyte)((object)value);
					this.register.sbyte_3 = (sbyte)((object)value);
					this.register.sbyte_4 = (sbyte)((object)value);
					this.register.sbyte_5 = (sbyte)((object)value);
					this.register.sbyte_6 = (sbyte)((object)value);
					this.register.sbyte_7 = (sbyte)((object)value);
					this.register.sbyte_8 = (sbyte)((object)value);
					this.register.sbyte_9 = (sbyte)((object)value);
					this.register.sbyte_10 = (sbyte)((object)value);
					this.register.sbyte_11 = (sbyte)((object)value);
					this.register.sbyte_12 = (sbyte)((object)value);
					this.register.sbyte_13 = (sbyte)((object)value);
					this.register.sbyte_14 = (sbyte)((object)value);
					this.register.sbyte_15 = (sbyte)((object)value);
					return;
				}
				if (typeof(T) == typeof(ushort))
				{
					this.register.uint16_0 = (ushort)((object)value);
					this.register.uint16_1 = (ushort)((object)value);
					this.register.uint16_2 = (ushort)((object)value);
					this.register.uint16_3 = (ushort)((object)value);
					this.register.uint16_4 = (ushort)((object)value);
					this.register.uint16_5 = (ushort)((object)value);
					this.register.uint16_6 = (ushort)((object)value);
					this.register.uint16_7 = (ushort)((object)value);
					return;
				}
				if (typeof(T) == typeof(short))
				{
					this.register.int16_0 = (short)((object)value);
					this.register.int16_1 = (short)((object)value);
					this.register.int16_2 = (short)((object)value);
					this.register.int16_3 = (short)((object)value);
					this.register.int16_4 = (short)((object)value);
					this.register.int16_5 = (short)((object)value);
					this.register.int16_6 = (short)((object)value);
					this.register.int16_7 = (short)((object)value);
					return;
				}
				if (typeof(T) == typeof(uint))
				{
					this.register.uint32_0 = (uint)((object)value);
					this.register.uint32_1 = (uint)((object)value);
					this.register.uint32_2 = (uint)((object)value);
					this.register.uint32_3 = (uint)((object)value);
					return;
				}
				if (typeof(T) == typeof(int))
				{
					this.register.int32_0 = (int)((object)value);
					this.register.int32_1 = (int)((object)value);
					this.register.int32_2 = (int)((object)value);
					this.register.int32_3 = (int)((object)value);
					return;
				}
				if (typeof(T) == typeof(ulong))
				{
					this.register.uint64_0 = (ulong)((object)value);
					this.register.uint64_1 = (ulong)((object)value);
					return;
				}
				if (typeof(T) == typeof(long))
				{
					this.register.int64_0 = (long)((object)value);
					this.register.int64_1 = (long)((object)value);
					return;
				}
				if (typeof(T) == typeof(float))
				{
					this.register.single_0 = (float)((object)value);
					this.register.single_1 = (float)((object)value);
					this.register.single_2 = (float)((object)value);
					this.register.single_3 = (float)((object)value);
					return;
				}
				if (typeof(T) == typeof(double))
				{
					this.register.double_0 = (double)((object)value);
					this.register.double_1 = (double)((object)value);
				}
			}
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x000C5232 File Offset: 0x000C3432
		internal unsafe Vector(void* dataPointer)
		{
			this = new Vector<T>(dataPointer, 0);
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x000C523C File Offset: 0x000C343C
		internal unsafe Vector(void* dataPointer, int offset)
		{
			this = default(Vector<T>);
			if (typeof(T) == typeof(byte))
			{
				byte* ptr = (byte*)dataPointer + offset;
				fixed (byte* ptr2 = &this.register.byte_0)
				{
					byte* ptr3 = ptr2;
					for (int i = 0; i < Vector<T>.Count; i++)
					{
						ptr3[i] = ptr[i];
					}
				}
				return;
			}
			if (typeof(T) == typeof(sbyte))
			{
				sbyte* ptr4 = (sbyte*)((byte*)dataPointer + offset);
				fixed (sbyte* ptr5 = &this.register.sbyte_0)
				{
					sbyte* ptr6 = ptr5;
					for (int j = 0; j < Vector<T>.Count; j++)
					{
						ptr6[j] = ptr4[j];
					}
				}
				return;
			}
			if (typeof(T) == typeof(ushort))
			{
				ushort* ptr7 = (ushort*)((byte*)dataPointer + (IntPtr)offset * 2);
				fixed (ushort* ptr8 = &this.register.uint16_0)
				{
					ushort* ptr9 = ptr8;
					for (int k = 0; k < Vector<T>.Count; k++)
					{
						ptr9[k] = ptr7[k];
					}
				}
				return;
			}
			if (typeof(T) == typeof(short))
			{
				short* ptr10 = (short*)((byte*)dataPointer + (IntPtr)offset * 2);
				fixed (short* ptr11 = &this.register.int16_0)
				{
					short* ptr12 = ptr11;
					for (int l = 0; l < Vector<T>.Count; l++)
					{
						ptr12[l] = ptr10[l];
					}
				}
				return;
			}
			if (typeof(T) == typeof(uint))
			{
				uint* ptr13 = (uint*)((byte*)dataPointer + (IntPtr)offset * 4);
				fixed (uint* ptr14 = &this.register.uint32_0)
				{
					uint* ptr15 = ptr14;
					for (int m = 0; m < Vector<T>.Count; m++)
					{
						ptr15[m] = ptr13[m];
					}
				}
				return;
			}
			if (typeof(T) == typeof(int))
			{
				int* ptr16 = (int*)((byte*)dataPointer + (IntPtr)offset * 4);
				fixed (int* ptr17 = &this.register.int32_0)
				{
					int* ptr18 = ptr17;
					for (int n = 0; n < Vector<T>.Count; n++)
					{
						ptr18[n] = ptr16[n];
					}
				}
				return;
			}
			if (typeof(T) == typeof(ulong))
			{
				ulong* ptr19 = (ulong*)((byte*)dataPointer + (IntPtr)offset * 8);
				fixed (ulong* ptr20 = &this.register.uint64_0)
				{
					ulong* ptr21 = ptr20;
					for (int num = 0; num < Vector<T>.Count; num++)
					{
						ptr21[num] = ptr19[num];
					}
				}
				return;
			}
			if (typeof(T) == typeof(long))
			{
				long* ptr22 = (long*)((byte*)dataPointer + (IntPtr)offset * 8);
				fixed (long* ptr23 = &this.register.int64_0)
				{
					long* ptr24 = ptr23;
					for (int num2 = 0; num2 < Vector<T>.Count; num2++)
					{
						ptr24[num2] = ptr22[num2];
					}
				}
				return;
			}
			if (typeof(T) == typeof(float))
			{
				float* ptr25 = (float*)((byte*)dataPointer + (IntPtr)offset * 4);
				fixed (float* ptr26 = &this.register.single_0)
				{
					float* ptr27 = ptr26;
					for (int num3 = 0; num3 < Vector<T>.Count; num3++)
					{
						ptr27[num3] = ptr25[num3];
					}
				}
				return;
			}
			if (typeof(T) == typeof(double))
			{
				double* ptr28 = (double*)((byte*)dataPointer + (IntPtr)offset * 8);
				fixed (double* ptr29 = &this.register.double_0)
				{
					double* ptr30 = ptr29;
					for (int num4 = 0; num4 < Vector<T>.Count; num4++)
					{
						ptr30[num4] = ptr28[num4];
					}
				}
				return;
			}
			throw new NotSupportedException("Specified type is not supported");
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x000C5621 File Offset: 0x000C3821
		private Vector(ref Register existingRegister)
		{
			this.register = existingRegister;
		}

		// Token: 0x170007A6 RID: 1958
		public unsafe T this[int index]
		{
			[Intrinsic]
			get
			{
				if (index >= Vector<T>.Count || index < 0)
				{
					throw new IndexOutOfRangeException(SR.Format("Specified argument was out of the range of valid values.", index));
				}
				if (typeof(T) == typeof(byte))
				{
					fixed (byte* ptr = &this.register.byte_0)
					{
						return (T)((object)ptr[index]);
					}
				}
				if (typeof(T) == typeof(sbyte))
				{
					fixed (sbyte* ptr2 = &this.register.sbyte_0)
					{
						return (T)((object)ptr2[index]);
					}
				}
				if (typeof(T) == typeof(ushort))
				{
					fixed (ushort* ptr3 = &this.register.uint16_0)
					{
						return (T)((object)ptr3[index]);
					}
				}
				if (typeof(T) == typeof(short))
				{
					fixed (short* ptr4 = &this.register.int16_0)
					{
						return (T)((object)ptr4[index]);
					}
				}
				if (typeof(T) == typeof(uint))
				{
					fixed (uint* ptr5 = &this.register.uint32_0)
					{
						return (T)((object)ptr5[index]);
					}
				}
				if (typeof(T) == typeof(int))
				{
					fixed (int* ptr6 = &this.register.int32_0)
					{
						return (T)((object)ptr6[index]);
					}
				}
				if (typeof(T) == typeof(ulong))
				{
					fixed (ulong* ptr7 = &this.register.uint64_0)
					{
						return (T)((object)ptr7[index]);
					}
				}
				if (typeof(T) == typeof(long))
				{
					fixed (long* ptr8 = &this.register.int64_0)
					{
						return (T)((object)ptr8[index]);
					}
				}
				if (typeof(T) == typeof(float))
				{
					fixed (float* ptr9 = &this.register.single_0)
					{
						return (T)((object)ptr9[index]);
					}
				}
				if (typeof(T) == typeof(double))
				{
					fixed (double* ptr10 = &this.register.double_0)
					{
						return (T)((object)ptr10[index]);
					}
				}
				throw new NotSupportedException("Specified type is not supported");
			}
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x000C58B3 File Offset: 0x000C3AB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			return obj is Vector<T> && this.Equals((Vector<T>)obj);
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000C58CC File Offset: 0x000C3ACC
		[Intrinsic]
		public bool Equals(Vector<T> other)
		{
			if (Vector.IsHardwareAccelerated)
			{
				for (int i = 0; i < Vector<T>.Count; i++)
				{
					if (!Vector<T>.ScalarEquals(this[i], other[i]))
					{
						return false;
					}
				}
				return true;
			}
			if (typeof(T) == typeof(byte))
			{
				return this.register.byte_0 == other.register.byte_0 && this.register.byte_1 == other.register.byte_1 && this.register.byte_2 == other.register.byte_2 && this.register.byte_3 == other.register.byte_3 && this.register.byte_4 == other.register.byte_4 && this.register.byte_5 == other.register.byte_5 && this.register.byte_6 == other.register.byte_6 && this.register.byte_7 == other.register.byte_7 && this.register.byte_8 == other.register.byte_8 && this.register.byte_9 == other.register.byte_9 && this.register.byte_10 == other.register.byte_10 && this.register.byte_11 == other.register.byte_11 && this.register.byte_12 == other.register.byte_12 && this.register.byte_13 == other.register.byte_13 && this.register.byte_14 == other.register.byte_14 && this.register.byte_15 == other.register.byte_15;
			}
			if (typeof(T) == typeof(sbyte))
			{
				return this.register.sbyte_0 == other.register.sbyte_0 && this.register.sbyte_1 == other.register.sbyte_1 && this.register.sbyte_2 == other.register.sbyte_2 && this.register.sbyte_3 == other.register.sbyte_3 && this.register.sbyte_4 == other.register.sbyte_4 && this.register.sbyte_5 == other.register.sbyte_5 && this.register.sbyte_6 == other.register.sbyte_6 && this.register.sbyte_7 == other.register.sbyte_7 && this.register.sbyte_8 == other.register.sbyte_8 && this.register.sbyte_9 == other.register.sbyte_9 && this.register.sbyte_10 == other.register.sbyte_10 && this.register.sbyte_11 == other.register.sbyte_11 && this.register.sbyte_12 == other.register.sbyte_12 && this.register.sbyte_13 == other.register.sbyte_13 && this.register.sbyte_14 == other.register.sbyte_14 && this.register.sbyte_15 == other.register.sbyte_15;
			}
			if (typeof(T) == typeof(ushort))
			{
				return this.register.uint16_0 == other.register.uint16_0 && this.register.uint16_1 == other.register.uint16_1 && this.register.uint16_2 == other.register.uint16_2 && this.register.uint16_3 == other.register.uint16_3 && this.register.uint16_4 == other.register.uint16_4 && this.register.uint16_5 == other.register.uint16_5 && this.register.uint16_6 == other.register.uint16_6 && this.register.uint16_7 == other.register.uint16_7;
			}
			if (typeof(T) == typeof(short))
			{
				return this.register.int16_0 == other.register.int16_0 && this.register.int16_1 == other.register.int16_1 && this.register.int16_2 == other.register.int16_2 && this.register.int16_3 == other.register.int16_3 && this.register.int16_4 == other.register.int16_4 && this.register.int16_5 == other.register.int16_5 && this.register.int16_6 == other.register.int16_6 && this.register.int16_7 == other.register.int16_7;
			}
			if (typeof(T) == typeof(uint))
			{
				return this.register.uint32_0 == other.register.uint32_0 && this.register.uint32_1 == other.register.uint32_1 && this.register.uint32_2 == other.register.uint32_2 && this.register.uint32_3 == other.register.uint32_3;
			}
			if (typeof(T) == typeof(int))
			{
				return this.register.int32_0 == other.register.int32_0 && this.register.int32_1 == other.register.int32_1 && this.register.int32_2 == other.register.int32_2 && this.register.int32_3 == other.register.int32_3;
			}
			if (typeof(T) == typeof(ulong))
			{
				return this.register.uint64_0 == other.register.uint64_0 && this.register.uint64_1 == other.register.uint64_1;
			}
			if (typeof(T) == typeof(long))
			{
				return this.register.int64_0 == other.register.int64_0 && this.register.int64_1 == other.register.int64_1;
			}
			if (typeof(T) == typeof(float))
			{
				return this.register.single_0 == other.register.single_0 && this.register.single_1 == other.register.single_1 && this.register.single_2 == other.register.single_2 && this.register.single_3 == other.register.single_3;
			}
			if (typeof(T) == typeof(double))
			{
				return this.register.double_0 == other.register.double_0 && this.register.double_1 == other.register.double_1;
			}
			throw new NotSupportedException("Specified type is not supported");
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000C60C4 File Offset: 0x000C42C4
		public override int GetHashCode()
		{
			int num = 0;
			if (Vector.IsHardwareAccelerated)
			{
				if (typeof(T) == typeof(byte))
				{
					for (int i = 0; i < Vector<T>.Count; i++)
					{
						num = HashHelpers.Combine(num, ((byte)((object)this[i])).GetHashCode());
					}
					return num;
				}
				if (typeof(T) == typeof(sbyte))
				{
					for (int j = 0; j < Vector<T>.Count; j++)
					{
						num = HashHelpers.Combine(num, ((sbyte)((object)this[j])).GetHashCode());
					}
					return num;
				}
				if (typeof(T) == typeof(ushort))
				{
					for (int k = 0; k < Vector<T>.Count; k++)
					{
						num = HashHelpers.Combine(num, ((ushort)((object)this[k])).GetHashCode());
					}
					return num;
				}
				if (typeof(T) == typeof(short))
				{
					for (int l = 0; l < Vector<T>.Count; l++)
					{
						num = HashHelpers.Combine(num, ((short)((object)this[l])).GetHashCode());
					}
					return num;
				}
				if (typeof(T) == typeof(uint))
				{
					for (int m = 0; m < Vector<T>.Count; m++)
					{
						num = HashHelpers.Combine(num, ((uint)((object)this[m])).GetHashCode());
					}
					return num;
				}
				if (typeof(T) == typeof(int))
				{
					for (int n = 0; n < Vector<T>.Count; n++)
					{
						num = HashHelpers.Combine(num, ((int)((object)this[n])).GetHashCode());
					}
					return num;
				}
				if (typeof(T) == typeof(ulong))
				{
					for (int num2 = 0; num2 < Vector<T>.Count; num2++)
					{
						num = HashHelpers.Combine(num, ((ulong)((object)this[num2])).GetHashCode());
					}
					return num;
				}
				if (typeof(T) == typeof(long))
				{
					for (int num3 = 0; num3 < Vector<T>.Count; num3++)
					{
						num = HashHelpers.Combine(num, ((long)((object)this[num3])).GetHashCode());
					}
					return num;
				}
				if (typeof(T) == typeof(float))
				{
					for (int num4 = 0; num4 < Vector<T>.Count; num4++)
					{
						num = HashHelpers.Combine(num, ((float)((object)this[num4])).GetHashCode());
					}
					return num;
				}
				if (typeof(T) == typeof(double))
				{
					for (int num5 = 0; num5 < Vector<T>.Count; num5++)
					{
						num = HashHelpers.Combine(num, ((double)((object)this[num5])).GetHashCode());
					}
					return num;
				}
				throw new NotSupportedException("Specified type is not supported");
			}
			else
			{
				if (typeof(T) == typeof(byte))
				{
					num = HashHelpers.Combine(num, this.register.byte_0.GetHashCode());
					num = HashHelpers.Combine(num, this.register.byte_1.GetHashCode());
					num = HashHelpers.Combine(num, this.register.byte_2.GetHashCode());
					num = HashHelpers.Combine(num, this.register.byte_3.GetHashCode());
					num = HashHelpers.Combine(num, this.register.byte_4.GetHashCode());
					num = HashHelpers.Combine(num, this.register.byte_5.GetHashCode());
					num = HashHelpers.Combine(num, this.register.byte_6.GetHashCode());
					num = HashHelpers.Combine(num, this.register.byte_7.GetHashCode());
					num = HashHelpers.Combine(num, this.register.byte_8.GetHashCode());
					num = HashHelpers.Combine(num, this.register.byte_9.GetHashCode());
					num = HashHelpers.Combine(num, this.register.byte_10.GetHashCode());
					num = HashHelpers.Combine(num, this.register.byte_11.GetHashCode());
					num = HashHelpers.Combine(num, this.register.byte_12.GetHashCode());
					num = HashHelpers.Combine(num, this.register.byte_13.GetHashCode());
					num = HashHelpers.Combine(num, this.register.byte_14.GetHashCode());
					return HashHelpers.Combine(num, this.register.byte_15.GetHashCode());
				}
				if (typeof(T) == typeof(sbyte))
				{
					num = HashHelpers.Combine(num, this.register.sbyte_0.GetHashCode());
					num = HashHelpers.Combine(num, this.register.sbyte_1.GetHashCode());
					num = HashHelpers.Combine(num, this.register.sbyte_2.GetHashCode());
					num = HashHelpers.Combine(num, this.register.sbyte_3.GetHashCode());
					num = HashHelpers.Combine(num, this.register.sbyte_4.GetHashCode());
					num = HashHelpers.Combine(num, this.register.sbyte_5.GetHashCode());
					num = HashHelpers.Combine(num, this.register.sbyte_6.GetHashCode());
					num = HashHelpers.Combine(num, this.register.sbyte_7.GetHashCode());
					num = HashHelpers.Combine(num, this.register.sbyte_8.GetHashCode());
					num = HashHelpers.Combine(num, this.register.sbyte_9.GetHashCode());
					num = HashHelpers.Combine(num, this.register.sbyte_10.GetHashCode());
					num = HashHelpers.Combine(num, this.register.sbyte_11.GetHashCode());
					num = HashHelpers.Combine(num, this.register.sbyte_12.GetHashCode());
					num = HashHelpers.Combine(num, this.register.sbyte_13.GetHashCode());
					num = HashHelpers.Combine(num, this.register.sbyte_14.GetHashCode());
					return HashHelpers.Combine(num, this.register.sbyte_15.GetHashCode());
				}
				if (typeof(T) == typeof(ushort))
				{
					num = HashHelpers.Combine(num, this.register.uint16_0.GetHashCode());
					num = HashHelpers.Combine(num, this.register.uint16_1.GetHashCode());
					num = HashHelpers.Combine(num, this.register.uint16_2.GetHashCode());
					num = HashHelpers.Combine(num, this.register.uint16_3.GetHashCode());
					num = HashHelpers.Combine(num, this.register.uint16_4.GetHashCode());
					num = HashHelpers.Combine(num, this.register.uint16_5.GetHashCode());
					num = HashHelpers.Combine(num, this.register.uint16_6.GetHashCode());
					return HashHelpers.Combine(num, this.register.uint16_7.GetHashCode());
				}
				if (typeof(T) == typeof(short))
				{
					num = HashHelpers.Combine(num, this.register.int16_0.GetHashCode());
					num = HashHelpers.Combine(num, this.register.int16_1.GetHashCode());
					num = HashHelpers.Combine(num, this.register.int16_2.GetHashCode());
					num = HashHelpers.Combine(num, this.register.int16_3.GetHashCode());
					num = HashHelpers.Combine(num, this.register.int16_4.GetHashCode());
					num = HashHelpers.Combine(num, this.register.int16_5.GetHashCode());
					num = HashHelpers.Combine(num, this.register.int16_6.GetHashCode());
					return HashHelpers.Combine(num, this.register.int16_7.GetHashCode());
				}
				if (typeof(T) == typeof(uint))
				{
					num = HashHelpers.Combine(num, this.register.uint32_0.GetHashCode());
					num = HashHelpers.Combine(num, this.register.uint32_1.GetHashCode());
					num = HashHelpers.Combine(num, this.register.uint32_2.GetHashCode());
					return HashHelpers.Combine(num, this.register.uint32_3.GetHashCode());
				}
				if (typeof(T) == typeof(int))
				{
					num = HashHelpers.Combine(num, this.register.int32_0.GetHashCode());
					num = HashHelpers.Combine(num, this.register.int32_1.GetHashCode());
					num = HashHelpers.Combine(num, this.register.int32_2.GetHashCode());
					return HashHelpers.Combine(num, this.register.int32_3.GetHashCode());
				}
				if (typeof(T) == typeof(ulong))
				{
					num = HashHelpers.Combine(num, this.register.uint64_0.GetHashCode());
					return HashHelpers.Combine(num, this.register.uint64_1.GetHashCode());
				}
				if (typeof(T) == typeof(long))
				{
					num = HashHelpers.Combine(num, this.register.int64_0.GetHashCode());
					return HashHelpers.Combine(num, this.register.int64_1.GetHashCode());
				}
				if (typeof(T) == typeof(float))
				{
					num = HashHelpers.Combine(num, this.register.single_0.GetHashCode());
					num = HashHelpers.Combine(num, this.register.single_1.GetHashCode());
					num = HashHelpers.Combine(num, this.register.single_2.GetHashCode());
					return HashHelpers.Combine(num, this.register.single_3.GetHashCode());
				}
				if (typeof(T) == typeof(double))
				{
					num = HashHelpers.Combine(num, this.register.double_0.GetHashCode());
					return HashHelpers.Combine(num, this.register.double_1.GetHashCode());
				}
				throw new NotSupportedException("Specified type is not supported");
			}
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000C6B41 File Offset: 0x000C4D41
		public override string ToString()
		{
			return this.ToString("G", CultureInfo.CurrentCulture);
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000C6B54 File Offset: 0x000C4D54
		public string ToString(string format, IFormatProvider formatProvider)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
			stringBuilder.Append('<');
			for (int i = 0; i < Vector<T>.Count - 1; i++)
			{
				stringBuilder.Append(((IFormattable)((object)this[i])).ToString(format, formatProvider));
				stringBuilder.Append(numberGroupSeparator);
				stringBuilder.Append(' ');
			}
			stringBuilder.Append(((IFormattable)((object)this[Vector<T>.Count - 1])).ToString(format, formatProvider));
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x000C6BF2 File Offset: 0x000C4DF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector<T> left, Vector<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x000C6BFC File Offset: 0x000C4DFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector<T> left, Vector<T> right)
		{
			return !(left == right);
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x000C6C08 File Offset: 0x000C4E08
		[CLSCompliant(false)]
		[Intrinsic]
		public static explicit operator Vector<ulong>(Vector<T> value)
		{
			return new Vector<ulong>(ref value.register);
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x000C6C18 File Offset: 0x000C4E18
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static Vector<T> Equals(Vector<T> left, Vector<T> right)
		{
			if (Vector.IsHardwareAccelerated)
			{
				if (typeof(T) == typeof(byte))
				{
					byte* ptr = stackalloc byte[(UIntPtr)Vector<T>.Count];
					for (int i = 0; i < Vector<T>.Count; i++)
					{
						ptr[i] = (Vector<T>.ScalarEquals(left[i], right[i]) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr);
				}
				if (typeof(T) == typeof(sbyte))
				{
					sbyte* ptr2 = stackalloc sbyte[(UIntPtr)Vector<T>.Count];
					for (int j = 0; j < Vector<T>.Count; j++)
					{
						ptr2[j] = (Vector<T>.ScalarEquals(left[j], right[j]) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr2);
				}
				if (typeof(T) == typeof(ushort))
				{
					ushort* ptr3;
					checked
					{
						ptr3 = stackalloc ushort[unchecked((UIntPtr)Vector<T>.Count) * 2];
					}
					for (int k = 0; k < Vector<T>.Count; k++)
					{
						ptr3[k] = (Vector<T>.ScalarEquals(left[k], right[k]) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr3);
				}
				if (typeof(T) == typeof(short))
				{
					short* ptr4;
					checked
					{
						ptr4 = stackalloc short[unchecked((UIntPtr)Vector<T>.Count) * 2];
					}
					for (int l = 0; l < Vector<T>.Count; l++)
					{
						ptr4[l] = (Vector<T>.ScalarEquals(left[l], right[l]) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr4);
				}
				if (typeof(T) == typeof(uint))
				{
					uint* ptr5;
					checked
					{
						ptr5 = stackalloc uint[unchecked((UIntPtr)Vector<T>.Count) * 4];
					}
					for (int m = 0; m < Vector<T>.Count; m++)
					{
						ptr5[m] = (Vector<T>.ScalarEquals(left[m], right[m]) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					}
					return new Vector<T>((void*)ptr5);
				}
				if (typeof(T) == typeof(int))
				{
					int* ptr6;
					checked
					{
						ptr6 = stackalloc int[unchecked((UIntPtr)Vector<T>.Count) * 4];
					}
					for (int n = 0; n < Vector<T>.Count; n++)
					{
						ptr6[n] = (Vector<T>.ScalarEquals(left[n], right[n]) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					}
					return new Vector<T>((void*)ptr6);
				}
				if (typeof(T) == typeof(ulong))
				{
					ulong* ptr7;
					checked
					{
						ptr7 = stackalloc ulong[unchecked((UIntPtr)Vector<T>.Count) * 8];
					}
					for (int num = 0; num < Vector<T>.Count; num++)
					{
						ptr7[num] = (Vector<T>.ScalarEquals(left[num], right[num]) ? ConstantHelper.GetUInt64WithAllBitsSet() : 0UL);
					}
					return new Vector<T>((void*)ptr7);
				}
				if (typeof(T) == typeof(long))
				{
					long* ptr8;
					checked
					{
						ptr8 = stackalloc long[unchecked((UIntPtr)Vector<T>.Count) * 8];
					}
					for (int num2 = 0; num2 < Vector<T>.Count; num2++)
					{
						ptr8[num2] = (Vector<T>.ScalarEquals(left[num2], right[num2]) ? ConstantHelper.GetInt64WithAllBitsSet() : 0L);
					}
					return new Vector<T>((void*)ptr8);
				}
				if (typeof(T) == typeof(float))
				{
					float* ptr9;
					checked
					{
						ptr9 = stackalloc float[unchecked((UIntPtr)Vector<T>.Count) * 4];
					}
					for (int num3 = 0; num3 < Vector<T>.Count; num3++)
					{
						ptr9[num3] = (Vector<T>.ScalarEquals(left[num3], right[num3]) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					}
					return new Vector<T>((void*)ptr9);
				}
				if (typeof(T) == typeof(double))
				{
					double* ptr10;
					checked
					{
						ptr10 = stackalloc double[unchecked((UIntPtr)Vector<T>.Count) * 8];
					}
					for (int num4 = 0; num4 < Vector<T>.Count; num4++)
					{
						ptr10[num4] = (Vector<T>.ScalarEquals(left[num4], right[num4]) ? ConstantHelper.GetDoubleWithAllBitsSet() : 0.0);
					}
					return new Vector<T>((void*)ptr10);
				}
				throw new NotSupportedException("Specified type is not supported");
			}
			else
			{
				Register register = default(Register);
				if (typeof(T) == typeof(byte))
				{
					register.byte_0 = ((left.register.byte_0 == right.register.byte_0) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_1 = ((left.register.byte_1 == right.register.byte_1) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_2 = ((left.register.byte_2 == right.register.byte_2) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_3 = ((left.register.byte_3 == right.register.byte_3) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_4 = ((left.register.byte_4 == right.register.byte_4) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_5 = ((left.register.byte_5 == right.register.byte_5) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_6 = ((left.register.byte_6 == right.register.byte_6) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_7 = ((left.register.byte_7 == right.register.byte_7) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_8 = ((left.register.byte_8 == right.register.byte_8) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_9 = ((left.register.byte_9 == right.register.byte_9) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_10 = ((left.register.byte_10 == right.register.byte_10) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_11 = ((left.register.byte_11 == right.register.byte_11) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_12 = ((left.register.byte_12 == right.register.byte_12) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_13 = ((left.register.byte_13 == right.register.byte_13) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_14 = ((left.register.byte_14 == right.register.byte_14) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					register.byte_15 = ((left.register.byte_15 == right.register.byte_15) ? ConstantHelper.GetByteWithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(sbyte))
				{
					register.sbyte_0 = ((left.register.sbyte_0 == right.register.sbyte_0) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_1 = ((left.register.sbyte_1 == right.register.sbyte_1) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_2 = ((left.register.sbyte_2 == right.register.sbyte_2) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_3 = ((left.register.sbyte_3 == right.register.sbyte_3) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_4 = ((left.register.sbyte_4 == right.register.sbyte_4) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_5 = ((left.register.sbyte_5 == right.register.sbyte_5) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_6 = ((left.register.sbyte_6 == right.register.sbyte_6) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_7 = ((left.register.sbyte_7 == right.register.sbyte_7) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_8 = ((left.register.sbyte_8 == right.register.sbyte_8) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_9 = ((left.register.sbyte_9 == right.register.sbyte_9) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_10 = ((left.register.sbyte_10 == right.register.sbyte_10) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_11 = ((left.register.sbyte_11 == right.register.sbyte_11) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_12 = ((left.register.sbyte_12 == right.register.sbyte_12) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_13 = ((left.register.sbyte_13 == right.register.sbyte_13) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_14 = ((left.register.sbyte_14 == right.register.sbyte_14) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					register.sbyte_15 = ((left.register.sbyte_15 == right.register.sbyte_15) ? ConstantHelper.GetSByteWithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(ushort))
				{
					register.uint16_0 = ((left.register.uint16_0 == right.register.uint16_0) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_1 = ((left.register.uint16_1 == right.register.uint16_1) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_2 = ((left.register.uint16_2 == right.register.uint16_2) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_3 = ((left.register.uint16_3 == right.register.uint16_3) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_4 = ((left.register.uint16_4 == right.register.uint16_4) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_5 = ((left.register.uint16_5 == right.register.uint16_5) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_6 = ((left.register.uint16_6 == right.register.uint16_6) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					register.uint16_7 = ((left.register.uint16_7 == right.register.uint16_7) ? ConstantHelper.GetUInt16WithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(short))
				{
					register.int16_0 = ((left.register.int16_0 == right.register.int16_0) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_1 = ((left.register.int16_1 == right.register.int16_1) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_2 = ((left.register.int16_2 == right.register.int16_2) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_3 = ((left.register.int16_3 == right.register.int16_3) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_4 = ((left.register.int16_4 == right.register.int16_4) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_5 = ((left.register.int16_5 == right.register.int16_5) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_6 = ((left.register.int16_6 == right.register.int16_6) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					register.int16_7 = ((left.register.int16_7 == right.register.int16_7) ? ConstantHelper.GetInt16WithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(uint))
				{
					register.uint32_0 = ((left.register.uint32_0 == right.register.uint32_0) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					register.uint32_1 = ((left.register.uint32_1 == right.register.uint32_1) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					register.uint32_2 = ((left.register.uint32_2 == right.register.uint32_2) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					register.uint32_3 = ((left.register.uint32_3 == right.register.uint32_3) ? ConstantHelper.GetUInt32WithAllBitsSet() : 0U);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(int))
				{
					register.int32_0 = ((left.register.int32_0 == right.register.int32_0) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					register.int32_1 = ((left.register.int32_1 == right.register.int32_1) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					register.int32_2 = ((left.register.int32_2 == right.register.int32_2) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					register.int32_3 = ((left.register.int32_3 == right.register.int32_3) ? ConstantHelper.GetInt32WithAllBitsSet() : 0);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(ulong))
				{
					register.uint64_0 = ((left.register.uint64_0 == right.register.uint64_0) ? ConstantHelper.GetUInt64WithAllBitsSet() : 0UL);
					register.uint64_1 = ((left.register.uint64_1 == right.register.uint64_1) ? ConstantHelper.GetUInt64WithAllBitsSet() : 0UL);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(long))
				{
					register.int64_0 = ((left.register.int64_0 == right.register.int64_0) ? ConstantHelper.GetInt64WithAllBitsSet() : 0L);
					register.int64_1 = ((left.register.int64_1 == right.register.int64_1) ? ConstantHelper.GetInt64WithAllBitsSet() : 0L);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(float))
				{
					register.single_0 = ((left.register.single_0 == right.register.single_0) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					register.single_1 = ((left.register.single_1 == right.register.single_1) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					register.single_2 = ((left.register.single_2 == right.register.single_2) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					register.single_3 = ((left.register.single_3 == right.register.single_3) ? ConstantHelper.GetSingleWithAllBitsSet() : 0f);
					return new Vector<T>(ref register);
				}
				if (typeof(T) == typeof(double))
				{
					register.double_0 = ((left.register.double_0 == right.register.double_0) ? ConstantHelper.GetDoubleWithAllBitsSet() : 0.0);
					register.double_1 = ((left.register.double_1 == right.register.double_1) ? ConstantHelper.GetDoubleWithAllBitsSet() : 0.0);
					return new Vector<T>(ref register);
				}
				throw new NotSupportedException("Specified type is not supported");
			}
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x000C7C24 File Offset: 0x000C5E24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool ScalarEquals(T left, T right)
		{
			if (typeof(T) == typeof(byte))
			{
				return (byte)((object)left) == (byte)((object)right);
			}
			if (typeof(T) == typeof(sbyte))
			{
				return (sbyte)((object)left) == (sbyte)((object)right);
			}
			if (typeof(T) == typeof(ushort))
			{
				return (ushort)((object)left) == (ushort)((object)right);
			}
			if (typeof(T) == typeof(short))
			{
				return (short)((object)left) == (short)((object)right);
			}
			if (typeof(T) == typeof(uint))
			{
				return (uint)((object)left) == (uint)((object)right);
			}
			if (typeof(T) == typeof(int))
			{
				return (int)((object)left) == (int)((object)right);
			}
			if (typeof(T) == typeof(ulong))
			{
				return (ulong)((object)left) == (ulong)((object)right);
			}
			if (typeof(T) == typeof(long))
			{
				return (long)((object)left) == (long)((object)right);
			}
			if (typeof(T) == typeof(float))
			{
				return (float)((object)left) == (float)((object)right);
			}
			if (typeof(T) == typeof(double))
			{
				return (double)((object)left) == (double)((object)right);
			}
			throw new NotSupportedException("Specified type is not supported");
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x000C7E44 File Offset: 0x000C6044
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static T GetOneValue()
		{
			if (typeof(T) == typeof(byte))
			{
				return (T)((object)1);
			}
			if (typeof(T) == typeof(sbyte))
			{
				return (T)((object)1);
			}
			if (typeof(T) == typeof(ushort))
			{
				return (T)((object)1);
			}
			if (typeof(T) == typeof(short))
			{
				return (T)((object)1);
			}
			if (typeof(T) == typeof(uint))
			{
				return (T)((object)1U);
			}
			if (typeof(T) == typeof(int))
			{
				return (T)((object)1);
			}
			if (typeof(T) == typeof(ulong))
			{
				return (T)((object)1UL);
			}
			if (typeof(T) == typeof(long))
			{
				return (T)((object)1L);
			}
			if (typeof(T) == typeof(float))
			{
				return (T)((object)1f);
			}
			if (typeof(T) == typeof(double))
			{
				return (T)((object)1.0);
			}
			throw new NotSupportedException("Specified type is not supported");
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x000C7FF0 File Offset: 0x000C61F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static T GetAllBitsSetValue()
		{
			if (typeof(T) == typeof(byte))
			{
				return (T)((object)ConstantHelper.GetByteWithAllBitsSet());
			}
			if (typeof(T) == typeof(sbyte))
			{
				return (T)((object)ConstantHelper.GetSByteWithAllBitsSet());
			}
			if (typeof(T) == typeof(ushort))
			{
				return (T)((object)ConstantHelper.GetUInt16WithAllBitsSet());
			}
			if (typeof(T) == typeof(short))
			{
				return (T)((object)ConstantHelper.GetInt16WithAllBitsSet());
			}
			if (typeof(T) == typeof(uint))
			{
				return (T)((object)ConstantHelper.GetUInt32WithAllBitsSet());
			}
			if (typeof(T) == typeof(int))
			{
				return (T)((object)ConstantHelper.GetInt32WithAllBitsSet());
			}
			if (typeof(T) == typeof(ulong))
			{
				return (T)((object)ConstantHelper.GetUInt64WithAllBitsSet());
			}
			if (typeof(T) == typeof(long))
			{
				return (T)((object)ConstantHelper.GetInt64WithAllBitsSet());
			}
			if (typeof(T) == typeof(float))
			{
				return (T)((object)ConstantHelper.GetSingleWithAllBitsSet());
			}
			if (typeof(T) == typeof(double))
			{
				return (T)((object)ConstantHelper.GetDoubleWithAllBitsSet());
			}
			throw new NotSupportedException("Specified type is not supported");
		}

		// Token: 0x04001B6F RID: 7023
		private Register register;

		// Token: 0x04001B70 RID: 7024
		private static readonly int s_count = Vector<T>.InitializeCount();

		// Token: 0x04001B71 RID: 7025
		private static readonly Vector<T> s_zero = default(Vector<T>);

		// Token: 0x04001B72 RID: 7026
		private static readonly Vector<T> s_one = new Vector<T>(Vector<T>.GetOneValue());

		// Token: 0x04001B73 RID: 7027
		private static readonly Vector<T> s_allOnes = new Vector<T>(Vector<T>.GetAllBitsSetValue());

		// Token: 0x02000689 RID: 1673
		private struct VectorSizeHelper
		{
			// Token: 0x04001B74 RID: 7028
			internal Vector<T> _placeholder;

			// Token: 0x04001B75 RID: 7029
			internal byte _byte;
		}
	}
}
