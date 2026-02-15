using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013D9 RID: 5081
	public class SuperScrollView
	{
		// Token: 0x06009333 RID: 37683 RVA: 0x0014BC9C File Offset: 0x00149E9C
		public SuperScrollView(int columnCount, float itemWidth, float itemHeight, float topPadding, float bottomPadding, GameObject itemObject, Action<string[], GameObject> itemOnListRefresh, ScrollRect scrollRect, int extraShow = 2)
		{
			this.columnCount = columnCount;
			this.itemWidth = itemWidth;
			this.itemHeight = itemHeight;
			this.topPadding = topPadding;
			this.bottomPadding = bottomPadding;
			this.itemObject = itemObject;
			this.itemOnListRefresh = itemOnListRefresh;
			this.scrollRect = scrollRect;
			this.extraShow = extraShow;
			this.Install();
		}

		// Token: 0x06009334 RID: 37684 RVA: 0x0014BD38 File Offset: 0x00149F38
		private void Install()
		{
			this.scrollRectWindow = this.scrollRect.GetComponent<RectTransform>();
			this.scrollRect.verticalScrollbar.value = 1f;
			this.scrollRect.verticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.OnScrollBarChange));
		}

		// Token: 0x06009335 RID: 37685 RVA: 0x0014BD8C File Offset: 0x00149F8C
		public void Clear()
		{
			foreach (GameObject go in this.gameObjects)
			{
				SelectionToggle_ScrollRectItem handler;
				if (go.TryGetComponent<SelectionToggle_ScrollRectItem>(out handler))
				{
					handler.Dispose();
				}
				else
				{
					global::UnityEngine.Object.Destroy(go);
				}
			}
			this.items.Clear();
			this.gameObjects.Clear();
			this.scrollRect.content.sizeDelta = new Vector2(0f, 0f);
			this._columnCount = -1;
		}

		// Token: 0x06009336 RID: 37686 RVA: 0x0014BE2C File Offset: 0x0014A02C
		public void ToTop()
		{
			this.scrollRect.verticalNormalizedPosition = 1f;
		}

		// Token: 0x06009337 RID: 37687 RVA: 0x0014BE3E File Offset: 0x0014A03E
		public int GetColumnCount()
		{
			if (this.columnCount > 0)
			{
				return this.columnCount;
			}
			if (this._columnCount < 0)
			{
				this._columnCount = (int)Math.Floor((double)(this.GetContentRectWidth() / this.itemWidth));
			}
			return this._columnCount;
		}

		// Token: 0x06009338 RID: 37688 RVA: 0x0014BE7C File Offset: 0x0014A07C
		private float GetContentRectWidth()
		{
			return this.scrollRectWindow.rect.width - this.scrollRect.verticalScrollbarSpacing - this.scrollRect.verticalScrollbar.handleRect.rect.width;
		}

		// Token: 0x06009339 RID: 37689 RVA: 0x0014BEC6 File Offset: 0x0014A0C6
		public int GetRowsCount()
		{
			return (int)MathF.Ceiling((float)this.items.Count / (float)this.GetColumnCount());
		}

		// Token: 0x0600933A RID: 37690 RVA: 0x0014BEE4 File Offset: 0x0014A0E4
		public void Print(List<string[]> tasks)
		{
			this.Clear();
			for (int i = 0; i < tasks.Count; i++)
			{
				SuperScrollView.Item it = new SuperScrollView.Item
				{
					args = tasks[i],
					gameObject = null
				};
				this.items.Add(it);
			}
			this.CalculateChildrenPositon();
			int cc = this.GetColumnCount();
			this.scrollRect.content.sizeDelta = new Vector2(0f, (float)this.GetRowsCount() * this.itemHeight + this.topPadding + this.bottomPadding);
			int maxShowLines = (int)Math.Ceiling((double)(this.scrollRectWindow.rect.height / this.itemHeight));
			this.maxShow = cc * (maxShowLines + this.extraShow);
			if (this.maxShow > this.items.Count)
			{
				this.maxShow = this.items.Count;
			}
			for (int j = 0; j < this.maxShow; j++)
			{
				this.CreateItem(j);
			}
			this.ToTop();
		}

		// Token: 0x0600933B RID: 37691 RVA: 0x0014BFEC File Offset: 0x0014A1EC
		private void RefreshLayout(List<GameObject> obs = null)
		{
			int hiddenRows = (int)math.floor((1f - this.scrollRect.verticalScrollbar.value) * (this.scrollRect.content.rect.height - this.scrollRectWindow.rect.height) / this.itemHeight);
			if (hiddenRows < 0 && obs == null)
			{
				return;
			}
			if (hiddenRows < 0)
			{
				hiddenRows = 0;
			}
			if (hiddenRows > 0)
			{
				hiddenRows--;
			}
			if (hiddenRows == this.lastHiddenRows && obs == null)
			{
				return;
			}
			this.lastHiddenRows = hiddenRows;
			int cc = this.GetColumnCount();
			int start = hiddenRows * cc;
			int end = start + this.maxShow;
			if (end > this.items.Count)
			{
				start -= end - this.items.Count;
				end = this.items.Count;
			}
			List<GameObject> objects = obs ?? new List<GameObject>();
			if (obs == null)
			{
				bool found = false;
				for (int i = 0; i < this.items.Count; i++)
				{
					if (this.items[i].gameObject == null)
					{
						if (found)
						{
							break;
						}
					}
					else
					{
						found = true;
						if (i < start || i >= end)
						{
							objects.Add(this.items[i].gameObject);
							this.items[i].gameObject = null;
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < this.items.Count; j++)
				{
					if (!(this.items[j].gameObject == null))
					{
						objects.Add(this.items[j].gameObject);
						this.items[j].gameObject = null;
					}
				}
			}
			for (int k = start; k < end; k++)
			{
				if (this.items[k].gameObject == null)
				{
					this.items[k].gameObject = objects[0];
					objects.RemoveAt(0);
					this.ItemRefreshPositon(k);
				}
			}
			foreach (GameObject ob in objects)
			{
				this.gameObjects.Remove(ob);
				global::UnityEngine.Object.Destroy(ob);
			}
		}

		// Token: 0x0600933C RID: 37692 RVA: 0x0014C244 File Offset: 0x0014A444
		public void OnScrollBarChange(float value)
		{
			if (this.items.Count == 0)
			{
				return;
			}
			this.RefreshLayout(null);
		}

		// Token: 0x0600933D RID: 37693 RVA: 0x0014C25C File Offset: 0x0014A45C
		private void CreateItem(int i)
		{
			if (this.items[i].gameObject == null)
			{
				this.items[i].gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.itemObject);
				this.items[i].gameObject.SetActive(true);
				this.items[i].gameObject.transform.SetParent(this.scrollRect.content, false);
				SuperScrollViewItem mono;
				if (this.items[i].gameObject.TryGetComponent<SuperScrollViewItem>(out mono))
				{
					mono.handler = this;
				}
				this.gameObjects.Add(this.items[i].gameObject);
				this.ItemRefreshPositon(i);
			}
		}

		// Token: 0x0600933E RID: 37694 RVA: 0x0014C324 File Offset: 0x0014A524
		private void ItemRefreshPositon(int i)
		{
			if (this.items[i].gameObject == null)
			{
				return;
			}
			this.items[i].gameObject.GetComponent<RectTransform>().anchoredPosition = this.childrenPosition[i];
			SelectionToggle_ScrollRectItem selection;
			SelectionButton button;
			SuperScrollViewItem mono;
			if (this.items[i].gameObject.TryGetComponent<SelectionToggle_ScrollRectItem>(out selection))
			{
				if (i == this.selected)
				{
					selection.ToggleOnNow();
				}
				else
				{
					selection.ToggleOffNow();
				}
				selection.index = i;
			}
			else if (this.items[i].gameObject.TryGetComponent<SelectionButton>(out button))
			{
				button.index = i;
			}
			else if (this.items[i].gameObject.TryGetComponent<SuperScrollViewItem>(out mono))
			{
				mono.id = i;
			}
			this.itemOnListRefresh(this.items[i].args, this.items[i].gameObject);
		}

		// Token: 0x0600933F RID: 37695 RVA: 0x0014C424 File Offset: 0x0014A624
		private void CalculateChildrenPositon()
		{
			this.childrenPosition.Clear();
			int npr = this.GetColumnCount();
			for (int i = 0; i < this.items.Count; i++)
			{
				int x = i % npr;
				int y = (int)Math.Floor((double)((float)i / (float)npr));
				Vector3 position = new Vector3(-((float)npr / 2f) * this.itemWidth + ((float)x + 0.5f) * this.itemWidth, (float)(-(float)y) * this.itemHeight - this.topPadding, 0f);
				this.childrenPosition.Add(position);
			}
		}

		// Token: 0x06009340 RID: 37696 RVA: 0x0014C4B4 File Offset: 0x0014A6B4
		public void Add(string[] args)
		{
			this.items.Add(new SuperScrollView.Item
			{
				args = args,
				gameObject = null
			});
			int npr = this.GetColumnCount();
			this.scrollRect.content.sizeDelta = new Vector2(0f, (float)this.GetRowsCount() * this.itemHeight + this.topPadding + this.bottomPadding);
			int maxShowLines = (int)Math.Ceiling((double)(this.scrollRectWindow.rect.height / this.itemHeight));
			this.maxShow = npr * (maxShowLines + this.extraShow);
			if (this.maxShow > this.items.Count)
			{
				this.maxShow = this.items.Count;
			}
			this.CalculateChildrenPositon();
			this.RefreshLayout(null);
		}

		// Token: 0x06009341 RID: 37697 RVA: 0x0014C580 File Offset: 0x0014A780
		public void RemoveAt(int removed)
		{
			if (this.items.Count <= removed)
			{
				return;
			}
			List<GameObject> objects = new List<GameObject>();
			for (int i = removed; i < this.items.Count; i++)
			{
				if (!(this.items[i].gameObject == null))
				{
					objects.Add(this.items[i].gameObject);
					this.items[i].gameObject = null;
				}
			}
			this.items.RemoveAt(removed);
			int cc = this.GetColumnCount();
			this.scrollRect.content.sizeDelta = new Vector2(0f, (float)this.GetRowsCount() * this.itemHeight + this.topPadding + this.bottomPadding);
			int maxShowLines = (int)Math.Ceiling((double)(this.scrollRectWindow.rect.height / this.itemHeight));
			this.maxShow = cc * (maxShowLines + this.extraShow);
			if (this.maxShow > this.items.Count)
			{
				this.maxShow = this.items.Count;
			}
			this.CalculateChildrenPositon();
			this.RefreshLayout(objects);
		}

		// Token: 0x06009342 RID: 37698 RVA: 0x0014C6A8 File Offset: 0x0014A8A8
		public void UpdateAt(int id, string[] args)
		{
			this.items[id].args = args;
			if (this.items[id].gameObject != null)
			{
				this.itemOnListRefresh(args, this.items[id].gameObject);
			}
		}

		// Token: 0x06009343 RID: 37699 RVA: 0x0014C700 File Offset: 0x0014A900
		public SelectionToggle_ScrollRectItem GetItemByIndex(int index)
		{
			if (index >= this.items.Count)
			{
				index = this.items.Count - 1;
			}
			for (int i = index; i >= 0; i--)
			{
				if (this.items[i].gameObject != null)
				{
					return this.items[i].gameObject.GetComponent<SelectionToggle_ScrollRectItem>();
				}
			}
			return null;
		}

		// Token: 0x0400D184 RID: 53636
		public int selected = -1;

		// Token: 0x0400D185 RID: 53637
		public List<SuperScrollView.Item> items = new List<SuperScrollView.Item>();

		// Token: 0x0400D186 RID: 53638
		public List<GameObject> gameObjects = new List<GameObject>();

		// Token: 0x0400D187 RID: 53639
		public ScrollRect scrollRect;

		// Token: 0x0400D188 RID: 53640
		protected int columnCount;

		// Token: 0x0400D189 RID: 53641
		protected float itemWidth;

		// Token: 0x0400D18A RID: 53642
		protected float itemHeight;

		// Token: 0x0400D18B RID: 53643
		protected float topPadding;

		// Token: 0x0400D18C RID: 53644
		protected float bottomPadding;

		// Token: 0x0400D18D RID: 53645
		protected int extraShow = 2;

		// Token: 0x0400D18E RID: 53646
		protected GameObject itemObject;

		// Token: 0x0400D18F RID: 53647
		protected Action<string[], GameObject> itemOnListRefresh;

		// Token: 0x0400D190 RID: 53648
		private RectTransform scrollRectWindow;

		// Token: 0x0400D191 RID: 53649
		private int maxShow;

		// Token: 0x0400D192 RID: 53650
		private List<Vector3> childrenPosition = new List<Vector3>();

		// Token: 0x0400D193 RID: 53651
		private int lastHiddenRows = -1;

		// Token: 0x0400D194 RID: 53652
		private int _columnCount = -1;

		// Token: 0x020013DA RID: 5082
		public class Item
		{
			// Token: 0x0400D195 RID: 53653
			public string[] args;

			// Token: 0x0400D196 RID: 53654
			public GameObject gameObject;
		}
	}
}
