using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem
{
	// Token: 0x020004CD RID: 1229
	public class ShaderResourceCache
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06002754 RID: 10068 RVA: 0x0000216A File Offset: 0x0000036A
		public static ShaderResourceCache instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002756 RID: 10070 RVA: 0x0000216D File Offset: 0x0000036D
		public bool busy
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator InitializeImpl()
		{
			return null;
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x0000216A File Offset: 0x0000036A
		public Shader GetShader(string shaderLabel)
		{
			return null;
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x0000216A File Offset: 0x0000036A
		private Shader GetShaderImpl(string shaderLabel)
		{
			return null;
		}

		// Token: 0x0400283C RID: 10300
		public Material[] preloadMaterials;

		// Token: 0x0400283D RID: 10301
		private Dictionary<string, Shader> shaders;

		// Token: 0x0400283E RID: 10302
		public const string shaderSetResPath = "BundleMaterials/MaterialPack.Unity2018_4_2f1";

		// Token: 0x0400283F RID: 10303
		private static ShaderResourceCache m_instance;
	}
}
