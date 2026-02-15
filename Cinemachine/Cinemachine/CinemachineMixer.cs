using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

// Token: 0x02000005 RID: 5
internal sealed class CinemachineMixer : PlayableBehaviour
{
	// Token: 0x0600000B RID: 11 RVA: 0x00002371 File Offset: 0x00000571
	public override void OnPlayableDestroy(Playable playable)
	{
		if (this.m_BrainOverrideStack != null)
		{
			this.m_BrainOverrideStack.ReleaseCameraOverride(this.m_BrainOverrideId);
		}
		this.m_BrainOverrideId = -1;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002393 File Offset: 0x00000593
	public override void PrepareFrame(Playable playable, FrameData info)
	{
		this.m_PreviewPlay = false;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x0000239C File Offset: 0x0000059C
	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		base.ProcessFrame(playable, info, playerData);
		this.m_BrainOverrideStack = playerData as ICameraOverrideStack;
		if (this.m_BrainOverrideStack == null)
		{
			return;
		}
		int activeInputs = 0;
		int clipIndexA = -1;
		int clipIndexB = -1;
		bool incomingIsA = false;
		float weightB = 1f;
		for (int i = 0; i < playable.GetInputCount<Playable>(); i++)
		{
			float weight = playable.GetInputWeight(i);
			ScriptPlayable<CinemachineShotPlayable> clip = (ScriptPlayable<T>)playable.GetInput(i);
			CinemachineShotPlayable shot = clip.GetBehaviour();
			if (shot != null && shot.IsValid && playable.GetPlayState<Playable>() == PlayState.Playing && weight > 0f)
			{
				clipIndexA = clipIndexB;
				clipIndexB = i;
				weightB = weight;
				if (++activeInputs == 2)
				{
					Playable clipA = playable.GetInput(clipIndexA);
					incomingIsA = clip.GetTime<ScriptPlayable<CinemachineShotPlayable>>() >= clipA.GetTime<Playable>();
					if (clip.GetTime<ScriptPlayable<CinemachineShotPlayable>>() == clipA.GetTime<Playable>())
					{
						incomingIsA = clip.GetDuration<ScriptPlayable<CinemachineShotPlayable>>() < clipA.GetDuration<Playable>();
						break;
					}
					break;
				}
			}
		}
		if (activeInputs == 1 && weightB < 1f && playable.GetInput(clipIndexB).GetTime<Playable>() > playable.GetInput(clipIndexB).GetDuration<Playable>() / 2.0)
		{
			incomingIsA = true;
		}
		if (incomingIsA)
		{
			int num = clipIndexB;
			int num2 = clipIndexA;
			clipIndexA = num;
			clipIndexB = num2;
			weightB = 1f - weightB;
		}
		ICinemachineCamera camA = null;
		if (clipIndexA >= 0)
		{
			camA = ((ScriptPlayable<T>)playable.GetInput(clipIndexA)).GetBehaviour().VirtualCamera;
		}
		ICinemachineCamera camB = null;
		if (clipIndexB >= 0)
		{
			camB = ((ScriptPlayable<T>)playable.GetInput(clipIndexB)).GetBehaviour().VirtualCamera;
		}
		this.m_BrainOverrideId = this.m_BrainOverrideStack.SetCameraOverride(this.m_BrainOverrideId, camA, camB, weightB, this.GetDeltaTime(info.deltaTime));
	}

	// Token: 0x0600000E RID: 14 RVA: 0x0000253B File Offset: 0x0000073B
	private float GetDeltaTime(float deltaTime)
	{
		if (this.m_PreviewPlay || Application.isPlaying)
		{
			return deltaTime;
		}
		if (TargetPositionCache.CacheMode == TargetPositionCache.Mode.Playback && TargetPositionCache.HasCurrentTime)
		{
			return 0f;
		}
		return -1f;
	}

	// Token: 0x0400000F RID: 15
	public static CinemachineMixer.MasterDirectorDelegate GetMasterPlayableDirector;

	// Token: 0x04000010 RID: 16
	private ICameraOverrideStack m_BrainOverrideStack;

	// Token: 0x04000011 RID: 17
	private int m_BrainOverrideId = -1;

	// Token: 0x04000012 RID: 18
	private bool m_PreviewPlay;

	// Token: 0x02000006 RID: 6
	// (Invoke) Token: 0x06000011 RID: 17
	public delegate PlayableDirector MasterDirectorDelegate();
}
