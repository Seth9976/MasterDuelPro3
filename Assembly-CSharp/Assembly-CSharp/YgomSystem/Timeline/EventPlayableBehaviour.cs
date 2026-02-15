using System;
using System.Collections.Generic;
using DG.Tweening;
using MDPro3;
using MDPro3.Servant;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006A4 RID: 1700
	public class EventPlayableBehaviour : PlayableBehaviour
	{
		// Token: 0x0600355D RID: 13661 RVA: 0x000F2D78 File Offset: 0x000F0F78
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			this.PlayContent();
			foreach (EventPlayableBehaviour.EventInfo e in this.eventList)
			{
				if (e.label == "WinStart" && !e.isDone)
				{
					e.isDone = true;
					DOTween.To(delegate(float v)
					{
					}, 0f, 0f, (float)e.time).OnComplete(delegate
					{
						Action endingAction = OcgCore.endingAction;
						if (endingAction == null)
						{
							return;
						}
						endingAction();
					});
				}
			}
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000F2E4C File Offset: 0x000F104C
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			if (playable.GetPlayState<Playable>() == PlayState.Playing)
			{
				this.PlayContent();
			}
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckEventInfos(Playable playable)
		{
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x000F2E60 File Offset: 0x000F1060
		private void PlayContent()
		{
			if (this.played)
			{
				return;
			}
			this.played = true;
			if (!(this.label == "StartCard"))
			{
				if (this.label == "StrongSummon")
				{
					if (Program.instance == null)
					{
						return;
					}
					if (Program.instance.currentServant != Program.instance.ocgcore)
					{
						return;
					}
					if (OcgCore.summonCard == null)
					{
						return;
					}
					int code = OcgCore.summonCard.GetData().Id;
					if (CutinViewer.HasCutin(code))
					{
						CutinViewer.Play(code, (int)OcgCore.summonCard.p.controller);
						return;
					}
				}
				else if (this.label == "Next")
				{
					Action nextEventAction = OcgCore.nextEventAction;
					if (nextEventAction == null)
					{
						return;
					}
					nextEventAction();
				}
				return;
			}
			Action startCard = OcgCore.startCard;
			if (startCard == null)
			{
				return;
			}
			startCard();
		}

		// Token: 0x040030BC RID: 12476
		public List<EventPlayableBehaviour.EventInfo> eventList;

		// Token: 0x040030BD RID: 12477
		public double startTime;

		// Token: 0x040030BE RID: 12478
		private bool processed;

		// Token: 0x040030BF RID: 12479
		public string label;

		// Token: 0x040030C0 RID: 12480
		private PlayableDirector director;

		// Token: 0x040030C1 RID: 12481
		private bool played;

		// Token: 0x020006A5 RID: 1701
		public class EventInfo
		{
			// Token: 0x040030C2 RID: 12482
			public string label;

			// Token: 0x040030C3 RID: 12483
			public double time;

			// Token: 0x040030C4 RID: 12484
			public bool isDone;
		}
	}
}
