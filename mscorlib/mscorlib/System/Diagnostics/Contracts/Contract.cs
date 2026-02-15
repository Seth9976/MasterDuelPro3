using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

namespace System.Diagnostics.Contracts
{
	/// <summary>Contains static methods for representing program contracts such as preconditions, postconditions, and object invariants.</summary>
	// Token: 0x020006E0 RID: 1760
	public static class Contract
	{
		/// <summary>Determines whether all the elements in a collection exist within a function.</summary>
		/// <returns>true if and only if <paramref name="predicate" /> returns true for all elements of type <paramref name="T" /> in <paramref name="collection" />.</returns>
		/// <param name="collection">The collection from which elements of type <paramref name="T" /> will be drawn to pass to <paramref name="predicate" />.</param>
		/// <param name="predicate">The function to evaluate for the existence of all the elements in <paramref name="collection" />.</param>
		/// <typeparam name="T">The type that is contained in <paramref name="collection" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x060037A9 RID: 14249 RVA: 0x000DB52C File Offset: 0x000D972C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static bool ForAll<T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (T t in collection)
			{
				if (!predicate(t))
				{
					return false;
				}
			}
			return true;
		}
	}
}
