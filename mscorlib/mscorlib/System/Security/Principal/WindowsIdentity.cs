using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;

namespace System.Security.Principal
{
	/// <summary>Represents a Windows user.</summary>
	// Token: 0x020003DC RID: 988
	[ComVisible(true)]
	[Serializable]
	public class WindowsIdentity : ClaimsIdentity, IIdentity, IDeserializationCallback, ISerializable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified Windows account token, the specified authentication type, the specified Windows account type, and the specified authentication status.</summary>
		/// <param name="userToken">The account token for the user on whose behalf the code is running. </param>
		/// <param name="type">(Informational use only.) The type of authentication used to identify the user. For more information, see Remarks.</param>
		/// <param name="acctType">One of the enumeration values. </param>
		/// <param name="isAuthenticated">true to indicate that the user is authenticated; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="userToken" /> is 0.-or-<paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
		// Token: 0x0600218A RID: 8586 RVA: 0x0008BB81 File Offset: 0x00089D81
		public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType, bool isAuthenticated)
		{
			this._type = type;
			this._account = acctType;
			this._authenticated = isAuthenticated;
			this._name = null;
			this.SetToken(userToken);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by information in a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> stream.</summary>
		/// <param name="info">The object containing the account information for the user. </param>
		/// <param name="context">An object that indicates the stream characteristics. </param>
		/// <exception cref="T:System.NotSupportedException">A <see cref="T:System.Security.Principal.WindowsIdentity" /> cannot be serialized across processes. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-A Win32 error occurred.</exception>
		// Token: 0x0600218B RID: 8587 RVA: 0x0008BBAD File Offset: 0x00089DAD
		public WindowsIdentity(SerializationInfo info, StreamingContext context)
		{
			this._info = info;
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x0008BBBC File Offset: 0x00089DBC
		internal WindowsIdentity(ClaimsIdentity claimsIdentity, IntPtr userToken)
			: base(claimsIdentity)
		{
			if (userToken != IntPtr.Zero && userToken.ToInt64() > 0L)
			{
				this.SetToken(userToken);
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Principal.WindowsIdentity" />. </summary>
		// Token: 0x0600218D RID: 8589 RVA: 0x0008BBE4 File Offset: 0x00089DE4
		[ComVisible(false)]
		public void Dispose()
		{
			this._token = IntPtr.Zero;
		}

		/// <summary>Returns a <see cref="T:System.Security.Principal.WindowsIdentity" /> object that represents the current Windows user.</summary>
		/// <returns>An object that represents the current user.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x0600218E RID: 8590 RVA: 0x0008BBF1 File Offset: 0x00089DF1
		public static WindowsIdentity GetCurrent()
		{
			return new WindowsIdentity(WindowsIdentity.GetCurrentToken(), null, WindowsAccountType.Normal, true);
		}

		/// <summary>Impersonates the user represented by the <see cref="T:System.Security.Principal.WindowsIdentity" /> object.</summary>
		/// <returns>An object that represents the Windows user prior to impersonation; this can be used to revert to the original user's context.</returns>
		/// <exception cref="T:System.InvalidOperationException">An anonymous identity attempted to perform an impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">A Win32 error occurred.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x0600218F RID: 8591 RVA: 0x0008BC00 File Offset: 0x00089E00
		public virtual WindowsImpersonationContext Impersonate()
		{
			return new WindowsImpersonationContext(this._token);
		}

		/// <summary>Gets the type of authentication used to identify the user.</summary>
		/// <returns>The type of authentication used to identify the user.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">Windows returned the Windows NT status code STATUS_ACCESS_DENIED.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. -or-The computer is not attached to a Windows 2003 or later domain.-or-The computer is not running Windows 2003 or later.-or-The user is not a member of the domain the computer is attached to.</exception>
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x0008BC0D File Offset: 0x00089E0D
		public sealed override string AuthenticationType
		{
			get
			{
				return this._type;
			}
		}

		/// <summary>Gets the user's Windows logon name.</summary>
		/// <returns>The Windows logon name of the user on whose behalf the code is being run.</returns>
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x0008BC15 File Offset: 0x00089E15
		public override string Name
		{
			get
			{
				if (this._name == null)
				{
					this._name = WindowsIdentity.GetTokenName(this._token);
				}
				return this._name;
			}
		}

		/// <summary>Gets the security identifier (SID) for the user.</summary>
		/// <returns>An object for the user.</returns>
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x00033EF4 File Offset: 0x000320F4
		[MonoTODO("not implemented")]
		[ComVisible(false)]
		public SecurityIdentifier User
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event. </param>
		// Token: 0x06002193 RID: 8595 RVA: 0x0008BC38 File Offset: 0x00089E38
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this._token = (IntPtr)this._info.GetValue("m_userToken", typeof(IntPtr));
			this._name = this._info.GetString("m_name");
			if (this._name != null)
			{
				if (WindowsIdentity.GetTokenName(this._token) != this._name)
				{
					throw new SerializationException("Token-Name mismatch.");
				}
			}
			else
			{
				this._name = WindowsIdentity.GetTokenName(this._token);
				if (this._name == null)
				{
					throw new SerializationException("Token doesn't match a user.");
				}
			}
			this._type = this._info.GetString("m_type");
			this._account = (WindowsAccountType)this._info.GetValue("m_acctType", typeof(WindowsAccountType));
			this._authenticated = this._info.GetBoolean("m_isAuthenticated");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the logical context information needed to recreate an instance of this execution context.</summary>
		/// <param name="info">An object containing the information required to serialize the <see cref="T:System.Collections.Hashtable" />. </param>
		/// <param name="context">An object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Hashtable" />. </param>
		// Token: 0x06002194 RID: 8596 RVA: 0x0008BD20 File Offset: 0x00089F20
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_userToken", this._token);
			info.AddValue("m_name", this._name);
			info.AddValue("m_type", this._type);
			info.AddValue("m_acctType", this._account);
			info.AddValue("m_isAuthenticated", this._authenticated);
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x0008BD8C File Offset: 0x00089F8C
		internal ClaimsIdentity CloneAsBase()
		{
			return base.Clone();
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x0008BD94 File Offset: 0x00089F94
		internal IntPtr GetTokenInternal()
		{
			return this._token;
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x0008BD9C File Offset: 0x00089F9C
		private void SetToken(IntPtr token)
		{
			if (Environment.IsUnix)
			{
				this._token = token;
				if (this._type == null)
				{
					this._type = "POSIX";
				}
				if (this._token == IntPtr.Zero)
				{
					this._account = WindowsAccountType.System;
					return;
				}
			}
			else
			{
				if (token == WindowsIdentity.invalidWindows && this._account != WindowsAccountType.Anonymous)
				{
					throw new ArgumentException("Invalid token");
				}
				this._token = token;
				if (this._type == null)
				{
					this._type = "NTLM";
				}
			}
		}

		// Token: 0x06002198 RID: 8600
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetCurrentToken();

		// Token: 0x06002199 RID: 8601
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetTokenName(IntPtr token);

		// Token: 0x04001012 RID: 4114
		private IntPtr _token;

		// Token: 0x04001013 RID: 4115
		private string _type;

		// Token: 0x04001014 RID: 4116
		private WindowsAccountType _account;

		// Token: 0x04001015 RID: 4117
		private bool _authenticated;

		// Token: 0x04001016 RID: 4118
		private string _name;

		// Token: 0x04001017 RID: 4119
		private SerializationInfo _info;

		// Token: 0x04001018 RID: 4120
		private static IntPtr invalidWindows = IntPtr.Zero;
	}
}
