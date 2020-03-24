using System;

namespace RestAPIsApplication.Utilities
{
    [Serializable]
    public class UserParseException : Exception
    {
        public UserParseException()
        {
            Console.WriteLine("Errors occured when trying to parse the user. Please contact an admin");
        }
        public UserParseException(string errorousValue)
        {
            Console.WriteLine("Error occured trying to parse value: " + errorousValue + ".");
        }
    }
}