using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore
{
	// Token: 0x02000002 RID: 2
	[UsedByNativeCode]
	[Serializable]
	public struct FaceInfo
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal int faceIndex
		{
			get
			{
				return this.m_FaceIndex;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public string familyName
		{
			get
			{
				return this.m_FamilyName;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002080 File Offset: 0x00000280
		public string styleName
		{
			get
			{
				return this.m_StyleName;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002098 File Offset: 0x00000298
		public float pointSize
		{
			get
			{
				return this.m_PointSize;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020B0 File Offset: 0x000002B0
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020C8 File Offset: 0x000002C8
		public float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020D4 File Offset: 0x000002D4
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020EC File Offset: 0x000002EC
		[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
		internal int unitsPerEM
		{
			get
			{
				return this.m_UnitsPerEM;
			}
			set
			{
				this.m_UnitsPerEM = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020F8 File Offset: 0x000002F8
		public float lineHeight
		{
			get
			{
				return this.m_LineHeight;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002110 File Offset: 0x00000310
		public float ascentLine
		{
			get
			{
				return this.m_AscentLine;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002128 File Offset: 0x00000328
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002140 File Offset: 0x00000340
		public float capLine
		{
			get
			{
				return this.m_CapLine;
			}
			set
			{
				this.m_CapLine = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000214C File Offset: 0x0000034C
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002164 File Offset: 0x00000364
		public float meanLine
		{
			get
			{
				return this.m_MeanLine;
			}
			set
			{
				this.m_MeanLine = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002170 File Offset: 0x00000370
		public float baseline
		{
			get
			{
				return this.m_Baseline;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002188 File Offset: 0x00000388
		public float descentLine
		{
			get
			{
				return this.m_DescentLine;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021A0 File Offset: 0x000003A0
		public float superscriptOffset
		{
			get
			{
				return this.m_SuperscriptOffset;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021B8 File Offset: 0x000003B8
		public float superscriptSize
		{
			get
			{
				return this.m_SuperscriptSize;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021D0 File Offset: 0x000003D0
		public float subscriptOffset
		{
			get
			{
				return this.m_SubscriptOffset;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000021E8 File Offset: 0x000003E8
		public float subscriptSize
		{
			get
			{
				return this.m_SubscriptSize;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002200 File Offset: 0x00000400
		public float underlineOffset
		{
			get
			{
				return this.m_UnderlineOffset;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002218 File Offset: 0x00000418
		public float underlineThickness
		{
			get
			{
				return this.m_UnderlineThickness;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002230 File Offset: 0x00000430
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002248 File Offset: 0x00000448
		public float strikethroughOffset
		{
			get
			{
				return this.m_StrikethroughOffset;
			}
			set
			{
				this.m_StrikethroughOffset = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002254 File Offset: 0x00000454
		public float tabWidth
		{
			get
			{
				return this.m_TabWidth;
			}
		}

		// Token: 0x04000001 RID: 1
		[SerializeField]
		[NativeName("faceIndex")]
		private int m_FaceIndex;

		// Token: 0x04000002 RID: 2
		[SerializeField]
		[NativeName("familyName")]
		private string m_FamilyName;

		// Token: 0x04000003 RID: 3
		[SerializeField]
		[NativeName("styleName")]
		private string m_StyleName;

		// Token: 0x04000004 RID: 4
		[SerializeField]
		[NativeName("pointSize")]
		private float m_PointSize;

		// Token: 0x04000005 RID: 5
		[NativeName("scale")]
		[SerializeField]
		private float m_Scale;

		// Token: 0x04000006 RID: 6
		[NativeName("unitsPerEM")]
		[SerializeField]
		private int m_UnitsPerEM;

		// Token: 0x04000007 RID: 7
		[SerializeField]
		[NativeName("lineHeight")]
		private float m_LineHeight;

		// Token: 0x04000008 RID: 8
		[SerializeField]
		[NativeName("ascentLine")]
		private float m_AscentLine;

		// Token: 0x04000009 RID: 9
		[NativeName("capLine")]
		[SerializeField]
		private float m_CapLine;

		// Token: 0x0400000A RID: 10
		[NativeName("meanLine")]
		[SerializeField]
		private float m_MeanLine;

		// Token: 0x0400000B RID: 11
		[SerializeField]
		[NativeName("baseline")]
		private float m_Baseline;

		// Token: 0x0400000C RID: 12
		[NativeName("descentLine")]
		[SerializeField]
		private float m_DescentLine;

		// Token: 0x0400000D RID: 13
		[NativeName("superscriptOffset")]
		[SerializeField]
		private float m_SuperscriptOffset;

		// Token: 0x0400000E RID: 14
		[NativeName("superscriptSize")]
		[SerializeField]
		private float m_SuperscriptSize;

		// Token: 0x0400000F RID: 15
		[SerializeField]
		[NativeName("subscriptOffset")]
		private float m_SubscriptOffset;

		// Token: 0x04000010 RID: 16
		[SerializeField]
		[NativeName("subscriptSize")]
		private float m_SubscriptSize;

		// Token: 0x04000011 RID: 17
		[SerializeField]
		[NativeName("underlineOffset")]
		private float m_UnderlineOffset;

		// Token: 0x04000012 RID: 18
		[SerializeField]
		[NativeName("underlineThickness")]
		private float m_UnderlineThickness;

		// Token: 0x04000013 RID: 19
		[SerializeField]
		[NativeName("strikethroughOffset")]
		private float m_StrikethroughOffset;

		// Token: 0x04000014 RID: 20
		[SerializeField]
		[NativeName("strikethroughThickness")]
		private float m_StrikethroughThickness;

		// Token: 0x04000015 RID: 21
		[SerializeField]
		[NativeName("tabWidth")]
		private float m_TabWidth;
	}
}
