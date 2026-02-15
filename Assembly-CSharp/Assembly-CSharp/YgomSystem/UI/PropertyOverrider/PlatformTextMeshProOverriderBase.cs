using System;
using TMPro;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x0200067C RID: 1660
	public abstract class PlatformTextMeshProOverriderBase<TARGET> : PropertyOverriderBase<TARGET> where TARGET : TMP_Text
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06003367 RID: 13159 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideFloatProperty fontSize
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06003368 RID: 13160 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideBoolProperty enableAutoSize
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06003369 RID: 13161 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideFloatProperty fontSizeMin
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x0600336A RID: 13162 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideFloatProperty fontSizeMax
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x0600336B RID: 13163 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideFloatProperty characterSpacing
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x0600336C RID: 13164 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideFloatProperty wordSpacing
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x0600336D RID: 13165 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideFloatProperty lineSpacing
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideFloatProperty paragraphSpacing
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(TARGET target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Export(TARGET target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x04002F97 RID: 12183
		[SerializeField]
		private OverrideFloatProperty m_FontSize;

		// Token: 0x04002F98 RID: 12184
		[SerializeField]
		private OverrideBoolProperty m_EnableAutoSize;

		// Token: 0x04002F99 RID: 12185
		[SerializeField]
		private OverrideFloatProperty m_FontSizeMin;

		// Token: 0x04002F9A RID: 12186
		[SerializeField]
		private OverrideFloatProperty m_FontSizeMax;

		// Token: 0x04002F9B RID: 12187
		[SerializeField]
		private OverrideFloatProperty m_CharacterSpacing;

		// Token: 0x04002F9C RID: 12188
		[SerializeField]
		private OverrideFloatProperty m_WordSpacing;

		// Token: 0x04002F9D RID: 12189
		[SerializeField]
		private OverrideFloatProperty m_LineSpacing;

		// Token: 0x04002F9E RID: 12190
		[SerializeField]
		private OverrideFloatProperty m_ParagraphSpacing;
	}
}
