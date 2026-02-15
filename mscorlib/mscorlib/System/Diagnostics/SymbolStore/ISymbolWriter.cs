using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a symbol writer for managed code.</summary>
	// Token: 0x020006E4 RID: 1764
	[ComVisible(true)]
	public interface ISymbolWriter
	{
		/// <summary>Closes <see cref="T:System.Diagnostics.SymbolStore.ISymbolWriter" /> and commits the symbols to the symbol store.</summary>
		// Token: 0x060037AE RID: 14254
		void Close();

		/// <summary>Closes the current method.</summary>
		// Token: 0x060037AF RID: 14255
		void CloseMethod();

		/// <summary>Closes the most recent namespace.</summary>
		// Token: 0x060037B0 RID: 14256
		void CloseNamespace();

		/// <summary>Defines a source document.</summary>
		/// <returns>The object that represents the document.</returns>
		/// <param name="url">The URL that identifies the document. </param>
		/// <param name="language">The document language. This parameter can be <see cref="F:System.Guid.Empty" />. </param>
		/// <param name="languageVendor">The identity of the vendor for the document language. This parameter can be <see cref="F:System.Guid.Empty" />. </param>
		/// <param name="documentType">The type of the document. This parameter can be <see cref="F:System.Guid.Empty" />. </param>
		// Token: 0x060037B1 RID: 14257
		ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType);

		/// <summary>Defines a single variable in the current lexical scope.</summary>
		/// <param name="name">The local variable name. </param>
		/// <param name="attributes">A bitwise combination of the local variable attributes. </param>
		/// <param name="signature">The local variable signature. </param>
		/// <param name="addrKind">The address types for <paramref name="addr1" />, <paramref name="addr2" />, and <paramref name="addr3" />. </param>
		/// <param name="addr1">The first address for the local variable specification. </param>
		/// <param name="addr2">The second address for the local variable specification. </param>
		/// <param name="addr3">The third address for the local variable specification. </param>
		/// <param name="startOffset">The start offset for the variable. If this parameter is zero, it is ignored and the variable is defined throughout the entire scope. If the parameter is nonzero, the variable falls within the offsets of the current scope. </param>
		/// <param name="endOffset">The end offset for the variable. If this parameter is zero, it is ignored and the variable is defined throughout the entire scope. If the parameter is nonzero, the variable falls within the offsets of the current scope. </param>
		// Token: 0x060037B2 RID: 14258
		void DefineLocalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3, int startOffset, int endOffset);

		/// <summary>Defines a group of sequence points within the current method.</summary>
		/// <param name="document">The document object for which the sequence points are being defined. </param>
		/// <param name="offsets">The sequence point offsets measured from the beginning of methods. </param>
		/// <param name="lines">The document lines for the sequence points. </param>
		/// <param name="columns">The document positions for the sequence points. </param>
		/// <param name="endLines">The document end lines for the sequence points. </param>
		/// <param name="endColumns">The document end positions for the sequence points. </param>
		// Token: 0x060037B3 RID: 14259
		void DefineSequencePoints(ISymbolDocumentWriter document, int[] offsets, int[] lines, int[] columns, int[] endLines, int[] endColumns);

		/// <summary>Sets the metadata emitter interface to associate with a writer.</summary>
		/// <param name="emitter">The metadata emitter interface. </param>
		/// <param name="filename">The file name for which the debugging symbols are written. Some writers require a file name, and others do not. If a file name is specified for a writer that does not use file names, this parameter is ignored. </param>
		/// <param name="fFullBuild">true indicates that this is a full rebuild; false indicates that this is an incremental compilation. </param>
		// Token: 0x060037B4 RID: 14260
		void Initialize(IntPtr emitter, string filename, bool fFullBuild);

		/// <summary>Opens a method to place symbol information into.</summary>
		/// <param name="method">The metadata token for the method to be opened. </param>
		// Token: 0x060037B5 RID: 14261
		void OpenMethod(SymbolToken method);

		/// <summary>Opens a new namespace.</summary>
		/// <param name="name">The name of the new namespace. </param>
		// Token: 0x060037B6 RID: 14262
		void OpenNamespace(string name);

		/// <summary>Specifies the true start and end of a method within a source file. Use <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.SetMethodSourceRange(System.Diagnostics.SymbolStore.ISymbolDocumentWriter,System.Int32,System.Int32,System.Diagnostics.SymbolStore.ISymbolDocumentWriter,System.Int32,System.Int32)" /> to specify the extent of a method, independent of the sequence points that exist within the method.</summary>
		/// <param name="startDoc">The document that contains the starting position. </param>
		/// <param name="startLine">The starting line number. </param>
		/// <param name="startColumn">The starting column. </param>
		/// <param name="endDoc">The document that contains the ending position. </param>
		/// <param name="endLine">The ending line number. </param>
		/// <param name="endColumn">The ending column number. </param>
		// Token: 0x060037B7 RID: 14263
		void SetMethodSourceRange(ISymbolDocumentWriter startDoc, int startLine, int startColumn, ISymbolDocumentWriter endDoc, int endLine, int endColumn);

		/// <summary>Defines an attribute when given the attribute name and the attribute value.</summary>
		/// <param name="parent">The metadata token for which the attribute is being defined. </param>
		/// <param name="name">The attribute name. </param>
		/// <param name="data">The attribute value. </param>
		// Token: 0x060037B8 RID: 14264
		void SetSymAttribute(SymbolToken parent, string name, byte[] data);
	}
}
