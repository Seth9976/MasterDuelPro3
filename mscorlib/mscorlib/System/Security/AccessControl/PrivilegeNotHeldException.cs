using System;
using System.Runtime.Serialization;

namespace System.Security.AccessControl
{
	/// <summary>The exception that is thrown when a method in the <see cref="N:System.Security.AccessControl" /> namespace attempts to enable a privilege that it does not have.</summary>
	// Token: 0x020003E3 RID: 995
	[Serializable]
	public sealed class PrivilegeNotHeldException : UnauthorizedAccessException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.PrivilegeNotHeldException" /> class.</summary>
		// Token: 0x060021DC RID: 8668 RVA: 0x0008D31E File Offset: 0x0008B51E
		public PrivilegeNotHeldException()
			: base("The process does not possess some privilege required for this operation.")
		{
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x0008D32B File Offset: 0x0008B52B
		private PrivilegeNotHeldException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._privilegeName = info.GetString("PrivilegeName");
		}

		/// <summary>Sets the <paramref name="info" /> parameter with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060021DE RID: 8670 RVA: 0x0008D346 File Offset: 0x0008B546
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("PrivilegeName", this._privilegeName, typeof(string));
		}

		// Token: 0x0400103D RID: 4157
		private readonly string _privilegeName;
	}
}
