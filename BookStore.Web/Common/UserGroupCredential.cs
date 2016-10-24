using System;

namespace BookStore.Web.Common
{
    [Serializable]
    public class UserGroupCredential
    {
        public string ID { set; get; }
        public string Name { set; get; }
    }
}