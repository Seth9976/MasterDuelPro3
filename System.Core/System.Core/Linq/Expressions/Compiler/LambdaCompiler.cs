using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020000F5 RID: 245
	internal sealed class LambdaCompiler : ILocalCache
	{
		// Token: 0x06000806 RID: 2054 RVA: 0x0001B1C5 File Offset: 0x000193C5
		private void EmitAddress(Expression node, Type type)
		{
			this.EmitAddress(node, type, LambdaCompiler.CompilationFlags.EmitExpressionStart);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001B1D0 File Offset: 0x000193D0
		private void EmitAddress(Expression node, Type type, LambdaCompiler.CompilationFlags flags)
		{
			bool flag = (flags & LambdaCompiler.CompilationFlags.EmitExpressionStartMask) == LambdaCompiler.CompilationFlags.EmitExpressionStart;
			LambdaCompiler.CompilationFlags compilationFlags = (flag ? this.EmitExpressionStart(node) : LambdaCompiler.CompilationFlags.EmitNoExpressionStart);
			ExpressionType nodeType = node.NodeType;
			if (nodeType <= ExpressionType.MemberAccess)
			{
				if (nodeType == ExpressionType.ArrayIndex)
				{
					this.AddressOf((BinaryExpression)node, type);
					goto IL_00A2;
				}
				if (nodeType == ExpressionType.Call)
				{
					this.AddressOf((MethodCallExpression)node, type);
					goto IL_00A2;
				}
				if (nodeType == ExpressionType.MemberAccess)
				{
					this.AddressOf((MemberExpression)node, type);
					goto IL_00A2;
				}
			}
			else
			{
				if (nodeType == ExpressionType.Parameter)
				{
					this.AddressOf((ParameterExpression)node, type);
					goto IL_00A2;
				}
				if (nodeType == ExpressionType.Index)
				{
					this.AddressOf((IndexExpression)node, type);
					goto IL_00A2;
				}
				if (nodeType == ExpressionType.Unbox)
				{
					this.AddressOf((UnaryExpression)node, type);
					goto IL_00A2;
				}
			}
			this.EmitExpressionAddress(node, type);
			IL_00A2:
			if (flag)
			{
				this.EmitExpressionEnd(compilationFlags);
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001B28C File Offset: 0x0001948C
		private void AddressOf(BinaryExpression node, Type type)
		{
			if (TypeUtils.AreEquivalent(type, node.Type))
			{
				this.EmitExpression(node.Left);
				this.EmitExpression(node.Right);
				this._ilg.Emit(OpCodes.Ldelema, node.Type);
				return;
			}
			this.EmitExpressionAddress(node, type);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001B2E0 File Offset: 0x000194E0
		private void AddressOf(ParameterExpression node, Type type)
		{
			if (TypeUtils.AreEquivalent(type, node.Type))
			{
				if (node.IsByRef)
				{
					this._scope.EmitGet(node);
					return;
				}
				this._scope.EmitAddressOf(node);
				return;
			}
			else
			{
				if (node.Type.IsByRef && node.Type.GetElementType() == type)
				{
					this.EmitExpression(node);
					return;
				}
				this.EmitExpressionAddress(node, type);
				return;
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001B350 File Offset: 0x00019550
		private void AddressOf(MemberExpression node, Type type)
		{
			if (TypeUtils.AreEquivalent(type, node.Type))
			{
				Type type2 = null;
				if (node.Expression != null)
				{
					this.EmitInstance(node.Expression, out type2);
				}
				this.EmitMemberAddress(node.Member, type2);
				return;
			}
			this.EmitExpressionAddress(node, type);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001B39C File Offset: 0x0001959C
		private void EmitMemberAddress(MemberInfo member, Type objectType)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null && !fieldInfo.IsLiteral && !fieldInfo.IsInitOnly)
			{
				this._ilg.EmitFieldAddress(fieldInfo);
				return;
			}
			this.EmitMemberGet(member, objectType);
			LocalBuilder local = this.GetLocal(LambdaCompiler.GetMemberType(member));
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldloca, local);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001B408 File Offset: 0x00019608
		private void AddressOf(MethodCallExpression node, Type type)
		{
			if (!node.Method.IsStatic && node.Object.Type.IsArray && node.Method == node.Object.Type.GetMethod("Get", BindingFlags.Instance | BindingFlags.Public))
			{
				MethodInfo method = node.Object.Type.GetMethod("Address", BindingFlags.Instance | BindingFlags.Public);
				this.EmitMethodCall(node.Object, method, node);
				return;
			}
			this.EmitExpressionAddress(node, type);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001B488 File Offset: 0x00019688
		private void AddressOf(IndexExpression node, Type type)
		{
			if (!TypeUtils.AreEquivalent(type, node.Type) || node.Indexer != null)
			{
				this.EmitExpressionAddress(node, type);
				return;
			}
			if (node.ArgumentCount == 1)
			{
				this.EmitExpression(node.Object);
				this.EmitExpression(node.GetArgument(0));
				this._ilg.Emit(OpCodes.Ldelema, node.Type);
				return;
			}
			MethodInfo method = node.Object.Type.GetMethod("Address", BindingFlags.Instance | BindingFlags.Public);
			this.EmitMethodCall(node.Object, method, node);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001B519 File Offset: 0x00019719
		private void AddressOf(UnaryExpression node, Type type)
		{
			this.EmitExpression(node.Operand);
			this._ilg.Emit(OpCodes.Unbox, type);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001B538 File Offset: 0x00019738
		private void EmitExpressionAddress(Expression node, Type type)
		{
			this.EmitExpression(node, LambdaCompiler.CompilationFlags.EmitNoExpressionStart | LambdaCompiler.CompilationFlags.EmitAsNoTail);
			LocalBuilder local = this.GetLocal(type);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldloca, local);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001B57C File Offset: 0x0001977C
		private LambdaCompiler.WriteBack EmitAddressWriteBack(Expression node, Type type)
		{
			LambdaCompiler.CompilationFlags compilationFlags = this.EmitExpressionStart(node);
			LambdaCompiler.WriteBack writeBack = null;
			if (TypeUtils.AreEquivalent(type, node.Type))
			{
				ExpressionType nodeType = node.NodeType;
				if (nodeType != ExpressionType.MemberAccess)
				{
					if (nodeType == ExpressionType.Index)
					{
						writeBack = this.AddressOfWriteBack((IndexExpression)node);
					}
				}
				else
				{
					writeBack = this.AddressOfWriteBack((MemberExpression)node);
				}
			}
			if (writeBack == null)
			{
				this.EmitAddress(node, type, LambdaCompiler.CompilationFlags.EmitNoExpressionStart | LambdaCompiler.CompilationFlags.EmitAsNoTail);
			}
			this.EmitExpressionEnd(compilationFlags);
			return writeBack;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001B5E8 File Offset: 0x000197E8
		private LambdaCompiler.WriteBack AddressOfWriteBack(MemberExpression node)
		{
			PropertyInfo propertyInfo = node.Member as PropertyInfo;
			if (propertyInfo == null || !propertyInfo.CanWrite)
			{
				return null;
			}
			return this.AddressOfWriteBackCore(node);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001B618 File Offset: 0x00019818
		private LambdaCompiler.WriteBack AddressOfWriteBackCore(MemberExpression node)
		{
			LocalBuilder instanceLocal = null;
			Type type = null;
			if (node.Expression != null)
			{
				this.EmitInstance(node.Expression, out type);
				this._ilg.Emit(OpCodes.Dup);
				this._ilg.Emit(OpCodes.Stloc, instanceLocal = this.GetInstanceLocal(type));
			}
			PropertyInfo pi = (PropertyInfo)node.Member;
			this.EmitCall(type, pi.GetGetMethod(true));
			LocalBuilder valueLocal = this.GetLocal(node.Type);
			this._ilg.Emit(OpCodes.Stloc, valueLocal);
			this._ilg.Emit(OpCodes.Ldloca, valueLocal);
			return delegate(LambdaCompiler @this)
			{
				if (instanceLocal != null)
				{
					@this._ilg.Emit(OpCodes.Ldloc, instanceLocal);
					@this.FreeLocal(instanceLocal);
				}
				@this._ilg.Emit(OpCodes.Ldloc, valueLocal);
				@this.FreeLocal(valueLocal);
				LocalBuilder instanceLocal2 = instanceLocal;
				@this.EmitCall((instanceLocal2 != null) ? instanceLocal2.LocalType : null, pi.GetSetMethod(true));
			};
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001B6E8 File Offset: 0x000198E8
		private LambdaCompiler.WriteBack AddressOfWriteBack(IndexExpression node)
		{
			if (node.Indexer == null || !node.Indexer.CanWrite)
			{
				return null;
			}
			return this.AddressOfWriteBackCore(node);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001B710 File Offset: 0x00019910
		private LambdaCompiler.WriteBack AddressOfWriteBackCore(IndexExpression node)
		{
			LocalBuilder instanceLocal = null;
			Type type = null;
			if (node.Object != null)
			{
				this.EmitInstance(node.Object, out type);
				this._ilg.Emit(OpCodes.Dup);
				this._ilg.Emit(OpCodes.Stloc, instanceLocal = this.GetInstanceLocal(type));
			}
			int argumentCount = node.ArgumentCount;
			LocalBuilder[] args = new LocalBuilder[argumentCount];
			for (int i = 0; i < argumentCount; i++)
			{
				Expression argument = node.GetArgument(i);
				this.EmitExpression(argument);
				LocalBuilder local = this.GetLocal(argument.Type);
				this._ilg.Emit(OpCodes.Dup);
				this._ilg.Emit(OpCodes.Stloc, local);
				args[i] = local;
			}
			this.EmitGetIndexCall(node, type);
			LocalBuilder valueLocal = this.GetLocal(node.Type);
			this._ilg.Emit(OpCodes.Stloc, valueLocal);
			this._ilg.Emit(OpCodes.Ldloca, valueLocal);
			return delegate(LambdaCompiler @this)
			{
				if (instanceLocal != null)
				{
					@this._ilg.Emit(OpCodes.Ldloc, instanceLocal);
					@this.FreeLocal(instanceLocal);
				}
				foreach (LocalBuilder localBuilder in args)
				{
					@this._ilg.Emit(OpCodes.Ldloc, localBuilder);
					@this.FreeLocal(localBuilder);
				}
				@this._ilg.Emit(OpCodes.Ldloc, valueLocal);
				@this.FreeLocal(valueLocal);
				IndexExpression node2 = node;
				LocalBuilder instanceLocal2 = instanceLocal;
				@this.EmitSetIndexCall(node2, (instanceLocal2 != null) ? instanceLocal2.LocalType : null);
			};
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001B85C File Offset: 0x00019A5C
		private LocalBuilder GetInstanceLocal(Type type)
		{
			Type type2 = (type.IsValueType ? type.MakeByRefType() : type);
			return this.GetLocal(type2);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001B882 File Offset: 0x00019A82
		private void EmitBinaryExpression(Expression expr)
		{
			this.EmitBinaryExpression(expr, LambdaCompiler.CompilationFlags.EmitAsNoTail);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001B890 File Offset: 0x00019A90
		private void EmitBinaryExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			if (binaryExpression.Method != null)
			{
				this.EmitBinaryMethod(binaryExpression, flags);
				return;
			}
			if ((binaryExpression.NodeType == ExpressionType.Equal || binaryExpression.NodeType == ExpressionType.NotEqual) && (binaryExpression.Type == typeof(bool) || binaryExpression.Type == typeof(bool?)))
			{
				if (ConstantCheck.IsNull(binaryExpression.Left) && !ConstantCheck.IsNull(binaryExpression.Right) && binaryExpression.Right.Type.IsNullableType())
				{
					this.EmitNullEquality(binaryExpression.NodeType, binaryExpression.Right, binaryExpression.IsLiftedToNull);
					return;
				}
				if (ConstantCheck.IsNull(binaryExpression.Right) && !ConstantCheck.IsNull(binaryExpression.Left) && binaryExpression.Left.Type.IsNullableType())
				{
					this.EmitNullEquality(binaryExpression.NodeType, binaryExpression.Left, binaryExpression.IsLiftedToNull);
					return;
				}
				this.EmitExpression(LambdaCompiler.GetEqualityOperand(binaryExpression.Left));
				this.EmitExpression(LambdaCompiler.GetEqualityOperand(binaryExpression.Right));
			}
			else
			{
				this.EmitExpression(binaryExpression.Left);
				this.EmitExpression(binaryExpression.Right);
			}
			this.EmitBinaryOperator(binaryExpression.NodeType, binaryExpression.Left.Type, binaryExpression.Right.Type, binaryExpression.Type, binaryExpression.IsLiftedToNull);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001B9F8 File Offset: 0x00019BF8
		private void EmitNullEquality(ExpressionType op, Expression e, bool isLiftedToNull)
		{
			if (isLiftedToNull)
			{
				this.EmitExpressionAsVoid(e);
				this._ilg.EmitDefault(typeof(bool?), this);
				return;
			}
			this.EmitAddress(e, e.Type);
			this._ilg.EmitHasValue(e.Type);
			if (op == ExpressionType.Equal)
			{
				this._ilg.Emit(OpCodes.Ldc_I4_0);
				this._ilg.Emit(OpCodes.Ceq);
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001BA6C File Offset: 0x00019C6C
		private void EmitBinaryMethod(BinaryExpression b, LambdaCompiler.CompilationFlags flags)
		{
			if (b.IsLifted)
			{
				ParameterExpression parameterExpression = Expression.Variable(b.Left.Type.GetNonNullableType(), null);
				ParameterExpression parameterExpression2 = Expression.Variable(b.Right.Type.GetNonNullableType(), null);
				MethodCallExpression methodCallExpression = Expression.Call(null, b.Method, parameterExpression, parameterExpression2);
				Type type;
				if (b.IsLiftedToNull)
				{
					type = methodCallExpression.Type.GetNullableType();
				}
				else
				{
					type = typeof(bool);
				}
				this.EmitLift(b.NodeType, type, methodCallExpression, new ParameterExpression[] { parameterExpression, parameterExpression2 }, new Expression[] { b.Left, b.Right });
				return;
			}
			this.EmitMethodCallExpression(Expression.Call(null, b.Method, b.Left, b.Right), flags);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001BB36 File Offset: 0x00019D36
		private void EmitBinaryOperator(ExpressionType op, Type leftType, Type rightType, Type resultType, bool liftedToNull)
		{
			if (op == ExpressionType.ArrayIndex)
			{
				this.EmitGetArrayElement(leftType);
				return;
			}
			if (leftType.IsNullableType() || rightType.IsNullableType())
			{
				this.EmitLiftedBinaryOp(op, leftType, rightType, resultType, liftedToNull);
				return;
			}
			this.EmitUnliftedBinaryOp(op, leftType, rightType);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001BB6C File Offset: 0x00019D6C
		private void EmitUnliftedBinaryOp(ExpressionType op, Type leftType, Type rightType)
		{
			switch (op)
			{
			case ExpressionType.Add:
				this._ilg.Emit(OpCodes.Add);
				goto IL_034E;
			case ExpressionType.AddChecked:
				this._ilg.Emit(leftType.IsFloatingPoint() ? OpCodes.Add : (leftType.IsUnsigned() ? OpCodes.Add_Ovf_Un : OpCodes.Add_Ovf));
				goto IL_034E;
			case ExpressionType.And:
			case ExpressionType.AndAlso:
				this._ilg.Emit(OpCodes.And);
				return;
			case ExpressionType.ArrayLength:
			case ExpressionType.ArrayIndex:
			case ExpressionType.Call:
			case ExpressionType.Coalesce:
			case ExpressionType.Conditional:
			case ExpressionType.Constant:
			case ExpressionType.Convert:
			case ExpressionType.ConvertChecked:
			case ExpressionType.Invoke:
			case ExpressionType.Lambda:
			case ExpressionType.ListInit:
			case ExpressionType.MemberAccess:
			case ExpressionType.MemberInit:
				goto IL_034E;
			case ExpressionType.Divide:
				this._ilg.Emit(leftType.IsUnsigned() ? OpCodes.Div_Un : OpCodes.Div);
				goto IL_034E;
			case ExpressionType.Equal:
				break;
			case ExpressionType.ExclusiveOr:
				goto IL_02FD;
			case ExpressionType.GreaterThan:
				this._ilg.Emit(leftType.IsUnsigned() ? OpCodes.Cgt_Un : OpCodes.Cgt);
				return;
			case ExpressionType.GreaterThanOrEqual:
				this._ilg.Emit((leftType.IsUnsigned() || leftType.IsFloatingPoint()) ? OpCodes.Clt_Un : OpCodes.Clt);
				this._ilg.Emit(OpCodes.Ldc_I4_0);
				this._ilg.Emit(OpCodes.Ceq);
				return;
			case ExpressionType.LeftShift:
				this.EmitShiftMask(leftType);
				this._ilg.Emit(OpCodes.Shl);
				goto IL_034E;
			case ExpressionType.LessThan:
				this._ilg.Emit(leftType.IsUnsigned() ? OpCodes.Clt_Un : OpCodes.Clt);
				return;
			case ExpressionType.LessThanOrEqual:
				this._ilg.Emit((leftType.IsUnsigned() || leftType.IsFloatingPoint()) ? OpCodes.Cgt_Un : OpCodes.Cgt);
				this._ilg.Emit(OpCodes.Ldc_I4_0);
				this._ilg.Emit(OpCodes.Ceq);
				return;
			case ExpressionType.Modulo:
				this._ilg.Emit(leftType.IsUnsigned() ? OpCodes.Rem_Un : OpCodes.Rem);
				return;
			case ExpressionType.Multiply:
				this._ilg.Emit(OpCodes.Mul);
				goto IL_034E;
			case ExpressionType.MultiplyChecked:
				this._ilg.Emit(leftType.IsFloatingPoint() ? OpCodes.Mul : (leftType.IsUnsigned() ? OpCodes.Mul_Ovf_Un : OpCodes.Mul_Ovf));
				goto IL_034E;
			default:
				switch (op)
				{
				case ExpressionType.NotEqual:
					if (leftType.GetTypeCode() == TypeCode.Boolean)
					{
						goto IL_02FD;
					}
					this._ilg.Emit(OpCodes.Ceq);
					this._ilg.Emit(OpCodes.Ldc_I4_0);
					break;
				case ExpressionType.Or:
				case ExpressionType.OrElse:
					this._ilg.Emit(OpCodes.Or);
					return;
				case ExpressionType.Parameter:
				case ExpressionType.Power:
				case ExpressionType.Quote:
					goto IL_034E;
				case ExpressionType.RightShift:
					this.EmitShiftMask(leftType);
					this._ilg.Emit(leftType.IsUnsigned() ? OpCodes.Shr_Un : OpCodes.Shr);
					return;
				case ExpressionType.Subtract:
					this._ilg.Emit(OpCodes.Sub);
					goto IL_034E;
				case ExpressionType.SubtractChecked:
					if (leftType.IsUnsigned())
					{
						this._ilg.Emit(OpCodes.Sub_Ovf_Un);
						return;
					}
					this._ilg.Emit(leftType.IsFloatingPoint() ? OpCodes.Sub : OpCodes.Sub_Ovf);
					goto IL_034E;
				default:
					goto IL_034E;
				}
				break;
			}
			this._ilg.Emit(OpCodes.Ceq);
			return;
			IL_02FD:
			this._ilg.Emit(OpCodes.Xor);
			return;
			IL_034E:
			this.EmitConvertArithmeticResult(op, leftType);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001BED0 File Offset: 0x0001A0D0
		private void EmitShiftMask(Type leftType)
		{
			int num = (leftType.IsInteger64() ? 63 : 31);
			this._ilg.EmitPrimitive(num);
			this._ilg.Emit(OpCodes.And);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001BF08 File Offset: 0x0001A108
		private void EmitConvertArithmeticResult(ExpressionType op, Type resultType)
		{
			switch (resultType.GetTypeCode())
			{
			case TypeCode.SByte:
				this._ilg.Emit(LambdaCompiler.IsChecked(op) ? OpCodes.Conv_Ovf_I1 : OpCodes.Conv_I1);
				return;
			case TypeCode.Byte:
				this._ilg.Emit(LambdaCompiler.IsChecked(op) ? OpCodes.Conv_Ovf_U1 : OpCodes.Conv_U1);
				return;
			case TypeCode.Int16:
				this._ilg.Emit(LambdaCompiler.IsChecked(op) ? OpCodes.Conv_Ovf_I2 : OpCodes.Conv_I2);
				return;
			case TypeCode.UInt16:
				this._ilg.Emit(LambdaCompiler.IsChecked(op) ? OpCodes.Conv_Ovf_U2 : OpCodes.Conv_U2);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001BFB4 File Offset: 0x0001A1B4
		private void EmitLiftedBinaryOp(ExpressionType op, Type leftType, Type rightType, Type resultType, bool liftedToNull)
		{
			if (op > ExpressionType.And)
			{
				switch (op)
				{
				case ExpressionType.Divide:
				case ExpressionType.ExclusiveOr:
				case ExpressionType.LeftShift:
				case ExpressionType.Modulo:
				case ExpressionType.Multiply:
				case ExpressionType.MultiplyChecked:
					goto IL_00CF;
				case ExpressionType.Equal:
				case ExpressionType.GreaterThan:
				case ExpressionType.GreaterThanOrEqual:
				case ExpressionType.LessThan:
				case ExpressionType.LessThanOrEqual:
				case ExpressionType.NotEqual:
					if (liftedToNull)
					{
						this.EmitLiftedToNullRelational(op, leftType);
						return;
					}
					this.EmitLiftedRelational(op, leftType);
					break;
				case ExpressionType.Invoke:
				case ExpressionType.Lambda:
				case ExpressionType.ListInit:
				case ExpressionType.MemberAccess:
				case ExpressionType.MemberInit:
				case ExpressionType.Negate:
				case ExpressionType.UnaryPlus:
				case ExpressionType.NegateChecked:
				case ExpressionType.New:
				case ExpressionType.NewArrayInit:
				case ExpressionType.NewArrayBounds:
				case ExpressionType.Not:
					break;
				case ExpressionType.Or:
					if (leftType == typeof(bool?))
					{
						this.EmitLiftedBooleanOr();
						return;
					}
					this.EmitLiftedBinaryArithmetic(op, leftType, rightType, resultType);
					return;
				default:
					if (op - ExpressionType.RightShift > 2)
					{
						return;
					}
					goto IL_00CF;
				}
				return;
			}
			if (op > ExpressionType.AddChecked)
			{
				if (op != ExpressionType.And)
				{
					return;
				}
				if (leftType == typeof(bool?))
				{
					this.EmitLiftedBooleanAnd();
					return;
				}
				this.EmitLiftedBinaryArithmetic(op, leftType, rightType, resultType);
				return;
			}
			IL_00CF:
			this.EmitLiftedBinaryArithmetic(op, leftType, rightType, resultType);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001C0B4 File Offset: 0x0001A2B4
		private void EmitLiftedRelational(ExpressionType op, Type type)
		{
			bool flag = op == ExpressionType.NotEqual;
			if (flag)
			{
				op = ExpressionType.Equal;
			}
			LocalBuilder local = this.GetLocal(type);
			LocalBuilder local2 = this.GetLocal(type);
			this._ilg.Emit(OpCodes.Stloc, local2);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitGetValueOrDefault(type);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this._ilg.EmitGetValueOrDefault(type);
			Type nonNullableType = type.GetNonNullableType();
			this.EmitUnliftedBinaryOp(op, nonNullableType, nonNullableType);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitHasValue(type);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this._ilg.EmitHasValue(type);
			this.FreeLocal(local);
			this.FreeLocal(local2);
			this._ilg.Emit((op == ExpressionType.Equal) ? OpCodes.Ceq : OpCodes.And);
			this._ilg.Emit(OpCodes.And);
			if (flag)
			{
				this._ilg.Emit(OpCodes.Ldc_I4_0);
				this._ilg.Emit(OpCodes.Ceq);
			}
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001C1E0 File Offset: 0x0001A3E0
		private void EmitLiftedToNullRelational(ExpressionType op, Type type)
		{
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			LocalBuilder local = this.GetLocal(type);
			LocalBuilder local2 = this.GetLocal(type);
			this._ilg.Emit(OpCodes.Stloc, local2);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitHasValue(type);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this._ilg.EmitHasValue(type);
			this._ilg.Emit(OpCodes.And);
			this._ilg.Emit(OpCodes.Brtrue_S, label);
			this._ilg.EmitDefault(typeof(bool?), this);
			this._ilg.Emit(OpCodes.Br_S, label2);
			this._ilg.MarkLabel(label);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitGetValueOrDefault(type);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this._ilg.EmitGetValueOrDefault(type);
			this.FreeLocal(local);
			this.FreeLocal(local2);
			Type nonNullableType = type.GetNonNullableType();
			this.EmitUnliftedBinaryOp(op, nonNullableType, nonNullableType);
			this._ilg.Emit(OpCodes.Newobj, CachedReflectionInfo.Nullable_Boolean_Ctor);
			this._ilg.MarkLabel(label2);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001C344 File Offset: 0x0001A544
		private void EmitLiftedBinaryArithmetic(ExpressionType op, Type leftType, Type rightType, Type resultType)
		{
			bool flag = leftType.IsNullableType();
			bool flag2 = rightType.IsNullableType();
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			LocalBuilder local = this.GetLocal(leftType);
			LocalBuilder local2 = this.GetLocal(rightType);
			LocalBuilder local3 = this.GetLocal(resultType);
			this._ilg.Emit(OpCodes.Stloc, local2);
			this._ilg.Emit(OpCodes.Stloc, local);
			if (flag)
			{
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.EmitHasValue(leftType);
			}
			if (flag2)
			{
				this._ilg.Emit(OpCodes.Ldloca, local2);
				this._ilg.EmitHasValue(rightType);
				if (flag)
				{
					this._ilg.Emit(OpCodes.And);
				}
			}
			this._ilg.Emit(OpCodes.Brfalse_S, label);
			if (flag)
			{
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.EmitGetValueOrDefault(leftType);
			}
			else
			{
				this._ilg.Emit(OpCodes.Ldloc, local);
			}
			if (flag2)
			{
				this._ilg.Emit(OpCodes.Ldloca, local2);
				this._ilg.EmitGetValueOrDefault(rightType);
			}
			else
			{
				this._ilg.Emit(OpCodes.Ldloc, local2);
			}
			this.FreeLocal(local);
			this.FreeLocal(local2);
			this.EmitBinaryOperator(op, leftType.GetNonNullableType(), rightType.GetNonNullableType(), resultType.GetNonNullableType(), false);
			ConstructorInfo constructor = resultType.GetConstructor(new Type[] { resultType.GetNonNullableType() });
			this._ilg.Emit(OpCodes.Newobj, constructor);
			this._ilg.Emit(OpCodes.Stloc, local3);
			this._ilg.Emit(OpCodes.Br_S, label2);
			this._ilg.MarkLabel(label);
			this._ilg.Emit(OpCodes.Ldloca, local3);
			this._ilg.Emit(OpCodes.Initobj, resultType);
			this._ilg.MarkLabel(label2);
			this._ilg.Emit(OpCodes.Ldloc, local3);
			this.FreeLocal(local3);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001C550 File Offset: 0x0001A750
		private void EmitLiftedBooleanAnd()
		{
			Type typeFromHandle = typeof(bool?);
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			LocalBuilder local = this.GetLocal(typeFromHandle);
			LocalBuilder local2 = this.GetLocal(typeFromHandle);
			this._ilg.Emit(OpCodes.Stloc, local2);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Brtrue_S, label);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitHasValue(typeFromHandle);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Or);
			this._ilg.Emit(OpCodes.Brfalse_S, label);
			this._ilg.Emit(OpCodes.Ldloc, local);
			this.FreeLocal(local);
			this._ilg.Emit(OpCodes.Br_S, label2);
			this._ilg.MarkLabel(label);
			this._ilg.Emit(OpCodes.Ldloc, local2);
			this.FreeLocal(local2);
			this._ilg.MarkLabel(label2);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001C69C File Offset: 0x0001A89C
		private void EmitLiftedBooleanOr()
		{
			Type typeFromHandle = typeof(bool?);
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			LocalBuilder local = this.GetLocal(typeFromHandle);
			LocalBuilder local2 = this.GetLocal(typeFromHandle);
			this._ilg.Emit(OpCodes.Stloc, local2);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Brtrue_S, label);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitHasValue(typeFromHandle);
			this._ilg.Emit(OpCodes.Or);
			this._ilg.Emit(OpCodes.Brfalse_S, label);
			this._ilg.Emit(OpCodes.Ldloc, local2);
			this.FreeLocal(local2);
			this._ilg.Emit(OpCodes.Br_S, label2);
			this._ilg.MarkLabel(label);
			this._ilg.Emit(OpCodes.Ldloc, local);
			this.FreeLocal(local);
			this._ilg.MarkLabel(label2);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		private LabelInfo EnsureLabel(LabelTarget node)
		{
			LabelInfo labelInfo;
			if (!this._labelInfo.TryGetValue(node, out labelInfo))
			{
				this._labelInfo.Add(node, labelInfo = new LabelInfo(this._ilg, node, false));
			}
			return labelInfo;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001C821 File Offset: 0x0001AA21
		private LabelInfo ReferenceLabel(LabelTarget node)
		{
			LabelInfo labelInfo = this.EnsureLabel(node);
			labelInfo.Reference(this._labelBlock);
			return labelInfo;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001C836 File Offset: 0x0001AA36
		private LabelInfo DefineLabel(LabelTarget node)
		{
			if (node == null)
			{
				return new LabelInfo(this._ilg, null, false);
			}
			LabelInfo labelInfo = this.EnsureLabel(node);
			labelInfo.Define(this._labelBlock);
			return labelInfo;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001C85C File Offset: 0x0001AA5C
		private void PushLabelBlock(LabelScopeKind type)
		{
			this._labelBlock = new LabelScopeInfo(this._labelBlock, type);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001C870 File Offset: 0x0001AA70
		private void PopLabelBlock(LabelScopeKind kind)
		{
			this._labelBlock = this._labelBlock.Parent;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001C884 File Offset: 0x0001AA84
		private void EmitLabelExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			LabelExpression labelExpression = (LabelExpression)expr;
			LabelInfo labelInfo = null;
			if (this._labelBlock.Kind == LabelScopeKind.Block)
			{
				this._labelBlock.TryGetLabelInfo(labelExpression.Target, out labelInfo);
				if (labelInfo == null && this._labelBlock.Parent.Kind == LabelScopeKind.Switch)
				{
					this._labelBlock.Parent.TryGetLabelInfo(labelExpression.Target, out labelInfo);
				}
			}
			if (labelInfo == null)
			{
				labelInfo = this.DefineLabel(labelExpression.Target);
			}
			if (labelExpression.DefaultValue != null)
			{
				if (labelExpression.Target.Type == typeof(void))
				{
					this.EmitExpressionAsVoid(labelExpression.DefaultValue, flags);
				}
				else
				{
					flags = LambdaCompiler.UpdateEmitExpressionStartFlag(flags, LambdaCompiler.CompilationFlags.EmitExpressionStart);
					this.EmitExpression(labelExpression.DefaultValue, flags);
				}
			}
			labelInfo.Mark();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001C94C File Offset: 0x0001AB4C
		private void EmitGotoExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			GotoExpression gotoExpression = (GotoExpression)expr;
			LabelInfo labelInfo = this.ReferenceLabel(gotoExpression.Target);
			LambdaCompiler.CompilationFlags compilationFlags = flags & LambdaCompiler.CompilationFlags.EmitAsTailCallMask;
			if (compilationFlags != LambdaCompiler.CompilationFlags.EmitAsNoTail)
			{
				compilationFlags = (labelInfo.CanReturn ? LambdaCompiler.CompilationFlags.EmitAsTail : LambdaCompiler.CompilationFlags.EmitAsNoTail);
				flags = LambdaCompiler.UpdateEmitAsTailCallFlag(flags, compilationFlags);
			}
			if (gotoExpression.Value != null)
			{
				if (gotoExpression.Target.Type == typeof(void))
				{
					this.EmitExpressionAsVoid(gotoExpression.Value, flags);
				}
				else
				{
					flags = LambdaCompiler.UpdateEmitExpressionStartFlag(flags, LambdaCompiler.CompilationFlags.EmitExpressionStart);
					this.EmitExpression(gotoExpression.Value, flags);
				}
			}
			labelInfo.EmitJump();
			this.EmitUnreachable(gotoExpression, flags);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001C9F2 File Offset: 0x0001ABF2
		private void EmitUnreachable(Expression node, LambdaCompiler.CompilationFlags flags)
		{
			if (node.Type != typeof(void) && (flags & LambdaCompiler.CompilationFlags.EmitAsVoidType) == (LambdaCompiler.CompilationFlags)0)
			{
				this._ilg.EmitDefault(node.Type, this);
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001CA24 File Offset: 0x0001AC24
		private bool TryPushLabelBlock(Expression node)
		{
			ExpressionType nodeType = node.NodeType;
			if (nodeType <= ExpressionType.Convert)
			{
				if (nodeType == ExpressionType.Conditional)
				{
					goto IL_015F;
				}
				if (nodeType == ExpressionType.Convert)
				{
					if (!(node.Type != typeof(void)))
					{
						this.PushLabelBlock(LabelScopeKind.Statement);
						return true;
					}
				}
			}
			else if (nodeType != ExpressionType.Block)
			{
				switch (nodeType)
				{
				case ExpressionType.Goto:
				case ExpressionType.Loop:
					goto IL_015F;
				case ExpressionType.Label:
					if (this._labelBlock.Kind == LabelScopeKind.Block)
					{
						LabelTarget target = ((LabelExpression)node).Target;
						if (this._labelBlock.ContainsTarget(target))
						{
							return false;
						}
						if (this._labelBlock.Parent.Kind == LabelScopeKind.Switch && this._labelBlock.Parent.ContainsTarget(target))
						{
							return false;
						}
					}
					this.PushLabelBlock(LabelScopeKind.Statement);
					return true;
				case ExpressionType.Switch:
				{
					this.PushLabelBlock(LabelScopeKind.Switch);
					SwitchExpression switchExpression = (SwitchExpression)node;
					foreach (SwitchCase switchCase in switchExpression.Cases)
					{
						this.DefineBlockLabels(switchCase.Body);
					}
					this.DefineBlockLabels(switchExpression.DefaultBody);
					return true;
				}
				}
			}
			else if (!(node is SpilledExpressionBlock))
			{
				this.PushLabelBlock(LabelScopeKind.Block);
				if (this._labelBlock.Parent.Kind != LabelScopeKind.Switch)
				{
					this.DefineBlockLabels(node);
				}
				return true;
			}
			if (this._labelBlock.Kind != LabelScopeKind.Expression)
			{
				this.PushLabelBlock(LabelScopeKind.Expression);
				return true;
			}
			return false;
			IL_015F:
			this.PushLabelBlock(LabelScopeKind.Statement);
			return true;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001CBA8 File Offset: 0x0001ADA8
		private void DefineBlockLabels(Expression node)
		{
			BlockExpression blockExpression = node as BlockExpression;
			if (blockExpression == null || blockExpression is SpilledExpressionBlock)
			{
				return;
			}
			int i = 0;
			int expressionCount = blockExpression.ExpressionCount;
			while (i < expressionCount)
			{
				LabelExpression labelExpression = blockExpression.GetExpression(i) as LabelExpression;
				if (labelExpression != null)
				{
					this.DefineLabel(labelExpression.Target);
				}
				i++;
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001CBF8 File Offset: 0x0001ADF8
		private void AddReturnLabel(LambdaExpression lambda)
		{
			Expression expression = lambda.Body;
			ExpressionType nodeType;
			for (;;)
			{
				nodeType = expression.NodeType;
				if (nodeType != ExpressionType.Block)
				{
					break;
				}
				BlockExpression blockExpression = (BlockExpression)expression;
				if (blockExpression.ExpressionCount == 0)
				{
					return;
				}
				for (int i = blockExpression.ExpressionCount - 1; i >= 0; i--)
				{
					expression = blockExpression.GetExpression(i);
					if (LambdaCompiler.Significant(expression))
					{
						break;
					}
				}
			}
			if (nodeType != ExpressionType.Label)
			{
				return;
			}
			LabelTarget target = ((LabelExpression)expression).Target;
			this._labelInfo.Add(target, new LabelInfo(this._ilg, target, TypeUtils.AreReferenceAssignable(lambda.ReturnType, target.Type)));
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001CC90 File Offset: 0x0001AE90
		private static LambdaCompiler.CompilationFlags UpdateEmitAsTailCallFlag(LambdaCompiler.CompilationFlags flags, LambdaCompiler.CompilationFlags newValue)
		{
			LambdaCompiler.CompilationFlags compilationFlags = flags & LambdaCompiler.CompilationFlags.EmitAsTailCallMask;
			return (flags ^ compilationFlags) | newValue;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001CCAC File Offset: 0x0001AEAC
		private static LambdaCompiler.CompilationFlags UpdateEmitExpressionStartFlag(LambdaCompiler.CompilationFlags flags, LambdaCompiler.CompilationFlags newValue)
		{
			LambdaCompiler.CompilationFlags compilationFlags = flags & LambdaCompiler.CompilationFlags.EmitExpressionStartMask;
			return (flags ^ compilationFlags) | newValue;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001CCC4 File Offset: 0x0001AEC4
		private static LambdaCompiler.CompilationFlags UpdateEmitAsTypeFlag(LambdaCompiler.CompilationFlags flags, LambdaCompiler.CompilationFlags newValue)
		{
			LambdaCompiler.CompilationFlags compilationFlags = flags & LambdaCompiler.CompilationFlags.EmitAsTypeMask;
			return (flags ^ compilationFlags) | newValue;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001CCDE File Offset: 0x0001AEDE
		internal void EmitExpression(Expression node)
		{
			this.EmitExpression(node, LambdaCompiler.CompilationFlags.EmitExpressionStart | LambdaCompiler.CompilationFlags.EmitAsNoTail);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001CCEC File Offset: 0x0001AEEC
		private void EmitExpressionAsVoid(Expression node)
		{
			this.EmitExpressionAsVoid(node, LambdaCompiler.CompilationFlags.EmitAsNoTail);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001CCFC File Offset: 0x0001AEFC
		private void EmitExpressionAsVoid(Expression node, LambdaCompiler.CompilationFlags flags)
		{
			LambdaCompiler.CompilationFlags compilationFlags = this.EmitExpressionStart(node);
			ExpressionType nodeType = node.NodeType;
			if (nodeType <= ExpressionType.Assign)
			{
				if (nodeType == ExpressionType.Constant || nodeType == ExpressionType.Parameter)
				{
					goto IL_00D5;
				}
				if (nodeType == ExpressionType.Assign)
				{
					this.EmitAssign((AssignBinaryExpression)node, LambdaCompiler.CompilationFlags.EmitAsVoidType);
					goto IL_00D5;
				}
			}
			else if (nodeType <= ExpressionType.Default)
			{
				if (nodeType == ExpressionType.Block)
				{
					this.Emit((BlockExpression)node, LambdaCompiler.UpdateEmitAsTypeFlag(flags, LambdaCompiler.CompilationFlags.EmitAsVoidType));
					goto IL_00D5;
				}
				if (nodeType == ExpressionType.Default)
				{
					goto IL_00D5;
				}
			}
			else
			{
				if (nodeType == ExpressionType.Goto)
				{
					this.EmitGotoExpression(node, LambdaCompiler.UpdateEmitAsTypeFlag(flags, LambdaCompiler.CompilationFlags.EmitAsVoidType));
					goto IL_00D5;
				}
				if (nodeType == ExpressionType.Throw)
				{
					this.EmitThrow((UnaryExpression)node, LambdaCompiler.CompilationFlags.EmitAsVoidType);
					goto IL_00D5;
				}
			}
			if (node.Type == typeof(void))
			{
				this.EmitExpression(node, LambdaCompiler.UpdateEmitExpressionStartFlag(flags, LambdaCompiler.CompilationFlags.EmitNoExpressionStart));
			}
			else
			{
				this.EmitExpression(node, LambdaCompiler.CompilationFlags.EmitNoExpressionStart | LambdaCompiler.CompilationFlags.EmitAsNoTail);
				this._ilg.Emit(OpCodes.Pop);
			}
			IL_00D5:
			this.EmitExpressionEnd(compilationFlags);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001CDE8 File Offset: 0x0001AFE8
		private void EmitExpressionAsType(Expression node, Type type, LambdaCompiler.CompilationFlags flags)
		{
			if (type == typeof(void))
			{
				this.EmitExpressionAsVoid(node, flags);
				return;
			}
			if (!TypeUtils.AreEquivalent(node.Type, type))
			{
				this.EmitExpression(node);
				this._ilg.Emit(OpCodes.Castclass, type);
				return;
			}
			this.EmitExpression(node, LambdaCompiler.UpdateEmitExpressionStartFlag(flags, LambdaCompiler.CompilationFlags.EmitExpressionStart));
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001CE45 File Offset: 0x0001B045
		private LambdaCompiler.CompilationFlags EmitExpressionStart(Expression node)
		{
			if (this.TryPushLabelBlock(node))
			{
				return LambdaCompiler.CompilationFlags.EmitExpressionStart;
			}
			return LambdaCompiler.CompilationFlags.EmitNoExpressionStart;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001CE53 File Offset: 0x0001B053
		private void EmitExpressionEnd(LambdaCompiler.CompilationFlags flags)
		{
			if ((flags & LambdaCompiler.CompilationFlags.EmitExpressionStartMask) == LambdaCompiler.CompilationFlags.EmitExpressionStart)
			{
				this.PopLabelBlock(this._labelBlock.Kind);
			}
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001CE70 File Offset: 0x0001B070
		private void EmitInvocationExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			InvocationExpression invocationExpression = (InvocationExpression)expr;
			if (invocationExpression.LambdaOperand != null)
			{
				this.EmitInlinedInvoke(invocationExpression, flags);
				return;
			}
			expr = invocationExpression.Expression;
			this.EmitMethodCall(expr, expr.Type.GetInvokeMethod(), invocationExpression, LambdaCompiler.CompilationFlags.EmitExpressionStart | LambdaCompiler.CompilationFlags.EmitAsNoTail);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001CEB8 File Offset: 0x0001B0B8
		private void EmitInlinedInvoke(InvocationExpression invoke, LambdaCompiler.CompilationFlags flags)
		{
			LambdaExpression lambdaOperand = invoke.LambdaOperand;
			List<LambdaCompiler.WriteBack> list = this.EmitArguments(lambdaOperand.Type.GetInvokeMethod(), invoke);
			LambdaCompiler lambdaCompiler = new LambdaCompiler(this, lambdaOperand, invoke);
			if (list != null)
			{
				flags = LambdaCompiler.UpdateEmitAsTailCallFlag(flags, LambdaCompiler.CompilationFlags.EmitAsNoTail);
			}
			lambdaCompiler.EmitLambdaBody(this._scope, true, flags);
			this.EmitWriteBack(list);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001CF0C File Offset: 0x0001B10C
		private void EmitIndexExpression(Expression expr)
		{
			IndexExpression indexExpression = (IndexExpression)expr;
			Type type = null;
			if (indexExpression.Object != null)
			{
				this.EmitInstance(indexExpression.Object, out type);
			}
			int i = 0;
			int argumentCount = indexExpression.ArgumentCount;
			while (i < argumentCount)
			{
				Expression argument = indexExpression.GetArgument(i);
				this.EmitExpression(argument);
				i++;
			}
			this.EmitGetIndexCall(indexExpression, type);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001CF64 File Offset: 0x0001B164
		private void EmitIndexAssignment(AssignBinaryExpression node, LambdaCompiler.CompilationFlags flags)
		{
			IndexExpression indexExpression = (IndexExpression)node.Left;
			LambdaCompiler.CompilationFlags compilationFlags = flags & LambdaCompiler.CompilationFlags.EmitAsTypeMask;
			Type type = null;
			if (indexExpression.Object != null)
			{
				this.EmitInstance(indexExpression.Object, out type);
			}
			int i = 0;
			int argumentCount = indexExpression.ArgumentCount;
			while (i < argumentCount)
			{
				Expression argument = indexExpression.GetArgument(i);
				this.EmitExpression(argument);
				i++;
			}
			this.EmitExpression(node.Right);
			LocalBuilder localBuilder = null;
			if (compilationFlags != LambdaCompiler.CompilationFlags.EmitAsVoidType)
			{
				this._ilg.Emit(OpCodes.Dup);
				this._ilg.Emit(OpCodes.Stloc, localBuilder = this.GetLocal(node.Type));
			}
			this.EmitSetIndexCall(indexExpression, type);
			if (compilationFlags != LambdaCompiler.CompilationFlags.EmitAsVoidType)
			{
				this._ilg.Emit(OpCodes.Ldloc, localBuilder);
				this.FreeLocal(localBuilder);
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001D030 File Offset: 0x0001B230
		private void EmitGetIndexCall(IndexExpression node, Type objectType)
		{
			if (node.Indexer != null)
			{
				MethodInfo getMethod = node.Indexer.GetGetMethod(true);
				this.EmitCall(objectType, getMethod);
				return;
			}
			this.EmitGetArrayElement(objectType);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001D068 File Offset: 0x0001B268
		private void EmitGetArrayElement(Type arrayType)
		{
			if (arrayType.IsSZArray)
			{
				this._ilg.EmitLoadElement(arrayType.GetElementType());
				return;
			}
			this._ilg.Emit(OpCodes.Call, arrayType.GetMethod("Get", BindingFlags.Instance | BindingFlags.Public));
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001D0A4 File Offset: 0x0001B2A4
		private void EmitSetIndexCall(IndexExpression node, Type objectType)
		{
			if (node.Indexer != null)
			{
				MethodInfo setMethod = node.Indexer.GetSetMethod(true);
				this.EmitCall(objectType, setMethod);
				return;
			}
			this.EmitSetArrayElement(objectType);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001D0DC File Offset: 0x0001B2DC
		private void EmitSetArrayElement(Type arrayType)
		{
			if (arrayType.IsSZArray)
			{
				this._ilg.EmitStoreElement(arrayType.GetElementType());
				return;
			}
			this._ilg.Emit(OpCodes.Call, arrayType.GetMethod("Set", BindingFlags.Instance | BindingFlags.Public));
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001D118 File Offset: 0x0001B318
		private void EmitMethodCallExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			MethodCallExpression methodCallExpression = (MethodCallExpression)expr;
			this.EmitMethodCall(methodCallExpression.Object, methodCallExpression.Method, methodCallExpression, flags);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001D140 File Offset: 0x0001B340
		private void EmitMethodCallExpression(Expression expr)
		{
			this.EmitMethodCallExpression(expr, LambdaCompiler.CompilationFlags.EmitAsNoTail);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001D14E File Offset: 0x0001B34E
		private void EmitMethodCall(Expression obj, MethodInfo method, IArgumentProvider methodCallExpr)
		{
			this.EmitMethodCall(obj, method, methodCallExpr, LambdaCompiler.CompilationFlags.EmitAsNoTail);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001D160 File Offset: 0x0001B360
		private void EmitMethodCall(Expression obj, MethodInfo method, IArgumentProvider methodCallExpr, LambdaCompiler.CompilationFlags flags)
		{
			Type type = null;
			if (!method.IsStatic)
			{
				this.EmitInstance(obj, out type);
			}
			if (obj != null && obj.Type.IsValueType)
			{
				this.EmitMethodCall(method, methodCallExpr, type);
				return;
			}
			this.EmitMethodCall(method, methodCallExpr, type, flags);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001D1A5 File Offset: 0x0001B3A5
		private void EmitMethodCall(MethodInfo mi, IArgumentProvider args, Type objectType)
		{
			this.EmitMethodCall(mi, args, objectType, LambdaCompiler.CompilationFlags.EmitAsNoTail);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001D1B8 File Offset: 0x0001B3B8
		private void EmitMethodCall(MethodInfo mi, IArgumentProvider args, Type objectType, LambdaCompiler.CompilationFlags flags)
		{
			List<LambdaCompiler.WriteBack> list = this.EmitArguments(mi, args);
			OpCode opCode = (LambdaCompiler.UseVirtual(mi) ? OpCodes.Callvirt : OpCodes.Call);
			if (opCode == OpCodes.Callvirt && objectType.IsValueType)
			{
				this._ilg.Emit(OpCodes.Constrained, objectType);
			}
			if ((flags & LambdaCompiler.CompilationFlags.EmitAsTailCallMask) == LambdaCompiler.CompilationFlags.EmitAsTail && !LambdaCompiler.MethodHasByRefParameter(mi))
			{
				this._ilg.Emit(OpCodes.Tailcall);
			}
			if (mi.CallingConvention == CallingConventions.VarArgs)
			{
				int argumentCount = args.ArgumentCount;
				Type[] array = new Type[argumentCount];
				for (int i = 0; i < argumentCount; i++)
				{
					array[i] = args.GetArgument(i).Type;
				}
				this._ilg.EmitCall(opCode, mi, array);
			}
			else
			{
				this._ilg.Emit(opCode, mi);
			}
			this.EmitWriteBack(list);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001D28C File Offset: 0x0001B48C
		private static bool MethodHasByRefParameter(MethodInfo mi)
		{
			ParameterInfo[] parametersCached = mi.GetParametersCached();
			for (int i = 0; i < parametersCached.Length; i++)
			{
				if (parametersCached[i].IsByRefParameter())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001D2BC File Offset: 0x0001B4BC
		private void EmitCall(Type objectType, MethodInfo method)
		{
			if (method.CallingConvention == CallingConventions.VarArgs)
			{
				throw Error.UnexpectedVarArgsCall(method);
			}
			OpCode opCode = (LambdaCompiler.UseVirtual(method) ? OpCodes.Callvirt : OpCodes.Call);
			if (opCode == OpCodes.Callvirt && objectType.IsValueType)
			{
				this._ilg.Emit(OpCodes.Constrained, objectType);
			}
			this._ilg.Emit(opCode, method);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001D321 File Offset: 0x0001B521
		private static bool UseVirtual(MethodInfo mi)
		{
			return !mi.IsStatic && !mi.DeclaringType.IsValueType;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001D33D File Offset: 0x0001B53D
		private List<LambdaCompiler.WriteBack> EmitArguments(MethodBase method, IArgumentProvider args)
		{
			return this.EmitArguments(method, args, 0);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001D348 File Offset: 0x0001B548
		private List<LambdaCompiler.WriteBack> EmitArguments(MethodBase method, IArgumentProvider args, int skipParameters)
		{
			ParameterInfo[] parametersCached = method.GetParametersCached();
			List<LambdaCompiler.WriteBack> list = null;
			int i = skipParameters;
			int num = parametersCached.Length;
			while (i < num)
			{
				ParameterInfo parameterInfo = parametersCached[i];
				Expression argument = args.GetArgument(i - skipParameters);
				Type type = parameterInfo.ParameterType;
				if (type.IsByRef)
				{
					type = type.GetElementType();
					LambdaCompiler.WriteBack writeBack = this.EmitAddressWriteBack(argument, type);
					if (writeBack != null)
					{
						if (list == null)
						{
							list = new List<LambdaCompiler.WriteBack>();
						}
						list.Add(writeBack);
					}
				}
				else
				{
					this.EmitExpression(argument);
				}
				i++;
			}
			return list;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001D3C4 File Offset: 0x0001B5C4
		private void EmitWriteBack(List<LambdaCompiler.WriteBack> writeBacks)
		{
			if (writeBacks != null)
			{
				foreach (LambdaCompiler.WriteBack writeBack in writeBacks)
				{
					writeBack(this);
				}
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001D414 File Offset: 0x0001B614
		private void EmitConstantExpression(Expression expr)
		{
			ConstantExpression constantExpression = (ConstantExpression)expr;
			this.EmitConstant(constantExpression.Value, constantExpression.Type);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001D43A File Offset: 0x0001B63A
		private void EmitConstant(object value)
		{
			this.EmitConstant(value, value.GetType());
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001D449 File Offset: 0x0001B649
		private void EmitConstant(object value, Type type)
		{
			if (!this._ilg.TryEmitConstant(value, type, this))
			{
				this._boundConstants.EmitConstant(this, value, type);
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001D46C File Offset: 0x0001B66C
		private void EmitDynamicExpression(Expression expr)
		{
			if (!(this._method is DynamicMethod))
			{
				throw Error.CannotCompileDynamic();
			}
			IDynamicExpression dynamicExpression = (IDynamicExpression)expr;
			object obj = dynamicExpression.CreateCallSite();
			Type type = obj.GetType();
			MethodInfo invokeMethod = dynamicExpression.DelegateType.GetInvokeMethod();
			this.EmitConstant(obj, type);
			this._ilg.Emit(OpCodes.Dup);
			LocalBuilder local = this.GetLocal(type);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldfld, type.GetField("Target"));
			this._ilg.Emit(OpCodes.Ldloc, local);
			this.FreeLocal(local);
			List<LambdaCompiler.WriteBack> list = this.EmitArguments(invokeMethod, dynamicExpression, 1);
			this._ilg.Emit(OpCodes.Callvirt, invokeMethod);
			this.EmitWriteBack(list);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001D53C File Offset: 0x0001B73C
		private void EmitNewExpression(Expression expr)
		{
			NewExpression newExpression = (NewExpression)expr;
			if (!(newExpression.Constructor != null))
			{
				LocalBuilder local = this.GetLocal(newExpression.Type);
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.Emit(OpCodes.Initobj, newExpression.Type);
				this._ilg.Emit(OpCodes.Ldloc, local);
				this.FreeLocal(local);
				return;
			}
			if (newExpression.Constructor.DeclaringType.IsAbstract)
			{
				throw Error.NonAbstractConstructorRequired();
			}
			List<LambdaCompiler.WriteBack> list = this.EmitArguments(newExpression.Constructor, newExpression);
			this._ilg.Emit(OpCodes.Newobj, newExpression.Constructor);
			this.EmitWriteBack(list);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001D5F0 File Offset: 0x0001B7F0
		private void EmitTypeBinaryExpression(Expression expr)
		{
			TypeBinaryExpression typeBinaryExpression = (TypeBinaryExpression)expr;
			if (typeBinaryExpression.NodeType == ExpressionType.TypeEqual)
			{
				this.EmitExpression(typeBinaryExpression.ReduceTypeEqual());
				return;
			}
			Type type = typeBinaryExpression.Expression.Type;
			AnalyzeTypeIsResult analyzeTypeIsResult = ConstantCheck.AnalyzeTypeIs(typeBinaryExpression);
			if (analyzeTypeIsResult == AnalyzeTypeIsResult.KnownTrue || analyzeTypeIsResult == AnalyzeTypeIsResult.KnownFalse)
			{
				this.EmitExpressionAsVoid(typeBinaryExpression.Expression);
				this._ilg.EmitPrimitive(analyzeTypeIsResult == AnalyzeTypeIsResult.KnownTrue);
				return;
			}
			if (analyzeTypeIsResult != AnalyzeTypeIsResult.KnownAssignable)
			{
				this.EmitExpression(typeBinaryExpression.Expression);
				if (type.IsValueType)
				{
					this._ilg.Emit(OpCodes.Box, type);
				}
				this._ilg.Emit(OpCodes.Isinst, typeBinaryExpression.TypeOperand);
				this._ilg.Emit(OpCodes.Ldnull);
				this._ilg.Emit(OpCodes.Cgt_Un);
				return;
			}
			if (type.IsNullableType())
			{
				this.EmitAddress(typeBinaryExpression.Expression, type);
				this._ilg.EmitHasValue(type);
				return;
			}
			this.EmitExpression(typeBinaryExpression.Expression);
			this._ilg.Emit(OpCodes.Ldnull);
			this._ilg.Emit(OpCodes.Cgt_Un);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001D700 File Offset: 0x0001B900
		private void EmitVariableAssignment(AssignBinaryExpression node, LambdaCompiler.CompilationFlags flags)
		{
			ParameterExpression parameterExpression = (ParameterExpression)node.Left;
			int num = (int)(flags & LambdaCompiler.CompilationFlags.EmitAsTypeMask);
			if (node.IsByRef)
			{
				this.EmitAddress(node.Right, node.Right.Type);
			}
			else
			{
				this.EmitExpression(node.Right);
			}
			if (num != 32)
			{
				this._ilg.Emit(OpCodes.Dup);
			}
			if (parameterExpression.IsByRef)
			{
				LocalBuilder local = this.GetLocal(parameterExpression.Type);
				this._ilg.Emit(OpCodes.Stloc, local);
				this._scope.EmitGet(parameterExpression);
				this._ilg.Emit(OpCodes.Ldloc, local);
				this.FreeLocal(local);
				this._ilg.EmitStoreValueIndirect(parameterExpression.Type);
				return;
			}
			this._scope.EmitSet(parameterExpression);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001D7C9 File Offset: 0x0001B9C9
		private void EmitAssignBinaryExpression(Expression expr)
		{
			this.EmitAssign((AssignBinaryExpression)expr, LambdaCompiler.CompilationFlags.EmitAsDefaultType);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001D7DC File Offset: 0x0001B9DC
		private void EmitAssign(AssignBinaryExpression node, LambdaCompiler.CompilationFlags emitAs)
		{
			ExpressionType nodeType = node.Left.NodeType;
			if (nodeType == ExpressionType.MemberAccess)
			{
				this.EmitMemberAssignment(node, emitAs);
				return;
			}
			if (nodeType == ExpressionType.Parameter)
			{
				this.EmitVariableAssignment(node, emitAs);
				return;
			}
			if (nodeType == ExpressionType.Index)
			{
				this.EmitIndexAssignment(node, emitAs);
				return;
			}
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001D824 File Offset: 0x0001BA24
		private void EmitParameterExpression(Expression expr)
		{
			ParameterExpression parameterExpression = (ParameterExpression)expr;
			this._scope.EmitGet(parameterExpression);
			if (parameterExpression.IsByRef)
			{
				this._ilg.EmitLoadValueIndirect(parameterExpression.Type);
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001D860 File Offset: 0x0001BA60
		private void EmitLambdaExpression(Expression expr)
		{
			LambdaExpression lambdaExpression = (LambdaExpression)expr;
			this.EmitDelegateConstruction(lambdaExpression);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001D87C File Offset: 0x0001BA7C
		private void EmitRuntimeVariablesExpression(Expression expr)
		{
			RuntimeVariablesExpression runtimeVariablesExpression = (RuntimeVariablesExpression)expr;
			this._scope.EmitVariableAccess(this, runtimeVariablesExpression.Variables);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001D8A4 File Offset: 0x0001BAA4
		private void EmitMemberAssignment(AssignBinaryExpression node, LambdaCompiler.CompilationFlags flags)
		{
			MemberExpression memberExpression = (MemberExpression)node.Left;
			MemberInfo member = memberExpression.Member;
			Type type = null;
			if (memberExpression.Expression != null)
			{
				this.EmitInstance(memberExpression.Expression, out type);
			}
			this.EmitExpression(node.Right);
			LocalBuilder localBuilder = null;
			LambdaCompiler.CompilationFlags compilationFlags = flags & LambdaCompiler.CompilationFlags.EmitAsTypeMask;
			if (compilationFlags != LambdaCompiler.CompilationFlags.EmitAsVoidType)
			{
				this._ilg.Emit(OpCodes.Dup);
				this._ilg.Emit(OpCodes.Stloc, localBuilder = this.GetLocal(node.Type));
			}
			if (member is FieldInfo)
			{
				this._ilg.EmitFieldSet((FieldInfo)member);
			}
			else
			{
				PropertyInfo propertyInfo = (PropertyInfo)member;
				this.EmitCall(type, propertyInfo.GetSetMethod(true));
			}
			if (compilationFlags != LambdaCompiler.CompilationFlags.EmitAsVoidType)
			{
				this._ilg.Emit(OpCodes.Ldloc, localBuilder);
				this.FreeLocal(localBuilder);
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001D974 File Offset: 0x0001BB74
		private void EmitMemberExpression(Expression expr)
		{
			MemberExpression memberExpression = (MemberExpression)expr;
			Type type = null;
			if (memberExpression.Expression != null)
			{
				this.EmitInstance(memberExpression.Expression, out type);
			}
			this.EmitMemberGet(memberExpression.Member, type);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001D9B0 File Offset: 0x0001BBB0
		private void EmitMemberGet(MemberInfo member, Type objectType)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo == null)
			{
				PropertyInfo propertyInfo = (PropertyInfo)member;
				this.EmitCall(objectType, propertyInfo.GetGetMethod(true));
				return;
			}
			if (fieldInfo.IsLiteral)
			{
				this.EmitConstant(fieldInfo.GetRawConstantValue(), fieldInfo.FieldType);
				return;
			}
			this._ilg.EmitFieldGet(fieldInfo);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001DA04 File Offset: 0x0001BC04
		private void EmitInstance(Expression instance, out Type type)
		{
			type = instance.Type;
			if (type.IsByRef)
			{
				type = type.GetElementType();
				this.EmitExpression(instance);
				return;
			}
			if (type.IsValueType)
			{
				this.EmitAddress(instance, type);
				return;
			}
			this.EmitExpression(instance);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001DA44 File Offset: 0x0001BC44
		private void EmitNewArrayExpression(Expression expr)
		{
			NewArrayExpression newArrayExpression = (NewArrayExpression)expr;
			ReadOnlyCollection<Expression> expressions = newArrayExpression.Expressions;
			int count = expressions.Count;
			if (newArrayExpression.NodeType == ExpressionType.NewArrayInit)
			{
				Type elementType = newArrayExpression.Type.GetElementType();
				this._ilg.EmitArray(elementType, count);
				for (int i = 0; i < count; i++)
				{
					this._ilg.Emit(OpCodes.Dup);
					this._ilg.EmitPrimitive(i);
					this.EmitExpression(expressions[i]);
					this._ilg.EmitStoreElement(elementType);
				}
				return;
			}
			for (int j = 0; j < count; j++)
			{
				Expression expression = expressions[j];
				this.EmitExpression(expression);
				this._ilg.EmitConvertToType(expression.Type, typeof(int), true, this);
			}
			this._ilg.EmitArray(newArrayExpression.Type);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0000A01D File Offset: 0x0000821D
		private void EmitDebugInfoExpression(Expression expr)
		{
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001DB22 File Offset: 0x0001BD22
		private void EmitListInitExpression(Expression expr)
		{
			this.EmitListInit((ListInitExpression)expr);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001DB30 File Offset: 0x0001BD30
		private void EmitMemberInitExpression(Expression expr)
		{
			this.EmitMemberInit((MemberInitExpression)expr);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001DB40 File Offset: 0x0001BD40
		private void EmitBinding(MemberBinding binding, Type objectType)
		{
			switch (binding.BindingType)
			{
			case MemberBindingType.Assignment:
				this.EmitMemberAssignment((MemberAssignment)binding, objectType);
				return;
			case MemberBindingType.MemberBinding:
				this.EmitMemberMemberBinding((MemberMemberBinding)binding);
				return;
			case MemberBindingType.ListBinding:
				this.EmitMemberListBinding((MemberListBinding)binding);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001DB90 File Offset: 0x0001BD90
		private void EmitMemberAssignment(MemberAssignment binding, Type objectType)
		{
			this.EmitExpression(binding.Expression);
			FieldInfo fieldInfo = binding.Member as FieldInfo;
			if (fieldInfo != null)
			{
				this._ilg.Emit(OpCodes.Stfld, fieldInfo);
				return;
			}
			this.EmitCall(objectType, (binding.Member as PropertyInfo).GetSetMethod(true));
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001DBE4 File Offset: 0x0001BDE4
		private void EmitMemberMemberBinding(MemberMemberBinding binding)
		{
			Type memberType = LambdaCompiler.GetMemberType(binding.Member);
			if (binding.Member is PropertyInfo && memberType.IsValueType)
			{
				throw Error.CannotAutoInitializeValueTypeMemberThroughProperty(binding.Member);
			}
			if (memberType.IsValueType)
			{
				this.EmitMemberAddress(binding.Member, binding.Member.DeclaringType);
			}
			else
			{
				this.EmitMemberGet(binding.Member, binding.Member.DeclaringType);
			}
			this.EmitMemberInit(binding.Bindings, false, memberType);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001DC64 File Offset: 0x0001BE64
		private void EmitMemberListBinding(MemberListBinding binding)
		{
			Type memberType = LambdaCompiler.GetMemberType(binding.Member);
			if (binding.Member is PropertyInfo && memberType.IsValueType)
			{
				throw Error.CannotAutoInitializeValueTypeElementThroughProperty(binding.Member);
			}
			if (memberType.IsValueType)
			{
				this.EmitMemberAddress(binding.Member, binding.Member.DeclaringType);
			}
			else
			{
				this.EmitMemberGet(binding.Member, binding.Member.DeclaringType);
			}
			this.EmitListInit(binding.Initializers, false, memberType);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001DCE4 File Offset: 0x0001BEE4
		private void EmitMemberInit(MemberInitExpression init)
		{
			this.EmitExpression(init.NewExpression);
			LocalBuilder localBuilder = null;
			if (init.NewExpression.Type.IsValueType && init.Bindings.Count > 0)
			{
				localBuilder = this.GetLocal(init.NewExpression.Type);
				this._ilg.Emit(OpCodes.Stloc, localBuilder);
				this._ilg.Emit(OpCodes.Ldloca, localBuilder);
			}
			this.EmitMemberInit(init.Bindings, localBuilder == null, init.NewExpression.Type);
			if (localBuilder != null)
			{
				this._ilg.Emit(OpCodes.Ldloc, localBuilder);
				this.FreeLocal(localBuilder);
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0001DD8C File Offset: 0x0001BF8C
		private void EmitMemberInit(ReadOnlyCollection<MemberBinding> bindings, bool keepOnStack, Type objectType)
		{
			int count = bindings.Count;
			if (count == 0)
			{
				if (!keepOnStack)
				{
					this._ilg.Emit(OpCodes.Pop);
					return;
				}
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					if (keepOnStack || i < count - 1)
					{
						this._ilg.Emit(OpCodes.Dup);
					}
					this.EmitBinding(bindings[i], objectType);
				}
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001DDEC File Offset: 0x0001BFEC
		private void EmitListInit(ListInitExpression init)
		{
			this.EmitExpression(init.NewExpression);
			LocalBuilder localBuilder = null;
			if (init.NewExpression.Type.IsValueType)
			{
				localBuilder = this.GetLocal(init.NewExpression.Type);
				this._ilg.Emit(OpCodes.Stloc, localBuilder);
				this._ilg.Emit(OpCodes.Ldloca, localBuilder);
			}
			this.EmitListInit(init.Initializers, localBuilder == null, init.NewExpression.Type);
			if (localBuilder != null)
			{
				this._ilg.Emit(OpCodes.Ldloc, localBuilder);
				this.FreeLocal(localBuilder);
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001DE84 File Offset: 0x0001C084
		private void EmitListInit(ReadOnlyCollection<ElementInit> initializers, bool keepOnStack, Type objectType)
		{
			int count = initializers.Count;
			if (count == 0)
			{
				if (!keepOnStack)
				{
					this._ilg.Emit(OpCodes.Pop);
					return;
				}
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					if (keepOnStack || i < count - 1)
					{
						this._ilg.Emit(OpCodes.Dup);
					}
					this.EmitMethodCall(initializers[i].AddMethod, initializers[i], objectType);
					if (initializers[i].AddMethod.ReturnType != typeof(void))
					{
						this._ilg.Emit(OpCodes.Pop);
					}
				}
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001DF24 File Offset: 0x0001C124
		private static Type GetMemberType(MemberInfo member)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo == null)
			{
				return (member as PropertyInfo).PropertyType;
			}
			return fieldInfo.FieldType;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001DF50 File Offset: 0x0001C150
		private void EmitLift(ExpressionType nodeType, Type resultType, MethodCallExpression mc, ParameterExpression[] paramList, Expression[] argList)
		{
			switch (nodeType)
			{
			case ExpressionType.Equal:
				goto IL_02B5;
			case ExpressionType.ExclusiveOr:
			case ExpressionType.GreaterThan:
			case ExpressionType.GreaterThanOrEqual:
			case ExpressionType.Invoke:
			case ExpressionType.Lambda:
			case ExpressionType.LeftShift:
			case ExpressionType.LessThan:
			case ExpressionType.LessThanOrEqual:
				break;
			default:
				if (nodeType == ExpressionType.NotEqual)
				{
					goto IL_02B5;
				}
				break;
			}
			IL_0035:
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			LocalBuilder local = this.GetLocal(typeof(bool));
			int i = 0;
			int num = paramList.Length;
			while (i < num)
			{
				ParameterExpression parameterExpression = paramList[i];
				Expression expression = argList[i];
				if (expression.Type.IsNullableType())
				{
					this._scope.AddLocal(this, parameterExpression);
					this.EmitAddress(expression, expression.Type);
					this._ilg.Emit(OpCodes.Dup);
					this._ilg.EmitHasValue(expression.Type);
					this._ilg.Emit(OpCodes.Ldc_I4_0);
					this._ilg.Emit(OpCodes.Ceq);
					this._ilg.Emit(OpCodes.Stloc, local);
					this._ilg.EmitGetValueOrDefault(expression.Type);
					this._scope.EmitSet(parameterExpression);
				}
				else
				{
					this._scope.AddLocal(this, parameterExpression);
					this.EmitExpression(expression);
					if (!expression.Type.IsValueType)
					{
						this._ilg.Emit(OpCodes.Dup);
						this._ilg.Emit(OpCodes.Ldnull);
						this._ilg.Emit(OpCodes.Ceq);
						this._ilg.Emit(OpCodes.Stloc, local);
					}
					this._scope.EmitSet(parameterExpression);
				}
				this._ilg.Emit(OpCodes.Ldloc, local);
				this._ilg.Emit(OpCodes.Brtrue, label2);
				i++;
			}
			this.EmitMethodCallExpression(mc);
			if (resultType.IsNullableType() && !TypeUtils.AreEquivalent(resultType, mc.Type))
			{
				ConstructorInfo constructor = resultType.GetConstructor(new Type[] { mc.Type });
				this._ilg.Emit(OpCodes.Newobj, constructor);
			}
			this._ilg.Emit(OpCodes.Br_S, label);
			this._ilg.MarkLabel(label2);
			if (TypeUtils.AreEquivalent(resultType, mc.Type.GetNullableType()))
			{
				if (resultType.IsValueType)
				{
					LocalBuilder local2 = this.GetLocal(resultType);
					this._ilg.Emit(OpCodes.Ldloca, local2);
					this._ilg.Emit(OpCodes.Initobj, resultType);
					this._ilg.Emit(OpCodes.Ldloc, local2);
					this.FreeLocal(local2);
				}
				else
				{
					this._ilg.Emit(OpCodes.Ldnull);
				}
			}
			else
			{
				this._ilg.Emit(OpCodes.Ldc_I4_0);
			}
			this._ilg.MarkLabel(label);
			this.FreeLocal(local);
			return;
			IL_02B5:
			if (!TypeUtils.AreEquivalent(resultType, mc.Type.GetNullableType()))
			{
				Label label3 = this._ilg.DefineLabel();
				Label label4 = this._ilg.DefineLabel();
				Label label5 = this._ilg.DefineLabel();
				LocalBuilder local3 = this.GetLocal(typeof(bool));
				LocalBuilder local4 = this.GetLocal(typeof(bool));
				this._ilg.Emit(OpCodes.Ldc_I4_0);
				this._ilg.Emit(OpCodes.Stloc, local3);
				this._ilg.Emit(OpCodes.Ldc_I4_1);
				this._ilg.Emit(OpCodes.Stloc, local4);
				int j = 0;
				int num2 = paramList.Length;
				while (j < num2)
				{
					ParameterExpression parameterExpression2 = paramList[j];
					Expression expression2 = argList[j];
					this._scope.AddLocal(this, parameterExpression2);
					if (expression2.Type.IsNullableType())
					{
						this.EmitAddress(expression2, expression2.Type);
						this._ilg.Emit(OpCodes.Dup);
						this._ilg.EmitHasValue(expression2.Type);
						this._ilg.Emit(OpCodes.Ldc_I4_0);
						this._ilg.Emit(OpCodes.Ceq);
						this._ilg.Emit(OpCodes.Dup);
						this._ilg.Emit(OpCodes.Ldloc, local3);
						this._ilg.Emit(OpCodes.Or);
						this._ilg.Emit(OpCodes.Stloc, local3);
						this._ilg.Emit(OpCodes.Ldloc, local4);
						this._ilg.Emit(OpCodes.And);
						this._ilg.Emit(OpCodes.Stloc, local4);
						this._ilg.EmitGetValueOrDefault(expression2.Type);
					}
					else
					{
						this.EmitExpression(expression2);
						if (!expression2.Type.IsValueType)
						{
							this._ilg.Emit(OpCodes.Dup);
							this._ilg.Emit(OpCodes.Ldnull);
							this._ilg.Emit(OpCodes.Ceq);
							this._ilg.Emit(OpCodes.Dup);
							this._ilg.Emit(OpCodes.Ldloc, local3);
							this._ilg.Emit(OpCodes.Or);
							this._ilg.Emit(OpCodes.Stloc, local3);
							this._ilg.Emit(OpCodes.Ldloc, local4);
							this._ilg.Emit(OpCodes.And);
							this._ilg.Emit(OpCodes.Stloc, local4);
						}
						else
						{
							this._ilg.Emit(OpCodes.Ldc_I4_0);
							this._ilg.Emit(OpCodes.Stloc, local4);
						}
					}
					this._scope.EmitSet(parameterExpression2);
					j++;
				}
				this._ilg.Emit(OpCodes.Ldloc, local4);
				this._ilg.Emit(OpCodes.Brtrue, label4);
				this._ilg.Emit(OpCodes.Ldloc, local3);
				this._ilg.Emit(OpCodes.Brtrue, label5);
				this.EmitMethodCallExpression(mc);
				if (resultType.IsNullableType() && !TypeUtils.AreEquivalent(resultType, mc.Type))
				{
					ConstructorInfo constructor2 = resultType.GetConstructor(new Type[] { mc.Type });
					this._ilg.Emit(OpCodes.Newobj, constructor2);
				}
				this._ilg.Emit(OpCodes.Br_S, label3);
				this._ilg.MarkLabel(label4);
				this._ilg.EmitPrimitive(nodeType == ExpressionType.Equal);
				this._ilg.Emit(OpCodes.Br_S, label3);
				this._ilg.MarkLabel(label5);
				this._ilg.EmitPrimitive(nodeType == ExpressionType.NotEqual);
				this._ilg.MarkLabel(label3);
				this.FreeLocal(local3);
				this.FreeLocal(local4);
				return;
			}
			goto IL_0035;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001E5DC File Offset: 0x0001C7DC
		private void EmitExpression(Expression node, LambdaCompiler.CompilationFlags flags)
		{
			if (!this._guard.TryEnterOnCurrentStack())
			{
				this._guard.RunOnEmptyStack<LambdaCompiler, Expression, LambdaCompiler.CompilationFlags>(delegate(LambdaCompiler @this, Expression n, LambdaCompiler.CompilationFlags f)
				{
					@this.EmitExpression(n, f);
				}, this, node, flags);
				return;
			}
			bool flag = (flags & LambdaCompiler.CompilationFlags.EmitExpressionStartMask) == LambdaCompiler.CompilationFlags.EmitExpressionStart;
			LambdaCompiler.CompilationFlags compilationFlags = (flag ? this.EmitExpressionStart(node) : LambdaCompiler.CompilationFlags.EmitNoExpressionStart);
			flags &= LambdaCompiler.CompilationFlags.EmitAsTailCallMask;
			switch (node.NodeType)
			{
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
			case ExpressionType.And:
			case ExpressionType.ArrayIndex:
			case ExpressionType.Divide:
			case ExpressionType.Equal:
			case ExpressionType.ExclusiveOr:
			case ExpressionType.GreaterThan:
			case ExpressionType.GreaterThanOrEqual:
			case ExpressionType.LeftShift:
			case ExpressionType.LessThan:
			case ExpressionType.LessThanOrEqual:
			case ExpressionType.Modulo:
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
			case ExpressionType.NotEqual:
			case ExpressionType.Or:
			case ExpressionType.Power:
			case ExpressionType.RightShift:
			case ExpressionType.Subtract:
			case ExpressionType.SubtractChecked:
				this.EmitBinaryExpression(node, flags);
				break;
			case ExpressionType.AndAlso:
				this.EmitAndAlsoBinaryExpression(node, flags);
				break;
			case ExpressionType.ArrayLength:
			case ExpressionType.Negate:
			case ExpressionType.UnaryPlus:
			case ExpressionType.NegateChecked:
			case ExpressionType.Not:
			case ExpressionType.TypeAs:
			case ExpressionType.Decrement:
			case ExpressionType.Increment:
			case ExpressionType.OnesComplement:
			case ExpressionType.IsTrue:
			case ExpressionType.IsFalse:
				this.EmitUnaryExpression(node, flags);
				break;
			case ExpressionType.Call:
				this.EmitMethodCallExpression(node, flags);
				break;
			case ExpressionType.Coalesce:
				this.EmitCoalesceBinaryExpression(node);
				break;
			case ExpressionType.Conditional:
				this.EmitConditionalExpression(node, flags);
				break;
			case ExpressionType.Constant:
				this.EmitConstantExpression(node);
				break;
			case ExpressionType.Convert:
			case ExpressionType.ConvertChecked:
				this.EmitConvertUnaryExpression(node, flags);
				break;
			case ExpressionType.Invoke:
				this.EmitInvocationExpression(node, flags);
				break;
			case ExpressionType.Lambda:
				this.EmitLambdaExpression(node);
				break;
			case ExpressionType.ListInit:
				this.EmitListInitExpression(node);
				break;
			case ExpressionType.MemberAccess:
				this.EmitMemberExpression(node);
				break;
			case ExpressionType.MemberInit:
				this.EmitMemberInitExpression(node);
				break;
			case ExpressionType.New:
				this.EmitNewExpression(node);
				break;
			case ExpressionType.NewArrayInit:
			case ExpressionType.NewArrayBounds:
				this.EmitNewArrayExpression(node);
				break;
			case ExpressionType.OrElse:
				this.EmitOrElseBinaryExpression(node, flags);
				break;
			case ExpressionType.Parameter:
				this.EmitParameterExpression(node);
				break;
			case ExpressionType.Quote:
				this.EmitQuoteUnaryExpression(node);
				break;
			case ExpressionType.TypeIs:
			case ExpressionType.TypeEqual:
				this.EmitTypeBinaryExpression(node);
				break;
			case ExpressionType.Assign:
				this.EmitAssignBinaryExpression(node);
				break;
			case ExpressionType.Block:
				this.EmitBlockExpression(node, flags);
				break;
			case ExpressionType.DebugInfo:
				this.EmitDebugInfoExpression(node);
				break;
			case ExpressionType.Dynamic:
				this.EmitDynamicExpression(node);
				break;
			case ExpressionType.Default:
				this.EmitDefaultExpression(node);
				break;
			case ExpressionType.Goto:
				this.EmitGotoExpression(node, flags);
				break;
			case ExpressionType.Index:
				this.EmitIndexExpression(node);
				break;
			case ExpressionType.Label:
				this.EmitLabelExpression(node, flags);
				break;
			case ExpressionType.RuntimeVariables:
				this.EmitRuntimeVariablesExpression(node);
				break;
			case ExpressionType.Loop:
				this.EmitLoopExpression(node);
				break;
			case ExpressionType.Switch:
				this.EmitSwitchExpression(node, flags);
				break;
			case ExpressionType.Throw:
				this.EmitThrowUnaryExpression(node);
				break;
			case ExpressionType.Try:
				this.EmitTryExpression(node);
				break;
			case ExpressionType.Unbox:
				this.EmitUnboxUnaryExpression(node);
				break;
			}
			if (flag)
			{
				this.EmitExpressionEnd(compilationFlags);
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001E91F File Offset: 0x0001CB1F
		private static bool IsChecked(ExpressionType op)
		{
			if (op <= ExpressionType.MultiplyChecked)
			{
				if (op != ExpressionType.AddChecked && op != ExpressionType.ConvertChecked && op != ExpressionType.MultiplyChecked)
				{
					return false;
				}
			}
			else if (op != ExpressionType.NegateChecked && op != ExpressionType.SubtractChecked && op - ExpressionType.AddAssignChecked > 2)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001E94C File Offset: 0x0001CB4C
		internal void EmitConstantArray<T>(T[] array)
		{
			if (this._method is DynamicMethod)
			{
				this.EmitConstant(array, typeof(T[]));
				return;
			}
			if (this._typeBuilder != null)
			{
				FieldBuilder fieldBuilder = this.CreateStaticField("ConstantArray", typeof(T[]));
				Label label = this._ilg.DefineLabel();
				this._ilg.Emit(OpCodes.Ldsfld, fieldBuilder);
				this._ilg.Emit(OpCodes.Ldnull);
				this._ilg.Emit(OpCodes.Bne_Un, label);
				this._ilg.EmitArray(array, this);
				this._ilg.Emit(OpCodes.Stsfld, fieldBuilder);
				this._ilg.MarkLabel(label);
				this._ilg.Emit(OpCodes.Ldsfld, fieldBuilder);
				return;
			}
			this._ilg.EmitArray(array, this);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001EA28 File Offset: 0x0001CC28
		private void EmitClosureCreation(LambdaCompiler inner)
		{
			bool needsClosure = inner._scope.NeedsClosure;
			bool flag = inner._boundConstants.Count > 0;
			if (!needsClosure && !flag)
			{
				this._ilg.EmitNull();
				return;
			}
			if (flag)
			{
				this._boundConstants.EmitConstant(this, inner._boundConstants.ToArray(), typeof(object[]));
			}
			else
			{
				this._ilg.EmitNull();
			}
			if (needsClosure)
			{
				this._scope.EmitGet(this._scope.NearestHoistedLocals.SelfVariable);
			}
			else
			{
				this._ilg.EmitNull();
			}
			this._ilg.EmitNew(CachedReflectionInfo.Closure_ObjectArray_ObjectArray);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001EAD0 File Offset: 0x0001CCD0
		private void EmitDelegateConstruction(LambdaCompiler inner)
		{
			Type type = inner._lambda.Type;
			DynamicMethod dynamicMethod = inner._method as DynamicMethod;
			if (dynamicMethod != null)
			{
				this._boundConstants.EmitConstant(this, dynamicMethod, typeof(MethodInfo));
				this._ilg.EmitType(type);
				this.EmitClosureCreation(inner);
				this._ilg.Emit(OpCodes.Callvirt, CachedReflectionInfo.MethodInfo_CreateDelegate_Type_Object);
				this._ilg.Emit(OpCodes.Castclass, type);
				return;
			}
			this.EmitClosureCreation(inner);
			this._ilg.Emit(OpCodes.Ldftn, inner._method);
			this._ilg.Emit(OpCodes.Newobj, (ConstructorInfo)type.GetMember(".ctor")[0]);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001EB90 File Offset: 0x0001CD90
		private void EmitDelegateConstruction(LambdaExpression lambda)
		{
			LambdaCompiler lambdaCompiler;
			if (this._method is DynamicMethod)
			{
				lambdaCompiler = new LambdaCompiler(this._tree, lambda);
			}
			else
			{
				string text = (string.IsNullOrEmpty(lambda.Name) ? LambdaCompiler.GetUniqueMethodName() : lambda.Name);
				MethodBuilder methodBuilder = this._typeBuilder.DefineMethod(text, MethodAttributes.Private | MethodAttributes.Static);
				lambdaCompiler = new LambdaCompiler(this._tree, lambda, methodBuilder);
			}
			lambdaCompiler.EmitLambdaBody(this._scope, false, LambdaCompiler.CompilationFlags.EmitAsNoTail);
			this.EmitDelegateConstruction(lambdaCompiler);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001EC0C File Offset: 0x0001CE0C
		private static Type[] GetParameterTypes(LambdaExpression lambda, Type firstType)
		{
			int parameterCount = lambda.ParameterCount;
			Type[] array;
			int num;
			if (firstType != null)
			{
				array = new Type[parameterCount + 1];
				array[0] = firstType;
				num = 1;
			}
			else
			{
				array = new Type[parameterCount];
				num = 0;
			}
			int i = 0;
			while (i < parameterCount)
			{
				ParameterExpression parameter = lambda.GetParameter(i);
				array[num] = (parameter.IsByRef ? parameter.Type.MakeByRefType() : parameter.Type);
				i++;
				num++;
			}
			return array;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001EC80 File Offset: 0x0001CE80
		private static string GetUniqueMethodName()
		{
			return "<ExpressionCompilerImplementationDetails>{" + Interlocked.Increment(ref LambdaCompiler.s_counter).ToString() + "}lambda_method";
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001ECB0 File Offset: 0x0001CEB0
		private void EmitLambdaBody()
		{
			LambdaCompiler.CompilationFlags compilationFlags = (this._lambda.TailCall ? LambdaCompiler.CompilationFlags.EmitAsTail : LambdaCompiler.CompilationFlags.EmitAsNoTail);
			this.EmitLambdaBody(null, false, compilationFlags);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001ECE0 File Offset: 0x0001CEE0
		private void EmitLambdaBody(CompilerScope parent, bool inlined, LambdaCompiler.CompilationFlags flags)
		{
			this._scope.Enter(this, parent);
			if (inlined)
			{
				for (int i = this._lambda.ParameterCount - 1; i >= 0; i--)
				{
					this._scope.EmitSet(this._lambda.GetParameter(i));
				}
			}
			flags = LambdaCompiler.UpdateEmitExpressionStartFlag(flags, LambdaCompiler.CompilationFlags.EmitExpressionStart);
			if (this._lambda.ReturnType == typeof(void))
			{
				this.EmitExpressionAsVoid(this._lambda.Body, flags);
			}
			else
			{
				this.EmitExpression(this._lambda.Body, flags);
			}
			if (!inlined)
			{
				this._ilg.Emit(OpCodes.Ret);
			}
			this._scope.Exit();
			foreach (LabelInfo labelInfo in this._labelInfo.Values)
			{
				labelInfo.ValidateFinish();
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001EDE0 File Offset: 0x0001CFE0
		private void EmitConditionalExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			ConditionalExpression conditionalExpression = (ConditionalExpression)expr;
			Label label = this._ilg.DefineLabel();
			this.EmitExpressionAndBranch(false, conditionalExpression.Test, label);
			this.EmitExpressionAsType(conditionalExpression.IfTrue, conditionalExpression.Type, flags);
			if (LambdaCompiler.NotEmpty(conditionalExpression.IfFalse))
			{
				Label label2 = this._ilg.DefineLabel();
				if ((flags & LambdaCompiler.CompilationFlags.EmitAsTailCallMask) == LambdaCompiler.CompilationFlags.EmitAsTail)
				{
					this._ilg.Emit(OpCodes.Ret);
				}
				else
				{
					this._ilg.Emit(OpCodes.Br, label2);
				}
				this._ilg.MarkLabel(label);
				this.EmitExpressionAsType(conditionalExpression.IfFalse, conditionalExpression.Type, flags);
				this._ilg.MarkLabel(label2);
				return;
			}
			this._ilg.MarkLabel(label);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001EEA4 File Offset: 0x0001D0A4
		private static bool NotEmpty(Expression node)
		{
			DefaultExpression defaultExpression = node as DefaultExpression;
			return defaultExpression == null || defaultExpression.Type != typeof(void);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001EED8 File Offset: 0x0001D0D8
		private static bool Significant(Expression node)
		{
			BlockExpression blockExpression = node as BlockExpression;
			if (blockExpression != null)
			{
				for (int i = 0; i < blockExpression.ExpressionCount; i++)
				{
					if (LambdaCompiler.Significant(blockExpression.GetExpression(i)))
					{
						return true;
					}
				}
				return false;
			}
			return LambdaCompiler.NotEmpty(node) && !(node is DebugInfoExpression);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001EF28 File Offset: 0x0001D128
		private void EmitCoalesceBinaryExpression(Expression expr)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			if (binaryExpression.Left.Type.IsNullableType())
			{
				this.EmitNullableCoalesce(binaryExpression);
				return;
			}
			if (binaryExpression.Conversion != null)
			{
				this.EmitLambdaReferenceCoalesce(binaryExpression);
				return;
			}
			this.EmitReferenceCoalesceWithoutConversion(binaryExpression);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001EF70 File Offset: 0x0001D170
		private void EmitNullableCoalesce(BinaryExpression b)
		{
			LocalBuilder local = this.GetLocal(b.Left.Type);
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			this.EmitExpression(b.Left);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitHasValue(b.Left.Type);
			this._ilg.Emit(OpCodes.Brfalse, label);
			Type nonNullableType = b.Left.Type.GetNonNullableType();
			if (b.Conversion != null)
			{
				Expression parameter = b.Conversion.GetParameter(0);
				this.EmitLambdaExpression(b.Conversion);
				if (!parameter.Type.IsAssignableFrom(b.Left.Type))
				{
					this._ilg.Emit(OpCodes.Ldloca, local);
					this._ilg.EmitGetValueOrDefault(b.Left.Type);
				}
				else
				{
					this._ilg.Emit(OpCodes.Ldloc, local);
				}
				this._ilg.Emit(OpCodes.Callvirt, b.Conversion.Type.GetInvokeMethod());
			}
			else if (TypeUtils.AreEquivalent(b.Type, b.Left.Type))
			{
				this._ilg.Emit(OpCodes.Ldloc, local);
			}
			else
			{
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.EmitGetValueOrDefault(b.Left.Type);
				if (!TypeUtils.AreEquivalent(b.Type, nonNullableType))
				{
					this._ilg.EmitConvertToType(nonNullableType, b.Type, true, this);
				}
			}
			this.FreeLocal(local);
			this._ilg.Emit(OpCodes.Br, label2);
			this._ilg.MarkLabel(label);
			this.EmitExpression(b.Right);
			if (!TypeUtils.AreEquivalent(b.Right.Type, b.Type))
			{
				this._ilg.EmitConvertToType(b.Right.Type, b.Type, true, this);
			}
			this._ilg.MarkLabel(label2);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001F18C File Offset: 0x0001D38C
		private void EmitLambdaReferenceCoalesce(BinaryExpression b)
		{
			LocalBuilder local = this.GetLocal(b.Left.Type);
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			this.EmitExpression(b.Left);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Brtrue, label2);
			this.EmitExpression(b.Right);
			this._ilg.Emit(OpCodes.Br, label);
			this._ilg.MarkLabel(label2);
			this.EmitLambdaExpression(b.Conversion);
			this._ilg.Emit(OpCodes.Ldloc, local);
			this.FreeLocal(local);
			this._ilg.Emit(OpCodes.Callvirt, b.Conversion.Type.GetInvokeMethod());
			this._ilg.MarkLabel(label);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001F27C File Offset: 0x0001D47C
		private void EmitReferenceCoalesceWithoutConversion(BinaryExpression b)
		{
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			this.EmitExpression(b.Left);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Brtrue, label2);
			this._ilg.Emit(OpCodes.Pop);
			this.EmitExpression(b.Right);
			if (!TypeUtils.AreEquivalent(b.Right.Type, b.Type))
			{
				if (b.Right.Type.IsValueType)
				{
					this._ilg.Emit(OpCodes.Box, b.Right.Type);
				}
				this._ilg.Emit(OpCodes.Castclass, b.Type);
			}
			this._ilg.Emit(OpCodes.Br_S, label);
			this._ilg.MarkLabel(label2);
			if (!TypeUtils.AreEquivalent(b.Left.Type, b.Type))
			{
				this._ilg.Emit(OpCodes.Castclass, b.Type);
			}
			this._ilg.MarkLabel(label);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001F39C File Offset: 0x0001D59C
		private void EmitLiftedAndAlso(BinaryExpression b)
		{
			Type typeFromHandle = typeof(bool?);
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			Label label3 = this._ilg.DefineLabel();
			this.EmitExpression(b.Left);
			LocalBuilder local = this.GetLocal(typeFromHandle);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitHasValue(typeFromHandle);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Not);
			this._ilg.Emit(OpCodes.And);
			this._ilg.Emit(OpCodes.Brtrue, label);
			this.EmitExpression(b.Right);
			LocalBuilder local2 = this.GetLocal(typeFromHandle);
			this._ilg.Emit(OpCodes.Stloc, local2);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Brtrue_S, label2);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Brtrue_S, label);
			this._ilg.MarkLabel(label2);
			this._ilg.Emit(OpCodes.Ldloc, local2);
			this.FreeLocal(local2);
			this._ilg.Emit(OpCodes.Br_S, label3);
			this._ilg.MarkLabel(label);
			this._ilg.Emit(OpCodes.Ldloc, local);
			this.FreeLocal(local);
			this._ilg.MarkLabel(label3);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001F55C File Offset: 0x0001D75C
		private void EmitMethodAndAlso(BinaryExpression b, LambdaCompiler.CompilationFlags flags)
		{
			Label label = this._ilg.DefineLabel();
			this.EmitExpression(b.Left);
			this._ilg.Emit(OpCodes.Dup);
			MethodInfo booleanOperator = TypeUtils.GetBooleanOperator(b.Method.DeclaringType, "op_False");
			this._ilg.Emit(OpCodes.Call, booleanOperator);
			this._ilg.Emit(OpCodes.Brtrue, label);
			this.EmitExpression(b.Right);
			if ((flags & LambdaCompiler.CompilationFlags.EmitAsTailCallMask) == LambdaCompiler.CompilationFlags.EmitAsTail)
			{
				this._ilg.Emit(OpCodes.Tailcall);
			}
			this._ilg.Emit(OpCodes.Call, b.Method);
			this._ilg.MarkLabel(label);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001F618 File Offset: 0x0001D818
		private void EmitUnliftedAndAlso(BinaryExpression b)
		{
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			this.EmitExpressionAndBranch(false, b.Left, label);
			this.EmitExpression(b.Right);
			this._ilg.Emit(OpCodes.Br, label2);
			this._ilg.MarkLabel(label);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.MarkLabel(label2);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001F690 File Offset: 0x0001D890
		private void EmitAndAlsoBinaryExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			if (binaryExpression.Method != null)
			{
				if (binaryExpression.IsLiftedLogical)
				{
					this.EmitExpression(binaryExpression.ReduceUserdefinedLifted());
					return;
				}
				this.EmitMethodAndAlso(binaryExpression, flags);
				return;
			}
			else
			{
				if (binaryExpression.Left.Type == typeof(bool?))
				{
					this.EmitLiftedAndAlso(binaryExpression);
					return;
				}
				this.EmitUnliftedAndAlso(binaryExpression);
				return;
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001F6FC File Offset: 0x0001D8FC
		private void EmitLiftedOrElse(BinaryExpression b)
		{
			Type typeFromHandle = typeof(bool?);
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			LocalBuilder local = this.GetLocal(typeFromHandle);
			this.EmitExpression(b.Left);
			this._ilg.Emit(OpCodes.Stloc, local);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Brtrue, label);
			this.EmitExpression(b.Right);
			LocalBuilder local2 = this.GetLocal(typeFromHandle);
			this._ilg.Emit(OpCodes.Stloc, local2);
			this._ilg.Emit(OpCodes.Ldloca, local2);
			this._ilg.EmitGetValueOrDefault(typeFromHandle);
			this._ilg.Emit(OpCodes.Ldloca, local);
			this._ilg.EmitHasValue(typeFromHandle);
			this._ilg.Emit(OpCodes.Or);
			this._ilg.Emit(OpCodes.Brfalse_S, label);
			this._ilg.Emit(OpCodes.Ldloc, local2);
			this.FreeLocal(local2);
			this._ilg.Emit(OpCodes.Br_S, label2);
			this._ilg.MarkLabel(label);
			this._ilg.Emit(OpCodes.Ldloc, local);
			this.FreeLocal(local);
			this._ilg.MarkLabel(label2);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001F860 File Offset: 0x0001DA60
		private void EmitUnliftedOrElse(BinaryExpression b)
		{
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			this.EmitExpressionAndBranch(false, b.Left, label);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Br, label2);
			this._ilg.MarkLabel(label);
			this.EmitExpression(b.Right);
			this._ilg.MarkLabel(label2);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001F8D8 File Offset: 0x0001DAD8
		private void EmitMethodOrElse(BinaryExpression b, LambdaCompiler.CompilationFlags flags)
		{
			Label label = this._ilg.DefineLabel();
			this.EmitExpression(b.Left);
			this._ilg.Emit(OpCodes.Dup);
			MethodInfo booleanOperator = TypeUtils.GetBooleanOperator(b.Method.DeclaringType, "op_True");
			this._ilg.Emit(OpCodes.Call, booleanOperator);
			this._ilg.Emit(OpCodes.Brtrue, label);
			this.EmitExpression(b.Right);
			if ((flags & LambdaCompiler.CompilationFlags.EmitAsTailCallMask) == LambdaCompiler.CompilationFlags.EmitAsTail)
			{
				this._ilg.Emit(OpCodes.Tailcall);
			}
			this._ilg.Emit(OpCodes.Call, b.Method);
			this._ilg.MarkLabel(label);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001F994 File Offset: 0x0001DB94
		private void EmitOrElseBinaryExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			if (binaryExpression.Method != null)
			{
				if (binaryExpression.IsLiftedLogical)
				{
					this.EmitExpression(binaryExpression.ReduceUserdefinedLifted());
					return;
				}
				this.EmitMethodOrElse(binaryExpression, flags);
				return;
			}
			else
			{
				if (binaryExpression.Left.Type == typeof(bool?))
				{
					this.EmitLiftedOrElse(binaryExpression);
					return;
				}
				this.EmitUnliftedOrElse(binaryExpression);
				return;
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001FA00 File Offset: 0x0001DC00
		private void EmitExpressionAndBranch(bool branchValue, Expression node, Label label)
		{
			LambdaCompiler.CompilationFlags compilationFlags = this.EmitExpressionStart(node);
			ExpressionType nodeType = node.NodeType;
			if (nodeType <= ExpressionType.Equal)
			{
				if (nodeType != ExpressionType.AndAlso)
				{
					if (nodeType != ExpressionType.Equal)
					{
						goto IL_007F;
					}
					goto IL_006F;
				}
			}
			else
			{
				switch (nodeType)
				{
				case ExpressionType.Not:
					this.EmitBranchNot(branchValue, (UnaryExpression)node, label);
					goto IL_0093;
				case ExpressionType.NotEqual:
					goto IL_006F;
				case ExpressionType.Or:
					goto IL_007F;
				case ExpressionType.OrElse:
					break;
				default:
					if (nodeType != ExpressionType.Block)
					{
						goto IL_007F;
					}
					this.EmitBranchBlock(branchValue, (BlockExpression)node, label);
					goto IL_0093;
				}
			}
			this.EmitBranchLogical(branchValue, (BinaryExpression)node, label);
			goto IL_0093;
			IL_006F:
			this.EmitBranchComparison(branchValue, (BinaryExpression)node, label);
			goto IL_0093;
			IL_007F:
			this.EmitExpression(node, LambdaCompiler.CompilationFlags.EmitNoExpressionStart | LambdaCompiler.CompilationFlags.EmitAsNoTail);
			this.EmitBranchOp(branchValue, label);
			IL_0093:
			this.EmitExpressionEnd(compilationFlags);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001FAA7 File Offset: 0x0001DCA7
		private void EmitBranchOp(bool branch, Label label)
		{
			this._ilg.Emit(branch ? OpCodes.Brtrue : OpCodes.Brfalse, label);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001FAC4 File Offset: 0x0001DCC4
		private void EmitBranchNot(bool branch, UnaryExpression node, Label label)
		{
			if (node.Method != null)
			{
				this.EmitExpression(node, LambdaCompiler.CompilationFlags.EmitNoExpressionStart | LambdaCompiler.CompilationFlags.EmitAsNoTail);
				this.EmitBranchOp(branch, label);
				return;
			}
			this.EmitExpressionAndBranch(!branch, node.Operand, label);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001FAFC File Offset: 0x0001DCFC
		private void EmitBranchComparison(bool branch, BinaryExpression node, Label label)
		{
			bool flag = branch == (node.NodeType == ExpressionType.Equal);
			if (node.Method != null)
			{
				this.EmitBinaryMethod(node, LambdaCompiler.CompilationFlags.EmitAsNoTail);
				this.EmitBranchOp(branch, label);
				return;
			}
			if (ConstantCheck.IsNull(node.Left))
			{
				if (node.Right.Type.IsNullableType())
				{
					this.EmitAddress(node.Right, node.Right.Type);
					this._ilg.EmitHasValue(node.Right.Type);
				}
				else
				{
					this.EmitExpression(LambdaCompiler.GetEqualityOperand(node.Right));
				}
				this.EmitBranchOp(!flag, label);
				return;
			}
			if (ConstantCheck.IsNull(node.Right))
			{
				if (node.Left.Type.IsNullableType())
				{
					this.EmitAddress(node.Left, node.Left.Type);
					this._ilg.EmitHasValue(node.Left.Type);
				}
				else
				{
					this.EmitExpression(LambdaCompiler.GetEqualityOperand(node.Left));
				}
				this.EmitBranchOp(!flag, label);
				return;
			}
			if (node.Left.Type.IsNullableType() || node.Right.Type.IsNullableType())
			{
				this.EmitBinaryExpression(node);
				this.EmitBranchOp(branch, label);
				return;
			}
			this.EmitExpression(LambdaCompiler.GetEqualityOperand(node.Left));
			this.EmitExpression(LambdaCompiler.GetEqualityOperand(node.Right));
			this._ilg.Emit(flag ? OpCodes.Beq : OpCodes.Bne_Un, label);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001FC84 File Offset: 0x0001DE84
		private static Expression GetEqualityOperand(Expression expression)
		{
			if (expression.NodeType == ExpressionType.Convert)
			{
				UnaryExpression unaryExpression = (UnaryExpression)expression;
				if (TypeUtils.AreReferenceAssignable(unaryExpression.Type, unaryExpression.Operand.Type))
				{
					return unaryExpression.Operand;
				}
			}
			return expression;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001FCC4 File Offset: 0x0001DEC4
		private void EmitBranchLogical(bool branch, BinaryExpression node, Label label)
		{
			if (node.Method != null || node.IsLifted)
			{
				this.EmitExpression(node);
				this.EmitBranchOp(branch, label);
				return;
			}
			bool flag = node.NodeType == ExpressionType.AndAlso;
			if (branch == flag)
			{
				this.EmitBranchAnd(branch, node, label);
				return;
			}
			this.EmitBranchOr(branch, node, label);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001FD18 File Offset: 0x0001DF18
		private void EmitBranchAnd(bool branch, BinaryExpression node, Label label)
		{
			Label label2 = this._ilg.DefineLabel();
			this.EmitExpressionAndBranch(!branch, node.Left, label2);
			this.EmitExpressionAndBranch(branch, node.Right, label);
			this._ilg.MarkLabel(label2);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001FD5C File Offset: 0x0001DF5C
		private void EmitBranchOr(bool branch, BinaryExpression node, Label label)
		{
			this.EmitExpressionAndBranch(branch, node.Left, label);
			this.EmitExpressionAndBranch(branch, node.Right, label);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001FD7C File Offset: 0x0001DF7C
		private void EmitBranchBlock(bool branch, BlockExpression node, Label label)
		{
			this.EnterScope(node);
			int expressionCount = node.ExpressionCount;
			for (int i = 0; i < expressionCount - 1; i++)
			{
				this.EmitExpressionAsVoid(node.GetExpression(i));
			}
			this.EmitExpressionAndBranch(branch, node.GetExpression(expressionCount - 1), label);
			this.ExitScope(node);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001FDCA File Offset: 0x0001DFCA
		private void EmitBlockExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			this.Emit((BlockExpression)expr, LambdaCompiler.UpdateEmitAsTypeFlag(flags, LambdaCompiler.CompilationFlags.EmitAsDefaultType));
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001FDE0 File Offset: 0x0001DFE0
		private void Emit(BlockExpression node, LambdaCompiler.CompilationFlags flags)
		{
			int expressionCount = node.ExpressionCount;
			if (expressionCount == 0)
			{
				return;
			}
			this.EnterScope(node);
			LambdaCompiler.CompilationFlags compilationFlags = flags & LambdaCompiler.CompilationFlags.EmitAsTypeMask;
			LambdaCompiler.CompilationFlags compilationFlags2 = flags & LambdaCompiler.CompilationFlags.EmitAsTailCallMask;
			for (int i = 0; i < expressionCount - 1; i++)
			{
				Expression expression = node.GetExpression(i);
				Expression expression2 = node.GetExpression(i + 1);
				LambdaCompiler.CompilationFlags compilationFlags3;
				if (compilationFlags2 != LambdaCompiler.CompilationFlags.EmitAsNoTail)
				{
					GotoExpression gotoExpression = expression2 as GotoExpression;
					if (gotoExpression != null && (gotoExpression.Value == null || !LambdaCompiler.Significant(gotoExpression.Value)) && this.ReferenceLabel(gotoExpression.Target).CanReturn)
					{
						compilationFlags3 = LambdaCompiler.CompilationFlags.EmitAsTail;
					}
					else
					{
						compilationFlags3 = LambdaCompiler.CompilationFlags.EmitAsMiddle;
					}
				}
				else
				{
					compilationFlags3 = LambdaCompiler.CompilationFlags.EmitAsNoTail;
				}
				flags = LambdaCompiler.UpdateEmitAsTailCallFlag(flags, compilationFlags3);
				this.EmitExpressionAsVoid(expression, flags);
			}
			if (compilationFlags == LambdaCompiler.CompilationFlags.EmitAsVoidType || node.Type == typeof(void))
			{
				this.EmitExpressionAsVoid(node.GetExpression(expressionCount - 1), compilationFlags2);
			}
			else
			{
				this.EmitExpressionAsType(node.GetExpression(expressionCount - 1), node.Type, compilationFlags2);
			}
			this.ExitScope(node);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001FEF0 File Offset: 0x0001E0F0
		private void EnterScope(object node)
		{
			if (LambdaCompiler.HasVariables(node) && (this._scope.MergedScopes == null || !this._scope.MergedScopes.Contains(node)))
			{
				CompilerScope compilerScope;
				if (!this._tree.Scopes.TryGetValue(node, out compilerScope))
				{
					compilerScope = new CompilerScope(node, false)
					{
						NeedsClosure = this._scope.NeedsClosure
					};
				}
				this._scope = compilerScope.Enter(this, this._scope);
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001FF68 File Offset: 0x0001E168
		private static bool HasVariables(object node)
		{
			BlockExpression blockExpression = node as BlockExpression;
			if (blockExpression != null)
			{
				return blockExpression.Variables.Count > 0;
			}
			return ((CatchBlock)node).Variable != null;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001FF9C File Offset: 0x0001E19C
		private void ExitScope(object node)
		{
			if (this._scope.Node == node)
			{
				this._scope = this._scope.Exit();
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001FFC0 File Offset: 0x0001E1C0
		private void EmitDefaultExpression(Expression expr)
		{
			DefaultExpression defaultExpression = (DefaultExpression)expr;
			if (defaultExpression.Type != typeof(void))
			{
				this._ilg.EmitDefault(defaultExpression.Type, this);
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00020000 File Offset: 0x0001E200
		private void EmitLoopExpression(Expression expr)
		{
			LoopExpression loopExpression = (LoopExpression)expr;
			this.PushLabelBlock(LabelScopeKind.Statement);
			LabelInfo labelInfo = this.DefineLabel(loopExpression.BreakLabel);
			LabelInfo labelInfo2 = this.DefineLabel(loopExpression.ContinueLabel);
			labelInfo2.MarkWithEmptyStack();
			this.EmitExpressionAsVoid(loopExpression.Body);
			this._ilg.Emit(OpCodes.Br, labelInfo2.Label);
			this.PopLabelBlock(LabelScopeKind.Statement);
			labelInfo.MarkWithEmptyStack();
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00020068 File Offset: 0x0001E268
		private void EmitSwitchExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			SwitchExpression switchExpression = (SwitchExpression)expr;
			if (switchExpression.Cases.Count == 0)
			{
				this.EmitExpressionAsVoid(switchExpression.SwitchValue);
				if (switchExpression.DefaultBody != null)
				{
					this.EmitExpressionAsType(switchExpression.DefaultBody, switchExpression.Type, flags);
				}
				return;
			}
			if (this.TryEmitSwitchInstruction(switchExpression, flags))
			{
				return;
			}
			if (this.TryEmitHashtableSwitch(switchExpression, flags))
			{
				return;
			}
			ParameterExpression parameterExpression = Expression.Parameter(switchExpression.SwitchValue.Type, "switchValue");
			ParameterExpression parameterExpression2 = Expression.Parameter(LambdaCompiler.GetTestValueType(switchExpression), "testValue");
			this._scope.AddLocal(this, parameterExpression);
			this._scope.AddLocal(this, parameterExpression2);
			this.EmitExpression(switchExpression.SwitchValue);
			this._scope.EmitSet(parameterExpression);
			Label[] array = new Label[switchExpression.Cases.Count];
			bool[] array2 = new bool[switchExpression.Cases.Count];
			int i = 0;
			int count = switchExpression.Cases.Count;
			while (i < count)
			{
				this.DefineSwitchCaseLabel(switchExpression.Cases[i], out array[i], out array2[i]);
				foreach (Expression expression in switchExpression.Cases[i].TestValues)
				{
					this.EmitExpression(expression);
					this._scope.EmitSet(parameterExpression2);
					this.EmitExpressionAndBranch(true, Expression.Equal(parameterExpression, parameterExpression2, false, switchExpression.Comparison), array[i]);
				}
				i++;
			}
			Label label = this._ilg.DefineLabel();
			Label label2 = ((switchExpression.DefaultBody == null) ? label : this._ilg.DefineLabel());
			this.EmitSwitchCases(switchExpression, array, array2, label2, label, flags);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0002023C File Offset: 0x0001E43C
		private static Type GetTestValueType(SwitchExpression node)
		{
			if (node.Comparison == null)
			{
				return node.Cases[0].TestValues[0].Type;
			}
			Type type = node.Comparison.GetParametersCached()[1].ParameterType.GetNonRefType();
			if (node.IsLifted)
			{
				type = type.GetNullableType();
			}
			return type;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0002029C File Offset: 0x0001E49C
		private static bool FitsInBucket(List<LambdaCompiler.SwitchLabel> buckets, decimal key, int count)
		{
			decimal num = key - buckets[0].Key + 1m;
			return !(num > 2147483647m) && (buckets.Count + count) * 2 > num;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000202F0 File Offset: 0x0001E4F0
		private static void MergeBuckets(List<List<LambdaCompiler.SwitchLabel>> buckets)
		{
			while (buckets.Count > 1)
			{
				List<LambdaCompiler.SwitchLabel> list = buckets[buckets.Count - 2];
				List<LambdaCompiler.SwitchLabel> list2 = buckets[buckets.Count - 1];
				if (!LambdaCompiler.FitsInBucket(list, list2[list2.Count - 1].Key, list2.Count))
				{
					return;
				}
				list.AddRange(list2);
				buckets.RemoveAt(buckets.Count - 1);
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00020360 File Offset: 0x0001E560
		private static void AddToBuckets(List<List<LambdaCompiler.SwitchLabel>> buckets, LambdaCompiler.SwitchLabel key)
		{
			if (buckets.Count > 0)
			{
				List<LambdaCompiler.SwitchLabel> list = buckets[buckets.Count - 1];
				if (LambdaCompiler.FitsInBucket(list, key.Key, 1))
				{
					list.Add(key);
					LambdaCompiler.MergeBuckets(buckets);
					return;
				}
			}
			buckets.Add(new List<LambdaCompiler.SwitchLabel> { key });
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000203B4 File Offset: 0x0001E5B4
		private static bool CanOptimizeSwitchType(Type valueType)
		{
			TypeCode typeCode = valueType.GetTypeCode();
			return typeCode - TypeCode.Char <= 8;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000203D4 File Offset: 0x0001E5D4
		private bool TryEmitSwitchInstruction(SwitchExpression node, LambdaCompiler.CompilationFlags flags)
		{
			if (node.Comparison != null)
			{
				return false;
			}
			Type type = node.SwitchValue.Type;
			if (!LambdaCompiler.CanOptimizeSwitchType(type) || !TypeUtils.AreEquivalent(type, node.Cases[0].TestValues[0].Type))
			{
				return false;
			}
			if (!node.Cases.All((SwitchCase c) => c.TestValues.All((Expression t) => t is ConstantExpression)))
			{
				return false;
			}
			Label[] array = new Label[node.Cases.Count];
			bool[] array2 = new bool[node.Cases.Count];
			HashSet<decimal> hashSet = new HashSet<decimal>();
			List<LambdaCompiler.SwitchLabel> list = new List<LambdaCompiler.SwitchLabel>();
			for (int i = 0; i < node.Cases.Count; i++)
			{
				this.DefineSwitchCaseLabel(node.Cases[i], out array[i], out array2[i]);
				foreach (Expression expression in node.Cases[i].TestValues)
				{
					ConstantExpression constantExpression = (ConstantExpression)expression;
					decimal num = LambdaCompiler.ConvertSwitchValue(constantExpression.Value);
					if (hashSet.Add(num))
					{
						list.Add(new LambdaCompiler.SwitchLabel(num, constantExpression.Value, array[i]));
					}
				}
			}
			list.Sort((LambdaCompiler.SwitchLabel x, LambdaCompiler.SwitchLabel y) => Math.Sign(x.Key - y.Key));
			List<List<LambdaCompiler.SwitchLabel>> list2 = new List<List<LambdaCompiler.SwitchLabel>>();
			foreach (LambdaCompiler.SwitchLabel switchLabel in list)
			{
				LambdaCompiler.AddToBuckets(list2, switchLabel);
			}
			LocalBuilder local = this.GetLocal(node.SwitchValue.Type);
			this.EmitExpression(node.SwitchValue);
			this._ilg.Emit(OpCodes.Stloc, local);
			Label label = this._ilg.DefineLabel();
			Label label2 = ((node.DefaultBody == null) ? label : this._ilg.DefineLabel());
			LambdaCompiler.SwitchInfo switchInfo = new LambdaCompiler.SwitchInfo(node, local, label2);
			this.EmitSwitchBuckets(switchInfo, list2, 0, list2.Count - 1);
			this.EmitSwitchCases(node, array, array2, label2, label, flags);
			this.FreeLocal(local);
			return true;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00020650 File Offset: 0x0001E850
		private static decimal ConvertSwitchValue(object value)
		{
			if (value is char)
			{
				return (int)((char)value);
			}
			return Convert.ToDecimal(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00020674 File Offset: 0x0001E874
		private void DefineSwitchCaseLabel(SwitchCase @case, out Label label, out bool isGoto)
		{
			GotoExpression gotoExpression = @case.Body as GotoExpression;
			if (gotoExpression != null && gotoExpression.Value == null)
			{
				LabelInfo labelInfo = this.ReferenceLabel(gotoExpression.Target);
				if (labelInfo.CanBranch)
				{
					label = labelInfo.Label;
					isGoto = true;
					return;
				}
			}
			label = this._ilg.DefineLabel();
			isGoto = false;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x000206D4 File Offset: 0x0001E8D4
		private void EmitSwitchCases(SwitchExpression node, Label[] labels, bool[] isGoto, Label @default, Label end, LambdaCompiler.CompilationFlags flags)
		{
			this._ilg.Emit(OpCodes.Br, @default);
			int i = 0;
			int count = node.Cases.Count;
			while (i < count)
			{
				if (!isGoto[i])
				{
					this._ilg.MarkLabel(labels[i]);
					this.EmitExpressionAsType(node.Cases[i].Body, node.Type, flags);
					if (node.DefaultBody != null || i < count - 1)
					{
						if ((flags & LambdaCompiler.CompilationFlags.EmitAsTailCallMask) == LambdaCompiler.CompilationFlags.EmitAsTail)
						{
							this._ilg.Emit(OpCodes.Ret);
						}
						else
						{
							this._ilg.Emit(OpCodes.Br, end);
						}
					}
				}
				i++;
			}
			if (node.DefaultBody != null)
			{
				this._ilg.MarkLabel(@default);
				this.EmitExpressionAsType(node.DefaultBody, node.Type, flags);
			}
			this._ilg.MarkLabel(end);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x000207B8 File Offset: 0x0001E9B8
		private void EmitSwitchBuckets(LambdaCompiler.SwitchInfo info, List<List<LambdaCompiler.SwitchLabel>> buckets, int first, int last)
		{
			while (first != last)
			{
				int num = (int)(((long)first + (long)last + 1L) / 2L);
				if (first == num - 1)
				{
					this.EmitSwitchBucket(info, buckets[first]);
				}
				else
				{
					Label label = this._ilg.DefineLabel();
					this._ilg.Emit(OpCodes.Ldloc, info.Value);
					this.EmitConstant(buckets[num - 1].Last<LambdaCompiler.SwitchLabel>().Constant);
					this._ilg.Emit(info.IsUnsigned ? OpCodes.Bgt_Un : OpCodes.Bgt, label);
					this.EmitSwitchBuckets(info, buckets, first, num - 1);
					this._ilg.MarkLabel(label);
				}
				first = num;
			}
			this.EmitSwitchBucket(info, buckets[first]);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00020878 File Offset: 0x0001EA78
		private void EmitSwitchBucket(LambdaCompiler.SwitchInfo info, List<LambdaCompiler.SwitchLabel> bucket)
		{
			if (bucket.Count == 1)
			{
				this._ilg.Emit(OpCodes.Ldloc, info.Value);
				this.EmitConstant(bucket[0].Constant);
				this._ilg.Emit(OpCodes.Beq, bucket[0].Label);
				return;
			}
			Label? label = null;
			if (info.Is64BitSwitch)
			{
				label = new Label?(this._ilg.DefineLabel());
				this._ilg.Emit(OpCodes.Ldloc, info.Value);
				this.EmitConstant(bucket.Last<LambdaCompiler.SwitchLabel>().Constant);
				this._ilg.Emit(info.IsUnsigned ? OpCodes.Bgt_Un : OpCodes.Bgt, label.Value);
				this._ilg.Emit(OpCodes.Ldloc, info.Value);
				this.EmitConstant(bucket[0].Constant);
				this._ilg.Emit(info.IsUnsigned ? OpCodes.Blt_Un : OpCodes.Blt, label.Value);
			}
			this._ilg.Emit(OpCodes.Ldloc, info.Value);
			decimal num = bucket[0].Key;
			if (num != 0m)
			{
				this.EmitConstant(bucket[0].Constant);
				this._ilg.Emit(OpCodes.Sub);
			}
			if (info.Is64BitSwitch)
			{
				this._ilg.Emit(OpCodes.Conv_I4);
			}
			Label[] array = new Label[(int)(bucket[bucket.Count - 1].Key - bucket[0].Key + 1m)];
			int num2 = 0;
			foreach (LambdaCompiler.SwitchLabel switchLabel in bucket)
			{
				for (;;)
				{
					decimal num3 = num;
					num = num3 + 1m;
					if (!(num3 != switchLabel.Key))
					{
						break;
					}
					array[num2++] = info.Default;
				}
				array[num2++] = switchLabel.Label;
			}
			this._ilg.Emit(OpCodes.Switch, array);
			if (info.Is64BitSwitch)
			{
				this._ilg.MarkLabel(label.Value);
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00020ADC File Offset: 0x0001ECDC
		private bool TryEmitHashtableSwitch(SwitchExpression node, LambdaCompiler.CompilationFlags flags)
		{
			if (node.Comparison != CachedReflectionInfo.String_op_Equality_String_String && node.Comparison != CachedReflectionInfo.String_Equals_String_String)
			{
				return false;
			}
			int num = 0;
			foreach (SwitchCase switchCase in node.Cases)
			{
				using (IEnumerator<Expression> enumerator2 = switchCase.TestValues.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (!(enumerator2.Current is ConstantExpression))
						{
							return false;
						}
						num++;
					}
				}
			}
			if (num < 7)
			{
				return false;
			}
			List<ElementInit> list = new List<ElementInit>(num);
			global::System.Collections.Generic.ArrayBuilder<SwitchCase> arrayBuilder = new global::System.Collections.Generic.ArrayBuilder<SwitchCase>(node.Cases.Count);
			int num2 = -1;
			MethodInfo dictionaryOfStringInt32_Add_String_Int = CachedReflectionInfo.DictionaryOfStringInt32_Add_String_Int32;
			int i = 0;
			int count = node.Cases.Count;
			while (i < count)
			{
				foreach (Expression expression in node.Cases[i].TestValues)
				{
					ConstantExpression constantExpression = (ConstantExpression)expression;
					if (constantExpression.Value != null)
					{
						list.Add(Expression.ElementInit(dictionaryOfStringInt32_Add_String_Int, new TrueReadOnlyCollection<Expression>(new Expression[]
						{
							constantExpression,
							Utils.Constant(i)
						})));
					}
					else
					{
						num2 = i;
					}
				}
				arrayBuilder.UncheckedAdd(Expression.SwitchCase(node.Cases[i].Body, new TrueReadOnlyCollection<Expression>(new Expression[] { Utils.Constant(i) })));
				i++;
			}
			MemberExpression memberExpression = this.CreateLazyInitializedField<Dictionary<string, int>>("dictionarySwitch");
			Expression expression2 = Expression.Condition(Expression.Equal(memberExpression, Expression.Constant(null, memberExpression.Type)), Expression.Assign(memberExpression, Expression.ListInit(Expression.New(CachedReflectionInfo.DictionaryOfStringInt32_Ctor_Int32, new TrueReadOnlyCollection<Expression>(new Expression[] { Utils.Constant(list.Count) })), list)), memberExpression);
			ParameterExpression parameterExpression = Expression.Variable(typeof(string), "switchValue");
			ParameterExpression parameterExpression2 = Expression.Variable(typeof(int), "switchIndex");
			BlockExpression blockExpression = Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression2, parameterExpression }), new TrueReadOnlyCollection<Expression>(new Expression[]
			{
				Expression.Assign(parameterExpression, node.SwitchValue),
				Expression.IfThenElse(Expression.Equal(parameterExpression, Expression.Constant(null, typeof(string))), Expression.Assign(parameterExpression2, Utils.Constant(num2)), Expression.IfThenElse(Expression.Call(expression2, "TryGetValue", null, new Expression[] { parameterExpression, parameterExpression2 }), Utils.Empty, Expression.Assign(parameterExpression2, Utils.Constant(-1)))),
				Expression.Switch(node.Type, parameterExpression2, node.DefaultBody, null, arrayBuilder.ToReadOnly<SwitchCase>())
			}));
			this.EmitExpression(blockExpression, flags);
			return true;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00020DE0 File Offset: 0x0001EFE0
		private void CheckRethrow()
		{
			for (LabelScopeInfo labelScopeInfo = this._labelBlock; labelScopeInfo != null; labelScopeInfo = labelScopeInfo.Parent)
			{
				if (labelScopeInfo.Kind == LabelScopeKind.Catch)
				{
					return;
				}
				if (labelScopeInfo.Kind == LabelScopeKind.Finally)
				{
					break;
				}
			}
			throw Error.RethrowRequiresCatch();
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00020E18 File Offset: 0x0001F018
		private void CheckTry()
		{
			for (LabelScopeInfo labelScopeInfo = this._labelBlock; labelScopeInfo != null; labelScopeInfo = labelScopeInfo.Parent)
			{
				if (labelScopeInfo.Kind == LabelScopeKind.Filter)
				{
					throw Error.TryNotAllowedInFilter();
				}
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00020E47 File Offset: 0x0001F047
		private void EmitSaveExceptionOrPop(CatchBlock cb)
		{
			if (cb.Variable != null)
			{
				this._scope.EmitSet(cb.Variable);
				return;
			}
			this._ilg.Emit(OpCodes.Pop);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00020E74 File Offset: 0x0001F074
		private void EmitTryExpression(Expression expr)
		{
			TryExpression tryExpression = (TryExpression)expr;
			this.CheckTry();
			this.PushLabelBlock(LabelScopeKind.Try);
			this._ilg.BeginExceptionBlock();
			this.EmitExpression(tryExpression.Body);
			Type type = tryExpression.Type;
			LocalBuilder localBuilder = null;
			if (type != typeof(void))
			{
				localBuilder = this.GetLocal(type);
				this._ilg.Emit(OpCodes.Stloc, localBuilder);
			}
			foreach (CatchBlock catchBlock in tryExpression.Handlers)
			{
				this.PushLabelBlock(LabelScopeKind.Catch);
				if (catchBlock.Filter == null)
				{
					this._ilg.BeginCatchBlock(catchBlock.Test);
				}
				else
				{
					this._ilg.BeginExceptFilterBlock();
				}
				this.EnterScope(catchBlock);
				this.EmitCatchStart(catchBlock);
				this.EmitExpression(catchBlock.Body);
				if (type != typeof(void))
				{
					this._ilg.Emit(OpCodes.Stloc, localBuilder);
				}
				this.ExitScope(catchBlock);
				this.PopLabelBlock(LabelScopeKind.Catch);
			}
			if (tryExpression.Finally != null || tryExpression.Fault != null)
			{
				this.PushLabelBlock(LabelScopeKind.Finally);
				if (tryExpression.Finally != null)
				{
					this._ilg.BeginFinallyBlock();
				}
				else
				{
					this._ilg.BeginFaultBlock();
				}
				this.EmitExpressionAsVoid(tryExpression.Finally ?? tryExpression.Fault);
				this._ilg.EndExceptionBlock();
				this.PopLabelBlock(LabelScopeKind.Finally);
			}
			else
			{
				this._ilg.EndExceptionBlock();
			}
			if (type != typeof(void))
			{
				this._ilg.Emit(OpCodes.Ldloc, localBuilder);
				this.FreeLocal(localBuilder);
			}
			this.PopLabelBlock(LabelScopeKind.Try);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0002103C File Offset: 0x0001F23C
		private void EmitCatchStart(CatchBlock cb)
		{
			if (cb.Filter == null)
			{
				this.EmitSaveExceptionOrPop(cb);
				return;
			}
			Label label = this._ilg.DefineLabel();
			Label label2 = this._ilg.DefineLabel();
			this._ilg.Emit(OpCodes.Isinst, cb.Test);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Brtrue, label2);
			this._ilg.Emit(OpCodes.Pop);
			this._ilg.Emit(OpCodes.Ldc_I4_0);
			this._ilg.Emit(OpCodes.Br, label);
			this._ilg.MarkLabel(label2);
			this.EmitSaveExceptionOrPop(cb);
			this.PushLabelBlock(LabelScopeKind.Filter);
			this.EmitExpression(cb.Filter);
			this.PopLabelBlock(LabelScopeKind.Filter);
			this._ilg.MarkLabel(label);
			this._ilg.BeginCatchBlock(null);
			this._ilg.Emit(OpCodes.Pop);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0002112E File Offset: 0x0001F32E
		private void EmitQuoteUnaryExpression(Expression expr)
		{
			this.EmitQuote((UnaryExpression)expr);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0002113C File Offset: 0x0001F33C
		private void EmitQuote(UnaryExpression quote)
		{
			this.EmitConstant(quote.Operand, quote.Type);
			if (this._scope.NearestHoistedLocals != null)
			{
				this.EmitConstant(this._scope.NearestHoistedLocals, typeof(object));
				this._scope.EmitGet(this._scope.NearestHoistedLocals.SelfVariable);
				this._ilg.Emit(OpCodes.Call, CachedReflectionInfo.RuntimeOps_Quote);
				this._ilg.Emit(OpCodes.Castclass, quote.Type);
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x000211C9 File Offset: 0x0001F3C9
		private void EmitThrowUnaryExpression(Expression expr)
		{
			this.EmitThrow((UnaryExpression)expr, LambdaCompiler.CompilationFlags.EmitAsDefaultType);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x000211DC File Offset: 0x0001F3DC
		private void EmitThrow(UnaryExpression expr, LambdaCompiler.CompilationFlags flags)
		{
			if (expr.Operand == null)
			{
				this.CheckRethrow();
				this._ilg.Emit(OpCodes.Rethrow);
			}
			else
			{
				this.EmitExpression(expr.Operand);
				this._ilg.Emit(OpCodes.Throw);
			}
			this.EmitUnreachable(expr, flags);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0002122D File Offset: 0x0001F42D
		private void EmitUnaryExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			this.EmitUnary((UnaryExpression)expr, flags);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0002123C File Offset: 0x0001F43C
		private void EmitUnary(UnaryExpression node, LambdaCompiler.CompilationFlags flags)
		{
			if (node.Method != null)
			{
				this.EmitUnaryMethod(node, flags);
				return;
			}
			if (node.NodeType != ExpressionType.NegateChecked || !node.Operand.Type.IsInteger())
			{
				this.EmitExpression(node.Operand);
				this.EmitUnaryOperator(node.NodeType, node.Operand.Type, node.Type);
				return;
			}
			Type type = node.Type;
			if (type.IsNullableType())
			{
				Label label = this._ilg.DefineLabel();
				Label label2 = this._ilg.DefineLabel();
				this.EmitExpression(node.Operand);
				LocalBuilder local = this.GetLocal(type);
				this._ilg.Emit(OpCodes.Stloc, local);
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.EmitGetValueOrDefault(type);
				this._ilg.Emit(OpCodes.Brfalse_S, label);
				Type nonNullableType = type.GetNonNullableType();
				this._ilg.EmitDefault(nonNullableType, null);
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.EmitGetValueOrDefault(type);
				this.EmitBinaryOperator(ExpressionType.SubtractChecked, nonNullableType, nonNullableType, nonNullableType, false);
				this._ilg.Emit(OpCodes.Newobj, type.GetConstructor(new Type[] { nonNullableType }));
				this._ilg.Emit(OpCodes.Br_S, label2);
				this._ilg.MarkLabel(label);
				this._ilg.Emit(OpCodes.Ldloc, local);
				this.FreeLocal(local);
				this._ilg.MarkLabel(label2);
				return;
			}
			this._ilg.EmitDefault(type, null);
			this.EmitExpression(node.Operand);
			this.EmitBinaryOperator(ExpressionType.SubtractChecked, type, type, type, false);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x000213F4 File Offset: 0x0001F5F4
		private void EmitUnaryOperator(ExpressionType op, Type operandType, Type resultType)
		{
			bool flag = operandType.IsNullableType();
			if (op == ExpressionType.ArrayLength)
			{
				this._ilg.Emit(OpCodes.Ldlen);
				return;
			}
			if (!flag)
			{
				if (op <= ExpressionType.TypeAs)
				{
					switch (op)
					{
					case ExpressionType.Negate:
					case ExpressionType.NegateChecked:
						this._ilg.Emit(OpCodes.Neg);
						return;
					case ExpressionType.UnaryPlus:
						return;
					case ExpressionType.New:
					case ExpressionType.NewArrayInit:
					case ExpressionType.NewArrayBounds:
						goto IL_02EB;
					case ExpressionType.Not:
						if (operandType == typeof(bool))
						{
							this._ilg.Emit(OpCodes.Ldc_I4_0);
							this._ilg.Emit(OpCodes.Ceq);
							return;
						}
						break;
					default:
						if (op != ExpressionType.TypeAs)
						{
							goto IL_02EB;
						}
						if (operandType != resultType)
						{
							if (operandType.IsValueType)
							{
								this._ilg.Emit(OpCodes.Box, operandType);
							}
							this._ilg.Emit(OpCodes.Isinst, resultType);
							if (resultType.IsNullableType())
							{
								this._ilg.Emit(OpCodes.Unbox_Any, resultType);
							}
						}
						return;
					}
				}
				else
				{
					if (op == ExpressionType.Decrement)
					{
						this.EmitConstantOne(resultType);
						this._ilg.Emit(OpCodes.Sub);
						goto IL_02EB;
					}
					if (op == ExpressionType.Increment)
					{
						this.EmitConstantOne(resultType);
						this._ilg.Emit(OpCodes.Add);
						goto IL_02EB;
					}
					switch (op)
					{
					case ExpressionType.OnesComplement:
						break;
					case ExpressionType.IsTrue:
						this._ilg.Emit(OpCodes.Ldc_I4_1);
						this._ilg.Emit(OpCodes.Ceq);
						return;
					case ExpressionType.IsFalse:
						this._ilg.Emit(OpCodes.Ldc_I4_0);
						this._ilg.Emit(OpCodes.Ceq);
						return;
					default:
						goto IL_02EB;
					}
				}
				this._ilg.Emit(OpCodes.Not);
				if (!operandType.IsUnsigned())
				{
					return;
				}
				IL_02EB:
				this.EmitConvertArithmeticResult(op, resultType);
				return;
			}
			if (op == ExpressionType.UnaryPlus)
			{
				return;
			}
			if (op != ExpressionType.TypeAs)
			{
				Label label = this._ilg.DefineLabel();
				Label label2 = this._ilg.DefineLabel();
				LocalBuilder local = this.GetLocal(operandType);
				this._ilg.Emit(OpCodes.Stloc, local);
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.EmitHasValue(operandType);
				this._ilg.Emit(OpCodes.Brfalse_S, label);
				this._ilg.Emit(OpCodes.Ldloca, local);
				this._ilg.EmitGetValueOrDefault(operandType);
				Type nonNullableType = resultType.GetNonNullableType();
				this.EmitUnaryOperator(op, nonNullableType, nonNullableType);
				ConstructorInfo constructor = resultType.GetConstructor(new Type[] { nonNullableType });
				this._ilg.Emit(OpCodes.Newobj, constructor);
				this._ilg.Emit(OpCodes.Br_S, label2);
				this._ilg.MarkLabel(label);
				this._ilg.Emit(OpCodes.Ldloc, local);
				this.FreeLocal(local);
				this._ilg.MarkLabel(label2);
				return;
			}
			if (operandType != resultType)
			{
				this._ilg.Emit(OpCodes.Box, operandType);
				this._ilg.Emit(OpCodes.Isinst, resultType);
				if (resultType.IsNullableType())
				{
					this._ilg.Emit(OpCodes.Unbox_Any, resultType);
				}
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x000216F4 File Offset: 0x0001F8F4
		private void EmitConstantOne(Type type)
		{
			switch (type.GetTypeCode())
			{
			case TypeCode.Int64:
			case TypeCode.UInt64:
				this._ilg.Emit(OpCodes.Ldc_I4_1);
				this._ilg.Emit(OpCodes.Conv_I8);
				return;
			case TypeCode.Single:
				this._ilg.Emit(OpCodes.Ldc_R4, 1f);
				return;
			case TypeCode.Double:
				this._ilg.Emit(OpCodes.Ldc_R8, 1.0);
				return;
			default:
				this._ilg.Emit(OpCodes.Ldc_I4_1);
				return;
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00021784 File Offset: 0x0001F984
		private void EmitUnboxUnaryExpression(Expression expr)
		{
			UnaryExpression unaryExpression = (UnaryExpression)expr;
			this.EmitExpression(unaryExpression.Operand);
			this._ilg.Emit(OpCodes.Unbox_Any, unaryExpression.Type);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x000217BA File Offset: 0x0001F9BA
		private void EmitConvertUnaryExpression(Expression expr, LambdaCompiler.CompilationFlags flags)
		{
			this.EmitConvert((UnaryExpression)expr, flags);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x000217CC File Offset: 0x0001F9CC
		private void EmitConvert(UnaryExpression node, LambdaCompiler.CompilationFlags flags)
		{
			if (node.Method != null)
			{
				if (!node.IsLifted || (node.Type.IsValueType && node.Operand.Type.IsValueType))
				{
					this.EmitUnaryMethod(node, flags);
					return;
				}
				Type type = node.Method.GetParametersCached()[0].ParameterType;
				if (type.IsByRef)
				{
					type = type.GetElementType();
				}
				UnaryExpression unaryExpression = Expression.Convert(node.Operand, type);
				node = Expression.Convert(Expression.Call(node.Method, unaryExpression), node.Type);
			}
			if (node.Type == typeof(void))
			{
				this.EmitExpressionAsVoid(node.Operand, flags);
				return;
			}
			if (TypeUtils.AreEquivalent(node.Operand.Type, node.Type))
			{
				this.EmitExpression(node.Operand, flags);
				return;
			}
			this.EmitExpression(node.Operand);
			this._ilg.EmitConvertToType(node.Operand.Type, node.Type, node.NodeType == ExpressionType.ConvertChecked, this);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x000218E0 File Offset: 0x0001FAE0
		private void EmitUnaryMethod(UnaryExpression node, LambdaCompiler.CompilationFlags flags)
		{
			if (node.IsLifted)
			{
				ParameterExpression parameterExpression = Expression.Variable(node.Operand.Type.GetNonNullableType(), null);
				MethodCallExpression methodCallExpression = Expression.Call(node.Method, parameterExpression);
				Type nullableType = methodCallExpression.Type.GetNullableType();
				this.EmitLift(node.NodeType, nullableType, methodCallExpression, new ParameterExpression[] { parameterExpression }, new Expression[] { node.Operand });
				this._ilg.EmitConvertToType(nullableType, node.Type, false, this);
				return;
			}
			this.EmitMethodCallExpression(Expression.Call(node.Method, node.Operand), flags);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0002197C File Offset: 0x0001FB7C
		private LambdaCompiler(AnalyzedTree tree, LambdaExpression lambda)
		{
			Type[] parameterTypes = LambdaCompiler.GetParameterTypes(lambda, typeof(Closure));
			DynamicMethod dynamicMethod = new DynamicMethod(lambda.Name ?? "lambda_method", lambda.ReturnType, parameterTypes, true);
			this._tree = tree;
			this._lambda = lambda;
			this._method = dynamicMethod;
			this._ilg = dynamicMethod.GetILGenerator();
			this._hasClosureArgument = true;
			this._scope = tree.Scopes[lambda];
			this._boundConstants = tree.Constants[lambda];
			this.InitializeMethod();
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00021A40 File Offset: 0x0001FC40
		private LambdaCompiler(AnalyzedTree tree, LambdaExpression lambda, MethodBuilder method)
		{
			CompilerScope compilerScope = tree.Scopes[lambda];
			bool needsClosure = compilerScope.NeedsClosure;
			Type[] parameterTypes = LambdaCompiler.GetParameterTypes(lambda, needsClosure ? typeof(Closure) : null);
			method.SetReturnType(lambda.ReturnType);
			method.SetParameters(parameterTypes);
			ReadOnlyCollection<ParameterExpression> parameters = lambda.Parameters;
			int num = (needsClosure ? 2 : 1);
			int i = 0;
			int count = parameters.Count;
			while (i < count)
			{
				method.DefineParameter(i + num, ParameterAttributes.None, parameters[i].Name);
				i++;
			}
			this._tree = tree;
			this._lambda = lambda;
			this._typeBuilder = (TypeBuilder)method.DeclaringType;
			this._method = method;
			this._hasClosureArgument = needsClosure;
			this._ilg = method.GetILGenerator();
			this._scope = compilerScope;
			this._boundConstants = tree.Constants[lambda];
			this.InitializeMethod();
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00021B5C File Offset: 0x0001FD5C
		private LambdaCompiler(LambdaCompiler parent, LambdaExpression lambda, InvocationExpression invocation)
		{
			this._tree = parent._tree;
			this._lambda = lambda;
			this._method = parent._method;
			this._ilg = parent._ilg;
			this._hasClosureArgument = parent._hasClosureArgument;
			this._typeBuilder = parent._typeBuilder;
			this._scope = this._tree.Scopes[invocation];
			this._boundConstants = parent._boundConstants;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00021C03 File Offset: 0x0001FE03
		private void InitializeMethod()
		{
			this.AddReturnLabel(this._lambda);
			this._boundConstants.EmitCacheConstants(this);
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00021C1D File Offset: 0x0001FE1D
		internal ILGenerator IL
		{
			get
			{
				return this._ilg;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00021C25 File Offset: 0x0001FE25
		internal IParameterProvider Parameters
		{
			get
			{
				return this._lambda;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00021C2D File Offset: 0x0001FE2D
		internal bool CanEmitBoundConstants
		{
			get
			{
				return this._method is DynamicMethod;
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00021C3D File Offset: 0x0001FE3D
		internal static Delegate Compile(LambdaExpression lambda)
		{
			lambda.ValidateArgumentCount();
			LambdaCompiler lambdaCompiler = new LambdaCompiler(LambdaCompiler.AnalyzeLambda(ref lambda), lambda);
			lambdaCompiler.EmitLambdaBody();
			return lambdaCompiler.CreateDelegate();
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00021C5D File Offset: 0x0001FE5D
		private static AnalyzedTree AnalyzeLambda(ref LambdaExpression lambda)
		{
			lambda = StackSpiller.AnalyzeLambda(lambda);
			return VariableBinder.Bind(lambda);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00021C6F File Offset: 0x0001FE6F
		public LocalBuilder GetLocal(Type type)
		{
			return this._freeLocals.TryPop(type) ?? this._ilg.DeclareLocal(type);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00021C8D File Offset: 0x0001FE8D
		public void FreeLocal(LocalBuilder local)
		{
			this._freeLocals.Push(local.LocalType, local);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00021CA1 File Offset: 0x0001FEA1
		internal int GetLambdaArgument(int index)
		{
			return index + (this._hasClosureArgument ? 1 : 0) + (this._method.IsStatic ? 0 : 1);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00021CC3 File Offset: 0x0001FEC3
		internal void EmitLambdaArgument(int index)
		{
			this._ilg.EmitLoadArg(this.GetLambdaArgument(index));
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00021CD7 File Offset: 0x0001FED7
		internal void EmitClosureArgument()
		{
			this._ilg.EmitLoadArg(0);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00021CE5 File Offset: 0x0001FEE5
		private Delegate CreateDelegate()
		{
			return this._method.CreateDelegate(this._lambda.Type, new Closure(this._boundConstants.ToArray(), null));
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00021D10 File Offset: 0x0001FF10
		private FieldBuilder CreateStaticField(string name, Type type)
		{
			return this._typeBuilder.DefineField("<ExpressionCompilerImplementationDetails>{" + Interlocked.Increment(ref LambdaCompiler.s_counter).ToString() + "}" + name, type, FieldAttributes.Private | FieldAttributes.Static);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00021D50 File Offset: 0x0001FF50
		private MemberExpression CreateLazyInitializedField<T>(string name)
		{
			if (this._method is DynamicMethod)
			{
				return Expression.Field(Expression.Constant(new StrongBox<T>(default(T))), "Value");
			}
			return Expression.Field(null, this.CreateStaticField(name, typeof(T)));
		}

		// Token: 0x04000281 RID: 641
		private readonly StackGuard _guard = new StackGuard();

		// Token: 0x04000282 RID: 642
		private static int s_counter;

		// Token: 0x04000283 RID: 643
		private readonly AnalyzedTree _tree;

		// Token: 0x04000284 RID: 644
		private readonly ILGenerator _ilg;

		// Token: 0x04000285 RID: 645
		private readonly TypeBuilder _typeBuilder;

		// Token: 0x04000286 RID: 646
		private readonly MethodInfo _method;

		// Token: 0x04000287 RID: 647
		private LabelScopeInfo _labelBlock = new LabelScopeInfo(null, LabelScopeKind.Lambda);

		// Token: 0x04000288 RID: 648
		private readonly Dictionary<LabelTarget, LabelInfo> _labelInfo = new Dictionary<LabelTarget, LabelInfo>();

		// Token: 0x04000289 RID: 649
		private CompilerScope _scope;

		// Token: 0x0400028A RID: 650
		private readonly LambdaExpression _lambda;

		// Token: 0x0400028B RID: 651
		private readonly bool _hasClosureArgument;

		// Token: 0x0400028C RID: 652
		private readonly BoundConstants _boundConstants;

		// Token: 0x0400028D RID: 653
		private readonly KeyedStack<Type, LocalBuilder> _freeLocals = new KeyedStack<Type, LocalBuilder>();

		// Token: 0x020000F6 RID: 246
		[Flags]
		internal enum CompilationFlags
		{
			// Token: 0x0400028F RID: 655
			EmitExpressionStart = 1,
			// Token: 0x04000290 RID: 656
			EmitNoExpressionStart = 2,
			// Token: 0x04000291 RID: 657
			EmitAsDefaultType = 16,
			// Token: 0x04000292 RID: 658
			EmitAsVoidType = 32,
			// Token: 0x04000293 RID: 659
			EmitAsTail = 256,
			// Token: 0x04000294 RID: 660
			EmitAsMiddle = 512,
			// Token: 0x04000295 RID: 661
			EmitAsNoTail = 1024,
			// Token: 0x04000296 RID: 662
			EmitExpressionStartMask = 15,
			// Token: 0x04000297 RID: 663
			EmitAsTypeMask = 240,
			// Token: 0x04000298 RID: 664
			EmitAsTailCallMask = 3840
		}

		// Token: 0x020000F7 RID: 247
		private sealed class SwitchLabel
		{
			// Token: 0x060008C2 RID: 2242 RVA: 0x00021D9F File Offset: 0x0001FF9F
			internal SwitchLabel(decimal key, object constant, Label label)
			{
				this.Key = key;
				this.Constant = constant;
				this.Label = label;
			}

			// Token: 0x04000299 RID: 665
			internal readonly decimal Key;

			// Token: 0x0400029A RID: 666
			internal readonly Label Label;

			// Token: 0x0400029B RID: 667
			internal readonly object Constant;
		}

		// Token: 0x020000F8 RID: 248
		private sealed class SwitchInfo
		{
			// Token: 0x060008C3 RID: 2243 RVA: 0x00021DBC File Offset: 0x0001FFBC
			internal SwitchInfo(SwitchExpression node, LocalBuilder value, Label @default)
			{
				this.Node = node;
				this.Value = value;
				this.Default = @default;
				this.Type = this.Node.SwitchValue.Type;
				this.IsUnsigned = this.Type.IsUnsigned();
				TypeCode typeCode = this.Type.GetTypeCode();
				this.Is64BitSwitch = typeCode == TypeCode.UInt64 || typeCode == TypeCode.Int64;
			}

			// Token: 0x0400029C RID: 668
			internal readonly SwitchExpression Node;

			// Token: 0x0400029D RID: 669
			internal readonly LocalBuilder Value;

			// Token: 0x0400029E RID: 670
			internal readonly Label Default;

			// Token: 0x0400029F RID: 671
			internal readonly Type Type;

			// Token: 0x040002A0 RID: 672
			internal readonly bool IsUnsigned;

			// Token: 0x040002A1 RID: 673
			internal readonly bool Is64BitSwitch;
		}

		// Token: 0x020000F9 RID: 249
		// (Invoke) Token: 0x060008C5 RID: 2245
		private delegate void WriteBack(LambdaCompiler compiler);
	}
}
