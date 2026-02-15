using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a member is typically used for binding. This class cannot be inherited.</summary>
	// Token: 0x02000259 RID: 601
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class with a Boolean value.</summary>
		/// <param name="bindable">true to use property for binding; otherwise, false.</param>
		// Token: 0x06000E55 RID: 3669 RVA: 0x0003F23D File Offset: 0x0003D43D
		public BindableAttribute(bool bindable)
			: this(bindable, BindingDirection.OneWay)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <param name="bindable">true to use property for binding; otherwise, false.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.BindingDirection" /> values.</param>
		// Token: 0x06000E56 RID: 3670 RVA: 0x0003F247 File Offset: 0x0003D447
		public BindableAttribute(bool bindable, BindingDirection direction)
		{
			this.Bindable = bindable;
			this.<Direction>k__BackingField = direction;
		}

		/// <summary>Gets a value indicating that a property is typically used for binding.</summary>
		/// <returns>true if the property is typically used for binding; otherwise, false.</returns>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0003F25D File Offset: 0x0003D45D
		public bool Bindable { get; }

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.BindableAttribute" /> objects are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.BindableAttribute" /> is equal to the current <see cref="T:System.ComponentModel.BindableAttribute" />; false if it is not equal.</returns>
		/// <param name="obj">The object to compare.</param>
		// Token: 0x06000E58 RID: 3672 RVA: 0x0003F265 File Offset: 0x0003D465
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is BindableAttribute && ((BindableAttribute)obj).Bindable == this.Bindable);
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.BindableAttribute" />.</returns>
		// Token: 0x06000E59 RID: 3673 RVA: 0x0003F290 File Offset: 0x0003D490
		public override int GetHashCode()
		{
			return this.Bindable.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06000E5A RID: 3674 RVA: 0x0003F2AB File Offset: 0x0003D4AB
		public override bool IsDefaultAttribute()
		{
			return this.Equals(BindableAttribute.Default) || this._isDefault;
		}

		/// <summary>Specifies that a property is typically used for binding. This field is read-only.</summary>
		// Token: 0x040009C0 RID: 2496
		public static readonly BindableAttribute Yes = new BindableAttribute(true);

		/// <summary>Specifies that a property is not typically used for binding. This field is read-only.</summary>
		// Token: 0x040009C1 RID: 2497
		public static readonly BindableAttribute No = new BindableAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.BindableAttribute" />, which is <see cref="F:System.ComponentModel.BindableAttribute.No" />. This field is read-only.</summary>
		// Token: 0x040009C2 RID: 2498
		public static readonly BindableAttribute Default = BindableAttribute.No;

		// Token: 0x040009C3 RID: 2499
		private bool _isDefault;
	}
}
