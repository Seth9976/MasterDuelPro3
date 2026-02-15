using System;
using System.Diagnostics;
using System.Threading;

namespace System
{
	/// <summary>Provides support for lazy initialization.</summary>
	/// <typeparam name="T">The type of object that is being lazily initialized.</typeparam>
	// Token: 0x02000117 RID: 279
	[DebuggerTypeProxy(typeof(LazyDebugView<>))]
	[DebuggerDisplay("ThreadSafetyMode={Mode}, IsValueCreated={IsValueCreated}, IsValueFaulted={IsValueFaulted}, Value={ValueForDebugDisplay}")]
	[Serializable]
	public class Lazy<T>
	{
		// Token: 0x0600091D RID: 2333 RVA: 0x0002818D File Offset: 0x0002638D
		private static T CreateViaDefaultConstructor()
		{
			return (T)((object)LazyHelper.CreateViaDefaultConstructor(typeof(T)));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`1" /> class. When lazy initialization occurs, the default constructor of the target type is used.</summary>
		// Token: 0x0600091E RID: 2334 RVA: 0x000281A3 File Offset: 0x000263A3
		public Lazy()
			: this(null, LazyThreadSafetyMode.ExecutionAndPublication, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Lazy`1" /> class. When lazy initialization occurs, the specified initialization function is used.</summary>
		/// <param name="valueFactory">The delegate that is invoked to produce the lazily initialized value when it is needed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="valueFactory" /> is null. </exception>
		// Token: 0x0600091F RID: 2335 RVA: 0x000281AE File Offset: 0x000263AE
		public Lazy(Func<T> valueFactory)
			: this(valueFactory, LazyThreadSafetyMode.ExecutionAndPublication, false)
		{
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x000281B9 File Offset: 0x000263B9
		private Lazy(Func<T> valueFactory, LazyThreadSafetyMode mode, bool useDefaultConstructor)
		{
			if (valueFactory == null && !useDefaultConstructor)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this._factory = valueFactory;
			this._state = LazyHelper.Create(mode, useDefaultConstructor);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x000281E8 File Offset: 0x000263E8
		private void ViaConstructor()
		{
			this._value = Lazy<T>.CreateViaDefaultConstructor();
			this._state = null;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00028200 File Offset: 0x00026400
		private void ViaFactory(LazyThreadSafetyMode mode)
		{
			try
			{
				Func<T> factory = this._factory;
				if (factory == null)
				{
					throw new InvalidOperationException("ValueFactory attempted to access the Value property of this instance.");
				}
				this._factory = null;
				this._value = factory();
				this._state = null;
			}
			catch (Exception ex)
			{
				this._state = new LazyHelper(mode, ex);
				throw;
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00028264 File Offset: 0x00026464
		private void ExecutionAndPublication(LazyHelper executionAndPublication, bool useDefaultConstructor)
		{
			lock (executionAndPublication)
			{
				if (this._state == executionAndPublication)
				{
					if (useDefaultConstructor)
					{
						this.ViaConstructor();
					}
					else
					{
						this.ViaFactory(LazyThreadSafetyMode.ExecutionAndPublication);
					}
				}
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000282B8 File Offset: 0x000264B8
		private void PublicationOnly(LazyHelper publicationOnly, T possibleValue)
		{
			if (Interlocked.CompareExchange<LazyHelper>(ref this._state, LazyHelper.PublicationOnlyWaitForOtherThreadToPublish, publicationOnly) == publicationOnly)
			{
				this._factory = null;
				this._value = possibleValue;
				this._state = null;
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000282E5 File Offset: 0x000264E5
		private void PublicationOnlyViaConstructor(LazyHelper initializer)
		{
			this.PublicationOnly(initializer, Lazy<T>.CreateViaDefaultConstructor());
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x000282F4 File Offset: 0x000264F4
		private void PublicationOnlyViaFactory(LazyHelper initializer)
		{
			Func<T> factory = this._factory;
			if (factory == null)
			{
				this.PublicationOnlyWaitForOtherThreadToPublish();
				return;
			}
			this.PublicationOnly(initializer, factory());
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00028320 File Offset: 0x00026520
		private void PublicationOnlyWaitForOtherThreadToPublish()
		{
			SpinWait spinWait = default(SpinWait);
			while (this._state != null)
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00028348 File Offset: 0x00026548
		private T CreateValue()
		{
			LazyHelper state = this._state;
			if (state != null)
			{
				switch (state.State)
				{
				case LazyState.NoneViaConstructor:
					this.ViaConstructor();
					goto IL_0084;
				case LazyState.NoneViaFactory:
					this.ViaFactory(LazyThreadSafetyMode.None);
					goto IL_0084;
				case LazyState.PublicationOnlyViaConstructor:
					this.PublicationOnlyViaConstructor(state);
					goto IL_0084;
				case LazyState.PublicationOnlyViaFactory:
					this.PublicationOnlyViaFactory(state);
					goto IL_0084;
				case LazyState.PublicationOnlyWait:
					this.PublicationOnlyWaitForOtherThreadToPublish();
					goto IL_0084;
				case LazyState.ExecutionAndPublicationViaConstructor:
					this.ExecutionAndPublication(state, true);
					goto IL_0084;
				case LazyState.ExecutionAndPublicationViaFactory:
					this.ExecutionAndPublication(state, false);
					goto IL_0084;
				}
				state.ThrowException();
			}
			IL_0084:
			return this.Value;
		}

		/// <summary>Creates and returns a string representation of the <see cref="P:System.Lazy`1.Value" /> property for this instance.</summary>
		/// <returns>The result of calling the <see cref="M:System.Object.ToString" /> method on the <see cref="P:System.Lazy`1.Value" /> property for this instance, if the value has been created (that is, if the <see cref="P:System.Lazy`1.IsValueCreated" /> property returns true). Otherwise, a string indicating that the value has not been created. </returns>
		/// <exception cref="T:System.NullReferenceException">The <see cref="P:System.Lazy`1.Value" /> property is null.</exception>
		// Token: 0x06000929 RID: 2345 RVA: 0x000283E0 File Offset: 0x000265E0
		public override string ToString()
		{
			if (!this.IsValueCreated)
			{
				return "Value is not created.";
			}
			T value = this.Value;
			return value.ToString();
		}

		/// <summary>Gets a value that indicates whether a value has been created for this <see cref="T:System.Lazy`1" /> instance.</summary>
		/// <returns>true if a value has been created for this <see cref="T:System.Lazy`1" /> instance; otherwise, false.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0002840F File Offset: 0x0002660F
		public bool IsValueCreated
		{
			get
			{
				return this._state == null;
			}
		}

		/// <summary>Gets the lazily initialized value of the current <see cref="T:System.Lazy`1" /> instance.</summary>
		/// <returns>The lazily initialized value of the current <see cref="T:System.Lazy`1" /> instance.</returns>
		/// <exception cref="T:System.MemberAccessException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and permissions to access the constructor are missing. </exception>
		/// <exception cref="T:System.MissingMemberException">The <see cref="T:System.Lazy`1" /> instance is initialized to use the default constructor of the type that is being lazily initialized, and that type does not have a public, parameterless constructor. </exception>
		/// <exception cref="T:System.InvalidOperationException">The initialization function tries to access <see cref="P:System.Lazy`1.Value" /> on this instance. </exception>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0002841C File Offset: 0x0002661C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public T Value
		{
			get
			{
				if (this._state != null)
				{
					return this.CreateValue();
				}
				return this._value;
			}
		}

		// Token: 0x04000442 RID: 1090
		private volatile LazyHelper _state;

		// Token: 0x04000443 RID: 1091
		private Func<T> _factory;

		// Token: 0x04000444 RID: 1092
		private T _value;
	}
}
