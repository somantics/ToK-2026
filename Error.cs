namespace ToK_2026
{
    //define my error here

    public class InputException : Exception
    {

        public InputException(string message)
            : base (message)
        {
        }
        public InputException(string message, Exception inner)
            : base (message, inner)
        {
        }

    }
}