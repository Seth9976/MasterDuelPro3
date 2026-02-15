using System;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x02000411 RID: 1041
	[VisibleToOtherModules]
	internal struct SpriteChannelInfo
	{
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x0003D000 File Offset: 0x0003B200
		public unsafe void* buffer
		{
			get
			{
				return (void*)this.m_Buffer;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x0003D020 File Offset: 0x0003B220
		public int count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001B8F RID: 7055 RVA: 0x0003D038 File Offset: 0x0003B238
		public int offset
		{
			get
			{
				return this.m_Offset;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x0003D050 File Offset: 0x0003B250
		public int stride
		{
			get
			{
				return this.m_Stride;
			}
		}

		// Token: 0x04000E8B RID: 3723
		[NativeName("buffer")]
		private IntPtr m_Buffer;

		// Token: 0x04000E8C RID: 3724
		[NativeName("count")]
		private int m_Count;

		// Token: 0x04000E8D RID: 3725
		[NativeName("offset")]
		private int m_Offset;

		// Token: 0x04000E8E RID: 3726
		[NativeName("stride")]
		private int m_Stride;
	}
}
