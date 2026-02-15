using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;

namespace System
{
	/// <summary>Enables access to objects across application domain boundaries in applications that support remoting.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D2 RID: 466
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class MarshalByRefObject
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x00049B1F File Offset: 0x00047D1F
		// (set) Token: 0x06001242 RID: 4674 RVA: 0x00049B27 File Offset: 0x00047D27
		internal ServerIdentity ObjectIdentity
		{
			get
			{
				return this._identity;
			}
			set
			{
				this._identity = value;
			}
		}

		/// <summary>Creates an object that contains all the relevant information required to generate a proxy used to communicate with a remote object.</summary>
		/// <returns>Information required to generate a proxy.</returns>
		/// <param name="requestedType">The <see cref="T:System.Type" /> of the object that the new <see cref="T:System.Runtime.Remoting.ObjRef" /> will reference. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">This instance is not a valid remoting object. </exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06001243 RID: 4675 RVA: 0x00049B30 File Offset: 0x00047D30
		public virtual ObjRef CreateObjRef(Type requestedType)
		{
			if (this._identity == null)
			{
				throw new RemotingException(Locale.GetText("No remoting information was found for the object."));
			}
			return this._identity.CreateObjRef(requestedType);
		}

		/// <summary>Retrieves the current lifetime service object that controls the lifetime policy for this instance.</summary>
		/// <returns>An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> used to control the lifetime policy for this instance.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06001244 RID: 4676 RVA: 0x00049B56 File Offset: 0x00047D56
		public object GetLifetimeService()
		{
			if (this._identity == null)
			{
				return null;
			}
			return this._identity.Lease;
		}

		/// <summary>Obtains a lifetime service object to control the lifetime policy for this instance.</summary>
		/// <returns>An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> used to control the lifetime policy for this instance. This is the current lifetime service object for this instance if one exists; otherwise, a new lifetime service object initialized to the value of the <see cref="P:System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime" /> property.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06001245 RID: 4677 RVA: 0x00049B6D File Offset: 0x00047D6D
		public virtual object InitializeLifetimeService()
		{
			if (this._identity != null && this._identity.Lease != null)
			{
				return this._identity.Lease;
			}
			return new Lease();
		}

		// Token: 0x0400075F RID: 1887
		[NonSerialized]
		private ServerIdentity _identity;
	}
}
