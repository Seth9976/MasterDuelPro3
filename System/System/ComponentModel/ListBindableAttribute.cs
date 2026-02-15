using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that a list can be used as a data source. A visual designer should use this attribute to determine whether to display a particular list in a data-binding picker. This class cannot be inherited.</summary>
	// Token: 0x02000287 RID: 647
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ListBindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListBindableAttribute" /> class using a value to indicate whether the list is bindable.</summary>
		/// <param name="listBindable">true if the list is bindable; otherwise, false. </param>
		// Token: 0x06000F5C RID: 3932 RVA: 0x00041FBD File Offset: 0x000401BD
		public ListBindableAttribute(bool listBindable)
		{
			this.ListBindable = listBindable;
		}

		/// <summary>Gets whether the list is bindable.</summary>
		/// <returns>true if the list is bindable; otherwise, false.</returns>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x00041FCC File Offset: 0x000401CC
		public bool ListBindable { get; }

		/// <summary>Returns whether the object passed is equal to this <see cref="T:System.ComponentModel.ListBindableAttribute" />.</summary>
		/// <returns>true if the object passed is equal to this <see cref="T:System.ComponentModel.ListBindableAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The object to test equality with. </param>
		// Token: 0x06000F5E RID: 3934 RVA: 0x00041FD4 File Offset: 0x000401D4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ListBindableAttribute listBindableAttribute = obj as ListBindableAttribute;
			return listBindableAttribute != null && listBindableAttribute.ListBindable == this.ListBindable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ListBindableAttribute" />.</returns>
		// Token: 0x06000F5F RID: 3935 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns whether <see cref="P:System.ComponentModel.ListBindableAttribute.ListBindable" /> is set to the default value.</summary>
		/// <returns>true if <see cref="P:System.ComponentModel.ListBindableAttribute.ListBindable" /> is set to the default value; otherwise, false.</returns>
		// Token: 0x06000F60 RID: 3936 RVA: 0x00042001 File Offset: 0x00040201
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ListBindableAttribute.Default) || this._isDefault;
		}

		/// <summary>Specifies that the list is bindable. This static field is read-only.</summary>
		// Token: 0x04000A02 RID: 2562
		public static readonly ListBindableAttribute Yes = new ListBindableAttribute(true);

		/// <summary>Specifies that the list is not bindable. This static field is read-only.</summary>
		// Token: 0x04000A03 RID: 2563
		public static readonly ListBindableAttribute No = new ListBindableAttribute(false);

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.ListBindableAttribute" />.</summary>
		// Token: 0x04000A04 RID: 2564
		public static readonly ListBindableAttribute Default = ListBindableAttribute.Yes;

		// Token: 0x04000A05 RID: 2565
		private bool _isDefault;
	}
}
