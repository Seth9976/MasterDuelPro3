using System;
using System.Text;
using UnityEngine;

// Token: 0x02000052 RID: 82
[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour
{
	// Token: 0x17000012 RID: 18
	// (get) Token: 0x06000173 RID: 371 RVA: 0x0000216A File Offset: 0x0000036A
	protected static SteamManager Instance
	{
		get
		{
			return null;
		}
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x06000174 RID: 372 RVA: 0x000029CC File Offset: 0x00000BCC
	public static bool Initialized
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000175 RID: 373 RVA: 0x0000216D File Offset: 0x0000036D
	protected static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
	}

	// Token: 0x06000176 RID: 374 RVA: 0x0000216D File Offset: 0x0000036D
	[RuntimeInitializeOnLoadMethod]
	private static void InitOnPlayMode()
	{
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0000216D File Offset: 0x0000036D
	protected virtual void Awake()
	{
	}

	// Token: 0x06000178 RID: 376 RVA: 0x0000216D File Offset: 0x0000036D
	protected virtual void OnEnable()
	{
	}

	// Token: 0x06000179 RID: 377 RVA: 0x0000216D File Offset: 0x0000036D
	protected virtual void OnDestroy()
	{
	}

	// Token: 0x0600017A RID: 378 RVA: 0x0000216D File Offset: 0x0000036D
	protected virtual void Update()
	{
	}

	// Token: 0x0400020F RID: 527
	protected static bool s_EverInitialized;

	// Token: 0x04000210 RID: 528
	protected static SteamManager s_instance;

	// Token: 0x04000211 RID: 529
	protected bool m_bInitialized;
}
