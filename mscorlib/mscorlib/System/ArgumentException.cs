using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when one of the arguments provided to a method is not valid.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000BC RID: 188
	[Serializable]
	public class ArgumentException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class.</summary>
		// Token: 0x06000499 RID: 1177 RVA: 0x00018774 File Offset: 0x00016974
		public ArgumentException()
			: base("Value does not fall within the expected range.")
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x0600049A RID: 1178 RVA: 0x0001878C File Offset: 0x0001698C
		public ArgumentException(string message)
			: base(message)
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600049B RID: 1179 RVA: 0x000187A0 File Offset: 0x000169A0
		public ArgumentException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message, the parameter name, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="paramName">The name of the parameter that caused the current exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600049C RID: 1180 RVA: 0x000187B5 File Offset: 0x000169B5
		public ArgumentException(string message, string paramName, Exception innerException)
			: base(message, innerException)
		{
			this._paramName = paramName;
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="paramName">The name of the parameter that caused the current exception. </param>
		// Token: 0x0600049D RID: 1181 RVA: 0x000187D1 File Offset: 0x000169D1
		public ArgumentException(string message, string paramName)
			: base(message)
		{
			this._paramName = paramName;
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600049E RID: 1182 RVA: 0x000187EC File Offset: 0x000169EC
		protected ArgumentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._paramName = info.GetString("ParamName");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is a null reference (Nothing in Visual Basic). </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600049F RID: 1183 RVA: 0x00018807 File Offset: 0x00016A07
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ParamName", this._paramName, typeof(string));
		}

		/// <summary>Gets the error message and the parameter name, or only the error message if no parameter name is set.</summary>
		/// <returns>A text string describing the details of the exception. The value of this property takes one of two forms: Condition Value The <paramref name="paramName" /> is a null reference (Nothing in Visual Basic) or of zero length. The <paramref name="message" /> string passed to the constructor. The <paramref name="paramName" /> is not null reference (Nothing in Visual Basic) and it has a length greater than zero. The <paramref name="message" /> string appended with the name of the invalid parameter. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0001882C File Offset: 0x00016A2C
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (!string.IsNullOrEmpty(this._paramName))
				{
					string text = SR.Format("Parameter name: {0}", this._paramName);
					return message + Environment.NewLine + text;
				}
				return message;
			}
		}

		// Token: 0x040002B7 RID: 695
		private string _paramName;
	}
}
