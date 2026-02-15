using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Security.Claims
{
	/// <summary>Represents a claim.</summary>
	// Token: 0x020003DF RID: 991
	[Serializable]
	public class Claim
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified claim type, value, value type, issuer, original issuer and subject.</summary>
		/// <param name="type">The claim type.</param>
		/// <param name="value">The claim value.</param>
		/// <param name="valueType">The claim value type. If this parameter is null, then <see cref="F:System.Security.Claims.ClaimValueTypes.String" /> is used.</param>
		/// <param name="issuer">The claim issuer. If this parameter is empty or null, then <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultIssuer" /> is used.</param>
		/// <param name="originalIssuer">The original issuer of the claim. If this parameter is empty or null, then the <see cref="P:System.Security.Claims.Claim.OriginalIssuer" /> property is set to the value of the <see cref="P:System.Security.Claims.Claim.Issuer" /> property.</param>
		/// <param name="subject">The subject that this claim describes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="value" /> is null.</exception>
		// Token: 0x060021A4 RID: 8612 RVA: 0x0008BEC8 File Offset: 0x0008A0C8
		public Claim(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity subject)
			: this(type, value, valueType, issuer, originalIssuer, subject, null, null)
		{
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0008BEE8 File Offset: 0x0008A0E8
		internal Claim(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity subject, string propertyKey, string propertyValue)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_type = type;
			this.m_value = value;
			if (string.IsNullOrEmpty(valueType))
			{
				this.m_valueType = "http://www.w3.org/2001/XMLSchema#string";
			}
			else
			{
				this.m_valueType = valueType;
			}
			if (string.IsNullOrEmpty(issuer))
			{
				this.m_issuer = "LOCAL AUTHORITY";
			}
			else
			{
				this.m_issuer = issuer;
			}
			if (string.IsNullOrEmpty(originalIssuer))
			{
				this.m_originalIssuer = this.m_issuer;
			}
			else
			{
				this.m_originalIssuer = originalIssuer;
			}
			this.m_subject = subject;
			if (propertyKey != null)
			{
				this.Properties.Add(propertyKey, propertyValue);
			}
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x0008BFA4 File Offset: 0x0008A1A4
		protected Claim(Claim other, ClaimsIdentity subject)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.m_issuer = other.m_issuer;
			this.m_originalIssuer = other.m_originalIssuer;
			this.m_subject = subject;
			this.m_type = other.m_type;
			this.m_value = other.m_value;
			this.m_valueType = other.m_valueType;
			if (other.m_properties != null)
			{
				this.m_properties = new Dictionary<string, string>();
				foreach (string text in other.m_properties.Keys)
				{
					this.m_properties.Add(text, other.m_properties[text]);
				}
			}
			if (other.m_userSerializationData != null)
			{
				this.m_userSerializationData = other.m_userSerializationData.Clone() as byte[];
			}
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x0008C0A0 File Offset: 0x0008A2A0
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			this.m_propertyLock = new object();
		}

		/// <summary>Gets a dictionary that contains additional properties associated with this claim.</summary>
		/// <returns>A dictionary that contains additional properties associated with the claim. The properties are represented as name-value pairs.</returns>
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060021A8 RID: 8616 RVA: 0x0008C0B0 File Offset: 0x0008A2B0
		public IDictionary<string, string> Properties
		{
			get
			{
				if (this.m_properties == null)
				{
					object propertyLock = this.m_propertyLock;
					lock (propertyLock)
					{
						if (this.m_properties == null)
						{
							this.m_properties = new Dictionary<string, string>();
						}
					}
				}
				return this.m_properties;
			}
		}

		/// <summary>Gets the subject of the claim.</summary>
		/// <returns>The subject of the claim.</returns>
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x0008C10C File Offset: 0x0008A30C
		// (set) Token: 0x060021AA RID: 8618 RVA: 0x0008C114 File Offset: 0x0008A314
		public ClaimsIdentity Subject
		{
			get
			{
				return this.m_subject;
			}
			internal set
			{
				this.m_subject = value;
			}
		}

		/// <summary>Gets the claim type of the claim.</summary>
		/// <returns>The claim type.</returns>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x0008C11D File Offset: 0x0008A31D
		public string Type
		{
			get
			{
				return this.m_type;
			}
		}

		/// <summary>Gets the value of the claim.</summary>
		/// <returns>The claim value.</returns>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x0008C125 File Offset: 0x0008A325
		public string Value
		{
			get
			{
				return this.m_value;
			}
		}

		/// <summary>Returns a new <see cref="T:System.Security.Claims.Claim" /> object copied from this object. The subject of the new claim is set to the specified ClaimsIdentity.</summary>
		/// <returns>The new claim object.</returns>
		/// <param name="identity">The intended subject of the new claim.</param>
		// Token: 0x060021AD RID: 8621 RVA: 0x0008C12D File Offset: 0x0008A32D
		public virtual Claim Clone(ClaimsIdentity identity)
		{
			return new Claim(this, identity);
		}

		/// <summary>Returns a string representation of this <see cref="T:System.Security.Claims.Claim" /> object.</summary>
		/// <returns>The string representation of this <see cref="T:System.Security.Claims.Claim" /> object.</returns>
		// Token: 0x060021AE RID: 8622 RVA: 0x0008C136 File Offset: 0x0008A336
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: {1}", this.m_type, this.m_value);
		}

		// Token: 0x0400101C RID: 4124
		private string m_issuer;

		// Token: 0x0400101D RID: 4125
		private string m_originalIssuer;

		// Token: 0x0400101E RID: 4126
		private string m_type;

		// Token: 0x0400101F RID: 4127
		private string m_value;

		// Token: 0x04001020 RID: 4128
		private string m_valueType;

		// Token: 0x04001021 RID: 4129
		[NonSerialized]
		private byte[] m_userSerializationData;

		// Token: 0x04001022 RID: 4130
		private Dictionary<string, string> m_properties;

		// Token: 0x04001023 RID: 4131
		[NonSerialized]
		private object m_propertyLock;

		// Token: 0x04001024 RID: 4132
		[NonSerialized]
		private ClaimsIdentity m_subject;
	}
}
