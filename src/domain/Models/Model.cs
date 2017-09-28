namespace domain.Models
{
    public abstract class Model : Disposable
    {
        public long Id { get; set; }

        public abstract bool IsValid();

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Type: {0} | Id: {1}", this.GetType().Name, this.Id);
        }

        public override bool Equals(object obj)
        {
            return this.ToString().Equals(obj.ToString());
        }
    }
}