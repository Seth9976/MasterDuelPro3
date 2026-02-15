using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;

namespace System.Security
{
	/// <summary>The exception that is thrown when a security error is detected.</summary>
	// Token: 0x02000328 RID: 808
	[ComVisible(true)]
	[Serializable]
	public class SecurityException : SystemException
	{
		/// <summary>Gets or sets the demanded security permission, permission set, or permission set collection that failed.</summary>
		/// <returns>A permission, permission set, or permission set collection object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x00070B9C File Offset: 0x0006ED9C
		[ComVisible(false)]
		public object Demanded
		{
			get
			{
				return this._demanded;
			}
		}

		/// <summary>Gets or sets the first permission in a permission set or permission set collection that failed the demand.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> object representing the first permission that failed.</returns>
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x00070BA4 File Offset: 0x0006EDA4
		public IPermission FirstPermissionThatFailed
		{
			get
			{
				return this._firstperm;
			}
		}

		/// <summary>Gets or sets the state of the permission that threw the exception.</summary>
		/// <returns>The state of the permission at the time the exception was thrown.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x00070BAC File Offset: 0x0006EDAC
		public string PermissionState
		{
			get
			{
				return this.permissionState;
			}
		}

		/// <summary>Gets or sets the type of the permission that failed.</summary>
		/// <returns>The type of the permission that failed.</returns>
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x00070BB4 File Offset: 0x0006EDB4
		public Type PermissionType
		{
			get
			{
				return this.permissionType;
			}
		}

		/// <summary>Gets or sets the granted permission set of the assembly that caused the <see cref="T:System.Security.SecurityException" />.</summary>
		/// <returns>The XML representation of the granted set of the assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x00070BBC File Offset: 0x0006EDBC
		public string GrantedSet
		{
			get
			{
				return this._granted;
			}
		}

		/// <summary>Gets or sets the refused permission set of the assembly that caused the <see cref="T:System.Security.SecurityException" />.</summary>
		/// <returns>The XML representation of the refused permission set of the assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x00070BC4 File Offset: 0x0006EDC4
		public string RefusedSet
		{
			get
			{
				return this._refused;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with default properties.</summary>
		// Token: 0x06001CD8 RID: 7384 RVA: 0x00070BCC File Offset: 0x0006EDCC
		public SecurityException()
			: this(Locale.GetText("A security error has been detected."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06001CD9 RID: 7385 RVA: 0x00070BDE File Offset: 0x0006EDDE
		public SecurityException(string message)
			: base(message)
		{
			base.HResult = -2146233078;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info " />is null.</exception>
		// Token: 0x06001CDA RID: 7386 RVA: 0x00070BF4 File Offset: 0x0006EDF4
		protected SecurityException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			base.HResult = -2146233078;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "PermissionState")
				{
					this.permissionState = (string)enumerator.Value;
					return;
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001CDB RID: 7387 RVA: 0x00070C49 File Offset: 0x0006EE49
		public SecurityException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233078;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the <see cref="T:System.Security.SecurityException" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06001CDC RID: 7388 RVA: 0x00070C60 File Offset: 0x0006EE60
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			try
			{
				info.AddValue("PermissionState", this.permissionState);
			}
			catch (SecurityException)
			{
			}
		}

		/// <summary>Returns a representation of the current <see cref="T:System.Security.SecurityException" />.</summary>
		/// <returns>A string representation of the current <see cref="T:System.Security.SecurityException" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06001CDD RID: 7389 RVA: 0x00070C9C File Offset: 0x0006EE9C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString());
			try
			{
				if (this.permissionType != null)
				{
					stringBuilder.AppendFormat("{0}Type: {1}", Environment.NewLine, this.PermissionType);
				}
				if (this._method != null)
				{
					string text = this._method.ToString();
					int num = text.IndexOf(" ") + 1;
					stringBuilder.AppendFormat("{0}Method: {1} {2}.{3}", new object[]
					{
						Environment.NewLine,
						this._method.ReturnType.Name,
						this._method.ReflectedType,
						text.Substring(num)
					});
				}
				if (this.permissionState != null)
				{
					stringBuilder.AppendFormat("{0}State: {1}", Environment.NewLine, this.PermissionState);
				}
				if (this._granted != null && this._granted.Length > 0)
				{
					stringBuilder.AppendFormat("{0}Granted: {1}", Environment.NewLine, this.GrantedSet);
				}
				if (this._refused != null && this._refused.Length > 0)
				{
					stringBuilder.AppendFormat("{0}Refused: {1}", Environment.NewLine, this.RefusedSet);
				}
				if (this._demanded != null)
				{
					stringBuilder.AppendFormat("{0}Demanded: {1}", Environment.NewLine, this.Demanded);
				}
				if (this._firstperm != null)
				{
					stringBuilder.AppendFormat("{0}Failed Permission: {1}", Environment.NewLine, this.FirstPermissionThatFailed);
				}
				if (this._evidence != null)
				{
					stringBuilder.AppendFormat("{0}Evidences:", Environment.NewLine);
					foreach (object obj in this._evidence)
					{
						if (!(obj is Hash))
						{
							stringBuilder.AppendFormat("{0}\t{1}", Environment.NewLine, obj);
						}
					}
				}
			}
			catch (SecurityException)
			{
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000D3A RID: 3386
		private string permissionState;

		// Token: 0x04000D3B RID: 3387
		private Type permissionType;

		// Token: 0x04000D3C RID: 3388
		private string _granted;

		// Token: 0x04000D3D RID: 3389
		private string _refused;

		// Token: 0x04000D3E RID: 3390
		private object _demanded;

		// Token: 0x04000D3F RID: 3391
		private IPermission _firstperm;

		// Token: 0x04000D40 RID: 3392
		private MethodInfo _method;

		// Token: 0x04000D41 RID: 3393
		private Evidence _evidence;
	}
}
