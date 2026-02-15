using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomSystem.Effect
{
	// Token: 0x02000787 RID: 1927
	public class ScreenFiller : MonoBehaviour
	{
		// Token: 0x06003BD9 RID: 15321 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSpriteColor(Color color)
		{
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartFade(Color targetColor, float fadeTime = 0.2f)
		{
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateFade(bool force = false)
		{
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetLock(bool isLock)
		{
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetFillColor(Color color)
		{
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StartFillFade(Color color, float fadeTime = 0.2f)
		{
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetFillLock(bool isLock)
		{
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckEnabled()
		{
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x040034B5 RID: 13493
		private ElementObjectManager elements;

		// Token: 0x040034B6 RID: 13494
		private SpriteRenderer fillSprite;

		// Token: 0x040034B7 RID: 13495
		private static ScreenFiller instance;

		// Token: 0x040034B8 RID: 13496
		private float time;

		// Token: 0x040034B9 RID: 13497
		private float fadeTime;

		// Token: 0x040034BA RID: 13498
		private const float DefaultFadeTime = 0.2f;

		// Token: 0x040034BB RID: 13499
		private Color startColor;

		// Token: 0x040034BC RID: 13500
		private Color targetColor;

		// Token: 0x040034BD RID: 13501
		private bool isLock;
	}
}
