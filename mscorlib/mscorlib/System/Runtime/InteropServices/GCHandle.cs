using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a way to access a managed object from unmanaged memory.</summary>
	// Token: 0x02000546 RID: 1350
	[ComVisible(true)]
	public struct GCHandle
	{
		// Token: 0x06002960 RID: 10592 RVA: 0x000A8772 File Offset: 0x000A6972
		private GCHandle(IntPtr h)
		{
			this.handle = h;
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x000A877B File Offset: 0x000A697B
		private GCHandle(object obj)
		{
			this = new GCHandle(obj, GCHandleType.Normal);
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x000A8785 File Offset: 0x000A6985
		internal GCHandle(object value, GCHandleType type)
		{
			if (type < GCHandleType.Weak || type > GCHandleType.Pinned)
			{
				type = GCHandleType.Normal;
			}
			this.handle = GCHandle.GetTargetHandle(value, IntPtr.Zero, type);
		}

		/// <summary>Gets a value indicating whether the handle is allocated.</summary>
		/// <returns>true if the handle is allocated; otherwise, false.</returns>
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06002963 RID: 10595 RVA: 0x000A87A4 File Offset: 0x000A69A4
		public bool IsAllocated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.handle != IntPtr.Zero;
			}
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000A87B6 File Offset: 0x000A69B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static object GetRef(IntPtr handle)
		{
			return *Unsafe.As<IntPtr, object>(ref *(IntPtr*)(void*)handle);
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x000A87C4 File Offset: 0x000A69C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static void SetRef(IntPtr handle, object value)
		{
			*Unsafe.As<IntPtr, object>(ref *(IntPtr*)(void*)handle) = value;
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x000A87D3 File Offset: 0x000A69D3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool CanDereferenceHandle(IntPtr handle)
		{
			return (handle & (IntPtr)1) == (IntPtr)0;
		}

		/// <summary>Gets or sets the object this handle represents.</summary>
		/// <returns>The object this handle represents.</returns>
		/// <exception cref="T:System.InvalidOperationException">The handle was freed, or never initialized. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x000A87DD File Offset: 0x000A69DD
		// (set) Token: 0x06002968 RID: 10600 RVA: 0x000A8816 File Offset: 0x000A6A16
		public object Target
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (!this.IsAllocated)
				{
					throw new InvalidOperationException("Handle is not allocated");
				}
				if (GCHandle.CanDereferenceHandle(this.handle))
				{
					return GCHandle.GetRef(this.handle);
				}
				return GCHandle.GetTarget(this.handle);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				if (GCHandle.CanDereferenceHandle(this.handle))
				{
					GCHandle.SetRef(this.handle, value);
					return;
				}
				this.handle = GCHandle.GetTargetHandle(value, this.handle, (GCHandleType)(-1));
			}
		}

		/// <summary>Retrieves the address of an object in a <see cref="F:System.Runtime.InteropServices.GCHandleType.Pinned" /> handle.</summary>
		/// <returns>The address of the of the Pinned object as an <see cref="T:System.IntPtr" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The handle is any type other than <see cref="F:System.Runtime.InteropServices.GCHandleType.Pinned" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002969 RID: 10601 RVA: 0x000A8845 File Offset: 0x000A6A45
		public IntPtr AddrOfPinnedObject()
		{
			IntPtr addrOfPinnedObject = GCHandle.GetAddrOfPinnedObject(this.handle);
			if (addrOfPinnedObject == (IntPtr)(-1))
			{
				throw new ArgumentException("Object contains non-primitive or non-blittable data.");
			}
			if (addrOfPinnedObject == (IntPtr)(-2))
			{
				throw new InvalidOperationException("Handle is not pinned.");
			}
			return addrOfPinnedObject;
		}

		/// <summary>Allocates a <see cref="F:System.Runtime.InteropServices.GCHandleType.Normal" /> handle for the specified object.</summary>
		/// <returns>A new <see cref="T:System.Runtime.InteropServices.GCHandle" /> that protects the object from garbage collection. This <see cref="T:System.Runtime.InteropServices.GCHandle" /> must be released with <see cref="M:System.Runtime.InteropServices.GCHandle.Free" /> when it is no longer needed.</returns>
		/// <param name="value">The object that uses the <see cref="T:System.Runtime.InteropServices.GCHandle" />. </param>
		/// <exception cref="T:System.ArgumentException">An instance with nonprimitive (non-blittable) members cannot be pinned. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600296A RID: 10602 RVA: 0x000A8885 File Offset: 0x000A6A85
		public static GCHandle Alloc(object value)
		{
			return new GCHandle(value);
		}

		/// <summary>Allocates a handle of the specified type for the specified object.</summary>
		/// <returns>A new <see cref="T:System.Runtime.InteropServices.GCHandle" /> of the specified type. This <see cref="T:System.Runtime.InteropServices.GCHandle" /> must be released with <see cref="M:System.Runtime.InteropServices.GCHandle.Free" /> when it is no longer needed.</returns>
		/// <param name="value">The object that uses the <see cref="T:System.Runtime.InteropServices.GCHandle" />. </param>
		/// <param name="type">One of the <see cref="T:System.Runtime.InteropServices.GCHandleType" /> values, indicating the type of <see cref="T:System.Runtime.InteropServices.GCHandle" /> to create. </param>
		/// <exception cref="T:System.ArgumentException">An instance with nonprimitive (non-blittable) members cannot be pinned. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600296B RID: 10603 RVA: 0x000A888D File Offset: 0x000A6A8D
		public static GCHandle Alloc(object value, GCHandleType type)
		{
			return new GCHandle(value, type);
		}

		/// <summary>Releases a <see cref="T:System.Runtime.InteropServices.GCHandle" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The handle was freed or never initialized. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600296C RID: 10604 RVA: 0x000A8898 File Offset: 0x000A6A98
		public void Free()
		{
			IntPtr intPtr = this.handle;
			if (intPtr != IntPtr.Zero && Interlocked.CompareExchange(ref this.handle, IntPtr.Zero, intPtr) == intPtr)
			{
				GCHandle.FreeHandle(intPtr);
				return;
			}
			throw new InvalidOperationException("Handle is not initialized.");
		}

		/// <summary>A <see cref="T:System.Runtime.InteropServices.GCHandle" /> is stored using an internal integer representation.</summary>
		/// <returns>The integer value.</returns>
		/// <param name="value">The <see cref="T:System.Runtime.InteropServices.GCHandle" /> for which the integer is required. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600296D RID: 10605 RVA: 0x000A88E3 File Offset: 0x000A6AE3
		public static explicit operator IntPtr(GCHandle value)
		{
			return value.handle;
		}

		/// <summary>A <see cref="T:System.Runtime.InteropServices.GCHandle" /> is stored using an internal integer representation.</summary>
		/// <returns>The stored <see cref="T:System.Runtime.InteropServices.GCHandle" /> object using an internal integer representation.</returns>
		/// <param name="value">An <see cref="T:System.IntPtr" /> that indicates the handle for which the conversion is required. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600296E RID: 10606 RVA: 0x000A88EB File Offset: 0x000A6AEB
		public static explicit operator GCHandle(IntPtr value)
		{
			if (value == IntPtr.Zero)
			{
				throw new InvalidOperationException("GCHandle value cannot be zero");
			}
			if (!GCHandle.CheckCurrentDomain(value))
			{
				throw new ArgumentException("GCHandle value belongs to a different domain");
			}
			return new GCHandle(value);
		}

		// Token: 0x0600296F RID: 10607
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CheckCurrentDomain(IntPtr handle);

		// Token: 0x06002970 RID: 10608
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetTarget(IntPtr handle);

		// Token: 0x06002971 RID: 10609
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetTargetHandle(object obj, IntPtr handle, GCHandleType type);

		// Token: 0x06002972 RID: 10610
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FreeHandle(IntPtr handle);

		// Token: 0x06002973 RID: 10611
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetAddrOfPinnedObject(IntPtr handle);

		/// <summary>Returns a value indicating whether two <see cref="T:System.Runtime.InteropServices.GCHandle" /> objects are equal.</summary>
		/// <returns>true if the <paramref name="a" /> and <paramref name="b" /> parameters are equal; otherwise, false.</returns>
		/// <param name="a">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the <paramref name="b" /> parameter. </param>
		/// <param name="b">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the <paramref name="a" /> parameter.  </param>
		// Token: 0x06002974 RID: 10612 RVA: 0x000A891E File Offset: 0x000A6B1E
		public static bool operator ==(GCHandle a, GCHandle b)
		{
			return a.handle == b.handle;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Runtime.InteropServices.GCHandle" /> object is equal to the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</summary>
		/// <returns>true if the specified <see cref="T:System.Runtime.InteropServices.GCHandle" /> object is equal to the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to compare with the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</param>
		// Token: 0x06002975 RID: 10613 RVA: 0x000A8931 File Offset: 0x000A6B31
		public override bool Equals(object o)
		{
			return o is GCHandle && this == (GCHandle)o;
		}

		/// <summary>Returns an identifier for the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</summary>
		/// <returns>An identifier for the current <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</returns>
		// Token: 0x06002976 RID: 10614 RVA: 0x000A894E File Offset: 0x000A6B4E
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		/// <summary>Returns a new <see cref="T:System.Runtime.InteropServices.GCHandle" /> object created from a handle to a managed object.</summary>
		/// <returns>A new <see cref="T:System.Runtime.InteropServices.GCHandle" /> object that corresponds to the value parameter.  </returns>
		/// <param name="value">An <see cref="T:System.IntPtr" /> handle to a managed object to create a <see cref="T:System.Runtime.InteropServices.GCHandle" /> object from.</param>
		/// <exception cref="T:System.InvalidOperationException">The value of the <paramref name="value" /> parameter is <see cref="F:System.IntPtr.Zero" />.</exception>
		// Token: 0x06002977 RID: 10615 RVA: 0x000A895B File Offset: 0x000A6B5B
		public static GCHandle FromIntPtr(IntPtr value)
		{
			return (GCHandle)value;
		}

		/// <summary>Returns the internal integer representation of a <see cref="T:System.Runtime.InteropServices.GCHandle" /> object.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> object that represents a <see cref="T:System.Runtime.InteropServices.GCHandle" /> object. </returns>
		/// <param name="value">A <see cref="T:System.Runtime.InteropServices.GCHandle" /> object to retrieve an internal integer representation from.</param>
		// Token: 0x06002978 RID: 10616 RVA: 0x000A8963 File Offset: 0x000A6B63
		public static IntPtr ToIntPtr(GCHandle value)
		{
			return (IntPtr)value;
		}

		// Token: 0x04001591 RID: 5521
		private IntPtr handle;
	}
}
