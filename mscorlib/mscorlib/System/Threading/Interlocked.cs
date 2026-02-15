using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Unity;

namespace System.Threading
{
	/// <summary>Provides atomic operations for variables that are shared by multiple threads. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200027D RID: 637
	public static class Interlocked
	{
		/// <summary>Compares two 32-bit signed integers for equality and, if they are equal, replaces one of the values.</summary>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <param name="location1">The destination, whose value is compared with <paramref name="comparand" /> and possibly replaced. </param>
		/// <param name="value">The value that replaces the destination value if the comparison results in equality. </param>
		/// <param name="comparand">The value that is compared to the value at <paramref name="location1" />. </param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001798 RID: 6040
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int CompareExchange(ref int location1, int value, int comparand);

		// Token: 0x06001799 RID: 6041
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int CompareExchange(ref int location1, int value, int comparand, ref bool succeeded);

		// Token: 0x0600179A RID: 6042
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CompareExchange(ref object location1, ref object value, ref object comparand, ref object result);

		/// <summary>Compares two objects for reference equality and, if they are equal, replaces one of the objects.</summary>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <param name="location1">The destination object that is compared with <paramref name="comparand" /> and possibly replaced. </param>
		/// <param name="value">The object that replaces the destination object if the comparison results in equality. </param>
		/// <param name="comparand">The object that is compared to the object at <paramref name="location1" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location1" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600179B RID: 6043 RVA: 0x0005B9DC File Offset: 0x00059BDC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static object CompareExchange(ref object location1, object value, object comparand)
		{
			object obj = null;
			Interlocked.CompareExchange(ref location1, ref value, ref comparand, ref obj);
			return obj;
		}

		/// <summary>Compares two single-precision floating point numbers for equality and, if they are equal, replaces one of the values.</summary>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <param name="location1">The destination, whose value is compared with <paramref name="comparand" /> and possibly replaced. </param>
		/// <param name="value">The value that replaces the destination value if the comparison results in equality. </param>
		/// <param name="comparand">The value that is compared to the value at <paramref name="location1" />. </param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600179C RID: 6044
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float CompareExchange(ref float location1, float value, float comparand);

		/// <summary>Decrements a specified variable and stores the result, as an atomic operation.</summary>
		/// <returns>The decremented value.</returns>
		/// <param name="location">The variable whose value is to be decremented. </param>
		/// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600179D RID: 6045
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Decrement(ref int location);

		/// <summary>Decrements the specified variable and stores the result, as an atomic operation.</summary>
		/// <returns>The decremented value.</returns>
		/// <param name="location">The variable whose value is to be decremented. </param>
		/// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600179E RID: 6046
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Decrement(ref long location);

		/// <summary>Increments a specified variable and stores the result, as an atomic operation.</summary>
		/// <returns>The incremented value.</returns>
		/// <param name="location">The variable whose value is to be incremented. </param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600179F RID: 6047
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Increment(ref int location);

		/// <summary>Increments a specified variable and stores the result, as an atomic operation.</summary>
		/// <returns>The incremented value.</returns>
		/// <param name="location">The variable whose value is to be incremented. </param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017A0 RID: 6048
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Increment(ref long location);

		/// <summary>Sets a 32-bit signed integer to a specified value and returns the original value, as an atomic operation.</summary>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <param name="location1">The variable to set to the specified value. </param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set. </param>
		/// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location1" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017A1 RID: 6049
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Exchange(ref int location1, int value);

		// Token: 0x060017A2 RID: 6050
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Exchange(ref object location1, ref object value, ref object result);

		/// <summary>Sets an object to a specified value and returns a reference to the original object, as an atomic operation.</summary>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <param name="location1">The variable to set to the specified value. </param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set. </param>
		/// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location1" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017A3 RID: 6051 RVA: 0x0005B9F8 File Offset: 0x00059BF8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static object Exchange(ref object location1, object value)
		{
			object obj = null;
			Interlocked.Exchange(ref location1, ref value, ref obj);
			return obj;
		}

		/// <summary>Sets a single-precision floating point number to a specified value and returns the original value, as an atomic operation.</summary>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <param name="location1">The variable to set to the specified value. </param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set. </param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017A4 RID: 6052
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Exchange(ref float location1, float value);

		/// <summary>Compares two 64-bit signed integers for equality and, if they are equal, replaces one of the values.</summary>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <param name="location1">The destination, whose value is compared with <paramref name="comparand" /> and possibly replaced. </param>
		/// <param name="value">The value that replaces the destination value if the comparison results in equality. </param>
		/// <param name="comparand">The value that is compared to the value at <paramref name="location1" />. </param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017A5 RID: 6053
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long CompareExchange(ref long location1, long value, long comparand);

		/// <summary>Compares two platform-specific handles or pointers for equality and, if they are equal, replaces one of them.</summary>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <param name="location1">The destination <see cref="T:System.IntPtr" />, whose value is compared with the value of <paramref name="comparand" /> and possibly replaced by <paramref name="value" />. </param>
		/// <param name="value">The <see cref="T:System.IntPtr" /> that replaces the destination value if the comparison results in equality. </param>
		/// <param name="comparand">The <see cref="T:System.IntPtr" /> that is compared to the value at <paramref name="location1" />. </param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017A6 RID: 6054
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CompareExchange(ref IntPtr location1, IntPtr value, IntPtr comparand);

		/// <summary>Compares two double-precision floating point numbers for equality and, if they are equal, replaces one of the values.</summary>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <param name="location1">The destination, whose value is compared with <paramref name="comparand" /> and possibly replaced. </param>
		/// <param name="value">The value that replaces the destination value if the comparison results in equality. </param>
		/// <param name="comparand">The value that is compared to the value at <paramref name="location1" />. </param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017A7 RID: 6055
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double CompareExchange(ref double location1, double value, double comparand);

		/// <summary>Compares two instances of the specified reference type <paramref name="T" /> for equality and, if they are equal, replaces one of them.</summary>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <param name="location1">The destination, whose value is compared with <paramref name="comparand" /> and possibly replaced. This is a reference parameter (ref in C#, ByRef in Visual Basic). </param>
		/// <param name="value">The value that replaces the destination value if the comparison results in equality. </param>
		/// <param name="comparand">The value that is compared to the value at <paramref name="location1" />. </param>
		/// <typeparam name="T">The type to be used for <paramref name="location1" />, <paramref name="value" />, and <paramref name="comparand" />. This type must be a reference type.</typeparam>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
		// Token: 0x060017A8 RID: 6056 RVA: 0x0005BA14 File Offset: 0x00059C14
		[ComVisible(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[Intrinsic]
		public static T CompareExchange<T>(ref T location1, T value, T comparand) where T : class
		{
			if (Unsafe.AsPointer<T>(ref location1) == null)
			{
				throw new NullReferenceException();
			}
			T t = default(T);
			Interlocked.CompareExchange(Unsafe.As<T, object>(ref location1), Unsafe.As<T, object>(ref value), Unsafe.As<T, object>(ref comparand), Unsafe.As<T, object>(ref t));
			return t;
		}

		/// <summary>Sets a 64-bit signed integer to a specified value and returns the original value, as an atomic operation.</summary>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <param name="location1">The variable to set to the specified value. </param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set. </param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017A9 RID: 6057
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Exchange(ref long location1, long value);

		/// <summary>Sets a platform-specific handle or pointer to a specified value and returns the original value, as an atomic operation.</summary>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <param name="location1">The variable to set to the specified value. </param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set. </param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017AA RID: 6058
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Exchange(ref IntPtr location1, IntPtr value);

		/// <summary>Sets a double-precision floating point number to a specified value and returns the original value, as an atomic operation.</summary>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <param name="location1">The variable to set to the specified value. </param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set. </param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017AB RID: 6059
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Exchange(ref double location1, double value);

		/// <summary>Sets a variable of the specified type <paramref name="T" /> to a specified value and returns the original value, as an atomic operation.</summary>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <param name="location1">The variable to set to the specified value. This is a reference parameter (ref in C#, ByRef in Visual Basic). </param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set. </param>
		/// <typeparam name="T">The type to be used for <paramref name="location1" /> and <paramref name="value" />. This type must be a reference type.</typeparam>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
		// Token: 0x060017AC RID: 6060 RVA: 0x0005BA5C File Offset: 0x00059C5C
		[ComVisible(false)]
		[Intrinsic]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static T Exchange<T>(ref T location1, T value) where T : class
		{
			if (Unsafe.AsPointer<T>(ref location1) == null)
			{
				throw new NullReferenceException();
			}
			T t = default(T);
			Interlocked.Exchange(Unsafe.As<T, object>(ref location1), Unsafe.As<T, object>(ref value), Unsafe.As<T, object>(ref t));
			return t;
		}

		/// <summary>Returns a 64-bit value, loaded as an atomic operation.</summary>
		/// <returns>The loaded value.</returns>
		/// <param name="location">The 64-bit value to be loaded.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017AD RID: 6061
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Read(ref long location);

		/// <summary>Adds two 32-bit integers and replaces the first integer with the sum, as an atomic operation.</summary>
		/// <returns>The new value stored at <paramref name="location1" />.</returns>
		/// <param name="location1">A variable containing the first value to be added. The sum of the two values is stored in <paramref name="location1" />.</param>
		/// <param name="value">The value to be added to the integer at <paramref name="location1" />.</param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017AE RID: 6062
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Add(ref int location1, int value);

		/// <summary>Adds two 64-bit integers and replaces the first integer with the sum, as an atomic operation.</summary>
		/// <returns>The new value stored at <paramref name="location1" />.</returns>
		/// <param name="location1">A variable containing the first value to be added. The sum of the two values is stored in <paramref name="location1" />.</param>
		/// <param name="value">The value to be added to the integer at <paramref name="location1" />.</param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017AF RID: 6063
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Add(ref long location1, long value);

		/// <summary>Synchronizes memory access as follows: The processor that executes the current thread cannot reorder instructions in such a way that memory accesses before the call to <see cref="M:System.Threading.Interlocked.MemoryBarrier" /> execute after memory accesses that follow the call to <see cref="M:System.Threading.Interlocked.MemoryBarrier" />.</summary>
		// Token: 0x060017B0 RID: 6064 RVA: 0x0005BA9B File Offset: 0x00059C9B
		public static void MemoryBarrier()
		{
			Thread.MemoryBarrier();
		}

		// Token: 0x060017B1 RID: 6065
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MemoryBarrierProcessWide();

		// Token: 0x060017B2 RID: 6066 RVA: 0x000176B9 File Offset: 0x000158B9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void SpeculationBarrier()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
