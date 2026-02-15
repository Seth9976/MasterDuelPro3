using System;

namespace System.ComponentModel
{
	/// <summary>
	///   <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> marks a component's visibility. If <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Yes" /> is present, a visual designer can show this component on a designer.</summary>
	// Token: 0x02000271 RID: 625
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public sealed class DesignTimeVisibleAttribute : Attribute
	{
		/// <summary>Creates a new <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> with the <see cref="P:System.ComponentModel.DesignTimeVisibleAttribute.Visible" /> property set to the given value in <paramref name="visible" />.</summary>
		/// <param name="visible">The value that the <see cref="P:System.ComponentModel.DesignTimeVisibleAttribute.Visible" /> property will be set against. </param>
		// Token: 0x06000EC7 RID: 3783 RVA: 0x000413E8 File Offset: 0x0003F5E8
		public DesignTimeVisibleAttribute(bool visible)
		{
			this.Visible = visible;
		}

		/// <summary>Gets or sets whether the component should be shown at design time.</summary>
		/// <returns>true if this component should be shown at design time, or false if it shouldn't.</returns>
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x000413F7 File Offset: 0x0003F5F7
		public bool Visible { get; }

		/// <param name="obj">The object to compare.</param>
		// Token: 0x06000EC9 RID: 3785 RVA: 0x00041400 File Offset: 0x0003F600
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignTimeVisibleAttribute designTimeVisibleAttribute = obj as DesignTimeVisibleAttribute;
			return designTimeVisibleAttribute != null && designTimeVisibleAttribute.Visible == this.Visible;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0004142D File Offset: 0x0003F62D
		public override int GetHashCode()
		{
			return typeof(DesignTimeVisibleAttribute).GetHashCode() ^ (this.Visible ? (-1) : 0);
		}

		/// <summary>Gets a value indicating if this instance is equal to the <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Default" /> value.</summary>
		/// <returns>true, if this instance is equal to the <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Default" /> value; otherwise, false.</returns>
		// Token: 0x06000ECB RID: 3787 RVA: 0x0004144B File Offset: 0x0003F64B
		public override bool IsDefaultAttribute()
		{
			return this.Visible == DesignTimeVisibleAttribute.Default.Visible;
		}

		/// <summary>Marks a component as visible in a visual designer.</summary>
		// Token: 0x040009EB RID: 2539
		public static readonly DesignTimeVisibleAttribute Yes = new DesignTimeVisibleAttribute(true);

		/// <summary>Marks a component as not visible in a visual designer.</summary>
		// Token: 0x040009EC RID: 2540
		public static readonly DesignTimeVisibleAttribute No = new DesignTimeVisibleAttribute(false);

		/// <summary>The default visibility which is Yes.</summary>
		// Token: 0x040009ED RID: 2541
		public static readonly DesignTimeVisibleAttribute Default = DesignTimeVisibleAttribute.Yes;
	}
}
