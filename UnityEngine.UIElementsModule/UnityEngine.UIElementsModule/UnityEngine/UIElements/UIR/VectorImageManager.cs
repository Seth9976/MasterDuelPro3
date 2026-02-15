using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000566 RID: 1382
	internal class VectorImageManager : IDisposable
	{
		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x060025F3 RID: 9715 RVA: 0x00096EEC File Offset: 0x000950EC
		public Texture2D atlas
		{
			get
			{
				GradientSettingsAtlas gradientSettingsAtlas = this.m_GradientSettingsAtlas;
				return (gradientSettingsAtlas != null) ? gradientSettingsAtlas.atlas : null;
			}
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x00096F10 File Offset: 0x00095110
		public VectorImageManager(AtlasBase atlas)
		{
			VectorImageManager.instances.Add(this);
			this.m_Atlas = atlas;
			this.m_Registered = new Dictionary<VectorImage, VectorImageRenderInfo>(32);
			this.m_RenderInfoPool = new VectorImageRenderInfoPool();
			this.m_GradientRemapPool = new GradientRemapPool();
			this.m_GradientSettingsAtlas = new GradientSettingsAtlas(4096);
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x060025F5 RID: 9717 RVA: 0x00096F6B File Offset: 0x0009516B
		// (set) Token: 0x060025F6 RID: 9718 RVA: 0x00096F73 File Offset: 0x00095173
		private protected bool disposed { protected get; private set; }

		// Token: 0x060025F7 RID: 9719 RVA: 0x00096F7C File Offset: 0x0009517C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x00096F90 File Offset: 0x00095190
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_Registered.Clear();
					this.m_RenderInfoPool.Clear();
					this.m_GradientRemapPool.Clear();
					this.m_GradientSettingsAtlas.Dispose();
					VectorImageManager.instances.Remove(this);
				}
				this.disposed = true;
			}
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x00096FF8 File Offset: 0x000951F8
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_GradientSettingsAtlas.Commit();
			}
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x00097028 File Offset: 0x00095228
		public GradientRemap AddUser(VectorImage vi, VisualElement context)
		{
			bool disposed = this.disposed;
			GradientRemap gradientRemap;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				gradientRemap = null;
			}
			else
			{
				bool flag = vi == null;
				if (flag)
				{
					gradientRemap = null;
				}
				else
				{
					VectorImageRenderInfo renderInfo;
					bool flag2 = this.m_Registered.TryGetValue(vi, out renderInfo);
					if (flag2)
					{
						renderInfo.useCount++;
					}
					else
					{
						renderInfo = this.Register(vi, context);
					}
					gradientRemap = renderInfo.firstGradientRemap;
				}
			}
			return gradientRemap;
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x00097094 File Offset: 0x00095294
		private VectorImageRenderInfo Register(VectorImage vi, VisualElement context)
		{
			VectorImageRenderInfo renderInfo = this.m_RenderInfoPool.Get();
			renderInfo.useCount = 1;
			this.m_Registered[vi] = renderInfo;
			GradientSettings[] settings = vi.settings;
			bool flag = settings != null && settings.Length != 0;
			if (flag)
			{
				int gradientCount = vi.settings.Length;
				Alloc alloc = this.m_GradientSettingsAtlas.Add(gradientCount);
				bool flag2 = alloc.size > 0U;
				if (flag2)
				{
					TextureId atlasId;
					RectInt uvs;
					bool flag3 = this.m_Atlas.TryGetAtlas(context, vi.atlas, out atlasId, out uvs);
					if (flag3)
					{
						GradientRemap previous = null;
						for (int i = 0; i < gradientCount; i++)
						{
							GradientRemap current = this.m_GradientRemapPool.Get();
							bool flag4 = i > 0;
							if (flag4)
							{
								previous.next = current;
							}
							else
							{
								renderInfo.firstGradientRemap = current;
							}
							previous = current;
							current.origIndex = i;
							current.destIndex = (int)(alloc.start + (uint)i);
							GradientSettings gradient = vi.settings[i];
							RectInt location = gradient.location;
							location.x += uvs.x;
							location.y += uvs.y;
							current.location = location;
							current.atlas = atlasId;
						}
						this.m_GradientSettingsAtlas.Write(alloc, vi.settings, renderInfo.firstGradientRemap);
					}
					else
					{
						GradientRemap previous2 = null;
						for (int j = 0; j < gradientCount; j++)
						{
							GradientRemap current2 = this.m_GradientRemapPool.Get();
							bool flag5 = j > 0;
							if (flag5)
							{
								previous2.next = current2;
							}
							else
							{
								renderInfo.firstGradientRemap = current2;
							}
							previous2 = current2;
							current2.origIndex = j;
							current2.destIndex = (int)(alloc.start + (uint)j);
							current2.atlas = TextureId.invalid;
						}
						this.m_GradientSettingsAtlas.Write(alloc, vi.settings, null);
					}
				}
				else
				{
					bool flag6 = !this.m_LoggedExhaustedSettingsAtlas;
					if (flag6)
					{
						string text = "Exhausted max gradient settings (";
						string text2 = this.m_GradientSettingsAtlas.length.ToString();
						string text3 = ") for atlas: ";
						Texture2D atlas = this.m_GradientSettingsAtlas.atlas;
						Debug.LogError(text + text2 + text3 + ((atlas != null) ? atlas.name : null));
						this.m_LoggedExhaustedSettingsAtlas = true;
					}
				}
			}
			return renderInfo;
		}

		// Token: 0x04001324 RID: 4900
		public static List<VectorImageManager> instances = new List<VectorImageManager>(16);

		// Token: 0x04001325 RID: 4901
		private static ProfilerMarker s_MarkerRegister = new ProfilerMarker("UIR.VectorImageManager.Register");

		// Token: 0x04001326 RID: 4902
		private static ProfilerMarker s_MarkerUnregister = new ProfilerMarker("UIR.VectorImageManager.Unregister");

		// Token: 0x04001327 RID: 4903
		private readonly AtlasBase m_Atlas;

		// Token: 0x04001328 RID: 4904
		private Dictionary<VectorImage, VectorImageRenderInfo> m_Registered;

		// Token: 0x04001329 RID: 4905
		private VectorImageRenderInfoPool m_RenderInfoPool;

		// Token: 0x0400132A RID: 4906
		private GradientRemapPool m_GradientRemapPool;

		// Token: 0x0400132B RID: 4907
		private GradientSettingsAtlas m_GradientSettingsAtlas;

		// Token: 0x0400132C RID: 4908
		private bool m_LoggedExhaustedSettingsAtlas;
	}
}
