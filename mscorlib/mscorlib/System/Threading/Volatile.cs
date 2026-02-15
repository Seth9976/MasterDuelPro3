using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.Threading
{
	/// <summary>Contains methods for performing volatile memory operations.</summary>
	// Token: 0x0200028A RID: 650
	public static class Volatile
	{
		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method. </summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06001830 RID: 6192 RVA: 0x0005D322 File Offset: 0x0005B522
		[Intrinsic]
		public static bool Read(ref bool location)
		{
			return Unsafe.As<bool, Volatile.VolatileBoolean>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer. </param>
		// Token: 0x06001831 RID: 6193 RVA: 0x0005D331 File Offset: 0x0005B531
		[Intrinsic]
		public static void Write(ref bool location, bool value)
		{
			Unsafe.As<bool, Volatile.VolatileBoolean>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06001832 RID: 6194 RVA: 0x0005D341 File Offset: 0x0005B541
		[Intrinsic]
		public static byte Read(ref byte location)
		{
			return Unsafe.As<byte, Volatile.VolatileByte>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06001833 RID: 6195 RVA: 0x0005D350 File Offset: 0x0005B550
		[Intrinsic]
		public static void Write(ref byte location, byte value)
		{
			Unsafe.As<byte, Volatile.VolatileByte>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06001834 RID: 6196 RVA: 0x0005D360 File Offset: 0x0005B560
		[Intrinsic]
		public static short Read(ref short location)
		{
			return Unsafe.As<short, Volatile.VolatileInt16>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06001835 RID: 6197 RVA: 0x0005D36F File Offset: 0x0005B56F
		[Intrinsic]
		public static void Write(ref short location, short value)
		{
			Unsafe.As<short, Volatile.VolatileInt16>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06001836 RID: 6198 RVA: 0x0005D37F File Offset: 0x0005B57F
		[Intrinsic]
		public static int Read(ref int location)
		{
			return Unsafe.As<int, Volatile.VolatileInt32>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06001837 RID: 6199 RVA: 0x0005D38E File Offset: 0x0005B58E
		[Intrinsic]
		public static void Write(ref int location, int value)
		{
			Unsafe.As<int, Volatile.VolatileInt32>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06001838 RID: 6200 RVA: 0x0005D39E File Offset: 0x0005B59E
		[Intrinsic]
		public static IntPtr Read(ref IntPtr location)
		{
			return Unsafe.As<IntPtr, Volatile.VolatileIntPtr>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06001839 RID: 6201 RVA: 0x0005D3AD File Offset: 0x0005B5AD
		[Intrinsic]
		public static void Write(ref IntPtr location, IntPtr value)
		{
			Unsafe.As<IntPtr, Volatile.VolatileIntPtr>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x0600183A RID: 6202 RVA: 0x0005D3BD File Offset: 0x0005B5BD
		[Intrinsic]
		[CLSCompliant(false)]
		public static sbyte Read(ref sbyte location)
		{
			return Unsafe.As<sbyte, Volatile.VolatileSByte>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x0600183B RID: 6203 RVA: 0x0005D3CC File Offset: 0x0005B5CC
		[CLSCompliant(false)]
		[Intrinsic]
		public static void Write(ref sbyte location, sbyte value)
		{
			Unsafe.As<sbyte, Volatile.VolatileSByte>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x0600183C RID: 6204 RVA: 0x0005D3DC File Offset: 0x0005B5DC
		[Intrinsic]
		public static float Read(ref float location)
		{
			return Unsafe.As<float, Volatile.VolatileSingle>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x0600183D RID: 6205 RVA: 0x0005D3EB File Offset: 0x0005B5EB
		[Intrinsic]
		public static void Write(ref float location, float value)
		{
			Unsafe.As<float, Volatile.VolatileSingle>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x0600183E RID: 6206 RVA: 0x0005D3FB File Offset: 0x0005B5FB
		[CLSCompliant(false)]
		[Intrinsic]
		public static ushort Read(ref ushort location)
		{
			return Unsafe.As<ushort, Volatile.VolatileUInt16>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x0600183F RID: 6207 RVA: 0x0005D40A File Offset: 0x0005B60A
		[CLSCompliant(false)]
		[Intrinsic]
		public static void Write(ref ushort location, ushort value)
		{
			Unsafe.As<ushort, Volatile.VolatileUInt16>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06001840 RID: 6208 RVA: 0x0005D41A File Offset: 0x0005B61A
		[CLSCompliant(false)]
		[Intrinsic]
		public static uint Read(ref uint location)
		{
			return Unsafe.As<uint, Volatile.VolatileUInt32>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06001841 RID: 6209 RVA: 0x0005D429 File Offset: 0x0005B629
		[CLSCompliant(false)]
		[Intrinsic]
		public static void Write(ref uint location, uint value)
		{
			Unsafe.As<uint, Volatile.VolatileUInt32>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06001842 RID: 6210 RVA: 0x0005D439 File Offset: 0x0005B639
		[CLSCompliant(false)]
		[Intrinsic]
		public static UIntPtr Read(ref UIntPtr location)
		{
			return Unsafe.As<UIntPtr, Volatile.VolatileUIntPtr>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06001843 RID: 6211 RVA: 0x0005D448 File Offset: 0x0005B648
		[CLSCompliant(false)]
		[Intrinsic]
		public static void Write(ref UIntPtr location, UIntPtr value)
		{
			Unsafe.As<UIntPtr, Volatile.VolatileUIntPtr>(ref location).Value = value;
		}

		/// <summary>Reads the object reference from the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The reference to <paramref name="T" /> that was read. This reference is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		/// <typeparam name="T">The type of field to read. This must be a reference type, not a value type.</typeparam>
		// Token: 0x06001844 RID: 6212 RVA: 0x0005D458 File Offset: 0x0005B658
		[Intrinsic]
		public static T Read<T>(ref T location) where T : class
		{
			return Unsafe.As<T>(Unsafe.As<T, Volatile.VolatileObject>(ref location).Value);
		}

		/// <summary>Writes the specified object reference to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method. </summary>
		/// <param name="location">The field where the object reference is written.</param>
		/// <param name="value">The object reference to write. The reference is written immediately so that it is visible to all processors in the computer.</param>
		/// <typeparam name="T">The type of field to write. This must be a reference type, not a value type. </typeparam>
		// Token: 0x06001845 RID: 6213 RVA: 0x0005D46C File Offset: 0x0005B66C
		[Intrinsic]
		public static void Write<T>(ref T location, T value) where T : class
		{
			Unsafe.As<T, Volatile.VolatileObject>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06001846 RID: 6214
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Read(ref long location);

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06001847 RID: 6215
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong Read(ref ulong location);

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06001848 RID: 6216
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Read(ref double location);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a memory operation appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06001849 RID: 6217
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref long location, long value);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x0600184A RID: 6218
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref ulong location, ulong value);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x0600184B RID: 6219
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref double location, double value);

		// Token: 0x0200028B RID: 651
		private struct VolatileBoolean
		{
			// Token: 0x04000B86 RID: 2950
			public volatile bool Value;
		}

		// Token: 0x0200028C RID: 652
		private struct VolatileByte
		{
			// Token: 0x04000B87 RID: 2951
			public volatile byte Value;
		}

		// Token: 0x0200028D RID: 653
		private struct VolatileInt16
		{
			// Token: 0x04000B88 RID: 2952
			public volatile short Value;
		}

		// Token: 0x0200028E RID: 654
		private struct VolatileInt32
		{
			// Token: 0x04000B89 RID: 2953
			public volatile int Value;
		}

		// Token: 0x0200028F RID: 655
		private struct VolatileIntPtr
		{
			// Token: 0x04000B8A RID: 2954
			public volatile IntPtr Value;
		}

		// Token: 0x02000290 RID: 656
		private struct VolatileSByte
		{
			// Token: 0x04000B8B RID: 2955
			public volatile sbyte Value;
		}

		// Token: 0x02000291 RID: 657
		private struct VolatileSingle
		{
			// Token: 0x04000B8C RID: 2956
			public volatile float Value;
		}

		// Token: 0x02000292 RID: 658
		private struct VolatileUInt16
		{
			// Token: 0x04000B8D RID: 2957
			public volatile ushort Value;
		}

		// Token: 0x02000293 RID: 659
		private struct VolatileUInt32
		{
			// Token: 0x04000B8E RID: 2958
			public volatile uint Value;
		}

		// Token: 0x02000294 RID: 660
		private struct VolatileUIntPtr
		{
			// Token: 0x04000B8F RID: 2959
			public volatile UIntPtr Value;
		}

		// Token: 0x02000295 RID: 661
		private struct VolatileObject
		{
			// Token: 0x04000B90 RID: 2960
			public volatile object Value;
		}
	}
}
