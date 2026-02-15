using System;

namespace System
{
	/// <summary>Specifies the usage of another attribute class. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000C7 RID: 199
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	[Serializable]
	public sealed class AttributeUsageAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AttributeUsageAttribute" /> class with the specified list of <see cref="T:System.AttributeTargets" />, the <see cref="P:System.AttributeUsageAttribute.AllowMultiple" /> value, and the <see cref="P:System.AttributeUsageAttribute.Inherited" /> value.</summary>
		/// <param name="validOn">The set of values combined using a bitwise OR operation to indicate which program elements are valid. </param>
		// Token: 0x060004D9 RID: 1241 RVA: 0x00018CF2 File Offset: 0x00016EF2
		public AttributeUsageAttribute(AttributeTargets validOn)
		{
			this._attributeTarget = validOn;
		}

		/// <summary>Gets or sets a Boolean value indicating whether more than one instance of the indicated attribute can be specified for a single program element.</summary>
		/// <returns>true if more than one instance is allowed to be specified; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00018D13 File Offset: 0x00016F13
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x00018D1B File Offset: 0x00016F1B
		public bool AllowMultiple
		{
			get
			{
				return this._allowMultiple;
			}
			set
			{
				this._allowMultiple = value;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether the indicated attribute can be inherited by derived classes and overriding members.</summary>
		/// <returns>true if the attribute can be inherited by derived classes and overriding members; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00018D24 File Offset: 0x00016F24
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x00018D2C File Offset: 0x00016F2C
		public bool Inherited
		{
			get
			{
				return this._inherited;
			}
			set
			{
				this._inherited = value;
			}
		}

		// Token: 0x040002D3 RID: 723
		private AttributeTargets _attributeTarget = AttributeTargets.All;

		// Token: 0x040002D4 RID: 724
		private bool _allowMultiple;

		// Token: 0x040002D5 RID: 725
		private bool _inherited = true;

		// Token: 0x040002D6 RID: 726
		internal static AttributeUsageAttribute Default = new AttributeUsageAttribute(AttributeTargets.All);
	}
}
