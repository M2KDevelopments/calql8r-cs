//========================================================================
// This conversion was produced by the Free Edition of
// Java to C# Converter courtesy of Tangible Software Solutions.
// Order the Premium Edition at https://www.tangiblesoftwaresolutions.com
//========================================================================

using System.Collections.Generic;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
namespace calql8r.Enums
{
	/// 
	/// <summary>
	/// @author MARTIN
	/// </summary>
	public sealed class EnumError
	{
		public static readonly EnumError NONE = new EnumError("NONE", InnerEnum.NONE, "NONE");
		public static readonly EnumError MATH = new EnumError("MATH", InnerEnum.MATH, "MATH_ERROR");
		public static readonly EnumError SYNTAX = new EnumError("SYNTAX", InnerEnum.SYNTAX, "SYNTAX_ERROR");

		private static readonly IList<EnumError> valueList = new List<EnumError>();

		static EnumError()
		{
			valueList.Add(NONE);
			valueList.Add(MATH);
			valueList.Add(SYNTAX);
		}

		public enum InnerEnum
		{
			NONE,
			MATH,
			SYNTAX
		}

		public readonly InnerEnum innerEnumValue;
		private readonly string nameValue;
		private readonly int ordinalValue;
		private static int nextOrdinal = 0;
		public readonly string TEXT;
		internal EnumError(string name, InnerEnum innerEnum, string error)
		{
			TEXT = error;

			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		public static IList<EnumError> values()
		{
			return valueList;
		}

		public int ordinal()
		{
			return ordinalValue;
		}

		public override string ToString()
		{
			return nameValue;
		}

		public static EnumError valueOf(string name)
		{
			foreach (EnumError enumInstance in EnumError.valueList)
			{
				if (enumInstance.nameValue == name)
				{
					return enumInstance;
				}
			}
			throw new System.ArgumentException(name);
		}
	}

}