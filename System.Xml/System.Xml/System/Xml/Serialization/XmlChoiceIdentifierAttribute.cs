using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the member can be further detected by using an enumeration.</summary>
	// Token: 0x020001AA RID: 426
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false)]
	public class XmlChoiceIdentifierAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlChoiceIdentifierAttribute" /> class.</summary>
		// Token: 0x06001417 RID: 5143 RVA: 0x0005995F File Offset: 0x00057B5F
		public XmlChoiceIdentifierAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlChoiceIdentifierAttribute" /> class.</summary>
		/// <param name="name">The member name that returns the enumeration used to detect a choice. </param>
		// Token: 0x06001418 RID: 5144 RVA: 0x000614A5 File Offset: 0x0005F6A5
		public XmlChoiceIdentifierAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets or sets the name of the field that returns the enumeration to use when detecting types.</summary>
		/// <returns>The name of a field that returns an enumeration.</returns>
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x000614B4 File Offset: 0x0005F6B4
		// (set) Token: 0x0600141A RID: 5146 RVA: 0x000614CA File Offset: 0x0005F6CA
		public string MemberName
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x000614D3 File Offset: 0x0005F6D3
		// (set) Token: 0x0600141C RID: 5148 RVA: 0x000614DB File Offset: 0x0005F6DB
		internal MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
			set
			{
				this.memberInfo = value;
			}
		}

		// Token: 0x04000965 RID: 2405
		private string name;

		// Token: 0x04000966 RID: 2406
		private MemberInfo memberInfo;
	}
}
