using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.IO.Enumeration
{
	// Token: 0x020007E9 RID: 2025
	public class FileSystemEnumerable<TResult> : IEnumerable<TResult>, IEnumerable
	{
		// Token: 0x0600413A RID: 16698 RVA: 0x000FBAE4 File Offset: 0x000F9CE4
		public FileSystemEnumerable(string directory, FileSystemEnumerable<TResult>.FindTransform transform, EnumerationOptions options = null)
		{
			if (directory == null)
			{
				throw new ArgumentNullException("directory");
			}
			this._directory = directory;
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			this._transform = transform;
			this._options = options ?? EnumerationOptions.Default;
			this._enumerator = new FileSystemEnumerable<TResult>.DelegateEnumerator(this);
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x0600413B RID: 16699 RVA: 0x000FBB3F File Offset: 0x000F9D3F
		// (set) Token: 0x0600413C RID: 16700 RVA: 0x000FBB47 File Offset: 0x000F9D47
		public FileSystemEnumerable<TResult>.FindPredicate ShouldIncludePredicate { get; set; }

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x0600413D RID: 16701 RVA: 0x000FBB50 File Offset: 0x000F9D50
		public FileSystemEnumerable<TResult>.FindPredicate ShouldRecursePredicate { get; }

		// Token: 0x0600413E RID: 16702 RVA: 0x000FBB58 File Offset: 0x000F9D58
		public IEnumerator<TResult> GetEnumerator()
		{
			return Interlocked.Exchange<FileSystemEnumerable<TResult>.DelegateEnumerator>(ref this._enumerator, null) ?? new FileSystemEnumerable<TResult>.DelegateEnumerator(this);
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x000FBB70 File Offset: 0x000F9D70
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04002129 RID: 8489
		private FileSystemEnumerable<TResult>.DelegateEnumerator _enumerator;

		// Token: 0x0400212A RID: 8490
		private readonly FileSystemEnumerable<TResult>.FindTransform _transform;

		// Token: 0x0400212B RID: 8491
		private readonly EnumerationOptions _options;

		// Token: 0x0400212C RID: 8492
		private readonly string _directory;

		// Token: 0x020007EA RID: 2026
		// (Invoke) Token: 0x06004141 RID: 16705
		public delegate bool FindPredicate(ref FileSystemEntry entry);

		// Token: 0x020007EB RID: 2027
		// (Invoke) Token: 0x06004143 RID: 16707
		public delegate TResult FindTransform(ref FileSystemEntry entry);

		// Token: 0x020007EC RID: 2028
		private sealed class DelegateEnumerator : FileSystemEnumerator<TResult>
		{
			// Token: 0x06004144 RID: 16708 RVA: 0x000FBB78 File Offset: 0x000F9D78
			public DelegateEnumerator(FileSystemEnumerable<TResult> enumerable)
				: base(enumerable._directory, enumerable._options)
			{
				this._enumerable = enumerable;
			}

			// Token: 0x06004145 RID: 16709 RVA: 0x000FBB93 File Offset: 0x000F9D93
			protected override TResult TransformEntry(ref FileSystemEntry entry)
			{
				return this._enumerable._transform(ref entry);
			}

			// Token: 0x06004146 RID: 16710 RVA: 0x000FBBA6 File Offset: 0x000F9DA6
			protected override bool ShouldRecurseIntoEntry(ref FileSystemEntry entry)
			{
				FileSystemEnumerable<TResult>.FindPredicate shouldRecursePredicate = this._enumerable.ShouldRecursePredicate;
				return shouldRecursePredicate == null || shouldRecursePredicate(ref entry);
			}

			// Token: 0x06004147 RID: 16711 RVA: 0x000FBBBF File Offset: 0x000F9DBF
			protected override bool ShouldIncludeEntry(ref FileSystemEntry entry)
			{
				FileSystemEnumerable<TResult>.FindPredicate shouldIncludePredicate = this._enumerable.ShouldIncludePredicate;
				return shouldIncludePredicate == null || shouldIncludePredicate(ref entry);
			}

			// Token: 0x0400212F RID: 8495
			private readonly FileSystemEnumerable<TResult> _enumerable;
		}
	}
}
