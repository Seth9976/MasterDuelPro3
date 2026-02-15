using System;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x0200005E RID: 94
	public abstract class ResourceProviderBase : IResourceProvider, IInitializableObject
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00009058 File Offset: 0x00007258
		public virtual string ProviderId
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_ProviderId))
				{
					this.m_ProviderId = base.GetType().FullName;
				}
				return this.m_ProviderId;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000907E File Offset: 0x0000727E
		public virtual bool Initialize(string id, string data)
		{
			this.m_ProviderId = id;
			return !string.IsNullOrEmpty(this.m_ProviderId);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00009095 File Offset: 0x00007295
		public virtual bool CanProvide(Type t, IResourceLocation location)
		{
			return this.GetDefaultType(location).IsAssignableFrom(t);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000090A4 File Offset: 0x000072A4
		public override string ToString()
		{
			return this.ProviderId;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00006444 File Offset: 0x00004644
		public virtual void Release(IResourceLocation location, object obj)
		{
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000090AC File Offset: 0x000072AC
		public virtual Type GetDefaultType(IResourceLocation location)
		{
			return typeof(object);
		}

		// Token: 0x06000219 RID: 537
		public abstract void Provide(ProvideHandle provideHandle);

		// Token: 0x0600021A RID: 538 RVA: 0x000090B8 File Offset: 0x000072B8
		public virtual AsyncOperationHandle<bool> InitializeAsync(ResourceManager rm, string id, string data)
		{
			ResourceProviderBase.BaseInitAsyncOp baseInitOp = new ResourceProviderBase.BaseInitAsyncOp();
			baseInitOp.Init(() => this.Initialize(id, data));
			return rm.StartOperation<bool>(baseInitOp, default(AsyncOperationHandle));
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00009108 File Offset: 0x00007308
		ProviderBehaviourFlags IResourceProvider.BehaviourFlags
		{
			get
			{
				return this.m_BehaviourFlags;
			}
		}

		// Token: 0x040000F1 RID: 241
		protected string m_ProviderId;

		// Token: 0x040000F2 RID: 242
		protected ProviderBehaviourFlags m_BehaviourFlags;

		// Token: 0x0200005F RID: 95
		private class BaseInitAsyncOp : AsyncOperationBase<bool>
		{
			// Token: 0x0600021D RID: 541 RVA: 0x00009110 File Offset: 0x00007310
			public void Init(Func<bool> callback)
			{
				this.m_CallBack = callback;
			}

			// Token: 0x0600021E RID: 542 RVA: 0x00009119 File Offset: 0x00007319
			protected override bool InvokeWaitForCompletion()
			{
				ResourceManager rm = this.m_RM;
				if (rm != null)
				{
					rm.Update(Time.unscaledDeltaTime);
				}
				if (!this.HasExecuted)
				{
					base.InvokeExecute();
				}
				return true;
			}

			// Token: 0x0600021F RID: 543 RVA: 0x00009140 File Offset: 0x00007340
			protected override void Execute()
			{
				if (this.m_CallBack != null)
				{
					base.Complete(this.m_CallBack(), true, "");
					return;
				}
				base.Complete(true, true, "");
			}

			// Token: 0x040000F3 RID: 243
			private Func<bool> m_CallBack;
		}
	}
}
