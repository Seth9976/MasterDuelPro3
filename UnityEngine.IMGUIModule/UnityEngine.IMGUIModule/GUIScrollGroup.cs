using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000024 RID: 36
	internal sealed class GUIScrollGroup : GUILayoutGroup
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00008695 File Offset: 0x00006895
		[RequiredByNativeCode]
		public GUIScrollGroup()
		{
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000086B0 File Offset: 0x000068B0
		public override void CalcWidth()
		{
			float _minWidth = this.minWidth;
			float _maxWidth = this.maxWidth;
			bool flag = this.allowHorizontalScroll;
			if (flag)
			{
				this.minWidth = 0f;
				this.maxWidth = 0f;
			}
			base.CalcWidth();
			this.calcMinWidth = this.minWidth;
			this.calcMaxWidth = this.maxWidth;
			bool flag2 = this.allowHorizontalScroll;
			if (flag2)
			{
				bool flag3 = this.minWidth > 32f;
				if (flag3)
				{
					this.minWidth = 32f;
				}
				bool flag4 = _minWidth != 0f;
				if (flag4)
				{
					this.minWidth = _minWidth;
				}
				bool flag5 = _maxWidth != 0f;
				if (flag5)
				{
					this.maxWidth = _maxWidth;
					this.stretchWidth = 0;
				}
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008770 File Offset: 0x00006970
		public override void SetHorizontal(float x, float width)
		{
			float _cWidth = (this.needsVerticalScrollbar ? (width - this.verticalScrollbar.fixedWidth - (float)this.verticalScrollbar.margin.left) : width);
			bool flag = this.allowHorizontalScroll && _cWidth < this.calcMinWidth;
			if (flag)
			{
				this.needsHorizontalScrollbar = true;
				this.minWidth = this.calcMinWidth;
				this.maxWidth = this.calcMaxWidth;
				base.SetHorizontal(x, this.calcMinWidth);
				this.rect.width = width;
				this.clientWidth = this.calcMinWidth;
			}
			else
			{
				this.needsHorizontalScrollbar = false;
				bool flag2 = this.allowHorizontalScroll;
				if (flag2)
				{
					this.minWidth = this.calcMinWidth;
					this.maxWidth = this.calcMaxWidth;
				}
				base.SetHorizontal(x, _cWidth);
				this.rect.width = width;
				this.clientWidth = _cWidth;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00008858 File Offset: 0x00006A58
		public override void CalcHeight()
		{
			float _minHeight = this.minHeight;
			float _maxHeight = this.maxHeight;
			bool flag = this.allowVerticalScroll;
			if (flag)
			{
				this.minHeight = 0f;
				this.maxHeight = 0f;
			}
			base.CalcHeight();
			this.calcMinHeight = this.minHeight;
			this.calcMaxHeight = this.maxHeight;
			bool flag2 = this.needsHorizontalScrollbar;
			if (flag2)
			{
				float scrollerSize = this.horizontalScrollbar.fixedHeight + (float)this.horizontalScrollbar.margin.top;
				this.minHeight += scrollerSize;
				this.maxHeight += scrollerSize;
			}
			bool flag3 = this.allowVerticalScroll;
			if (flag3)
			{
				bool flag4 = this.minHeight > 32f;
				if (flag4)
				{
					this.minHeight = 32f;
				}
				bool flag5 = _minHeight != 0f;
				if (flag5)
				{
					this.minHeight = _minHeight;
				}
				bool flag6 = _maxHeight != 0f;
				if (flag6)
				{
					this.maxHeight = _maxHeight;
					this.stretchHeight = 0;
				}
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00008964 File Offset: 0x00006B64
		public override void SetVertical(float y, float height)
		{
			float availableHeight = height;
			bool flag = this.needsHorizontalScrollbar;
			if (flag)
			{
				availableHeight -= this.horizontalScrollbar.fixedHeight + (float)this.horizontalScrollbar.margin.top;
			}
			bool flag2 = this.allowVerticalScroll && availableHeight < this.calcMinHeight;
			if (flag2)
			{
				bool flag3 = !this.needsHorizontalScrollbar && !this.needsVerticalScrollbar;
				if (flag3)
				{
					this.clientWidth = this.rect.width - this.verticalScrollbar.fixedWidth - (float)this.verticalScrollbar.margin.left;
					bool flag4 = this.clientWidth < this.calcMinWidth;
					if (flag4)
					{
						this.clientWidth = this.calcMinWidth;
					}
					float outsideWidth = this.rect.width;
					this.SetHorizontal(this.rect.x, this.clientWidth);
					this.CalcHeight();
					this.rect.width = outsideWidth;
				}
				float origMinHeight = this.minHeight;
				float origMaxHeight = this.maxHeight;
				this.minHeight = this.calcMinHeight;
				this.maxHeight = this.calcMaxHeight;
				base.SetVertical(y, this.calcMinHeight);
				this.minHeight = origMinHeight;
				this.maxHeight = origMaxHeight;
				this.rect.height = height;
				this.clientHeight = this.calcMinHeight;
			}
			else
			{
				bool flag5 = this.allowVerticalScroll;
				if (flag5)
				{
					this.minHeight = this.calcMinHeight;
					this.maxHeight = this.calcMaxHeight;
				}
				base.SetVertical(y, availableHeight);
				this.rect.height = height;
				this.clientHeight = availableHeight;
			}
		}

		// Token: 0x040000EA RID: 234
		public float calcMinWidth;

		// Token: 0x040000EB RID: 235
		public float calcMaxWidth;

		// Token: 0x040000EC RID: 236
		public float calcMinHeight;

		// Token: 0x040000ED RID: 237
		public float calcMaxHeight;

		// Token: 0x040000EE RID: 238
		public float clientWidth;

		// Token: 0x040000EF RID: 239
		public float clientHeight;

		// Token: 0x040000F0 RID: 240
		public bool allowHorizontalScroll = true;

		// Token: 0x040000F1 RID: 241
		public bool allowVerticalScroll = true;

		// Token: 0x040000F2 RID: 242
		public bool needsHorizontalScrollbar;

		// Token: 0x040000F3 RID: 243
		public bool needsVerticalScrollbar;

		// Token: 0x040000F4 RID: 244
		public GUIStyle horizontalScrollbar;

		// Token: 0x040000F5 RID: 245
		public GUIStyle verticalScrollbar;
	}
}
