using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace System.Runtime.CompilerServices
{
	/// <summary>Class responsible for runtime binding of the dynamic operations on the dynamic call site.</summary>
	// Token: 0x02000113 RID: 275
	public abstract class CallSiteBinder
	{
		/// <summary>Gets a label that can be used to cause the binding to be updated. It indicates that the expression's binding is no longer valid. This is typically used when the "version" of a dynamic object has changed.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.LabelTarget" /> object representing a label that can be used to trigger the binding update.</returns>
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x000253EE File Offset: 0x000235EE
		public static LabelTarget UpdateLabel { get; } = Expression.Label("CallSiteBinder.UpdateLabel");

		/// <summary>Performs the runtime binding of the dynamic operation on a set of arguments.</summary>
		/// <returns>An Expression that performs tests on the dynamic operation arguments, and performs the dynamic operation if the tests are valid. If the tests fail on subsequent occurrences of the dynamic operation, Bind will be called again to produce a new <see cref="T:System.Linq.Expressions.Expression" /> for the new argument types.</returns>
		/// <param name="args">An array of arguments to the dynamic operation.</param>
		/// <param name="parameters">The array of <see cref="T:System.Linq.Expressions.ParameterExpression" /> instances that represent the parameters of the call site in the binding process.</param>
		/// <param name="returnLabel">A LabelTarget used to return the result of the dynamic binding.</param>
		// Token: 0x06000963 RID: 2403
		public abstract Expression Bind(object[] args, ReadOnlyCollection<ParameterExpression> parameters, LabelTarget returnLabel);

		/// <summary>Provides low-level runtime binding support. Classes can override this and provide a direct delegate for the implementation of rule. This can enable saving rules to disk, having specialized rules available at runtime, or providing a different caching policy.</summary>
		/// <returns>A new delegate which replaces the CallSite Target.</returns>
		/// <param name="site">The CallSite the bind is being performed for.</param>
		/// <param name="args">The arguments for the binder.</param>
		/// <typeparam name="T">The target type of the CallSite.</typeparam>
		// Token: 0x06000964 RID: 2404 RVA: 0x000253F8 File Offset: 0x000235F8
		public virtual T BindDelegate<T>(CallSite<T> site, object[] args) where T : class
		{
			return default(T);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00025410 File Offset: 0x00023610
		internal T BindCore<T>(CallSite<T> site, object[] args) where T : class
		{
			T t = this.BindDelegate<T>(site, args);
			if (t != null)
			{
				return t;
			}
			CallSiteBinder.LambdaSignature<T> instance = CallSiteBinder.LambdaSignature<T>.Instance;
			Expression expression = this.Bind(args, instance.Parameters, instance.ReturnLabel);
			if (expression == null)
			{
				throw Error.NoOrInvalidRuleProduced();
			}
			T t2 = CallSiteBinder.Stitch<T>(expression, instance).Compile();
			this.CacheTarget<T>(t2);
			return t2;
		}

		/// <summary>Adds a target to the cache of known targets. The cached targets will be scanned before calling BindDelegate to produce the new rule.</summary>
		/// <param name="target">The target delegate to be added to the cache.</param>
		/// <typeparam name="T">The type of target being added.</typeparam>
		// Token: 0x06000966 RID: 2406 RVA: 0x00025466 File Offset: 0x00023666
		protected void CacheTarget<T>(T target) where T : class
		{
			this.GetRuleCache<T>().AddRule(target);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00025474 File Offset: 0x00023674
		private static Expression<T> Stitch<T>(Expression binding, CallSiteBinder.LambdaSignature<T> signature) where T : class
		{
			Type typeFromHandle = typeof(CallSite<T>);
			ReadOnlyCollectionBuilder<Expression> readOnlyCollectionBuilder = new ReadOnlyCollectionBuilder<Expression>(3);
			readOnlyCollectionBuilder.Add(binding);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(CallSite), "$site");
			TrueReadOnlyCollection<ParameterExpression> trueReadOnlyCollection = signature.Parameters.AddFirst(parameterExpression);
			Expression expression = Expression.Label(CallSiteBinder.UpdateLabel);
			readOnlyCollectionBuilder.Add(expression);
			readOnlyCollectionBuilder.Add(Expression.Label(signature.ReturnLabel, Expression.Condition(Expression.Call(CachedReflectionInfo.CallSiteOps_SetNotMatched, parameterExpression), Expression.Default(signature.ReturnLabel.Type), Expression.Invoke(Expression.Property(Expression.Convert(parameterExpression, typeFromHandle), typeof(CallSite<T>).GetProperty("Update")), trueReadOnlyCollection))));
			return Expression.Lambda<T>(Expression.Block(readOnlyCollectionBuilder), "CallSite.Target", true, trueReadOnlyCollection);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00025538 File Offset: 0x00023738
		internal RuleCache<T> GetRuleCache<T>() where T : class
		{
			if (this.Cache == null)
			{
				Interlocked.CompareExchange<Dictionary<Type, object>>(ref this.Cache, new Dictionary<Type, object>(), null);
			}
			Dictionary<Type, object> cache = this.Cache;
			Dictionary<Type, object> dictionary = cache;
			object obj;
			lock (dictionary)
			{
				if (!cache.TryGetValue(typeof(T), out obj))
				{
					obj = (cache[typeof(T)] = new RuleCache<T>());
				}
			}
			return obj as RuleCache<T>;
		}

		// Token: 0x040002EA RID: 746
		internal Dictionary<Type, object> Cache;

		// Token: 0x02000114 RID: 276
		private sealed class LambdaSignature<T> where T : class
		{
			// Token: 0x17000192 RID: 402
			// (get) Token: 0x0600096A RID: 2410 RVA: 0x000255D1 File Offset: 0x000237D1
			internal static CallSiteBinder.LambdaSignature<T> Instance
			{
				get
				{
					if (CallSiteBinder.LambdaSignature<T>.s_instance == null)
					{
						CallSiteBinder.LambdaSignature<T>.s_instance = new CallSiteBinder.LambdaSignature<T>();
					}
					return CallSiteBinder.LambdaSignature<T>.s_instance;
				}
			}

			// Token: 0x0600096B RID: 2411 RVA: 0x000255EC File Offset: 0x000237EC
			private LambdaSignature()
			{
				Type typeFromHandle = typeof(T);
				if (!typeFromHandle.IsSubclassOf(typeof(MulticastDelegate)))
				{
					throw Error.TypeParameterIsNotDelegate(typeFromHandle);
				}
				MethodInfo invokeMethod = typeFromHandle.GetInvokeMethod();
				ParameterInfo[] parametersCached = invokeMethod.GetParametersCached();
				if (parametersCached[0].ParameterType != typeof(CallSite))
				{
					throw Error.FirstArgumentMustBeCallSite();
				}
				ParameterExpression[] array = new ParameterExpression[parametersCached.Length - 1];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Expression.Parameter(parametersCached[i + 1].ParameterType, "$arg" + i.ToString());
				}
				this.Parameters = new TrueReadOnlyCollection<ParameterExpression>(array);
				this.ReturnLabel = Expression.Label(invokeMethod.GetReturnType());
			}

			// Token: 0x040002EC RID: 748
			private static CallSiteBinder.LambdaSignature<T> s_instance;

			// Token: 0x040002ED RID: 749
			internal readonly ReadOnlyCollection<ParameterExpression> Parameters;

			// Token: 0x040002EE RID: 750
			internal readonly LabelTarget ReturnLabel;
		}
	}
}
