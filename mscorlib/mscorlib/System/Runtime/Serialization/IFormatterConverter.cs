using System;

namespace System.Runtime.Serialization
{
	/// <summary>Provides the connection between an instance of <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and the formatter-provided class best suited to parse the data inside the <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</summary>
	// Token: 0x020004A5 RID: 1189
	[CLSCompliant(false)]
	public interface IFormatterConverter
	{
		/// <summary>Converts a value to the given <see cref="T:System.Type" />.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		/// <param name="type">The <see cref="T:System.Type" /> into which <paramref name="value" /> is to be converted. </param>
		// Token: 0x06002625 RID: 9765
		object Convert(object value, Type type);

		/// <summary>Converts a value to a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x06002626 RID: 9766
		bool ToBoolean(object value);

		/// <summary>Converts a value to a 32-bit signed integer.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x06002627 RID: 9767
		int ToInt32(object value);

		/// <summary>Converts a value to a 64-bit signed integer.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x06002628 RID: 9768
		long ToInt64(object value);

		/// <summary>Converts a value to a single-precision floating-point number.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x06002629 RID: 9769
		float ToSingle(object value);

		/// <summary>Converts a value to a <see cref="T:System.String" />.</summary>
		/// <returns>The converted <paramref name="value" />.</returns>
		/// <param name="value">The object to be converted. </param>
		// Token: 0x0600262A RID: 9770
		string ToString(object value);
	}
}
