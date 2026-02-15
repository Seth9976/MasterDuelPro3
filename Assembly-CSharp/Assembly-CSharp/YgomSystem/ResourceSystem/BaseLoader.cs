using System;
using System.Collections;
using UnityEngine;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006D9 RID: 1753
	public abstract class BaseLoader : IResourceLoader
	{
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060036B1 RID: 14001 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool DisablePathForErrorHandler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Initialize()
		{
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Load(Resource res, uint crc)
		{
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IEnumerator LoadAsync(Resource res, uint crc)
		{
			return null;
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void LateUpdate()
		{
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void ClearCache()
		{
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x0000216A File Offset: 0x0000036A
		protected byte[] decompressedData(byte[] data)
		{
			return null;
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x0000216A File Offset: 0x0000036A
		protected byte[] decompressedData(TextAsset textasset)
		{
			return null;
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool checkLoadedAssets(Resource res)
		{
			return false;
		}
	}
}
