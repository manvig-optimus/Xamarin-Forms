using DocumentManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Models
{
    public class MainPageMenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Type TargetType { get; set; }
    }
}
