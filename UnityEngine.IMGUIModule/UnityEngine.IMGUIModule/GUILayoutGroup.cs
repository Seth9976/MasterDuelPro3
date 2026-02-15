using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000023 RID: 35
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEditor.CoreModule" })]
	internal class GUILayoutGroup : GUILayoutEntry
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00007340 File Offset: 0x00005540
		public override int marginLeft
		{
			get
			{
				return this.m_MarginLeft;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00007348 File Offset: 0x00005548
		public override int marginRight
		{
			get
			{
				return this.m_MarginRight;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007350 File Offset: 0x00005550
		public override int marginTop
		{
			get
			{
				return this.m_MarginTop;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00007358 File Offset: 0x00005558
		public override int marginBottom
		{
			get
			{
				return this.m_MarginBottom;
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007360 File Offset: 0x00005560
		public GUILayoutGroup()
			: base(0f, 0f, 0f, 0f, GUIStyle.none)
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007418 File Offset: 0x00005618
		public override void ApplyOptions(GUILayoutOption[] options)
		{
			bool flag = options == null;
			if (!flag)
			{
				base.ApplyOptions(options);
				foreach (GUILayoutOption i in options)
				{
					GUILayoutOption.Type type = i.type;
					GUILayoutOption.Type type2 = type;
					switch (type2)
					{
					case GUILayoutOption.Type.fixedWidth:
					case GUILayoutOption.Type.minWidth:
					case GUILayoutOption.Type.maxWidth:
						this.m_UserSpecifiedHeight = true;
						break;
					case GUILayoutOption.Type.fixedHeight:
					case GUILayoutOption.Type.minHeight:
					case GUILayoutOption.Type.maxHeight:
						this.m_UserSpecifiedWidth = true;
						break;
					default:
						if (type2 == GUILayoutOption.Type.spacing)
						{
							this.spacing = (float)((int)i.value);
						}
						break;
					}
				}
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000074AC File Offset: 0x000056AC
		protected override void ApplyStyleSettings(GUIStyle style)
		{
			base.ApplyStyleSettings(style);
			RectOffset mar = style.margin;
			this.m_MarginLeft = mar.left;
			this.m_MarginRight = mar.right;
			this.m_MarginTop = mar.top;
			this.m_MarginBottom = mar.bottom;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000074F9 File Offset: 0x000056F9
		public void ResetCursor()
		{
			this.m_Cursor = 0;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007504 File Offset: 0x00005704
		public override void CalcWidth()
		{
			bool flag = this.entries.Count == 0;
			if (flag)
			{
				this.maxWidth = (this.minWidth = (float)base.style.padding.horizontal);
			}
			else
			{
				int leftMarginMin = 0;
				int rightMarginMin = 0;
				this.m_ChildMinWidth = 0f;
				this.m_ChildMaxWidth = 0f;
				this.m_StretchableCountX = 0;
				bool first = true;
				bool flag2 = this.isVertical;
				if (flag2)
				{
					foreach (GUILayoutEntry i in this.entries)
					{
						i.CalcWidth();
						bool consideredForMargin = i.consideredForMargin;
						if (consideredForMargin)
						{
							bool flag3 = !first;
							if (flag3)
							{
								leftMarginMin = Mathf.Min(i.marginLeft, leftMarginMin);
								rightMarginMin = Mathf.Min(i.marginRight, rightMarginMin);
							}
							else
							{
								leftMarginMin = i.marginLeft;
								rightMarginMin = i.marginRight;
								first = false;
							}
							this.m_ChildMinWidth = Mathf.Max(i.minWidth + (float)i.marginHorizontal, this.m_ChildMinWidth);
							this.m_ChildMaxWidth = Mathf.Max(i.maxWidth + (float)i.marginHorizontal, this.m_ChildMaxWidth);
						}
						this.m_StretchableCountX += i.stretchWidth;
					}
					this.m_ChildMinWidth -= (float)(leftMarginMin + rightMarginMin);
					this.m_ChildMaxWidth -= (float)(leftMarginMin + rightMarginMin);
				}
				else
				{
					int lastMargin = 0;
					foreach (GUILayoutEntry j in this.entries)
					{
						j.CalcWidth();
						bool consideredForMargin2 = j.consideredForMargin;
						if (consideredForMargin2)
						{
							bool flag4 = !first;
							int margin;
							if (flag4)
							{
								margin = ((lastMargin > j.marginLeft) ? lastMargin : j.marginLeft);
							}
							else
							{
								margin = 0;
								first = false;
							}
							this.m_ChildMinWidth += j.minWidth + this.spacing + (float)margin;
							this.m_ChildMaxWidth += j.maxWidth + this.spacing + (float)margin;
							lastMargin = j.marginRight;
							this.m_StretchableCountX += j.stretchWidth;
						}
						else
						{
							this.m_ChildMinWidth += j.minWidth;
							this.m_ChildMaxWidth += j.maxWidth;
							this.m_StretchableCountX += j.stretchWidth;
						}
					}
					this.m_ChildMinWidth -= this.spacing;
					this.m_ChildMaxWidth -= this.spacing;
					bool flag5 = this.entries.Count != 0;
					if (flag5)
					{
						leftMarginMin = this.entries[0].marginLeft;
						rightMarginMin = lastMargin;
					}
					else
					{
						rightMarginMin = (leftMarginMin = 0);
					}
				}
				bool flag6 = base.style != GUIStyle.none || this.m_UserSpecifiedWidth;
				float leftPadding;
				float rightPadding;
				if (flag6)
				{
					leftPadding = (float)Mathf.Max(base.style.padding.left, leftMarginMin);
					rightPadding = (float)Mathf.Max(base.style.padding.right, rightMarginMin);
				}
				else
				{
					this.m_MarginLeft = leftMarginMin;
					this.m_MarginRight = rightMarginMin;
					rightPadding = (leftPadding = 0f);
				}
				this.minWidth = Mathf.Max(this.minWidth, this.m_ChildMinWidth + leftPadding + rightPadding);
				bool flag7 = this.maxWidth == 0f;
				if (flag7)
				{
					this.stretchWidth += this.m_StretchableCountX + (base.style.stretchWidth ? 1 : 0);
					this.maxWidth = this.m_ChildMaxWidth + leftPadding + rightPadding;
				}
				else
				{
					this.stretchWidth = 0;
				}
				this.maxWidth = Mathf.Max(this.maxWidth, this.minWidth);
				bool flag8 = base.style.fixedWidth != 0f;
				if (flag8)
				{
					this.maxWidth = (this.minWidth = base.style.fixedWidth);
					this.stretchWidth = 0;
				}
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007968 File Offset: 0x00005B68
		public override void SetHorizontal(float x, float width)
		{
			base.SetHorizontal(x, width);
			bool flag = this.resetCoords;
			if (flag)
			{
				x = 0f;
			}
			RectOffset padding = base.style.padding;
			bool flag2 = this.isVertical;
			if (flag2)
			{
				bool flag3 = base.style != GUIStyle.none;
				if (flag3)
				{
					foreach (GUILayoutEntry i in this.entries)
					{
						float leftMar = (float)Mathf.Max(i.marginLeft, padding.left);
						float thisX = x + leftMar;
						float thisWidth = width - (float)Mathf.Max(i.marginRight, padding.right) - leftMar;
						bool flag4 = i.stretchWidth != 0;
						if (flag4)
						{
							i.SetHorizontal(thisX, thisWidth);
						}
						else
						{
							i.SetHorizontal(thisX, Mathf.Clamp(thisWidth, i.minWidth, i.maxWidth));
						}
					}
				}
				else
				{
					float thisX2 = x - (float)this.marginLeft;
					float thisWidth2 = width + (float)base.marginHorizontal;
					foreach (GUILayoutEntry j in this.entries)
					{
						bool flag5 = j.stretchWidth != 0;
						if (flag5)
						{
							j.SetHorizontal(thisX2 + (float)j.marginLeft, thisWidth2 - (float)j.marginHorizontal);
						}
						else
						{
							j.SetHorizontal(thisX2 + (float)j.marginLeft, Mathf.Clamp(thisWidth2 - (float)j.marginHorizontal, j.minWidth, j.maxWidth));
						}
					}
				}
			}
			else
			{
				bool flag6 = base.style != GUIStyle.none;
				if (flag6)
				{
					float leftMar2 = (float)padding.left;
					float rightMar = (float)padding.right;
					bool flag7 = this.entries.Count != 0;
					if (flag7)
					{
						leftMar2 = Mathf.Max(leftMar2, (float)this.entries[0].marginLeft);
						rightMar = Mathf.Max(rightMar, (float)this.entries[this.entries.Count - 1].marginRight);
					}
					x += leftMar2;
					width -= rightMar + leftMar2;
				}
				float widthToDistribute = width - this.spacing * (float)(this.entries.Count - 1);
				float minMaxScale = 0f;
				bool flag8 = this.m_ChildMinWidth != this.m_ChildMaxWidth;
				if (flag8)
				{
					minMaxScale = Mathf.Clamp((widthToDistribute - this.m_ChildMinWidth) / (this.m_ChildMaxWidth - this.m_ChildMinWidth), 0f, 1f);
				}
				float perItemStretch = 0f;
				bool flag9 = widthToDistribute > this.m_ChildMaxWidth;
				if (flag9)
				{
					bool flag10 = this.m_StretchableCountX > 0;
					if (flag10)
					{
						perItemStretch = (widthToDistribute - this.m_ChildMaxWidth) / (float)this.m_StretchableCountX;
					}
				}
				int lastMargin = 0;
				bool firstMargin = true;
				foreach (GUILayoutEntry k in this.entries)
				{
					float thisWidth3 = Mathf.Lerp(k.minWidth, k.maxWidth, minMaxScale);
					thisWidth3 += perItemStretch * (float)k.stretchWidth;
					bool consideredForMargin = k.consideredForMargin;
					if (consideredForMargin)
					{
						int leftMargin = k.marginLeft;
						bool flag11 = firstMargin;
						if (flag11)
						{
							leftMargin = 0;
							firstMargin = false;
						}
						int margin = ((lastMargin > leftMargin) ? lastMargin : leftMargin);
						x += (float)margin;
						lastMargin = k.marginRight;
					}
					k.SetHorizontal(Mathf.Round(x), Mathf.Round(thisWidth3));
					x += thisWidth3 + this.spacing;
				}
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007D50 File Offset: 0x00005F50
		public override void CalcHeight()
		{
			bool flag = this.entries.Count == 0;
			if (flag)
			{
				this.maxHeight = (this.minHeight = (float)base.style.padding.vertical);
			}
			else
			{
				int topMarginMin = 0;
				int bottomMarginMin = 0;
				this.m_ChildMinHeight = 0f;
				this.m_ChildMaxHeight = 0f;
				this.m_StretchableCountY = 0;
				bool flag2 = this.isVertical;
				if (flag2)
				{
					int lastMargin = 0;
					bool first = true;
					foreach (GUILayoutEntry i in this.entries)
					{
						i.CalcHeight();
						bool consideredForMargin = i.consideredForMargin;
						if (consideredForMargin)
						{
							bool flag3 = !first;
							int margin;
							if (flag3)
							{
								margin = Mathf.Max(lastMargin, i.marginTop);
							}
							else
							{
								margin = 0;
								first = false;
							}
							this.m_ChildMinHeight += i.minHeight + this.spacing + (float)margin;
							this.m_ChildMaxHeight += i.maxHeight + this.spacing + (float)margin;
							lastMargin = i.marginBottom;
							this.m_StretchableCountY += i.stretchHeight;
						}
						else
						{
							this.m_ChildMinHeight += i.minHeight;
							this.m_ChildMaxHeight += i.maxHeight;
							this.m_StretchableCountY += i.stretchHeight;
						}
					}
					this.m_ChildMinHeight -= this.spacing;
					this.m_ChildMaxHeight -= this.spacing;
					bool flag4 = this.entries.Count != 0;
					if (flag4)
					{
						topMarginMin = this.entries[0].marginTop;
						bottomMarginMin = lastMargin;
					}
					else
					{
						topMarginMin = (bottomMarginMin = 0);
					}
				}
				else
				{
					bool first2 = true;
					foreach (GUILayoutEntry j in this.entries)
					{
						j.CalcHeight();
						bool consideredForMargin2 = j.consideredForMargin;
						if (consideredForMargin2)
						{
							bool flag5 = !first2;
							if (flag5)
							{
								topMarginMin = Mathf.Min(j.marginTop, topMarginMin);
								bottomMarginMin = Mathf.Min(j.marginBottom, bottomMarginMin);
							}
							else
							{
								topMarginMin = j.marginTop;
								bottomMarginMin = j.marginBottom;
								first2 = false;
							}
							this.m_ChildMinHeight = Mathf.Max(j.minHeight, this.m_ChildMinHeight);
							this.m_ChildMaxHeight = Mathf.Max(j.maxHeight, this.m_ChildMaxHeight);
						}
						this.m_StretchableCountY += j.stretchHeight;
					}
				}
				bool flag6 = base.style != GUIStyle.none || this.m_UserSpecifiedHeight;
				float firstPadding;
				float lastPadding;
				if (flag6)
				{
					firstPadding = (float)Mathf.Max(base.style.padding.top, topMarginMin);
					lastPadding = (float)Mathf.Max(base.style.padding.bottom, bottomMarginMin);
				}
				else
				{
					this.m_MarginTop = topMarginMin;
					this.m_MarginBottom = bottomMarginMin;
					lastPadding = (firstPadding = 0f);
				}
				this.minHeight = Mathf.Max(this.minHeight, this.m_ChildMinHeight + firstPadding + lastPadding);
				bool flag7 = this.maxHeight == 0f;
				if (flag7)
				{
					this.stretchHeight += this.m_StretchableCountY + (base.style.stretchHeight ? 1 : 0);
					this.maxHeight = this.m_ChildMaxHeight + firstPadding + lastPadding;
				}
				else
				{
					this.stretchHeight = 0;
				}
				this.maxHeight = Mathf.Max(this.maxHeight, this.minHeight);
				bool flag8 = base.style.fixedHeight != 0f;
				if (flag8)
				{
					this.maxHeight = (this.minHeight = base.style.fixedHeight);
					this.stretchHeight = 0;
				}
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008178 File Offset: 0x00006378
		public override void SetVertical(float y, float height)
		{
			base.SetVertical(y, height);
			bool flag = this.entries.Count == 0;
			if (!flag)
			{
				RectOffset padding = base.style.padding;
				bool flag2 = this.resetCoords;
				if (flag2)
				{
					y = 0f;
				}
				bool flag3 = this.isVertical;
				if (flag3)
				{
					bool flag4 = base.style != GUIStyle.none;
					if (flag4)
					{
						float topMar = (float)padding.top;
						float bottomMar = (float)padding.bottom;
						bool flag5 = this.entries.Count != 0;
						if (flag5)
						{
							topMar = Mathf.Max(topMar, (float)this.entries[0].marginTop);
							bottomMar = Mathf.Max(bottomMar, (float)this.entries[this.entries.Count - 1].marginBottom);
						}
						y += topMar;
						height -= bottomMar + topMar;
					}
					float heightToDistribute = height - this.spacing * (float)(this.entries.Count - 1);
					float minMaxScale = 0f;
					bool flag6 = this.m_ChildMinHeight != this.m_ChildMaxHeight;
					if (flag6)
					{
						minMaxScale = Mathf.Clamp((heightToDistribute - this.m_ChildMinHeight) / (this.m_ChildMaxHeight - this.m_ChildMinHeight), 0f, 1f);
					}
					float perItemStretch = 0f;
					bool flag7 = heightToDistribute > this.m_ChildMaxHeight;
					if (flag7)
					{
						bool flag8 = this.m_StretchableCountY > 0;
						if (flag8)
						{
							perItemStretch = (heightToDistribute - this.m_ChildMaxHeight) / (float)this.m_StretchableCountY;
						}
					}
					int lastMargin = 0;
					bool firstMargin = true;
					foreach (GUILayoutEntry i in this.entries)
					{
						float thisHeight = Mathf.Lerp(i.minHeight, i.maxHeight, minMaxScale);
						thisHeight += perItemStretch * (float)i.stretchHeight;
						bool consideredForMargin = i.consideredForMargin;
						if (consideredForMargin)
						{
							int topMargin = i.marginTop;
							bool flag9 = firstMargin;
							if (flag9)
							{
								topMargin = 0;
								firstMargin = false;
							}
							int margin = ((lastMargin > topMargin) ? lastMargin : topMargin);
							y += (float)margin;
							lastMargin = i.marginBottom;
						}
						i.SetVertical(Mathf.Round(y), Mathf.Round(thisHeight));
						y += thisHeight + this.spacing;
					}
				}
				else
				{
					bool flag10 = base.style != GUIStyle.none;
					if (flag10)
					{
						foreach (GUILayoutEntry j in this.entries)
						{
							float topMar2 = (float)Mathf.Max(j.marginTop, padding.top);
							float thisY = y + topMar2;
							float thisHeight2 = height - (float)Mathf.Max(j.marginBottom, padding.bottom) - topMar2;
							bool flag11 = j.stretchHeight != 0;
							if (flag11)
							{
								j.SetVertical(thisY, thisHeight2);
							}
							else
							{
								j.SetVertical(thisY, Mathf.Clamp(thisHeight2, j.minHeight, j.maxHeight));
							}
						}
					}
					else
					{
						float thisY2 = y - (float)this.marginTop;
						float thisHeight3 = height + (float)base.marginVertical;
						foreach (GUILayoutEntry k in this.entries)
						{
							bool flag12 = k.stretchHeight != 0;
							if (flag12)
							{
								k.SetVertical(thisY2 + (float)k.marginTop, thisHeight3 - (float)k.marginVertical);
							}
							else
							{
								k.SetVertical(thisY2 + (float)k.marginTop, Mathf.Clamp(thisHeight3 - (float)k.marginVertical, k.minHeight, k.maxHeight));
							}
						}
					}
				}
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008574 File Offset: 0x00006774
		public override string ToString()
		{
			string str = "";
			string space = "";
			for (int i = 0; i < GUILayoutEntry.indent; i++)
			{
				space += " ";
			}
			str = string.Concat(new string[]
			{
				str,
				base.ToString(),
				" Margins: ",
				this.m_ChildMinHeight.ToString(),
				" {\n"
			});
			GUILayoutEntry.indent += 4;
			foreach (GUILayoutEntry j in this.entries)
			{
				string text = str;
				GUILayoutEntry guilayoutEntry = j;
				str = text + ((guilayoutEntry != null) ? guilayoutEntry.ToString() : null) + "\n";
			}
			str = str + space + "}";
			GUILayoutEntry.indent -= 4;
			return str;
		}

		// Token: 0x040000D5 RID: 213
		public List<GUILayoutEntry> entries = new List<GUILayoutEntry>();

		// Token: 0x040000D6 RID: 214
		public bool isVertical = true;

		// Token: 0x040000D7 RID: 215
		public bool resetCoords = false;

		// Token: 0x040000D8 RID: 216
		public float spacing = 0f;

		// Token: 0x040000D9 RID: 217
		public bool sameSize = true;

		// Token: 0x040000DA RID: 218
		public bool isWindow = false;

		// Token: 0x040000DB RID: 219
		public int windowID = -1;

		// Token: 0x040000DC RID: 220
		private int m_Cursor = 0;

		// Token: 0x040000DD RID: 221
		protected int m_StretchableCountX = 100;

		// Token: 0x040000DE RID: 222
		protected int m_StretchableCountY = 100;

		// Token: 0x040000DF RID: 223
		protected bool m_UserSpecifiedWidth = false;

		// Token: 0x040000E0 RID: 224
		protected bool m_UserSpecifiedHeight = false;

		// Token: 0x040000E1 RID: 225
		protected float m_ChildMinWidth = 100f;

		// Token: 0x040000E2 RID: 226
		protected float m_ChildMaxWidth = 100f;

		// Token: 0x040000E3 RID: 227
		protected float m_ChildMinHeight = 100f;

		// Token: 0x040000E4 RID: 228
		protected float m_ChildMaxHeight = 100f;

		// Token: 0x040000E5 RID: 229
		protected int m_MarginLeft;

		// Token: 0x040000E6 RID: 230
		protected int m_MarginRight;

		// Token: 0x040000E7 RID: 231
		protected int m_MarginTop;

		// Token: 0x040000E8 RID: 232
		protected int m_MarginBottom;

		// Token: 0x040000E9 RID: 233
		private static readonly GUILayoutEntry none = new GUILayoutEntry(0f, 1f, 0f, 1f, GUIStyle.none);
	}
}
