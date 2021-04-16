
using System.Collections.Generic;

namespace BatchHttpTools.Domain{

    public class HttpCallList{
        private List<HttpCall> _httpCalls { get; set; }
        public string OutPutFolder { get; set; }
        public List<HttpCall> Calls
        {
            get
            {
                if (_httpCalls == null)
                    _httpCalls = new List<HttpCall>();
                return _httpCalls;
            }
            set
            {
                _httpCalls = value;
            }
        }

    }
}