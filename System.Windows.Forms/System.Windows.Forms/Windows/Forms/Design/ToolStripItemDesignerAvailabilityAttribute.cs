using System;

namespace System.Windows.Forms.Design
{
	/// <summary>Specifies which types a <see cref="T:System.Windows.Forms.ToolStripItem" /> can appear in. This class cannot be inherited.</summary>
	// Token: 0x02000396 RID: 918
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ToolStripItemDesignerAvailabilityAttribute : Attribute
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailabilityAttribute" /> class. </summary>
		// Token: 0x06001DB1 RID: 7601 RVA: 0x00093A6C File Offset: 0x00091C6C
		public ToolStripItemDesignerAvailabilityAttribute()
		{
			this.visibility = ToolStripItemDesignerAvailability.None;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailabilityAttribute" /> class with the specified visibility. </summary>
		/// <param name="visibility">A <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailability" /> value specifying the visibility.</param>
		// Token: 0x06001DB2 RID: 7602 RVA: 0x00093A7B File Offset: 0x00091C7B
		public ToolStripItemDesignerAvailabilityAttribute(ToolStripItemDesignerAvailability visibility)
		{
			this.visibility = visibility;
		}

		/// <summary>Gets the visibility of a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailability" /> representing the visibility.</returns>
		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x00093A8A File Offset: 0x00091C8A
		public ToolStripItemDesignerAvailability ItemAdditionVisibility
		{
			get
			{
				return this.visibility;
			}
		}

		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x06001DB4 RID: 7604 RVA: 0x00093A92 File Offset: 0x00091C92
		public override bool Equals(object obj)
		{
			return obj is ToolStripItemDesignerAvailabilityAttribute && this.ItemAdditionVisibility == (obj as ToolStripItemDesignerAvailabilityAttribute).ItemAdditionVisibility;
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001DB5 RID: 7605 RVA: 0x00093A8A File Offset: 0x00091C8A
		public override int GetHashCode()
		{
			return (int)this.visibility;
		}

		/// <summary>When overriden in a derived class, indicates whether the value of this instance is the default value for the derived class.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x06001DB6 RID: 7606 RVA: 0x00093AB1 File Offset: 0x00091CB1
		public override bool IsDefaultAttribute()
		{
			return this.visibility == ToolStripItemDesignerAvailability.None;
		}

		// Token: 0x04001CE0 RID: 7392
		private ToolStripItemDesignerAvailability visibility;

		/// <summary>Specifies the default value of the <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailabilityAttribute" />. This field is read-only.</summary>
		// Token: 0x04001CE1 RID: 7393
		public static readonly ToolStripItemDesignerAvailabilityAttribute Default = new ToolStripItemDesignerAvailabilityAttribute();
	}
}
