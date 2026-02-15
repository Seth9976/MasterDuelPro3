using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class MotionModel : MonoBehaviour
{
	// Token: 0x060000CD RID: 205 RVA: 0x0000216D File Offset: 0x0000036D
	private void Awake()
	{
	}

	// Token: 0x060000CE RID: 206 RVA: 0x0000216D File Offset: 0x0000036D
	public void Init(Animator animator)
	{
	}

	// Token: 0x060000CF RID: 207 RVA: 0x0000216D File Offset: 0x0000036D
	public void Play(string label)
	{
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x0000216D File Offset: 0x0000036D
	private void PlayBool(string label)
	{
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x000029CC File Offset: 0x00000BCC
	public bool IsTransitionCompleteLoopState()
	{
		return false;
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x0000216D File Offset: 0x0000036D
	private void PlayTrigger(string label, string seLabel = null)
	{
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x0000216D File Offset: 0x0000036D
	public void SetSELabel(string label, int modelId)
	{
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x0000216D File Offset: 0x0000036D
	public void SetEnableSE(bool flg)
	{
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x0000216A File Offset: 0x0000036A
	private IEnumerator DelaySetFalse(Action action)
	{
		return null;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x0000216D File Offset: 0x0000036D
	public void PlayAnimationEventSe(string seLabel)
	{
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x0000216D File Offset: 0x0000036D
	public void SetSePan(float pan)
	{
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x0000216D File Offset: 0x0000036D
	public void StopSe(float fade = -1f)
	{
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x000029CC File Offset: 0x00000BCC
	public bool CheckDefaultState()
	{
		return false;
	}

	// Token: 0x060000DA RID: 218 RVA: 0x000029CC File Offset: 0x00000BCC
	public bool HasMotion(string label)
	{
		return false;
	}

	// Token: 0x060000DB RID: 219 RVA: 0x0000216D File Offset: 0x0000036D
	public void ChangeSyncLayerWeight(float val)
	{
	}

	// Token: 0x04000120 RID: 288
	private Animator animator;

	// Token: 0x04000121 RID: 289
	private AnimatorStateInfo defaultState;

	// Token: 0x04000122 RID: 290
	private int syncLayerIdx;

	// Token: 0x04000123 RID: 291
	private bool playing;

	// Token: 0x04000124 RID: 292
	private bool enableSE;

	// Token: 0x04000125 RID: 293
	private string modelSeLabel;

	// Token: 0x04000126 RID: 294
	private int seId;

	// Token: 0x04000127 RID: 295
	private float sePan;

	// Token: 0x04000128 RID: 296
	private int loadModelId;

	// Token: 0x04000129 RID: 297
	private bool isMotionSELoaded;

	// Token: 0x0400012A RID: 298
	private string preAnimationEventSeLabel;

	// Token: 0x0400012B RID: 299
	private List<string> triggerList;

	// Token: 0x0400012C RID: 300
	private const string syncLayerName = "Sync Layer";
}
