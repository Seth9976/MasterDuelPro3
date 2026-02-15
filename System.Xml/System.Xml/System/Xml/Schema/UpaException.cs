using System;

namespace System.Xml.Schema
{
	// Token: 0x0200021A RID: 538
	internal class UpaException : Exception
	{
		// Token: 0x06001A88 RID: 6792 RVA: 0x0009A360 File Offset: 0x00098560
		public UpaException(object particle1, object particle2)
		{
			this.particle1 = particle1;
			this.particle2 = particle2;
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x0009A376 File Offset: 0x00098576
		public object Particle1
		{
			get
			{
				return this.particle1;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x0009A37E File Offset: 0x0009857E
		public object Particle2
		{
			get
			{
				return this.particle2;
			}
		}

		// Token: 0x04000B5C RID: 2908
		private object particle1;

		// Token: 0x04000B5D RID: 2909
		private object particle2;
	}
}
