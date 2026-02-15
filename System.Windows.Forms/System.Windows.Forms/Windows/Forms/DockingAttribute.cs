using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the default docking behavior for a control.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200006C RID: 108
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DockingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DockingAttribute" /> class. </summary>
		// Token: 0x060004F2 RID: 1266 RVA: 0x00013515 File Offset: 0x00011715
		public DockingAttribute()
		{
			this.dockingBehavior = DockingBehavior.Never;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DockingAttribute" /> class with the given docking behavior. </summary>
		/// <param name="dockingBehavior">A <see cref="T:System.Windows.Forms.DockingBehavior" /> value specifying the default behavior.</param>
		// Token: 0x060004F3 RID: 1267 RVA: 0x00013524 File Offset: 0x00011724
		public DockingAttribute(DockingBehavior dockingBehavior)
		{
			this.dockingBehavior = dockingBehavior;
		}

		/// <summary>Gets the docking behavior supplied to this attribute.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DockingBehavior" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00013533 File Offset: 0x00011733
		public DockingBehavior DockingBehavior
		{
			get
			{
				return this.dockingBehavior;
			}
		}

		/// <summary>Compares an arbitrary object with the <see cref="T:System.Windows.Forms.DockingAttribute" /> object for equality.</summary>
		/// <returns>true is <paramref name="obj" /> is equal to this <see cref="T:System.Windows.Forms.DockingAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> against which to compare this <see cref="T:System.Windows.Forms.DockingAttribute" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004F5 RID: 1269 RVA: 0x0001353B File Offset: 0x0001173B
		public override bool Equals(object obj)
		{
			return obj is DockingAttribute && this.dockingBehavior == ((DockingAttribute)obj).DockingBehavior;
		}

		/// <summary>The hash code for this object.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing an in-memory hash of this object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004F6 RID: 1270 RVA: 0x0001355A File Offset: 0x0001175A
		public override int GetHashCode()
		{
			return this.dockingBehavior.GetHashCode();
		}

		/// <summary>Specifies whether this <see cref="T:System.Windows.Forms.DockingAttribute" /> is the default docking attribute.</summary>
		/// <returns>true is the current <see cref="T:System.Windows.Forms.DockingAttribute" /> is the default; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004F7 RID: 1271 RVA: 0x0001356D File Offset: 0x0001176D
		public override bool IsDefaultAttribute()
		{
			return DockingAttribute.Default.Equals(this);
		}

		// Token: 0x040002D4 RID: 724
		private DockingBehavior dockingBehavior;

		/// <summary>The default <see cref="T:System.Windows.Forms.DockingAttribute" /> for this control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040002D5 RID: 725
		public static readonly DockingAttribute Default = new DockingAttribute();
	}
}
