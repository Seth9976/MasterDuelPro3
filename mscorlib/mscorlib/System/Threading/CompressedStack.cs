using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Unity;

namespace System.Threading
{
	/// <summary>Provides methods for setting and capturing the compressed stack on the current thread. This class cannot be inherited. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200027A RID: 634
	[Serializable]
	public sealed class CompressedStack : ISerializable
	{
		// Token: 0x06001782 RID: 6018 RVA: 0x0005B8F0 File Offset: 0x00059AF0
		internal CompressedStack(int length)
		{
			if (length > 0)
			{
				this._list = new ArrayList(length);
			}
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0005B908 File Offset: 0x00059B08
		internal CompressedStack(CompressedStack cs)
		{
			if (cs != null && cs._list != null)
			{
				this._list = (ArrayList)cs._list.Clone();
			}
		}

		/// <summary>Creates a copy of the current compressed stack.</summary>
		/// <returns>A <see cref="T:System.Threading.CompressedStack" /> object representing the current compressed stack.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001784 RID: 6020 RVA: 0x0005B931 File Offset: 0x00059B31
		[ComVisible(false)]
		public CompressedStack CreateCopy()
		{
			return new CompressedStack(this);
		}

		/// <summary>Captures the compressed stack from the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.CompressedStack" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001785 RID: 6021 RVA: 0x000339FF File Offset: 0x00031BFF
		public static CompressedStack Capture()
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the compressed stack for the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.CompressedStack" /> for the current thread.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.-or-The request for <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" />
		/// </PermissionSet>
		// Token: 0x06001786 RID: 6022 RVA: 0x000339FF File Offset: 0x00031BFF
		public static CompressedStack GetCompressedStack()
		{
			throw new NotSupportedException();
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the logical context information needed to recreate an instance of this execution context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to be populated with serialization information. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure representing the destination context of the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001787 RID: 6023 RVA: 0x00047EAE File Offset: 0x000460AE
		[MonoTODO("incomplete")]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		/// <summary>Runs a method in the specified compressed stack on the current thread.</summary>
		/// <param name="compressedStack">The <see cref="T:System.Threading.CompressedStack" /> to set.</param>
		/// <param name="callback">A <see cref="T:System.Threading.ContextCallback" /> that represents the method to be run in the specified security context.</param>
		/// <param name="state">The object to be passed to the callback method.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="compressedStack" /> is null.</exception>
		// Token: 0x06001788 RID: 6024 RVA: 0x000339FF File Offset: 0x00031BFF
		public static void Run(CompressedStack compressedStack, ContextCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0005B939 File Offset: 0x00059B39
		internal bool Equals(CompressedStack cs)
		{
			if (this.IsEmpty())
			{
				return cs.IsEmpty();
			}
			return !cs.IsEmpty() && this._list.Count == cs._list.Count;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0005B96F File Offset: 0x00059B6F
		internal bool IsEmpty()
		{
			return this._list == null || this._list.Count == 0;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x0005B989 File Offset: 0x00059B89
		internal IList List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x000176B9 File Offset: 0x000158B9
		internal CompressedStack()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000B35 RID: 2869
		private ArrayList _list;
	}
}
