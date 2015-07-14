using System;
using System.Web.Mvc;
using Bespoke.Sph.Domain;
using System.Threading.Tasks;
using System.Linq;

namespace web.sph.App_Code
{
      public class JpaHomeViewModel
     {
         public Designation Designation { get; set; }
         public UserProfile Profile { get; set; }
         public string StartModule { get; set; }
         public int TotalMessageCount { get; set; }
         private readonly ObjectCollection<Message> m_itemCollection = new ObjectCollection<Message>();

         public ObjectCollection<Message> Messages
         {
             get { return m_itemCollection; }
         }
     }
}
