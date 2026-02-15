using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.Htjson
{
	// Token: 0x0200075B RID: 1883
	public class HtjsonNode : MonoBehaviour, HtjsonContext
	{
		// Token: 0x06003AD4 RID: 15060 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void SetTextColor(Color col)
		{
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void AddReplaceParam(Dictionary<string, object> param)
		{
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool StandbyParentContext()
		{
			return false;
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x000F3818 File Offset: 0x000F1A18
		public virtual Color GetTextColor()
		{
			return default(Color);
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual Dictionary<string, object> GetReplaceParam()
		{
			return null;
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x0000216A File Offset: 0x0000036A
		public HtjsonReceiver GetReceiver()
		{
			return null;
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x0000216A File Offset: 0x0000036A
		public string ProcPath(string path)
		{
			return null;
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadStyle(string path)
		{
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetStyle(string id, object dic)
		{
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<string, object> GetStyle(string id)
		{
			return null;
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void InsertItem(Transform item)
		{
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void InsertItemList(List<object> list)
		{
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x0000216D File Offset: 0x0000036D
		public void InsertItemHtjson(object obj)
		{
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Clear()
		{
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBgEnable(bool enable)
		{
		}

		// Token: 0x06003AE3 RID: 15075 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetNodeParam(Dictionary<string, object> dic)
		{
		}

		// Token: 0x04003462 RID: 13410
		private bool getColor;

		// Token: 0x04003463 RID: 13411
		private bool parentGet;

		// Token: 0x04003464 RID: 13412
		private bool replaceGet;

		// Token: 0x04003465 RID: 13413
		private Color textColor;

		// Token: 0x04003466 RID: 13414
		private Dictionary<string, object> replaceParam;

		// Token: 0x04003467 RID: 13415
		private HtjsonContext parentContext;

		// Token: 0x04003468 RID: 13416
		private Dictionary<string, object> styles;
	}
}
