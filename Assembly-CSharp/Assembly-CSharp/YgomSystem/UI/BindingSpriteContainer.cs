using System;
using UnityEngine;
using YgomGame.Menu.Common;

namespace YgomSystem.UI
{
	// Token: 0x0200056D RID: 1389
	public class BindingSpriteContainer : Binding, IAsyncProgressContent
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06002C43 RID: 11331 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002C44 RID: 11332 RVA: 0x0000216D File Offset: 0x0000036D
		[SerializeField]
		public string SpriteContainerPath
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002C46 RID: 11334 RVA: 0x0000216D File Offset: 0x0000036D
		[SerializeField]
		public string SpriteLabel
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnRebind()
		{
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBinding()
		{
			return false;
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x04002AA9 RID: 10921
		[SerializeField]
		private string spriteContainerPath;

		// Token: 0x04002AAA RID: 10922
		[SerializeField]
		private string spriteLabel;

		// Token: 0x04002AAB RID: 10923
		[SerializeField]
		public bool immediate;

		// Token: 0x04002AAC RID: 10924
		[SerializeField]
		public bool showloading;

		// Token: 0x04002AAD RID: 10925
		private uint crc;
	}
}
