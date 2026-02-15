using System;

namespace System.Windows.Forms
{
	/// <summary>Contains information that enables a <see cref="T:System.Windows.Forms.Binding" /> to resolve a data binding to either the property of an object or the property of the current object in a list of objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000021 RID: 33
	public struct BindingMemberInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingMemberInfo" /> class.</summary>
		/// <param name="dataMember">A navigation path that resolves to either the property of an object or the property of the current object in a list of objects. </param>
		// Token: 0x060000A9 RID: 169 RVA: 0x00003E70 File Offset: 0x00002070
		public BindingMemberInfo(string dataMember)
		{
			if (dataMember != null)
			{
				this.data_member = dataMember;
			}
			else
			{
				this.data_member = string.Empty;
			}
			int num = this.data_member.LastIndexOf('.');
			if (num != -1)
			{
				this.data_field = this.data_member.Substring(num + 1);
				this.data_path = this.data_member.Substring(0, num);
				return;
			}
			this.data_field = this.data_member;
			this.data_path = string.Empty;
		}

		/// <summary>Gets the property name of the data-bound object.</summary>
		/// <returns>The property name of the data-bound object. This can be an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003EE5 File Offset: 0x000020E5
		public string BindingField
		{
			get
			{
				return this.data_field;
			}
		}

		/// <summary>Gets the property name, or the period-delimited hierarchy of property names, that comes before the property name of the data-bound object.</summary>
		/// <returns>The property name, or the period-delimited hierarchy of property names, that comes before the data-bound object property name.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003EED File Offset: 0x000020ED
		public string BindingPath
		{
			get
			{
				return this.data_path;
			}
		}

		/// <summary>Determines whether the specified object is equal to this <see cref="T:System.Windows.Forms.BindingMemberInfo" />.</summary>
		/// <returns>true if <paramref name="otherObject" /> is a <see cref="T:System.Windows.Forms.BindingMemberInfo" /> and both <see cref="P:System.Windows.Forms.BindingMemberInfo.BindingMember" /> strings are equal; otherwise false.</returns>
		/// <param name="otherObject">The object to compare for equality.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060000AC RID: 172 RVA: 0x00003EF8 File Offset: 0x000020F8
		public override bool Equals(object otherObject)
		{
			return otherObject is BindingMemberInfo && (this.data_field == ((BindingMemberInfo)otherObject).data_field && this.data_path == ((BindingMemberInfo)otherObject).data_path) && this.data_member == ((BindingMemberInfo)otherObject).data_member;
		}

		/// <summary>Returns the hash code for this <see cref="T:System.Windows.Forms.BindingMemberInfo" />.</summary>
		/// <returns>The hash code for this <see cref="T:System.Windows.Forms.BindingMemberInfo" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060000AD RID: 173 RVA: 0x00003F57 File Offset: 0x00002157
		public override int GetHashCode()
		{
			return this.data_member.GetHashCode();
		}

		// Token: 0x040000C4 RID: 196
		private string data_member;

		// Token: 0x040000C5 RID: 197
		private string data_field;

		// Token: 0x040000C6 RID: 198
		private string data_path;
	}
}
