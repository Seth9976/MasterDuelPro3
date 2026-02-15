using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200004C RID: 76
public abstract class SingletonComponentBase<T> : MonoBehaviour where T : SingletonComponentBase<T>
{
	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600013E RID: 318 RVA: 0x00002A18 File Offset: 0x00000C18
	// (set) Token: 0x0600013F RID: 319 RVA: 0x0000216D File Offset: 0x0000036D
	protected static T m_Instance
	{
		get
		{
			return default(T);
		}
		set
		{
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000140 RID: 320 RVA: 0x000029CC File Offset: 0x00000BCC
	protected static bool m_IsAlive
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000141 RID: 321 RVA: 0x0000216D File Offset: 0x0000036D
	protected static void CreateInstance()
	{
	}

	// Token: 0x06000142 RID: 322 RVA: 0x0000216D File Offset: 0x0000036D
	protected static void CreateInstanceAsync(string path, UnityAction onFinish = null, Transform parent = null)
	{
	}

	// Token: 0x06000143 RID: 323 RVA: 0x0000216D File Offset: 0x0000036D
	public static void Release()
	{
	}

	// Token: 0x06000144 RID: 324
	protected abstract void Initialize();

	// Token: 0x06000145 RID: 325
	protected abstract void Terminate();

	// Token: 0x040001D6 RID: 470
	private static T _Instance;

	// Token: 0x040001D7 RID: 471
	public static bool IsReady;
}
