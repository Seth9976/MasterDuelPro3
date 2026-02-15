using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Duel;

// Token: 0x0200001B RID: 27
public class Character : MonoBehaviour
{
	// Token: 0x17000005 RID: 5
	// (get) Token: 0x0600005A RID: 90 RVA: 0x0000216A File Offset: 0x0000036A
	public AvatarModelSetting ModelSetting
	{
		get
		{
			return null;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x0600005B RID: 91 RVA: 0x0000216A File Offset: 0x0000036A
	public AvatarMotionSetting MotionSetting
	{
		get
		{
			return null;
		}
	}

	// Token: 0x0600005C RID: 92 RVA: 0x0000216D File Offset: 0x0000036D
	public void InitializeAsync(int modelId, bool enableSE = true, bool is2D = false, bool enbleWait = false, Action onComplete = null)
	{
	}

	// Token: 0x0600005D RID: 93 RVA: 0x0000216A File Offset: 0x0000036A
	private IEnumerator yInitializeAsync(int modelId, bool enableSE = true, bool is2D = false, bool enbleWait = false, Action onComplete = null)
	{
		return null;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x0000216D File Offset: 0x0000036D
	public void Initialize(int modelId, bool enableSE = true, bool is2D = false, bool enbleWait = false, GameObject preloadModel = null, AvatarModelSetting avatarSetting = null)
	{
	}

	// Token: 0x0600005F RID: 95 RVA: 0x0000216D File Offset: 0x0000036D
	public void InitializeSelector()
	{
	}

	// Token: 0x06000060 RID: 96 RVA: 0x0000216D File Offset: 0x0000036D
	private void Update()
	{
	}

	// Token: 0x06000061 RID: 97 RVA: 0x0000216D File Offset: 0x0000036D
	public void OnDestroy()
	{
	}

	// Token: 0x06000062 RID: 98 RVA: 0x0000216D File Offset: 0x0000036D
	public void Wait3Reset()
	{
	}

	// Token: 0x06000063 RID: 99 RVA: 0x0000216A File Offset: 0x0000036A
	private string GetModelPath(int modelId)
	{
		return null;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x0000216A File Offset: 0x0000036A
	private string GetSubAvatarPath(int modelId)
	{
		return null;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x0000216D File Offset: 0x0000036D
	private void ModelDestroy()
	{
	}

	// Token: 0x06000066 RID: 102 RVA: 0x0000216D File Offset: 0x0000036D
	public void SetMotionSePan(float pan)
	{
	}

	// Token: 0x06000067 RID: 103 RVA: 0x0000216D File Offset: 0x0000036D
	public void StopMotionSe(float fade = -1f)
	{
	}

	// Token: 0x06000068 RID: 104 RVA: 0x0000216D File Offset: 0x0000036D
	public void SetEnableSe(bool flg)
	{
	}

	// Token: 0x06000069 RID: 105 RVA: 0x0000216D File Offset: 0x0000036D
	public void PlayMotion(AvatarMotionSetting.MotionID motionId)
	{
	}

	// Token: 0x0600006A RID: 106 RVA: 0x0000216D File Offset: 0x0000036D
	public void ChangeSyncLayerWeight(float val)
	{
	}

	// Token: 0x0600006B RID: 107 RVA: 0x000029CC File Offset: 0x00000BCC
	public bool HasMotion(AvatarMotionSetting.MotionID motionId)
	{
		return false;
	}

	// Token: 0x0600006C RID: 108 RVA: 0x0000216D File Offset: 0x0000036D
	public void PlayTapMotion()
	{
	}

	// Token: 0x0600006D RID: 109 RVA: 0x000029CC File Offset: 0x00000BCC
	public bool IsTransitionCompleteLoopState()
	{
		return false;
	}

	// Token: 0x0600006E RID: 110 RVA: 0x0000216D File Offset: 0x0000036D
	public void SetTapCallback(Action callback)
	{
	}

	// Token: 0x04000050 RID: 80
	private GameObject avatarModel;

	// Token: 0x04000051 RID: 81
	private MotionModel motion;

	// Token: 0x04000052 RID: 82
	private static AvatarModelSetting modelSetting;

	// Token: 0x04000053 RID: 83
	private static AvatarMotionSetting motionSetting;

	// Token: 0x04000054 RID: 84
	private bool enableWaitInterval;

	// Token: 0x04000055 RID: 85
	private float[] waitInterval;

	// Token: 0x04000056 RID: 86
	private float[] waitTarget;

	// Token: 0x04000057 RID: 87
	private float tapInterval;

	// Token: 0x04000058 RID: 88
	private List<Material> matList;

	// Token: 0x04000059 RID: 89
	private const float wait2RandMin = 20f;

	// Token: 0x0400005A RID: 90
	private const float wait2RandMax = 70f;

	// Token: 0x0400005B RID: 91
	private const float wait3Interval = 90f;

	// Token: 0x0400005C RID: 92
	private const float tapIntervalVal = 2f;

	// Token: 0x0400005D RID: 93
	private Coroutine m_InitializeRoutine;

	// Token: 0x0400005E RID: 94
	private Action tapCallback;

	// Token: 0x0400005F RID: 95
	public static readonly string CharacterPrefabPath;

	// Token: 0x0200001C RID: 28
	public enum TapPhase
	{
		// Token: 0x04000061 RID: 97
		TapPhaseNone,
		// Token: 0x04000062 RID: 98
		TapPhaseWait,
		// Token: 0x04000063 RID: 99
		TapPhaseWaitToPhase1,
		// Token: 0x04000064 RID: 100
		TapPhase1,
		// Token: 0x04000065 RID: 101
		TapPhasePhase1ToWait,
		// Token: 0x04000066 RID: 102
		TapPhaseWaitToPhase2,
		// Token: 0x04000067 RID: 103
		TapPhase2,
		// Token: 0x04000068 RID: 104
		TapPhasePhase2ToWait,
		// Token: 0x04000069 RID: 105
		TapPhaseWaitToPhase3,
		// Token: 0x0400006A RID: 106
		TapPhase3,
		// Token: 0x0400006B RID: 107
		TapPhasePhase3ToWait
	}

	// Token: 0x0200001D RID: 29
	public enum WaitType
	{
		// Token: 0x0400006D RID: 109
		WAIT2,
		// Token: 0x0400006E RID: 110
		WAIT3,
		// Token: 0x0400006F RID: 111
		WAIT_TYPE_MAX
	}

	// Token: 0x0200001E RID: 30
	public enum SubAvatarChange
	{
		// Token: 0x04000071 RID: 113
		None,
		// Token: 0x04000072 RID: 114
		ChangeBattlePhase,
		// Token: 0x04000073 RID: 115
		SummonExDeck,
		// Token: 0x04000074 RID: 116
		DamageBorder,
		// Token: 0x04000075 RID: 117
		SubAvatarChangeDebug = 999
	}
}
