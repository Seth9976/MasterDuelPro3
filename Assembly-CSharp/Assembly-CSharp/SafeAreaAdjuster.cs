using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class SafeAreaAdjuster : MonoBehaviour
{
	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000007 RID: 7 RVA: 0x0000216A File Offset: 0x0000036A
	// (set) Token: 0x06000008 RID: 8 RVA: 0x0000216D File Offset: 0x0000036D
	public RectTransform targetNode
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000216D File Offset: 0x0000036D
	private void Awake()
	{
	}

	// Token: 0x0600000A RID: 10 RVA: 0x0000216D File Offset: 0x0000036D
	private void Update()
	{
	}

	// Token: 0x0600000B RID: 11 RVA: 0x0000216D File Offset: 0x0000036D
	public void RefreshSafeArea()
	{
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000216D File Offset: 0x0000036D
	private void ApplySafeArea(Rect area, RectTransform target)
	{
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002170 File Offset: 0x00000370
	public static Rect GetSafeArea()
	{
		return default(Rect);
	}

	// Token: 0x04000003 RID: 3
	[SerializeField]
	private RectTransform m_targetNode;

	// Token: 0x04000004 RID: 4
	[SerializeField]
	private bool m_isOffVerticalSafearea;

	// Token: 0x04000005 RID: 5
	[SerializeField]
	private bool m_isOffHorizontalSafearea;

	// Token: 0x04000006 RID: 6
	private bool m_needRefreshSafeArea;
}
