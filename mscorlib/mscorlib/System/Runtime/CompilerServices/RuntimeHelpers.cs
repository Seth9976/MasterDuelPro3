using System;
using System.Runtime.ConstrainedExecution;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides a set of static methods and properties that provide support for compilers. This class cannot be inherited.</summary>
	// Token: 0x020005BF RID: 1471
	public static class RuntimeHelpers
	{
		// Token: 0x06002B95 RID: 11157
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeArray(Array array, IntPtr fldHandle);

		/// <summary>Provides a fast way to initialize an array from data that is stored in a module.</summary>
		/// <param name="array">The array to be initialized. </param>
		/// <param name="fldHandle">A field handle that specifies the location of the data used to initialize the array. </param>
		// Token: 0x06002B96 RID: 11158 RVA: 0x000AC693 File Offset: 0x000AA893
		public static void InitializeArray(Array array, RuntimeFieldHandle fldHandle)
		{
			if (array == null || fldHandle.Value == IntPtr.Zero)
			{
				throw new ArgumentNullException();
			}
			RuntimeHelpers.InitializeArray(array, fldHandle.Value);
		}

		/// <summary>Gets the offset, in bytes, to the data in the given string.</summary>
		/// <returns>The byte offset, from the start of the <see cref="T:System.String" /> object to the first character in the string.</returns>
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06002B97 RID: 11159
		public static extern int OffsetToStringData
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		/// <summary>Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the object identified by the <paramref name="o" /> parameter.</returns>
		/// <param name="o">An object to retrieve the hash code for. </param>
		// Token: 0x06002B98 RID: 11160 RVA: 0x0004AE1B File Offset: 0x0004901B
		public static int GetHashCode(object o)
		{
			return object.InternalGetHashCode(o);
		}

		// Token: 0x06002B99 RID: 11161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RunClassConstructor(IntPtr type);

		/// <summary>Runs a specified class constructor method.</summary>
		/// <param name="type">A type handle that specifies the class constructor method to run. </param>
		/// <exception cref="T:System.TypeInitializationException">The class initializer throws an exception. </exception>
		// Token: 0x06002B9A RID: 11162 RVA: 0x000AC6BE File Offset: 0x000AA8BE
		public static void RunClassConstructor(RuntimeTypeHandle type)
		{
			if (type.Value == IntPtr.Zero)
			{
				throw new ArgumentException("Handle is not initialized.", "type");
			}
			RuntimeHelpers.RunClassConstructor(type.Value);
		}

		// Token: 0x06002B9B RID: 11163
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SufficientExecutionStack();

		/// <summary>Ensures that the remaining stack space is large enough to execute the average .NET Framework function.</summary>
		/// <exception cref="T:System.InsufficientExecutionStackException">The available stack space is insufficient to execute the average .NET Framework function.</exception>
		// Token: 0x06002B9C RID: 11164 RVA: 0x000AC6EF File Offset: 0x000AA8EF
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void EnsureSufficientExecutionStack()
		{
			if (RuntimeHelpers.SufficientExecutionStack())
			{
				return;
			}
			throw new InsufficientExecutionStackException();
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000AC6FE File Offset: 0x000AA8FE
		public static bool TryEnsureSufficientExecutionStack()
		{
			return RuntimeHelpers.SufficientExecutionStack();
		}

		/// <summary>Designates a body of code as a constrained execution region (CER).</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B9E RID: 11166 RVA: 0x00002C89 File Offset: 0x00000E89
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void PrepareConstrainedRegions()
		{
		}

		/// <summary>Indicates that the specified delegate should be prepared for inclusion in a constrained execution region (CER).</summary>
		/// <param name="d">The delegate type to prepare.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002B9F RID: 11167 RVA: 0x00002C89 File Offset: 0x00000E89
		public static void PrepareDelegate(Delegate d)
		{
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000AC705 File Offset: 0x000AA905
		public static bool IsReferenceOrContainsReferences<T>()
		{
			return !typeof(T).IsValueType || RuntimeTypeHandle.HasReferences(typeof(T) as RuntimeType);
		}
	}
}
