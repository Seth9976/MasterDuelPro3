using System;

namespace System.ComponentModel
{
	/// <summary>Indicates that the parent property is notified when the value of the property that this attribute is applied to is modified. This class cannot be inherited.</summary>
	// Token: 0x020002D7 RID: 727
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class NotifyParentPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NotifyParentPropertyAttribute" /> class, using the specified value to determine whether the parent property is notified of changes to the value of the property.</summary>
		/// <param name="notifyParent">true if the parent should be notified of changes; otherwise, false. </param>
		// Token: 0x060011C2 RID: 4546 RVA: 0x000515C2 File Offset: 0x0004F7C2
		public NotifyParentPropertyAttribute(bool notifyParent)
		{
			this.notifyParent = notifyParent;
		}

		/// <summary>Gets or sets a value indicating whether the parent property should be notified of changes to the value of the property.</summary>
		/// <returns>true if the parent property should be notified of changes; otherwise, false.</returns>
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x000515D1 File Offset: 0x0004F7D1
		public bool NotifyParent
		{
			get
			{
				return this.notifyParent;
			}
		}

		/// <summary>Gets a value indicating whether the specified object is the same as the current object.</summary>
		/// <returns>true if the object is the same as this object; otherwise, false.</returns>
		/// <param name="obj">The object to test for equality. </param>
		// Token: 0x060011C4 RID: 4548 RVA: 0x000515D9 File Offset: 0x0004F7D9
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is NotifyParentPropertyAttribute && ((NotifyParentPropertyAttribute)obj).NotifyParent == this.notifyParent);
		}

		/// <summary>Gets the hash code for this object.</summary>
		/// <returns>The hash code for the object the attribute belongs to.</returns>
		// Token: 0x060011C5 RID: 4549 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default value of the attribute; otherwise, false.</returns>
		// Token: 0x060011C6 RID: 4550 RVA: 0x00051601 File Offset: 0x0004F801
		public override bool IsDefaultAttribute()
		{
			return this.Equals(NotifyParentPropertyAttribute.Default);
		}

		/// <summary>Indicates that the parent property is notified of changes to the value of the property. This field is read-only.</summary>
		// Token: 0x04000AD6 RID: 2774
		public static readonly NotifyParentPropertyAttribute Yes = new NotifyParentPropertyAttribute(true);

		/// <summary>Indicates that the parent property is not be notified of changes to the value of the property. This field is read-only.</summary>
		// Token: 0x04000AD7 RID: 2775
		public static readonly NotifyParentPropertyAttribute No = new NotifyParentPropertyAttribute(false);

		/// <summary>Indicates the default attribute state, that the property should not notify the parent property of changes to its value. This field is read-only.</summary>
		// Token: 0x04000AD8 RID: 2776
		public static readonly NotifyParentPropertyAttribute Default = NotifyParentPropertyAttribute.No;

		// Token: 0x04000AD9 RID: 2777
		private bool notifyParent;
	}
}
