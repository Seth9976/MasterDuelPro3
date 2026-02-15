using System;
using DG.Tweening;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MDPro3.Servant
{
	// Token: 0x02001301 RID: 4865
	public class Servant : MonoBehaviour
	{
		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06008E65 RID: 36453 RVA: 0x00130723 File Offset: 0x0012E923
		public virtual float TransitionTime
		{
			get
			{
				return 0.4f;
			}
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x06008E66 RID: 36454 RVA: 0x0000763C File Offset: 0x0000583C
		public virtual int Depth
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x06008E67 RID: 36455 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool ShowLine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x06008E68 RID: 36456 RVA: 0x0000763C File Offset: 0x0000583C
		protected virtual bool NeedExitButton
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x06008E69 RID: 36457 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected virtual float BlackAlpha
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06008E6A RID: 36458 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected virtual float SubBlackAlpha
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06008E6B RID: 36459 RVA: 0x0013072A File Offset: 0x0012E92A
		protected virtual string Label_UI
		{
			get
			{
				return "ServantUI/" + base.GetType().Name + "UI.prefab";
			}
		}

		// Token: 0x06008E6C RID: 36460 RVA: 0x00130746 File Offset: 0x0012E946
		public virtual void Initialize()
		{
			UserInput.OnMouseCursorHide += this.OnMouseCursorHide;
		}

		// Token: 0x06008E6D RID: 36461 RVA: 0x0013075C File Offset: 0x0012E95C
		public void Show(int preDepth)
		{
			if (this.showing)
			{
				return;
			}
			this.showing = true;
			if (this.servantUI != null)
			{
				this.ApplyShowArrangement(preDepth);
				return;
			}
			this.inTransition = true;
			Addressables.InstantiateAsync(this.Label_UI, null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(this.transform, false);
				this.servantUI = result.Result.GetComponent<ServantUI>();
				this.ApplyShowArrangement(preDepth);
				this.FirstLoadEvent();
			};
		}

		// Token: 0x06008E6E RID: 36462 RVA: 0x001307D1 File Offset: 0x0012E9D1
		public void Hide(int nextDepth)
		{
			if (!this.showing)
			{
				return;
			}
			this.showing = false;
			this.ApplyHideArrangement(nextDepth);
		}

		// Token: 0x06008E6F RID: 36463 RVA: 0x001307EA File Offset: 0x0012E9EA
		public virtual void PerFrameFunction()
		{
			if (!this.NeedResponseInput())
			{
				return;
			}
			if (UserInput.MouseRightDown || UserInput.WasCancelPressed)
			{
				this.OnReturn();
			}
		}

		// Token: 0x06008E70 RID: 36464 RVA: 0x00130809 File Offset: 0x0012EA09
		public virtual void OnReturn()
		{
			AudioManager.PlaySE("SE_MENU_CANCEL", 1f);
			if (this.returnAction != null)
			{
				this.returnAction();
				return;
			}
			this.OnExit();
		}

		// Token: 0x06008E71 RID: 36465 RVA: 0x00130834 File Offset: 0x0012EA34
		public virtual void OnExit()
		{
			if (!(Program.instance.currentSubServant == this))
			{
				Program.instance.ShiftToServant(this.returnServant);
				return;
			}
			if (this is SettingServant)
			{
				this.Hide(-2);
				Program.instance.currentSubServant = null;
				return;
			}
			Program.instance.ShowSubServant(this.returnServant);
		}

		// Token: 0x06008E72 RID: 36466 RVA: 0x00130890 File Offset: 0x0012EA90
		public virtual bool NeedResponseInput()
		{
			return this.showing && !this.inTransition && !(this.servantUI == null) && !(UIManager.InputBlocker != null) && !(Program.instance.ui_.currentPopup != null) && !(Program.instance.ui_.currentPopupB != null) && !(Program.instance.ui_.currentSidePanel != null) && !UserInput.InputFieldActivating();
		}

		// Token: 0x06008E73 RID: 36467 RVA: 0x00130925 File Offset: 0x0012EB25
		public virtual void Select(bool forced = false)
		{
			if (!forced && !UserInput.NeedDefaultSelect())
			{
				return;
			}
			if (this.lastSelectable != null)
			{
				this.lastSelectable.Select();
				return;
			}
			if (this.servantUI != null)
			{
				this.servantUI.SelectDefaultSelectable();
			}
		}

		// Token: 0x06008E74 RID: 36468 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void JudgeInputBlockerExitMark(object o)
		{
		}

		// Token: 0x06008E75 RID: 36469 RVA: 0x00130965 File Offset: 0x0012EB65
		public virtual T GetUI<T>() where T : ServantUI
		{
			return this.servantUI as T;
		}

		// Token: 0x06008E76 RID: 36470 RVA: 0x00130978 File Offset: 0x0012EB78
		protected void LoadUI()
		{
			Addressables.InstantiateAsync(this.Label_UI, null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(base.transform, false);
				this.servantUI = result.Result.GetComponent<ServantUI>();
				this.FirstLoadEvent();
			};
		}

		// Token: 0x06008E77 RID: 36471 RVA: 0x001309A7 File Offset: 0x0012EBA7
		protected virtual void FirstLoadEvent()
		{
			this.servantUI.Initialize(this);
		}

		// Token: 0x06008E78 RID: 36472 RVA: 0x001309B8 File Offset: 0x0012EBB8
		protected virtual void ApplyShowArrangement(int preDepth)
		{
			this.inTransition = true;
			if (Program.instance.currentServant == this && preDepth == -1)
			{
				this.servantUI.ShowEvent();
				DOTween.To(delegate(float v)
				{
				}, 0f, 0f, Program.instance.ocgcore.TransitionTime).SetUpdate(true).OnComplete(delegate
				{
					this.servantUI.gameObject.SetActive(true);
					this.inTransition = false;
					this.servantUI.ResetUI();
					if (this.NeedExitButton)
					{
						UIManager.ShowExitButton(0f, Ease.Linear);
					}
					else
					{
						UIManager.HideExitButton(0f, Ease.Linear);
					}
					if (this.ShowLine)
					{
						UIManager.ShowLine(0f);
					}
					else
					{
						UIManager.HideLine(0f);
					}
					UIManager.ShowBlackBack(this.BlackAlpha, 0f, null);
					this.servantUI.AfterShowEvent();
					this.AfterShowingEvent();
				});
				return;
			}
			this.servantUI.gameObject.SetActive(true);
			this.servantUI.Show(preDepth > this.Depth);
			if (this.NeedExitButton)
			{
				UIManager.ShowExitButton(this.TransitionTime, Ease.Linear);
			}
			else
			{
				UIManager.HideExitButton(this.TransitionTime, Ease.Linear);
			}
			if (this.ShowLine)
			{
				UIManager.ShowLine(this.TransitionTime);
			}
			else
			{
				UIManager.HideLine(this.TransitionTime);
			}
			UIManager.ShowBlackBack((Program.instance.currentServant is OcgCore) ? this.SubBlackAlpha : this.BlackAlpha, this.TransitionTime, delegate
			{
				this.inTransition = false;
				this.AfterShowingEvent();
			});
		}

		// Token: 0x06008E79 RID: 36473 RVA: 0x00130AE5 File Offset: 0x0012ECE5
		protected virtual void AfterShowingEvent()
		{
			if (UserInput.NeedDefaultSelect())
			{
				this.Select(false);
			}
		}

		// Token: 0x06008E7A RID: 36474 RVA: 0x00130AF8 File Offset: 0x0012ECF8
		protected virtual void ApplyHideArrangement(int nextDepth)
		{
			this.inTransition = true;
			if (nextDepth == -1)
			{
				DOTween.To(delegate(float v)
				{
				}, 0f, 0f, this.TransitionTime).SetUpdate(true).OnComplete(delegate
				{
					this.servantUI.ShutDown();
					this.inTransition = false;
				});
				return;
			}
			this.servantUI.Hide(nextDepth > this.Depth);
			DOTween.To(delegate(float v)
			{
			}, 0f, 0f, this.TransitionTime).SetUpdate(true).OnComplete(delegate
			{
				this.inTransition = false;
				this.AfterHidingEvent();
			});
		}

		// Token: 0x06008E7B RID: 36475 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void AfterHidingEvent()
		{
		}

		// Token: 0x06008E7C RID: 36476 RVA: 0x00130BBF File Offset: 0x0012EDBF
		protected virtual void OnMouseCursorHide()
		{
			if (this.NeedResponseInput())
			{
				this.Select(false);
			}
		}

		// Token: 0x0400CC4D RID: 52301
		[HideInInspector]
		public Servant returnServant;

		// Token: 0x0400CC4E RID: 52302
		[HideInInspector]
		public Selectable lastSelectable;

		// Token: 0x0400CC4F RID: 52303
		[HideInInspector]
		public bool showing;

		// Token: 0x0400CC50 RID: 52304
		[HideInInspector]
		public bool inTransition;

		// Token: 0x0400CC51 RID: 52305
		public Action returnAction;

		// Token: 0x0400CC52 RID: 52306
		public ServantUI servantUI;
	}
}
