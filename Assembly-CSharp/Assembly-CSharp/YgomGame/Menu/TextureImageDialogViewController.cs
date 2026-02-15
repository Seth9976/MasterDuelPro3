using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000AF8 RID: 2808
	public class TextureImageDialogViewController : TweenViewController, IBokeSupported
	{
		// Token: 0x060051AA RID: 20906 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Push(Dictionary<string, object> args)
		{
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PushProtector(int sid, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x0000216D File Offset: 0x0000036D
		public override void TransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnPushed()
		{
		}

		// Token: 0x0400900B RID: 36875
		public const string ARGKEY_TEXTURE = "texture";

		// Token: 0x0400900C RID: 36876
		public const string ARGKEY_WIDTH = "width";

		// Token: 0x0400900D RID: 36877
		public const string ARGKEY_HEIGHT = "height";

		// Token: 0x0400900E RID: 36878
		public const string ARGKEY_ANIM_ROT = "rotateAnim";

		// Token: 0x0400900F RID: 36879
		public const string ARGKEY_ANIM_POS = "positionAnim";

		// Token: 0x04009010 RID: 36880
		public const string ARGKEY_BGCOLOR = "bgcolor";

		// Token: 0x04009011 RID: 36881
		public const string ARGKEY_VOFS = "vofs";

		// Token: 0x04009012 RID: 36882
		public const string ARGKEY_CAPTION = "caption";

		// Token: 0x04009013 RID: 36883
		[SerializeField]
		private MDText caption;

		// Token: 0x04009014 RID: 36884
		private const string PREFAB_NAME = "TextureImageDialog";

		// Token: 0x04009015 RID: 36885
		public RawImage rawImage;
	}
}
