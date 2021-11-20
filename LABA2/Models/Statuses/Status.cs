namespace LABA2.Models.Statuses
{
    public abstract class Status
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public Status()
        {
        }

        public abstract bool IsError();
    }
}