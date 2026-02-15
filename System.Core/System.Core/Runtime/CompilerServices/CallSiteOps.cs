using System;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Runtime.CompilerServices
{
	/// <summary>Creates and caches binding rules.</summary>
	// Token: 0x02000115 RID: 277
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class CallSiteOps
	{
		/// <summary>Creates an instance of a dynamic call site used for cache lookup.</summary>
		/// <returns>The new call site.</returns>
		/// <param name="site">An instance of the dynamic call site.</param>
		/// <typeparam name="T">The type of the delegate of the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</typeparam>
		// Token: 0x0600096C RID: 2412 RVA: 0x000256AF File Offset: 0x000238AF
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static CallSite<T> CreateMatchmaker<T>(CallSite<T> site) where T : class
		{
			CallSite<T> callSite = site.CreateMatchMaker();
			callSite._match = true;
			return callSite;
		}

		/// <summary>Checks if a dynamic site requires an update.</summary>
		/// <returns>true if rule does not need updating, false otherwise.</returns>
		/// <param name="site">An instance of the dynamic call site.</param>
		// Token: 0x0600096D RID: 2413 RVA: 0x000256BE File Offset: 0x000238BE
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool SetNotMatched(CallSite site)
		{
			bool match = site._match;
			site._match = false;
			return match;
		}

		/// <summary>Checks whether the executed rule matched</summary>
		/// <returns>true if rule matched, false otherwise.</returns>
		/// <param name="site">An instance of the dynamic call site.</param>
		// Token: 0x0600096E RID: 2414 RVA: 0x000256CD File Offset: 0x000238CD
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool GetMatch(CallSite site)
		{
			return site._match;
		}

		/// <summary>Clears the match flag on the matchmaker call site.</summary>
		/// <param name="site">An instance of the dynamic call site.</param>
		// Token: 0x0600096F RID: 2415 RVA: 0x000256D5 File Offset: 0x000238D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static void ClearMatch(CallSite site)
		{
			site._match = true;
		}

		/// <summary>Adds a rule to the cache maintained on the dynamic call site.</summary>
		/// <param name="site">An instance of the dynamic call site.</param>
		/// <param name="rule">An instance of the call site rule.</param>
		/// <typeparam name="T">The type of the delegate of the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</typeparam>
		// Token: 0x06000970 RID: 2416 RVA: 0x000256DE File Offset: 0x000238DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static void AddRule<T>(CallSite<T> site, T rule) where T : class
		{
			site.AddRule(rule);
		}

		/// <summary>Updates rules in the cache.</summary>
		/// <param name="this">An instance of the dynamic call site.</param>
		/// <param name="matched">The matched rule index.</param>
		/// <typeparam name="T">The type of the delegate of the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</typeparam>
		// Token: 0x06000971 RID: 2417 RVA: 0x000256E7 File Offset: 0x000238E7
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void UpdateRules<T>(CallSite<T> @this, int matched) where T : class
		{
			if (matched > 1)
			{
				@this.MoveRule(matched);
			}
		}

		/// <summary>Gets the dynamic binding rules from the call site.</summary>
		/// <returns>An array of dynamic binding rules.</returns>
		/// <param name="site">An instance of the dynamic call site.</param>
		/// <typeparam name="T">The type of the delegate of the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</typeparam>
		// Token: 0x06000972 RID: 2418 RVA: 0x000256F4 File Offset: 0x000238F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static T[] GetRules<T>(CallSite<T> site) where T : class
		{
			return site.Rules;
		}

		/// <summary>Retrieves binding rule cache.</summary>
		/// <returns>The cache.</returns>
		/// <param name="site">An instance of the dynamic call site.</param>
		/// <typeparam name="T">The type of the delegate of the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</typeparam>
		// Token: 0x06000973 RID: 2419 RVA: 0x000256FC File Offset: 0x000238FC
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static RuleCache<T> GetRuleCache<T>(CallSite<T> site) where T : class
		{
			return site.Binder.GetRuleCache<T>();
		}

		/// <summary>Moves the binding rule within the cache.</summary>
		/// <param name="cache">The call site rule cache.</param>
		/// <param name="rule">An instance of the call site rule.</param>
		/// <param name="i">An index of the call site rule.</param>
		/// <typeparam name="T">The type of the delegate of the <see cref="T:System.Runtime.CompilerServices.CallSite" />. </typeparam>
		// Token: 0x06000974 RID: 2420 RVA: 0x00025709 File Offset: 0x00023909
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void MoveRule<T>(RuleCache<T> cache, T rule, int i) where T : class
		{
			if (i > 1)
			{
				cache.MoveRule(rule, i);
			}
		}

		/// <summary>Searches the dynamic rule cache for rules applicable to the dynamic operation.</summary>
		/// <returns>The collection of applicable rules.</returns>
		/// <param name="cache">The cache.</param>
		/// <typeparam name="T">The type of the delegate of the <see cref="T:System.Runtime.CompilerServices.CallSite" />. </typeparam>
		// Token: 0x06000975 RID: 2421 RVA: 0x00025717 File Offset: 0x00023917
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static T[] GetCachedRules<T>(RuleCache<T> cache) where T : class
		{
			return cache.GetRules();
		}

		/// <summary>Updates the call site target with a new rule based on the arguments.</summary>
		/// <returns>The new call site target.</returns>
		/// <param name="binder">The call site binder.</param>
		/// <param name="site">An instance of the dynamic call site.</param>
		/// <param name="args">Arguments to the call site.</param>
		/// <typeparam name="T">The type of the delegate of the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</typeparam>
		// Token: 0x06000976 RID: 2422 RVA: 0x0002571F File Offset: 0x0002391F
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static T Bind<T>(CallSiteBinder binder, CallSite<T> site, object[] args) where T : class
		{
			return binder.BindCore<T>(site, args);
		}
	}
}
