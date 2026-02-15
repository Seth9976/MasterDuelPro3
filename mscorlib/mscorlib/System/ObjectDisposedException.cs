using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an operation is performed on a disposed object.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200012E RID: 302
	[Serializable]
	public class ObjectDisposedException : InvalidOperationException
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x0002E4CD File Offset: 0x0002C6CD
		private ObjectDisposedException()
			: this(null, "Cannot access a disposed object.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with a string containing the name of the disposed object.</summary>
		/// <param name="objectName">A string containing the name of the disposed object. </param>
		// Token: 0x06000A2E RID: 2606 RVA: 0x0002E4DB File Offset: 0x0002C6DB
		public ObjectDisposedException(string objectName)
			: this(objectName, "Cannot access a disposed object.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with the specified object name and message.</summary>
		/// <param name="objectName">The name of the disposed object. </param>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06000A2F RID: 2607 RVA: 0x0002E4E9 File Offset: 0x0002C6E9
		public ObjectDisposedException(string objectName, string message)
			: base(message)
		{
			base.HResult = -2146232798;
			this._objectName = objectName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObjectDisposedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06000A30 RID: 2608 RVA: 0x0002E504 File Offset: 0x0002C704
		protected ObjectDisposedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._objectName = info.GetString("ObjectName");
		}

		/// <summary>Retrieves the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000A31 RID: 2609 RVA: 0x0002E51F File Offset: 0x0002C71F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ObjectName", this.ObjectName, typeof(string));
		}

		/// <summary>Gets the message that describes the error.</summary>
		/// <returns>A string that describes the error.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0002E544 File Offset: 0x0002C744
		public override string Message
		{
			get
			{
				string objectName = this.ObjectName;
				if (objectName == null || objectName.Length == 0)
				{
					return base.Message;
				}
				string text = SR.Format("Object name: '{0}'.", objectName);
				return base.Message + Environment.NewLine + text;
			}
		}

		/// <summary>Gets the name of the disposed object.</summary>
		/// <returns>A string containing the name of the disposed object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0002E587 File Offset: 0x0002C787
		public string ObjectName
		{
			get
			{
				if (this._objectName == null)
				{
					return string.Empty;
				}
				return this._objectName;
			}
		}

		// Token: 0x04000462 RID: 1122
		private string _objectName;
	}
}
