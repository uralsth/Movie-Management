namespace Common.Lib
{
    public class OperationResponse<T>
    {

        public List<string> ErrorMessage { get; } = new List<string>();
        public void AddError(string Message)
        {
            ErrorMessage.Add(Message);
        }
        public void AddError(List<string> Messages)
        {
            ErrorMessage.AddRange(Messages);
        }
        public bool IsSucess { get { return ErrorMessage.Count() == 0; } }
        
        public T Result { get; set; }
    }
}