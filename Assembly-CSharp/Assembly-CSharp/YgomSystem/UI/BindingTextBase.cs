using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200056F RID: 1391
	public abstract class BindingTextBase<BINDING_TEXT, TARGET> : Binding where BINDING_TEXT : BindingTextBase<BINDING_TEXT, TARGET> where TARGET : MonoBehaviour
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06002C50 RID: 11344 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002C51 RID: 11345 RVA: 0x0000216D File Offset: 0x0000036D
		[SerializeField]
		public string TextId
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06002C52 RID: 11346 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002C53 RID: 11347 RVA: 0x0000216D File Offset: 0x0000036D
		public bool RichText
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06002C54 RID: 11348 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002C55 RID: 11349 RVA: 0x0000216D File Offset: 0x0000036D
		public bool ReplaceNbsp
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ForceRebindAllText()
		{
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ForceRebindAllTextInChildren(GameObject parent, bool includeInactive)
		{
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetText(BINDING_TEXT bindingText, string id, object[] formatArg = null)
		{
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnRebind()
		{
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBinding()
		{
			return false;
		}

		// Token: 0x06002C5B RID: 11355
		protected abstract void SetText(TARGET target, string text);

		// Token: 0x06002C5C RID: 11356
		protected abstract string GetText(TARGET target);

		// Token: 0x06002C5D RID: 11357 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFromatArg(object[] arg)
		{
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x0000216A File Offset: 0x0000036A
		private static object[] AdjustFormatArg(string str, object[] formatArg)
		{
			return null;
		}

		// Token: 0x04002AAE RID: 10926
		[SerializeField]
		private string textId;

		// Token: 0x04002AAF RID: 10927
		[SerializeField]
		private bool richText;

		// Token: 0x04002AB0 RID: 10928
		[SerializeField]
		private bool replaceNbsp;

		// Token: 0x04002AB1 RID: 10929
		private object[] m_formatArg;
	}
}
