using System;
using UnityEngine;

namespace Unity.Properties.Internal
{
	// Token: 0x02000078 RID: 120
	internal class Vector3PropertyBag : ContainerPropertyBag<Vector3>
	{
		// Token: 0x0600028B RID: 651 RVA: 0x0000A5A7 File Offset: 0x000087A7
		public Vector3PropertyBag()
		{
			base.AddProperty<float>(new Vector3PropertyBag.XProperty());
			base.AddProperty<float>(new Vector3PropertyBag.YProperty());
			base.AddProperty<float>(new Vector3PropertyBag.ZProperty());
		}

		// Token: 0x02000079 RID: 121
		private class XProperty : Property<Vector3, float>
		{
			// Token: 0x1700005D RID: 93
			// (get) Token: 0x0600028C RID: 652 RVA: 0x0000A56E File Offset: 0x0000876E
			public override string Name
			{
				get
				{
					return "x";
				}
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x0600028D RID: 653 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600028E RID: 654 RVA: 0x0000A5D5 File Offset: 0x000087D5
			public override float GetValue(ref Vector3 container)
			{
				return container.x;
			}

			// Token: 0x0600028F RID: 655 RVA: 0x0000A5DD File Offset: 0x000087DD
			public override void SetValue(ref Vector3 container, float value)
			{
				container.x = value;
			}
		}

		// Token: 0x0200007A RID: 122
		private class YProperty : Property<Vector3, float>
		{
			// Token: 0x1700005F RID: 95
			// (get) Token: 0x06000291 RID: 657 RVA: 0x0000A58F File Offset: 0x0000878F
			public override string Name
			{
				get
				{
					return "y";
				}
			}

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x06000292 RID: 658 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000293 RID: 659 RVA: 0x0000A5EF File Offset: 0x000087EF
			public override float GetValue(ref Vector3 container)
			{
				return container.y;
			}

			// Token: 0x06000294 RID: 660 RVA: 0x0000A5F7 File Offset: 0x000087F7
			public override void SetValue(ref Vector3 container, float value)
			{
				container.y = value;
			}
		}

		// Token: 0x0200007B RID: 123
		private class ZProperty : Property<Vector3, float>
		{
			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000296 RID: 662 RVA: 0x0000A600 File Offset: 0x00008800
			public override string Name
			{
				get
				{
					return "z";
				}
			}

			// Token: 0x17000062 RID: 98
			// (get) Token: 0x06000297 RID: 663 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000298 RID: 664 RVA: 0x0000A607 File Offset: 0x00008807
			public override float GetValue(ref Vector3 container)
			{
				return container.z;
			}

			// Token: 0x06000299 RID: 665 RVA: 0x0000A60F File Offset: 0x0000880F
			public override void SetValue(ref Vector3 container, float value)
			{
				container.z = value;
			}
		}
	}
}
