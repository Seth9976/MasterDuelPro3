using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public class FontData : ISerializationCallbackReceiver
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000531C File Offset: 0x0000351C
		public static FontData defaultFontData
		{
			get
			{
				return new FontData
				{
					m_FontSize = 14,
					m_LineSpacing = 1f,
					m_FontStyle = FontStyle.Normal,
					m_BestFit = false,
					m_MinSize = 10,
					m_MaxSize = 40,
					m_Alignment = TextAnchor.UpperLeft,
					m_HorizontalOverflow = HorizontalWrapMode.Wrap,
					m_VerticalOverflow = VerticalWrapMode.Truncate,
					m_RichText = true,
					m_AlignByGeometry = false
				};
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00005382 File Offset: 0x00003582
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x0000538A File Offset: 0x0000358A
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				this.m_Font = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005393 File Offset: 0x00003593
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x0000539B File Offset: 0x0000359B
		public int fontSize
		{
			get
			{
				return this.m_FontSize;
			}
			set
			{
				this.m_FontSize = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000053A4 File Offset: 0x000035A4
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x000053AC File Offset: 0x000035AC
		public FontStyle fontStyle
		{
			get
			{
				return this.m_FontStyle;
			}
			set
			{
				this.m_FontStyle = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000053B5 File Offset: 0x000035B5
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000053BD File Offset: 0x000035BD
		public bool bestFit
		{
			get
			{
				return this.m_BestFit;
			}
			set
			{
				this.m_BestFit = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000053C6 File Offset: 0x000035C6
		// (set) Token: 0x060000CD RID: 205 RVA: 0x000053CE File Offset: 0x000035CE
		public int minSize
		{
			get
			{
				return this.m_MinSize;
			}
			set
			{
				this.m_MinSize = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000053D7 File Offset: 0x000035D7
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000053DF File Offset: 0x000035DF
		public int maxSize
		{
			get
			{
				return this.m_MaxSize;
			}
			set
			{
				this.m_MaxSize = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000053E8 File Offset: 0x000035E8
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000053F0 File Offset: 0x000035F0
		public TextAnchor alignment
		{
			get
			{
				return this.m_Alignment;
			}
			set
			{
				this.m_Alignment = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000053F9 File Offset: 0x000035F9
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00005401 File Offset: 0x00003601
		public bool alignByGeometry
		{
			get
			{
				return this.m_AlignByGeometry;
			}
			set
			{
				this.m_AlignByGeometry = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000540A File Offset: 0x0000360A
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00005412 File Offset: 0x00003612
		public bool richText
		{
			get
			{
				return this.m_RichText;
			}
			set
			{
				this.m_RichText = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000541B File Offset: 0x0000361B
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00005423 File Offset: 0x00003623
		public HorizontalWrapMode horizontalOverflow
		{
			get
			{
				return this.m_HorizontalOverflow;
			}
			set
			{
				this.m_HorizontalOverflow = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000542C File Offset: 0x0000362C
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00005434 File Offset: 0x00003634
		public VerticalWrapMode verticalOverflow
		{
			get
			{
				return this.m_VerticalOverflow;
			}
			set
			{
				this.m_VerticalOverflow = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000DA RID: 218 RVA: 0x0000543D File Offset: 0x0000363D
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00005445 File Offset: 0x00003645
		public float lineSpacing
		{
			get
			{
				return this.m_LineSpacing;
			}
			set
			{
				this.m_LineSpacing = value;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00002209 File Offset: 0x00000409
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005450 File Offset: 0x00003650
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.m_FontSize = Mathf.Clamp(this.m_FontSize, 0, 300);
			this.m_MinSize = Mathf.Clamp(this.m_MinSize, 0, this.m_FontSize);
			this.m_MaxSize = Mathf.Clamp(this.m_MaxSize, this.m_FontSize, 300);
		}

		// Token: 0x04000060 RID: 96
		[SerializeField]
		[FormerlySerializedAs("font")]
		private Font m_Font;

		// Token: 0x04000061 RID: 97
		[SerializeField]
		[FormerlySerializedAs("fontSize")]
		private int m_FontSize;

		// Token: 0x04000062 RID: 98
		[SerializeField]
		[FormerlySerializedAs("fontStyle")]
		private FontStyle m_FontStyle;

		// Token: 0x04000063 RID: 99
		[SerializeField]
		private bool m_BestFit;

		// Token: 0x04000064 RID: 100
		[SerializeField]
		private int m_MinSize;

		// Token: 0x04000065 RID: 101
		[SerializeField]
		private int m_MaxSize;

		// Token: 0x04000066 RID: 102
		[SerializeField]
		[FormerlySerializedAs("alignment")]
		private TextAnchor m_Alignment;

		// Token: 0x04000067 RID: 103
		[SerializeField]
		private bool m_AlignByGeometry;

		// Token: 0x04000068 RID: 104
		[SerializeField]
		[FormerlySerializedAs("richText")]
		private bool m_RichText;

		// Token: 0x04000069 RID: 105
		[SerializeField]
		private HorizontalWrapMode m_HorizontalOverflow;

		// Token: 0x0400006A RID: 106
		[SerializeField]
		private VerticalWrapMode m_VerticalOverflow;

		// Token: 0x0400006B RID: 107
		[SerializeField]
		private float m_LineSpacing;
	}
}
