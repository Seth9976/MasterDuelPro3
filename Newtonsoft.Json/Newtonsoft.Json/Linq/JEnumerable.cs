using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x0200016B RID: 363
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct JEnumerable<[Nullable(0)] T> : IJEnumerable<T>, IEnumerable<T>, IEnumerable, IEquatable<JEnumerable<T>> where T : JToken
	{
		// Token: 0x06000BD0 RID: 3024 RVA: 0x00035DF6 File Offset: 0x00033FF6
		public JEnumerable(IEnumerable<T> enumerable)
		{
			ValidationUtils.ArgumentNotNull(enumerable, "enumerable");
			this._enumerable = enumerable;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00035E0A File Offset: 0x0003400A
		public IEnumerator<T> GetEnumerator()
		{
			return (this._enumerable ?? JEnumerable<T>.Empty).GetEnumerator();
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00035E25 File Offset: 0x00034025
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170001F0 RID: 496
		public IJEnumerable<JToken> this[object key]
		{
			get
			{
				if (this._enumerable == null)
				{
					return JEnumerable<JToken>.Empty;
				}
				return new JEnumerable<JToken>(this._enumerable.Values(key));
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00035E58 File Offset: 0x00034058
		public bool Equals([Nullable(new byte[] { 0, 1 })] JEnumerable<T> other)
		{
			return object.Equals(this._enumerable, other._enumerable);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00035E6C File Offset: 0x0003406C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is JEnumerable<T>)
			{
				JEnumerable<T> jenumerable = (JEnumerable<T>)obj;
				return this.Equals(jenumerable);
			}
			return false;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00035E91 File Offset: 0x00034091
		public override int GetHashCode()
		{
			if (this._enumerable == null)
			{
				return 0;
			}
			return this._enumerable.GetHashCode();
		}

		// Token: 0x040006BD RID: 1725
		[Nullable(new byte[] { 0, 1 })]
		public static readonly JEnumerable<T> Empty = new JEnumerable<T>(Enumerable.Empty<T>());

		// Token: 0x040006BE RID: 1726
		private readonly IEnumerable<T> _enumerable;
	}
}
