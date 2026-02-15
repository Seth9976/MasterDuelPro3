using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Provides the ability to uniquely identify a manifest-activated application. This class cannot be inherited. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C2 RID: 450
	[ComVisible(false)]
	[Serializable]
	public sealed class ApplicationIdentity : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationIdentity" /> class. </summary>
		/// <param name="applicationIdentityFullName">The full name of the application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationIdentityFullName" /> is null.</exception>
		// Token: 0x060011AA RID: 4522 RVA: 0x00048517 File Offset: 0x00046717
		public ApplicationIdentity(string applicationIdentityFullName)
		{
			if (applicationIdentityFullName == null)
			{
				throw new ArgumentNullException("applicationIdentityFullName");
			}
			if (applicationIdentityFullName.IndexOf(", Culture=") == -1)
			{
				this._fullName = applicationIdentityFullName + ", Culture=neutral";
				return;
			}
			this._fullName = applicationIdentityFullName;
		}

		/// <summary>Gets the full name of the application.</summary>
		/// <returns>The full name of the application, also known as the display name.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00048554 File Offset: 0x00046754
		public string FullName
		{
			get
			{
				return this._fullName;
			}
		}

		/// <summary>Returns the full name of the manifest-activated application.</summary>
		/// <returns>The full name of the manifest-activated application.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060011AC RID: 4524 RVA: 0x00048554 File Offset: 0x00046754
		public override string ToString()
		{
			return this._fullName;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" />) structure for the serialization.</param>
		// Token: 0x060011AD RID: 4525 RVA: 0x00047EAE File Offset: 0x000460AE
		[MonoTODO("Missing serialization")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		// Token: 0x0400072F RID: 1839
		private string _fullName;
	}
}
