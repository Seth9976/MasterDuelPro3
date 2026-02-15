using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System
{
	/// <summary>Controls the system garbage collector, a service that automatically reclaims unused memory.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A5 RID: 421
	public static class GC
	{
		// Token: 0x06000F83 RID: 3971
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCollectionCount(int generation);

		// Token: 0x06000F84 RID: 3972
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxGeneration();

		// Token: 0x06000F85 RID: 3973
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalCollect(int generation);

		// Token: 0x06000F86 RID: 3974
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RecordPressure(long bytesAllocated);

		// Token: 0x06000F87 RID: 3975
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void register_ephemeron_array(Ephemeron[] array);

		// Token: 0x06000F88 RID: 3976
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object get_ephemeron_tombstone();

		// Token: 0x06000F89 RID: 3977 RVA: 0x00041616 File Offset: 0x0003F816
		internal static void GetMemoryInfo(out uint highMemLoadThreshold, out ulong totalPhysicalMem, out uint lastRecordedMemLoad, out UIntPtr lastRecordedHeapSize, out UIntPtr lastRecordedFragmentation)
		{
			highMemLoadThreshold = 0U;
			totalPhysicalMem = ulong.MaxValue;
			lastRecordedMemLoad = 0U;
			lastRecordedHeapSize = UIntPtr.Zero;
			lastRecordedFragmentation = UIntPtr.Zero;
		}

		/// <summary>Informs the runtime of a large allocation of unmanaged memory that should be taken into account when scheduling garbage collection.</summary>
		/// <param name="bytesAllocated">The incremental amount of unmanaged memory that has been allocated. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bytesAllocated" /> is less than or equal to 0.-or-On a 32-bit computer, <paramref name="bytesAllocated" /> is larger than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F8A RID: 3978 RVA: 0x00041634 File Offset: 0x0003F834
		public static void AddMemoryPressure(long bytesAllocated)
		{
			if (bytesAllocated <= 0L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("Positive number required."));
			}
			if (4 == IntPtr.Size && bytesAllocated > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("pressure", Environment.GetResourceString("Value must be non-negative and less than or equal to Int32.MaxValue."));
			}
			GC.RecordPressure(bytesAllocated);
		}

		/// <summary>Informs the runtime that unmanaged memory has been released and no longer needs to be taken into account when scheduling garbage collection.</summary>
		/// <param name="bytesAllocated">The amount of unmanaged memory that has been released. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bytesAllocated" /> is less than or equal to 0. -or- On a 32-bit computer, <paramref name="bytesAllocated" /> is larger than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F8B RID: 3979 RVA: 0x00041688 File Offset: 0x0003F888
		public static void RemoveMemoryPressure(long bytesAllocated)
		{
			if (bytesAllocated <= 0L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("Positive number required."));
			}
			if (4 == IntPtr.Size && bytesAllocated > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("Value must be non-negative and less than or equal to Int32.MaxValue."));
			}
			GC.RecordPressure(-bytesAllocated);
		}

		/// <summary>Forces an immediate garbage collection of all generations. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F8C RID: 3980 RVA: 0x000416DC File Offset: 0x0003F8DC
		public static void Collect()
		{
			GC.InternalCollect(GC.MaxGeneration);
		}

		/// <summary>Returns the number of times garbage collection has occurred for the specified generation of objects.</summary>
		/// <returns>The number of times garbage collection has occurred for the specified generation since the process was started.</returns>
		/// <param name="generation">The generation of objects for which the garbage collection count is to be determined. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="generation" /> is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F8D RID: 3981 RVA: 0x000416E8 File Offset: 0x0003F8E8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int CollectionCount(int generation)
		{
			if (generation < 0)
			{
				throw new ArgumentOutOfRangeException("generation", Environment.GetResourceString("Value must be positive."));
			}
			return GC.GetCollectionCount(generation);
		}

		/// <summary>References the specified object, which makes it ineligible for garbage collection from the start of the current routine to the point where this method is called.</summary>
		/// <param name="obj">The object to reference. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F8E RID: 3982 RVA: 0x00002C89 File Offset: 0x00000E89
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void KeepAlive(object obj)
		{
		}

		/// <summary>Gets the maximum number of generations that the system currently supports.</summary>
		/// <returns>A value that ranges from zero to the maximum number of supported generations.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00041709 File Offset: 0x0003F909
		public static int MaxGeneration
		{
			get
			{
				return GC.GetMaxGeneration();
			}
		}

		/// <summary>Suspends the current thread until the thread that is processing the queue of finalizers has emptied that queue.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F90 RID: 3984
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WaitForPendingFinalizers();

		// Token: 0x06000F91 RID: 3985
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _SuppressFinalize(object o);

		/// <summary>Requests that the system not call the finalizer for the specified object.</summary>
		/// <param name="obj">The object that a finalizer must not be called for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F92 RID: 3986 RVA: 0x00041710 File Offset: 0x0003F910
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void SuppressFinalize(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			GC._SuppressFinalize(obj);
		}

		// Token: 0x06000F93 RID: 3987
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _ReRegisterForFinalize(object o);

		/// <summary>Requests that the system call the finalizer for the specified object for which <see cref="M:System.GC.SuppressFinalize(System.Object)" /> has previously been called.</summary>
		/// <param name="obj">The object that a finalizer must be called for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000F94 RID: 3988 RVA: 0x00041726 File Offset: 0x0003F926
		public static void ReRegisterForFinalize(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			GC._ReRegisterForFinalize(obj);
		}

		// Token: 0x04000611 RID: 1553
		internal static readonly object EPHEMERON_TOMBSTONE = GC.get_ephemeron_tombstone();
	}
}
