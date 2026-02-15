using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Collects the characteristics associated with flow layouts.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000AC RID: 172
	[DefaultProperty("FlowDirection")]
	public class FlowLayoutSettings : LayoutSettings
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x0001BC89 File Offset: 0x00019E89
		internal FlowLayoutSettings()
			: this(null)
		{
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001BC92 File Offset: 0x00019E92
		internal FlowLayoutSettings(Control owner)
		{
			this.flow_breaks = new Dictionary<object, bool>();
			this.wrap_contents = true;
			this.flow_direction = FlowDirection.LeftToRight;
			this.owner = owner;
		}

		/// <summary>Gets or sets a value indicating the flow direction of consecutive controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FlowDirection" /> indicating the flow direction of consecutive controls in the container. The default is <see cref="F:System.Windows.Forms.FlowDirection.LeftToRight" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001BCBA File Offset: 0x00019EBA
		[DefaultValue(FlowDirection.LeftToRight)]
		public FlowDirection FlowDirection
		{
			get
			{
				return this.flow_direction;
			}
		}

		/// <summary>Gets or sets a value indicating whether the contents should be wrapped or clipped when they exceed the original boundaries of their container.</summary>
		/// <returns>true if the contents should be wrapped; otherwise, false if the contents should be clipped. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0001BCC2 File Offset: 0x00019EC2
		[DefaultValue(true)]
		public bool WrapContents
		{
			get
			{
				return this.wrap_contents;
			}
		}

		/// <summary>Returns a value that represents the flow break setting of the control.</summary>
		/// <returns>true if the flow break is set; otherwise, false.</returns>
		/// <param name="child">The child control.</param>
		// Token: 0x06000675 RID: 1653 RVA: 0x0001BCCC File Offset: 0x00019ECC
		public bool GetFlowBreak(object child)
		{
			bool flag;
			return this.flow_breaks.TryGetValue(child, out flag) && flag;
		}

		// Token: 0x0400043E RID: 1086
		private FlowDirection flow_direction;

		// Token: 0x0400043F RID: 1087
		private bool wrap_contents;

		// Token: 0x04000440 RID: 1088
		private Dictionary<object, bool> flow_breaks;

		// Token: 0x04000441 RID: 1089
		private Control owner;
	}
}
