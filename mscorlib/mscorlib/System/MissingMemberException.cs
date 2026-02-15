using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to dynamically access a class member that does not exist.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017F RID: 383
	[Serializable]
	public class MissingMemberException : MemberAccessException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class.</summary>
		// Token: 0x06000DB6 RID: 3510 RVA: 0x0003A1A8 File Offset: 0x000383A8
		public MissingMemberException()
			: base("Attempted to access a missing member.")
		{
			base.HResult = -2146233070;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000DB7 RID: 3511 RVA: 0x0003A1C0 File Offset: 0x000383C0
		public MissingMemberException(string message)
			: base(message)
		{
			base.HResult = -2146233070;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MissingMemberException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000DB8 RID: 3512 RVA: 0x0003A1D4 File Offset: 0x000383D4
		protected MissingMemberException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ClassName = info.GetString("MMClassName");
			this.MemberName = info.GetString("MMMemberName");
			this.Signature = (byte[])info.GetValue("MMSignature", typeof(byte[]));
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the class name, the member name, the signature of the missing member, and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000DB9 RID: 3513 RVA: 0x0003A22C File Offset: 0x0003842C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("MMClassName", this.ClassName, typeof(string));
			info.AddValue("MMMemberName", this.MemberName, typeof(string));
			info.AddValue("MMSignature", this.Signature, typeof(byte[]));
		}

		/// <summary>Gets the text string showing the class name, the member name, and the signature of the missing member.</summary>
		/// <returns>The error message string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x0003A294 File Offset: 0x00038494
		public override string Message
		{
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				return SR.Format("Member '{0}' not found.", this.ClassName + "." + this.MemberName + ((this.Signature != null) ? (" " + MissingMemberException.FormatSignature(this.Signature)) : string.Empty));
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0001C227 File Offset: 0x0001A427
		internal static string FormatSignature(byte[] signature)
		{
			return string.Empty;
		}

		/// <summary>Holds the class name of the missing member.</summary>
		// Token: 0x040005A5 RID: 1445
		protected string ClassName;

		/// <summary>Holds the name of the missing member.</summary>
		// Token: 0x040005A6 RID: 1446
		protected string MemberName;

		/// <summary>Holds the signature of the missing member.</summary>
		// Token: 0x040005A7 RID: 1447
		protected byte[] Signature;
	}
}
