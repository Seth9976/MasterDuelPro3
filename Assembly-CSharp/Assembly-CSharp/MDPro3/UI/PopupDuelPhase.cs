using System;
using MDPro3.Duel.YGOSharp;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200138D RID: 5005
	public class PopupDuelPhase : PopupDuel
	{
		// Token: 0x060090BF RID: 37055 RVA: 0x0013DAEC File Offset: 0x0013BCEC
		public override void InitializeSelections()
		{
			if (this.selections[0] == DuelPhase.Main1.ToString())
			{
				this.main1.transition = Selectable.Transition.Animation;
				this.main1.GetComponent<Animator>().runtimeAnimatorController = this.overrideController;
				this.main1.onClick.AddListener(new UnityAction(this.Hide));
			}
			else if (this.selections[0] == DuelPhase.BattleStart.ToString())
			{
				this.battle.transition = Selectable.Transition.Animation;
				this.battle.GetComponent<Animator>().runtimeAnimatorController = this.overrideController;
				this.battle.onClick.AddListener(new UnityAction(this.Hide));
			}
			else if (this.selections[0] == DuelPhase.Main2.ToString())
			{
				this.main2.transition = Selectable.Transition.Animation;
				this.main2.GetComponent<Animator>().runtimeAnimatorController = this.overrideController;
				this.main2.onClick.AddListener(new UnityAction(this.Hide));
			}
			for (int i = 1; i < this.selections.Count; i++)
			{
				if (this.selections[i] == DuelPhase.BattleStart.ToString())
				{
					this.battle.transition = Selectable.Transition.Animation;
					this.battle.onClick.AddListener(new UnityAction(this.OnBattle));
				}
				else if (this.selections[i] == DuelPhase.Main2.ToString())
				{
					this.main2.transition = Selectable.Transition.Animation;
					this.main2.onClick.AddListener(new UnityAction(this.OnMain2));
				}
				else if (this.selections[i] == DuelPhase.End.ToString())
				{
					this.end.transition = Selectable.Transition.Animation;
					this.end.onClick.AddListener(new UnityAction(this.OnEnd));
				}
			}
			if (this.main1.transition == Selectable.Transition.None)
			{
				this.main1.interactable = false;
				this.main1.transition = Selectable.Transition.Animation;
				global::UnityEngine.Object.Destroy(this.main1.GetComponent<UIEventWithAudio>());
			}
			if (this.battle.transition == Selectable.Transition.None)
			{
				this.battle.interactable = false;
				this.battle.transition = Selectable.Transition.Animation;
				global::UnityEngine.Object.Destroy(this.battle.GetComponent<UIEventWithAudio>());
			}
			if (this.main2.transition == Selectable.Transition.None)
			{
				this.main2.interactable = false;
				this.main2.transition = Selectable.Transition.Animation;
				global::UnityEngine.Object.Destroy(this.main2.GetComponent<UIEventWithAudio>());
			}
			if (this.end.transition == Selectable.Transition.None)
			{
				this.end.interactable = false;
				this.end.transition = Selectable.Transition.Animation;
				global::UnityEngine.Object.Destroy(this.end.GetComponent<UIEventWithAudio>());
			}
		}

		// Token: 0x060090C0 RID: 37056 RVA: 0x0013DE04 File Offset: 0x0013C004
		public override void Show()
		{
			base.Show();
			AudioManager.PlaySE("SE_PHASE_WINDOW_OPEN", 1f);
		}

		// Token: 0x060090C1 RID: 37057 RVA: 0x0013DE1B File Offset: 0x0013C01B
		public override void Hide()
		{
			base.Hide();
			AudioManager.PlaySE("SE_PHASE_WINDOW_CLOSE", 1f);
		}

		// Token: 0x060090C2 RID: 37058 RVA: 0x0013DE34 File Offset: 0x0013C034
		private void OnBattle()
		{
			BinaryMaster i = new BinaryMaster(null);
			i.writer.Write(6);
			base.SendReturn(i.Get());
			this.Hide();
		}

		// Token: 0x060090C3 RID: 37059 RVA: 0x0013DE68 File Offset: 0x0013C068
		private void OnMain2()
		{
			BinaryMaster i = new BinaryMaster(null);
			i.writer.Write(2);
			base.SendReturn(i.Get());
			this.Hide();
		}

		// Token: 0x060090C4 RID: 37060 RVA: 0x0013DE9C File Offset: 0x0013C09C
		private void OnEnd()
		{
			BinaryMaster i = new BinaryMaster(null);
			if (this.selections[0] == DuelPhase.BattleStart.ToString())
			{
				i.writer.Write(3);
			}
			else
			{
				i.writer.Write(7);
			}
			base.SendReturn(i.Get());
			this.Hide();
		}

		// Token: 0x0400CF78 RID: 53112
		[Header("Popup Duel Phase Reference")]
		public AnimatorOverrideController overrideController;

		// Token: 0x0400CF79 RID: 53113
		public Button main1;

		// Token: 0x0400CF7A RID: 53114
		public Button battle;

		// Token: 0x0400CF7B RID: 53115
		public Button main2;

		// Token: 0x0400CF7C RID: 53116
		public Button end;
	}
}
