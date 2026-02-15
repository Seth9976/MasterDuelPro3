using System;
using UnityEngine;

namespace YgomSystem.Effect
{
	// Token: 0x02000786 RID: 1926
	public static class ScreenEffectUtil
	{
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06003BD1 RID: 15313 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003BD2 RID: 15314 RVA: 0x0000216D File Offset: 0x0000036D
		public static Camera defaultCamera3DOnUI
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06003BD3 RID: 15315 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003BD4 RID: 15316 RVA: 0x0000216D File Offset: 0x0000036D
		public static Camera defaultCamera2DOnUI
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06003BD5 RID: 15317 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003BD6 RID: 15318 RVA: 0x0000216D File Offset: 0x0000036D
		public static Camera defaultCamera3D
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06003BD7 RID: 15319 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003BD8 RID: 15320 RVA: 0x0000216D File Offset: 0x0000036D
		public static Camera defaultCamera2D
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x040034AD RID: 13485
		public static int layer3DOnUI;

		// Token: 0x040034AE RID: 13486
		private static Camera _defaultCamera3DOnUI;

		// Token: 0x040034AF RID: 13487
		public static int layer2DOnUI;

		// Token: 0x040034B0 RID: 13488
		private static Camera _defaultCamera2DOnUI;

		// Token: 0x040034B1 RID: 13489
		public static int layer3D;

		// Token: 0x040034B2 RID: 13490
		private static Camera _defaultCamera3D;

		// Token: 0x040034B3 RID: 13491
		public static int layer2D;

		// Token: 0x040034B4 RID: 13492
		private static Camera _defaultCamera2D;
	}
}
