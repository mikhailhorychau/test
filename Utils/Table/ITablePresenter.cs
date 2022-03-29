using System.Collections.Generic;

namespace UIScripts.Table
{
    public interface ITablePresenter<TBodyProps>
    {
        public List<TBodyProps> BodyProps { get; set; }
    }
}