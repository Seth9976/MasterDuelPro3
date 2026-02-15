using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using MDPro3;
using MDPro3.Net;
using MDPro3.Utility;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000005 RID: 5
public class NewsManager : Manager
{
	// Token: 0x06000013 RID: 19 RVA: 0x00002190 File Offset: 0x00000390
	private void Start()
	{
		this.Hide();
		this.width = base.GetComponent<RectTransform>().rect.width;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000021BC File Offset: 0x000003BC
	private void Update()
	{
		if (!this.showing)
		{
			return;
		}
		if (!Program.instance.menu.showing)
		{
			return;
		}
		this.idleTime += Time.deltaTime;
		if (this.idleTime > 5f)
		{
			this.OnRight(0.2f);
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000220E File Offset: 0x0000040E
	private int GetMax()
	{
		if (this.news == null)
		{
			return 0;
		}
		if (this.news.ChineseCN.Length <= this.maxLoad)
		{
			return this.news.ChineseCN.Length;
		}
		return this.maxLoad;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002244 File Offset: 0x00000444
	public void Show()
	{
		if (this.news == null || this.newsPics.Count == 0)
		{
			return;
		}
		CanvasGroup component = base.GetComponent<CanvasGroup>();
		component.alpha = 1f;
		component.blocksRaycasts = true;
		this.showing = true;
		this.newsPic.texture = this.newsPics[0];
		this.newsText.text = this.news.ChineseCN[0].title;
		this.newsPic.rectTransform.anchoredPosition = new Vector2(0f, 0f);
		this.newsPic2.rectTransform.anchoredPosition = new Vector2(-this.width, 0f);
		this.currentNewsIndex = 0;
		this.textCount.text = string.Format("{0}/{1}", this.currentNewsIndex + 1, this.GetMax());
	}

	// Token: 0x06000017 RID: 23 RVA: 0x0000232D File Offset: 0x0000052D
	public void Hide()
	{
		CanvasGroup component = base.GetComponent<CanvasGroup>();
		component.alpha = 0f;
		component.blocksRaycasts = false;
		this.showing = false;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x0000234D File Offset: 0x0000054D
	public void LoadNews()
	{
		if (this.news == null)
		{
			return;
		}
		this.LoadNewsImageAsync();
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002360 File Offset: 0x00000560
	private async Task LoadNewsImageAsync()
	{
		this.Hide();
		int i = 0;
		while (i < this.news.ChineseCN.Length && i < this.maxLoad)
		{
			NewsManager.<>c__DisplayClass17_0 CS$<>8__locals1 = new NewsManager.<>c__DisplayClass17_0();
			CS$<>8__locals1.load = Tools.DownloadImageAsync(this.news.ChineseCN[i].image);
			await TaskUtility.WaitUntil(() => CS$<>8__locals1.load.IsCompleted);
			if (!Application.isPlaying)
			{
				return;
			}
			this.newsPics.Add(CS$<>8__locals1.load.Result);
			if (i == 0)
			{
				this.Show();
			}
			CS$<>8__locals1 = null;
			i++;
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000023A4 File Offset: 0x000005A4
	public void OnRight(float moveTime = 0.1f)
	{
		this.idleTime = 0f;
		if (this.news.ChineseCN.Length < 2)
		{
			return;
		}
		int next = (this.currentNewsIndex + 1) % this.GetMax();
		if (this.newsPics.Count < this.currentNewsIndex + 1)
		{
			this.newsPic.texture = null;
		}
		else
		{
			this.newsPic.texture = this.newsPics[this.currentNewsIndex];
		}
		this.newsText.text = this.news.ChineseCN[this.currentNewsIndex].title;
		if (this.newsPics.Count < next + 1)
		{
			this.newsPic2.texture = null;
		}
		else
		{
			this.newsPic2.texture = this.newsPics[next];
		}
		this.newsText2.text = this.news.ChineseCN[next].title;
		this.currentNewsIndex = next;
		this.textCount.text = string.Format("{0}/{1}", this.currentNewsIndex + 1, this.GetMax());
		this.newsPic.transform.localPosition = new Vector2(0f, 0f);
		this.newsPic2.transform.localPosition = new Vector2(this.width, 0f);
		this.newsPic.transform.DOLocalMoveX(-this.width, moveTime, false);
		this.newsPic2.transform.DOLocalMoveX(0f, moveTime, false);
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002540 File Offset: 0x00000740
	public void OnLeft(float moveTime = 0.1f)
	{
		this.idleTime = 0f;
		if (this.news.ChineseCN.Length < 2)
		{
			return;
		}
		int max = this.GetMax();
		int next = (max + this.currentNewsIndex - 1) % max;
		if (this.newsPics.Count < this.currentNewsIndex + 1)
		{
			this.newsPic.texture = null;
		}
		else
		{
			this.newsPic.texture = this.newsPics[this.currentNewsIndex];
		}
		this.newsText.text = this.news.ChineseCN[this.currentNewsIndex].title;
		if (this.newsPics.Count < next + 1)
		{
			this.newsPic2.texture = null;
		}
		else
		{
			this.newsPic2.texture = this.newsPics[next];
		}
		this.newsText2.text = this.news.ChineseCN[next].title;
		this.currentNewsIndex = next;
		this.textCount.text = string.Format("{0}/{1}", this.currentNewsIndex + 1, this.GetMax());
		this.newsPic.transform.localPosition = new Vector2(0f, 0f);
		this.newsPic2.transform.localPosition = new Vector2(-this.width, 0f);
		this.newsPic.transform.DOLocalMoveX(this.width, moveTime, false);
		this.newsPic2.transform.DOLocalMoveX(0f, moveTime, false);
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000026DF File Offset: 0x000008DF
	public void OnNewsClick()
	{
		Application.OpenURL(this.news.ChineseCN[this.currentNewsIndex].url.Replace("ygobbs.com", "ygobbs2.com"));
	}

	// Token: 0x0600001D RID: 29 RVA: 0x0000270C File Offset: 0x0000090C
	public void OnClose()
	{
		this.Hide();
	}

	// Token: 0x04000007 RID: 7
	public MyCardNews news;

	// Token: 0x04000008 RID: 8
	public RawImage newsPic;

	// Token: 0x04000009 RID: 9
	public RawImage newsPic2;

	// Token: 0x0400000A RID: 10
	public Text newsText;

	// Token: 0x0400000B RID: 11
	public Text newsText2;

	// Token: 0x0400000C RID: 12
	public Text textCount;

	// Token: 0x0400000D RID: 13
	public bool showing;

	// Token: 0x0400000E RID: 14
	private List<Texture2D> newsPics = new List<Texture2D>();

	// Token: 0x0400000F RID: 15
	private float width = 455f;

	// Token: 0x04000010 RID: 16
	private int maxLoad = 5;

	// Token: 0x04000011 RID: 17
	private float idleTime;

	// Token: 0x04000012 RID: 18
	private int currentNewsIndex;
}
