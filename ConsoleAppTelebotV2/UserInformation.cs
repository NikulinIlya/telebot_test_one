using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTelebotV2
{
    class UserInformation
    {
        public UserInformation()
        {
            CurrentResponseIndex = 0;
            index = 0;
            TotalAnswer = 0;
        }

        int index;

        public int TotalAnswer { get; set; }

        public int CurrentResponseIndex
        {
            get { return index; }
            set { index = value % Repository.Count; }
        }
    }
}
