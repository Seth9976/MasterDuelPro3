using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200011D RID: 285
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonDynamicContract : JsonContainerContract
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00027B3A File Offset: 0x00025D3A
		public JsonPropertyCollection Properties { get; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00027B42 File Offset: 0x00025D42
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x00027B4A File Offset: 0x00025D4A
		[Nullable(new byte[] { 2, 1, 1 })]
		public Func<string, string> PropertyNameResolver
		{
			[return: Nullable(new byte[] { 2, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1 })]
			set;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00027B53 File Offset: 0x00025D53
		private static CallSite<Func<CallSite, object, object>> CreateCallSiteGetter(string name)
		{
			return CallSite<Func<CallSite, object, object>>.Create(new NoThrowGetBinderMember((GetMemberBinder)DynamicUtils.BinderWrapper.GetMember(name, typeof(DynamicUtils))));
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00027B74 File Offset: 0x00025D74
		[return: Nullable(new byte[] { 1, 1, 1, 1, 2, 1 })]
		private static CallSite<Func<CallSite, object, object, object>> CreateCallSiteSetter(string name)
		{
			return CallSite<Func<CallSite, object, object, object>>.Create(new NoThrowSetBinderMember((SetMemberBinder)DynamicUtils.BinderWrapper.SetMember(name, typeof(DynamicUtils))));
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00027B98 File Offset: 0x00025D98
		public JsonDynamicContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Dynamic;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00027BF4 File Offset: 0x00025DF4
		internal bool TryGetMember(IDynamicMetaObjectProvider dynamicProvider, string name, [Nullable(2)] out object value)
		{
			ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");
			CallSite<Func<CallSite, object, object>> callSite = this._callSiteGetters.Get(name);
			object obj = callSite.Target(callSite, dynamicProvider);
			if (obj != NoThrowExpressionVisitor.ErrorResult)
			{
				value = obj;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00027C38 File Offset: 0x00025E38
		internal bool TrySetMember(IDynamicMetaObjectProvider dynamicProvider, string name, [Nullable(2)] object value)
		{
			ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");
			CallSite<Func<CallSite, object, object, object>> callSite = this._callSiteSetters.Get(name);
			return callSite.Target(callSite, dynamicProvider, value) != NoThrowExpressionVisitor.ErrorResult;
		}

		// Token: 0x04000552 RID: 1362
		private readonly ThreadSafeStore<string, CallSite<Func<CallSite, object, object>>> _callSiteGetters = new ThreadSafeStore<string, CallSite<Func<CallSite, object, object>>>(new Func<string, CallSite<Func<CallSite, object, object>>>(JsonDynamicContract.CreateCallSiteGetter));

		// Token: 0x04000553 RID: 1363
		[Nullable(new byte[] { 1, 1, 1, 1, 1, 1, 2, 1 })]
		private readonly ThreadSafeStore<string, CallSite<Func<CallSite, object, object, object>>> _callSiteSetters = new ThreadSafeStore<string, CallSite<Func<CallSite, object, object, object>>>(new Func<string, CallSite<Func<CallSite, object, object, object>>>(JsonDynamicContract.CreateCallSiteSetter));
	}
}
