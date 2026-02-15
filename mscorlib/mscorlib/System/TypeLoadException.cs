using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when type-loading failures occur.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B2 RID: 434
	[ComVisible(true)]
	[Serializable]
	public class TypeLoadException : SystemException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TypeLoadException" /> class.</summary>
		// Token: 0x0600104A RID: 4170 RVA: 0x00044FF1 File Offset: 0x000431F1
		public TypeLoadException()
			: base(Environment.GetResourceString("Failure has occurred while loading a type."))
		{
			base.SetErrorCode(-2146233054);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeLoadException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x0600104B RID: 4171 RVA: 0x0004500E File Offset: 0x0004320E
		public TypeLoadException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233054);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeLoadException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600104C RID: 4172 RVA: 0x00045022 File Offset: 0x00043222
		public TypeLoadException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233054);
		}

		/// <summary>Gets the error message for this exception.</summary>
		/// <returns>The error message string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x00045037 File Offset: 0x00043237
		public override string Message
		{
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00045048 File Offset: 0x00043248
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this.ClassName == null && this.ResourceId == 0)
				{
					this._message = Environment.GetResourceString("Failure has occurred while loading a type.");
					return;
				}
				if (this.AssemblyName == null)
				{
					this.AssemblyName = Environment.GetResourceString("[Unknown]");
				}
				if (this.ClassName == null)
				{
					this.ClassName = Environment.GetResourceString("[Unknown]");
				}
				string text = "Could not load type '{0}' from assembly '{1}'.";
				this._message = string.Format(CultureInfo.CurrentCulture, text, this.ClassName, this.AssemblyName, this.MessageArg);
			}
		}

		/// <summary>Gets the fully qualified name of the type that causes the exception.</summary>
		/// <returns>The fully qualified type name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x000450D9 File Offset: 0x000432D9
		public string TypeName
		{
			get
			{
				if (this.ClassName == null)
				{
					return string.Empty;
				}
				return this.ClassName;
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000450EF File Offset: 0x000432EF
		private TypeLoadException(string className, string assemblyName)
			: this(className, assemblyName, null, 0)
		{
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x000450FB File Offset: 0x000432FB
		private TypeLoadException(string className, string assemblyName, string messageArg, int resourceId)
			: base(null)
		{
			base.SetErrorCode(-2146233054);
			this.ClassName = className;
			this.AssemblyName = assemblyName;
			this.MessageArg = messageArg;
			this.ResourceId = resourceId;
			this.SetMessageField();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeLoadException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is null. </exception>
		// Token: 0x06001052 RID: 4178 RVA: 0x00045134 File Offset: 0x00043334
		protected TypeLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.ClassName = info.GetString("TypeLoadClassName");
			this.AssemblyName = info.GetString("TypeLoadAssemblyName");
			this.MessageArg = info.GetString("TypeLoadMessageArg");
			this.ResourceId = info.GetInt32("TypeLoadResourceID");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the class name, method name, resource ID, and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001053 RID: 4179 RVA: 0x0004519C File Offset: 0x0004339C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("TypeLoadClassName", this.ClassName, typeof(string));
			info.AddValue("TypeLoadAssemblyName", this.AssemblyName, typeof(string));
			info.AddValue("TypeLoadMessageArg", this.MessageArg, typeof(string));
			info.AddValue("TypeLoadResourceID", this.ResourceId);
		}

		// Token: 0x040006AF RID: 1711
		private string ClassName;

		// Token: 0x040006B0 RID: 1712
		private string AssemblyName;

		// Token: 0x040006B1 RID: 1713
		private string MessageArg;

		// Token: 0x040006B2 RID: 1714
		internal int ResourceId;
	}
}
