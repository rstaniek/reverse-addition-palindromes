using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palindromes
{
    /// <summary>
    /// @Author: Rajmund Staniek
    /// </summary>
    /// <see cref="http://rajmundstaniek.info"/>
    class Palindrome
    {
        private List<int> _initialNum, _palindrome;
        private int _numOfSteps;
        private int _maxNumOfSteps = 1000;

        /// <summary>
        /// Shows the number the class was initiated with
        /// </summary>
        public string InitialNumber
        {
            get { return _lts(_initialNum); }
            private set { }
        }
        /// <summary>
        /// Shows the amunt of steps the algorithm iterated through to achieve the expected outcome (palindrome)
        /// </summary>
        public int StepsTaken { get { return _numOfSteps; } private set { } }
        /// <summary>
        /// Final number that should be a palindrome
        /// </summary>
        public string Outcome
        {
            get { return _lts(_palindrome); }
            private set { }
        }
        /// <summary>
        /// Property regulating the max amount of iterations to prevent endless loops
        /// </summary>
        public int MaxSteps { get { return _maxNumOfSteps; } set { _maxNumOfSteps = value; } }

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="number"></param>
        public Palindrome(string number)
        {
            _initialNum = _stl(number);
            _palindrome = new List<int>();
        }

        /// <summary>
        /// Private method that determines whether a given number stored in a List<int> form is a palindrome.
        /// This means that the number first and last digit and following to the middle of the number are the same.
        /// </summary>
        /// <param name="data">A list containing digits of a number you want to check.</param>
        /// <returns>True if palindrome, False otherwise</returns>
        private bool _isPalindrome(List<int> data)
        {
            for(int i = 0; i <= data.Count / 2; i++)
            {
                if (!data.ElementAt(i).Equals(data.ElementAt(data.Count - (i + 1)))) return false;
            }
            return true;
        }

        /// <summary>
        /// Static initializer
        /// </summary>
        /// <param name="l">Takes a long type as a parameter</param>
        /// <returns></returns>
        public static Palindrome FromNumeric(long l)
        {
             return new Palindrome(Convert.ToString(l));
        }
        /// <summary>
        /// Static initializer
        /// </summary>
        /// <param name="i">Takes an int as a parameter</param>
        /// <returns></returns>
        public static Palindrome FromNumeric(int i)
        {
            return new Palindrome(Convert.ToString(i));
        }

        /// <summary>
        /// Private method converting number from a list to its string representation.
        /// </summary>
        /// <param name="list">List containing digits of a number</param>
        /// <returns>A number parsed to string</returns>
        private string _lts(List<int> list)
        {
            string temp = "";
            foreach (var e in list)
            {
                temp += Convert.ToString(e);
            }
            return temp;
        }

        /// <summary>
        /// Private method converting a string representation of a number into a list containing its digits.
        /// 
        /// WARNING!!!
        /// Only positive numbers for that matter allowed.
        /// 
        /// 
        /// </summary>
        /// <param name="str">String representation of a natural number</param>
        /// <returns>List of digits</returns>
        private List<int> _stl(string str)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < str.Length; i++)
            {
                list.Add(Convert.ToInt32(str.ElementAt(i)) - 48);
            }
            return list;
        }

        /// <summary>
        /// Method that adds a number to its reversed copy
        /// 
        /// Example:
        /// AddReverse(241) = 383:
        ///     241 + 142 = 383
        /// </summary>
        /// <param name="list">a number in its list representation</param>
        /// <returns>a result of reverse addition</returns>
        public static List<int> AddReverse(List<int> list)
        {
            List<int> reversed = new List<int>(list);
            reversed.Reverse();
            List<int> result = new List<int>();
            int remainder = 0;
            for(int i = 0; i < list.Count; i++)
            {
                int temp = list.ElementAt(i) + reversed.ElementAt(i) + remainder;
                if (temp < 10)
                {
                    result.Add(temp);
                    remainder = 0;
                }
                else
                {
                    result.Add(temp % 10);
                    remainder = temp / 10;
                }
            }
            if (remainder != 0) result.Add(remainder);
            result.Reverse();
            return result;
        }

        /// <summary>
        /// Initializes the check for how much iterations are needed to convert a number to a palindrome through revesed addition
        /// </summary>
        /// <returns>Number of iterations. If equals -1 then the algorithm has ran out of bounds.</returns>
        public int Run(RunMode mode = RunMode.Standard)
        {
            _numOfSteps = 0;
            List<int> number = new List<int>(_initialNum);
            while (!_isPalindrome(number))
            {
                number = AddReverse(number);
                _numOfSteps++;
                if (mode.Equals(RunMode.Verbose)) Console.WriteLine("[Test of: {0}] Iteration: {1}; after reverse addition: {2}", InitialNumber, _numOfSteps, _lts(number));
                if (_numOfSteps > _maxNumOfSteps) return -1; //throw new ApplicationException(String.Format("Amount of steps has exceeded {0}. In order to avoid indefinite loop execution of this method has stopped.", MaxSteps));
            }
            _palindrome = number;
            return _numOfSteps;
        }

        public enum RunMode
        {
            Verbose,
            Standard
        }
    }
}
