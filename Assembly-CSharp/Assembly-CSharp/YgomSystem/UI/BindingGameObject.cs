using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000568 RID: 1384
	public class BindingGameObject : MonoBehaviour
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002C15 RID: 11285 RVA: 0x0000216D File Offset: 0x0000036D
		public string PrefabPath
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x0000216D File Offset: 0x0000036D
		public void Rebind(bool force = false)
		{
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetOnCreated(Action<GameObject> act)
		{
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetImmediate(bool immediate)
		{
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadGameObject()
		{
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SplitPath()
		{
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void PostModifiyByArgs(GameObject go)
		{
		}

		// Token: 0x04002A8A RID: 10890
		[SerializeField]
		protected string prefabPath;

		// Token: 0x04002A8B RID: 10891
		[HideInInspector]
		public bool IsDone;

		// Token: 0x04002A8C RID: 10892
		[SerializeField]
		private bool immediate;

		// Token: 0x04002A8D RID: 10893
		[SerializeField]
		public bool StartOnAwake;

		// Token: 0x04002A8E RID: 10894
		protected bool IsSetParent;

		// Token: 0x04002A8F RID: 10895
		protected string basePath;

		// Token: 0x04002A90 RID: 10896
		protected Dictionary<string, object> pathArgs;

		// Token: 0x04002A91 RID: 10897
		private Action<GameObject> onCreated;
	}
}
