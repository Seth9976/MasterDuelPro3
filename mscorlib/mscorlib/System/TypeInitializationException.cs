using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown as a wrapper around the exception thrown by the class initializer. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015B RID: 347
	[Serializable]
	public sealed class TypeInitializationException : SystemException
	{
		// Token: 0x06000C4C RID: 3148 RVA: 0x000340E3 File Offset: 0x000322E3
		private TypeInitializationException()
			: base("Type constructor threw an exception.")
		{
			base.HResult = -2146233036;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeInitializationException" /> class with the default error message, the specified type name, and a reference to the inner exception that is the root cause of this exception.</summary>
		/// <param name="fullTypeName">The fully qualified name of the type that fails to initialize. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000C4D RID: 3149 RVA: 0x000340FB File Offset: 0x000322FB
		public TypeInitializationException(string fullTypeName, Exception innerException)
			: this(fullTypeName, SR.Format("The type initializer for '{0}' threw an exception.", fullTypeName), innerException)
		{
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00034110 File Offset: 0x00032310
		internal TypeInitializationException(string message)
			: base(message)
		{
			base.HResult = -2146233036;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00034124 File Offset: 0x00032324
		internal TypeInitializationException(string fullTypeName, string message, Exception innerException)
			: base(message, innerException)
		{
			this._typeName = fullTypeName;
			base.HResult = -2146233036;
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00034140 File Offset: 0x00032340
		internal TypeInitializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._typeName = info.GetString("TypeName");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the type name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000C51 RID: 3153 RVA: 0x0003415B File Offset: 0x0003235B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("TypeName", this.TypeName, typeof(string));
		}

		/// <summary>Gets the fully qualified name of the type that fails to initialize.</summary>
		/// <returns>The fully qualified name of the type that fails to initialize.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x00034180 File Offset: 0x00032380
		public string TypeName
		{
			get
			{
				if (this._typeName == null)
				{
					return string.Empty;
				}
				return this._typeName;
			}
		}

		// Token: 0x040004BD RID: 1213
		private string _typeName;
	}
}
