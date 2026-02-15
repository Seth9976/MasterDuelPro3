using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.Utility
{
	// Token: 0x02000540 RID: 1344
	public class ScreenFade : MonoBehaviour
	{
		// Token: 0x06002AE5 RID: 10981 RVA: 0x0000216D File Offset: 0x0000036D
		public static void FadeIn(float time, float startDelay = 0f, float endDelay = 0f, float easingPow = 1f, bool inputEnable = false, bool autoDestroy = true, bool ignoreTimeScale = false, Action onFinihed = null)
		{
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x0000216D File Offset: 0x0000036D
		public static void FadeOut(float time, float startDelay = 0f, float endDelay = 0f, float easingPow = 1f, bool inputEnable = false, bool autoDestroy = true, bool ignoreTimeScale = false, Action onFinihed = null)
		{
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x0000216D File Offset: 0x0000036D
		public static void FadeColor(Color baseColor, Color targetColor, float time, float startDelay = 0f, float endDelay = 0f, float easingPow = 1f, bool inputEnable = false, bool autoDestroy = true, bool ignoreTimeScale = false, Action onFinihed = null)
		{
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x0000216D File Offset: 0x0000036D
		public static void FadeEnd()
		{
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x0000216D File Offset: 0x0000036D
		private static void CreateFade()
		{
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnRenderObject()
		{
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x0000216D File Offset: 0x0000036D
		public void Play(Color baseColor, Color targetColor, float time, float startDelay = 0f, float endDelay = 0f, float easingPow = 1f, bool inputEnable = false, bool autoDestroy = true, bool ignoreTimeScale = false, Action onFinihed = null)
		{
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x0000216D File Offset: 0x0000036D
		private void DestroyFade()
		{
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetInputEnable(bool enable)
		{
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x0000216A File Offset: 0x0000036A
		private Mesh CreateScreenMesh()
		{
			return null;
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x0000216D File Offset: 0x0000036D
		private void DwarScreen(Material mat)
		{
		}

		// Token: 0x040029FC RID: 10748
		private static ScreenFade s_Instance;

		// Token: 0x040029FD RID: 10749
		private Mesh m_screenMesh;

		// Token: 0x040029FE RID: 10750
		private Color m_baseColor;

		// Token: 0x040029FF RID: 10751
		private Color m_targetColor;

		// Token: 0x04002A00 RID: 10752
		private Material m_material;

		// Token: 0x04002A01 RID: 10753
		private float m_timeMax;

		// Token: 0x04002A02 RID: 10754
		private float m_timeCnt;

		// Token: 0x04002A03 RID: 10755
		private float m_startDelay;

		// Token: 0x04002A04 RID: 10756
		private float m_endDelay;

		// Token: 0x04002A05 RID: 10757
		private float m_easingPow;

		// Token: 0x04002A06 RID: 10758
		private bool m_ignoreTimeScale;

		// Token: 0x04002A07 RID: 10759
		private bool m_inputEnable;

		// Token: 0x04002A08 RID: 10760
		private bool m_autoDestroy;

		// Token: 0x04002A09 RID: 10761
		private Action m_onFinihed;

		// Token: 0x04002A0A RID: 10762
		private List<GraphicRaycaster> raycasterList;
	}
}
