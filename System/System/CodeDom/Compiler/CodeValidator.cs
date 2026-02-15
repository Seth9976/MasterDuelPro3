using System;
using System.IO;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200022D RID: 557
	internal sealed class CodeValidator
	{
		// Token: 0x06000D0C RID: 3340 RVA: 0x0003C154 File Offset: 0x0003A354
		internal void ValidateIdentifiers(CodeObject e)
		{
			if (e is CodeCompileUnit)
			{
				this.ValidateCodeCompileUnit((CodeCompileUnit)e);
				return;
			}
			if (e is CodeComment)
			{
				this.ValidateComment((CodeComment)e);
				return;
			}
			if (e is CodeExpression)
			{
				this.ValidateExpression((CodeExpression)e);
				return;
			}
			if (e is CodeNamespace)
			{
				this.ValidateNamespace((CodeNamespace)e);
				return;
			}
			if (e is CodeNamespaceImport)
			{
				CodeValidator.ValidateNamespaceImport((CodeNamespaceImport)e);
				return;
			}
			if (e is CodeStatement)
			{
				this.ValidateStatement((CodeStatement)e);
				return;
			}
			if (e is CodeTypeMember)
			{
				this.ValidateTypeMember((CodeTypeMember)e);
				return;
			}
			if (e is CodeTypeReference)
			{
				CodeValidator.ValidateTypeReference((CodeTypeReference)e);
				return;
			}
			if (e is CodeDirective)
			{
				CodeValidator.ValidateCodeDirective((CodeDirective)e);
				return;
			}
			throw new ArgumentException(SR.Format("Element type {0} is not supported.", e.GetType().FullName), "e");
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0003C23C File Offset: 0x0003A43C
		private void ValidateTypeMember(CodeTypeMember e)
		{
			this.ValidateCommentStatements(e.Comments);
			CodeValidator.ValidateCodeDirectives(e.StartDirectives);
			CodeValidator.ValidateCodeDirectives(e.EndDirectives);
			if (e.LinePragma != null)
			{
				this.ValidateLinePragmaStart(e.LinePragma);
			}
			if (e is CodeMemberEvent)
			{
				this.ValidateEvent((CodeMemberEvent)e);
				return;
			}
			if (e is CodeMemberField)
			{
				this.ValidateField((CodeMemberField)e);
				return;
			}
			if (e is CodeMemberMethod)
			{
				this.ValidateMemberMethod((CodeMemberMethod)e);
				return;
			}
			if (e is CodeMemberProperty)
			{
				this.ValidateProperty((CodeMemberProperty)e);
				return;
			}
			if (e is CodeSnippetTypeMember)
			{
				this.ValidateSnippetMember((CodeSnippetTypeMember)e);
				return;
			}
			if (e is CodeTypeDeclaration)
			{
				this.ValidateTypeDeclaration((CodeTypeDeclaration)e);
				return;
			}
			throw new ArgumentException(SR.Format("Element type {0} is not supported.", e.GetType().FullName), "e");
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0003C31C File Offset: 0x0003A51C
		private void ValidateCodeCompileUnit(CodeCompileUnit e)
		{
			CodeValidator.ValidateCodeDirectives(e.StartDirectives);
			CodeValidator.ValidateCodeDirectives(e.EndDirectives);
			if (e is CodeSnippetCompileUnit)
			{
				this.ValidateSnippetCompileUnit((CodeSnippetCompileUnit)e);
				return;
			}
			this.ValidateCompileUnitStart(e);
			this.ValidateNamespaces(e);
			this.ValidateCompileUnitEnd(e);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0003C369 File Offset: 0x0003A569
		private void ValidateSnippetCompileUnit(CodeSnippetCompileUnit e)
		{
			if (e.LinePragma != null)
			{
				this.ValidateLinePragmaStart(e.LinePragma);
			}
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0003C37F File Offset: 0x0003A57F
		private void ValidateCompileUnitStart(CodeCompileUnit e)
		{
			if (e.AssemblyCustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.AssemblyCustomAttributes);
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00002FA0 File Offset: 0x000011A0
		private void ValidateCompileUnitEnd(CodeCompileUnit e)
		{
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0003C39C File Offset: 0x0003A59C
		private void ValidateNamespaces(CodeCompileUnit e)
		{
			foreach (object obj in e.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				this.ValidateNamespace(codeNamespace);
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0003C3F8 File Offset: 0x0003A5F8
		private void ValidateNamespace(CodeNamespace e)
		{
			this.ValidateCommentStatements(e.Comments);
			CodeValidator.ValidateNamespaceStart(e);
			this.ValidateNamespaceImports(e);
			this.ValidateTypes(e);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0003C41A File Offset: 0x0003A61A
		private static void ValidateNamespaceStart(CodeNamespace e)
		{
			if (!string.IsNullOrEmpty(e.Name))
			{
				CodeValidator.ValidateTypeName(e, "Name", e.Name);
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0003C43C File Offset: 0x0003A63C
		private void ValidateNamespaceImports(CodeNamespace e)
		{
			foreach (object obj in e.Imports)
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj;
				if (codeNamespaceImport.LinePragma != null)
				{
					this.ValidateLinePragmaStart(codeNamespaceImport.LinePragma);
				}
				CodeValidator.ValidateNamespaceImport(codeNamespaceImport);
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0003C4A8 File Offset: 0x0003A6A8
		private static void ValidateNamespaceImport(CodeNamespaceImport e)
		{
			CodeValidator.ValidateTypeName(e, "Namespace", e.Namespace);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0003C4BC File Offset: 0x0003A6BC
		private void ValidateAttributes(CodeAttributeDeclarationCollection attributes)
		{
			if (attributes.Count == 0)
			{
				return;
			}
			foreach (object obj in attributes)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				CodeValidator.ValidateTypeName(codeAttributeDeclaration, "Name", codeAttributeDeclaration.Name);
				CodeValidator.ValidateTypeReference(codeAttributeDeclaration.AttributeType);
				foreach (object obj2 in codeAttributeDeclaration.Arguments)
				{
					CodeAttributeArgument codeAttributeArgument = (CodeAttributeArgument)obj2;
					this.ValidateAttributeArgument(codeAttributeArgument);
				}
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0003C57C File Offset: 0x0003A77C
		private void ValidateAttributeArgument(CodeAttributeArgument arg)
		{
			if (!string.IsNullOrEmpty(arg.Name))
			{
				CodeValidator.ValidateIdentifier(arg, "Name", arg.Name);
			}
			this.ValidateExpression(arg.Value);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0003C5A8 File Offset: 0x0003A7A8
		private void ValidateTypes(CodeNamespace e)
		{
			foreach (object obj in e.Types)
			{
				CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj;
				this.ValidateTypeDeclaration(codeTypeDeclaration);
			}
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0003C604 File Offset: 0x0003A804
		private void ValidateTypeDeclaration(CodeTypeDeclaration e)
		{
			CodeTypeDeclaration currentClass = this._currentClass;
			this._currentClass = e;
			this.ValidateTypeStart(e);
			this.ValidateTypeParameters(e.TypeParameters);
			this.ValidateTypeMembers(e);
			CodeValidator.ValidateTypeReferences(e.BaseTypes);
			this._currentClass = currentClass;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0003C64C File Offset: 0x0003A84C
		private void ValidateTypeMembers(CodeTypeDeclaration e)
		{
			foreach (object obj in e.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				this.ValidateTypeMember(codeTypeMember);
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0003C6A8 File Offset: 0x0003A8A8
		private void ValidateTypeParameters(CodeTypeParameterCollection parameters)
		{
			for (int i = 0; i < parameters.Count; i++)
			{
				this.ValidateTypeParameter(parameters[i]);
			}
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0003C6D3 File Offset: 0x0003A8D3
		private void ValidateTypeParameter(CodeTypeParameter e)
		{
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			CodeValidator.ValidateTypeReferences(e.Constraints);
			this.ValidateAttributes(e.CustomAttributes);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0003C700 File Offset: 0x0003A900
		private void ValidateField(CodeMemberField e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			if (!this.IsCurrentEnum)
			{
				CodeValidator.ValidateTypeReference(e.Type);
			}
			if (e.InitExpression != null)
			{
				this.ValidateExpression(e.InitExpression);
			}
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0003C760 File Offset: 0x0003A960
		private void ValidateConstructor(CodeConstructor e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			this.ValidateParameters(e.Parameters);
			CodeExpressionCollection baseConstructorArgs = e.BaseConstructorArgs;
			CodeExpressionCollection chainedConstructorArgs = e.ChainedConstructorArgs;
			if (baseConstructorArgs.Count > 0)
			{
				this.ValidateExpressionList(baseConstructorArgs);
			}
			if (chainedConstructorArgs.Count > 0)
			{
				this.ValidateExpressionList(chainedConstructorArgs);
			}
			this.ValidateStatements(e.Statements);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0003C7D0 File Offset: 0x0003A9D0
		private void ValidateProperty(CodeMemberProperty e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			CodeValidator.ValidateTypeReference(e.Type);
			CodeValidator.ValidateTypeReferences(e.ImplementationTypes);
			if (e.PrivateImplementationType != null && !this.IsCurrentInterface)
			{
				CodeValidator.ValidateTypeReference(e.PrivateImplementationType);
			}
			if (e.Parameters.Count > 0 && string.Equals(e.Name, "Item", StringComparison.OrdinalIgnoreCase))
			{
				this.ValidateParameters(e.Parameters);
			}
			else
			{
				CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			}
			if (e.HasGet && !this.IsCurrentInterface && (e.Attributes & MemberAttributes.ScopeMask) != MemberAttributes.Abstract)
			{
				this.ValidateStatements(e.GetStatements);
			}
			if (e.HasSet && !this.IsCurrentInterface && (e.Attributes & MemberAttributes.ScopeMask) != MemberAttributes.Abstract)
			{
				this.ValidateStatements(e.SetStatements);
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0003C8B8 File Offset: 0x0003AAB8
		private void ValidateMemberMethod(CodeMemberMethod e)
		{
			this.ValidateCommentStatements(e.Comments);
			if (e.LinePragma != null)
			{
				this.ValidateLinePragmaStart(e.LinePragma);
			}
			this.ValidateTypeParameters(e.TypeParameters);
			CodeValidator.ValidateTypeReferences(e.ImplementationTypes);
			if (e is CodeEntryPointMethod)
			{
				this.ValidateStatements(((CodeEntryPointMethod)e).Statements);
				return;
			}
			if (e is CodeConstructor)
			{
				this.ValidateConstructor((CodeConstructor)e);
				return;
			}
			if (e is CodeTypeConstructor)
			{
				this.ValidateTypeConstructor((CodeTypeConstructor)e);
				return;
			}
			this.ValidateMethod(e);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0003C947 File Offset: 0x0003AB47
		private void ValidateTypeConstructor(CodeTypeConstructor e)
		{
			this.ValidateStatements(e.Statements);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0003C958 File Offset: 0x0003AB58
		private void ValidateMethod(CodeMemberMethod e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			if (e.ReturnTypeCustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.ReturnTypeCustomAttributes);
			}
			CodeValidator.ValidateTypeReference(e.ReturnType);
			if (e.PrivateImplementationType != null)
			{
				CodeValidator.ValidateTypeReference(e.PrivateImplementationType);
			}
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			this.ValidateParameters(e.Parameters);
			if (!this.IsCurrentInterface && (e.Attributes & MemberAttributes.ScopeMask) != MemberAttributes.Abstract)
			{
				this.ValidateStatements(e.Statements);
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00002FA0 File Offset: 0x000011A0
		private void ValidateSnippetMember(CodeSnippetTypeMember e)
		{
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0003C9F4 File Offset: 0x0003ABF4
		private void ValidateTypeStart(CodeTypeDeclaration e)
		{
			this.ValidateCommentStatements(e.Comments);
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			if (this.IsCurrentDelegate)
			{
				CodeTypeDelegate codeTypeDelegate = (CodeTypeDelegate)e;
				CodeValidator.ValidateTypeReference(codeTypeDelegate.ReturnType);
				this.ValidateParameters(codeTypeDelegate.Parameters);
				return;
			}
			foreach (object obj in e.BaseTypes)
			{
				CodeValidator.ValidateTypeReference((CodeTypeReference)obj);
			}
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0003CAA8 File Offset: 0x0003ACA8
		private void ValidateCommentStatements(CodeCommentStatementCollection e)
		{
			foreach (object obj in e)
			{
				CodeCommentStatement codeCommentStatement = (CodeCommentStatement)obj;
				this.ValidateCommentStatement(codeCommentStatement);
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0003CAFC File Offset: 0x0003ACFC
		private void ValidateCommentStatement(CodeCommentStatement e)
		{
			this.ValidateComment(e.Comment);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00002FA0 File Offset: 0x000011A0
		private void ValidateComment(CodeComment e)
		{
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0003CB0C File Offset: 0x0003AD0C
		private void ValidateStatement(CodeStatement e)
		{
			CodeValidator.ValidateCodeDirectives(e.StartDirectives);
			CodeValidator.ValidateCodeDirectives(e.EndDirectives);
			if (e is CodeCommentStatement)
			{
				this.ValidateCommentStatement((CodeCommentStatement)e);
				return;
			}
			if (e is CodeMethodReturnStatement)
			{
				this.ValidateMethodReturnStatement((CodeMethodReturnStatement)e);
				return;
			}
			if (e is CodeConditionStatement)
			{
				this.ValidateConditionStatement((CodeConditionStatement)e);
				return;
			}
			if (e is CodeTryCatchFinallyStatement)
			{
				this.ValidateTryCatchFinallyStatement((CodeTryCatchFinallyStatement)e);
				return;
			}
			if (e is CodeAssignStatement)
			{
				this.ValidateAssignStatement((CodeAssignStatement)e);
				return;
			}
			if (e is CodeExpressionStatement)
			{
				this.ValidateExpressionStatement((CodeExpressionStatement)e);
				return;
			}
			if (e is CodeIterationStatement)
			{
				this.ValidateIterationStatement((CodeIterationStatement)e);
				return;
			}
			if (e is CodeThrowExceptionStatement)
			{
				this.ValidateThrowExceptionStatement((CodeThrowExceptionStatement)e);
				return;
			}
			if (e is CodeSnippetStatement)
			{
				this.ValidateSnippetStatement((CodeSnippetStatement)e);
				return;
			}
			if (e is CodeVariableDeclarationStatement)
			{
				this.ValidateVariableDeclarationStatement((CodeVariableDeclarationStatement)e);
				return;
			}
			if (e is CodeAttachEventStatement)
			{
				this.ValidateAttachEventStatement((CodeAttachEventStatement)e);
				return;
			}
			if (e is CodeRemoveEventStatement)
			{
				this.ValidateRemoveEventStatement((CodeRemoveEventStatement)e);
				return;
			}
			if (e is CodeGotoStatement)
			{
				CodeValidator.ValidateGotoStatement((CodeGotoStatement)e);
				return;
			}
			if (e is CodeLabeledStatement)
			{
				this.ValidateLabeledStatement((CodeLabeledStatement)e);
				return;
			}
			throw new ArgumentException(SR.Format("Element type {0} is not supported.", e.GetType().FullName), "e");
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0003CC74 File Offset: 0x0003AE74
		private void ValidateStatements(CodeStatementCollection stmts)
		{
			foreach (object obj in stmts)
			{
				CodeStatement codeStatement = (CodeStatement)obj;
				this.ValidateStatement(codeStatement);
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0003CCC8 File Offset: 0x0003AEC8
		private void ValidateExpressionStatement(CodeExpressionStatement e)
		{
			this.ValidateExpression(e.Expression);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0003CCD6 File Offset: 0x0003AED6
		private void ValidateIterationStatement(CodeIterationStatement e)
		{
			this.ValidateStatement(e.InitStatement);
			this.ValidateExpression(e.TestExpression);
			this.ValidateStatement(e.IncrementStatement);
			this.ValidateStatements(e.Statements);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0003CD08 File Offset: 0x0003AF08
		private void ValidateThrowExceptionStatement(CodeThrowExceptionStatement e)
		{
			if (e.ToThrow != null)
			{
				this.ValidateExpression(e.ToThrow);
			}
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0003CD1E File Offset: 0x0003AF1E
		private void ValidateMethodReturnStatement(CodeMethodReturnStatement e)
		{
			if (e.Expression != null)
			{
				this.ValidateExpression(e.Expression);
			}
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0003CD34 File Offset: 0x0003AF34
		private void ValidateConditionStatement(CodeConditionStatement e)
		{
			this.ValidateExpression(e.Condition);
			this.ValidateStatements(e.TrueStatements);
			if (e.FalseStatements.Count > 0)
			{
				this.ValidateStatements(e.FalseStatements);
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0003CD68 File Offset: 0x0003AF68
		private void ValidateTryCatchFinallyStatement(CodeTryCatchFinallyStatement e)
		{
			this.ValidateStatements(e.TryStatements);
			CodeCatchClauseCollection catchClauses = e.CatchClauses;
			if (catchClauses.Count > 0)
			{
				foreach (object obj in catchClauses)
				{
					CodeCatchClause codeCatchClause = (CodeCatchClause)obj;
					CodeValidator.ValidateTypeReference(codeCatchClause.CatchExceptionType);
					CodeValidator.ValidateIdentifier(codeCatchClause, "LocalName", codeCatchClause.LocalName);
					this.ValidateStatements(codeCatchClause.Statements);
				}
			}
			CodeStatementCollection finallyStatements = e.FinallyStatements;
			if (finallyStatements.Count > 0)
			{
				this.ValidateStatements(finallyStatements);
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0003CE14 File Offset: 0x0003B014
		private void ValidateAssignStatement(CodeAssignStatement e)
		{
			this.ValidateExpression(e.Left);
			this.ValidateExpression(e.Right);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0003CE2E File Offset: 0x0003B02E
		private void ValidateAttachEventStatement(CodeAttachEventStatement e)
		{
			this.ValidateEventReferenceExpression(e.Event);
			this.ValidateExpression(e.Listener);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0003CE48 File Offset: 0x0003B048
		private void ValidateRemoveEventStatement(CodeRemoveEventStatement e)
		{
			this.ValidateEventReferenceExpression(e.Event);
			this.ValidateExpression(e.Listener);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0003CE62 File Offset: 0x0003B062
		private static void ValidateGotoStatement(CodeGotoStatement e)
		{
			CodeValidator.ValidateIdentifier(e, "Label", e.Label);
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0003CE75 File Offset: 0x0003B075
		private void ValidateLabeledStatement(CodeLabeledStatement e)
		{
			CodeValidator.ValidateIdentifier(e, "Label", e.Label);
			if (e.Statement != null)
			{
				this.ValidateStatement(e.Statement);
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0003CE9C File Offset: 0x0003B09C
		private void ValidateVariableDeclarationStatement(CodeVariableDeclarationStatement e)
		{
			CodeValidator.ValidateTypeReference(e.Type);
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			if (e.InitExpression != null)
			{
				this.ValidateExpression(e.InitExpression);
			}
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00002FA0 File Offset: 0x000011A0
		private void ValidateLinePragmaStart(CodeLinePragma e)
		{
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0003CED0 File Offset: 0x0003B0D0
		private void ValidateEvent(CodeMemberEvent e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			if (e.PrivateImplementationType != null)
			{
				CodeValidator.ValidateTypeReference(e.Type);
				CodeValidator.ValidateIdentifier(e, "Name", e.Name);
			}
			CodeValidator.ValidateTypeReferences(e.ImplementationTypes);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0003CF28 File Offset: 0x0003B128
		private void ValidateParameters(CodeParameterDeclarationExpressionCollection parameters)
		{
			foreach (object obj in parameters)
			{
				CodeParameterDeclarationExpression codeParameterDeclarationExpression = (CodeParameterDeclarationExpression)obj;
				this.ValidateParameterDeclarationExpression(codeParameterDeclarationExpression);
			}
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00002FA0 File Offset: 0x000011A0
		private void ValidateSnippetStatement(CodeSnippetStatement e)
		{
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0003CF7C File Offset: 0x0003B17C
		private void ValidateExpressionList(CodeExpressionCollection expressions)
		{
			foreach (object obj in expressions)
			{
				CodeExpression codeExpression = (CodeExpression)obj;
				this.ValidateExpression(codeExpression);
			}
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0003CFD0 File Offset: 0x0003B1D0
		private static void ValidateTypeReference(CodeTypeReference e)
		{
			CodeValidator.ValidateTypeName(e, "BaseType", e.BaseType);
			CodeValidator.ValidateArity(e);
			CodeValidator.ValidateTypeReferences(e.TypeArguments);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0003CFF4 File Offset: 0x0003B1F4
		private static void ValidateTypeReferences(CodeTypeReferenceCollection refs)
		{
			for (int i = 0; i < refs.Count; i++)
			{
				CodeValidator.ValidateTypeReference(refs[i]);
			}
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0003D020 File Offset: 0x0003B220
		private static void ValidateArity(CodeTypeReference e)
		{
			string baseType = e.BaseType;
			int num = 0;
			for (int i = 0; i < baseType.Length; i++)
			{
				if (baseType[i] == '`')
				{
					i++;
					int num2 = 0;
					while (i < baseType.Length && baseType[i] >= '0' && baseType[i] <= '9')
					{
						num2 = num2 * 10 + (int)(baseType[i] - '0');
						i++;
					}
					num += num2;
				}
			}
			if (num != e.TypeArguments.Count && e.TypeArguments.Count != 0)
			{
				throw new ArgumentException(SR.Format("The total arity specified in '{0}' does not match the number of TypeArguments supplied.  There were '{1}' TypeArguments supplied.", baseType, e.TypeArguments.Count));
			}
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0003D0CD File Offset: 0x0003B2CD
		private static void ValidateTypeName(object e, string propertyName, string typeName)
		{
			if (!CodeGenerator.IsValidLanguageIndependentTypeName(typeName))
			{
				throw new ArgumentException(SR.Format("The type name:\"{0}\" on the property:\"{1}\" of type:\"{2}\" is not a valid language-independent type name.", typeName, propertyName, e.GetType().FullName), "typeName");
			}
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0003D0F9 File Offset: 0x0003B2F9
		private static void ValidateIdentifier(object e, string propertyName, string identifier)
		{
			if (!CodeGenerator.IsValidLanguageIndependentIdentifier(identifier))
			{
				throw new ArgumentException(SR.Format("The identifier:\"{0}\" on the property:\"{1}\" of type:\"{2}\" is not a valid language-independent identifier name. Check to see if CodeGenerator.IsValidLanguageIndependentIdentifier allows the identifier name.", identifier, propertyName, e.GetType().FullName), "identifier");
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0003D128 File Offset: 0x0003B328
		private void ValidateExpression(CodeExpression e)
		{
			if (e is CodeArrayCreateExpression)
			{
				this.ValidateArrayCreateExpression((CodeArrayCreateExpression)e);
				return;
			}
			if (e is CodeBaseReferenceExpression)
			{
				this.ValidateBaseReferenceExpression((CodeBaseReferenceExpression)e);
				return;
			}
			if (e is CodeBinaryOperatorExpression)
			{
				this.ValidateBinaryOperatorExpression((CodeBinaryOperatorExpression)e);
				return;
			}
			if (e is CodeCastExpression)
			{
				this.ValidateCastExpression((CodeCastExpression)e);
				return;
			}
			if (e is CodeDefaultValueExpression)
			{
				CodeValidator.ValidateDefaultValueExpression((CodeDefaultValueExpression)e);
				return;
			}
			if (e is CodeDelegateCreateExpression)
			{
				this.ValidateDelegateCreateExpression((CodeDelegateCreateExpression)e);
				return;
			}
			if (e is CodeFieldReferenceExpression)
			{
				this.ValidateFieldReferenceExpression((CodeFieldReferenceExpression)e);
				return;
			}
			if (e is CodeArgumentReferenceExpression)
			{
				CodeValidator.ValidateArgumentReferenceExpression((CodeArgumentReferenceExpression)e);
				return;
			}
			if (e is CodeVariableReferenceExpression)
			{
				CodeValidator.ValidateVariableReferenceExpression((CodeVariableReferenceExpression)e);
				return;
			}
			if (e is CodeIndexerExpression)
			{
				this.ValidateIndexerExpression((CodeIndexerExpression)e);
				return;
			}
			if (e is CodeArrayIndexerExpression)
			{
				this.ValidateArrayIndexerExpression((CodeArrayIndexerExpression)e);
				return;
			}
			if (e is CodeSnippetExpression)
			{
				this.ValidateSnippetExpression((CodeSnippetExpression)e);
				return;
			}
			if (e is CodeMethodInvokeExpression)
			{
				this.ValidateMethodInvokeExpression((CodeMethodInvokeExpression)e);
				return;
			}
			if (e is CodeMethodReferenceExpression)
			{
				this.ValidateMethodReferenceExpression((CodeMethodReferenceExpression)e);
				return;
			}
			if (e is CodeEventReferenceExpression)
			{
				this.ValidateEventReferenceExpression((CodeEventReferenceExpression)e);
				return;
			}
			if (e is CodeDelegateInvokeExpression)
			{
				this.ValidateDelegateInvokeExpression((CodeDelegateInvokeExpression)e);
				return;
			}
			if (e is CodeObjectCreateExpression)
			{
				this.ValidateObjectCreateExpression((CodeObjectCreateExpression)e);
				return;
			}
			if (e is CodeParameterDeclarationExpression)
			{
				this.ValidateParameterDeclarationExpression((CodeParameterDeclarationExpression)e);
				return;
			}
			if (e is CodeDirectionExpression)
			{
				this.ValidateDirectionExpression((CodeDirectionExpression)e);
				return;
			}
			if (e is CodePrimitiveExpression)
			{
				this.ValidatePrimitiveExpression((CodePrimitiveExpression)e);
				return;
			}
			if (e is CodePropertyReferenceExpression)
			{
				this.ValidatePropertyReferenceExpression((CodePropertyReferenceExpression)e);
				return;
			}
			if (e is CodePropertySetValueReferenceExpression)
			{
				this.ValidatePropertySetValueReferenceExpression((CodePropertySetValueReferenceExpression)e);
				return;
			}
			if (e is CodeThisReferenceExpression)
			{
				this.ValidateThisReferenceExpression((CodeThisReferenceExpression)e);
				return;
			}
			if (e is CodeTypeReferenceExpression)
			{
				CodeValidator.ValidateTypeReference(((CodeTypeReferenceExpression)e).Type);
				return;
			}
			if (e is CodeTypeOfExpression)
			{
				CodeValidator.ValidateTypeOfExpression((CodeTypeOfExpression)e);
				return;
			}
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			throw new ArgumentException(SR.Format("Element type {0} is not supported.", e.GetType().FullName), "e");
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0003D370 File Offset: 0x0003B570
		private void ValidateArrayCreateExpression(CodeArrayCreateExpression e)
		{
			CodeValidator.ValidateTypeReference(e.CreateType);
			CodeExpressionCollection initializers = e.Initializers;
			if (initializers.Count > 0)
			{
				this.ValidateExpressionList(initializers);
				return;
			}
			if (e.SizeExpression != null)
			{
				this.ValidateExpression(e.SizeExpression);
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00002FA0 File Offset: 0x000011A0
		private void ValidateBaseReferenceExpression(CodeBaseReferenceExpression e)
		{
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0003D3B4 File Offset: 0x0003B5B4
		private void ValidateBinaryOperatorExpression(CodeBinaryOperatorExpression e)
		{
			this.ValidateExpression(e.Left);
			this.ValidateExpression(e.Right);
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0003D3CE File Offset: 0x0003B5CE
		private void ValidateCastExpression(CodeCastExpression e)
		{
			CodeValidator.ValidateTypeReference(e.TargetType);
			this.ValidateExpression(e.Expression);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0003D3E7 File Offset: 0x0003B5E7
		private static void ValidateDefaultValueExpression(CodeDefaultValueExpression e)
		{
			CodeValidator.ValidateTypeReference(e.Type);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0003D3F4 File Offset: 0x0003B5F4
		private void ValidateDelegateCreateExpression(CodeDelegateCreateExpression e)
		{
			CodeValidator.ValidateTypeReference(e.DelegateType);
			this.ValidateExpression(e.TargetObject);
			CodeValidator.ValidateIdentifier(e, "MethodName", e.MethodName);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0003D41E File Offset: 0x0003B61E
		private void ValidateFieldReferenceExpression(CodeFieldReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.ValidateExpression(e.TargetObject);
			}
			CodeValidator.ValidateIdentifier(e, "FieldName", e.FieldName);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0003D445 File Offset: 0x0003B645
		private static void ValidateArgumentReferenceExpression(CodeArgumentReferenceExpression e)
		{
			CodeValidator.ValidateIdentifier(e, "ParameterName", e.ParameterName);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0003D458 File Offset: 0x0003B658
		private static void ValidateVariableReferenceExpression(CodeVariableReferenceExpression e)
		{
			CodeValidator.ValidateIdentifier(e, "VariableName", e.VariableName);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0003D46C File Offset: 0x0003B66C
		private void ValidateIndexerExpression(CodeIndexerExpression e)
		{
			this.ValidateExpression(e.TargetObject);
			foreach (object obj in e.Indices)
			{
				CodeExpression codeExpression = (CodeExpression)obj;
				this.ValidateExpression(codeExpression);
			}
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0003D4D4 File Offset: 0x0003B6D4
		private void ValidateArrayIndexerExpression(CodeArrayIndexerExpression e)
		{
			this.ValidateExpression(e.TargetObject);
			foreach (object obj in e.Indices)
			{
				CodeExpression codeExpression = (CodeExpression)obj;
				this.ValidateExpression(codeExpression);
			}
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00002FA0 File Offset: 0x000011A0
		private void ValidateSnippetExpression(CodeSnippetExpression e)
		{
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0003D53C File Offset: 0x0003B73C
		private void ValidateMethodInvokeExpression(CodeMethodInvokeExpression e)
		{
			this.ValidateMethodReferenceExpression(e.Method);
			this.ValidateExpressionList(e.Parameters);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0003D556 File Offset: 0x0003B756
		private void ValidateMethodReferenceExpression(CodeMethodReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.ValidateExpression(e.TargetObject);
			}
			CodeValidator.ValidateIdentifier(e, "MethodName", e.MethodName);
			CodeValidator.ValidateTypeReferences(e.TypeArguments);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0003D588 File Offset: 0x0003B788
		private void ValidateEventReferenceExpression(CodeEventReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.ValidateExpression(e.TargetObject);
			}
			CodeValidator.ValidateIdentifier(e, "EventName", e.EventName);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0003D5AF File Offset: 0x0003B7AF
		private void ValidateDelegateInvokeExpression(CodeDelegateInvokeExpression e)
		{
			if (e.TargetObject != null)
			{
				this.ValidateExpression(e.TargetObject);
			}
			this.ValidateExpressionList(e.Parameters);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0003D5D1 File Offset: 0x0003B7D1
		private void ValidateObjectCreateExpression(CodeObjectCreateExpression e)
		{
			CodeValidator.ValidateTypeReference(e.CreateType);
			this.ValidateExpressionList(e.Parameters);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0003D5EA File Offset: 0x0003B7EA
		private void ValidateParameterDeclarationExpression(CodeParameterDeclarationExpression e)
		{
			if (e.CustomAttributes.Count > 0)
			{
				this.ValidateAttributes(e.CustomAttributes);
			}
			CodeValidator.ValidateTypeReference(e.Type);
			CodeValidator.ValidateIdentifier(e, "Name", e.Name);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0003D622 File Offset: 0x0003B822
		private void ValidateDirectionExpression(CodeDirectionExpression e)
		{
			this.ValidateExpression(e.Expression);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00002FA0 File Offset: 0x000011A0
		private void ValidatePrimitiveExpression(CodePrimitiveExpression e)
		{
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0003D630 File Offset: 0x0003B830
		private void ValidatePropertyReferenceExpression(CodePropertyReferenceExpression e)
		{
			if (e.TargetObject != null)
			{
				this.ValidateExpression(e.TargetObject);
			}
			CodeValidator.ValidateIdentifier(e, "PropertyName", e.PropertyName);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00002FA0 File Offset: 0x000011A0
		private void ValidatePropertySetValueReferenceExpression(CodePropertySetValueReferenceExpression e)
		{
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00002FA0 File Offset: 0x000011A0
		private void ValidateThisReferenceExpression(CodeThisReferenceExpression e)
		{
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0003D657 File Offset: 0x0003B857
		private static void ValidateTypeOfExpression(CodeTypeOfExpression e)
		{
			CodeValidator.ValidateTypeReference(e.Type);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0003D664 File Offset: 0x0003B864
		private static void ValidateCodeDirectives(CodeDirectiveCollection e)
		{
			for (int i = 0; i < e.Count; i++)
			{
				CodeValidator.ValidateCodeDirective(e[i]);
			}
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0003D690 File Offset: 0x0003B890
		private static void ValidateCodeDirective(CodeDirective e)
		{
			if (e is CodeChecksumPragma)
			{
				CodeValidator.ValidateChecksumPragma((CodeChecksumPragma)e);
				return;
			}
			if (e is CodeRegionDirective)
			{
				CodeValidator.ValidateRegionDirective((CodeRegionDirective)e);
				return;
			}
			throw new ArgumentException(SR.Format("Element type {0} is not supported.", e.GetType().FullName), "e");
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0003D6E4 File Offset: 0x0003B8E4
		private static void ValidateChecksumPragma(CodeChecksumPragma e)
		{
			if (e.FileName.IndexOfAny(Path.GetInvalidPathChars()) != -1)
			{
				throw new ArgumentException(SR.Format("The CodeChecksumPragma file name '{0}' contains invalid path characters.", e.FileName));
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0003D70F File Offset: 0x0003B90F
		private static void ValidateRegionDirective(CodeRegionDirective e)
		{
			if (e.RegionText.IndexOfAny(CodeValidator.s_newLineChars) != -1)
			{
				throw new ArgumentException(SR.Format("The region directive '{0}' contains invalid characters.  RegionText cannot contain any new line characters.", e.RegionText));
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0003D73A File Offset: 0x0003B93A
		private bool IsCurrentInterface
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsInterface;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0003D75E File Offset: 0x0003B95E
		private bool IsCurrentEnum
		{
			get
			{
				return this._currentClass != null && !(this._currentClass is CodeTypeDelegate) && this._currentClass.IsEnum;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0003D782 File Offset: 0x0003B982
		private bool IsCurrentDelegate
		{
			get
			{
				return this._currentClass != null && this._currentClass is CodeTypeDelegate;
			}
		}

		// Token: 0x04000930 RID: 2352
		private static readonly char[] s_newLineChars = new char[] { '\r', '\n', '\u2028', '\u2029', '\u0085' };

		// Token: 0x04000931 RID: 2353
		private CodeTypeDeclaration _currentClass;
	}
}
