using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SStest.Models.Product
{
    public class Star
    {
       
            public int Comment_ID { get; set; }
            public int Member_ID { get; set; }
            public int Product_ID { get; set; }
            public string Comment1 { get; set; }
            public virtual ICollection<Comment> comment{ get; set; }
            [NotMapped]
            //[UIHint("Rating")]
            public int Stars
            {
                get
                {
                    if (comment != null && comment.Count > 0)
                    {
                        double avg = comment.Average(r => r.Stars.Value);
                        if (avg % 1 > 0.0 && avg % 1 > 0.5)
                            return (int)Math.Ceiling(avg);
                        else
                            return (int)Math.Floor(avg);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            public System.DateTime Date { get; set; }

            public virtual Members Members { get; set; }
            public virtual Products Products { get; set; }
      
    }
}