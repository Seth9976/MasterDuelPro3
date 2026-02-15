using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000022 RID: 34
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal class GUILayoutEntry
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00006DFC File Offset: 0x00004FFC
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00006E14 File Offset: 0x00005014
		public GUIStyle style
		{
			get
			{
				return this.m_Style;
			}
			set
			{
				this.m_Style = value;
				this.ApplyStyleSettings(value);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00006E26 File Offset: 0x00005026
		public virtual int marginLeft
		{
			get
			{
				return this.style.margin.left;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00006E38 File Offset: 0x00005038
		public virtual int marginRight
		{
			get
			{
				return this.style.margin.right;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00006E4A File Offset: 0x0000504A
		public virtual int marginTop
		{
			get
			{
				return this.style.margin.top;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00006E5C File Offset: 0x0000505C
		public virtual int marginBottom
		{
			get
			{
				return this.style.margin.bottom;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00006E6E File Offset: 0x0000506E
		public int marginHorizontal
		{
			get
			{
				return this.marginLeft + this.marginRight;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006E7D File Offset: 0x0000507D
		public int marginVertical
		{
			get
			{
				return this.marginBottom + this.marginTop;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006E8C File Offset: 0x0000508C
		public GUILayoutEntry(float _minWidth, float _maxWidth, float _minHeight, float _maxHeight, GUIStyle _style)
		{
			this.minWidth = _minWidth;
			this.maxWidth = _maxWidth;
			this.minHeight = _minHeight;
			this.maxHeight = _maxHeight;
			bool flag = _style == null;
			if (flag)
			{
				_style = GUIStyle.none;
			}
			this.style = _style;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006F08 File Offset: 0x00005108
		public virtual void CalcWidth()
		{
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006F08 File Offset: 0x00005108
		public virtual void CalcHeight()
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006F0B File Offset: 0x0000510B
		public virtual void SetHorizontal(float x, float width)
		{
			this.rect.x = x;
			this.rect.width = width;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006F28 File Offset: 0x00005128
		public virtual void SetVertical(float y, float height)
		{
			this.rect.y = y;
			this.rect.height = height;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006F48 File Offset: 0x00005148
		protected virtual void ApplyStyleSettings(GUIStyle style)
		{
			this.stretchWidth = ((style.fixedWidth == 0f && style.stretchWidth) ? 1 : 0);
			this.stretchHeight = ((style.fixedHeight == 0f && style.stretchHeight) ? 1 : 0);
			this.m_Style = style;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00006F9C File Offset: 0x0000519C
		public virtual void ApplyOptions(GUILayoutOption[] options)
		{
			bool flag = options == null;
			if (!flag)
			{
				foreach (GUILayoutOption i in options)
				{
					switch (i.type)
					{
					case GUILayoutOption.Type.fixedWidth:
						this.minWidth = (this.maxWidth = (float)i.value);
						this.stretchWidth = 0;
						break;
					case GUILayoutOption.Type.fixedHeight:
						this.minHeight = (this.maxHeight = (float)i.value);
						this.stretchHeight = 0;
						break;
					case GUILayoutOption.Type.minWidth:
					{
						this.minWidth = (float)i.value;
						bool flag2 = this.maxWidth < this.minWidth;
						if (flag2)
						{
							this.maxWidth = this.minWidth;
						}
						break;
					}
					case GUILayoutOption.Type.maxWidth:
					{
						this.maxWidth = (float)i.value;
						bool flag3 = this.minWidth > this.maxWidth;
						if (flag3)
						{
							this.minWidth = this.maxWidth;
						}
						this.stretchWidth = 0;
						break;
					}
					case GUILayoutOption.Type.minHeight:
					{
						this.minHeight = (float)i.value;
						bool flag4 = this.maxHeight < this.minHeight;
						if (flag4)
						{
							this.maxHeight = this.minHeight;
						}
						break;
					}
					case GUILayoutOption.Type.maxHeight:
					{
						this.maxHeight = (float)i.value;
						bool flag5 = this.minHeight > this.maxHeight;
						if (flag5)
						{
							this.minHeight = this.maxHeight;
						}
						this.stretchHeight = 0;
						break;
					}
					case GUILayoutOption.Type.stretchWidth:
						this.stretchWidth = (int)i.value;
						break;
					case GUILayoutOption.Type.stretchHeight:
						this.stretchHeight = (int)i.value;
						break;
					}
				}
				bool flag6 = this.maxWidth != 0f && this.maxWidth < this.minWidth;
				if (flag6)
				{
					this.maxWidth = this.minWidth;
				}
				bool flag7 = this.maxHeight != 0f && this.maxHeight < this.minHeight;
				if (flag7)
				{
					this.maxHeight = this.minHeight;
				}
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000071C4 File Offset: 0x000053C4
		public override string ToString()
		{
			string space = "";
			for (int i = 0; i < GUILayoutEntry.indent; i++)
			{
				space += " ";
			}
			return string.Concat(new string[]
			{
				space,
				UnityString.Format("{1}-{0} (x:{2}-{3}, y:{4}-{5})", new object[]
				{
					(this.style != null) ? this.style.name : "NULL",
					base.GetType(),
					this.rect.x,
					this.rect.xMax,
					this.rect.y,
					this.rect.yMax
				}),
				"   -   W: ",
				this.minWidth.ToString(),
				"-",
				this.maxWidth.ToString(),
				(this.stretchWidth != 0) ? "+" : "",
				", H: ",
				this.minHeight.ToString(),
				"-",
				this.maxHeight.ToString(),
				(this.stretchHeight != 0) ? "+" : ""
			});
		}

		// Token: 0x040000CA RID: 202
		public float minWidth;

		// Token: 0x040000CB RID: 203
		public float maxWidth;

		// Token: 0x040000CC RID: 204
		public float minHeight;

		// Token: 0x040000CD RID: 205
		public float maxHeight;

		// Token: 0x040000CE RID: 206
		public Rect rect = new Rect(0f, 0f, 0f, 0f);

		// Token: 0x040000CF RID: 207
		public int stretchWidth;

		// Token: 0x040000D0 RID: 208
		public int stretchHeight;

		// Token: 0x040000D1 RID: 209
		public bool consideredForMargin = true;

		// Token: 0x040000D2 RID: 210
		private GUIStyle m_Style = GUIStyle.none;

		// Token: 0x040000D3 RID: 211
		internal static Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x040000D4 RID: 212
		protected static int indent = 0;
	}
}
