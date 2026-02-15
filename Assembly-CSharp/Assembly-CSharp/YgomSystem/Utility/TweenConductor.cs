using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.UI;

namespace YgomSystem.Utility
{
	// Token: 0x02000557 RID: 1367
	internal class TweenConductor : MonoBehaviour
	{
		// Token: 0x06002BBD RID: 11197 RVA: 0x0000216A File Offset: 0x0000036A
		public static TweenConductor Create(GameObject addComponentTarget, GameObject target, List<string> labelList, UnityAction<string> onStarted, UnityAction<string> onFinished, UnityAction onCompleted)
		{
			return null;
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayStart()
		{
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsPlaying(string label)
		{
			return false;
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x0000216D File Offset: 0x0000036D
		private void Play(string label)
		{
		}

		// Token: 0x04002A4B RID: 10827
		public bool playOnStart;

		// Token: 0x04002A4C RID: 10828
		public bool destroyOnFinished;

		// Token: 0x04002A4D RID: 10829
		[SerializeField]
		private GameObject target;

		// Token: 0x04002A4E RID: 10830
		[SerializeField]
		private List<string> labelList;

		// Token: 0x04002A4F RID: 10831
		[SerializeField]
		private UnityEvent<string> onStarted;

		// Token: 0x04002A50 RID: 10832
		[SerializeField]
		private UnityEvent<string> onFinished;

		// Token: 0x04002A51 RID: 10833
		[SerializeField]
		private UnityEvent onCompleted;

		// Token: 0x04002A52 RID: 10834
		private bool playing;

		// Token: 0x04002A53 RID: 10835
		private int currentLabelIndex;

		// Token: 0x04002A54 RID: 10836
		private string currentLabel;

		// Token: 0x04002A55 RID: 10837
		private List<Tween> tweens;
	}
}
