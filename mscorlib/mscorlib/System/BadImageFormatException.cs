using System;
using System.IO;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the file image of a dynamic link library (DLL) or an executable program is invalid. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C8 RID: 200
	[Serializable]
	public class BadImageFormatException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class.</summary>
		// Token: 0x060004DF RID: 1247 RVA: 0x00018D46 File Offset: 0x00016F46
		public BadImageFormatException()
			: base("Format of the executable (.exe) or library (.dll) is invalid.")
		{
			base.HResult = -2147024885;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x060004E0 RID: 1248 RVA: 0x00018D5E File Offset: 0x00016F5E
		public BadImageFormatException(string message)
			: base(message)
		{
			base.HResult = -2147024885;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060004E1 RID: 1249 RVA: 0x00018D72 File Offset: 0x00016F72
		public BadImageFormatException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2147024885;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with a specified error message and file name.</summary>
		/// <param name="message">A message that describes the error. </param>
		/// <param name="fileName">The full name of the file with the invalid image. </param>
		// Token: 0x060004E2 RID: 1250 RVA: 0x00018D87 File Offset: 0x00016F87
		public BadImageFormatException(string message, string fileName)
			: base(message)
		{
			base.HResult = -2147024885;
			this._fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.BadImageFormatException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x060004E3 RID: 1251 RVA: 0x00018DA2 File Offset: 0x00016FA2
		protected BadImageFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._fileName = info.GetString("BadImageFormat_FileName");
			this._fusionLog = info.GetString("BadImageFormat_FusionLog");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name, assembly cache log, and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x060004E4 RID: 1252 RVA: 0x00018DCE File Offset: 0x00016FCE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("BadImageFormat_FileName", this._fileName, typeof(string));
			info.AddValue("BadImageFormat_FusionLog", this._fusionLog, typeof(string));
		}

		/// <summary>Gets the error message and the name of the file that caused this exception.</summary>
		/// <returns>A string containing the error message and the name of the file that caused this exception.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00018E0E File Offset: 0x0001700E
		public override string Message
		{
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00018E1C File Offset: 0x0001701C
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this._fileName == null && base.HResult == -2146233088)
				{
					this._message = "Format of the executable (.exe) or library (.dll) is invalid.";
					return;
				}
				this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, base.HResult);
			}
		}

		/// <summary>Returns the fully qualified name of this exception and possibly the error message, the name of the inner exception, and the stack trace.</summary>
		/// <returns>A string containing the fully qualified name of this exception and possibly the error message, the name of the inner exception, and the stack trace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x060004E7 RID: 1255 RVA: 0x00018E6C File Offset: 0x0001706C
		public override string ToString()
		{
			string text = base.GetType().ToString() + ": " + this.Message;
			if (this._fileName != null && this._fileName.Length != 0)
			{
				text = text + Environment.NewLine + SR.Format("File name: '{0}'", this._fileName);
			}
			if (base.InnerException != null)
			{
				text = text + " ---> " + base.InnerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			if (this._fusionLog != null)
			{
				if (text == null)
				{
					text = " ";
				}
				text += Environment.NewLine;
				text += Environment.NewLine;
				text += this._fusionLog;
			}
			return text;
		}

		// Token: 0x040002D7 RID: 727
		private string _fileName;

		// Token: 0x040002D8 RID: 728
		private string _fusionLog;
	}
}
