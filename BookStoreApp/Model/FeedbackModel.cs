using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class FeedbackModel
    {
        public int feedbackId { get; set; }
        public string feedback { get; set; }
        public int bookId { get; set; }
        public float rating { get; set; }
        public int userId { get; set; }
    }
}
