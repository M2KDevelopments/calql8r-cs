using System.Collections.Generic;
using System;

namespace calql8r.Enums
{
	using calql8r.Functions;
	using calql8r.Operators;
	using calql8r.Logic;
	using CalTime = calql8r.OperatorsExtra.CalTime;
	using CalNumber = calql8r.Values.CalNumber;
	using GeneralCalValue = calql8r.Values.GeneralCalValue;

	/// 
	/// <summary>
	/// @author MARTIN
	/// </summary>
	public sealed class EnumCalObjs
	{

		//FUNCTIONS
		public static readonly EnumCalObjs CAL_SINH = new EnumCalObjs("CAL_SINH", InnerEnum.CAL_SINH, calql8r.GeneralCalButton.FUNCTION,"sinh","Sinh",new CalSinh());
		public static readonly EnumCalObjs CAL_ARCSIN = new EnumCalObjs("CAL_ARCSIN", InnerEnum.CAL_ARCSIN, calql8r.GeneralCalButton.FUNCTION,"arcsin","asin",new CalArcSin());
		public static readonly EnumCalObjs CAL_SIN = new EnumCalObjs("CAL_SIN", InnerEnum.CAL_SIN, calql8r.GeneralCalButton.FUNCTION,"sin","Sin",new CalSin());
		public static readonly EnumCalObjs CAL_COSH = new EnumCalObjs("CAL_COSH", InnerEnum.CAL_COSH, calql8r.GeneralCalButton.FUNCTION,"cosh","Cosh",new CalCosh());
		public static readonly EnumCalObjs CAL_ARCCOS = new EnumCalObjs("CAL_ARCCOS", InnerEnum.CAL_ARCCOS, calql8r.GeneralCalButton.FUNCTION,"arccos","acos",new CalArcCos());
		public static readonly EnumCalObjs CAL_COS = new EnumCalObjs("CAL_COS", InnerEnum.CAL_COS, calql8r.GeneralCalButton.FUNCTION,"cos","Cos",new CalCos());
		public static readonly EnumCalObjs CAL_TANH = new EnumCalObjs("CAL_TANH", InnerEnum.CAL_TANH, calql8r.GeneralCalButton.FUNCTION,"tanh","Tanh",new CalTanh());
		public static readonly EnumCalObjs CAL_ARCTAN = new EnumCalObjs("CAL_ARCTAN", InnerEnum.CAL_ARCTAN, calql8r.GeneralCalButton.FUNCTION,"arctan","atan",new CalArcTan());
		public static readonly EnumCalObjs CAL_TAN = new EnumCalObjs("CAL_TAN", InnerEnum.CAL_TAN, calql8r.GeneralCalButton.FUNCTION,"tan","Tan",new CalTan());
		public static readonly EnumCalObjs CAL_EXP = new EnumCalObjs("CAL_EXP", InnerEnum.CAL_EXP, calql8r.GeneralCalButton.FUNCTION,"exp","e",new CalExp());
		public static readonly EnumCalObjs CAL_LN = new EnumCalObjs("CAL_LN", InnerEnum.CAL_LN, calql8r.GeneralCalButton.FUNCTION,"ln","Ln",new CalLn());
		public static readonly EnumCalObjs CAL_LOG = new EnumCalObjs("CAL_LOG", InnerEnum.CAL_LOG, calql8r.GeneralCalButton.FUNCTION,"log","Log",new CalLog());

		//OPERATORS
		public static readonly EnumCalObjs CAL_FACTORIAL = new EnumCalObjs("CAL_FACTORIAL", InnerEnum.CAL_FACTORIAL, calql8r.GeneralCalButton.OPERATOR,"!","!",new CalFactorial());
		public static readonly EnumCalObjs CAL_POW = new EnumCalObjs("CAL_POW", InnerEnum.CAL_POW, calql8r.GeneralCalButton.OPERATOR,"^","^",new CalPow());
		public static readonly EnumCalObjs CAL_COMBINATION = new EnumCalObjs("CAL_COMBINATION", InnerEnum.CAL_COMBINATION, calql8r.GeneralCalButton.OPERATOR,"C","C",new CalCombinations());
		public static readonly EnumCalObjs CAL_PERMUTATION = new EnumCalObjs("CAL_PERMUTATION", InnerEnum.CAL_PERMUTATION, calql8r.GeneralCalButton.OPERATOR,"P","P",new CalPermutations());
		public static readonly EnumCalObjs CAL_ROOT = new EnumCalObjs("CAL_ROOT", InnerEnum.CAL_ROOT, calql8r.GeneralCalButton.OPERATOR,"R","ⁿ√",new CalRoot());
		public static readonly EnumCalObjs CAL_BASE10 = new EnumCalObjs("CAL_BASE10", InnerEnum.CAL_BASE10, calql8r.GeneralCalButton.OPERATOR,"E","E",new CalBase10());
		public static readonly EnumCalObjs CAL_DIVISION = new EnumCalObjs("CAL_DIVISION", InnerEnum.CAL_DIVISION, calql8r.GeneralCalButton.OPERATOR,"/","÷",new CalDivision());
		public static readonly EnumCalObjs CAL_MULTIPLICATION = new EnumCalObjs("CAL_MULTIPLICATION", InnerEnum.CAL_MULTIPLICATION, calql8r.GeneralCalButton.OPERATOR,"*","x",new CalMultiplication());
		public static readonly EnumCalObjs CAL_MODULUS = new EnumCalObjs("CAL_MODULUS", InnerEnum.CAL_MODULUS, calql8r.GeneralCalButton.OPERATOR,"M","MOD",new CalModulus());
		public static readonly EnumCalObjs CAL_SUBTRACT = new EnumCalObjs("CAL_SUBTRACT", InnerEnum.CAL_SUBTRACT, calql8r.GeneralCalButton.OPERATOR,"-","─",new CalSubtract());
		public static readonly EnumCalObjs CAL_ADD = new EnumCalObjs("CAL_ADD", InnerEnum.CAL_ADD, calql8r.GeneralCalButton.OPERATOR,"+","+",new CalAdd());


		//EXTRA OPERATORS
		public static readonly EnumCalObjs CAL_TIME = new EnumCalObjs("CAL_TIME", InnerEnum.CAL_TIME, calql8r.GeneralCalButton.OPERATOR,"⁰","t",new calql8r.OperatorsExtra.CalTime());


		//LOGIC 
		public static readonly EnumCalObjs CAL_BRACKET_OPEN = new EnumCalObjs("CAL_BRACKET_OPEN", InnerEnum.CAL_BRACKET_OPEN, calql8r.GeneralCalButton.LOGIC,"(","[",new CalBracketOpen());
		public static readonly EnumCalObjs CAL_BRACKET_CLOSE = new EnumCalObjs("CAL_BRACKET_CLOSE", InnerEnum.CAL_BRACKET_CLOSE, calql8r.GeneralCalButton.LOGIC,")","]", new CalBracketClose());
		public static readonly EnumCalObjs CAL_DECIMAL = new EnumCalObjs("CAL_DECIMAL", InnerEnum.CAL_DECIMAL, calql8r.GeneralCalButton.LOGIC,".",".",new CalDecimal());
		public static readonly EnumCalObjs CAL_PARAMETERSEPARATOR = new EnumCalObjs("CAL_PARAMETERSEPARATOR", InnerEnum.CAL_PARAMETERSEPARATOR, calql8r.GeneralCalButton.LOGIC,",",",",new CalParameterSeparator());
		public static readonly EnumCalObjs CAL_ABSOLUTE_BRACKET = new EnumCalObjs("CAL_ABSOLUTE_BRACKET", InnerEnum.CAL_ABSOLUTE_BRACKET, calql8r.GeneralCalButton.LOGIC,"|","|",new CalAbsBracket());

		//VALUES
		public static readonly EnumCalObjs CAL_NUMBER0 = new EnumCalObjs("CAL_NUMBER0", InnerEnum.CAL_NUMBER0, calql8r.GeneralCalButton.VALUES,"0","0",new calql8r.Values.CalNumber(0));
		public static readonly EnumCalObjs CAL_NUMBER1 = new EnumCalObjs("CAL_NUMBER1", InnerEnum.CAL_NUMBER1, calql8r.GeneralCalButton.VALUES,"1","1",new calql8r.Values.CalNumber(1));
		public static readonly EnumCalObjs CAL_NUMBER2 = new EnumCalObjs("CAL_NUMBER2", InnerEnum.CAL_NUMBER2, calql8r.GeneralCalButton.VALUES,"2","2",new calql8r.Values.CalNumber(2));
		public static readonly EnumCalObjs CAL_NUMBER3 = new EnumCalObjs("CAL_NUMBER3", InnerEnum.CAL_NUMBER3, calql8r.GeneralCalButton.VALUES,"3","3",new calql8r.Values.CalNumber(3));
		public static readonly EnumCalObjs CAL_NUMBER4 = new EnumCalObjs("CAL_NUMBER4", InnerEnum.CAL_NUMBER4, calql8r.GeneralCalButton.VALUES,"4","4",new calql8r.Values.CalNumber(4));
		public static readonly EnumCalObjs CAL_NUMBER5 = new EnumCalObjs("CAL_NUMBER5", InnerEnum.CAL_NUMBER5, calql8r.GeneralCalButton.VALUES,"5","5",new calql8r.Values.CalNumber(5));
		public static readonly EnumCalObjs CAL_NUMBER6 = new EnumCalObjs("CAL_NUMBER6", InnerEnum.CAL_NUMBER6, calql8r.GeneralCalButton.VALUES,"6","6",new calql8r.Values.CalNumber(6));
		public static readonly EnumCalObjs CAL_NUMBER7 = new EnumCalObjs("CAL_NUMBER7", InnerEnum.CAL_NUMBER7, calql8r.GeneralCalButton.VALUES,"7","7",new calql8r.Values.CalNumber(7));
		public static readonly EnumCalObjs CAL_NUMBER8 = new EnumCalObjs("CAL_NUMBER8", InnerEnum.CAL_NUMBER8, calql8r.GeneralCalButton.VALUES,"8","8",new calql8r.Values.CalNumber(8));
		public static readonly EnumCalObjs CAL_NUMBER9 = new EnumCalObjs("CAL_NUMBER9", InnerEnum.CAL_NUMBER9, calql8r.GeneralCalButton.VALUES,"9","9",new calql8r.Values.CalNumber(9));
		public static readonly EnumCalObjs CAL_PI = new EnumCalObjs("CAL_PI", InnerEnum.CAL_PI, calql8r.GeneralCalButton.VALUES,"pi","PI",new calql8r.Values.GeneralCalValue(Math.PI));

		private static readonly IList<EnumCalObjs> valueList = new List<EnumCalObjs>();

		static EnumCalObjs()
		{
			valueList.Add(CAL_SINH);
			valueList.Add(CAL_ARCSIN);
			valueList.Add(CAL_SIN);
			valueList.Add(CAL_COSH);
			valueList.Add(CAL_ARCCOS);
			valueList.Add(CAL_COS);
			valueList.Add(CAL_TANH);
			valueList.Add(CAL_ARCTAN);
			valueList.Add(CAL_TAN);
			valueList.Add(CAL_EXP);
			valueList.Add(CAL_LN);
			valueList.Add(CAL_LOG);
			valueList.Add(CAL_FACTORIAL);
			valueList.Add(CAL_POW);
			valueList.Add(CAL_COMBINATION);
			valueList.Add(CAL_PERMUTATION);
			valueList.Add(CAL_ROOT);
			valueList.Add(CAL_BASE10);
			valueList.Add(CAL_DIVISION);
			valueList.Add(CAL_MULTIPLICATION);
			valueList.Add(CAL_MODULUS);
			valueList.Add(CAL_SUBTRACT);
			valueList.Add(CAL_ADD);
			valueList.Add(CAL_TIME);
			valueList.Add(CAL_BRACKET_OPEN);
			valueList.Add(CAL_BRACKET_CLOSE);
			valueList.Add(CAL_DECIMAL);
			valueList.Add(CAL_PARAMETERSEPARATOR);
			valueList.Add(CAL_ABSOLUTE_BRACKET);
			valueList.Add(CAL_NUMBER0);
			valueList.Add(CAL_NUMBER1);
			valueList.Add(CAL_NUMBER2);
			valueList.Add(CAL_NUMBER3);
			valueList.Add(CAL_NUMBER4);
			valueList.Add(CAL_NUMBER5);
			valueList.Add(CAL_NUMBER6);
			valueList.Add(CAL_NUMBER7);
			valueList.Add(CAL_NUMBER8);
			valueList.Add(CAL_NUMBER9);
			valueList.Add(CAL_PI);
		}

		public enum InnerEnum
		{
			CAL_SINH,
			CAL_ARCSIN,
			CAL_SIN,
			CAL_COSH,
			CAL_ARCCOS,
			CAL_COS,
			CAL_TANH,
			CAL_ARCTAN,
			CAL_TAN,
			CAL_EXP,
			CAL_LN,
			CAL_LOG,
			CAL_FACTORIAL,
			CAL_POW,
			CAL_COMBINATION,
			CAL_PERMUTATION,
			CAL_ROOT,
			CAL_BASE10,
			CAL_DIVISION,
			CAL_MULTIPLICATION,
			CAL_MODULUS,
			CAL_SUBTRACT,
			CAL_ADD,
			CAL_TIME,
			CAL_BRACKET_OPEN,
			CAL_BRACKET_CLOSE,
			CAL_DECIMAL,
			CAL_PARAMETERSEPARATOR,
			CAL_ABSOLUTE_BRACKET,
			CAL_NUMBER0,
			CAL_NUMBER1,
			CAL_NUMBER2,
			CAL_NUMBER3,
			CAL_NUMBER4,
			CAL_NUMBER5,
			CAL_NUMBER6,
			CAL_NUMBER7,
			CAL_NUMBER8,
			CAL_NUMBER9,
			CAL_PI
		}

		public readonly InnerEnum innerEnumValue;
		private readonly string nameValue;
		private readonly int ordinalValue;
		private static int nextOrdinal = 0;

		//properties
		private readonly int type;
		private readonly string displayText1;
		private readonly string displayText2;
		private readonly calql8r.GeneralCalButton calobject;

		internal EnumCalObjs(string name, InnerEnum innerEnum, int type, string displayText1, string displayText2, calql8r.GeneralCalButton calobject)
		{
			this.type = type;
			this.displayText1 = displayText1;
			this.displayText2 = displayText2;
			this.calobject = calobject;

			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		public int Type
		{
			get
			{
				return type;
			}
		}

		public string DisplayText1
		{
			get
			{
				return displayText1;
			}
		}

		public string DisplayText2
		{
			get
			{
				return displayText2;
			}
		}
		public calql8r.GeneralCalButton CalculateObject
		{
			get
			{
				return calobject;
			}
		}

		public static IList<EnumCalObjs> values()
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

		public static EnumCalObjs valueOf(string name)
		{
			foreach (EnumCalObjs enumInstance in EnumCalObjs.valueList)
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