using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MDPro3.Duel
{
	// Token: 0x020014BF RID: 5311
	public class PlayableGuide : MonoBehaviour
	{
		// Token: 0x06009B17 RID: 39703 RVA: 0x001800A3 File Offset: 0x0017E2A3
		private void Awake()
		{
			base.transform.SetParent(Program.instance.container_3D, false);
		}

		// Token: 0x06009B18 RID: 39704 RVA: 0x001800BC File Offset: 0x0017E2BC
		private void Update()
		{
			if (!this.loaded)
			{
				return;
			}
			if (this.guide0Showing)
			{
				this.guide0ShowLasted += Time.unscaledDeltaTime;
			}
			else
			{
				this.guide0ShowLasted = 0f;
			}
			if (this.guide1Showing)
			{
				this.guide1ShowLasted += Time.unscaledDeltaTime;
			}
			else
			{
				this.guide1ShowLasted = 0f;
			}
			if (this.guide0ShowLasted > this.noticeTime && this.coroutine0 == null)
			{
				this.Notice(true);
				this.guide0ShowLasted = 0f;
			}
			if (this.guide1ShowLasted > this.noticeTime && this.coroutine1 == null)
			{
				this.Notice(false);
				this.guide1ShowLasted = 0f;
			}
		}

		// Token: 0x06009B19 RID: 39705 RVA: 0x00180170 File Offset: 0x0017E370
		public void SetHeight(float height)
		{
			if (this.loaded)
			{
				this.animator0.transform.GetChild(2).localPosition = new Vector3(0f, height, 0f);
				this.animator1.transform.GetChild(2).localPosition = new Vector3(0f, height, 0f);
				return;
			}
			this.lumiHeight = height;
		}

		// Token: 0x06009B1A RID: 39706 RVA: 0x001801D9 File Offset: 0x0017E3D9
		public void Load(string field0Name, string field1Name)
		{
			this.field0Name = field0Name;
			this.field1Name = field1Name;
			this.loaded = false;
			this.LoadAsync();
		}

		// Token: 0x06009B1B RID: 39707 RVA: 0x001801F8 File Offset: 0x0017E3F8
		private async UniTask LoadAsync()
		{
			this.animator0 = ABLoader.LoadMasterDuelGameObject("PlayableGuide_c001_near").GetComponent<Animator>();
			this.animator0.transform.SetParent(base.transform, false);
			this.animator0.SetTrigger("Out");
			this.animator1 = ABLoader.LoadMasterDuelGameObject("PlayableGuide_c001_far").GetComponent<Animator>();
			this.animator1.transform.SetParent(base.transform, false);
			this.animator1.SetTrigger("Out");
			this.animator0.transform.GetChild(2).localPosition = new Vector3(0f, this.lumiHeight, 0f);
			this.animator1.transform.GetChild(2).localPosition = new Vector3(0f, this.lumiHeight, 0f);
			await UniTask.Yield();
			this.loaded = true;
		}

		// Token: 0x06009B1C RID: 39708 RVA: 0x0018023C File Offset: 0x0017E43C
		public void Set(bool me)
		{
			if (me)
			{
				if (!this.guide0Showing)
				{
					if (this.coroutine0 != null)
					{
						this.guide0Showing = true;
					}
					else
					{
						this.coroutine0 = base.StartCoroutine(this.PlayAnimator0Async(true));
					}
				}
				if (this.guide1Showing)
				{
					if (this.coroutine1 != null)
					{
						this.guide1Showing = false;
						return;
					}
					this.coroutine1 = base.StartCoroutine(this.PlayAnimator1Async(false));
					return;
				}
			}
			else
			{
				if (this.guide0Showing)
				{
					if (this.coroutine0 != null)
					{
						this.guide0Showing = false;
					}
					else
					{
						this.coroutine0 = base.StartCoroutine(this.PlayAnimator0Async(false));
					}
				}
				if (!this.guide1Showing)
				{
					if (this.coroutine1 != null)
					{
						this.guide1Showing = true;
						return;
					}
					this.coroutine1 = base.StartCoroutine(this.PlayAnimator1Async(true));
				}
			}
		}

		// Token: 0x06009B1D RID: 39709 RVA: 0x001802FB File Offset: 0x0017E4FB
		public void End()
		{
			this.animator0.SetTrigger("End");
			this.animator1.SetTrigger("End");
		}

		// Token: 0x06009B1E RID: 39710 RVA: 0x0018031D File Offset: 0x0017E51D
		private void Notice(bool me)
		{
			if (me)
			{
				this.animator0.SetTrigger("Notice");
				return;
			}
			this.animator1.SetTrigger("Notice");
		}

		// Token: 0x06009B1F RID: 39711 RVA: 0x00180343 File Offset: 0x0017E543
		private IEnumerator PlayAnimator0Async(bool show)
		{
			this.guide0Showing = show;
			if (show)
			{
				this.animator0.SetTrigger("Change");
			}
			else
			{
				this.animator0.SetTrigger("Out");
			}
			yield return new WaitForSeconds(this.playTime);
			if (this.guide0Showing != show)
			{
				this.coroutine0 = base.StartCoroutine(this.PlayAnimator0Async(this.guide0Showing));
			}
			else
			{
				this.coroutine0 = null;
			}
			yield break;
		}

		// Token: 0x06009B20 RID: 39712 RVA: 0x00180359 File Offset: 0x0017E559
		private IEnumerator PlayAnimator1Async(bool show)
		{
			this.guide1Showing = show;
			if (show)
			{
				this.animator1.SetTrigger("Change");
			}
			else
			{
				this.animator1.SetTrigger("Out");
			}
			yield return new WaitForSeconds(this.playTime);
			if (this.guide1Showing != show)
			{
				this.coroutine1 = base.StartCoroutine(this.PlayAnimator1Async(this.guide1Showing));
			}
			else
			{
				this.coroutine1 = null;
			}
			yield break;
		}

		// Token: 0x0400D911 RID: 55569
		private const string LABEL_TRIGGER_APPEAR = "Apper";

		// Token: 0x0400D912 RID: 55570
		private const string LABEL_TRIGGER_CHANGE = "Change";

		// Token: 0x0400D913 RID: 55571
		private const string LABEL_TRIGGER_CHANGE2 = "Change2";

		// Token: 0x0400D914 RID: 55572
		private const string LABEL_TRIGGER_NOTICE = "Notice";

		// Token: 0x0400D915 RID: 55573
		private const string LABEL_TRIGGER_OUT = "Out";

		// Token: 0x0400D916 RID: 55574
		private const string LABEL_TRIGGER_END = "End";

		// Token: 0x0400D917 RID: 55575
		private string field0Name;

		// Token: 0x0400D918 RID: 55576
		private string field1Name;

		// Token: 0x0400D919 RID: 55577
		private Animator animator0;

		// Token: 0x0400D91A RID: 55578
		private Animator animator1;

		// Token: 0x0400D91B RID: 55579
		private Coroutine coroutine0;

		// Token: 0x0400D91C RID: 55580
		private Coroutine coroutine1;

		// Token: 0x0400D91D RID: 55581
		private bool guide0Showing;

		// Token: 0x0400D91E RID: 55582
		private bool guide1Showing;

		// Token: 0x0400D91F RID: 55583
		private float guide0ShowLasted;

		// Token: 0x0400D920 RID: 55584
		private float guide1ShowLasted;

		// Token: 0x0400D921 RID: 55585
		private readonly float playTime = 1f;

		// Token: 0x0400D922 RID: 55586
		private readonly float noticeTime = 10f;

		// Token: 0x0400D923 RID: 55587
		private float lumiHeight = 0.11f;

		// Token: 0x0400D924 RID: 55588
		public bool loaded;
	}
}
