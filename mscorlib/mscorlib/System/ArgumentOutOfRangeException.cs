using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the value of an argument is outside the allowable range of values as defined by the invoked method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000BE RID: 190
	[Serializable]
	public class ArgumentOutOfRangeException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class.</summary>
		// Token: 0x060004A5 RID: 1189 RVA: 0x000188BC File Offset: 0x00016ABC
		public ArgumentOutOfRangeException()
			: base("Specified argument was out of the range of valid values.")
		{
			base.HResult = -2146233086;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class with the name of the parameter that causes this exception.</summary>
		/// <param name="paramName">The name of the parameter that causes this exception. </param>
		// Token: 0x060004A6 RID: 1190 RVA: 0x000188D4 File Offset: 0x00016AD4
		public ArgumentOutOfRangeException(string paramName)
			: base("Specified argument was out of the range of valid values.", paramName)
		{
			base.HResult = -2146233086;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class with the name of the parameter that causes this exception and a specified error message.</summary>
		/// <param name="paramName">The name of the parameter that caused the exception. </param>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x060004A7 RID: 1191 RVA: 0x000188ED File Offset: 0x00016AED
		public ArgumentOutOfRangeException(string paramName, string message)
			: base(message, paramName)
		{
			base.HResult = -2146233086;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class with the parameter name, the value of the argument, and a specified error message.</summary>
		/// <param name="paramName">The name of the parameter that caused the exception. </param>
		/// <param name="actualValue">The value of the argument that causes this exception. </param>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x060004A8 RID: 1192 RVA: 0x00018902 File Offset: 0x00016B02
		public ArgumentOutOfRangeException(string paramName, object actualValue, string message)
			: base(message, paramName)
		{
			this._actualValue = actualValue;
			base.HResult = -2146233086;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentOutOfRangeException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">An object that describes the source or destination of the serialized data. </param>
		// Token: 0x060004A9 RID: 1193 RVA: 0x0001891E File Offset: 0x00016B1E
		protected ArgumentOutOfRangeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._actualValue = info.GetValue("ActualValue", typeof(object));
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the invalid argument value and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">An object that describes the source or destination of the serialized data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> object is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060004AA RID: 1194 RVA: 0x00018943 File Offset: 0x00016B43
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ActualValue", this._actualValue, typeof(object));
		}

		/// <summary>Gets the error message and the string representation of the invalid argument value, or only the error message if the argument value is null.</summary>
		/// <returns>The text message for this exception. The value of this property takes one of two forms, as follows.Condition Value The <paramref name="actualValue" /> is null. The <paramref name="message" /> string passed to the constructor. The <paramref name="actualValue" /> is not null. The <paramref name="message" /> string appended with the string representation of the invalid argument value. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00018968 File Offset: 0x00016B68
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (this._actualValue == null)
				{
					return message;
				}
				string text = SR.Format("Actual value was {0}.", this._actualValue.ToString());
				if (message == null)
				{
					return text;
				}
				return message + Environment.NewLine + text;
			}
		}

		// Token: 0x040002B8 RID: 696
		private object _actualValue;
	}
}
