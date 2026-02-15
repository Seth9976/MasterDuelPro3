using System;
using System.Runtime.Serialization;

namespace System.Globalization
{
	/// <summary>The exception thrown when a method is invoked which attempts to construct a culture that is not available on the machine.</summary>
	// Token: 0x0200068F RID: 1679
	[Serializable]
	public class CultureNotFoundException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with its message string set to a system-supplied message.</summary>
		// Token: 0x060034B2 RID: 13490 RVA: 0x000C97B6 File Offset: 0x000C79B6
		public CultureNotFoundException()
			: base(CultureNotFoundException.DefaultMessage)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message and the name of the parameter that is the cause this exception.</summary>
		/// <param name="paramName">The name of the parameter that is the cause of the current exception.</param>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x060034B3 RID: 13491 RVA: 0x000C97C3 File Offset: 0x000C79C3
		public CultureNotFoundException(string paramName, string message)
			: base(message, paramName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060034B4 RID: 13492 RVA: 0x000C97D0 File Offset: 0x000C79D0
		protected CultureNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._invalidCultureId = (int?)info.GetValue("InvalidCultureId", typeof(int?));
			this._invalidCultureName = (string)info.GetValue("InvalidCultureName", typeof(string));
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x060034B5 RID: 13493 RVA: 0x000C9828 File Offset: 0x000C7A28
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("InvalidCultureId", this._invalidCultureId, typeof(int?));
			info.AddValue("InvalidCultureName", this._invalidCultureName, typeof(string));
		}

		/// <summary>Gets the Culture ID that cannot be found.</summary>
		/// <returns>The invalid Culture ID.</returns>
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060034B6 RID: 13494 RVA: 0x000C9878 File Offset: 0x000C7A78
		public virtual int? InvalidCultureId
		{
			get
			{
				return this._invalidCultureId;
			}
		}

		/// <summary>Gets the Culture Name that cannot be found.</summary>
		/// <returns>The invalid Culture Name.</returns>
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x060034B7 RID: 13495 RVA: 0x000C9880 File Offset: 0x000C7A80
		public virtual string InvalidCultureName
		{
			get
			{
				return this._invalidCultureName;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x060034B8 RID: 13496 RVA: 0x000C9888 File Offset: 0x000C7A88
		private static string DefaultMessage
		{
			get
			{
				return "Culture is not supported.";
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x060034B9 RID: 13497 RVA: 0x000C9890 File Offset: 0x000C7A90
		private string FormatedInvalidCultureId
		{
			get
			{
				if (this.InvalidCultureId == null)
				{
					return this.InvalidCultureName;
				}
				return string.Format(CultureInfo.InvariantCulture, "{0} (0x{0:x4})", this.InvalidCultureId.Value);
			}
		}

		/// <summary>Gets the error message that explains the reason for the exception.</summary>
		/// <returns>A text string describing the details of the exception.</returns>
		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x060034BA RID: 13498 RVA: 0x000C98D8 File Offset: 0x000C7AD8
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (this._invalidCultureId == null && this._invalidCultureName == null)
				{
					return message;
				}
				string text = SR.Format("{0} is an invalid culture identifier.", this.FormatedInvalidCultureId);
				if (message == null)
				{
					return text;
				}
				return message + Environment.NewLine + text;
			}
		}

		// Token: 0x04001B8E RID: 7054
		private string _invalidCultureName;

		// Token: 0x04001B8F RID: 7055
		private int? _invalidCultureId;
	}
}
