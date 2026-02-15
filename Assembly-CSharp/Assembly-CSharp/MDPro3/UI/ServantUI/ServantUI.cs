using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Servant;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x02001480 RID: 5248
	[RequireComponent(typeof(ElementObjectManager))]
	public class ServantUI : MonoBehaviour
	{
		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x060098A8 RID: 39080 RVA: 0x00168F64 File Offset: 0x00167164
		public ElementObjectManager Manager
		{
			get
			{
				return this.m_Manager = ((this.m_Manager != null) ? this.m_Manager : base.GetComponent<ElementObjectManager>());
			}
		}

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x060098A9 RID: 39081 RVA: 0x00168F98 File Offset: 0x00167198
		protected RectTransform Root
		{
			get
			{
				return this.m_Root = ((this.m_Root != null) ? this.m_Root : this.Manager.GetElement<RectTransform>("Root"));
			}
		}

		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x060098AA RID: 39082 RVA: 0x00168FD4 File Offset: 0x001671D4
		public virtual CanvasGroup CG
		{
			get
			{
				return this.m_CG = ((this.m_CG != null) ? this.m_CG : this.Root.GetComponent<CanvasGroup>());
			}
		}

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x060098AB RID: 39083 RVA: 0x0016900C File Offset: 0x0016720C
		protected TextMeshProUGUI Title
		{
			get
			{
				return this.m_Title = ((this.m_Title != null) ? this.m_Title : this.Manager.GetElement<TextMeshProUGUI>("TextTitle"));
			}
		}

		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x060098AC RID: 39084 RVA: 0x00169048 File Offset: 0x00167248
		protected RectTransform LeftPart
		{
			get
			{
				return this.m_LeftPart = ((this.m_LeftPart != null) ? this.m_LeftPart : this.Manager.GetElement<RectTransform>("LeftPart"));
			}
		}

		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x060098AD RID: 39085 RVA: 0x00169084 File Offset: 0x00167284
		protected RectTransform RightPart
		{
			get
			{
				return this.m_RightPart = ((this.m_RightPart != null) ? this.m_RightPart : this.Manager.GetElement<RectTransform>("RightPart"));
			}
		}

		// Token: 0x060098AE RID: 39086 RVA: 0x001690C0 File Offset: 0x001672C0
		public virtual void Initialize(Servant servant)
		{
			this.parentServant = servant;
			UIManager.Translate(base.gameObject);
		}

		// Token: 0x060098AF RID: 39087 RVA: 0x001690D4 File Offset: 0x001672D4
		public virtual void OnBack()
		{
			Program.instance.ExitCurrentServant();
		}

		// Token: 0x060098B0 RID: 39088 RVA: 0x001690E0 File Offset: 0x001672E0
		public virtual void SelectDefaultSelectable()
		{
			if (this.defaultSelectable != null)
			{
				this.defaultSelectable.Select();
			}
		}

		// Token: 0x060098B1 RID: 39089 RVA: 0x001690FC File Offset: 0x001672FC
		public virtual void ResetUI()
		{
			this.Root.anchoredPosition3D = Vector3.zero;
			this.Root.localEulerAngles = Vector3.zero;
			this.CG.alpha = 1f;
			this.CG.blocksRaycasts = true;
			this.ResetLeftAndRightParts();
		}

		// Token: 0x060098B2 RID: 39090 RVA: 0x0016914B File Offset: 0x0016734B
		public virtual void ShutDown()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060098B3 RID: 39091 RVA: 0x00169159 File Offset: 0x00167359
		public virtual void Show(bool cover)
		{
			if (this.HaveTwoParts())
			{
				this.Show_Push();
			}
			else if (cover)
			{
				this.Show_Cover();
			}
			else
			{
				this.Show_Uncover();
			}
			this.ShowEvent();
		}

		// Token: 0x060098B4 RID: 39092 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void ShowEvent()
		{
		}

		// Token: 0x060098B5 RID: 39093 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void AfterShowEvent()
		{
		}

		// Token: 0x060098B6 RID: 39094 RVA: 0x00169182 File Offset: 0x00167382
		public virtual void Hide(bool cover)
		{
			if (this.HaveTwoParts())
			{
				this.Hide_Pop();
			}
			else if (cover)
			{
				this.Hide_Cover();
			}
			else
			{
				this.Hide_Uncover();
			}
			this.HideEvent();
		}

		// Token: 0x060098B7 RID: 39095 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void HideEvent()
		{
		}

		// Token: 0x060098B8 RID: 39096 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void AfterHideEvent()
		{
		}

		// Token: 0x060098B9 RID: 39097 RVA: 0x001691AB File Offset: 0x001673AB
		protected T GetServant<T>() where T : Servant
		{
			return this.parentServant as T;
		}

		// Token: 0x060098BA RID: 39098 RVA: 0x001691BD File Offset: 0x001673BD
		protected bool HaveTwoParts()
		{
			return this.LeftPart != null && this.RightPart != null;
		}

		// Token: 0x060098BB RID: 39099 RVA: 0x001691DB File Offset: 0x001673DB
		protected virtual void ResetLeftAndRightParts()
		{
			if (!this.HaveTwoParts())
			{
				return;
			}
			this.LeftPart.anchoredPosition = Vector2.zero;
			this.RightPart.anchoredPosition = Vector2.zero;
		}

		// Token: 0x060098BC RID: 39100 RVA: 0x00169208 File Offset: 0x00167408
		protected virtual void Show_Push()
		{
			base.gameObject.SetActive(true);
			this.CG.alpha = 0f;
			this.CG.blocksRaycasts = false;
			DOTween.Sequence().SetUpdate(true).AppendInterval(0.2f)
				.Append(this.CG.DOFade(1f, 0.4f).SetEase(Ease.Linear))
				.OnComplete(delegate
				{
					this.CG.blocksRaycasts = true;
					this.AfterShowEvent();
				});
			this.LeftPart.anchoredPosition = new Vector2(-1500f, 0f);
			DOTween.Sequence().SetUpdate(true).AppendInterval(0.2f)
				.Append(this.LeftPart.DOAnchorPosX(0f, 0.4f, false).SetEase(Ease.OutQuart));
			this.RightPart.anchoredPosition = new Vector2(1500f, 0f);
			DOTween.Sequence().SetUpdate(true).AppendInterval(0.2f)
				.Append(this.RightPart.DOAnchorPosX(0f, 0.4f, false).SetEase(Ease.OutQuart));
		}

		// Token: 0x060098BD RID: 39101 RVA: 0x00169328 File Offset: 0x00167528
		protected virtual void Show_Cover()
		{
			base.gameObject.SetActive(true);
			this.CG.alpha = 0f;
			this.CG.blocksRaycasts = false;
			DOTween.Sequence().SetUpdate(true).AppendInterval(0.15f)
				.Append(this.CG.DOFade(1f, 0.3f).SetEase(Ease.Linear))
				.OnComplete(delegate
				{
					this.CG.blocksRaycasts = true;
					this.AfterShowEvent();
				});
			this.Root.anchoredPosition3D = new Vector3(-240f, 0f, -360f);
			DOTween.Sequence().SetUpdate(true).AppendInterval(0.15f)
				.Append(this.Root.DOAnchorPos3D(Vector3.zero, 0.5f, false).SetEase(Ease.OutQuart));
			this.Root.localEulerAngles = new Vector3(0f, 15f, 0f);
			DOTween.Sequence().SetUpdate(true).AppendInterval(0.15f)
				.Append(this.Root.DOLocalRotate(Vector3.zero, 0.5f, RotateMode.Fast).SetEase(Ease.OutQuart));
		}

		// Token: 0x060098BE RID: 39102 RVA: 0x00169454 File Offset: 0x00167654
		protected virtual void Show_Uncover()
		{
			base.gameObject.SetActive(true);
			this.CG.alpha = 0f;
			this.CG.blocksRaycasts = false;
			DOTween.Sequence().SetUpdate(true).AppendInterval(0.15f)
				.Append(this.CG.DOFade(1f, 0.3f).SetEase(Ease.Linear))
				.OnComplete(delegate
				{
					this.CG.blocksRaycasts = true;
					this.AfterShowEvent();
				});
			this.Root.anchoredPosition3D = new Vector3(240f, 0f, 360f);
			DOTween.Sequence().SetUpdate(true).AppendInterval(0.15f)
				.Append(this.Root.DOAnchorPos3D(Vector3.zero, 0.5f, false).SetEase(Ease.OutQuart));
			this.Root.localEulerAngles = new Vector3(0f, -15f, 0f);
			DOTween.Sequence().SetUpdate(true).AppendInterval(0.15f)
				.Append(this.Root.DOLocalRotate(Vector3.zero, 0.5f, RotateMode.Fast).SetEase(Ease.OutQuart));
		}

		// Token: 0x060098BF RID: 39103 RVA: 0x00169580 File Offset: 0x00167780
		protected virtual void Hide_Pop()
		{
			this.CG.blocksRaycasts = false;
			this.CG.DOFade(0f, 0.25f).SetEase(Ease.Linear).SetUpdate(true)
				.OnComplete(delegate
				{
					base.gameObject.SetActive(false);
					this.AfterHideEvent();
				});
			this.LeftPart.DOAnchorPosX(-1500f, 0.25f, false).SetEase(Ease.InCubic).SetUpdate(true);
			this.RightPart.DOAnchorPosX(1500f, 0.25f, false).SetEase(Ease.InCubic).SetUpdate(true);
		}

		// Token: 0x060098C0 RID: 39104 RVA: 0x00169614 File Offset: 0x00167814
		protected virtual void Hide_Cover()
		{
			this.CG.blocksRaycasts = false;
			this.CG.DOFade(0f, 0.2f).SetEase(Ease.Linear).SetUpdate(true)
				.OnComplete(delegate
				{
					base.gameObject.SetActive(false);
					this.AfterHideEvent();
				});
			this.Root.DOAnchorPos3D(new Vector3(-240f, 0f, -360f), 0.2f, false).SetEase(Ease.InCubic).SetUpdate(true);
			this.Root.DOLocalRotate(new Vector3(0f, 15f, 0f), 0.2f, RotateMode.Fast).SetEase(Ease.InCubic).SetUpdate(true);
		}

		// Token: 0x060098C1 RID: 39105 RVA: 0x001696C4 File Offset: 0x001678C4
		protected virtual void Hide_Uncover()
		{
			this.CG.blocksRaycasts = false;
			this.CG.DOFade(0f, 0.2f).SetEase(Ease.Linear).SetUpdate(true)
				.OnComplete(delegate
				{
					base.gameObject.SetActive(false);
					this.AfterHideEvent();
				});
			this.Root.DOAnchorPos3D(new Vector3(240f, 0f, 360f), 0.2f, false).SetEase(Ease.InCubic).SetUpdate(true);
			this.Root.DOLocalRotate(new Vector3(0f, -15f, 0f), 0.2f, RotateMode.Fast).SetEase(Ease.InCubic).SetUpdate(true);
		}

		// Token: 0x0400D6C3 RID: 54979
		private ElementObjectManager m_Manager;

		// Token: 0x0400D6C4 RID: 54980
		private const string LABEL_RT_ROOT = "Root";

		// Token: 0x0400D6C5 RID: 54981
		private RectTransform m_Root;

		// Token: 0x0400D6C6 RID: 54982
		private CanvasGroup m_CG;

		// Token: 0x0400D6C7 RID: 54983
		private const string LABEL_TXT_TITLE = "TextTitle";

		// Token: 0x0400D6C8 RID: 54984
		private TextMeshProUGUI m_Title;

		// Token: 0x0400D6C9 RID: 54985
		private const string LABEL_RT_LEFTPART = "LeftPart";

		// Token: 0x0400D6CA RID: 54986
		private RectTransform m_LeftPart;

		// Token: 0x0400D6CB RID: 54987
		private const string LABEL_RT_RIGHTPART = "RightPart";

		// Token: 0x0400D6CC RID: 54988
		private RectTransform m_RightPart;

		// Token: 0x0400D6CD RID: 54989
		[Header("Servant UI")]
		[SerializeField]
		protected Selectable defaultSelectable;

		// Token: 0x0400D6CE RID: 54990
		protected Servant parentServant;
	}
}
