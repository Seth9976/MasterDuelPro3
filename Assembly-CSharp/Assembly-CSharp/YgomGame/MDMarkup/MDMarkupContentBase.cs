using System;
using UnityEngine;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B90 RID: 2960
	[Serializable]
	public abstract class MDMarkupContentBase : IMDMarkupContent, ISerializationCallbackReceiver
	{
		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060054F3 RID: 21747
		public abstract MDMarkupDef.MarkupType markupType { get; }

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060054F4 RID: 21748
		public abstract int contentIndent { get; }

		// Token: 0x060054F5 RID: 21749 RVA: 0x0000216A File Offset: 0x0000036A
		public object ExportJsonObj()
		{
			return null;
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x060054F7 RID: 21751 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnAfterDeserialize()
		{
		}

		// Token: 0x060054F8 RID: 21752 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnBeforeSerialize()
		{
		}

		// Token: 0x060054F9 RID: 21753
		protected abstract object OnExportJsonObj(object jsonObj);

		// Token: 0x060054FA RID: 21754
		protected abstract void OnImportJsonObj(object jsonObj);

		// Token: 0x060054FB RID: 21755 RVA: 0x0000216A File Offset: 0x0000036A
		public string ToJson()
		{
			return null;
		}
	}
}
