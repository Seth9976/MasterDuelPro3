using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.Effect
{
	// Token: 0x02000782 RID: 1922
	public class MaterialSetterSpriteUVMinMax : MonoBehaviour
	{
		// Token: 0x06003BBE RID: 15294 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TrySetUvMinMax()
		{
			return false;
		}

		// Token: 0x04003497 RID: 13463
		[SerializeField]
		private string m_MinMaxLabel;

		// Token: 0x04003498 RID: 13464
		[SerializeField]
		private string m_SetTexLabel;

		// Token: 0x04003499 RID: 13465
		[SerializeField]
		private Sprite m_SourceSpriteDefault;

		// Token: 0x0400349A RID: 13466
		[SerializeField]
		private Sprite m_SourceSpriteMobile;

		// Token: 0x0400349B RID: 13467
		private Image m_TargetImage;

		// Token: 0x0400349C RID: 13468
		private MaterialSetterGraphWriter m_TargetWriter;

		// Token: 0x0400349D RID: 13469
		private Sprite m_TargetSprite;
	}
}
