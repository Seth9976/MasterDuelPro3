using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x02000092 RID: 146
	internal class BoundsPropertyBag : ContainerPropertyBag<Bounds>
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x0000A886 File Offset: 0x00008A86
		public BoundsPropertyBag()
		{
			base.AddProperty<Vector3>(new BoundsPropertyBag.CenterProperty());
			base.AddProperty<Vector3>(new BoundsPropertyBag.ExtentsProperty());
		}

		// Token: 0x02000093 RID: 147
		private class CenterProperty : Property<Bounds, Vector3>
		{
			// Token: 0x17000085 RID: 133
			// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000A8A8 File Offset: 0x00008AA8
			public override string Name
			{
				get
				{
					return "center";
				}
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002F8 RID: 760 RVA: 0x0000A8AF File Offset: 0x00008AAF
			public override Vector3 GetValue(ref Bounds container)
			{
				return container.center;
			}

			// Token: 0x060002F9 RID: 761 RVA: 0x0000A8B7 File Offset: 0x00008AB7
			public override void SetValue(ref Bounds container, Vector3 value)
			{
				container.center = value;
			}
		}

		// Token: 0x02000094 RID: 148
		private class ExtentsProperty : Property<Bounds, Vector3>
		{
			// Token: 0x17000087 RID: 135
			// (get) Token: 0x060002FB RID: 763 RVA: 0x0000A8CA File Offset: 0x00008ACA
			public override string Name
			{
				get
				{
					return "extents";
				}
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x060002FC RID: 764 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060002FD RID: 765 RVA: 0x0000A8D1 File Offset: 0x00008AD1
			public override Vector3 GetValue(ref Bounds container)
			{
				return container.extents;
			}

			// Token: 0x060002FE RID: 766 RVA: 0x0000A8D9 File Offset: 0x00008AD9
			public override void SetValue(ref Bounds container, Vector3 value)
			{
				container.extents = value;
			}
		}
	}
}
