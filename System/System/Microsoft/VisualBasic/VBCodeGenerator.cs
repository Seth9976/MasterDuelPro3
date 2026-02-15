using System;
using System.CodeDom.Compiler;

namespace Microsoft.VisualBasic
{
	// Token: 0x020000E2 RID: 226
	internal sealed class VBCodeGenerator : CodeCompiler
	{
		// Token: 0x04000376 RID: 886
		private static readonly char[] s_periodArray = new char[] { '.' };

		// Token: 0x04000377 RID: 887
		private static readonly string[][] s_keywords = new string[][]
		{
			null,
			new string[] { "as", "do", "if", "in", "is", "me", "of", "on", "or", "to" },
			new string[]
			{
				"and", "dim", "end", "for", "get", "let", "lib", "mod", "new", "not",
				"rem", "set", "sub", "try", "xor"
			},
			new string[]
			{
				"ansi", "auto", "byte", "call", "case", "cdbl", "cdec", "char", "cint", "clng",
				"cobj", "csng", "cstr", "date", "each", "else", "enum", "exit", "goto", "like",
				"long", "loop", "next", "step", "stop", "then", "true", "wend", "when", "with"
			},
			new string[]
			{
				"alias", "byref", "byval", "catch", "cbool", "cbyte", "cchar", "cdate", "class", "const",
				"ctype", "cuint", "culng", "endif", "erase", "error", "event", "false", "gosub", "isnot",
				"redim", "sbyte", "short", "throw", "ulong", "until", "using", "while"
			},
			new string[]
			{
				"csbyte", "cshort", "double", "elseif", "friend", "global", "module", "mybase", "object", "option",
				"orelse", "public", "resume", "return", "select", "shared", "single", "static", "string", "typeof",
				"ushort"
			},
			new string[]
			{
				"andalso", "boolean", "cushort", "decimal", "declare", "default", "finally", "gettype", "handles", "imports",
				"integer", "myclass", "nothing", "partial", "private", "shadows", "trycast", "unicode", "variant"
			},
			new string[]
			{
				"assembly", "continue", "delegate", "function", "inherits", "operator", "optional", "preserve", "property", "readonly",
				"synclock", "uinteger", "widening"
			},
			new string[] { "addressof", "interface", "namespace", "narrowing", "overloads", "overrides", "protected", "structure", "writeonly" },
			new string[] { "addhandler", "directcast", "implements", "paramarray", "raiseevent", "withevents" },
			new string[] { "mustinherit", "overridable" },
			new string[] { "mustoverride" },
			new string[] { "removehandler" },
			new string[] { "class_finalize", "notinheritable", "notoverridable" },
			null,
			new string[] { "class_initialize" }
		};
	}
}
