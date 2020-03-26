using System;
using System.Collections.Generic;

namespace calql8r
{
	using calql8r.Enums;
	using GeneralCalFunction = calql8r.Functions.GeneralCalFunction;
	using calql8r.Values;
	using calql8r.Logic;
	using CalAdd = calql8r.Operators.CalAdd;
	using CalBase10 = calql8r.Operators.CalBase10;
	using CalFactorial = calql8r.Operators.CalFactorial;
	using CalPow = calql8r.Operators.CalPow;
	using CalSubtract = calql8r.Operators.CalSubtract;
	using GeneralCalOperator = calql8r.Operators.GeneralCalOperator;
	using CalTime = calql8r.OperatorsExtra.CalTime;

	/// <summary>
	/// @author MARTIN
	/// </summary>
    public sealed class CalQl8R
    {

        private List<GeneralCalButton> calObjs;
        private CalAns answer;

        //CONSTRUCTORS
        public CalQl8R()
        {
            calObjs = new List<GeneralCalButton>();
            answer = new CalAns(0);
            calObjs.Clear();
        }

        /// <param name="expression"> The expression to be calculated. For example: 1+3/4*5-(42-24). </param>
        public CalQl8R(string expression)
        {
            calObjs = new List<GeneralCalButton>();
            answer = new CalAns(0);
            calObjs.Clear();
            setExpression(expression);
        }

        public void run()
        {
            calObjs = decimalLoop(calObjs);
            calObjs = numberLoop(calObjs);
            calObjs = timeLoop(calObjs);
            calObjs = positiveAndNegativeLoop(calObjs);
            calObjs = bracketAbsLoop(calObjs);
            calObjs = functionLoop(calObjs);
            answer = (CalAns)calculateSolution(calObjs);
        }

        private GeneralCalValue calculateSolution(List<GeneralCalButton> calculatorObjs)
        {
            calculatorObjs = decimalLoop(calculatorObjs);
            calculatorObjs = numberLoop(calculatorObjs);
            calculatorObjs = positiveAndNegativeLoop(calculatorObjs);
            calculatorObjs = bracketLoop(calculatorObjs);
            calculatorObjs = positiveAndNegativeLoop(calculatorObjs);
            return operatorLoop(calculatorObjs);
        }

        private GeneralCalValue calculateSolution(GeneralCalButton[] calculatorObjs)
        {
            List<GeneralCalButton> c = new List<GeneralCalButton>();
            for (int i =0; i<calculatorObjs.Length; i++){
                 c.Add(calculatorObjs[i]);
            }
            c = functionLoop(c);
            return calculateSolution(c);
        }

        public void clearMemory()
        {
            calObjs.Clear();
        }

        /// <summary>
        /// This method finds the preceeding and proceeding number from the decimal point and combines them. </summary>
        /// <param name="calculatorObj">
        /// @return </param>
        private List<GeneralCalButton> decimalLoop(List<GeneralCalButton> calculatorObj)
        {

            try
            {
                int searchingIndex = 0;
                while (searchingIndex < calculatorObj.Count)
                {

                    if (calculatorObj[searchingIndex] is CalDecimal)
                    {

                        //find preceeding numbers
                        int preceedIndex = searchingIndex - 1;
                        while (calculatorObj[preceedIndex] is CalNumber)
                        {
                            preceedIndex--;
                            if (preceedIndex == -1)
                            {
                                preceedIndex = 0;
                                break;
                            }
                        }

                        //find proceeding numbers
                        int proceedIndex = searchingIndex + 1;
                        while (calculatorObj[proceedIndex] is CalNumber)
                        {
                            proceedIndex++;
                            if (proceedIndex == calculatorObj.Count)
                            {
                                proceedIndex--;
                                break;
                            }
                        }

                        //fix the indices to corrct first and last values
                        if (preceedIndex > 0)
                        {
                            preceedIndex++;
                        }
                        else if (!(calculatorObj[0] is CalNumber))
                        {
                            preceedIndex++;
                        }
                        if (proceedIndex < (calculatorObj.Count - 1))
                        {
                            proceedIndex--;
                        }
                        else if (!(calculatorObj[calculatorObj.Count - 1] is CalNumber))
                        {
                            proceedIndex--;
                        }

                        //create the preceeding and proceeding numbers
                        double preceedingNumber = 0;
                        double proceedingNumber = 0;
                        const int BASE = 10;
                        for (int i = preceedIndex; i < searchingIndex; i++)
                        {
                            GeneralCalValue gv = (GeneralCalValue)calculatorObj[i];
                            preceedingNumber *= BASE;
                            preceedingNumber += gv.Value;
                        }

                        int pow = 0;
                        try
                        {
                            for (int i = searchingIndex + 1; i <= proceedIndex; i++)
                            {
                                GeneralCalValue gv = (GeneralCalValue)calculatorObj[i];
                                proceedingNumber += gv.Value / Math.Pow(BASE, ++pow);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine("Error...0x01\n" + "decimal loop error:proceeding number\n" + ex.Message);

                        }

                        //replace the objects
                        int number_of_object = proceedIndex - preceedIndex;
                        calculatorObj.Insert(preceedIndex, new GeneralCalValue(preceedingNumber + proceedingNumber));

                        //removing
                        for (int i = 0; i <= number_of_object; i++)
                        {
                            int del = preceedIndex + 1;
                            calculatorObj.RemoveAt(del);
                        }

                        //reset searching 
                        searchingIndex = -1;
                    }

                    //contiune or break the searching loop
                    searchingIndex++;
                }

                return calculatorObj;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error...0x01\n" + "decimal loop error\n" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Convert adjacent one object numbers into one general value number </summary>
        /// <param name="calculatorObj">
        /// @return </param>
        private List<GeneralCalButton> numberLoop(List<GeneralCalButton> calculatorObj)
        {
            try
            {
                int searchingIndex = 0;
                while (searchingIndex < calculatorObj.Count)
                {
                    if (calculatorObj[searchingIndex] is CalNumber)
                    {

                        //count adjacent numbers
                        int last_number = searchingIndex;
                        while (calculatorObj[last_number] is CalNumber)
                        {
                            last_number++;
                            if (last_number == calculatorObj.Count)
                            {
                                break;
                            }
                        }

                        double value = 0;
                        const int BASE = 10;
                        for (int i = searchingIndex; i < last_number; i++)
                        {
                            value *= BASE;
                            value += ((CalNumber)calculatorObj[i]).Value;
                        }

                        calculatorObj.Insert(searchingIndex, new GeneralCalValue(value));
                        //delete existing CalNumbers
                        for (int i = searchingIndex + 1; i <= last_number; i++)
                        {
                            int del = searchingIndex + 1;
                            calculatorObj.RemoveAt(del);
                        }

                        //reset search loop
                        searchingIndex = 0;
                        continue;
                    }

                    searchingIndex++;
                }

                return calculatorObj;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error...0x2\n" + "number loop error\n" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// This method finds an operator and calculate the value from left and right values. </summary>
        /// <param name="calculatorObj">
        /// @return </param>
        private CalAns operatorLoop(List<GeneralCalButton> calculatorObj)
        {
            try
            {

                //loop through emumeration of operators to mimic bodmas
                foreach (EnumCalObjs @operator in EnumCalObjs.values())
                {

                    int searchingIndex = 0;
                    while (searchingIndex < calculatorObj.Count)
                    {
                        if (calculatorObj[searchingIndex] is GeneralCalOperator)
                        {
                            GeneralCalOperator opr = ((GeneralCalOperator)calculatorObj[searchingIndex]);
                            if (opr.Type == @operator)
                            {

                                try
                                {
                                    //get and calculate values
                                    GeneralCalValue gv1 = (GeneralCalValue)calculatorObj[searchingIndex - 1];
                                    if (!opr.usesOneNumber())
                                    {

                                        GeneralCalValue gv2 = (GeneralCalValue)calculatorObj[searchingIndex + 1];

                                        opr.Number1 = gv1.Value;
                                        opr.Number2 = gv2.Value;
                                    }
                                    else
                                    {

                                        opr.Number1 = gv1.Value;
                                        opr.Number2 = gv1.Value;
                                    }

                                    GeneralCalValue gv = opr.calculate();

                                    if (gv is CalAns)
                                    {
                                        Console.Error.WriteLine("Error...0x3000 Calculation Error");
                                        return (CalAns)gv;
                                    }

                                    //replace expression
                                    calculatorObj.Insert(searchingIndex - 1, gv);
                                    calculatorObj.RemoveAt(searchingIndex); //num1
                                    calculatorObj.RemoveAt(searchingIndex); //operator
                                    if (!opr.usesOneNumber())
                                    {
                                        calculatorObj.RemoveAt(searchingIndex); //num2
                                    }

                                    //reset search
                                    searchingIndex = -1;

                                }
                                catch (Exception)
                                {
                                    Console.Error.WriteLine("Error...0x4\n" + "operator loop error\ncalculatorObj.size()=" + calculatorObj.Count);
                                    CalAns ans = new CalAns();
                                    ans.Error = EnumError.SYNTAX;
                                    return ans;
                                }
                            }

                        }
                        searchingIndex++;
                    }
                }
                try
                {
                    double ans = ((GeneralCalValue)calculatorObj[0]).Value;
                    return new CalAns(ans);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Error...0x3\n" + "operator loop error: last object not an answer\n" + ex.Message);
                }

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error...0x3\n" + "operator loop error\n" + ex.Message);
            }
            CalAns a = new CalAns(0);
            a.Error = EnumError.SYNTAX;
            return a;
        }

        /// <summary>
        /// This method finds the inner most bracket and calculate the solution to the expression </summary>
        /// <param name="calculatorObj">
        /// @return </param>
        private List<GeneralCalButton> bracketLoop(List<GeneralCalButton> calculatorObj)
        {
            try
            {

                int searching = 0;
                while (searching < calculatorObj.Count)
                {
                    if (calculatorObj[searching] is CalBracketOpen)
                    {
                        const int NONE = -1;
                        int last_opening_bracket = NONE;
                        int first_closing_bracket = NONE;

                        //get index of innermost opening bracket
                        for (int i = 0; i < calculatorObj.Count; i++)
                        {
                            if (calculatorObj[i] is CalBracketOpen)
                            {
                                last_opening_bracket = i;
                            }
                        }

                        //get index of first closing bracket
                        int closing_bracket_search = last_opening_bracket;
                        while (closing_bracket_search < calculatorObj.Count)
                        {

                            if (calculatorObj[closing_bracket_search] is CalBracketClose)
                            {
                                first_closing_bracket = closing_bracket_search;

                                //break this local loop
                                closing_bracket_search = calculatorObj.Count;
                            }
                            closing_bracket_search++;
                        }

                        //check if brackets are in pairs
                        if ((first_closing_bracket != NONE) && (last_opening_bracket == NONE))
                        {
                            Console.Error.WriteLine("Error...0x06\n" + "Bracket Loop Error: No Opening Bracket\n");
                            return null;
                        }
                        //check if brackets are NOT ) (
                        if ((first_closing_bracket < last_opening_bracket))
                        {
                            Console.Error.WriteLine("Error...0x07\n" + "Bracket Loop Error: No Opening Bracket\n");
                            return null;
                        }
                        //check if brackets are in pairs
                        if ((first_closing_bracket == NONE) && (last_opening_bracket != NONE))
                        {
                            Console.Error.WriteLine("Error...0x08\n" + "Bracket Loop Error: No Closing Bracket\n");
                            return null;
                        }

                        //get the expression inside the bracket
                        List<GeneralCalButton> mini_expression = new List<GeneralCalButton>();
                        mini_expression.Clear();
                        for (int i = last_opening_bracket + 1; i < first_closing_bracket; i++)
                        {
                            mini_expression.Add(calculatorObj[i]);
                        }

                        //calculate solution of the expression inside the bracket
                        GeneralCalValue gv = (GeneralCalValue)operatorLoop(mini_expression);

                        //replace brackets and exprssion with general value object
                        calculatorObj.Insert(last_opening_bracket, gv);
                        int number_of_items = first_closing_bracket - last_opening_bracket;
                        //removing objects
                        for (int i = 0; i <= number_of_items; i++)
                        {
                            int del = last_opening_bracket + 1;
                            calculatorObj.RemoveAt(del);
                        }

                        //reset loop
                        searching = -1;
                    }
                    searching++;
                }

                return calculatorObj;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error...0x05\n" + "Bracket Loop Error\n" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// This method finds repeating minus and addition signs and replaces them </summary>
        /// <param name="calculatorObj">
        /// @return </param>
        private List<GeneralCalButton> positiveAndNegativeLoop(List<GeneralCalButton> calculatorObj)
        {
            try
            {

                int searchingIndex = 0;
                while (searchingIndex < calculatorObj.Count - 1)
                {

                    if (calculatorObj[searchingIndex] is CalSubtract && (searchingIndex != calculatorObj.Count - 1))
                    {
                        try
                        {
                            //is the subtract/minus is the first object
                            if (searchingIndex == 0 && calculatorObj[searchingIndex + 1] is GeneralCalValue)
                            {
                                double value = ((GeneralCalValue)calculatorObj[searchingIndex + 1]).Value;
                                ((GeneralCalValue)calculatorObj[searchingIndex + 1]).Value = -value;
                                calculatorObj.RemoveAt(0);
                                continue;
                            }

                            //if subtract poses as a minus 
                            bool search_within_range = searchingIndex > 0 && searchingIndex < calculatorObj.Count - 1;

                            if (search_within_range)
                            {
                                bool left_object_is_potential_value = (calculatorObj[searchingIndex - 1] is GeneralCalValue) || (calculatorObj[searchingIndex - 1] is CalBracketClose);


                                bool right_object_is_value = calculatorObj[searchingIndex + 1] is GeneralCalValue;

                                if (!left_object_is_potential_value && right_object_is_value)
                                {
                                    double value = ((GeneralCalValue)calculatorObj[searchingIndex + 1]).Value;
                                    ((GeneralCalValue)calculatorObj[searchingIndex + 1]).Value = -value;
                                    calculatorObj.RemoveAt(searchingIndex);
                                }
                            } //if there are two minus objects
                            else if ((calculatorObj[searchingIndex + 1] is CalMinus) || (calculatorObj[searchingIndex + 1] is CalSubtract))
                            {
                                calculatorObj.Insert(searchingIndex, new CalAdd());
                                calculatorObj.RemoveAt(searchingIndex + 1); //minus
                                calculatorObj.RemoveAt(searchingIndex + 1); //subtraction

                                //reset search loop
                                searchingIndex = -1;
                            }
                            else if (calculatorObj[searchingIndex + 1] is CalAdd)
                            {
                                calculatorObj.RemoveAt(searchingIndex + 1); //remove addition
                                //reset search loop
                                searchingIndex = -1;
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine("Error...0x10\n" + "Positive and Negative Loop Error: No next object\n" + ex.Message);
                        }

                    }
                    else if (calculatorObj[searchingIndex] is CalMinus)
                    {
                        try
                        {
                            //if there are two minus objects 
                            if ((calculatorObj[searchingIndex + 1] is CalMinus))
                            {
                                calculatorObj.Insert(searchingIndex, new CalAdd());
                                calculatorObj.RemoveAt(searchingIndex + 1); //minus
                                calculatorObj.RemoveAt(searchingIndex + 1); //subtraction

                                //reset search loop
                                searchingIndex = -1;

                            }
                            else if ((calculatorObj[searchingIndex + 1] is GeneralCalValue))
                            {
                                //remove minus and negate value
                                double value = -((GeneralCalValue)calculatorObj[searchingIndex + 1]).Value;
                                ((GeneralCalValue)calculatorObj[searchingIndex + 1]).Value = value;
                                calculatorObj.RemoveAt(searchingIndex);

                                //reset search loop
                                searchingIndex = -1;
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine("Error..0x11\n" + "Positive and Negative Loop Error: No next object" + ex.Message);
                        }

                    }
                    else if (calculatorObj[searchingIndex] is CalAdd && (searchingIndex != calculatorObj.Count - 1))
                    {

                        if ((calculatorObj[searchingIndex + 1] is CalMinus) || (calculatorObj[searchingIndex + 1] is CalSubtract))
                        {
                            try
                            {
                                calculatorObj.Insert(searchingIndex, new CalSubtract());
                                calculatorObj.RemoveAt(searchingIndex + 1); //remove addition
                                calculatorObj.RemoveAt(searchingIndex + 1); //remove negative

                                //reset search loop
                                searchingIndex = -1;
                            }
                            catch (Exception ex)
                            {
                                Console.Error.WriteLine("Error..0x12\n" + "Positive and Negative Loop Error: No next object" + ex.Message);
                            }

                        }
                        else if ((calculatorObj[searchingIndex + 1] is CalAdd))
                        {
                            try
                            {
                                calculatorObj.RemoveAt(searchingIndex); //remove one addition
                                //reset search loop
                                searchingIndex = -1;
                            }
                            catch (Exception ex)
                            {
                                Console.Error.WriteLine("Error..0x13\n" + "Positive and Negative Loop Error: No next object" + ex.Message);
                            }
                        }
                    }

                    //continue loop
                    searchingIndex++;
                }
                return calculatorObj;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error..0x09\n" + "Positive and Negative Loop Error" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// This methods finds the three time point objects and converts the adjacent time values into a single value </summary>
        /// <param name="calculatorObjs">
        /// @return </param>
        private List<GeneralCalButton> timeLoop(List<GeneralCalButton> calculatorObjs)
        {
            try
            {
                int searchingIndex = 0;
                int number_of_found_time_points = 0;
                while (searchingIndex < calculatorObjs.Count)
                {

                    //if still count time points
                    if ((searchingIndex == calculatorObjs.Count - 1) && (!(calculatorObjs[searchingIndex] is CalTime)) && (number_of_found_time_points != 2 && number_of_found_time_points != 0) && calculatorObjs.Count > 1)
                    {
                        Console.Error.WriteLine("Error...0x19000 Time Loop Syntax");
                        return null;
                    }
                    else if (calculatorObjs[searchingIndex] is CalTime)
                    {
                        CalTime calTime = (CalTime)calculatorObjs[searchingIndex];
                        number_of_found_time_points++;

                        //set preceed value
                        if (calculatorObjs[searchingIndex - 1] is GeneralCalValue)
                        {
                            GeneralCalValue gv = (GeneralCalValue)calculatorObjs[searchingIndex - 1];

                            //if the value is positive
                            if (gv.Value > 0)
                            {
                                if (number_of_found_time_points == 1)
                                {
                                    calTime.Hours = gv;
                                }
                                else
                                { //the object before the general value must be a time point
                                    if (calculatorObjs[searchingIndex - 2] is CalTime)
                                    {

                                        if (number_of_found_time_points == 2)
                                        {
                                            calTime.Minutes = gv;
                                        }
                                        else if (number_of_found_time_points == 3)
                                        {
                                            calTime.Seconds = gv;

                                            //get calculated answer
                                            gv = calTime.calculate();

                                            //add calculated time
                                            int time_expression_length = 6; //#t #t #t
                                            calculatorObjs.Insert(searchingIndex - (time_expression_length - 1), gv); //add answer

                                            //remove time expression
                                            for (int i = 1; i <= time_expression_length; i++)
                                            {
                                                int del = searchingIndex - time_expression_length + 2;
                                                calculatorObjs.RemoveAt(del);
                                            }

                                            //reset function
                                            searchingIndex = -1;
                                            number_of_found_time_points = 0;
                                        }
                                    }
                                    else
                                    {
                                        Console.Error.WriteLine("Error...0x16000 Time Loop Syntax Error Brackets");
                                        return null;
                                    }
                                }
                            }

                        } //find and evaluate bracket expression then set value
                        else if (calculatorObjs[searchingIndex - 1] is CalBracketClose)
                        {
                            int number_of_closed_brackets = 0;
                            int number_of_open_brackets = 0;
                            int start_index = -1;
                            int end_index = searchingIndex - 1;

                            //find indices to encapsulate brackets
                            for (int i = searchingIndex - 1; i >= 0; i--)
                            {
                                if (calculatorObjs[i] is CalBracketOpen)
                                {
                                    number_of_open_brackets++;
                                }
                                else if (calculatorObjs[i] is CalBracketClose)
                                {
                                    number_of_closed_brackets++;
                                }
                                if (number_of_closed_brackets == number_of_open_brackets)
                                {
                                    start_index = i;
                                    break;
                                }
                            }

                            if (start_index != -1)
                            {
                                //calculate bracket expression
                                List<GeneralCalButton> local_expression = new List<GeneralCalButton>();

                                for (int i = start_index; i <= end_index; i++)
                                {
                                    local_expression.Add(calculatorObjs[i]); //add
                                }

                                //get local solution
                                GeneralCalValue gv = calculateSolution(local_expression);

                                //add value 
                                calculatorObjs.Insert(start_index, gv);

                                //remove bracket expression
                                for (int i = start_index; i <= end_index; i++)
                                {
                                    int del = start_index + 1;
                                    calculatorObjs.RemoveAt(del);
                                }

                                //reset function
                                searchingIndex = -1;
                                number_of_found_time_points = 0;
                                local_expression.Clear();

                            }
                            else
                            {
                                Console.Error.WriteLine("Error...0x17000 Time Loop Syntax Error Brackets");
                                return null;
                            }

                        }
                        else
                        {
                            Console.Error.WriteLine("Error...0x18000 Time Loop Syntax Error");
                            return null;
                        }
                    }
                    searchingIndex++;
                }

                return calculatorObjs;

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error...0x15000 Time Loop Error\n" + ex.Message);
                return null;
            }

        }

        /// <summary>
        /// This methods finds the inner most function object and its corresponding
        /// parameters and evaluates the expression. </summary>
        /// <param name="calculatorObj">
        /// @return </param>
        private List<GeneralCalButton> functionLoop(List<GeneralCalButton> calculatorObj)
        {

            int searchingIndex = 0;
            int firstOpenBracket = 0;
            int lastClosingBracket = calculatorObj.Count - 1;

            try
            {

                while (searchingIndex <= lastClosingBracket)
                {
                    if (calculatorObj[searchingIndex] is GeneralCalFunction)
                    {

                        //if generalvalue is the next object
                        if (calculatorObj[searchingIndex + 1] is GeneralCalValue)
                        {

                            //get the proceed value and calculate the one parametic function
                            GeneralCalFunction fnx = (GeneralCalFunction)calculatorObj[searchingIndex];

                            //if value value is being amplified by power, factorial or base
                            if (searchingIndex + 3 <= calculatorObj.Count - 1)
                            {

                                //if the next object is amplifying the number
                                if (calculatorObj[searchingIndex + 2] is CalPow || calculatorObj[searchingIndex + 2] is CalFactorial || calculatorObj[searchingIndex + 2] is CalBase10)
                                {

                                    //if the very next object is a value
                                    if (calculatorObj[searchingIndex + 3] is GeneralCalValue)
                                    {

                                        //evaluate for single value
                                        GeneralCalOperator opr = (GeneralCalOperator)calculatorObj[searchingIndex + 2];
                                        GeneralCalValue value1 = (GeneralCalValue)calculatorObj[searchingIndex + 1];
                                        GeneralCalValue value2 = (GeneralCalValue)calculatorObj[searchingIndex + 3];
                                        opr.Number1 = value1.Value;
                                        opr.Number2 = value2.Value;

                                        try
                                        {
                                            //calculate
                                            GeneralCalValue gv = opr.calculate();

                                            //replace values and objects
                                            calculatorObj.Insert(searchingIndex + 1, gv);
                                            calculatorObj.RemoveAt(searchingIndex + 2); //remove v1
                                            calculatorObj.RemoveAt(searchingIndex + 2); //remove operator that amplifies number
                                            calculatorObj.RemoveAt(searchingIndex + 2); //remove v2
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.Error.WriteLine("Error...0x14000 Function Loop Operator Calculations\n" + ex.Message);
                                            return null;
                                        }

                                    }
                                }
                            }

                            //add parameter to function
                            fnx.addParameter((GeneralCalValue)calculatorObj[searchingIndex + 1]);

                            if (fnx.parameterCountIsOK())
                            {

                                try
                                {

                                    GeneralCalValue value = fnx.calculate();

                                    calculatorObj.Insert(searchingIndex, value);

                                    calculatorObj.RemoveAt(searchingIndex + 1); //remove the function object

                                    calculatorObj.RemoveAt(searchingIndex + 1); //remove the value

                                }
                                catch (Exception ex)
                                {
                                    //return syntax error
                                    Console.Error.WriteLine("Error...0x13000 Function Loop Error\n" + ex.Message);
                                    return null;
                                }

                                /*
                                The class seems to store the same parameters for other functions. 
                                There is a need to clear the memory
                                 */
                                fnx.clearParameterMemory();

                                //reset
                                firstOpenBracket = 0;
                                lastClosingBracket = calculatorObj.Count - 1;
                                searchingIndex = -1;

                            }
                            else
                            {
                                //return syntax error
                                Console.Error.WriteLine("Error...0x12000 Function Loop Error");
                                return null;
                            }

                            //if an open bracket is the next object
                        }
                        else if (calculatorObj[searchingIndex + 1] is CalBracketOpen)
                        {
                            firstOpenBracket = searchingIndex + 1;
                            int numberOfOpenedBracket = 0;
                            int numberOfClosedBracket = 0;

                            for (int i = firstOpenBracket; i <= lastClosingBracket; i++)
                            {

                                //count the number of brackets
                                if (calculatorObj[i] is CalBracketOpen)
                                {
                                    numberOfOpenedBracket++;
                                }
                                else if (calculatorObj[i] is CalBracketClose)
                                {
                                    numberOfClosedBracket++;
                                }

                                //check if the equal there are equal number of coresponding brackets
                                if (numberOfOpenedBracket == numberOfClosedBracket)
                                {
                                    lastClosingBracket = i;
                                    break;
                                }
                            }

                        }
                    }
                    else
                    {
                        //function expression limits where found 
                        if (firstOpenBracket != 0)
                        {

                            //check if there is a inner function
                            bool innerFunctionExists = false;
                            for (int i = firstOpenBracket; i <= lastClosingBracket; i++)
                            {
                                if (calculatorObj[i] is GeneralCalFunction)
                                {
                                    innerFunctionExists = true;
                                }
                            }
                            if (!innerFunctionExists)
                            {

                                //storage for all local parameter separators
                                List<int> parameterIndexList = new List<int>();

                                //add index of first open bracket
                                parameterIndexList.Add(firstOpenBracket);

                                for (int i = firstOpenBracket; i <= lastClosingBracket; i++)
                                {

                                    //get locations of all local parameter separators
                                    if (calculatorObj[i] is CalParameterSeparator)
                                    {
                                        parameterIndexList.Add(i);
                                    }
                                }
                                //add index of last closing bracket
                                parameterIndexList.Add(lastClosingBracket);

                                //split the parameter into a list base on the indices
                                GeneralCalButton[][] parameters = new GeneralCalButton[parameterIndexList.Count - 1][];

                                for (int i = 0; i < parameterIndexList.Count; i++)
                                {

                                    int last_but_one = parameterIndexList.Count - 2;

                                    //calculate number of objects in the between the separators
                                    int number_of_objects = parameterIndexList[i + 1] - parameterIndexList[i] - 1;

                                    GeneralCalButton[] expression = new GeneralCalButton[number_of_objects];

                                    //get expression for parameter
                                    int index = 0;
                                    for (int j = parameterIndexList[i] + 1; j < parameterIndexList[i + 1]; j++)
                                    {
                                        expression[index++] = calculatorObj[j];
                                    }

                                    //add expression to list
                                    parameters[i] = expression;

                                    if (i == last_but_one)
                                    {
                                        break;
                                    }
                                }

                                //calculate function solution
                                GeneralCalFunction fnx = (GeneralCalFunction)calculatorObj[firstOpenBracket - 1];

                                foreach (GeneralCalButton[] parameter in parameters)
                                {
                                    GeneralCalValue gv = calculateSolution(parameter);
                                    fnx.addParameter(gv);
                                }
                                if (fnx.parameterCountIsOK())
                                {
                                    calculatorObj.Insert(firstOpenBracket - 1, fnx.calculate());

                                    //remove calculator objects
                                    for (int i = firstOpenBracket - 1; i <= lastClosingBracket; i++)
                                    {
                                        int del = firstOpenBracket;
                                        calculatorObj.RemoveAt(del);
                                    }

                                }
                                else
                                {
                                    //return syntax error
                                    Console.Error.WriteLine("Error...0x10000 Function Loop Error");
                                    return null;
                                }

                                //reset
                                firstOpenBracket = 0;
                                lastClosingBracket = calculatorObj.Count - 1;
                                searchingIndex = -1;

                                //clear memory
                                parameterIndexList.Clear();
                                fnx.clearParameterMemory();
                            }
                        }
                    }
                    //update loop
                    searchingIndex++;
                }

                return calculatorObj;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error...0x5000 Function Loop Error " + ex.Message);
            }
            return null;
        }

        private List<GeneralCalButton> bracketAbsLoop(List<GeneralCalButton> calculatorObj)
        {

            try
            {

                int number_of_abs_brackets = 0;
                for (int i = 0; i < calculatorObj.Count; i++)
                {
                    if (calculatorObj[i] is CalAbsBracket)
                    {
                        number_of_abs_brackets++;
                    }
                }

                //process if the abs bracket count is an even number
                if ((number_of_abs_brackets % 2) == 0)
                {

                    if (number_of_abs_brackets != 0)
                    {
                        //indices for bracket encapsulation
                        int last_open_bracket = -1;
                        int first_closed_bracket = -1;
                        int searching_index = 0;
                        int bracket_count = 0;

                        //search for brackets
                        while (searching_index < calculatorObj.Count)
                        {
                            if (calculatorObj[searching_index] is CalAbsBracket)
                            {
                                bracket_count++;

                                //when the last open bracket found
                                if ((number_of_abs_brackets / 2) == bracket_count)
                                {
                                    last_open_bracket = searching_index;

                                    //find the corresponding closing bracket
                                    for (int i = searching_index; i < calculatorObj.Count; i++)
                                    {
                                        if (calculatorObj[i] is CalAbsBracket)
                                        {
                                            first_closed_bracket = i;
                                        }
                                    }

                                    //if both indices are not -1
                                    if (!(last_open_bracket == -1 || first_closed_bracket == -1))
                                    {

                                        //get local expression and calculate
                                        List<GeneralCalButton> local_expression = new List<GeneralCalButton>();
                                        for (int i = last_open_bracket + 1; i < first_closed_bracket; i++)
                                        {
                                            local_expression.Add(calculatorObj[i]);
                                        }

                                        //caculate expression
                                        GeneralCalValue gv = calculateSolution(functionLoop(local_expression));

                                        //makea a negative answer positive
                                        if (gv.Value < 0)
                                        {
                                            gv.Value = gv.Value * -1;
                                        }

                                        //add answer to list
                                        calculatorObj.Insert(last_open_bracket, gv);

                                        //remove bracket expression
                                        for (int i = last_open_bracket + 1; i <= first_closed_bracket; i++)
                                        {
                                            int del = last_open_bracket + 1;
                                            calculatorObj.RemoveAt(del);
                                        }

                                        //reset 
                                        number_of_abs_brackets -= 2; //remove the two brackets
                                        searching_index = -1;
                                        first_closed_bracket = -1;
                                        last_open_bracket = -1;
                                        local_expression.Clear();
                                    }
                                }
                            }

                            //continue loop 
                            searching_index++;
                        }

                    }

                }
                else
                {
                    Console.Error.WriteLine("Error...0x21000 Bracket Absolute Loop Error: Absolute Bracket Count are not even" + number_of_abs_brackets);
                    return null;
                }

                return calculatorObj;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error...0x20000 Bracket Absolute Loop Error\n" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// This method converts the string expression to calculator objects for calculations. </summary>
        /// <param name="expression"> The expression to calculate. </param>
        /// <returns> Returns if a boolean indicating if expression was converted successfully  </returns>
        public bool setExpression(string expression)
        {
            //remove all white space
            expression = expression.Replace(" ", "");

            clearMemory();

            for (int i = 0; i < expression.Length; i++)
            {
                foreach (EnumCalObjs calculatorObj in EnumCalObjs.values())
                {

                    int index1 = expression.IndexOf(calculatorObj.DisplayText1);
                    int index2 = expression.IndexOf(calculatorObj.DisplayText2);

                    bool the_object_is_first = (index1 == 0 || index2 == 0);

                    if (the_object_is_first)
                    {
                        calObjs.Add(calculatorObj.CalculateObject);

                        //get length
                        int length_of_finding_text;
                        if (index1 == 0)
                        {
                            length_of_finding_text = calculatorObj.DisplayText1.Length;
                        }
                        else
                        {
                            length_of_finding_text = calculatorObj.DisplayText2.Length;
                        }

                        //truncate beginning of the text
                        expression = expression.Substring(length_of_finding_text, expression.Length - length_of_finding_text);

                        //reset for loop
                        i = -1;
                    }
                }
            }

            //error in converting string
            if (expression.Length > 0)
            {
                Console.Error.WriteLine("Error...0x16\nCalQl8R::SetExpression Method Error\n");
                CalAns a = new CalAns(0);
                a.Error = EnumError.SYNTAX;
                clearMemory();
                calObjs.Add(a);
                return false;
            }

            return true;

        }

        public string Answer
        {
            get
            {
                const int NO_LIMITS_TO_SIG_FIGS = -1;
                return getAnswer(NO_LIMITS_TO_SIG_FIGS);
            }
        }

        public string getAnswer(int number_of_dps)
	    {
		        if (answer.Error == EnumError.MATH)
		            {
			        return EnumError.MATH.TEXT;
		            }
		        else if (answer.Error == EnumError.SYNTAX)
		            {
			            return EnumError.SYNTAX.TEXT;
		            }

		//round off ans value to remove unnecessary trailing values
		//get base on answer foramt
			switch (answer.AnswerFormat)
			{
                case Enums.EnumAnswerFormat.VALUE:
					//round off ans value to remove unnecessary trailing values
					string ansText;
					if (number_of_dps == -1)
					{
						ansText = answer.Value.ToString();
					}
					else
					{
						ansText = answer.getValue(number_of_dps).ToString();
					}
					return ansText;
                case Enums.EnumAnswerFormat.FRACTION:
					return answer.AnswerFraction;
                case Enums.EnumAnswerFormat.TIME:
					return answer.AnswerTime;
				default:
					break;
			}
		
		return "0";
	}

        /// <summary>
        /// This method change the form of the answer in a time form. *Set after the calculator has already run*
        /// </summary>
        public void setAnswerFormatToTime()
        {
            answer.AnswerFormat = EnumAnswerFormat.TIME;
        }

        /// <summary>
        /// This method change the form of the answer in a fraction form. For instance 1.5 to 1/1/2. *Set after the calculator has already run*
        /// </summary>
        public void setAnswerFormatToFraction()
        {
            answer.AnswerFormat = EnumAnswerFormat.FRACTION;
        }

    }
}