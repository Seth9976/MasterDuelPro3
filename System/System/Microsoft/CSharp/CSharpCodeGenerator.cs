using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Microsoft.CSharp
{
	// Token: 0x020000E4 RID: 228
	internal sealed class CSharpCodeGenerator : ICodeCompiler, ICodeGenerator
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x000026E5 File Offset: 0x000008E5
		internal CSharpCodeGenerator()
		{
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000F2AE File Offset: 0x0000D4AE
		internal CSharpCodeGenerator(IDictionary<string, string> providerOptions)
		{
			this._provOptions = providerOptions;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000F2BD File Offset: 0x0000D4BD
		private string FileExtension
		{
			get
			{
				return ".cs";
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
		public bool Supports(GeneratorSupport support)
		{
			return (support & (GeneratorSupport.ArraysOfArrays | GeneratorSupport.EntryPointMethod | GeneratorSupport.GotoStatements | GeneratorSupport.MultidimensionalArrays | GeneratorSupport.StaticConstructors | GeneratorSupport.TryCatchStatements | GeneratorSupport.ReturnTypeAttributes | GeneratorSupport.DeclareValueTypes | GeneratorSupport.DeclareEnums | GeneratorSupport.DeclareDelegates | GeneratorSupport.DeclareInterfaces | GeneratorSupport.DeclareEvents | GeneratorSupport.AssemblyAttributes | GeneratorSupport.ParameterAttributes | GeneratorSupport.ReferenceParameters | GeneratorSupport.ChainedConstructorArguments | GeneratorSupport.NestedTypes | GeneratorSupport.MultipleInterfaceMembers | GeneratorSupport.PublicStaticMembers | GeneratorSupport.ComplexExpressions | GeneratorSupport.Win32Resources | GeneratorSupport.Resources | GeneratorSupport.PartialTypes | GeneratorSupport.GenericTypeReference | GeneratorSupport.GenericTypeDeclaration | GeneratorSupport.DeclareIndexerProperties)) == support;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000F2D0 File Offset: 0x0000D4D0
		public string CreateEscapedIdentifier(string name)
		{
			return CSharpHelpers.CreateEscapedIdentifier(name);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000F2D8 File Offset: 0x0000D4D8
		CompilerResults ICodeCompiler.CompileAssemblyFromSourceBatch(CompilerParameters options, string[] sources)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			CompilerResults compilerResults;
			try
			{
				compilerResults = this.FromSourceBatch(options, sources);
			}
			finally
			{
				options.TempFiles.SafeDelete();
			}
			return compilerResults;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000F31C File Offset: 0x0000D51C
		private CompilerResults FromSourceBatch(CompilerParameters options, string[] sources)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			string[] array = new string[sources.Length];
			for (int i = 0; i < sources.Length; i++)
			{
				string text = options.TempFiles.AddExtension(i.ToString() + this.FileExtension);
				using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.Read))
				{
					using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
					{
						streamWriter.Write(sources[i]);
						streamWriter.Flush();
					}
				}
				array[i] = text;
			}
			return this.FromFileBatch(options, array);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000F3E4 File Offset: 0x0000D5E4
		private CompilerResults FromFileBatch(CompilerParameters options, string[] fileNames)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			CompilerResults results = new CompilerResults(options.TempFiles);
			Process process = new Process();
			if (Path.DirectorySeparatorChar == '\\')
			{
				process.StartInfo.FileName = MonoToolsLocator.Mono;
				process.StartInfo.Arguments = "\"" + MonoToolsLocator.McsCSharpCompiler + "\" ";
			}
			else
			{
				process.StartInfo.FileName = MonoToolsLocator.McsCSharpCompiler;
			}
			ProcessStartInfo startInfo = process.StartInfo;
			startInfo.Arguments += CSharpCodeGenerator.BuildArgs(options, fileNames, this._provOptions);
			ManualResetEvent stderr_completed = new ManualResetEvent(false);
			ManualResetEvent stdout_completed = new ManualResetEvent(false);
			process.StartInfo.EnvironmentVariables.Remove("MONO_GC_PARAMS");
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs args)
			{
				if (args.Data != null)
				{
					results.Output.Add(args.Data);
					return;
				}
				stderr_completed.Set();
			};
			process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs args)
			{
				if (args.Data == null)
				{
					stdout_completed.Set();
				}
			};
			process.StartInfo.StandardOutputEncoding = (process.StartInfo.StandardErrorEncoding = Encoding.UTF8);
			try
			{
				process.Start();
			}
			catch (Exception ex)
			{
				Win32Exception ex2 = ex as Win32Exception;
				if (ex2 != null)
				{
					throw new SystemException(string.Format("Error running {0}: {1}", process.StartInfo.FileName, Win32Exception.GetErrorMessage(ex2.NativeErrorCode)));
				}
				throw;
			}
			try
			{
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();
				process.WaitForExit();
				results.NativeCompilerReturnValue = process.ExitCode;
			}
			finally
			{
				stderr_completed.WaitOne(TimeSpan.FromSeconds(30.0));
				stdout_completed.WaitOne(TimeSpan.FromSeconds(30.0));
				process.Close();
			}
			bool flag = true;
			foreach (string text in results.Output)
			{
				CompilerError compilerError = CSharpCodeGenerator.CreateErrorFromString(text);
				if (compilerError != null)
				{
					results.Errors.Add(compilerError);
					if (!compilerError.IsWarning)
					{
						flag = false;
					}
				}
			}
			if (results.Output.Count > 0)
			{
				results.Output.Insert(0, process.StartInfo.FileName + " " + process.StartInfo.Arguments + Environment.NewLine);
			}
			if (flag)
			{
				if (!File.Exists(options.OutputAssembly))
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (string text2 in results.Output)
					{
						stringBuilder.Append(text2 + Environment.NewLine);
					}
					throw new Exception("Compiler failed to produce the assembly. Output: '" + stringBuilder.ToString() + "'");
				}
				if (options.GenerateInMemory)
				{
					using (FileStream fileStream = File.OpenRead(options.OutputAssembly))
					{
						byte[] array = new byte[fileStream.Length];
						fileStream.Read(array, 0, array.Length);
						results.CompiledAssembly = Assembly.Load(array, null);
						fileStream.Close();
						goto IL_039F;
					}
				}
				results.PathToAssembly = options.OutputAssembly;
			}
			else
			{
				results.CompiledAssembly = null;
			}
			IL_039F:
			return results;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000F7D8 File Offset: 0x0000D9D8
		private static string BuildArgs(CompilerParameters options, string[] fileNames, IDictionary<string, string> providerOptions)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (options.GenerateExecutable)
			{
				stringBuilder.Append("/target:exe ");
			}
			else
			{
				stringBuilder.Append("/target:library ");
			}
			string privateBinPath = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
			if (privateBinPath != null && privateBinPath.Length > 0)
			{
				stringBuilder.AppendFormat("/lib:\"{0}\" ", privateBinPath);
			}
			if (options.Win32Resource != null)
			{
				stringBuilder.AppendFormat("/win32res:\"{0}\" ", options.Win32Resource);
			}
			if (options.IncludeDebugInformation)
			{
				stringBuilder.Append("/debug+ /optimize- ");
			}
			else
			{
				stringBuilder.Append("/debug- /optimize+ ");
			}
			if (options.TreatWarningsAsErrors)
			{
				stringBuilder.Append("/warnaserror ");
			}
			if (options.WarningLevel >= 0)
			{
				stringBuilder.AppendFormat("/warn:{0} ", options.WarningLevel);
			}
			if (options.OutputAssembly == null || options.OutputAssembly.Length == 0)
			{
				string text = (options.GenerateExecutable ? "exe" : "dll");
				options.OutputAssembly = CSharpCodeGenerator.GetTempFileNameWithExtension(options.TempFiles, text, !options.GenerateInMemory);
			}
			stringBuilder.AppendFormat("/out:\"{0}\" ", options.OutputAssembly);
			foreach (string text2 in options.ReferencedAssemblies)
			{
				if (text2 != null && text2.Length != 0)
				{
					stringBuilder.AppendFormat("/r:\"{0}\" ", text2);
				}
			}
			if (options.CompilerOptions != null)
			{
				stringBuilder.Append(options.CompilerOptions);
				stringBuilder.Append(" ");
			}
			foreach (string text3 in options.EmbeddedResources)
			{
				stringBuilder.AppendFormat("/resource:\"{0}\" ", text3);
			}
			foreach (string text4 in options.LinkedResources)
			{
				stringBuilder.AppendFormat("/linkresource:\"{0}\" ", text4);
			}
			if (providerOptions != null && providerOptions.Count > 0)
			{
				string text5;
				if (!providerOptions.TryGetValue("CompilerVersion", out text5))
				{
					text5 = "3.5";
				}
				if (text5.Length >= 1 && text5[0] == 'v')
				{
					text5 = text5.Substring(1);
				}
				if (!(text5 == "2.0"))
				{
					if (!(text5 == "3.5"))
					{
					}
				}
				else
				{
					stringBuilder.Append("/langversion:ISO-2 ");
				}
			}
			stringBuilder.Append("/noconfig ");
			stringBuilder.Append(" -- ");
			foreach (string text6 in fileNames)
			{
				stringBuilder.AppendFormat("\"{0}\" ", text6);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		private static CompilerError CreateErrorFromString(string error_string)
		{
			if (error_string.StartsWith("BETA"))
			{
				return null;
			}
			if (error_string == null || error_string == "")
			{
				return null;
			}
			CompilerError compilerError = new CompilerError();
			Match match = new Regex("\n\t\t\t^\n\t\t\t(\\s*(?<file>[^\\(]+)                         # filename (optional)\n\t\t\t (\\((?<line>\\d*)(,(?<column>\\d*[\\+]*))?\\))? # line+column (optional)\n\t\t\t :\\s+)?\n\t\t\t(?<level>\\w+)                               # error|warning\n\t\t\t\\s+\n\t\t\t(?<number>[^:]*\\d)                          # CS1234\n\t\t\t:\n\t\t\t\\s*\n\t\t\t(?<message>.*)$", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace).Match(error_string);
			if (match.Success)
			{
				if (string.Empty != match.Result("${file}"))
				{
					compilerError.FileName = match.Result("${file}");
				}
				if (string.Empty != match.Result("${line}"))
				{
					compilerError.Line = int.Parse(match.Result("${line}"));
				}
				if (string.Empty != match.Result("${column}"))
				{
					compilerError.Column = int.Parse(match.Result("${column}").Trim('+'));
				}
				string text = match.Result("${level}");
				if (text == "warning")
				{
					compilerError.IsWarning = true;
				}
				else if (text != "error")
				{
					return null;
				}
				compilerError.ErrorNumber = match.Result("${number}");
				compilerError.ErrorText = match.Result("${message}");
				return compilerError;
			}
			match = CSharpCodeGenerator.RelatedSymbolsRegex.Match(error_string);
			if (!match.Success)
			{
				compilerError.ErrorText = error_string;
				compilerError.IsWarning = false;
				compilerError.ErrorNumber = "";
				return compilerError;
			}
			return null;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000FC2D File Offset: 0x0000DE2D
		private static string GetTempFileNameWithExtension(TempFileCollection temp_files, string extension, bool keepFile)
		{
			return temp_files.AddExtension(extension, keepFile);
		}

		// Token: 0x04000379 RID: 889
		private static readonly char[] s_periodArray = new char[] { '.' };

		// Token: 0x0400037A RID: 890
		private readonly IDictionary<string, string> _provOptions;

		// Token: 0x0400037B RID: 891
		private static readonly string[][] s_keywords = new string[][]
		{
			null,
			new string[] { "as", "do", "if", "in", "is" },
			new string[] { "for", "int", "new", "out", "ref", "try" },
			new string[]
			{
				"base", "bool", "byte", "case", "char", "else", "enum", "goto", "lock", "long",
				"null", "this", "true", "uint", "void"
			},
			new string[]
			{
				"break", "catch", "class", "const", "event", "false", "fixed", "float", "sbyte", "short",
				"throw", "ulong", "using", "while"
			},
			new string[]
			{
				"double", "extern", "object", "params", "public", "return", "sealed", "sizeof", "static", "string",
				"struct", "switch", "typeof", "unsafe", "ushort"
			},
			new string[] { "checked", "decimal", "default", "finally", "foreach", "private", "virtual" },
			new string[] { "abstract", "continue", "delegate", "explicit", "implicit", "internal", "operator", "override", "readonly", "volatile" },
			new string[] { "__arglist", "__makeref", "__reftype", "interface", "namespace", "protected", "unchecked" },
			new string[] { "__refvalue", "stackalloc" }
		};

		// Token: 0x0400037C RID: 892
		private static readonly Regex RelatedSymbolsRegex = new Regex("\n            \\(Location\\ of\\ the\\ symbol\\ related\\ to\\ previous\\ (warning|error)\\)\n\t\t\t", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
	}
}
