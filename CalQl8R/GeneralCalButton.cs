/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
namespace calql8r
{
	using EnumError = calql8r.Enums.EnumError;
	using CalAns = calql8r.Values.CalAns;

	/// 
	/// <summary>
	/// @author MARTIN
	/// </summary>
	public abstract class GeneralCalButton
	{

		public const int FUNCTION = 1;
		public const int OPERATOR = 2;
		public const int EXTRA_OPERATOR = 3;
		public const int LOGIC = 4;
		public const int VALUES = 5;

		public abstract string displayText();

		public override string ToString()
		{
			return displayText();
		}

		protected internal virtual CalAns mathError()
		{
			CalAns a = new CalAns(0);
			a.Error = EnumError.MATH;
			return a;
		}

		protected internal virtual CalAns syntaxError()
		{
			CalAns a = new CalAns(0);
			a.Error = EnumError.SYNTAX;
			return a;
		}
	}

}