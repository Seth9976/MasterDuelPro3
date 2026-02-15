using System;
using UnityEngine;
using UnityEngine.ResourceManagement.Util;

// Token: 0x02000006 RID: 6
internal class MonoBehaviourCallbackHooks : ComponentSingleton<MonoBehaviourCallbackHooks>
{
	// Token: 0x14000003 RID: 3
	// (add) Token: 0x0600001F RID: 31 RVA: 0x000024F1 File Offset: 0x000006F1
	// (remove) Token: 0x06000020 RID: 32 RVA: 0x0000250A File Offset: 0x0000070A
	public event Action<float> OnUpdateDelegate
	{
		add
		{
			this.m_OnUpdateDelegate = (Action<float>)Delegate.Combine(this.m_OnUpdateDelegate, value);
		}
		remove
		{
			this.m_OnUpdateDelegate = (Action<float>)Delegate.Remove(this.m_OnUpdateDelegate, value);
		}
	}

	// Token: 0x14000004 RID: 4
	// (add) Token: 0x06000021 RID: 33 RVA: 0x00002523 File Offset: 0x00000723
	// (remove) Token: 0x06000022 RID: 34 RVA: 0x0000253C File Offset: 0x0000073C
	internal event Action<float> OnLateUpdateDelegate
	{
		add
		{
			this.m_OnLateUpdateDelegate = (Action<float>)Delegate.Combine(this.m_OnLateUpdateDelegate, value);
		}
		remove
		{
			this.m_OnLateUpdateDelegate = (Action<float>)Delegate.Remove(this.m_OnLateUpdateDelegate, value);
		}
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002555 File Offset: 0x00000755
	protected override string GetGameObjectName()
	{
		return "ResourceManagerCallbacks";
	}

	// Token: 0x06000024 RID: 36 RVA: 0x0000255C File Offset: 0x0000075C
	internal void Update()
	{
		Action<float> onUpdateDelegate = this.m_OnUpdateDelegate;
		if (onUpdateDelegate == null)
		{
			return;
		}
		onUpdateDelegate(Time.unscaledDeltaTime);
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00002573 File Offset: 0x00000773
	internal void LateUpdate()
	{
		Action<float> onLateUpdateDelegate = this.m_OnLateUpdateDelegate;
		if (onLateUpdateDelegate == null)
		{
			return;
		}
		onLateUpdateDelegate(Time.unscaledDeltaTime);
	}

	// Token: 0x04000008 RID: 8
	internal Action<float> m_OnUpdateDelegate;

	// Token: 0x04000009 RID: 9
	internal Action<float> m_OnLateUpdateDelegate;
}
