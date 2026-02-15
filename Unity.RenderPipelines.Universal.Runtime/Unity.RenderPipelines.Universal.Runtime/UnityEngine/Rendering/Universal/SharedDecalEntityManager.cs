using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000168 RID: 360
	internal class SharedDecalEntityManager : IDisposable
	{
		// Token: 0x060007BC RID: 1980 RVA: 0x000250A8 File Offset: 0x000232A8
		public DecalEntityManager Get()
		{
			if (this.m_DecalEntityManager == null)
			{
				this.m_DecalEntityManager = new DecalEntityManager();
				foreach (DecalProjector decalProjector in Object.FindObjectsByType<DecalProjector>(FindObjectsSortMode.InstanceID))
				{
					if (decalProjector.isActiveAndEnabled && !this.m_DecalEntityManager.IsValid(decalProjector.decalEntity))
					{
						decalProjector.decalEntity = this.m_DecalEntityManager.CreateDecalEntity(decalProjector);
					}
				}
				DecalProjector.onDecalAdd += this.OnDecalAdd;
				DecalProjector.onDecalRemove += this.OnDecalRemove;
				DecalProjector.onDecalPropertyChange += this.OnDecalPropertyChange;
				DecalProjector.onDecalMaterialChange += this.OnDecalMaterialChange;
				DecalProjector.onAllDecalPropertyChange += this.OnAllDecalPropertyChange;
			}
			this.m_ReferenceCounter++;
			return this.m_DecalEntityManager;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0002517A File Offset: 0x0002337A
		public void Release(DecalEntityManager decalEntityManager)
		{
			if (this.m_ReferenceCounter == 0)
			{
				return;
			}
			this.m_ReferenceCounter--;
			if (this.m_ReferenceCounter == 0)
			{
				this.Dispose();
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000251A4 File Offset: 0x000233A4
		public void Dispose()
		{
			this.m_DecalEntityManager.Dispose();
			this.m_DecalEntityManager = null;
			this.m_ReferenceCounter = 0;
			DecalProjector.onDecalAdd -= this.OnDecalAdd;
			DecalProjector.onDecalRemove -= this.OnDecalRemove;
			DecalProjector.onDecalPropertyChange -= this.OnDecalPropertyChange;
			DecalProjector.onDecalMaterialChange -= this.OnDecalMaterialChange;
			DecalProjector.onAllDecalPropertyChange -= this.OnAllDecalPropertyChange;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0002521F File Offset: 0x0002341F
		private void OnDecalAdd(DecalProjector decalProjector)
		{
			if (!this.m_DecalEntityManager.IsValid(decalProjector.decalEntity))
			{
				decalProjector.decalEntity = this.m_DecalEntityManager.CreateDecalEntity(decalProjector);
			}
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00025246 File Offset: 0x00023446
		private void OnDecalRemove(DecalProjector decalProjector)
		{
			this.m_DecalEntityManager.DestroyDecalEntity(decalProjector.decalEntity);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00025259 File Offset: 0x00023459
		private void OnDecalPropertyChange(DecalProjector decalProjector)
		{
			if (this.m_DecalEntityManager.IsValid(decalProjector.decalEntity))
			{
				this.m_DecalEntityManager.UpdateDecalEntityData(decalProjector.decalEntity, decalProjector);
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00025280 File Offset: 0x00023480
		private void OnAllDecalPropertyChange()
		{
			this.m_DecalEntityManager.UpdateAllDecalEntitiesData();
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0002528D File Offset: 0x0002348D
		private void OnDecalMaterialChange(DecalProjector decalProjector)
		{
			this.OnDecalRemove(decalProjector);
			this.OnDecalAdd(decalProjector);
		}

		// Token: 0x04000833 RID: 2099
		private DecalEntityManager m_DecalEntityManager;

		// Token: 0x04000834 RID: 2100
		private int m_ReferenceCounter;
	}
}
