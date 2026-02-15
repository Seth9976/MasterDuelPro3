using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when a managed assembly is found but cannot be loaded.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000797 RID: 1943
	[Serializable]
	public class FileLoadException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class, setting the <see cref="P:System.Exception.Message" /> property of the new instance to a system-supplied message that describes the error, such as "Could not load the specified file." This message takes into account the current system culture.</summary>
		// Token: 0x06003D53 RID: 15699 RVA: 0x000EC302 File Offset: 0x000EA502
		public FileLoadException()
			: base("Could not load the specified file.")
		{
			base.HResult = -2146232799;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with the specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		// Token: 0x06003D54 RID: 15700 RVA: 0x000EC31A File Offset: 0x000EA51A
		public FileLoadException(string message)
			: base(message)
		{
			base.HResult = -2146232799;
		}

		/// <summary>Gets the error message and the name of the file that caused this exception.</summary>
		/// <returns>A string containing the error message and the name of the file that caused this exception.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06003D55 RID: 15701 RVA: 0x000EC32E File Offset: 0x000EA52E
		public override string Message
		{
			get
			{
				if (this._message == null)
				{
					this._message = FileLoadException.FormatFileLoadExceptionMessage(this.FileName, base.HResult);
				}
				return this._message;
			}
		}

		/// <summary>Gets the name of the file that causes this exception.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the file with the invalid image, or a null reference if no file name was passed to the constructor for the current instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06003D56 RID: 15702 RVA: 0x000EC355 File Offset: 0x000EA555
		public string FileName { get; }

		/// <summary>Gets the log file that describes why an assembly load failed.</summary>
		/// <returns>A string containing errors reported by the assembly cache.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06003D57 RID: 15703 RVA: 0x000EC35D File Offset: 0x000EA55D
		public string FusionLog { get; }

		/// <summary>Returns the fully qualified name of the current exception, and possibly the error message, the name of the inner exception, and the stack trace.</summary>
		/// <returns>A string containing the fully qualified name of this exception, and possibly the error message, the name of the inner exception, and the stack trace, depending on which <see cref="T:System.IO.FileLoadException" /> constructor is used.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003D58 RID: 15704 RVA: 0x000EC368 File Offset: 0x000EA568
		public override string ToString()
		{
			string text = base.GetType().ToString() + ": " + this.Message;
			if (this.FileName != null && this.FileName.Length != 0)
			{
				text = text + Environment.NewLine + SR.Format("File name: '{0}'", this.FileName);
			}
			if (base.InnerException != null)
			{
				text = text + " ---> " + base.InnerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			if (this.FusionLog != null)
			{
				if (text == null)
				{
					text = " ";
				}
				text += Environment.NewLine;
				text += Environment.NewLine;
				text += this.FusionLog;
			}
			return text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileLoadException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06003D59 RID: 15705 RVA: 0x000EC432 File Offset: 0x000EA632
		protected FileLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.FileName = info.GetString("FileLoad_FileName");
			this.FusionLog = info.GetString("FileLoad_FusionLog");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the file name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003D5A RID: 15706 RVA: 0x000EC45E File Offset: 0x000EA65E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FileLoad_FileName", this.FileName, typeof(string));
			info.AddValue("FileLoad_FusionLog", this.FusionLog, typeof(string));
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x000EC49E File Offset: 0x000EA69E
		internal static string FormatFileLoadExceptionMessage(string fileName, int hResult)
		{
			if (fileName != null)
			{
				return SR.Format("Could not load the file '{0}'.", fileName);
			}
			return "Could not load the specified file.";
		}
	}
}
