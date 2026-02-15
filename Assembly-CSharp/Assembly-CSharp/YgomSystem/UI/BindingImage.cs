using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000569 RID: 1385
	public class BindingImage : Binding
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06002C1E RID: 11294 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002C1F RID: 11295 RVA: 0x0000216D File Offset: 0x0000036D
		[SerializeField]
		public string SpritePath
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06002C20 RID: 11296 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002C21 RID: 11297 RVA: 0x0000216D File Offset: 0x0000036D
		public string MaterialPath
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Start()
		{
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnRebind()
		{
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBinding()
		{
			return false;
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnBindingMaterial()
		{
			return false;
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMaterial(Material material)
		{
		}

		// Token: 0x04002A92 RID: 10898
		[SerializeField]
		private string spritePath;

		// Token: 0x04002A93 RID: 10899
		[SerializeField]
		public bool immediate;

		// Token: 0x04002A94 RID: 10900
		[SerializeField]
		public bool showloading;

		// Token: 0x04002A95 RID: 10901
		private string materialPath;

		// Token: 0x04002A96 RID: 10902
		private uint crc;

		// Token: 0x04002A97 RID: 10903
		private uint usingCrc;

		// Token: 0x04002A98 RID: 10904
		private string loadPath;

		// Token: 0x04002A99 RID: 10905
		private uint materialCrc;

		// Token: 0x04002A9A RID: 10906
		private uint materialUsingCrc;

		// Token: 0x04002A9B RID: 10907
		private bool isDoneMaterial;

		// Token: 0x04002A9C RID: 10908
		private string assetContainerLabel;

		// Token: 0x04002A9D RID: 10909
		private Type assetType;
	}
}
